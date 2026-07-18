using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
    private dynamic? _etabsObject;
    private dynamic? _sapModel;

    public EtabsSnapshot ConnectAndRead()
    {
        if (TryRead(out var current))
            return current;

        ReleaseComObjects();

        try
        {
            var etabsType = Type.GetTypeFromProgID("CSI.ETABS.API.ETABSObject", throwOnError: false);
            if (etabsType is null)
                return EtabsSnapshot.NotConnected("ETABS COM API is not registered. Install ETABS 22 or later.");

            var classId = etabsType.GUID;
            var result = NativeMethods.GetActiveObject(ref classId, IntPtr.Zero, out var activeObject);
            if (result != 0 || activeObject is null)
                return EtabsSnapshot.NotConnected("No running ETABS instance was found.");

            _etabsObject = activeObject;
            _sapModel = _etabsObject.SapModel;
            return TryRead(out var connected)
                ? connected
                : EtabsSnapshot.NotConnected("ETABS is running but no model is open.");
        }
        catch (Exception ex)
        {
            ReleaseComObjects();
            return EtabsSnapshot.NotConnected(ex.Message);
        }
    }

    public EtabsSnapshot ReadCurrent() =>
        TryRead(out var snapshot) ? snapshot : EtabsSnapshot.NotConnected("Not connected.");

    private bool TryRead(out EtabsSnapshot snapshot)
    {
        snapshot = EtabsSnapshot.NotConnected("Not connected.");
        if (_sapModel is null)
            return false;

        try
        {
            var filename = (string?)_sapModel.GetModelFilename();
            var modelName = string.IsNullOrWhiteSpace(filename) ? "Untitled ETABS model" : Path.GetFileName(filename);
            var isLocked = (bool)_sapModel.GetModelIsLocked();
            snapshot = new EtabsSnapshot(true, true, modelName, filename, isLocked, null, "0.3.0");
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
        new(true, false, null, null, null, error, "0.3.0");
}

internal static class NativeMethods
{
    [DllImport("oleaut32.dll", PreserveSig = true)]
    internal static extern int GetActiveObject(ref Guid classId, IntPtr reserved, [MarshalAs(UnmanagedType.IUnknown)] out object? value);
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
