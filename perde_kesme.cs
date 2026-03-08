using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CSiAPIv1;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace EtabsTools
{
    public class WallShearLogic
    {
        public readonly cSapModel SapModel;

        public readonly Dictionary<string, StoryInfo> _storyData = new Dictionary<string, StoryInfo>();
        public readonly List<string> _orderedStoryNames = new List<string>();
        public readonly List<string> _combos = new List<string>();
        public readonly List<string> _patterns = new List<string>();

        public readonly Dictionary<string, PierForceInfo> _pierData = new Dictionary<string, PierForceInfo>();
        public readonly List<RawResultRow> _rawResults = new List<RawResultRow>();

        public readonly Dictionary<string, V05CalculatedInfo> _calculated05VValues = new Dictionary<string, V05CalculatedInfo>();
        public readonly Dictionary<string, double> _shortPierResults = new Dictionary<string, double>();

        public readonly List<V05DetailRow> _detailed05VResults = new List<V05DetailRow>();
        public readonly List<ShortPierDetailRow> _detailedSpResults = new List<ShortPierDetailRow>();

        public WallShearLogic(cSapModel sapModel)
        {
            SapModel = sapModel;
        }

        #region Data Models

        public class StoryInfo { public double Elevation { get; set; } public double TopZ { get; set; } public double BottomZ { get; set; } public double Height { get; set; } public int Order { get; set; } }
        public class PierGeometryStoryInfo { public double Bw { get; set; } public double Lw { get; set; } }
        public class PierGeometryInfo { public double Hw { get; set; } public Dictionary<string, PierGeometryStoryInfo> Stories { get; set; } = new Dictionary<string, PierGeometryStoryInfo>(); public bool IsShortDef { get; set; } public double Ratio { get; set; } public double BottomLw { get; set; } }
        public class RawResultRow { public string Story { get; set; } = ""; public string Pier { get; set; } = ""; public string Combo { get; set; } = ""; public string Location { get; set; } = ""; public double P { get; set; } public double V2 { get; set; } public double V3 { get; set; } public double T { get; set; } public double M2 { get; set; } public double M3 { get; set; } }
        public class PierForceInfo { public double Vd { get; set; } }
        public class GeometryOverride { public double B { get; set; } public double D { get; set; } public bool IsCoupled { get; set; } public bool IsShort { get; set; } }
        public class V05CalculatedInfo { public double Total { get; set; } public double EqPart { get; set; } public double SoilPart { get; set; } }
        public class V05DetailRow { public string Story { get; set; } = ""; public string Pier { get; set; } = ""; public string EqCombo { get; set; } = "-"; public double RawEqVal { get; set; } public double EqVal { get; set; } public double Local05 { get; set; } public string Soil1Name { get; set; } = "-"; public double Soil1Val { get; set; } public string Soil2Name { get; set; } = "-"; public double Soil2Val { get; set; } public double Total { get; set; } }
        public class ShortPierDetailRow { public string Story { get; set; } = ""; public string Pier { get; set; } = ""; public double HwLw { get; set; } public double Coeff { get; set; } public string EqCombo { get; set; } = "-"; public double EqVal { get; set; } public double AmpEq { get; set; } public string Soil1Name { get; set; } = "-"; public double Soil1Val { get; set; } public string Soil2Name { get; set; } = "-"; public double Soil2Val { get; set; } public double Total { get; set; } }
        public class CalculationInput { public double FckLower { get; set; } public double FckUpper { get; set; } public bool SecondaryFckActive { get; set; } public string SplitStory { get; set; } = ""; public double Fyd { get; set; } public bool BwRuleActive { get; set; } public double BwThreshold { get; set; } public int BwMinNsh { get; set; } public bool Rule05Active { get; set; } public List<int> NOpts { get; set; } = new List<int>(); public List<int> FOpts { get; set; } = new List<int>(); public List<int> SOpts { get; set; } = new List<int>(); public Dictionary<string, GeometryOverride> GeometryOverrides { get; set; } = new Dictionary<string, GeometryOverride>(); public Dictionary<string, List<string>> StoryGroups { get; set; } = new Dictionary<string, List<string>>(); }
        public class CalculationCombination { public double C { get; set; } public int N { get; set; } public int F { get; set; } public int S { get; set; } }
        public class StoryCalculationSnapshot { public string Story { get; set; } = ""; public string Pier { get; set; } = ""; public double Bw { get; set; } public double Lw { get; set; } public bool IsCoupled { get; set; } public bool IsShort { get; set; } public double FckUsed { get; set; } public double HwLw { get; set; } public string Status { get; set; } public double Vmax { get; set; } public double Vc { get; set; } public double VdDesign { get; set; } public double V05Val { get; set; } public double VdFinal { get; set; } public double VdDesignRaw { get; set; } public string CalcSource { get; set; } = ""; public double Coeff { get; set; } public double CReq { get; set; } public PierForceInfo DataRaw { get; set; } public int N { get; set; } public int F { get; set; } public int S { get; set; } public double Vw { get; set; } public double Vr { get; set; } public string StatusText { get; set; } = ""; public string Tag { get; set; } = ""; public double KapVal { get; set; } public double PurVal { get; set; } }
        public class GraphStoryData { public string Story { get; set; } = ""; public double Bw { get; set; } public double Lw { get; set; } public double VmaxPos { get; set; } public double VmaxNeg { get; set; } public Dictionary<string, double> ExtraValues { get; set; } = new Dictionary<string, double>(); }
        public class GraphPierData { public string Pier { get; set; } = ""; public List<GraphStoryData> Data { get; set; } = new List<GraphStoryData>(); }

        #endregion

        #region Initial Load / Geometry

        public void LoadInitialData()
        {
            _combos.Clear(); _patterns.Clear(); _storyData.Clear(); _orderedStoryNames.Clear();
            try { int n = 0; string[] names = null; SapModel.RespCombo.GetNameList(ref n, ref names); if (n > 0 && names != null) foreach (var x in names) if (!string.IsNullOrWhiteSpace(x)) _combos.Add(x.Trim()); } catch { }
            try { int n = 0; string[] names = null; SapModel.LoadPatterns.GetNameList(ref n, ref names); if (n > 0 && names != null) foreach (var x in names) if (!string.IsNullOrWhiteSpace(x)) _patterns.Add(x.Trim()); } catch { }
            try { double b = 0; int n = 0; string[] sn = null; double[] e = null; double[] h = null; bool[] m = null; string[] sim = null; bool[] sa = null; double[] sh = null; int[] c = null;
                SapModel.Story.GetStories_2(ref b, ref n, ref sn, ref e, ref h, ref m, ref sim, ref sa, ref sh, ref c);
                for (int i = 0; i < n; i++) { string s = (sn[i] ?? "").Trim(); if (s.Length == 0) continue; _orderedStoryNames.Add(s); double top = e != null && i < e.Length ? e[i] : 0; double hh = h != null && i < h.Length ? h[i] : 0; _storyData[s] = new StoryInfo { Elevation = top, TopZ = top, BottomZ = top - hh, Height = hh, Order = i }; }
            } catch { }
        }

        public Dictionary<string, PierGeometryInfo> GetPierGeometryData()
        {
            var map = new Dictionary<string, PierGeometryInfo>();
            try { int n = 0; string[] pierNames = null; SapModel.PierLabel.GetNameList(ref n, ref pierNames); if (n <= 0 || pierNames == null) return map;
                foreach (string pierRaw in pierNames) { string pier = (pierRaw ?? "").Trim(); if (pier.Length == 0) continue;
                    int ns = 0; string[] sn = null; double[] axis = null; int[] na = null; int[] nl = null; double[] wb = null; double[] tb = null; double[] wt = null; double[] tt = null; string[] mp = null; double[] cbx = null, cby = null, cbz = null, ctx = null, cty = null, ctz = null;
                    SapModel.PierLabel.GetSectionProperties(pier, ref ns, ref sn, ref axis, ref na, ref nl, ref wb, ref tb, ref wt, ref tt, ref mp, ref cbx, ref cby, ref cbz, ref ctx, ref cty, ref ctz);
                    var g = new PierGeometryInfo();
                    for (int i = 0; i < ns; i++) { string s = (sn != null && i < sn.Length ? sn[i] : "").Trim(); if (s.Length == 0) continue; g.Stories[s] = new PierGeometryStoryInfo { Lw = wb != null && i < wb.Length ? wb[i] * 100 : 0, Bw = tb != null && i < tb.Length ? tb[i] * 100 : 0 }; }
                    double minBottom = double.PositiveInfinity, maxTop = double.NegativeInfinity, lowBottom = double.PositiveInfinity;
                    foreach (var kv in g.Stories) { if (_storyData.TryGetValue(kv.Key, out var sInfo)) { if (sInfo.BottomZ < minBottom) minBottom = sInfo.BottomZ; if (sInfo.TopZ > maxTop) maxTop = sInfo.TopZ; if (sInfo.BottomZ < lowBottom) { lowBottom = sInfo.BottomZ; g.BottomLw = kv.Value.Lw; } } }
                    g.Hw = maxTop > minBottom && minBottom < double.PositiveInfinity ? (maxTop - minBottom) : 0;
                    g.Ratio = g.BottomLw > 0 ? (g.Hw * 100.0 / g.BottomLw) : 0;
                    g.IsShortDef = g.Ratio > 0 && g.Ratio <= 2.0;
                    map[pier] = g;
                }
            } catch { } return map;
        }

        #endregion

        #region Force Fetch & 0.5V & Short Pier

        public List<RawResultRow> FetchForces(List<string> upperCombos, List<string> basementCombos, bool useRijit, string rijitStory)
        {
            _rawResults.Clear(); _pierData.Clear();
            var combosToFetch = useRijit && _orderedStoryNames.Contains(rijitStory) ? upperCombos.Concat(basementCombos).Distinct().ToList() : upperCombos.ToList();
            if (combosToFetch.Count == 0) return _rawResults;
            int splitIndex = useRijit && _orderedStoryNames.Contains(rijitStory) ? _orderedStoryNames.IndexOf(rijitStory) : -1;

            foreach (string combo in combosToFetch) {
                try { SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                    if (_patterns.Contains(combo)) SapModel.Results.Setup.SetCaseSelectedForOutput(combo); else SapModel.Results.Setup.SetComboSelectedForOutput(combo);
                    int n = 0; string[] st = null, pr = null, ld = null, loc = null; double[] p = null, v2 = null, v3 = null, t = null, m2 = null, m3 = null;
                    SapModel.Results.PierForce(ref n, ref st, ref pr, ref ld, ref loc, ref p, ref v2, ref v3, ref t, ref m2, ref m3);
                    if (n <= 0 || st == null || pr == null) continue;
                    for (int i = 0; i < n; i++) {
                        string storyName = (st[i] ?? "").Trim(); string pierName = (pr[i] ?? "").Trim();
                        if (useRijit && splitIndex != -1) { int idx = _orderedStoryNames.IndexOf(storyName); if (idx == -1) continue; if (idx > splitIndex) { if (!upperCombos.Contains(combo)) continue; } else { if (!basementCombos.Contains(combo)) continue; } } else { if (!upperCombos.Contains(combo)) continue; }
                        _rawResults.Add(new RawResultRow { Story = storyName, Pier = pierName, Combo = (ld[i] ?? "").Trim(), Location = (loc[i] ?? "").Trim(), P = p[i], V2 = v2[i], V3 = v3[i], T = t[i], M2 = m2[i], M3 = m3[i] });
                    }
                } catch { }
            }
            var tmp = new Dictionary<string, List<double>>();
            foreach (var r in _rawResults) { string k = r.Story + "::" + r.Pier; if (!tmp.ContainsKey(k)) tmp[k] = new List<double>(); tmp[k].Add(r.V2); }
            foreach (var kv in tmp) _pierData[kv.Key] = new PierForceInfo { Vd = kv.Value.Select(Math.Abs).DefaultIfEmpty(0.0).Max() };
            return _rawResults;
        }

        public void Fetch05VData(List<string> eqCombos, List<string> soilCombos, Dictionary<string, GeometryOverride> geometryOverrides)
        {
            _detailed05VResults.Clear(); _calculated05VValues.Clear();
            var allItems = eqCombos.Concat(soilCombos).Distinct().ToList(); if (allItems.Count == 0) return;
            try { SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput(); foreach (string item in allItems) { if (_patterns.Contains(item)) SapModel.Results.Setup.SetCaseSelectedForOutput(item); else SapModel.Results.Setup.SetComboSelectedForOutput(item); } } catch { return; }
            int n = 0; string[] st = null, pr = null, ld = null, loc = null; double[] p = null, v2 = null, v3 = null, t = null, m2 = null, m3 = null;
            try { SapModel.Results.PierForce(ref n, ref st, ref pr, ref ld, ref loc, ref p, ref v2, ref v3, ref t, ref m2, ref m3); } catch { return; }
            if (n <= 0 || st == null || pr == null) return;
            var eqSet = eqCombos.ToHashSet(); var soilSet = soilCombos.ToHashSet();
            var map = new Dictionary<string, (double eq, string eqCombo, Dictionary<string, double> soil)>();
            for (int i = 0; i < n; i++) { string load = (ld[i] ?? "").Trim(); if (!allItems.Contains(load)) continue; string key = st[i].Trim() + "::" + pr[i].Trim();
                if (!map.ContainsKey(key)) map[key] = (0, "-", new Dictionary<string, double>());
                var data = map[key]; double val = Math.Abs(v2[i]);
                if (eqSet.Contains(load)) { if (val > data.eq) data = (val, load, data.soil); }
                else if (soilSet.Contains(load)) { double curr = data.soil.TryGetValue(load, out var prev) ? prev : 0; if (val > curr) data.soil[load] = val; }
                map[key] = data;
            }
            var pierMaxEq = new Dictionary<string, double>();
            foreach (var kvp in map) { string pier = kvp.Key.Split(new[] { "::" }, StringSplitOptions.None)[1]; double l05 = kvp.Value.eq * 0.5; if (!pierMaxEq.ContainsKey(pier) || l05 > pierMaxEq[pier]) pierMaxEq[pier] = l05; }
            foreach (var kvp in map) { string[] parts = kvp.Key.Split(new[] { "::" }, StringSplitOptions.None); string story = parts[0]; string pier = parts[1];
                double globalEq = pierMaxEq.TryGetValue(pier, out var gm) ? gm : 0; double soilSum = kvp.Value.soil.Values.Sum(); double total = globalEq + soilSum;
                _calculated05VValues[kvp.Key] = new V05CalculatedInfo { Total = total, EqPart = globalEq, SoilPart = soilSum };
                var sKeys = kvp.Value.soil.Keys.OrderBy(x => x).ToList(); string s1Name = sKeys.Count > 0 ? sKeys[0] : "-"; double s1Val = sKeys.Count > 0 ? kvp.Value.soil[s1Name] : 0; string s2Name = sKeys.Count > 1 ? sKeys[1] : "-"; double s2Val = sKeys.Count > 1 ? kvp.Value.soil[s2Name] : 0;
                _detailed05VResults.Add(new V05DetailRow { Story = story, Pier = pier, EqCombo = kvp.Value.eqCombo, RawEqVal = kvp.Value.eq, EqVal = kvp.Value.eq, Local05 = kvp.Value.eq * 0.5, Soil1Name = s1Name, Soil1Val = s1Val, Soil2Name = s2Name, Soil2Val = s2Val, Total = total });
            }
        }

        public void FetchShortPierData(List<string> eqCombos, List<string> soilCombos, List<string> activePiersList, Dictionary<string, GeometryOverride> geometryOverrides)
        {
            _shortPierResults.Clear(); _detailedSpResults.Clear();
            var allItems = eqCombos.Concat(soilCombos).Distinct().ToList(); if (allItems.Count == 0) return;
            try { SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput(); foreach (string item in allItems) { if (_patterns.Contains(item)) SapModel.Results.Setup.SetCaseSelectedForOutput(item); else SapModel.Results.Setup.SetComboSelectedForOutput(item); } } catch { return; }
            int n = 0; string[] st = null, pr = null, ld = null, loc = null; double[] p = null, v2 = null, v3 = null, t = null, m2 = null, m3 = null;
            try { SapModel.Results.PierForce(ref n, ref st, ref pr, ref ld, ref loc, ref p, ref v2, ref v3, ref t, ref m2, ref m3); } catch { return; }
            if (n <= 0 || st == null || pr == null) return;
            var eqSet = eqCombos.ToHashSet(); var soilSet = soilCombos.ToHashSet();
            var map = new Dictionary<string, (double eq, string eqCombo, Dictionary<string, double> soil)>();
            for (int i = 0; i < n; i++) {
                string load = (ld[i] ?? "").Trim(); if (!allItems.Contains(load)) continue;
                string pier = (pr[i] ?? "").Trim(); string story = (st[i] ?? "").Trim();
                if (activePiersList != null && activePiersList.Count > 0 && !activePiersList.Contains(pier)) continue;
                string key = story + "::" + pier;
                if (!map.ContainsKey(key)) map[key] = (0, "-", new Dictionary<string, double>());
                var data = map[key]; double val = Math.Abs(v2[i]);
                if (eqSet.Contains(load)) { if (val > data.eq) data = (val, load, data.soil); }
                else if (soilSet.Contains(load)) { double curr = data.soil.TryGetValue(load, out var prev) ? prev : 0; if (val > curr) data.soil[load] = val; }
                map[key] = data;
            }
            var geoDims = GetPierGeometryData();
            foreach (var kvp in map) {
                string[] parts = kvp.Key.Split(new[] { "::" }, StringSplitOptions.None); string story = parts[0]; string pier = parts[1];
                if (!geoDims.TryGetValue(pier, out var g)) continue;
                double hwM = g.Hw * 100.0; double hwLw = 0;
                if (g.Stories.TryGetValue(story, out var sg) && sg.Lw > 0) hwLw = hwM / sg.Lw;
                if (!g.IsShortDef && hwLw > 2.0) continue; // Only process actual short piers
                double coeff = 1.0; if (hwLw > 0) coeff = Math.Max(1.0, Math.Min(3.0 / (1.0 + hwLw), 2.0));
                double eqVal = kvp.Value.eq; double ampEq = eqVal * coeff; double soilSum = kvp.Value.soil.Values.Sum(); double total = ampEq + soilSum;
                _shortPierResults[kvp.Key] = total;
                var sKeys = kvp.Value.soil.Keys.OrderBy(x => x).ToList(); string s1Name = sKeys.Count > 0 ? sKeys[0] : "-"; double s1Val = sKeys.Count > 0 ? kvp.Value.soil[s1Name] : 0; string s2Name = sKeys.Count > 1 ? sKeys[1] : "-"; double s2Val = sKeys.Count > 1 ? kvp.Value.soil[s2Name] : 0;
                _detailedSpResults.Add(new ShortPierDetailRow { Story = story, Pier = pier, HwLw = hwLw, Coeff = coeff, EqCombo = kvp.Value.eqCombo, EqVal = eqVal, AmpEq = ampEq, Soil1Name = s1Name, Soil1Val = s1Val, Soil2Name = s2Name, Soil2Val = s2Val, Total = total });
            }
        }

        #endregion

        #region Main Design Calculation

        public List<StoryCalculationSnapshot> PerformCalculation(CalculationInput inputs)
        {
            double fyd = inputs.Fyd; double fckLower = inputs.FckLower; double fckUpper = inputs.SecondaryFckActive ? inputs.FckUpper : fckLower;
            int splitStoryOrder = -1; if (inputs.SecondaryFckActive && _storyData.TryGetValue(inputs.SplitStory, out var sInfo)) splitStoryOrder = sInfo.Order;
            if (_detailed05VResults.Count > 0) {
                var tmpMax = new Dictionary<string, double>();
                foreach (var r in _detailed05VResults) {
                    bool isCoupled = inputs.GeometryOverrides.TryGetValue($"{r.Story}::{r.Pier}", out var ov) && ov.IsCoupled;
                    double eq = r.RawEqVal > 0 ? r.RawEqVal : r.EqVal; r.EqVal = eq; r.Local05 = eq * 0.5;
                    if (!tmpMax.ContainsKey(r.Pier) || r.Local05 > tmpMax[r.Pier]) tmpMax[r.Pier] = r.Local05;
                }
                _calculated05VValues.Clear();
                foreach (var r in _detailed05VResults) {
                    double globalMax = tmpMax.TryGetValue(r.Pier, out var gm) ? gm : 0; double soilSum = r.Soil1Val + r.Soil2Val; double tot = globalMax + soilSum; r.Total = tot;
                    _calculated05VValues[$"{r.Story}::{r.Pier}"] = new V05CalculatedInfo { Total = tot, EqPart = globalMax, SoilPart = soilSum };
                }
            }
            var sGroups = inputs.StoryGroups ?? new Dictionary<string, List<string>>();
            if (sGroups.Count == 0) sGroups = _pierData.Keys.Select(k => k.Split(new[] { "::" }, StringSplitOptions.None)[0]).Distinct().ToDictionary(s => s, s => new List<string> { s });
            var combos = new List<CalculationCombination>();
            foreach (int n in inputs.NOpts) foreach (int f in inputs.FOpts) foreach (int s in inputs.SOpts) combos.Add(new CalculationCombination { C = n * (Math.PI * Math.Pow(f * 0.1, 2) / 4.0) / s, N = n, F = f, S = s });
            var sortedCombos = combos.OrderBy(x => x.N).ThenBy(x => x.C).ToList();
            var pDims = GetPierGeometryData();
            var res = new List<StoryCalculationSnapshot>();
            var uPiers = _pierData.Keys.Select(k => k.Split(new[] { "::" }, StringSplitOptions.None)[1]).Distinct().OrderBy(x => x).ToList();

            foreach (string pier in uPiers) {
                int minF = 0, minN = 0; var pInfo = pDims.TryGetValue(pier, out var pi) ? pi : new PierGeometryInfo();
                double hwCm = pInfo.Hw * 100.0; double baseLw = pInfo.BottomLw; string bottomStory = null; double minZ = double.PositiveInfinity;
                foreach (var s in pInfo.Stories.Keys) if (_storyData.TryGetValue(s, out var si) && si.BottomZ < minZ) { minZ = si.BottomZ; bottomStory = s; }
                if (bottomStory != null && inputs.GeometryOverrides.TryGetValue($"{bottomStory}::{pier}", out var bov) && bov.D > 0) baseLw = bov.D;
                bool isManShort = inputs.GeometryOverrides.Any(kvp => kvp.Key.EndsWith($"::{pier}") && kvp.Value.IsShort);
                bool isGlobalShort = isManShort || (baseLw > 0 && hwCm / baseLw > 0 && hwCm / baseLw <= 2.0);
                var sortedRefs = sGroups.Keys.OrderByDescending(s => _storyData.ContainsKey(s) ? _storyData[s].Order : -1).ToList();

                foreach (string refStory in sortedRefs) {
                    var groupStories = sGroups[refStory]; var gData = new List<StoryCalculationSnapshot>(); double maxCReq = 0, maxBw = 0;
                    foreach (string story in groupStories) {
                        if (!_storyData.ContainsKey(story)) continue; string key = $"{story}::{pier}"; if (!_pierData.TryGetValue(key, out var data)) continue;
                        double currFck = (inputs.SecondaryFckActive && _storyData[story].Order >= splitStoryOrder) ? fckUpper : fckLower; double fctd = 0.35 * Math.Sqrt(currFck) / 1.5;
                        double bwCm, lwCm; bool isCoupled; if (inputs.GeometryOverrides.TryGetValue(key, out var ovr)) { bwCm = ovr.B; lwCm = ovr.D; isCoupled = ovr.IsCoupled; } else { var geo = pInfo.Stories.TryGetValue(story, out var gs) ? gs : new PierGeometryStoryInfo(); bwCm = geo.Bw; lwCm = geo.Lw; isCoupled = false; }
                        double lHwLw = lwCm > 0 ? hwCm / lwCm : 0;
                        var snap = new StoryCalculationSnapshot { Story = story, Pier = pier, Bw = bwCm, Lw = lwCm, IsCoupled = isCoupled, IsShort = isGlobalShort, FckUsed = currFck, HwLw = lHwLw, DataRaw = data };
                        if (bwCm <= 0 || lwCm <= 0) { snap.Status = "GEO ERR"; gData.Add(snap); continue; }
                        if (bwCm > maxBw) maxBw = bwCm;
                        double vdFinal = data.Vd; string cSrc = "Orijinal Vd"; double coeff = 1.0;
                        if (isGlobalShort && lHwLw > 0) { coeff = Math.Max(1.0, Math.Min(3.0 / (1.0 + lHwLw), 2.0)); if (_shortPierResults.TryGetValue(key, out var v2New)) { vdFinal = v2New; cSrc = $"Bodur: {v2New:0}"; } else { vdFinal *= coeff; cSrc = "Bodur Katsayılı"; } }
                        double v05 = 0; if (inputs.Rule05Active && !isGlobalShort && _calculated05VValues.TryGetValue(key, out var vDat)) v05 = vDat.Total;
                        double vdDes = Math.Max(vdFinal, v05); if (v05 > vdFinal) cSrc += " | 0.5V";
                        double vmax = (isCoupled ? 0.065 : 0.085) * bwCm * lwCm * Math.Sqrt(currFck); double vc = 0.065 * bwCm * lwCm * fctd; double cReq = (vdDes - vc) > 0 ? (vdDes - vc) / (lwCm * fyd * 0.1) : 0;
                        if (cReq > maxCReq) maxCReq = cReq;
                        snap.Vmax = vmax; snap.Vc = vc; snap.VdDesign = vdDes; snap.V05Val = v05; snap.VdFinal = vdFinal; snap.VdDesignRaw = vdFinal; snap.CalcSource = cSrc; snap.Coeff = coeff; snap.CReq = cReq;
                        gData.Add(snap);
                    }
                    if (gData.Count == 0) continue;
                    var valid = gData.Where(s => s.Status == null).ToList();
                    if (valid.Count == 0) { foreach (var s in gData) { s.Tag = "FAIL"; s.StatusText = "GEO ERR"; res.Add(s); } continue; }
                    int curMinN = minN; if (inputs.BwRuleActive && maxBw >= inputs.BwThreshold) curMinN = Math.Max(curMinN, inputs.BwMinNsh);
                    var validCombos = sortedCombos.Where(c => c.F >= minF && c.N >= curMinN).ToList(); CalculationCombination finalCombo = null;
                    if (validCombos.Count == 0) finalCombo = sortedCombos.OrderByDescending(x => x.C).FirstOrDefault();
                    else { foreach (var c in validCombos) { if (maxCReq > 0 && c.C < maxCReq) continue; bool allOk = true; foreach (var s in valid) if (c.N * (Math.PI * Math.Pow(c.F / 10.0, 2) / 4.0) * (100.0 / c.S) < 0.25 * s.Bw) { allOk = false; break; } if (allOk) { finalCombo = c; break; } } if (finalCombo == null) finalCombo = validCombos.OrderByDescending(x => x.C).FirstOrDefault(); }
                    if (finalCombo == null) continue;
                    minF = finalCombo.F; minN = finalCombo.N;
                    foreach (var s in gData) {
                        if (s.Status == "GEO ERR") { s.Tag = "FAIL"; s.StatusText = "GEO ERR"; res.Add(s); continue; }
                        double vw = finalCombo.C * s.Lw * fyd * 0.1; double vr = s.Vc + vw; string sText = "O.K.", sTag = "OK";
                        if (s.VdDesign > s.Vmax) { sText = "Vd > Vmax"; sTag = "FAIL"; } else if (s.VdDesign > vr) { sText = "Vd > Vr"; sTag = "FAIL"; } else if (vr > s.Vmax) { sText = "Vr > Vmax"; sTag = "FAIL"; } else if (finalCombo.N * (Math.PI * Math.Pow(finalCombo.F / 10.0, 2) / 4.0) * (100.0 / finalCombo.S) < 0.25 * s.Bw) { sText = "Min. Donatı"; sTag = "FAIL"; }
                        s.N = finalCombo.N; s.F = finalCombo.F; s.S = finalCombo.S; s.Vw = vw; s.Vr = vr; s.StatusText = sText; s.Tag = sTag; s.KapVal = s.Vmax > 0 ? s.VdDesign / s.Vmax : 0; s.PurVal = vr > 0 ? s.VdDesign / vr : 0; s.DataRaw = null; res.Add(s);
                    }
                }
            }
            return res.OrderBy(x => x.Pier).ThenByDescending(x => _storyData.ContainsKey(x.Story) ? _storyData[x.Story].Order : -1).ToList();
        }

        #endregion

        #region Excel Export

        public void ExportExcelEPPlus(string path, double fck, double fyd, List<StoryCalculationSnapshot> tableData)
        {
            if (tableData == null || tableData.Count == 0) throw new InvalidOperationException("Veri yok.");
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var p = new ExcelPackage()) {
                var ws = p.Workbook.Worksheets.Add("Perde Kesme Raporu");
                string[] h = { "Kat No", "Perde No", "fck (MPa)", "bw (cm)", "lw (cm)", "n", "ϕ", "s (cm)", "Vmax (kN)", "Vr (kN)", "Vd (kN)", "Durum", "Donatı Kapasite", "Kesit Kapasite" };
                for (int i = 0; i < h.Length; i++) { ws.Cells[1, i + 1].Value = h[i]; ws.Cells[1, i + 1].Style.Font.Bold = true; ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid; ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray); ws.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; ws.Cells[1, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin); }
                ws.Cells["Q1"].Value = "PARAMETRELER"; ws.Cells["Q1"].Style.Font.Bold = true;
                ws.Cells["Q2"].Value = "fck"; ws.Cells["R2"].Value = fck;
                ws.Cells["Q3"].Value = "fyd"; ws.Cells["R3"].Value = fyd;
                int r = 2;
                var grouped = tableData.GroupBy(x => x.Pier).OrderBy(g => g.Key).ToList();
                foreach (var grp in grouped) {
                    var sorted = grp.OrderByDescending(x => _storyData.ContainsKey(x.Story) ? _storyData[x.Story].Order : -1).ToList();
                    foreach (var res in sorted) {
                        ws.Cells[r, 1].Value = res.Story; ws.Cells[r, 2].Value = grp.Key; ws.Cells[r, 3].Value = res.FckUsed; ws.Cells[r, 4].Value = res.Bw; ws.Cells[r, 5].Value = res.Lw;
                        ws.Cells[r, 6].Value = res.N; ws.Cells[r, 7].Value = res.F; ws.Cells[r, 8].Value = res.S; ws.Cells[r, 9].Value = res.Vmax; ws.Cells[r, 10].Value = res.Vr; ws.Cells[r, 11].Value = res.VdDesign;
                        ws.Cells[r, 12].Value = res.StatusText; ws.Cells[r, 13].Value = res.PurVal; ws.Cells[r, 14].Value = res.KapVal;
                        for(int c=1; c<=14; c++) { ws.Cells[r, c].Style.Border.BorderAround(ExcelBorderStyle.Thin); ws.Cells[r, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; }
                        r++;
                    }
                }
                ws.Cells.AutoFitColumns();
                p.SaveAs(new FileInfo(path));
            }
        }

        #endregion
    }
    
    // === UI CLASS ===
    public class PerdeKesmeUI
    {
        private readonly Form1 _form;
        private readonly Func<cSapModel> _getSapModel;
        private readonly Func<int, Panel> _createNavigationPanel;
        private readonly Action<int> _goToPage;
        private readonly Color _colorBackground;

        private ListBox _lstCombos;
        private FlowLayoutPanel _pnlSelectedCombos;
        private TextBox _fck, _fyd, _bwThreshold;
        private CheckedListBox _clbN, _clbPhi, _clbS;
        private CheckBox _chk05, _chkBodrum, _chkBwRule;
        private NumericUpDown _numBodrumKat;
        private DataGridView _dgv;
        private Label _lbl;

        private WallShearLogic _logic;
        private List<WallShearLogic.StoryCalculationSnapshot> _lastResults;

        public PerdeKesmeUI(Form1 form, Func<cSapModel> getSapModel, Func<int, Panel> createNavigationPanel, Action<int> goToPage, Color colorBackground)
        {
            _form = form;
            _getSapModel = getSapModel;
            _createNavigationPanel = createNavigationPanel;
            _goToPage = goToPage;
            _colorBackground = colorBackground;
        }

        public void Initialize(TabPage page)
        {
            var main = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 1 };
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            main.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));
            main.Controls.Add(Form1.CreateHeaderLabel("Perde Kesme Güvenliği Kontrolü"), 0, 0);

            var content = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(20, 10, 20, 10) };
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64));
            content.Controls.Add(BuildLeft(), 0, 0);
            content.Controls.Add(BuildRight(), 1, 0);
            main.Controls.Add(content, 0, 1);

            page.Tag = 6;
            page.VisibleChanged += (s, e) => { if (page.Visible && main.Controls.Count < 3) main.Controls.Add(_createNavigationPanel(6), 0, 2); };
            page.Controls.Add(main);
        }

        private Control BuildLeft()
        {
            var p = new RoundedPanel { Dock = DockStyle.Fill, Title = "Veri ve Parametreler", BorderRadius = 15, Margin = new Padding(0, 0, 15, 10), AutoScroll = true };
            
            p.Controls.Add(new Label { Text = "Yük Kombinasyonları", Location = new Point(15, 35), AutoSize = true, Font = new Font("Segoe UI Semibold", 9f) });
            _lstCombos = new ListBox { Location = new Point(15, 60), Size = new Size(165, 115), SelectionMode = SelectionMode.MultiExtended };
            _lstCombos.DoubleClick += (s, e) => AddSelectedCombos();
            p.Controls.Add(_lstCombos);

            var bLoad = new SmoothButton { Text = "Yükle", Size = new Size(80, 28), Location = new Point(15, 185), BaseColor = Color.FromArgb(204, 229, 255), BorderRadius = 12 };
            bLoad.Click += BtnLoad; p.Controls.Add(bLoad);
            var bAdd = new SmoothButton { Text = "Seç", Size = new Size(80, 28), Location = new Point(100, 185), BaseColor = Color.FromArgb(204, 229, 255), BorderRadius = 12 };
            bAdd.Click += (s, e) => AddSelectedCombos(); p.Controls.Add(bAdd);

            p.Controls.Add(new Label { Text = "Seçilenler", Location = new Point(190, 35), AutoSize = true, Font = new Font("Segoe UI Semibold", 9f) });
            _pnlSelectedCombos = new FlowLayoutPanel { Location = new Point(190, 60), Size = new Size(180, 150), AutoScroll = true, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White };
            p.Controls.Add(_pnlSelectedCombos);

            var pnlParams = new Panel { Location = new Point(15, 230), Size = new Size(360, 250) };
            pnlParams.Controls.Add(new Label { Text = "Hesap Parametreleri", Location = new Point(0, 0), AutoSize = true, Font = new Font("Segoe UI Semibold", 9f) });
            
            pnlParams.Controls.Add(new Label { Text = "fck (MPa):", Location = new Point(0, 25), AutoSize = true });
            _fck = new TextBox { Location = new Point(70, 23), Width = 50, Text = "30" }; pnlParams.Controls.Add(_fck);
            
            pnlParams.Controls.Add(new Label { Text = "fyd (MPa):", Location = new Point(130, 25), AutoSize = true });
            _fyd = new TextBox { Location = new Point(200, 23), Width = 50, Text = "420" }; pnlParams.Controls.Add(_fyd);

            _chkBodrum = new CheckBox { Text = "Bodrum kabulü var mı?", Location = new Point(0, 55), AutoSize = true };
            pnlParams.Controls.Add(_chkBodrum);
            pnlParams.Controls.Add(new Label { Text = "Bodrum kat sayısı:", Location = new Point(160, 56), AutoSize = true });
            _numBodrumKat = new NumericUpDown { Location = new Point(265, 54), Width = 50, Minimum = 0, Maximum = 20 };
            _chkBodrum.CheckedChanged += (s, e) => { _numBodrumKat.Enabled = _chkBodrum.Checked; };
            _numBodrumKat.Enabled = false;
            pnlParams.Controls.Add(_numBodrumKat);

            pnlParams.Controls.Add(new Label { Text = "n adet", Location = new Point(0, 85), AutoSize = true });
            _clbN = new CheckedListBox { Location = new Point(0, 105), Size = new Size(70, 70), CheckOnClick = true };
            _clbN.Items.AddRange(new object[] { 2, 3, 4, 5 });
            for(int i=0; i<3; i++) _clbN.SetItemChecked(i, true);
            pnlParams.Controls.Add(_clbN);

            pnlParams.Controls.Add(new Label { Text = "çap (mm)", Location = new Point(80, 85), AutoSize = true });
            _clbPhi = new CheckedListBox { Location = new Point(80, 105), Size = new Size(80, 100), CheckOnClick = true };
            _clbPhi.Items.AddRange(new object[] { 10, 12, 14, 16, 18, 20, 22, 25, 28, 32 });
            int[] phis = {0,1,2,3,5,6}; foreach(int i in phis) _clbPhi.SetItemChecked(i, true);
            pnlParams.Controls.Add(_clbPhi);

            pnlParams.Controls.Add(new Label { Text = "aralık (cm)", Location = new Point(170, 85), AutoSize = true });
            _clbS = new CheckedListBox { Location = new Point(170, 105), Size = new Size(80, 100), CheckOnClick = true };
            _clbS.Items.AddRange(new object[] { 10, 15, 20, 25, 30, 35, 40, 45, 50 });
            for(int i=0; i<4; i++) _clbS.SetItemChecked(i, true);
            pnlParams.Controls.Add(_clbS);

            _chk05 = new CheckBox { Location = new Point(260, 105), Text = "0.5V aktif", Checked = true, AutoSize = true };
            pnlParams.Controls.Add(_chk05);
            _chkBwRule = new CheckBox { Location = new Point(260, 130), Text = "Gövde krl", Checked = false, AutoSize = true };
            pnlParams.Controls.Add(_chkBwRule);

            p.Controls.Add(pnlParams);

            var bFetch = new SmoothButton { Text = "Verileri Çek", Size = new Size(110, 34), Location = new Point(15, 460), BaseColor = Color.FromArgb(204, 229, 255), BorderRadius = 12 };
            bFetch.Click += BtnFetch; p.Controls.Add(bFetch);
            var bCalc = new SmoothButton { Text = "Hesapla", Size = new Size(110, 34), Location = new Point(135, 460), BaseColor = Color.FromArgb(204, 229, 255), BorderRadius = 12 };
            bCalc.Click += BtnCalc; p.Controls.Add(bCalc);
            var bX = new SmoothButton { Text = "Excel Rapor", Size = new Size(100, 34), Location = new Point(255, 460), BaseColor = Color.FromArgb(235, 240, 245), BorderRadius = 12 };
            bX.Click += BtnExport; p.Controls.Add(bX);

            return p;
        }

        private Control BuildRight()
        {
            var p = new RoundedPanel { Dock = DockStyle.Fill, BorderRadius = 15, Title = "Sonuç Tablosu", Margin = new Padding(15, 0, 0, 5), TitleFont = new Font("Segoe UI Semibold", 14f) };
            _dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, RowHeadersVisible = false, AllowUserToAddRows = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            string[] cols = { "Kat", "Perde", "fck", "bw", "lw", "n", "ϕ", "s", "Vmax", "Vr", "Vd", "Kaynak", "Durum", "Donatı %", "Kesit %" }; 
            foreach (var h in cols) _dgv.Columns.Add(h, h); 
            _dgv.CellFormatting += (s, e) => { if (_dgv.Columns[e.ColumnIndex].Name == "Durum" && e.Value != null) { string v = e.Value.ToString(); if (v.Contains("O.K.") && !v.Contains("NOT")) { e.CellStyle.BackColor = Color.FromArgb(198, 239, 206); } else if (v.Contains("NOT") || v.Contains("ERR") || v.Contains("FAIL")) { e.CellStyle.BackColor = Color.FromArgb(255, 199, 206); } } };
            _lbl = new Label { Text = "Sonuç bekleniyor...", Dock = DockStyle.Bottom, Height = 30, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(163, 174, 208) };
            var ctn = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20, 45, 20, 15) }; ctn.Controls.Add(_dgv); ctn.Controls.Add(_lbl); p.Controls.Add(ctn); return p;
        }

        private void BtnLoad(object sender, EventArgs e)
        {
            var sap = _getSapModel(); if (sap == null) { ToastForm.ShowToast("ETABS bağlantısı kurun.", _form, 1800); return; }
            _lstCombos.Items.Clear();
            try {
                var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                int n = 0; string[] a = null; sap.RespCombo.GetNameList(ref n, ref a);
                if (n > 0 && a != null) foreach (var x in a) if (!string.IsNullOrWhiteSpace(x)) set.Add(x.Trim());
                n = 0; a = null; sap.LoadPatterns.GetNameList(ref n, ref a);
                if (n > 0 && a != null) foreach (var x in a) if (!string.IsNullOrWhiteSpace(x)) set.Add(x.Trim());
                foreach (var x in set.OrderBy(x => x)) _lstCombos.Items.Add(x);
            } catch { }
        }

        private void AddSelectedCombos()
        {
            foreach (var o in _lstCombos.SelectedItems)
            {
                string s = o.ToString().Trim(); if (s.Length == 0) continue;
                if (_pnlSelectedCombos.Controls.Cast<Control>().Any(c => string.Equals((c.Tag ?? "").ToString(), s, StringComparison.OrdinalIgnoreCase))) continue;
                var b = new Button { Text = s + " x", AutoSize = true, Tag = s, FlatStyle = FlatStyle.Flat, Margin = new Padding(2) };
                b.FlatAppearance.BorderSize = 0; b.BackColor = Color.FromArgb(220, 230, 240);
                b.Click += (x, y) => _pnlSelectedCombos.Controls.Remove(b);
                _pnlSelectedCombos.Controls.Add(b);
            }
        }

        private List<string> ReadCombos() => _pnlSelectedCombos.Controls.Cast<Control>().Select(x => (x.Tag ?? "").ToString()).ToList();
        private bool ParseD(string s, out double v) => double.TryParse((s ?? "").Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out v) || double.TryParse(s, out v);
        private List<int> ReadInts(CheckedListBox clb) => clb.CheckedItems.Cast<object>().Select(x => int.TryParse(Convert.ToString(x), out int v) ? v : 0).Where(x => x > 0).Distinct().OrderBy(x => x).ToList();

        private void BtnFetch(object sender, EventArgs e)
        {
            var sap = _getSapModel(); if (sap == null) { ToastForm.ShowToast("ETABS bağımlı.", _form, 1800); return; }
            var combos = ReadCombos(); if (combos.Count == 0) { ToastForm.ShowToast("Kombinasyon seçin.", _form, 1800); return; }
            
            _logic = new WallShearLogic(sap);
            _logic.LoadInitialData();

            // Rijit bodrum hesabı için kat ayırma (üst ve alt combos aynı, veya ayıklayabiliriz. Yalnızca FetchForces kullanacağız)
            bool bodrum = _chkBodrum.Checked;
            string rijitStory = "";
            if (bodrum && _numBodrumKat.Value > 0) {
                var ordered = _logic._orderedStoryNames.ToList(); // Bottom is length-1 usually, but let's assume index 0 is bottom if order is bottom-up 
                // Wait, logic orders top-down mostly. We will get the name of the story corresponding to bodrumKatSayısı
                // WallShearLogic stories are 0-indexed top-down usually.
                int bKat = (int)_numBodrumKat.Value;
                var reversed = _logic._orderedStoryNames.OrderByDescending(x => _logic._storyData[x].BottomZ).ToList();
                if (bKat > 0 && bKat <= reversed.Count) rijitStory = reversed[reversed.Count - bKat];
            }

            var eqCombos = combos.Where(c => c.ToUpper().Contains("E") || c.ToUpper().Contains("X") || c.ToUpper().Contains("Y")).ToList();
            var soilCombos = combos.Where(c => c.ToUpper().Contains("H") || c.ToUpper().Contains("S") || c.ToUpper().Contains("SOIL")).ToList();

            var resForce = _logic.FetchForces(combos, combos, bodrum, rijitStory);
            _logic.Fetch05VData(eqCombos, soilCombos, null);
            _logic.FetchShortPierData(eqCombos, soilCombos, null, null);

            _lbl.Text = $"Perde verisi çekildi: {resForce.Count} kayıt.";
            _lbl.ForeColor = Color.Green;
        }

        private void BtnCalc(object sender, EventArgs e)
        {
            if (_logic == null || _logic._pierData.Count == 0) { ToastForm.ShowToast("Önce veri çekin.", _form, 1800); return; }
            if (!ParseD(_fck.Text, out double fck) || !ParseD(_fyd.Text, out double fyd)) { ToastForm.ShowToast("fck/fyd sayısal olmalı.", _form, 1800); return; }
            var n = ReadInts(_clbN); var p = ReadInts(_clbPhi); var s = ReadInts(_clbS);
            if (n.Count==0 || p.Count==0 || s.Count==0) { ToastForm.ShowToast("Aralık seçimi eksik.", _form, 1800); return; }

            var input = new WallShearLogic.CalculationInput {
                FckLower = fck, FckUpper = fck, SecondaryFckActive = false, Fyd = fyd, Rule05Active = _chk05.Checked,
                NOpts = n, FOpts = p, SOpts = s, BwRuleActive = _chkBwRule.Checked, BwThreshold = 40, BwMinNsh = 3
            };

            _lastResults = _logic.PerformCalculation(input);
            _dgv.Rows.Clear();
            foreach (var r in _lastResults) _dgv.Rows.Add(r.Story, r.Pier, r.FckUsed.ToString("F0"), r.Bw.ToString("F0"), r.Lw.ToString("F0"), r.N, r.F, r.S, r.Vmax.ToString("F0"), r.Vr.ToString("F0"), r.VdDesign.ToString("F0"), r.CalcSource, r.StatusText, r.PurVal.ToString("F2"), r.KapVal.ToString("F2"));
        }

        private void BtnExport(object sender, EventArgs e)
        {
            if (_logic == null || _lastResults == null) { ToastForm.ShowToast("Önce hesap yapın.", _form, 1800); return; }
            ParseD(_fck.Text, out double fck); ParseD(_fyd.Text, out double fyd);
            using (var sfd = new SaveFileDialog { Filter = "Excel|.xlsx", FileName = "PerdeKesme.xlsx" }) {
                if (sfd.ShowDialog() == DialogResult.OK) {
                    _logic.ExportExcelEPPlus(sfd.FileName, fck, fyd, _lastResults);
                    ToastForm.ShowToast("Excel OK", _form, 1800);
                }
            }
        }

        public void Reset()
        {
            if (_fck != null) _fck.Text = "30"; if (_fyd != null) _fyd.Text = "420";
            if (_lstCombos != null) _lstCombos.Items.Clear();
            if (_pnlSelectedCombos != null) _pnlSelectedCombos.Controls.Clear();
            if (_dgv != null) _dgv.Rows.Clear();
            _logic = null; _lastResults = null;
        }
    }
}
