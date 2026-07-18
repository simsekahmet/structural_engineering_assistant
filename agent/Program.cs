using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;

namespace StructuralEngineeringAssistant.Agent;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        try
        {
            AgentLog.Write("Agent starting.");
            ApplicationConfiguration.Initialize();
            Application.Run(new AgentApplicationContext());
            AgentLog.Write("Agent stopped normally.");
        }
        catch (Exception ex)
        {
            AgentLog.Write(ex.ToString());
            MessageBox.Show(ex.ToString(), "Structural Engineering Assistant Agent",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

internal sealed class AgentApplicationContext : ApplicationContext
{
    private readonly Control _dispatcher = new();
    private readonly EtabsConnection _etabs = new();
    private readonly LocalBridgeServer _server;
    private readonly NotifyIcon _trayIcon;

    public AgentApplicationContext()
    {
        AgentLog.Write("Application context initializing.");
        _dispatcher.CreateControl();
        _ = _dispatcher.Handle;
        AgentLog.Write("UI dispatcher created.");

        var menu = new ContextMenuStrip();
        menu.Items.Add("ETABS'a Bağlan / Connect", null, (_, _) => ShowConnectionResult());
        menu.Items.Add("Durumu Göster / Show Status", null, (_, _) => ShowStatus());
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Çıkış / Exit", null, (_, _) => ExitThread());

        _trayIcon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Text = "Structural Engineering Assistant Agent",
            ContextMenuStrip = menu,
            Visible = true
        };
        _trayIcon.DoubleClick += (_, _) => ShowConnectionResult();
        AgentLog.Write("Tray icon created.");

        _server = new LocalBridgeServer(GetSnapshotOnUiThread);
        try
        {
            _server.Start();
            AgentLog.Write("Local bridge listening on 127.0.0.1:5218.");
            _trayIcon.ShowBalloonTip(2500, "Structural Engineering Assistant",
                "Windows agent hazır. ETABS modelini açıp web sitesinden bağlanabilirsiniz.",
                ToolTipIcon.Info);
        }
        catch (Exception ex)
        {
            AgentLog.Write(ex.ToString());
            MessageBox.Show(
                $"Yerel köprü başlatılamadı / Local bridge could not start:\n\n{ex.Message}",
                "Structural Engineering Assistant Agent",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            ExitThread();
        }
    }

    private EtabsSnapshot GetSnapshotOnUiThread()
    {
        if (_dispatcher.InvokeRequired)
            return (EtabsSnapshot)_dispatcher.Invoke(new Func<EtabsSnapshot>(_etabs.ConnectAndRead));

        return _etabs.ConnectAndRead();
    }

    private void ShowConnectionResult()
    {
        var snapshot = _etabs.ConnectAndRead();
        var message = snapshot.EtabsConnected
            ? $"ETABS bağlantısı kuruldu.\nConnected to ETABS.\n\nModel: {snapshot.ModelName}"
            : "Açık bir ETABS modeli bulunamadı.\nNo open ETABS model was found.";

        MessageBox.Show(message, "Structural Engineering Assistant Agent",
            MessageBoxButtons.OK, snapshot.EtabsConnected ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
    }

    private void ShowStatus()
    {
        var snapshot = _etabs.ReadCurrent();
        MessageBox.Show(
            snapshot.EtabsConnected
                ? $"Agent: Online\nETABS: Connected\nModel: {snapshot.ModelName}"
                : "Agent: Online\nETABS: Not connected",
            "Structural Engineering Assistant Agent",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    protected override void ExitThreadCore()
    {
        _server.Dispose();
        _etabs.Dispose();
        _trayIcon.Visible = false;
        _trayIcon.Dispose();
        _dispatcher.Dispose();
        base.ExitThreadCore();
    }
}

internal sealed class EtabsConnection : IDisposable
{
    private object? _etabsObject;
    private object? _sapModel;
    private Type? _sapModelInterface;

    public EtabsSnapshot ConnectAndRead()
    {
        if (TryRead(out var current))
            return current;

        ReleaseComObjects();

        try
        {
            if (TryConnectWithInstalledApi(out var typedEtabs, out var typedSapModel, out var sapModelInterface))
            {
                _etabsObject = typedEtabs;
                _sapModel = typedSapModel;
                _sapModelInterface = sapModelInterface;
                return TryRead(out var typedSnapshot)
                    ? typedSnapshot
                    : EtabsSnapshot.NotConnected("ETABS is running but no model is open.");
            }

            var etabsType = Type.GetTypeFromProgID("CSI.ETABS.API.ETABSObject", throwOnError: false);
            if (etabsType is null)
                return EtabsSnapshot.NotConnected("ETABS COM API is not registered. Install ETABS 22 or later.");

            var activeObject = TryGetFromRunningObjectTable();
            activeObject ??= TryGetFromEtabsHelper();

            if (activeObject is null)
            {
                var classId = etabsType.GUID;
                var result = NativeMethods.GetActiveObject(ref classId, IntPtr.Zero, out activeObject);
                if (result != 0 || activeObject is null)
                    return EtabsSnapshot.NotConnected("No running ETABS instance was found.");
            }

            Release(activeObject);
            return EtabsSnapshot.NotConnected("ETABS was found, but its typed API could not be loaded.");
        }
        catch (Exception ex)
        {
            ReleaseComObjects();
            return EtabsSnapshot.NotConnected(ex.Message);
        }
    }

    public EtabsSnapshot ReadCurrent() =>
        TryRead(out var snapshot) ? snapshot : EtabsSnapshot.NotConnected("Not connected.");

    private static bool TryConnectWithInstalledApi(out object? etabsObject, out object? sapModel, out Type? sapModelInterface)
    {
        etabsObject = null;
        sapModel = null;
        sapModelInterface = null;
        object? helper = null;

        try
        {
            var apiPath = FindInstalledEtabsApi();
            if (apiPath is null)
            {
                AgentLog.Write("ETABSv1.dll was not found under the ETABS installation folder.");
                return false;
            }

            AgentLog.Write($"Loading installed ETABS API: {apiPath}");
            var apiAssembly = System.Reflection.Assembly.LoadFrom(apiPath);
            var helperClass = apiAssembly.GetType("ETABSv1.Helper", throwOnError: true)!;
            var helperInterface = apiAssembly.GetType("ETABSv1.cHelper", throwOnError: true)!;
            var etabsInterface = apiAssembly.GetType("ETABSv1.cOAPI", throwOnError: true)!;
            sapModelInterface = apiAssembly.GetType("ETABSv1.cSapModel", throwOnError: true)!;

            helper = Activator.CreateInstance(helperClass);
            if (helper is null) return false;

            var getObject = helperInterface.GetMethod("GetObject", new[] { typeof(string) });
            etabsObject = getObject?.Invoke(helper, new object[] { "CSI.ETABS.API.ETABSObject" });
            if (etabsObject is null) return false;

            var sapModelProperty = etabsInterface.GetProperty("SapModel");
            sapModel = sapModelProperty?.GetValue(etabsObject);
            return sapModel is not null;
        }
        catch (Exception ex)
        {
            AgentLog.Write($"Typed ETABS API connection failed: {ex}");
            Release(sapModel);
            Release(etabsObject);
            etabsObject = null;
            sapModel = null;
            sapModelInterface = null;
            return false;
        }
        finally
        {
            Release(helper);
        }
    }

    private static string? FindInstalledEtabsApi()
    {
        var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        var csiRoot = Path.Combine(programFiles, "Computers and Structures");
        if (!Directory.Exists(csiRoot)) return null;

        return Directory.EnumerateDirectories(csiRoot, "ETABS *", SearchOption.TopDirectoryOnly)
            .OrderByDescending(path => path, StringComparer.OrdinalIgnoreCase)
            .Select(path => Path.Combine(path, "ETABSv1.dll"))
            .FirstOrDefault(File.Exists);
    }

    private static object? TryGetFromRunningObjectTable()
    {
        IRunningObjectTable? runningObjectTable = null;
        IEnumMoniker? monikerEnumerator = null;
        IBindCtx? bindContext = null;

        try
        {
            if (NativeMethods.GetRunningObjectTable(0, out runningObjectTable) != 0)
                return null;

            runningObjectTable.EnumRunning(out monikerEnumerator);
            monikerEnumerator.Reset();

            if (NativeMethods.CreateBindCtx(0, out bindContext) != 0)
                return null;

            var monikers = new IMoniker[1];
            while (monikerEnumerator.Next(1, monikers, IntPtr.Zero) == 0)
            {
                try
                {
                    monikers[0].GetDisplayName(bindContext, null, out var displayName);
                    if (string.IsNullOrWhiteSpace(displayName) ||
                        !displayName.Contains("ETABS", StringComparison.OrdinalIgnoreCase))
                        continue;

                    runningObjectTable.GetObject(monikers[0], out var candidate);
                    if (candidate is null) continue;

                    try
                    {
                        var candidateType = candidate.GetType();
                        _ = candidateType.InvokeMember("SapModel",
                            System.Reflection.BindingFlags.GetProperty, null, candidate, null);
                        return candidate;
                    }
                    catch (Exception ex)
                    {
                        AgentLog.Write($"ROT ETABS candidate rejected: {ex}");
                        Release(candidate);
                    }
                }
                catch { }
                finally
                {
                    Release(monikers[0]);
                }
            }
        }
        catch (Exception ex) { AgentLog.Write($"ROT scan failed: {ex}"); }
        finally
        {
            Release(bindContext);
            Release(monikerEnumerator);
            Release(runningObjectTable);
        }

        return null;
    }

    private static object? TryGetFromEtabsHelper()
    {
        object? helper = null;
        try
        {
            var helperType = Type.GetTypeFromProgID("ETABSv1.Helper", throwOnError: false);
            if (helperType is null) return null;

            helper = Activator.CreateInstance(helperType);
            if (helper is null) return null;

            return helper.GetType().InvokeMember("GetObject",
                System.Reflection.BindingFlags.InvokeMethod, null, helper,
                new object[] { "CSI.ETABS.API.ETABSObject" });
        }
        catch (Exception ex)
        {
            AgentLog.Write($"ETABS Helper failed: {ex}");
            return null;
        }
        finally
        {
            Release(helper);
        }
    }

    private bool TryRead(out EtabsSnapshot snapshot)
    {
        snapshot = EtabsSnapshot.NotConnected("Not connected.");
        if (_sapModel is null)
            return false;

        try
        {
            if (_sapModelInterface is null) return false;

            var filename = (string?)_sapModelInterface.GetMethod("GetModelFilename")?.Invoke(_sapModel, null);
            var modelName = string.IsNullOrWhiteSpace(filename) ? "Untitled ETABS model" : Path.GetFileName(filename);
            var isLocked = (bool)(_sapModelInterface.GetMethod("GetModelIsLocked")?.Invoke(_sapModel, null) ?? false);
            // Keep the full local file path inside the agent; the web UI only needs the model name.
            snapshot = new EtabsSnapshot(true, true, modelName, null, isLocked, null, "0.3.2");
            return true;
        }
        catch
        {
            ReleaseComObjects();
            return false;
        }
    }

    private void ReleaseComObjects()
    {
        Release(_sapModel);
        Release(_etabsObject);
        _sapModel = null;
        _etabsObject = null;
        _sapModelInterface = null;
    }

    private static void Release(object? value)
    {
        if (value is null || !Marshal.IsComObject(value)) return;
        try { Marshal.FinalReleaseComObject(value); } catch { }
    }

    public void Dispose() => ReleaseComObjects();
}

internal sealed class LocalBridgeServer : IDisposable
{
    private const int Port = 5218;
    private static readonly HashSet<string> AllowedOrigins = new(StringComparer.OrdinalIgnoreCase)
    {
        "https://simsekahmet.github.io",
        "http://localhost:4173",
        "http://127.0.0.1:4173"
    };

    private readonly Func<EtabsSnapshot> _getSnapshot;
    private readonly CancellationTokenSource _cancellation = new();
    private TcpListener? _listener;

    public LocalBridgeServer(Func<EtabsSnapshot> getSnapshot) => _getSnapshot = getSnapshot;

    public void Start()
    {
        _listener = new TcpListener(IPAddress.Loopback, Port);
        _listener.Start();
        _ = AcceptLoopAsync(_cancellation.Token);
    }

    private async Task AcceptLoopAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested && _listener is not null)
        {
            try
            {
                var client = await _listener.AcceptTcpClientAsync(cancellationToken);
                _ = HandleClientAsync(client, cancellationToken);
            }
            catch (OperationCanceledException) { break; }
            catch (ObjectDisposedException) { break; }
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        using (client)
        using (var stream = client.GetStream())
        using (var reader = new StreamReader(stream, Encoding.ASCII, false, 4096, leaveOpen: true))
        {
            try
            {
                var requestLine = await reader.ReadLineAsync(cancellationToken);
                if (string.IsNullOrWhiteSpace(requestLine)) return;

                var parts = requestLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) return;

                var method = parts[0].ToUpperInvariant();
                var path = parts[1].Split('?', 2)[0];
                var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                string? line;
                while (!string.IsNullOrEmpty(line = await reader.ReadLineAsync(cancellationToken)))
                {
                    var separator = line.IndexOf(':');
                    if (separator > 0)
                        headers[line[..separator].Trim()] = line[(separator + 1)..].Trim();
                }

                headers.TryGetValue("Origin", out var origin);
                if (origin is not null && !AllowedOrigins.Contains(origin))
                {
                    await WriteResponseAsync(stream, 403, "Forbidden", new { error = "Origin is not allowed." }, null, cancellationToken);
                    return;
                }

                if (method == "OPTIONS")
                {
                    await WriteResponseAsync(stream, 204, "No Content", null, origin, cancellationToken);
                    return;
                }

                if (method != "GET")
                {
                    await WriteResponseAsync(stream, 405, "Method Not Allowed", new { error = "Only GET is allowed." }, origin, cancellationToken);
                    return;
                }

                if (path is "/api/health" or "/api/etabs/connect" or "/api/model")
                {
                    var snapshot = _getSnapshot();
                    await WriteResponseAsync(stream, 200, "OK", snapshot, origin, cancellationToken);
                    return;
                }

                await WriteResponseAsync(stream, 404, "Not Found", new { error = "Endpoint not found." }, origin, cancellationToken);
            }
            catch { }
        }
    }

    private static async Task WriteResponseAsync(
        NetworkStream stream,
        int statusCode,
        string reason,
        object? payload,
        string? origin,
        CancellationToken cancellationToken)
    {
        var body = payload is null
            ? Array.Empty<byte>()
            : JsonSerializer.SerializeToUtf8Bytes(payload, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        var header = new StringBuilder()
            .Append($"HTTP/1.1 {statusCode} {reason}\r\n")
            .Append("Content-Type: application/json; charset=utf-8\r\n")
            .Append($"Content-Length: {body.Length}\r\n")
            .Append("Cache-Control: no-store\r\n")
            .Append("Access-Control-Allow-Methods: GET, OPTIONS\r\n")
            .Append("Access-Control-Allow-Headers: Accept, Content-Type\r\n")
            .Append("Access-Control-Allow-Private-Network: true\r\n")
            .Append("Connection: close\r\n");

        if (!string.IsNullOrWhiteSpace(origin))
            header.Append($"Access-Control-Allow-Origin: {origin}\r\nVary: Origin\r\n");

        header.Append("\r\n");
        var headerBytes = Encoding.ASCII.GetBytes(header.ToString());
        await stream.WriteAsync(headerBytes, cancellationToken);
        if (body.Length > 0)
            await stream.WriteAsync(body, cancellationToken);
    }

    public void Dispose()
    {
        _cancellation.Cancel();
        _listener?.Stop();
        _listener = null;
        _cancellation.Dispose();
    }
}

internal sealed record EtabsSnapshot(
    bool AgentOnline,
    bool EtabsConnected,
    string? ModelName,
    string? ModelPath,
    bool? ModelLocked,
    string? Error,
    string AgentVersion)
{
    public static EtabsSnapshot NotConnected(string error) =>
        new(true, false, null, null, null, error, "0.3.2");
}

internal static class NativeMethods
{
    [DllImport("oleaut32.dll", PreserveSig = true)]
    internal static extern int GetActiveObject(ref Guid classId, IntPtr reserved, [MarshalAs(UnmanagedType.IUnknown)] out object? value);

    [DllImport("ole32.dll", PreserveSig = true)]
    internal static extern int GetRunningObjectTable(uint reserved, out IRunningObjectTable runningObjectTable);

    [DllImport("ole32.dll", PreserveSig = true)]
    internal static extern int CreateBindCtx(uint reserved, out IBindCtx bindContext);
}

internal static class AgentLog
{
    private static readonly string LogPath = Path.Combine(Path.GetTempPath(), "StructuralEngineeringAssistant.Agent.log");

    public static void Write(string message)
    {
        try
        {
            File.AppendAllText(LogPath, $"{DateTimeOffset.Now:O} {message}{Environment.NewLine}");
        }
        catch { }
    }
}
