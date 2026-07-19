using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DrawingColor = System.Drawing.Color;

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
            GetStoryDriftsOnUiThread,
            GetTableOnUiThread,
            SelectFramesOnUiThread,
            GetFrameSectionsOnUiThread);
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

    private TableResult GetTableOnUiThread(string tableName, string[] combos)
    {
        if (_dispatcher.InvokeRequired)
            return (TableResult)_dispatcher.Invoke(new Func<string, string[], TableResult>(_etabs.GetTable), tableName, combos);

        return _etabs.GetTable(tableName, combos);
    }

    private SelectResult SelectFramesOnUiThread(IReadOnlyList<FrameKey> items)
    {
        if (_dispatcher.InvokeRequired)
            return (SelectResult)_dispatcher.Invoke(new Func<IReadOnlyList<FrameKey>, SelectResult>(_etabs.SelectFrames), items);

        return _etabs.SelectFrames(items);
    }

    private FrameSectionsResult GetFrameSectionsOnUiThread()
    {
        if (_dispatcher.InvokeRequired)
            return (FrameSectionsResult)_dispatcher.Invoke(new Func<FrameSectionsResult>(_etabs.GetFrameSections));

        return _etabs.GetFrameSections();
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

    // Generic ETABS database-table reader used by most calculation modules. Returns the raw
    // display table (fields + string rows). When combos are supplied, they are selected for
    // output first (needed for result tables such as Story Forces / Story Drifts).
    public TableResult GetTable(string tableName, string[] combos)
    {
        if (string.IsNullOrWhiteSpace(tableName))
            return new TableResult(true, true, "No table name supplied.", Array.Empty<string>(), Array.Empty<string[]>());
        if (!EnsureModelReady(out var error))
            return new TableResult(true, false, error, Array.Empty<string>(), Array.Empty<string[]>());

        object? setup = null;
        Type? setupType = null;
        try
        {
            var sap = _sapModel!;
            var dbProp = _sapModelInterface!.GetProperty("DatabaseTables")!;
            var db = dbProp.GetValue(sap)!;
            var dbType = dbProp.PropertyType;

            if (combos.Length > 0)
            {
                var resultsProp = _sapModelInterface.GetProperty("Results")!;
                var results = resultsProp.GetValue(sap)!;
                var setupProp = resultsProp.PropertyType.GetProperty("Setup")!;
                setup = setupProp.GetValue(results)!;
                setupType = setupProp.PropertyType;

                setupType.GetMethod("DeselectAllCasesAndCombosForOutput")!.Invoke(setup, null);
                var setCombo = setupType.GetMethods().First(m => m.Name == "SetComboSelectedForOutput");
                var setCase = setupType.GetMethods().First(m => m.Name == "SetCaseSelectedForOutput");
                foreach (var c in combos)
                {
                    setCombo.Invoke(setup, new object?[] { c, true });
                    setCase.Invoke(setup, new object?[] { c, true });
                }

                var setDisplay = dbType.GetMethods().FirstOrDefault(m => m.Name == "SetLoadCombinationsSelectedForDisplay");
                if (setDisplay is not null)
                {
                    try { setDisplay.Invoke(db, new object?[] { combos }); }
                    catch (Exception ex) { AgentLog.Write($"SetLoadCombinationsSelectedForDisplay failed: {ex.Message}"); }
                }
            }

            var getTable = dbType.GetMethods().First(m => m.Name == "GetTableForDisplayArray");
            // (TableKey, ref FieldKeyList, GroupName, ref TableVersion, ref FieldsKeysIncluded, ref NumberRecords, ref TableData)
            var args = new object?[] { tableName, Array.Empty<string>(), "", 0, null, 0, null };
            getTable.Invoke(db, args);

            var fields = (string[]?)args[4] ?? Array.Empty<string>();
            var numRecords = (int)(args[5] ?? 0);
            var data = (string[]?)args[6] ?? Array.Empty<string>();
            var fieldCount = fields.Length;

            var rows = new List<string[]>(numRecords);
            for (int r = 0; r < numRecords && fieldCount > 0; r++)
            {
                var row = new string[fieldCount];
                Array.Copy(data, r * fieldCount, row, 0, fieldCount);
                rows.Add(row);
            }

            return new TableResult(true, true, null, fields, rows.ToArray());
        }
        catch (Exception ex)
        {
            AgentLog.Write($"GetTable('{tableName}') failed: {ex}");
            return new TableResult(true, false, ex.Message, Array.Empty<string>(), Array.Empty<string[]>());
        }
        finally
        {
            try { setupType?.GetMethod("DeselectAllCasesAndCombosForOutput")?.Invoke(setup, null); } catch { }
        }
    }

    // Selects the given frame objects (matched by Story + Label) in the ETABS model and
    // refreshes the active view, so the user can see failing members highlighted. This is the
    // only write operation the agent performs; everything else is read-only.
    public SelectResult SelectFrames(IReadOnlyList<FrameKey> items)
    {
        if (items.Count == 0)
            return new SelectResult(true, true, "No items to select.", 0);
        if (!EnsureModelReady(out var error))
            return new SelectResult(true, false, error, 0);

        try
        {
            var sap = _sapModel!;
            var frameProp = _sapModelInterface!.GetProperty("FrameObj")!;
            var frame = frameProp.GetValue(sap)!;
            var frameType = frameProp.PropertyType;

            var selectObjProp = _sapModelInterface.GetProperty("SelectObj")!;
            var selectObj = selectObjProp.GetValue(sap)!;
            selectObjProp.PropertyType.GetMethod("ClearSelection")!.Invoke(selectObj, null);

            var getNameList = frameType.GetMethods().First(m => m.Name == "GetNameList");
            var nameArgs = new object?[] { 0, null };
            getNameList.Invoke(frame, nameArgs);
            var names = (string[]?)nameArgs[1] ?? Array.Empty<string>();

            var getLabel = frameType.GetMethods().First(m => m.Name == "GetLabelFromName");
            var setSelected = frameType.GetMethods().First(m => m.Name == "SetSelected");
            var itemTypeDefault = setSelected.GetParameters()[2].DefaultValue;

            int count = 0;
            foreach (var name in names)
            {
                var labelArgs = new object?[] { name, "", "" };
                getLabel.Invoke(frame, labelArgs);
                var label = ((string?)labelArgs[1] ?? "").Trim();
                var story = ((string?)labelArgs[2] ?? "").Trim();

                var match = items.Any(it =>
                    string.Equals(it.Label.Trim(), label, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(it.Story.Trim(), story, StringComparison.OrdinalIgnoreCase));

                if (match)
                {
                    setSelected.Invoke(frame, new object?[] { name, true, itemTypeDefault });
                    count++;
                }
            }

            var viewProp = _sapModelInterface.GetProperty("View")!;
            var view = viewProp.GetValue(sap)!;
            var refreshView = viewProp.PropertyType.GetMethods().First(m => m.Name == "RefreshView");
            var rvArgs = refreshView.GetParameters().Select(p => p.HasDefaultValue ? p.DefaultValue : (object)0).ToArray();
            refreshView.Invoke(view, rvArgs);

            return new SelectResult(true, true, null, count);
        }
        catch (Exception ex)
        {
            AgentLog.Write($"SelectFrames failed: {ex}");
            return new SelectResult(true, false, ex.Message, 0);
        }
    }

    // For every frame object, returns its section property name plus the section's depth (h/T3)
    // and width (b/T2) via FrameObj.GetSection + PropFrame.GetRectangle — exactly how the desktop
    // beam checks read section geometry (works for any rectangular concrete section, not just ones
    // whose name encodes dimensions). Dims are in the model's length unit; the web side scales.
    public FrameSectionsResult GetFrameSections()
    {
        if (!EnsureModelReady(out var error))
            return new FrameSectionsResult(true, false, error, Array.Empty<FrameSectionInfo>());

        try
        {
            var sap = _sapModel!;
            var frameProp = _sapModelInterface!.GetProperty("FrameObj")!;
            var frame = frameProp.GetValue(sap)!;
            var frameType = frameProp.PropertyType;

            var propFrameProp = _sapModelInterface.GetProperty("PropFrame")!;
            var propFrame = propFrameProp.GetValue(sap)!;
            var propFrameType = propFrameProp.PropertyType;

            var getNameList = frameType.GetMethods().First(m => m.Name == "GetNameList");
            var nameArgs = new object?[] { 0, null };
            getNameList.Invoke(frame, nameArgs);
            var names = (string[]?)nameArgs[1] ?? Array.Empty<string>();

            var getSection = frameType.GetMethods().First(m => m.Name == "GetSection" && m.GetParameters().Length == 3);
            var getLabel = frameType.GetMethods().First(m => m.Name == "GetLabelFromName");
            var getRectangle = propFrameType.GetMethods().First(m => m.Name == "GetRectangle" && m.GetParameters().Length == 8);

            var sectionDims = new Dictionary<string, (double H, double B)>(StringComparer.OrdinalIgnoreCase);
            var list = new List<FrameSectionInfo>(names.Length);

            foreach (var name in names)
            {
                // GetSection(Name, ref PropName, ref SAuto)
                var secArgs = new object?[] { name, "", "" };
                getSection.Invoke(frame, secArgs);
                var prop = (string?)secArgs[1] ?? "";
                if (string.IsNullOrEmpty(prop)) continue;

                if (!sectionDims.TryGetValue(prop, out var dims))
                {
                    // GetRectangle(Name, ref FileName, ref MatProp, ref T3, ref T2, ref Color, ref Notes, ref GUID)
                    var rectArgs = new object?[] { prop, "", "", 0.0, 0.0, 0, "", "" };
                    var ret = getRectangle.Invoke(propFrame, rectArgs);
                    double h = 0, b = 0;
                    if (ret is 0) { h = (double)(rectArgs[3] ?? 0.0); b = (double)(rectArgs[4] ?? 0.0); }
                    dims = (h, b);
                    sectionDims[prop] = dims;
                }

                var labelArgs = new object?[] { name, "", "" };
                getLabel.Invoke(frame, labelArgs);

                list.Add(new FrameSectionInfo(
                    name,
                    ((string?)labelArgs[1] ?? "").Trim(),
                    ((string?)labelArgs[2] ?? "").Trim(),
                    prop, dims.H, dims.B));
            }

            return new FrameSectionsResult(true, true, null, list.ToArray());
        }
        catch (Exception ex)
        {
            AgentLog.Write($"GetFrameSections failed: {ex}");
            return new FrameSectionsResult(true, false, ex.Message, Array.Empty<FrameSectionInfo>());
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
    private readonly Func<string, string[], TableResult> _getTable;
    private readonly Func<IReadOnlyList<FrameKey>, SelectResult> _selectFrames;
    private readonly Func<FrameSectionsResult> _getFrameSections;
    private readonly CancellationTokenSource _cancellation = new();
    private TcpListener? _listener;

    public LocalBridgeServer(
        Func<EtabsSnapshot> getSnapshot,
        Func<NameListResult> getCombinations,
        Func<StoriesResult> getStories,
        Func<string[], StoryDriftsResult> getStoryDrifts,
        Func<string, string[], TableResult> getTable,
        Func<IReadOnlyList<FrameKey>, SelectResult> selectFrames,
        Func<FrameSectionsResult> getFrameSections)
    {
        _getSnapshot = getSnapshot;
        _getCombinations = getCombinations;
        _getStories = getStories;
        _getStoryDrifts = getStoryDrifts;
        _getTable = getTable;
        _selectFrames = selectFrames;
        _getFrameSections = getFrameSections;
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

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        using (client)
        using (var stream = client.GetStream())
        {
            try
            {
                var request = await ReadHttpRequestAsync(stream, cancellationToken);
                if (request is null) return;
                var (method, path, query, headers, body) = request.Value;

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

                if (method == "GET")
                {
                    if (path is "/api/health" or "/api/etabs/connect" or "/api/model")
                    {
                        await WriteResponseAsync(stream, 200, "OK", _getSnapshot(), origin, cancellationToken);
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

                    if (path == "/api/etabs/table")
                    {
                        query.TryGetValue("name", out var tableName);
                        query.TryGetValue("combos", out var tableCombos);
                        var combos = string.IsNullOrWhiteSpace(tableCombos)
                            ? Array.Empty<string>()
                            : tableCombos.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        await WriteResponseAsync(stream, 200, "OK", _getTable(tableName ?? "", combos), origin, cancellationToken);
                        return;
                    }

                    if (path == "/api/etabs/frame-sections")
                    {
                        await WriteResponseAsync(stream, 200, "OK", _getFrameSections(), origin, cancellationToken);
                        return;
                    }

                    await WriteResponseAsync(stream, 404, "Not Found", new { error = "Endpoint not found." }, origin, cancellationToken);
                    return;
                }

                if (method == "POST")
                {
                    var json = Encoding.UTF8.GetString(body);

                    if (path == "/api/etabs/select-frames")
                    {
                        var req = JsonSerializer.Deserialize<SelectFramesRequest>(json, JsonOptions);
                        var items = req?.Items ?? Array.Empty<FrameKey>();
                        await WriteResponseAsync(stream, 200, "OK", _selectFrames(items), origin, cancellationToken);
                        return;
                    }

                    if (path == "/api/etabs/export/column-axial")
                    {
                        var req = JsonSerializer.Deserialize<ColumnAxialExportRequest>(json, JsonOptions);
                        if (req is null) { await WriteResponseAsync(stream, 400, "Bad Request", new { error = "Invalid request body." }, origin, cancellationToken); return; }
                        var bytes = ColumnAxialExcelReport.Build(req.Fck, req.Limit, req.Rows);
                        await WriteBinaryResponseAsync(stream, 200, "OK", bytes, "Kolon_Eksenel_Raporu.xlsx", origin, cancellationToken);
                        return;
                    }

                    if (path == "/api/etabs/export/drift")
                    {
                        var req = JsonSerializer.Deserialize<GoreliKatExportRequest>(json, JsonOptions);
                        if (req is null) { await WriteResponseAsync(stream, 400, "Bad Request", new { error = "Invalid request body." }, origin, cancellationToken); return; }
                        var bytes = GoreliKatExcelReport.Build(req.SdsDD2, req.SdsDD3, req.Sd1DD2, req.Sd1DD3, req.Tp, req.K, req.EsnekDerz, req.Rows);
                        await WriteBinaryResponseAsync(stream, 200, "OK", bytes, "GoreliKat_Sonuc.xlsx", origin, cancellationToken);
                        return;
                    }

                    if (path == "/api/etabs/export/pdelta")
                    {
                        var req = JsonSerializer.Deserialize<IkinciMertebeExportRequest>(json, JsonOptions);
                        if (req is null) { await WriteResponseAsync(stream, 400, "Bad Request", new { error = "Invalid request body." }, origin, cancellationToken); return; }
                        var bytes = IkinciMertebeExcelReport.Build(req.Ch, req.R, req.D, req.Rows);
                        await WriteBinaryResponseAsync(stream, 200, "OK", bytes, "IkinciMertebe_Sonuc.xlsx", origin, cancellationToken);
                        return;
                    }

                    if (path == "/api/etabs/export/beam-shear")
                    {
                        var req = JsonSerializer.Deserialize<BeamShearExportRequest>(json, JsonOptions);
                        if (req is null) { await WriteResponseAsync(stream, 400, "Bad Request", new { error = "Invalid request body." }, origin, cancellationToken); return; }
                        var bytes = BeamShearExcelReport.Build(req.Fck, req.Fyk, req.UseVc, req.Rows);
                        await WriteBinaryResponseAsync(stream, 200, "OK", bytes, "Kiris_Kesme_Raporu.xlsx", origin, cancellationToken);
                        return;
                    }

                    if (path == "/api/etabs/export/beam-axial")
                    {
                        var req = JsonSerializer.Deserialize<BeamAxialExportRequest>(json, JsonOptions);
                        if (req is null) { await WriteResponseAsync(stream, 400, "Bad Request", new { error = "Invalid request body." }, origin, cancellationToken); return; }
                        var bytes = BeamAxialExcelReport.Build(req.Fck, req.Limit, req.Rows);
                        await WriteBinaryResponseAsync(stream, 200, "OK", bytes, "Kiris_Eksenel_Raporu.xlsx", origin, cancellationToken);
                        return;
                    }

                    await WriteResponseAsync(stream, 404, "Not Found", new { error = "Endpoint not found." }, origin, cancellationToken);
                    return;
                }

                await WriteResponseAsync(stream, 405, "Method Not Allowed", new { error = "Only GET/POST are allowed." }, origin, cancellationToken);
            }
            catch (Exception ex) { AgentLog.Write($"Request handling failed: {ex}"); }
        }
    }

    // Reads a full HTTP request (request line, headers, and exactly Content-Length body bytes)
    // directly off the socket as bytes. A StreamReader-based line reader was used previously, but
    // mixing buffered text reads with a raw byte-count body read risks losing or corrupting bytes
    // already pulled into the reader's internal buffer — especially for any non-ASCII body content.
    private static async Task<(string Method, string Path, Dictionary<string, string> Query, Dictionary<string, string> Headers, byte[] Body)?> ReadHttpRequestAsync(
        NetworkStream stream, CancellationToken cancellationToken)
    {
        var buffer = new List<byte>(4096);
        var chunk = new byte[4096];
        int headerEnd;

        while ((headerEnd = IndexOfHeaderTerminator(buffer)) < 0)
        {
            int read = await stream.ReadAsync(chunk, cancellationToken);
            if (read == 0) return null;
            for (int i = 0; i < read; i++) buffer.Add(chunk[i]);
            if (buffer.Count > 1_000_000) return null; // guard against runaway/garbage input
        }

        var headerText = Encoding.ASCII.GetString(buffer.GetRange(0, headerEnd).ToArray());
        var lines = headerText.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length == 0) return null;

        var requestParts = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (requestParts.Length < 2) return null;

        var method = requestParts[0].ToUpperInvariant();
        var pathAndQuery = requestParts[1].Split('?', 2);
        var path = pathAndQuery[0];
        var query = pathAndQuery.Length > 1 ? ParseQuery(pathAndQuery[1]) : new Dictionary<string, string>();

        var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        for (int i = 1; i < lines.Length; i++)
        {
            var separator = lines[i].IndexOf(':');
            if (separator > 0) headers[lines[i][..separator].Trim()] = lines[i][(separator + 1)..].Trim();
        }

        int contentLength = headers.TryGetValue("Content-Length", out var clStr) && int.TryParse(clStr, out var cl) ? cl : 0;
        var bodyBytes = new byte[contentLength];

        int bodyStart = headerEnd + 4; // skip the blank line ("\r\n\r\n")
        int alreadyBuffered = Math.Min(buffer.Count - bodyStart, contentLength);
        if (alreadyBuffered > 0) buffer.CopyTo(bodyStart, bodyBytes, 0, alreadyBuffered);

        int received = alreadyBuffered;
        while (received < contentLength)
        {
            int read = await stream.ReadAsync(bodyBytes.AsMemory(received, contentLength - received), cancellationToken);
            if (read == 0) break;
            received += read;
        }

        return (method, path, query, headers, bodyBytes);
    }

    private static int IndexOfHeaderTerminator(List<byte> buffer)
    {
        for (int i = 0; i + 3 < buffer.Count; i++)
        {
            if (buffer[i] == '\r' && buffer[i + 1] == '\n' && buffer[i + 2] == '\r' && buffer[i + 3] == '\n')
                return i;
        }
        return -1;
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
            : JsonSerializer.SerializeToUtf8Bytes(payload, JsonOptions);

        var header = new StringBuilder()
            .Append($"HTTP/1.1 {statusCode} {reason}\r\n")
            .Append("Content-Type: application/json; charset=utf-8\r\n")
            .Append($"Content-Length: {body.Length}\r\n")
            .Append("Cache-Control: no-store\r\n")
            .Append("Access-Control-Allow-Methods: GET, POST, OPTIONS\r\n")
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

    private static async Task WriteBinaryResponseAsync(
        NetworkStream stream,
        int statusCode,
        string reason,
        byte[] body,
        string fileName,
        string? origin,
        CancellationToken cancellationToken)
    {
        var header = new StringBuilder()
            .Append($"HTTP/1.1 {statusCode} {reason}\r\n")
            .Append("Content-Type: application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\r\n")
            .Append($"Content-Length: {body.Length}\r\n")
            .Append($"Content-Disposition: attachment; filename=\"{fileName}\"\r\n")
            .Append("Cache-Control: no-store\r\n")
            .Append("Access-Control-Allow-Methods: GET, POST, OPTIONS\r\n")
            .Append("Access-Control-Allow-Headers: Accept, Content-Type\r\n")
            .Append("Access-Control-Expose-Headers: Content-Disposition\r\n")
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
    public const string Version = "1.2.0";
}

internal sealed record NameListResult(bool AgentOnline, bool EtabsConnected, string? Error, string[] Names);

internal sealed record StoryInfo(string Name, double Elevation);

internal sealed record StoriesResult(bool AgentOnline, bool EtabsConnected, string? Error, StoryInfo[] Stories);

internal sealed record StoryDriftRow(string Story, string OutputCase, string Direction, double Drift);

internal sealed record StoryDriftsResult(bool AgentOnline, bool EtabsConnected, string? Error, StoryDriftRow[] Rows);

internal sealed record TableResult(bool AgentOnline, bool EtabsConnected, string? Error, string[] Fields, string[][] Rows);

internal sealed record FrameKey(string Story, string Label);

internal sealed record SelectResult(bool AgentOnline, bool EtabsConnected, string? Error, int SelectedCount);

internal sealed record FrameSectionInfo(string Unique, string Label, string Story, string Section, double H, double B);

internal sealed record FrameSectionsResult(bool AgentOnline, bool EtabsConnected, string? Error, FrameSectionInfo[] Sections);

internal sealed record SelectFramesRequest(FrameKey[] Items);

internal sealed record ColumnAxialRowDto(
    string Story, string Column, string UniqueName, string LoadCase,
    string Section, double B, double D, double P);

internal sealed record ColumnAxialExportRequest(double Fck, double Limit, ColumnAxialRowDto[] Rows);

// Builds the Kolon Eksenel Yük Excel report exactly as the desktop app's ExportExcel(): raw
// inputs only (Story/Column/Section/b/d/P), everything else (Ac, Ac*fck, Oran, Durum) is a live
// Excel formula referencing the fck/limit parameter cells, so the workbook stays editable after
// download.
internal static class ColumnAxialExcelReport
{
    public static byte[] Build(double fck, double limit, IReadOnlyList<ColumnAxialRowDto> rows)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Kolon Eksenel Raporu");

        ws.Cells[1, 1, 1, 13].Merge = true;
        ws.Cells[1, 1].Value = "KOLON EKSENEL YÜK KONTROLÜ";
        ws.Cells[1, 1].Style.Font.Size = 14;
        ws.Cells[1, 1].Style.Font.Bold = true;
        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        ws.Cells[1, 15].Value = "RAPOR PARAMETRELERİ";
        ws.Cells[1, 15, 1, 16].Merge = true;
        ws.Cells[1, 15].Style.Font.Bold = true;
        ws.Cells[1, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[1, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells[1, 15].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(240, 240, 240));

        ws.Cells[2, 15].Value = "Beton Sınıfı (fck):";
        ws.Cells[2, 16].Value = fck;
        ws.Cells[3, 15].Value = "Eksenel Yük Sınırı:";
        ws.Cells[3, 16].Value = limit;
        ws.Cells[2, 16].Style.Font.Bold = true;
        ws.Cells[3, 16].Style.Font.Bold = true;
        ws.Cells[3, 16].Style.Font.Color.SetColor(DrawingColor.DarkBlue);

        string[] headers = { "Story", "Column", "Unique Name", "fck", "Load Case", "Section", "b (cm)", "d (cm)", "Ac (cm2)", "Ac*fck (kN)", "P (kN)", "Oran", "Durum" };
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = ws.Cells[3, i + 1];
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(DrawingColor.LightGray);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        const int startRow = 4;
        for (int i = 0; i < rows.Count; i++)
        {
            var r = startRow + i;
            var item = rows[i];
            ws.Cells[r, 1].Value = item.Story;
            ws.Cells[r, 2].Value = item.Column;
            ws.Cells[r, 3].Value = item.UniqueName;
            ws.Cells[r, 4].Formula = "$P$2";
            ws.Cells[r, 5].Value = item.LoadCase;
            ws.Cells[r, 6].Value = item.Section;
            ws.Cells[r, 7].Value = item.B == 0 ? null : (object)item.B;
            ws.Cells[r, 8].Value = item.D;
            ws.Cells[r, 9].Formula = $"IF(G{r}=\"\", PI()*POWER(H{r},2)/4, G{r}*H{r})";
            ws.Cells[r, 10].Formula = $"(I{r}*D{r})/10";
            ws.Cells[r, 11].Value = item.P;
            ws.Cells[r, 12].Formula = $"IF(J{r}<>0, K{r}/J{r}, 0)";
            ws.Cells[r, 13].Formula = $"IF(L{r}<=$P$3, \"OK\", \"NOT OK\")";
        }

        int lastRow = startRow + rows.Count - 1;
        if (rows.Count > 0)
        {
            var range = ws.Cells[$"M{startRow}:M{lastRow}"];
            var notOk = ws.ConditionalFormatting.AddEqual(range);
            notOk.Formula = "\"NOT OK\"";
            notOk.Style.Fill.BackgroundColor.Color = DrawingColor.LightPink;
            notOk.Style.Font.Bold = true;

            var ok = ws.ConditionalFormatting.AddEqual(range);
            ok.Formula = "\"OK\"";
            ok.Style.Fill.BackgroundColor.Color = DrawingColor.LightGreen;

            var colorScale = ws.ConditionalFormatting.AddThreeColorScale(ws.Cells[$"L{startRow}:L{lastRow}"]);
            colorScale.LowValue.Color = DrawingColor.LightGreen;
            colorScale.MiddleValue.Color = DrawingColor.Yellow;
            colorScale.HighValue.Color = DrawingColor.Red;
        }

        ws.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }
}

internal sealed record GoreliKatExportRow(string Story, string Combo, string Direction, double Drift);

internal sealed record GoreliKatExportRequest(
    double SdsDD2, double SdsDD3, double Sd1DD2, double Sd1DD3, double Tp, double K,
    bool EsnekDerz, GoreliKatExportRow[] Rows);

// Göreli Kat Ötelemesi report: only the raw per-row Drift value and the TBDY parameters are
// hard values; Lambda, Limit, λ·δi/hi, and Durum are live Excel formulas so the sheet recomputes
// if the engineer edits SDS/SD1/Tp/k after downloading it — same pattern as the column axial report.
internal static class GoreliKatExcelReport
{
    public static byte[] Build(double sdsDD2, double sdsDD3, double sd1DD2, double sd1DD3, double tp, double k, bool esnekDerz, IReadOnlyList<GoreliKatExportRow> rows)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Goreli Kat Otelemesi");

        ws.Cells[1, 1, 1, 7].Merge = true;
        ws.Cells[1, 1].Value = "GÖRELİ KAT ÖTELEMESİ TAHKİKİ";
        ws.Cells[1, 1].Style.Font.Size = 14;
        ws.Cells[1, 1].Style.Font.Bold = true;
        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        ws.Cells[1, 9].Value = "RAPOR PARAMETRELERİ";
        ws.Cells[1, 9, 1, 10].Merge = true;
        ws.Cells[1, 9].Style.Font.Bold = true;
        ws.Cells[1, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[1, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells[1, 9].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(240, 240, 240));

        void Param(int row, string label, object value)
        {
            ws.Cells[row, 9].Value = label;
            ws.Cells[row, 10].Value = value;
            ws.Cells[row, 10].Style.Font.Bold = true;
        }
        Param(2, "SDS (DD-2):", sdsDD2);
        Param(3, "SDS (DD-3):", sdsDD3);
        Param(4, "SD1 (DD-2):", sd1DD2);
        Param(5, "SD1 (DD-3):", sd1DD3);
        Param(6, "Tp:", tp);
        Param(7, "k:", k);
        Param(8, "Esnek Derz (1=Var):", esnekDerz ? 1 : 0);
        ws.Cells[9, 9].Value = "TA (=SD1DD2/SDSDD2):";
        ws.Cells[9, 10].Formula = "IF($J$2=0,0,$J$4/$J$2)";
        ws.Cells[10, 9].Value = "Lambda:";
        ws.Cells[10, 10].Formula = "IF($J$2=0,0,IF($J$6<$J$9,$J$3/$J$2,$J$5/$J$4))";
        ws.Cells[11, 9].Value = "Limit:";
        ws.Cells[11, 10].Formula = "IF($J$8=1,0.016*$J$7,0.008*$J$7)";
        ws.Cells[10, 10].Style.Font.Color.SetColor(DrawingColor.DarkBlue);
        ws.Cells[11, 10].Style.Font.Color.SetColor(DrawingColor.DarkBlue);

        string[] headers = { "Kat", "Kombinasyon", "Yön", "Drift", "λ·δi/hi", "Limit", "Durum" };
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = ws.Cells[3, i + 1];
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(DrawingColor.LightGray);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        const int startRow = 4;
        for (int i = 0; i < rows.Count; i++)
        {
            var r = startRow + i;
            var item = rows[i];
            ws.Cells[r, 1].Value = item.Story;
            ws.Cells[r, 2].Value = item.Combo;
            ws.Cells[r, 3].Value = item.Direction;
            ws.Cells[r, 4].Value = item.Drift;
            ws.Cells[r, 5].Formula = $"$J$10*D{r}";
            ws.Cells[r, 6].Formula = "$J$11";
            ws.Cells[r, 7].Formula = $"IF(E{r}<=F{r},\"OK\",\"NOT OK\")";
        }

        int lastRow = startRow + rows.Count - 1;
        if (rows.Count > 0)
        {
            var range = ws.Cells[$"G{startRow}:G{lastRow}"];
            var notOk = ws.ConditionalFormatting.AddEqual(range);
            notOk.Formula = "\"NOT OK\"";
            notOk.Style.Fill.BackgroundColor.Color = DrawingColor.LightPink;
            notOk.Style.Font.Bold = true;

            var ok = ws.ConditionalFormatting.AddEqual(range);
            ok.Formula = "\"OK\"";
            ok.Style.Fill.BackgroundColor.Color = DrawingColor.LightGreen;
        }

        ws.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }
}

internal sealed record IkinciMertebeExportRow(string Story, string Combo, string Direction, double Vi, double Wij, double DriftRatio);

internal sealed record IkinciMertebeExportRequest(double Ch, double R, double D, IkinciMertebeExportRow[] Rows);

// İkinci Mertebe report: Story Forces / Mass / Drift inputs (Vi, Wij, DriftRatio) are raw values
// (they come from cumulative per-story ETABS results), while Theta, Limit, and Durum are live
// Excel formulas referencing the Ch/R/D parameter cells.
internal static class IkinciMertebeExcelReport
{
    public static byte[] Build(double ch, double r, double d, IReadOnlyList<IkinciMertebeExportRow> rows)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Ikinci Mertebe");

        ws.Cells[1, 1, 1, 9].Merge = true;
        ws.Cells[1, 1].Value = "İKİNCİ MERTEBE ETKİLERİ TAHKİKİ";
        ws.Cells[1, 1].Style.Font.Size = 14;
        ws.Cells[1, 1].Style.Font.Bold = true;
        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        ws.Cells[1, 11].Value = "RAPOR PARAMETRELERİ";
        ws.Cells[1, 11, 1, 12].Merge = true;
        ws.Cells[1, 11].Style.Font.Bold = true;
        ws.Cells[1, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[1, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells[1, 11].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(240, 240, 240));

        ws.Cells[2, 11].Value = "Ch:";
        ws.Cells[2, 12].Value = ch;
        ws.Cells[3, 11].Value = "R:";
        ws.Cells[3, 12].Value = r;
        ws.Cells[4, 11].Value = "D:";
        ws.Cells[4, 12].Value = d;
        ws.Cells[2, 12].Style.Font.Bold = true;
        ws.Cells[3, 12].Style.Font.Bold = true;
        ws.Cells[4, 12].Style.Font.Bold = true;
        ws.Cells[5, 11].Value = "Limit (=0.12*D/(Ch*R)):";
        ws.Cells[5, 12].Formula = "IF($L$2*$L$3=0,0,0.12*$L$4/($L$2*$L$3))";
        ws.Cells[5, 12].Style.Font.Color.SetColor(DrawingColor.DarkBlue);

        string[] headers = { "Kat", "Kombinasyon", "Yön", "Vi (kN)", "Wij (kN)", "Drift", "Theta (θ)", "Limit", "Durum" };
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = ws.Cells[3, i + 1];
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cell.Style.Fill.BackgroundColor.SetColor(DrawingColor.LightGray);
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        const int startRow = 4;
        for (int i = 0; i < rows.Count; i++)
        {
            var r2 = startRow + i;
            var item = rows[i];
            ws.Cells[r2, 1].Value = item.Story;
            ws.Cells[r2, 2].Value = item.Combo;
            ws.Cells[r2, 3].Value = item.Direction;
            ws.Cells[r2, 4].Value = item.Vi;
            ws.Cells[r2, 5].Value = item.Wij;
            ws.Cells[r2, 6].Value = item.DriftRatio;
            ws.Cells[r2, 7].Formula = $"IF(D{r2}=0,0,F{r2}*E{r2}/D{r2})";
            ws.Cells[r2, 8].Formula = "$L$5";
            ws.Cells[r2, 9].Formula = $"IF(G{r2}<=H{r2},\"OK\",\"NOT OK\")";
        }

        int lastRow = startRow + rows.Count - 1;
        if (rows.Count > 0)
        {
            var range = ws.Cells[$"I{startRow}:I{lastRow}"];
            var notOk = ws.ConditionalFormatting.AddEqual(range);
            notOk.Formula = "\"NOT OK\"";
            notOk.Style.Fill.BackgroundColor.Color = DrawingColor.LightPink;
            notOk.Style.Font.Bold = true;

            var ok = ws.ConditionalFormatting.AddEqual(range);
            ok.Formula = "\"OK\"";
            ok.Style.Fill.BackgroundColor.Color = DrawingColor.LightGreen;
        }

        ws.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }
}

internal sealed record BeamShearExportRow(string Story, string Label, string Section, double Vd, double B, double H, double D, int N, int Phi, double S);

internal sealed record BeamShearExportRequest(double Fck, double Fyk, bool UseVc, BeamShearExportRow[] Rows);

// Kiriş Kesme report — mirrors the desktop ExportExcel(): geometry/forces and the editable
// stirrup design (n legs, φ, spacing) are hard values, while Vr is an Excel formula referencing
// those cells (fyd/fctd and the Vc contribution are baked constants, exactly as the desktop does),
// and Durum is a formula. Editing n/φ/s/d in the sheet recomputes Vr and the pass/fail live.
internal static class BeamShearExcelReport
{
    public static byte[] Build(double fck, double fyk, bool useVc, IReadOnlyList<BeamShearExportRow> rows)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Kiris Kesme Raporu");

        ws.Cells[1, 1, 1, 12].Merge = true;
        ws.Cells[1, 1].Value = "KİRİŞ KESME GÜVENLİĞİ KONTROLÜ";
        ws.Cells[1, 1].Style.Font.Size = 14;
        ws.Cells[1, 1].Style.Font.Bold = true;
        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(204, 255, 204));

        ws.Cells[2, 1].Value = "fck:";
        ws.Cells[2, 2].Value = fck;
        ws.Cells[2, 3].Value = "fyk:";
        ws.Cells[2, 4].Value = fyk;
        ws.Cells[2, 5].Value = "Vc:";
        ws.Cells[2, 6].Value = useVc ? "Var" : "Yok";

        string[] headers = { "Story", "Beam", "Section", "Vd (kN)", "b(cm)", "h(cm)", "d(cm)", "Kolu (n)", "Çap (mm)", "Aralık (s)", "Vr (kN)", "Durum" };
        for (int i = 0; i < headers.Length; i++)
        {
            ws.Cells[3, i + 1].Value = headers[i];
            ws.Cells[3, i + 1].Style.Font.Bold = true;
            ws.Cells[3, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(240, 240, 240));
        }

        var fyd = fyk / 1.15;
        var fctd = 0.35 * Math.Sqrt(fck) / 1.5;
        var ci = System.Globalization.CultureInfo.InvariantCulture;

        int row = 4;
        foreach (var item in rows)
        {
            ws.Cells[row, 1].Value = item.Story;
            ws.Cells[row, 2].Value = item.Label;
            ws.Cells[row, 3].Value = item.Section;
            ws.Cells[row, 4].Value = item.Vd;
            ws.Cells[row, 5].Value = item.B;
            ws.Cells[row, 6].Value = item.H;
            ws.Cells[row, 7].Value = item.D;
            ws.Cells[row, 8].Value = item.N;
            ws.Cells[row, 9].Value = item.Phi;
            ws.Cells[row, 10].Value = item.S;

            var vcComponent = useVc ? 0.65 * fctd * (item.B / 100.0) * (item.D / 100.0) * 1000 * 0.8 : 0;
            ws.Cells[row, 11].Formula = $"((H{row}*3.14159265*(I{row}/10)^2)/4/J{row})*G{row}*{fyd.ToString(ci)}*0.1 + {vcComponent.ToString(ci)}";
            ws.Cells[row, 12].Formula = $"IF(D{row}<=K{row},\"OK\", \"NOT OK\")";
            row++;
        }

        int lastRow = row - 1;
        if (rows.Count > 0)
        {
            var vdRange = ws.Cells[$"D4:D{lastRow}"];
            var condScale = vdRange.ConditionalFormatting.AddThreeColorScale();
            condScale.LowValue.Color = DrawingColor.LightGreen;
            condScale.MiddleValue.Color = DrawingColor.Yellow;
            condScale.HighValue.Color = DrawingColor.Salmon;

            var statusRange = ws.Cells[$"L4:L{lastRow}"];
            var condOk = statusRange.ConditionalFormatting.AddEqual();
            condOk.Formula = "\"OK\"";
            condOk.Style.Font.Color.Color = DrawingColor.Green;
            var condNotOk = statusRange.ConditionalFormatting.AddNotEqual();
            condNotOk.Formula = "\"OK\"";
            condNotOk.Style.Font.Color.Color = DrawingColor.Red;
            condNotOk.Style.Font.Bold = true;
        }

        ws.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }
}

internal sealed record BeamAxialExportRow(string Story, string Label, string Unique, string LoadCase, string Section, double B, double D, double P);

internal sealed record BeamAxialExportRequest(double Fck, double Limit, BeamAxialExportRow[] Rows);

// Kiriş Eksenel report — geometry/forces are hard values; fck, Ac, Ac*fck, ratio and Durum are
// Excel formulas referencing the fck (B2) and limit (M2) parameter cells, mirroring the desktop.
internal static class BeamAxialExcelReport
{
    public static byte[] Build(double fck, double limit, IReadOnlyList<BeamAxialExportRow> rows)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Kiris Eksenel Raporu");

        ws.Cells[1, 1, 1, 13].Merge = true;
        ws.Cells[1, 1].Value = "KİRİŞ EKSENEL YÜK KONTROLÜ";
        ws.Cells[1, 1].Style.Font.Size = 14;
        ws.Cells[1, 1].Style.Font.Bold = true;
        ws.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        ws.Cells[1, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(204, 255, 204));

        ws.Cells[2, 1].Value = "fck";
        ws.Cells[2, 2].Value = fck;
        ws.Cells[2, 12].Value = "Sınır Oran";
        ws.Cells[2, 13].Value = limit;
        ws.Cells[2, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
        ws.Cells[2, 13].Style.Fill.BackgroundColor.SetColor(DrawingColor.LightYellow);

        string[] headers = { "Story", "Beam", "Unique Name", "fck", "Load Case", "Section", "b(cm)", "d(cm)", "Ac", "Ac*fck", "P", "ratio", "Durum" };
        for (int i = 0; i < headers.Length; i++)
        {
            ws.Cells[3, i + 1].Value = headers[i];
            ws.Cells[3, i + 1].Style.Font.Bold = true;
            ws.Cells[3, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[3, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[3, i + 1].Style.Fill.BackgroundColor.SetColor(DrawingColor.FromArgb(240, 240, 240));
        }

        int row = 4;
        foreach (var item in rows)
        {
            ws.Cells[row, 1].Value = item.Story;
            ws.Cells[row, 2].Value = item.Label;
            ws.Cells[row, 3].Value = item.Unique;
            ws.Cells[row, 4].Formula = "$B$2";
            ws.Cells[row, 5].Value = item.LoadCase;
            ws.Cells[row, 6].Value = item.Section;
            ws.Cells[row, 7].Value = item.B;
            ws.Cells[row, 8].Value = item.D;
            ws.Cells[row, 9].Formula = $"G{row}*H{row}";
            ws.Cells[row, 10].Formula = $"(I{row}*D{row})/10";
            ws.Cells[row, 11].Value = item.P;
            ws.Cells[row, 12].Formula = $"IF(J{row}<>0,K{row}/J{row},0)";
            ws.Cells[row, 13].Formula = $"IF(L{row}<=$M$2,\"OK\",\"KOLON GİBİ DONATILACAK\")";
            row++;
        }

        int lastRow = row - 1;
        if (rows.Count > 0)
        {
            var ratioRange = ws.Cells[$"L4:L{lastRow}"];
            var condScale = ratioRange.ConditionalFormatting.AddThreeColorScale();
            condScale.LowValue.Color = DrawingColor.LightGreen;
            condScale.MiddleValue.Color = DrawingColor.Yellow;
            condScale.HighValue.Color = DrawingColor.Salmon;

            var statusRange = ws.Cells[$"M4:M{lastRow}"];
            var condOk = statusRange.ConditionalFormatting.AddEqual();
            condOk.Formula = "\"OK\"";
            condOk.Style.Font.Color.Color = DrawingColor.Green;
            var condNotOk = statusRange.ConditionalFormatting.AddNotEqual();
            condNotOk.Formula = "\"OK\"";
            condNotOk.Style.Font.Color.Color = DrawingColor.Red;
            condNotOk.Style.Font.Bold = true;
        }

        ws.Cells.AutoFitColumns();
        return package.GetAsByteArray();
    }
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
