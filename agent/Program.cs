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

        _server = new LocalBridgeServer(
            GetSnapshotOnUiThread,
            GetCombinationsOnUiThread,
            GetStoriesOnUiThread,
            GetStoryDriftsOnUiThread);
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

    private NameListResult GetCombinationsOnUiThread()
    {
        if (_dispatcher.InvokeRequired)
            return (NameListResult)_dispatcher.Invoke(new Func<NameListResult>(_etabs.GetCombinationsAndCases));

        return _etabs.GetCombinationsAndCases();
    }

    private StoriesResult GetStoriesOnUiThread()
    {
        if (_dispatcher.InvokeRequired)
            return (StoriesResult)_dispatcher.Invoke(new Func<StoriesResult>(_etabs.GetStories));

        return _etabs.GetStories();
    }

    private StoryDriftsResult GetStoryDriftsOnUiThread(string[] names)
    {
        if (_dispatcher.InvokeRequired)
            return (StoryDriftsResult)_dispatcher.Invoke(new Func<string[], StoryDriftsResult>(_etabs.GetStoryDrifts), names);

        return _etabs.GetStoryDrifts(names);
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
    private const string EtabsProgId = "CSI.ETABS.API.ETABSObject";

    private object? _etabsObject;
    private object? _sapModel;

    // Typed interfaces loaded from the installed ETABSv1.dll. The CSI OAPI objects are
    // custom IUnknown COM interfaces (not IDispatch), so every call must go through these
    // typed interfaces — late binding via InvokeMember throws InvalidCastException.
    private System.Reflection.Assembly? _apiAssembly;
    private Type? _helperClass;
    private Type? _helperInterface;
    private Type? _oapiInterface;
    private Type? _sapModelInterface;

    public EtabsSnapshot ConnectAndRead()
    {
        if (TryRead(out var current))
            return current;

        ReleaseComObjects();

        try
        {
            if (!EnsureApiLoaded())
                return EtabsSnapshot.NotConnected(
                    "ETABS API (ETABSv1.dll) not found. Install ETABS 22 or later.");

            // 1) Preferred: the CSI helper attaches to the running instance.
            // 2) Fallback: native GetActiveObject / running object table.
            var etabs = TryHelperGetObject()
                        ?? TryGetActiveObject()
                        ?? TryGetFromRunningObjectTable();

            if (etabs is null)
                return EtabsSnapshot.NotConnected(
                    "No running ETABS instance was found. Open your model in ETABS, then connect. " +
                    "If ETABS runs as administrator, run this agent as administrator too.");

            _etabsObject = etabs;
            _sapModel = _oapiInterface!.GetProperty("SapModel")?.GetValue(etabs);
            if (_sapModel is null)
                return EtabsSnapshot.NotConnected("Connected to ETABS but no model is open.");

            return TryRead(out var snapshot)
                ? snapshot
                : EtabsSnapshot.NotConnected("Connected to ETABS but the model could not be read.");
        }
        catch (Exception ex)
        {
            AgentLog.Write($"ConnectAndRead failed: {ex}");
            ReleaseComObjects();
            return EtabsSnapshot.NotConnected(ex.Message);
        }
    }

    public EtabsSnapshot ReadCurrent() =>
        TryRead(out var snapshot) ? snapshot : EtabsSnapshot.NotConnected("Not connected.");

    // Response combination names and load case names, merged (mirrors the desktop app's
    // "Getir" button, which lists both in a single picker).
    public NameListResult GetCombinationsAndCases()
    {
        if (!EnsureModelReady(out var error))
            return new NameListResult(true, false, error, Array.Empty<string>());

        try
        {
            var sap = _sapModel!;

            var respComboProp = _sapModelInterface!.GetProperty("RespCombo")!;
            var respCombo = respComboProp.GetValue(sap);
            var comboArgs = new object?[] { 0, null };
            respComboProp.PropertyType.GetMethod("GetNameList")!.Invoke(respCombo, comboArgs);
            var combos = (string[]?)comboArgs[1] ?? Array.Empty<string>();

            var loadCasesProp = _sapModelInterface.GetProperty("LoadCases")!;
            var loadCases = loadCasesProp.GetValue(sap);
            // GetNameList(ref int, ref string[], eLoadCaseType) — the type filter has a default
            // value in the API, but reflection Invoke does not apply C# default parameters, so
            // it must be supplied explicitly.
            var getNameList = loadCasesProp.PropertyType.GetMethods().First(m => m.Name == "GetNameList");
            var caseTypeDefault = getNameList.GetParameters()[2].DefaultValue;
            var caseArgs = new object?[] { 0, null, caseTypeDefault };
            getNameList.Invoke(loadCases, caseArgs);
            var cases = (string[]?)caseArgs[1] ?? Array.Empty<string>();

            return new NameListResult(true, true, null, combos.Concat(cases).ToArray());
        }
        catch (Exception ex)
        {
            AgentLog.Write($"GetCombinationsAndCases failed: {ex}");
            return new NameListResult(true, false, ex.Message, Array.Empty<string>());
        }
    }

    // Story names and elevations, used client-side to determine which stories are basements.
    public StoriesResult GetStories()
    {
        if (!EnsureModelReady(out var error))
            return new StoriesResult(true, false, error, Array.Empty<StoryInfo>());

        try
        {
            var sap = _sapModel!;
            var storyProp = _sapModelInterface!.GetProperty("Story")!;
            var story = storyProp.GetValue(sap);
            var args = new object?[] { 0, null, null, null, null, null, null, null };
            storyProp.PropertyType.GetMethod("GetStories")!.Invoke(story, args);

            var names = (string[]?)args[1] ?? Array.Empty<string>();
            var elevations = (double[]?)args[2] ?? Array.Empty<double>();
            var stories = names
                .Select((name, i) => new StoryInfo(name, i < elevations.Length ? elevations[i] : 0))
                .ToArray();

            return new StoriesResult(true, true, null, stories);
        }
        catch (Exception ex)
        {
            AgentLog.Write($"GetStories failed: {ex}");
            return new StoriesResult(true, false, ex.Message, Array.Empty<StoryInfo>());
        }
    }

    // Raw story drift rows for the given response combinations / load cases. Each selected
    // name is tried as both a combination and a case, since the caller does not distinguish;
    // ETABS ignores the one that does not apply.
    public StoryDriftsResult GetStoryDrifts(string[] selectedNames)
    {
        if (selectedNames.Length == 0)
            return new StoryDriftsResult(true, true, "No combinations selected.", Array.Empty<StoryDriftRow>());
        if (!EnsureModelReady(out var error))
            return new StoryDriftsResult(true, false, error, Array.Empty<StoryDriftRow>());

        object? setup = null;
        Type? setupType = null;
        try
        {
            var sap = _sapModel!;
            var resultsProp = _sapModelInterface!.GetProperty("Results")!;
            var results = resultsProp.GetValue(sap)!;
            var resultsType = resultsProp.PropertyType;

            var setupProp = resultsType.GetProperty("Setup")!;
            setup = setupProp.GetValue(results)!;
            setupType = setupProp.PropertyType;

            setupType.GetMethod("DeselectAllCasesAndCombosForOutput")!.Invoke(setup, null);

            var setCombo = setupType.GetMethods().First(m => m.Name == "SetComboSelectedForOutput");
            var setCase = setupType.GetMethods().First(m => m.Name == "SetCaseSelectedForOutput");
            foreach (var name in selectedNames)
            {
                setCombo.Invoke(setup, new object?[] { name, true });
                setCase.Invoke(setup, new object?[] { name, true });
            }

            var storyDrifts = resultsType.GetMethod("StoryDrifts")!;
            var args = new object?[11];
            args[0] = 0;
            storyDrifts.Invoke(results, args);

            var numResults = (int)(args[0] ?? 0);
            var story = (string[]?)args[1] ?? Array.Empty<string>();
            var loadCase = (string[]?)args[2] ?? Array.Empty<string>();
            var direction = (string[]?)args[5] ?? Array.Empty<string>();
            var drift = (double[]?)args[6] ?? Array.Empty<double>();

            var rows = new List<StoryDriftRow>(numResults);
            for (int i = 0; i < numResults; i++)
                rows.Add(new StoryDriftRow(story[i], loadCase[i], direction[i], drift[i]));

            return new StoryDriftsResult(true, true, null, rows.ToArray());
        }
        catch (Exception ex)
        {
            AgentLog.Write($"GetStoryDrifts failed: {ex}");
            return new StoryDriftsResult(true, false, ex.Message, Array.Empty<StoryDriftRow>());
        }
        finally
        {
            try { setupType?.GetMethod("DeselectAllCasesAndCombosForOutput")?.Invoke(setup, null); } catch { }
        }
    }

    // Reuses ConnectAndRead so every data endpoint shares the same connect/reconnect logic
    // and always operates against a live, readable model.
    private bool EnsureModelReady(out string? error)
    {
        var snapshot = ConnectAndRead();
        if (!snapshot.EtabsConnected)
        {
            error = snapshot.Error ?? "ETABS is not connected.";
            return false;
        }

        error = null;
        return true;
    }

    private bool EnsureApiLoaded()
    {
        if (_apiAssembly is not null) return true;

        var apiPath = FindInstalledEtabsApi();
        if (apiPath is null)
        {
            AgentLog.Write("ETABSv1.dll was not found under the ETABS installation folder.");
            return false;
        }

        AgentLog.Write($"Loading installed ETABS API: {apiPath}");
        _apiAssembly = System.Reflection.Assembly.LoadFrom(apiPath);
        _helperClass = _apiAssembly.GetType("ETABSv1.Helper", throwOnError: true)!;
        _helperInterface = _apiAssembly.GetType("ETABSv1.cHelper", throwOnError: true)!;
        _oapiInterface = _apiAssembly.GetType("ETABSv1.cOAPI", throwOnError: true)!;
        _sapModelInterface = _apiAssembly.GetType("ETABSv1.cSapModel", throwOnError: true)!;
        return true;
    }

    private object? TryHelperGetObject()
    {
        try
        {
            var helper = Activator.CreateInstance(_helperClass!);
            if (helper is null) return null;

            var getObject = _helperInterface!.GetMethod("GetObject", new[] { typeof(string) });
            return getObject?.Invoke(helper, new object[] { EtabsProgId });
        }
        catch (Exception ex)
        {
            // Thrown when no instance is running, or on .NET where the helper relies on the
            // removed Marshal.GetActiveObject. The native fallbacks below cover both cases.
            AgentLog.Write($"Helper.GetObject failed: {ex.Message}");
            return null;
        }
    }

    private object? TryGetActiveObject()
    {
        try
        {
            var etabsType = Type.GetTypeFromProgID(EtabsProgId, throwOnError: false);
            if (etabsType is null) return null;

            var classId = etabsType.GUID;
            return NativeMethods.GetActiveObject(ref classId, IntPtr.Zero, out var active) == 0 ? active : null;
        }
        catch (Exception ex)
        {
            AgentLog.Write($"GetActiveObject failed: {ex.Message}");
            return null;
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

    private object? TryGetFromRunningObjectTable()
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

                    // Validate through the typed interface (not late binding).
                    if (_oapiInterface!.GetProperty("SapModel")?.GetValue(candidate) is not null)
                        return candidate;

                    Release(candidate);
                }
                catch (Exception ex)
                {
                    AgentLog.Write($"ROT ETABS candidate rejected: {ex.Message}");
                }
                finally
                {
                    Release(monikers[0]);
                }
            }
        }
        catch (Exception ex) { AgentLog.Write($"ROT scan failed: {ex.Message}"); }
        finally
        {
            Release(bindContext);
            Release(monikerEnumerator);
            Release(runningObjectTable);
        }

        return null;
    }

    private bool TryRead(out EtabsSnapshot snapshot)
    {
        snapshot = EtabsSnapshot.NotConnected("Not connected.");
        if (_sapModel is null || _sapModelInterface is null)
            return false;

        try
        {
            // GetModelFilename requires a bool (IncludePath) argument; invoking it without one
            // throws TargetParameterCountException and silently drops a valid connection.
            var getFilename = _sapModelInterface.GetMethod("GetModelFilename", new[] { typeof(bool) });
            var filename = (string?)getFilename?.Invoke(_sapModel, new object[] { true });
            var modelName = string.IsNullOrWhiteSpace(filename) ? "Untitled ETABS model" : Path.GetFileName(filename);
            var isLocked = (bool)(_sapModelInterface.GetMethod("GetModelIsLocked")?.Invoke(_sapModel, null) ?? false);
            // Keep the full local file path inside the agent; the web UI only needs the model name.
            snapshot = new EtabsSnapshot(true, true, modelName, null, isLocked, null, AgentInfo.Version);
            return true;
        }
        catch (Exception ex)
        {
            AgentLog.Write($"TryRead failed: {ex.Message}");
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
    private readonly Func<NameListResult> _getCombinations;
    private readonly Func<StoriesResult> _getStories;
    private readonly Func<string[], StoryDriftsResult> _getStoryDrifts;
    private readonly CancellationTokenSource _cancellation = new();
    private TcpListener? _listener;

    public LocalBridgeServer(
        Func<EtabsSnapshot> getSnapshot,
        Func<NameListResult> getCombinations,
        Func<StoriesResult> getStories,
        Func<string[], StoryDriftsResult> getStoryDrifts)
    {
        _getSnapshot = getSnapshot;
        _getCombinations = getCombinations;
        _getStories = getStories;
        _getStoryDrifts = getStoryDrifts;
    }

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
                var pathAndQuery = parts[1].Split('?', 2);
                var path = pathAndQuery[0];
                var query = pathAndQuery.Length > 1 ? ParseQuery(pathAndQuery[1]) : new Dictionary<string, string>();
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

                if (path == "/api/etabs/combinations")
                {
                    await WriteResponseAsync(stream, 200, "OK", _getCombinations(), origin, cancellationToken);
                    return;
                }

                if (path == "/api/etabs/stories")
                {
                    await WriteResponseAsync(stream, 200, "OK", _getStories(), origin, cancellationToken);
                    return;
                }

                if (path == "/api/etabs/story-drifts")
                {
                    query.TryGetValue("combos", out var combosParam);
                    var names = string.IsNullOrWhiteSpace(combosParam)
                        ? Array.Empty<string>()
                        : combosParam.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    await WriteResponseAsync(stream, 200, "OK", _getStoryDrifts(names), origin, cancellationToken);
                    return;
                }

                await WriteResponseAsync(stream, 404, "Not Found", new { error = "Endpoint not found." }, origin, cancellationToken);
            }
            catch { }
        }
    }

    private static Dictionary<string, string> ParseQuery(string query)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var pair in query.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var kv = pair.Split('=', 2);
            var key = Uri.UnescapeDataString(kv[0]);
            var value = kv.Length > 1 ? Uri.UnescapeDataString(kv[1]) : string.Empty;
            result[key] = value;
        }
        return result;
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
        new(true, false, null, null, null, error, AgentInfo.Version);
}

internal static class AgentInfo
{
    public const string Version = "0.4.0";
}

internal sealed record NameListResult(bool AgentOnline, bool EtabsConnected, string? Error, string[] Names);

internal sealed record StoryInfo(string Name, double Elevation);

internal sealed record StoriesResult(bool AgentOnline, bool EtabsConnected, string? Error, StoryInfo[] Stories);

internal sealed record StoryDriftRow(string Story, string OutputCase, string Direction, double Drift);

internal sealed record StoryDriftsResult(bool AgentOnline, bool EtabsConnected, string? Error, StoryDriftRow[] Rows);

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
