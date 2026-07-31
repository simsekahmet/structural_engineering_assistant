const moduleDefinitions = [
  { id: 'spectrum', key: 'spectrum', icon: '⌁', categoryKey: 'category.analysis', ready: true },
  { id: 'increment', key: 'increment', icon: '↟', categoryKey: 'category.analysis', ready: true },
  { id: 'drift', key: 'drift', icon: '↔', categoryKey: 'category.analysis', ready: true },
  { id: 'pdelta', key: 'pdelta', icon: 'ϑ', categoryKey: 'category.analysis', ready: true },
  { id: 'column-axial', key: 'columnAxial', icon: '▥', categoryKey: 'category.memberChecks', ready: true },
  { id: 'wall-shear', key: 'wallShear', icon: '▤', categoryKey: 'category.memberChecks', ready: true },
  { id: 'wall-axial', key: 'wallAxial', icon: '▯', categoryKey: 'category.memberChecks', ready: true },
  { id: 'beam-shear', key: 'beamShear', icon: '═', categoryKey: 'category.memberChecks', ready: true },
  { id: 'beam-axial', key: 'beamAxial', icon: '⇥', categoryKey: 'category.memberChecks', ready: true }
];

const translations = {
  en: {
    'brand.name': 'Structural Engineering Assistant', 'brand.developedBy': 'Developed by',
    'brand.subtitle': 'ETABS checks and reporting platform',
    'brand.home': 'Structural Engineering Assistant home', 'nav.aria': 'Application menu',
    'model.activeTitle': 'Active ETABS model', 'model.active': 'Active model', 'model.waiting': 'Waiting for connection',
    'action.connect': 'Connect to ETABS', 'action.clear': 'Clear', 'action.showAll': 'Show all →', 'action.showLess': 'Show less ↑',
    'action.disconnect': 'Disconnect', 'terminal.disconnected': 'Disconnected from the ETABS model.',
    'instances.title': 'Select ETABS model', 'instances.subtitle': 'More than one ETABS instance is running',
    'instances.connect': 'Connect',
    'action.viewArchitecture': 'View connection architecture', 'action.dashboard': '← Dashboard', 'action.close': 'Close', 'action.understood': 'Understood',
    'action.searching': 'Searching for bridge…', 'action.downloadAgent': 'Download Windows Agent',
    'nav.general': 'GENERAL', 'nav.analysis': 'ANALYSIS & CHECKS', 'nav.memberChecks': 'MEMBER CHECKS',
    'category.analysis': 'Analysis & Checks', 'category.memberChecks': 'Member Checks',
    'version': 'v0.2 · bilingual preview',
    'dashboard.eyebrow': 'PROJECT CENTER', 'dashboard.title': 'Structural Engineering Dashboard',
    'dashboard.description': 'Connect your ETABS model, manage engineering checks, and report results from one workspace.',
    'bridge.local': 'Local bridge', 'status.offline': 'Offline', 'status.connected': 'Connected',
    'stat.connectionRequired': 'ETABS connection required', 'stat.readyModules': 'Interface modules', 'stat.migrationDefined': 'Calculation engines not migrated yet', 'status.uiOnly': 'UI only', 'status.ready': 'Ready',
    'stat.lastCheck': 'Last check', 'stat.noCheck': 'No check has been run', 'stat.reports': 'Reports', 'stat.reportTypes': 'Excel / PDF outputs',
    'quick.title': 'Quick Start', 'quick.description': 'Select an engineering module',
    'workflow.title': 'Workflow', 'workflow.description': 'Local ETABS connection status',
    'workflow.open.title': 'Open ETABS', 'workflow.open.text': 'Load the model to be checked.',
    'workflow.agent.title': 'Run the local bridge', 'workflow.agent.text': 'The Windows agent provides COM API access.',
    'workflow.connect.title': 'Connect the web interface', 'workflow.connect.text': 'Use the connection button above.',
    'workflow.check.title': 'Run a check', 'workflow.check.text': 'Review and export the results.',
    'terminal.ready': 'Web interface ready.', 'terminal.waiting': 'Waiting for the Windows bridge to connect to ETABS.',
    'terminal.cleared': 'Terminal cleared.', 'terminal.searching': 'Searching for the local ETABS bridge at http://127.0.0.1:5218.',
    'terminal.connected': 'Connected successfully to {model}.',
    'terminal.etabsNotFound': 'The Windows agent is online, but no open ETABS model was found.',
    'terminal.notFound': 'Local bridge not found. Install and run the Windows agent, then try again.',
    'about.title': 'About the Platform', 'about.subtitle': 'Purpose, architecture, and current implementation status',
    'about.purpose.title': 'Engineering workspace', 'about.purpose.text': 'Structural Engineering Assistant brings ETABS analysis checks, member checks, results, and exports into one web interface.',
    'about.connection.title': 'Local ETABS bridge', 'about.connection.text': 'Because browsers cannot access the ETABS COM API directly, a Windows agent connects this interface to the model open on your computer. It listens only on 127.0.0.1:5218, accepts requests only from this site’s origin, and every call is written to a local log.',
    'about.status.title': 'Current version', 'about.status.text': 'The interface and ETABS connection agent are available, and engineering checks run against the active model through the local bridge.',
    'about.note.label': 'Important:', 'about.note.text': 'Engineering results must be reviewed and approved by the responsible structural engineer.',
    'moduleData.title': 'Model Data', 'moduleData.description': 'Dataset to be read from the ETABS model',
    'moduleData.waiting': 'Waiting for ETABS connection', 'moduleData.note': 'Module inputs are read from the active ETABS model through the local bridge.',
    'results.title': 'Check Results', 'results.description': 'Summary metrics and member-level results',
    'filter.all': 'All', 'filter.placeholder': 'Filter…', 'filter.clear': 'Clear filters', 'filter.noMatch': 'No rows match the current filters.',
    'combos.available': 'Available combinations', 'combos.selected': 'Combinations to check',
    'combos.add': 'Add selected', 'combos.remove': 'Remove selected',
    'combos.loading': 'Loading combinations from the model…',
    'combos.count': '{total} combinations in the model · {selected} selected',
    'combos.loadFailed': 'Combinations could not be loaded: {error}',
    'action.reset': 'Reset results', 'status.reset': 'Results cleared.',
    'calcBasis.title': 'Calculation basis', 'calcBasis.reference': 'Code reference',
    'calcBasis.note': 'Intermediate values are listed so the result can be re-derived by hand. The responsible engineer remains accountable for the check.',
    'validate.range': '{field} must be between {min} and {max}. Entered: {value}.',
    'validate.positive': '{field} must be greater than zero. Entered: {value}.',
    'validate.dOverR': 'D (overstrength factor) cannot exceed R (behaviour factor). Entered D = {d}, R = {r}. TBDY 2018 Table 4.1 always lists D ≤ R.',
    'validate.cornerPeriod': 'The corner period TB = SD1/SDS = {tb} s is outside the usual 0.1–1.5 s band. Check that SDS and SD1 belong to the same earthquake level and are in g.',
    'validate.storyHeight': 'Story height {value} m is not physically plausible. Expected 1.5–10 m — check the model unit system.',
    'validate.zeroMass': 'Total mass read from the model is zero. Run the analysis and confirm the mass source is defined.',
    'validate.zeroShear': 'Base shear read from the model is zero. Confirm the earthquake load cases were analysed.',
    'validate.fck': 'Concrete strength fck = {value} MPa is outside the 10–90 MPa range covered by TS 500. Check the unit (MPa, not kN/m²).',
    'validate.fyk': 'Reinforcement strength fyk = {value} MPa is outside the 200–700 MPa range. TS 500 S420 corresponds to 420 MPa.',
    'validate.spacing': 'Stirrup spacing {value} cm is not plausible. Expected 5–40 cm.',
    'validate.barDia': 'Bar diameter {value} mm is not plausible. Expected 6–40 mm.',
    'spectrum.basis.peakAt': 'Peak occurs at', 'spectrum.basis.peakCheck': 'Peak SaR = g·Sae/Ra',
    'drift.basis.rigid': 'rigid infill', 'drift.basis.flexible': 'flexible joint',
    'drift.basis.limitUsed': 'Limit applied', 'drift.basis.worst': 'Governing story', 'drift.basis.ofLimit': 'of limit',
    'pdelta.basis.worst': 'Governing story', 'pdelta.basis.ofLimit': 'of limit',
    'pdelta.basis.limitFormula': 'Limit = 0.12·D/(Ch·R)',
    'pdelta.basis.driftNote': 'Δi/hi is read directly from the ETABS story-drift table, so hi is already divided out.',
    'columnAxial.basis.worst': 'Governing column', 'beam.basis.worst': 'Governing beam',
    'wallShear.params.title': 'Calculation Parameters',
    'wallShear.section.combos': 'COMBINATIONS', 'wallShear.section.material': 'MATERIAL & PARAMETERS',
    'wallShear.section.rebar': 'REINFORCEMENT RULES', 'wallShear.section.short': 'SHORT WALL RULE',
    'wallShear.section.v05': '0.5V RULE', 'wallShear.section.rigid': 'RIGID BASEMENT',
    'wallShear.params.fck': 'fck (MPa)', 'wallShear.params.fyd': 'fyd (MPa)',
    'wallShear.params.secondaryFck': 'Different fck for upper stories',
    'wallShear.params.fckUpper': 'Upper fck (MPa)', 'wallShear.params.splitStory': 'From story',
    'wallShear.params.phi': 'Diameter', 'wallShear.params.spacing': 'Spacing (s)', 'wallShear.params.legs': 'Legs (n)',
    'wallShear.rebar.hint': 'Every selected diameter, spacing and leg count is tried; the lightest layout satisfying the demand and the 0.25% minimum is chosen. Reinforcement never gets lighter going down the building.',
    'wallShear.combos.hint': 'Superstructure combinations. Vd is the largest |V2| per story and wall.',
    'wallShear.short.eq': 'SHORT WALL EARTHQUAKE (EQ)', 'wallShear.short.soil': 'SHORT WALL SOIL',
    'wallShear.short.eqHint': 'Earthquake combinations amplified by the Hw/lw coefficient for short walls.',
    'wallShear.short.soilHint': 'Soil combinations added on top of the amplified earthquake shear.',
    'wallShear.short.detail': 'Detail table (short wall)',
    'wallShear.v05.active': '0.5V rule active', 'wallShear.v05.eq': '0.5V EARTHQUAKE (EQ)', 'wallShear.v05.soil': '0.5V SOIL',
    'wallShear.v05.eqHint': 'The largest earthquake shear anywhere on the wall is halved and used as a floor for Vd.',
    'wallShear.v05.soilHint': 'Soil combinations added to the halved earthquake shear.',
    'wallShear.v05.show': 'Show 0.5V values in the table', 'wallShear.v05.detail': 'Detail table (0.5V)',
    'wallShear.rigid.active': 'Rigid basement active', 'wallShear.rigid.story': 'Rigid story',
    'wallShear.rigid.note': '{story} and the stories below it are treated as rigid basement.',
    'wallShear.rigid.combos': 'BASEMENT COMBINATIONS',
    'wallShear.rigid.combosHint': 'Used instead of the superstructure combinations for the rigid basement stories.',
    'wallShear.table.pierGroup': '{pier} wall', 'wallShear.table.rebarCap': 'Rebar cap.', 'wallShear.table.sectionCap': 'Section cap.',
    'wallShear.table.coupled': 'Coupled', 'wallShear.table.coupledHint': 'Coupled wall - the Vmax coefficient drops from 0.085 to 0.065.',
    'wallShear.status.passed': 'All walls are safe in shear.', 'wallShear.status.failed': '{count} wall section(s) fail the shear check!',
    'wallShear.error.noRebarOptions': 'Select at least one diameter, one spacing and one leg count.',
    'wallShear.detail.empty': 'No detail rows - select the rule combinations and calculate first.',
    'wallShear.detail.eqCombo': 'EQ combo', 'wallShear.detail.globalEq': 'Wall max 0.5xEQ',
    'wallShear.detail.coeff': 'Coefficient', 'wallShear.detail.ampEq': 'Amplified EQ',
    'wallShear.detail.soil1': 'Soil 1', 'wallShear.detail.soil2': 'Soil 2',
    'wallShear.basis.worst': 'Governing wall', 'wallShear.basis.rebar': 'Chosen layout',
    'wallShear.basis.vdSource': 'Vd source',
    'wallShear.basis.minRebar': 'Minimum: n.PI(phi/10)^2/4 . 100/s >= 0.25.bw   (0.25%)',
    'wallShear.basis.order': 'phi and n may not decrease from one story to the story below.',
    'wallAxial.params.title': 'Calculation Parameters', 'wallAxial.table.pier': 'Pier', 'wallAxial.table.lw': 'lw (cm)',
    'wallAxial.combos.hint': 'Pier forces are read for the selected combinations; the governing (max |P|) result per story and pier is checked.',
    'wallAxial.status.passed': 'All walls are within the axial-load limit.', 'wallAxial.status.failed': '{count} wall(s) exceed the axial-load limit!',
    'wallAxial.error.noPierForces': 'No pier forces were returned. Confirm piers are assigned and the analysis has been run for the selected combinations.',
    'wallAxial.basis.worst': 'Governing pier',
    'wallAxial.selectFailing': 'Select failing walls in model',
    'wallAxial.status.selected': '{count} area object(s) selected in the model.',
    'wallAxial.basis.clause': 'wall axial load limit (0.35, versus 0.40 for columns)',
    'wallAxial.basis.lwNote': 'lw is the wall length end to end, read from PierLabel.GetSectionProperties (widthBot); bw is the thickness (thickBot).',
    'wallAxial.basis.pNote': 'Nd is the governing |P| across the selected combinations, from Results.PierForce.',
    'columnAxial.basis.signNote': 'Nd is taken as the compression (Min) value from the ETABS element-force table and used as a magnitude.',
    'beam.basis.vcOff': 'Vc contribution switched off by the user (Vcr = 0)',
    'beamAxial.basis.clause': 'members whose axial load exceeds the beam limit must be detailed as columns',
    'beamAxial.basis.rule': 'If the ratio exceeds the limit, the member is detailed as a column.',
    'beamAxial.basis.asColumn': 'DETAIL AS COLUMN',
    'module.notImplemented.title': 'Not implemented yet',
    'module.notImplemented.text': 'This module is a user-interface shell only — no calculation has been migrated for it yet, so connecting to ETABS would not produce a result. See the module status page for the current scope.',
    'module.notImplemented.button': 'Calculation not available',
    'preflight.title': 'Pre-flight check', 'preflight.subtitle': 'Conditions that decide whether a check is meaningful',
    'preflight.agent': 'Windows agent', 'preflight.versionMatch': 'Web / agent compatibility',
    'preflight.compatible': 'Compatible', 'preflight.versionMismatch': 'Mismatch — web v{web} vs agent v{agent}. Update the agent from the Releases page.',
    'preflight.etabsVersion': 'ETABS version', 'preflight.model': 'Active model',
    'preflight.lock': 'Model lock', 'preflight.locked': 'Locked', 'preflight.unlocked': 'Unlocked',
    'preflight.lockedNote': 'Normal after analysis. Reading results is unaffected.',
    'preflight.units': 'Unit system',
    'preflight.unitsWrong': 'All modules assume kN, m, C. Switch the ETABS units before running any check — results read in other units will be wrong.',
    'preflight.unitsUnknown': 'Could not be read. Confirm ETABS is set to kN, m, C before relying on results.',
    'preflight.analysis': 'Analysis results', 'preflight.analysisDone': 'All load cases finished',
    'preflight.analysisStale': 'Not all load cases have run', 'preflight.analysisNote': 'Run the analysis in ETABS, otherwise member forces may be missing or stale.',
    'preflight.unknown': 'Unknown',
    'preflight.verdict.ok': 'Environment looks suitable for running checks.',
    'preflight.verdict.blocked': 'Unit system is not kN-m — fix this before trusting any result.',
    'preflight.continue': 'Continue',
    'docs.aria': 'Documentation', 'docs.status': 'Module status', 'docs.validation': 'Validation cases',
    'docs.known': 'Known issues', 'docs.releases': 'Release notes',
    'table.member': 'Member', 'table.story': 'Story', 'table.demand': 'Demand', 'table.capacity': 'Capacity', 'table.ratio': 'Ratio', 'table.status': 'Status',
    'table.empty': 'Results will appear here after a connection is established.',
    'moduleLog.title': 'Module Log', 'moduleLog.ready': 'Module shell is ready for web migration.',
    'dialog.title': 'ETABS Web Connection', 'dialog.subtitle': 'Local bridge architecture', 'architecture.web': 'Web Interface',
    'architecture.agent': 'Windows Agent', 'nav.dashboard': 'Dashboard',
    'dialog.note': 'A browser cannot access COM objects directly. The local Windows tray agent reads the active ETABS model and returns data to the web interface as JSON. It is bound to the loopback address only and rejects requests from any other origin; the single endpoint that touches the model does nothing but select objects.',
    'module.spectrum.title': 'Design Spectrum', 'module.spectrum.description': 'Create the horizontal elastic design spectrum using TBDY 2018 parameters and transfer it to the ETABS model.',
    'module.increment.title': 'Base Shear Amplification', 'module.increment.description': 'Calculate the base shear amplification factor from modal results and analysis base shear.',
    'module.drift.title': 'Interstory Drift', 'module.drift.description': 'Calculate effective interstory drifts and compare them with TBDY 2018 limits.',
    'module.pdelta.title': 'Second-Order Effects', 'module.pdelta.description': 'Evaluate story stability coefficients and second-order amplification requirements.',
    'module.columnAxial.title': 'Column Axial Load', 'module.columnAxial.description': 'Check column axial load demands against section capacities and code limits.',
    'module.wallShear.title': 'Wall Shear', 'module.wallShear.description': 'Review wall shear demands, capacities, and governing load combinations by story.',
    'module.wallAxial.title': 'Wall Axial Load', 'module.wallAxial.description': 'Evaluate wall axial load ratios for governing combinations.',
    'module.beamShear.title': 'Beam Shear', 'module.beamShear.description': 'Check beam shear safety by member and story.',
    'module.beamAxial.title': 'Beam Axial Load', 'module.beamAxial.description': 'Filter and report axial force effects in beams.',
    'drift.params.title': 'Earthquake Parameters', 'drift.params.sdsDD2': 'SDS (DD-2)', 'drift.params.sdsDD3': 'SDS (DD-3)',
    'drift.params.sd1DD2': 'SD1 (DD-2)', 'drift.params.sd1DD3': 'SD1 (DD-3)', 'drift.params.k': 'k', 'drift.params.tp': 'Tp',
    'drift.params.flexibleJoint': 'Flexible joint present? (Yes: 0.016, No: 0.008)', 'drift.params.basement': 'Basement assumption?',
    'drift.params.basementCount': 'Number of basement stories',
    'drift.combos.title': 'Load Combinations', 'drift.combos.fetch': 'Fetch from ETABS',
    'drift.combos.hint': 'Select combinations containing direction (X/Y) and level (UST/ALT), e.g. RSXUST.',
    'drift.combos.fetched': '{count} combinations/cases found.',
    'drift.calculate': 'Calculate', 'drift.export': 'Download Excel',
    'drift.table.story': 'Story', 'drift.table.combo': 'Combination', 'drift.table.direction': 'Direction',
    'drift.table.drift': 'Drift', 'drift.table.lambdaDrift': 'λδi/hi', 'drift.table.limit': 'Limit', 'drift.table.status': 'Status',
    'drift.table.empty': 'Fetch combinations, select the ones to check, then Calculate.',
    'drift.status.pending': 'Waiting for calculation…',
    'drift.status.passed': 'Interstory drift check is satisfied.',
    'drift.status.failed': 'Interstory drift check is NOT satisfied.',
    'drift.error.notConnected': 'Connect to ETABS first.',
    'drift.error.noCombos': 'Select at least one combination.',
    'drift.error.noData': 'No story drift data was returned for the selected combinations.',
    'drift.error.fetchFailed': 'Could not reach the local ETABS bridge',
    'pdelta.params.title': 'Calculation Parameters', 'pdelta.params.ch': 'Ch', 'pdelta.params.r': 'R', 'pdelta.params.d': 'D',
    'pdelta.combos.hint': 'Select the earthquake combinations (direction X/Y, level UST/ALT), e.g. RSXUST.',
    'pdelta.table.vi': 'Vi (kN)', 'pdelta.table.wij': 'Wij (kN)', 'pdelta.table.theta': 'θ',
    'pdelta.status.passed': 'Second-order effects can be neglected.',
    'pdelta.status.failed': 'Second-order effects must be considered.',
    'spectrum.params.title': 'TBDY 2018 Parameters', 'spectrum.params.sds': 'SDS (g)', 'spectrum.params.sd1': 'SD1 (g)',
    'spectrum.params.r': 'R', 'spectrum.params.d': 'D', 'spectrum.params.i': 'I',
    'spectrum.calculate': 'Calculate', 'spectrum.download': 'Download spectrum (.txt)',
    'spectrum.chart.title': 'Design Spectrum', 'spectrum.chart.subtitle': 'Reduced horizontal elastic spectrum SaR(T)',
    'spectrum.chart.x': 'Period T (s)', 'spectrum.chart.y': 'SaR (m/s²)',
    'spectrum.summary.peak': 'Peak SaR', 'spectrum.summary.points': 'Points',
    'spectrum.status.pending': 'Enter parameters and calculate to see the spectrum.',
    'spectrum.status.done': 'Design spectrum calculated.',
    'spectrum.error.invalid': 'SDS, SD1, R and I must be greater than zero.',
    'increment.params.title': 'Calculation Parameters', 'increment.params.mt': 'Total mass Mt (ton)',
    'increment.params.hn': 'Building height Hn (m)', 'increment.params.ct': 'Ct (0.07)',
    'increment.params.tx': 'Period Tx (s)', 'increment.params.vtx': 'Modal Vt-X (kN)',
    'increment.params.ty': 'Period Ty (s)', 'increment.params.vty': 'Modal Vt-Y (kN)',
    'increment.fetch': 'Fetch', 'increment.combos.hint': 'Select combinations; the first one containing X or Y is used for each direction\'s base shear.',
    'increment.direction.x': 'X Direction', 'increment.direction.y': 'Y Direction',
    'increment.calculate': '{direction} DIRECTION — CALCULATE',
    'increment.modal.mode': 'Mode',
    'increment.result.period': '{direction} period used', 'increment.result.beta': 'Scaling factor β',
    'increment.status.pending': 'Not calculated yet.',
    'increment.status.massFetched': 'Total structural mass fetched.',
    'increment.status.periodFetchedX': 'X direction period fetched.', 'increment.status.periodFetchedY': 'Y direction period fetched.',
    'increment.status.vtFetchedX': 'X direction base shear fetched.', 'increment.status.vtFetchedY': 'Y direction base shear fetched.',
    'increment.status.calculated': '{direction} direction scaling factor calculated.',
    'increment.warning.periodCapped': 'WARNING: period ({period}s) > Tmax ({tMax}s); Tmax was used.',
    'increment.error.noSpectrum': 'Calculate the Design Spectrum first.',
    'increment.error.invalidInputs': 'Mt, period and Vt must all be greater than zero.',
    'increment.error.noModal': 'No modal data found (Case = Modal-Ust). Run analysis first.',
    'increment.error.noComboForDirection': 'No selected combination contains "{direction}".',
    'increment.error.noStoryForces': 'No Story Forces data found for {combo}.',
    'columnAxial.params.title': 'Calculation Parameters', 'columnAxial.params.fck': 'fck (N/mm²)', 'columnAxial.params.limit': 'Limit',
    'columnAxial.combos.hint': 'Select an envelope combination that reports both Max and Min (e.g. ENVE_DESG).',
    'columnAxial.frame.fetch': 'Fetch Frame Assignment', 'columnAxial.forces.fetch': 'Fetch Element Forces',
    'columnAxial.calculate': 'Calculate', 'columnAxial.export': 'Export to Excel',
    'columnAxial.selectFailing': 'Select failing columns in model',
    'columnAxial.table.column': 'Column', 'columnAxial.table.location': 'Location', 'columnAxial.table.p': 'P (kN)',
    'columnAxial.table.section': 'Section', 'columnAxial.table.b': 'b (cm)', 'columnAxial.table.d': 'd (cm)',
    'columnAxial.table.ac': 'Ac (cm²)', 'columnAxial.table.acFck': 'Ac·fck (kN)', 'columnAxial.table.ratio': 'Ratio',
    'columnAxial.failed.title': 'Columns exceeding the limit', 'columnAxial.failed.none': 'No column exceeds the limit.',
    'columnAxial.status.pending': 'Not calculated yet.',
    'columnAxial.status.passed': 'All columns satisfy the limit.',
    'columnAxial.status.failed': '{count} column(s) exceed the limit!',
    'columnAxial.status.frameFetched': '{count} column frame assignments fetched.',
    'columnAxial.status.forcesFetched': '{count} element force rows fetched.',
    'columnAxial.status.selected': '{count} failing column(s) selected in the model.',
    'columnAxial.error.noFrameData': 'Fetch Frame Assignment and Element Forces data first.',
    'beam.params.title': 'Calculation Settings', 'beam.params.fck': 'Concrete strength fck (MPa)',
    'beam.params.fyk': 'Rebar yield fyk (MPa)', 'beam.params.dprime': "Cover d' (cm)", 'beam.params.useVc': 'Include Vc (concrete shear contribution)',
    'beam.combos.hint': 'Select the design combinations to scan (governing value per beam is used).',
    'beam.table.beam': 'Beam', 'beam.table.h': 'h (cm)',
    'beam.status.allPass': 'All beams satisfy the check.', 'beam.status.selected': '{count} beam(s) selected in the model.',
    'beam.error.noData': 'No beam element-force data was returned for the selected combinations.',
    'beamShear.selectFailing': 'Select failing beams in model',
    'beamShear.table.vd': 'Vd (kN)', 'beamShear.table.legs': 'Legs (n)', 'beamShear.table.phi': 'φ (mm)',
    'beamShear.table.spacing': 's (cm)', 'beamShear.table.vr': 'Vr (kN)',
    'beamShear.status.passed': 'All beams are safe in shear.', 'beamShear.status.failed': '{count} beam(s) fail the shear check!',
    'beamAxial.params.limit': 'Limit ratio', 'beamAxial.selectFailing': 'Select column-like beams in model',
    'beamAxial.status.passed': 'All beams are within the axial-load limit.', 'beamAxial.status.failed': '{count} beam(s) must be detailed as columns!',
  },
  tr: {
    'brand.name': 'Yapısal Tasarım Asistanı', 'brand.developedBy': 'Geliştiren',
    'brand.subtitle': 'ETABS tahkik ve raporlama platformu',
    'brand.home': 'Yapısal Tasarım Asistanı ana sayfa', 'nav.aria': 'Uygulama menüsü',
    'model.activeTitle': 'Aktif ETABS modeli', 'model.active': 'Aktif model', 'model.waiting': 'Bağlantı bekleniyor',
    'action.connect': "ETABS'a Bağlan", 'action.clear': 'Temizle', 'action.showAll': 'Tümünü göster →', 'action.showLess': 'Daha az göster ↑',
    'action.disconnect': 'Bağlantıyı Kes', 'terminal.disconnected': 'ETABS model bağlantısı kesildi.',
    'instances.title': 'ETABS Modeli Seçin', 'instances.subtitle': 'Birden fazla ETABS örneği çalışıyor',
    'instances.connect': 'Bağlan',
    'action.viewArchitecture': 'Bağlantı mimarisini görüntüle', 'action.dashboard': '← Ana Sayfa', 'action.close': 'Kapat', 'action.understood': 'Anladım',
    'action.searching': 'Köprü aranıyor…', 'action.downloadAgent': 'Windows Agent’ı İndir',
    'nav.general': 'GENEL', 'nav.analysis': 'ANALİZ & KONTROL', 'nav.memberChecks': 'ELEMAN TAHKİKLERİ',
    'category.analysis': 'Analiz & Kontrol', 'category.memberChecks': 'Eleman Tahkikleri',
    'version': 'v0.2 · çift dilli önizleme',
    'dashboard.eyebrow': 'PROJE MERKEZİ', 'dashboard.title': 'Yapısal Mühendislik Paneli',
    'dashboard.description': 'ETABS modelinizi bağlayın, tahkikleri tek merkezden yönetin ve sonuçları raporlayın.',
    'bridge.local': 'Yerel köprü', 'status.offline': 'Çevrimdışı', 'status.connected': 'Bağlı',
    'stat.connectionRequired': 'ETABS bağlantısı gerekli', 'stat.readyModules': 'Arayüz modülü', 'stat.migrationDefined': 'Hesap motorları henüz taşınmadı', 'status.uiOnly': 'Yalnızca arayüz', 'status.ready': 'Hazır',
    'stat.lastCheck': 'Son kontrol', 'stat.noCheck': 'Henüz kontrol çalıştırılmadı', 'stat.reports': 'Raporlar', 'stat.reportTypes': 'Excel / PDF çıktıları',
    'quick.title': 'Hızlı Başlangıç', 'quick.description': 'Bir mühendislik modülü seçin',
    'workflow.title': 'Çalışma Akışı', 'workflow.description': 'ETABS yerel bağlantı durumu',
    'workflow.open.title': "ETABS'ı açın", 'workflow.open.text': 'Kontrol edilecek modeli yükleyin.',
    'workflow.agent.title': 'Yerel köprüyü çalıştırın', 'workflow.agent.text': 'Windows agent, COM API erişimini sağlar.',
    'workflow.connect.title': 'Web arayüzünü bağlayın', 'workflow.connect.text': 'Yukarıdaki bağlantı düğmesini kullanın.',
    'workflow.check.title': 'Tahkiki başlatın', 'workflow.check.text': 'Sonuçları tabloda inceleyip dışa aktarın.',
    'terminal.ready': 'Web arayüzü hazır.', 'terminal.waiting': 'ETABS bağlantısı için Windows yerel köprüsü bekleniyor.',
    'terminal.cleared': 'Terminal temizlendi.', 'terminal.searching': 'Yerel ETABS köprüsü http://127.0.0.1:5218 üzerinde aranıyor.',
    'terminal.connected': '{model} modeline başarıyla bağlanıldı.',
    'terminal.etabsNotFound': 'Windows agent çalışıyor ancak açık bir ETABS modeli bulunamadı.',
    'terminal.notFound': 'Yerel köprü bulunamadı. Windows agent kurulup çalıştırıldıktan sonra yeniden deneyin.',
    'about.title': 'Platform Hakkında', 'about.subtitle': 'Amaç, mimari ve güncel uygulama durumu',
    'about.purpose.title': 'Mühendislik çalışma alanı', 'about.purpose.text': 'Yapısal Tasarım Asistanı; ETABS analiz kontrollerini, eleman tahkiklerini, sonuçları ve dışa aktarımları tek bir web arayüzünde birleştirir.',
    'about.connection.title': 'Yerel ETABS köprüsü', 'about.connection.text': 'Tarayıcılar ETABS COM API’ye doğrudan erişemediği için bir Windows agent bu arayüzü bilgisayarınızda açık olan modele bağlar. Yalnızca 127.0.0.1:5218 adresini dinler, sadece bu sitenin origin’inden gelen istekleri kabul eder ve her çağrı yerel bir günlüğe yazılır.',
    'about.status.title': 'Mevcut sürüm', 'about.status.text': 'Arayüz ve ETABS bağlantı agent’ı kullanılabilir; mühendislik tahkikleri yerel köprü üzerinden aktif model üzerinde çalışır.',
    'about.note.label': 'Önemli:', 'about.note.text': 'Mühendislik sonuçları sorumlu inşaat mühendisi tarafından kontrol edilmeli ve onaylanmalıdır.',
    'moduleData.title': 'Model Verisi', 'moduleData.description': 'ETABS modelinden okunacak veri seti',
    'moduleData.waiting': 'ETABS bağlantısı bekleniyor', 'moduleData.note': 'Modül girdileri, yerel köprü üzerinden aktif ETABS modelinden okunur.',
    'results.title': 'Tahkik Sonuçları', 'results.description': 'Özet metrikler ve eleman bazlı sonuçlar',
    'filter.all': 'Tümü', 'filter.placeholder': 'Filtre…', 'filter.clear': 'Filtreleri temizle', 'filter.noMatch': 'Geçerli filtrelere uyan satır yok.',
    'combos.available': 'Mevcut kombinasyonlar', 'combos.selected': 'Tahkik edilecekler',
    'combos.add': 'Seçilenleri ekle', 'combos.remove': 'Seçilenleri çıkar',
    'combos.loading': 'Kombinasyonlar modelden yükleniyor…',
    'combos.count': 'Modelde {total} kombinasyon · {selected} seçili',
    'combos.loadFailed': 'Kombinasyonlar yüklenemedi: {error}',
    'action.reset': 'Sonuçları sıfırla', 'status.reset': 'Sonuçlar temizlendi.',
    'calcBasis.title': 'Hesap esası', 'calcBasis.reference': 'Yönetmelik dayanağı',
    'calcBasis.note': 'Sonucun elle yeniden türetilebilmesi için ara değerler listelenmiştir. Tahkikin sorumluluğu sorumlu mühendise aittir.',
    'validate.range': '{field} değeri {min} ile {max} arasında olmalıdır. Girilen: {value}.',
    'validate.positive': '{field} sıfırdan büyük olmalıdır. Girilen: {value}.',
    'validate.dOverR': 'D (dayanım fazlalığı katsayısı) R (taşıyıcı sistem davranış katsayısı) değerini aşamaz. Girilen D = {d}, R = {r}. TBDY 2018 Tablo 4.1’de daima D ≤ R’dir.',
    'validate.cornerPeriod': 'Köşe periyodu TB = SD1/SDS = {tb} s, alışılmış 0,1–1,5 s aralığının dışında. SDS ve SD1’in aynı deprem düzeyine ait ve g biriminde olduğunu kontrol edin.',
    'validate.storyHeight': 'Kat yüksekliği {value} m fiziksel olarak makul değil. Beklenen 1,5–10 m — model birim sistemini kontrol edin.',
    'validate.zeroMass': 'Modelden okunan toplam kütle sıfır. Analizi çalıştırın ve kütle kaynağının tanımlı olduğunu doğrulayın.',
    'validate.zeroShear': 'Modelden okunan taban kesme kuvveti sıfır. Deprem yükleme durumlarının analiz edildiğini doğrulayın.',
    'validate.fck': 'Beton dayanımı fck = {value} MPa, TS 500 kapsamındaki 10–90 MPa aralığının dışında. Birimi kontrol edin (MPa, kN/m² değil).',
    'validate.fyk': 'Donatı dayanımı fyk = {value} MPa, 200–700 MPa aralığının dışında. TS 500 S420 için 420 MPa’dır.',
    'validate.spacing': 'Etriye aralığı {value} cm makul değil. Beklenen 5–40 cm.',
    'validate.barDia': 'Donatı çapı {value} mm makul değil. Beklenen 6–40 mm.',
    'spectrum.basis.peakAt': 'Tepe noktası periyodu', 'spectrum.basis.peakCheck': 'Tepe SaR = g·Sae/Ra',
    'drift.basis.rigid': 'rijit dolgu', 'drift.basis.flexible': 'esnek derz',
    'drift.basis.limitUsed': 'Uygulanan sınır', 'drift.basis.worst': 'Belirleyici kat', 'drift.basis.ofLimit': 'sınırın',
    'pdelta.basis.worst': 'Belirleyici kat', 'pdelta.basis.ofLimit': 'sınırın',
    'pdelta.basis.limitFormula': 'Sınır = 0,12·D/(Ch·R)',
    'pdelta.basis.driftNote': 'Δi/hi doğrudan ETABS kat ötelemesi tablosundan okunur; hi zaten bölünmüş durumdadır.',
    'columnAxial.basis.worst': 'Belirleyici kolon', 'beam.basis.worst': 'Belirleyici kiriş',
    'wallShear.params.title': 'Hesap Parametreleri',
    'wallShear.section.combos': 'KOMBİNASYONLAR', 'wallShear.section.material': 'MALZEME & PARAMETRELER',
    'wallShear.section.rebar': 'DONATI KURALLARI', 'wallShear.section.short': 'BODUR PERDE KURALI',
    'wallShear.section.v05': '0.5V KURALI', 'wallShear.section.rigid': 'RİJİT BODRUM',
    'wallShear.params.fck': 'FCK (MPa)', 'wallShear.params.fyd': 'FYD (MPa)',
    'wallShear.params.secondaryFck': 'Üst katlar için farklı fck',
    'wallShear.params.fckUpper': 'Üst kat FCK (MPa)', 'wallShear.params.splitStory': 'Başlangıç katı',
    'wallShear.params.phi': 'Çap', 'wallShear.params.spacing': 'Aralık (s)', 'wallShear.params.legs': 'Kol (n)',
    'wallShear.rebar.hint': 'Seçilen her çap, aralık ve kol adedi denenir; talebi ve %0,25 minimum şartını sağlayan en hafif düzen seçilir. Aşağı inildikçe donatı hafifleyemez.',
    'wallShear.combos.hint': 'Üst yapı kombinasyonları. Vd, her kat ve perde için en büyük |V2| değeridir.',
    'wallShear.short.eq': 'BODUR PERDE DEPREM (EQ)', 'wallShear.short.soil': 'BODUR PERDE TOPRAK (SOİL)',
    'wallShear.short.eqHint': 'Bodur perdelerde Hw/lw katsayısıyla büyütülen deprem kombinasyonları.',
    'wallShear.short.soilHint': 'Büyütülmüş deprem kesmesine eklenen toprak kombinasyonları.',
    'wallShear.short.detail': 'Detay tablosu (bodur)',
    'wallShear.v05.active': '0.5V kuralı aktif', 'wallShear.v05.eq': '0.5V DEPREM (EQ)', 'wallShear.v05.soil': '0.5V TOPRAK (SOİL)',
    'wallShear.v05.eqHint': 'Perdenin herhangi bir katındaki en büyük deprem kesmesi yarıya indirilip Vd için alt sınır olarak kullanılır.',
    'wallShear.v05.soilHint': 'Yarılanmış deprem kesmesine eklenen toprak kombinasyonları.',
    'wallShear.v05.show': '0.5V değerlerini tabloda göster.', 'wallShear.v05.detail': 'Detay tablosu (0.5V)',
    'wallShear.rigid.active': 'Rijit bodrum aktif', 'wallShear.rigid.story': 'Rijit kat seçimi',
    'wallShear.rigid.note': '{story} ve aşağısındaki katlar Rijit Bodrum kabulüne dahildir.',
    'wallShear.rigid.combos': 'BODRUM KOMBİNASYONLARI',
    'wallShear.rigid.combosHint': 'Rijit bodrum katlarında üst yapı kombinasyonları yerine bunlar kullanılır.',
    'wallShear.table.pierGroup': '{pier} Perdesi', 'wallShear.table.rebarCap': 'Donatı Kap.', 'wallShear.table.sectionCap': 'Kesit Kap.',
    'wallShear.table.coupled': 'Bağl. kirişli', 'wallShear.table.coupledHint': 'Bağlantı kirişli perde - Vmax katsayısı 0,085 yerine 0,065 olur.',
    'wallShear.status.passed': 'Tüm perdeler kesme güvenliğini sağlıyor.', 'wallShear.status.failed': '{count} perde kesiti kesme tahkikini sağlamıyor!',
    'wallShear.error.noRebarOptions': 'En az bir çap, bir aralık ve bir kol adedi seçin.',
    'wallShear.detail.empty': 'Detay satırı yok - önce kuralın kombinasyonlarını seçip hesaplayın.',
    'wallShear.detail.eqCombo': 'EQ kombinasyonu', 'wallShear.detail.globalEq': 'Perde maks 0,5xEQ',
    'wallShear.detail.coeff': 'Katsayı', 'wallShear.detail.ampEq': 'Büyütülmüş EQ',
    'wallShear.detail.soil1': 'Toprak 1', 'wallShear.detail.soil2': 'Toprak 2',
    'wallShear.basis.worst': 'Belirleyici perde', 'wallShear.basis.rebar': 'Seçilen düzen',
    'wallShear.basis.vdSource': 'Vd kaynağı',
    'wallShear.basis.minRebar': 'Minimum: n·π(φ/10)²/4 · 100/s ≥ 0,25·bw   (%0,25)',
    'wallShear.basis.order': 'φ ve n, bir üst kattan alt kata azalamaz.',
    'wallAxial.params.title': 'Hesap Parametreleri', 'wallAxial.table.pier': 'Perde (Pier)', 'wallAxial.table.lw': 'lw (cm)',
    'wallAxial.combos.hint': 'Perde kuvvetleri seçilen kombinasyonlar için okunur; her kat ve perde için belirleyici (maks |P|) sonuç tahkik edilir.',
    'wallAxial.status.passed': 'Tüm perdeler eksenel yük sınırında.', 'wallAxial.status.failed': '{count} perde eksenel yük sınırını aşıyor!',
    'wallAxial.error.noPierForces': 'Perde kuvveti dönmedi. Perde (pier) atamalarının yapıldığını ve seçilen kombinasyonlar için analizin çalıştırıldığını doğrulayın.',
    'wallAxial.basis.worst': 'Belirleyici perde',
    'wallAxial.selectFailing': 'Yetersiz Perdeleri Modelde Seç',
    'wallAxial.status.selected': '{count} alan nesnesi modelde seçildi.',
    'wallAxial.basis.clause': 'perde eksenel yük sınırı (kolonlarda 0,40 iken perdede 0,35)',
    'wallAxial.basis.lwNote': 'lw, perdenin iki ucu arasındaki uzunluktur; PierLabel.GetSectionProperties (widthBot) ile okunur. bw ise kalınlıktır (thickBot).',
    'wallAxial.basis.pNote': 'Nd, seçilen kombinasyonlar içindeki belirleyici |P| değeridir (Results.PierForce).',
    'columnAxial.basis.signNote': 'Nd, ETABS eleman kuvvetleri tablosundan basınç (Min) değeri olarak alınır ve mutlak değeriyle kullanılır.',
    'beam.basis.vcOff': 'Vc katkısı kullanıcı tarafından kapatıldı (Vcr = 0)',
    'beamAxial.basis.clause': 'eksenel yükü kiriş sınırını aşan elemanlar kolon gibi donatılmalıdır',
    'beamAxial.basis.rule': 'Oran sınırı aşarsa eleman kolon gibi donatılır.',
    'beamAxial.basis.asColumn': 'KOLON GİBİ DONATILACAK',
    'module.notImplemented.title': 'Henüz uygulanmadı',
    'module.notImplemented.text': 'Bu modül yalnızca arayüz iskeletidir — hesabı henüz aktarılmadığından ETABS’e bağlanmak bir sonuç üretmez. Güncel kapsam için modül durumu sayfasına bakın.',
    'module.notImplemented.button': 'Hesap mevcut değil',
    'preflight.title': 'Ön kontrol', 'preflight.subtitle': 'Tahkikin anlamlı olup olmadığını belirleyen koşullar',
    'preflight.agent': 'Windows agent', 'preflight.versionMatch': 'Web / agent uyumu',
    'preflight.compatible': 'Uyumlu', 'preflight.versionMismatch': 'Uyumsuz — web v{web}, agent v{agent}. Agent’ı Releases sayfasından güncelleyin.',
    'preflight.etabsVersion': 'ETABS sürümü', 'preflight.model': 'Aktif model',
    'preflight.lock': 'Model kilidi', 'preflight.locked': 'Kilitli', 'preflight.unlocked': 'Kilitli değil',
    'preflight.lockedNote': 'Analiz sonrası normaldir. Sonuç okumayı etkilemez.',
    'preflight.units': 'Birim sistemi',
    'preflight.unitsWrong': 'Tüm modüller kN, m, C varsayar. Herhangi bir tahkik çalıştırmadan önce ETABS birimlerini değiştirin — başka birimde okunan sonuçlar hatalı olur.',
    'preflight.unitsUnknown': 'Okunamadı. Sonuçlara güvenmeden önce ETABS’in kN, m, C olduğunu doğrulayın.',
    'preflight.analysis': 'Analiz sonuçları', 'preflight.analysisDone': 'Tüm yükleme durumları tamamlandı',
    'preflight.analysisStale': 'Tüm yükleme durumları çalıştırılmamış', 'preflight.analysisNote': 'ETABS’te analizi çalıştırın; aksi halde eleman kuvvetleri eksik veya güncel olmayabilir.',
    'preflight.unknown': 'Bilinmiyor',
    'preflight.verdict.ok': 'Ortam tahkik çalıştırmaya uygun görünüyor.',
    'preflight.verdict.blocked': 'Birim sistemi kN-m değil — sonuçlara güvenmeden önce düzeltin.',
    'preflight.continue': 'Devam et',
    'docs.aria': 'Belgeler', 'docs.status': 'Modül durumu', 'docs.validation': 'Doğrulama örnekleri',
    'docs.known': 'Bilinen sorunlar', 'docs.releases': 'Sürüm notları',
    'table.member': 'Eleman', 'table.story': 'Kat', 'table.demand': 'Talep', 'table.capacity': 'Kapasite', 'table.ratio': 'Oran', 'table.status': 'Durum',
    'table.empty': 'Bağlantı kurulduktan sonra sonuçlar burada görüntülenecek.',
    'moduleLog.title': 'Modül Günlüğü', 'moduleLog.ready': 'Modül web uyarlaması için hazırlandı.',
    'dialog.title': 'ETABS Web Bağlantısı', 'dialog.subtitle': 'Yerel köprü mimarisi', 'architecture.web': 'Web Arayüzü',
    'architecture.agent': 'Windows Agent', 'nav.dashboard': 'Ana Sayfa',
    'dialog.note': "Tarayıcı COM nesnelerine doğrudan erişemez. Yerel Windows tray agent aktif ETABS modelini okur ve verileri JSON olarak web arayüzüne döndürür. Yalnızca loopback adresine bağlıdır ve başka origin’den gelen istekleri reddeder; modele dokunan tek uç yalnızca eleman seçimi yapar.",
    'module.spectrum.title': 'Tasarım Spektrumu', 'module.spectrum.description': 'TBDY 2018 parametreleriyle yatay elastik tasarım spektrumunu oluşturun ve ETABS modeline aktarın.',
    'module.increment.title': 'Taban Kesme Kuvveti Büyütmesi', 'module.increment.description': 'Modal sonuçlar ve analiz taban kesme kuvveti üzerinden büyütme katsayısını hesaplayın.',
    'module.drift.title': 'Göreli Kat Ötelemesi', 'module.drift.description': 'Etkin göreli kat ötelemelerini hesaplayın ve TBDY 2018 sınırlarıyla karşılaştırın.',
    'module.pdelta.title': 'İkinci Mertebe Etkileri', 'module.pdelta.description': 'Kat stabilite katsayılarını ve ikinci mertebe büyütme gereksinimini değerlendirin.',
    'module.columnAxial.title': 'Kolon Eksenel Yük', 'module.columnAxial.description': 'Kolon eksenel yük taleplerini kesit kapasiteleri ve yönetmelik sınırlarıyla tahkik edin.',
    'module.wallShear.title': 'Perde Kesme', 'module.wallShear.description': 'Perde kesme taleplerini, kapasitelerini ve kritik yük birleşimlerini kat bazında inceleyin.',
    'module.wallAxial.title': 'Perde Eksenel Yük', 'module.wallAxial.description': 'Perde eksenel yük oranlarını kritik kombinasyonlar için değerlendirin.',
    'module.beamShear.title': 'Kiriş Kesme', 'module.beamShear.description': 'Kiriş kesme güvenliğini eleman ve kat bazında tahkik edin.',
    'module.beamAxial.title': 'Kiriş Eksenel Yük', 'module.beamAxial.description': 'Kirişlerdeki eksenel kuvvet etkilerini filtreleyin ve raporlayın.',
    'drift.params.title': 'Deprem Parametreleri', 'drift.params.sdsDD2': 'SDS (DD-2)', 'drift.params.sdsDD3': 'SDS (DD-3)',
    'drift.params.sd1DD2': 'SD1 (DD-2)', 'drift.params.sd1DD3': 'SD1 (DD-3)', 'drift.params.k': 'k', 'drift.params.tp': 'Tp',
    'drift.params.flexibleJoint': 'Esnek derz var mı? (Var: 0.016, Yok: 0.008)', 'drift.params.basement': 'Bodrum kabulü var mı?',
    'drift.params.basementCount': 'Bodrum kat sayısı',
    'drift.combos.title': 'Yük Kombinasyonları', 'drift.combos.fetch': "ETABS'tan Getir",
    'drift.combos.hint': 'Yön (X/Y) ve seviye (ÜST/ALT) içeren kombinasyonları seçin, örn. RSXUST.',
    'drift.combos.fetched': '{count} kombinasyon/case bulundu.',
    'drift.calculate': 'Hesapla', 'drift.export': 'Excel İndir',
    'drift.table.story': 'Kat', 'drift.table.combo': 'Kombinasyon', 'drift.table.direction': 'Yön',
    'drift.table.drift': 'Drift', 'drift.table.lambdaDrift': 'λδi/hi', 'drift.table.limit': 'Limit', 'drift.table.status': 'Durum',
    'drift.table.empty': 'Kombinasyonları getirin, tahkik edilecekleri seçin, ardından Hesaplayın.',
    'drift.status.pending': 'Hesaplama bekleniyor…',
    'drift.status.passed': 'Göreli kat ötelemesi tahkiki sağlanmıştır.',
    'drift.status.failed': 'Göreli kat ötelemesi tahkiki sağlanmamıştır.',
    'drift.error.notConnected': "Önce ETABS'a bağlanın.",
    'drift.error.noCombos': 'En az bir kombinasyon seçin.',
    'drift.error.noData': 'Seçili kombinasyonlar için story drift verisi bulunamadı.',
    'drift.error.fetchFailed': 'Yerel ETABS köprüsüne ulaşılamadı',
    'pdelta.params.title': 'Hesap Parametreleri', 'pdelta.params.ch': 'Ch', 'pdelta.params.r': 'R', 'pdelta.params.d': 'D',
    'pdelta.combos.hint': 'Deprem kombinasyonlarını seçin (yön X/Y, seviye ÜST/ALT), örn. RSXUST.',
    'pdelta.table.vi': 'Vi (kN)', 'pdelta.table.wij': 'Wij (kN)', 'pdelta.table.theta': 'θ',
    'pdelta.status.passed': 'İkinci mertebe etkileri göz ardı edilebilir.',
    'pdelta.status.failed': 'İkinci mertebe etkileri hesaba katılmalı.',
    'spectrum.params.title': 'TBDY 2018 Parametreleri', 'spectrum.params.sds': 'SDS (g)', 'spectrum.params.sd1': 'SD1 (g)',
    'spectrum.params.r': 'R', 'spectrum.params.d': 'D', 'spectrum.params.i': 'I',
    'spectrum.calculate': 'Hesapla', 'spectrum.download': 'Spektrumu indir (.txt)',
    'spectrum.chart.title': 'Tasarım Spektrumu', 'spectrum.chart.subtitle': 'Azaltılmış yatay elastik spektrum SaR(T)',
    'spectrum.chart.x': 'Periyot T (s)', 'spectrum.chart.y': 'SaR (m/s²)',
    'spectrum.summary.peak': 'Tepe SaR', 'spectrum.summary.points': 'Nokta',
    'spectrum.status.pending': 'Parametreleri girip hesaplayın; spektrum burada görünecek.',
    'spectrum.status.done': 'Tasarım spektrumu hesaplandı.',
    'spectrum.error.invalid': 'SDS, SD1, R ve I sıfırdan büyük olmalıdır.',
    'increment.params.title': 'Hesap Parametreleri', 'increment.params.mt': 'Yapı Toplam Kütlesi Mt (ton)',
    'increment.params.hn': 'Bina Yüksekliği Hn (m)', 'increment.params.ct': 'Ct (0.07)',
    'increment.params.tx': 'Periyot Tx (s)', 'increment.params.vtx': 'Modal Vt-X (kN)',
    'increment.params.ty': 'Periyot Ty (s)', 'increment.params.vty': 'Modal Vt-Y (kN)',
    'increment.fetch': 'Getir', 'increment.combos.hint': 'Kombinasyonları seçin; her yön için adında X veya Y geçen ilk kombinasyon taban kesme kuvveti için kullanılır.',
    'increment.direction.x': 'X Yönü', 'increment.direction.y': 'Y Yönü',
    'increment.calculate': '{direction} YÖNÜ HESAPLA',
    'increment.modal.mode': 'Mod',
    'increment.result.period': 'Kullanılan {direction} periyodu', 'increment.result.beta': 'Büyütme Katsayısı β',
    'increment.status.pending': 'Henüz hesaplanmadı.',
    'increment.status.massFetched': 'Yapı toplam kütlesi çekildi.',
    'increment.status.periodFetchedX': 'X yönü periyot değeri çekildi.', 'increment.status.periodFetchedY': 'Y yönü periyot değeri çekildi.',
    'increment.status.vtFetchedX': 'X yönü taban kesme kuvveti çekildi.', 'increment.status.vtFetchedY': 'Y yönü taban kesme kuvveti çekildi.',
    'increment.status.calculated': '{direction} yönü artırım katsayısı hesaplandı.',
    'increment.warning.periodCapped': 'UYARI: periyot ({period}s) > Tmax ({tMax}s); hesapta Tmax kullanıldı.',
    'increment.error.noSpectrum': 'Önce Tasarım Spektrumu sayfasından spektrum hesaplayınız.',
    'increment.error.invalidInputs': 'Mt, periyot ve Vt değerleri sıfırdan büyük olmalıdır.',
    'increment.error.noModal': 'Modal veri bulunamadı (Case = Modal-Ust). Önce analiz çalıştırın.',
    'increment.error.noComboForDirection': 'Seçili kombinasyonlar arasında "{direction}" içeren yok.',
    'increment.error.noStoryForces': '{combo} için Story Forces verisi bulunamadı.',
    'columnAxial.params.title': 'Hesap Parametreleri', 'columnAxial.params.fck': 'fck (N/mm²)', 'columnAxial.params.limit': 'Limit',
    'columnAxial.combos.hint': 'Hem Max hem Min raporlayan bir zarf (envelope) kombinasyon seçin (örn. ENVE_DESG).',
    'columnAxial.frame.fetch': 'Frame Assignment Getir', 'columnAxial.forces.fetch': 'Element Forces Getir',
    'columnAxial.calculate': 'Hesapla', 'columnAxial.export': "Excel'e Aktar",
    'columnAxial.selectFailing': 'Sınırı Aşan Kolonları Modelde Seç',
    'columnAxial.table.column': 'Kolon', 'columnAxial.table.location': 'Konum', 'columnAxial.table.p': 'P (kN)',
    'columnAxial.table.section': 'Kesit', 'columnAxial.table.b': 'b (cm)', 'columnAxial.table.d': 'd (cm)',
    'columnAxial.table.ac': 'Ac (cm²)', 'columnAxial.table.acFck': 'Ac·fck (kN)', 'columnAxial.table.ratio': 'Oran',
    'columnAxial.failed.title': 'Sınırı Aşan Kolonlar', 'columnAxial.failed.none': 'Sınırı aşan kolon yok.',
    'columnAxial.status.pending': 'Henüz hesaplanmadı.',
    'columnAxial.status.passed': 'Tüm kolonlar limiti sağlıyor.',
    'columnAxial.status.failed': '{count} adet kolon limiti aşıyor!',
    'columnAxial.status.frameFetched': '{count} kolon frame assignment verisi çekildi.',
    'columnAxial.status.forcesFetched': '{count} element force satırı çekildi.',
    'columnAxial.status.selected': '{count} adet sınırı aşan kolon modelde seçildi.',
    'columnAxial.error.noFrameData': 'Önce Frame Assignment ve Element Forces verilerini "Getir" ile çekiniz.',
    'beam.params.title': 'Hesap Ayarları', 'beam.params.fck': 'Beton Dayanımı fck (MPa)',
    'beam.params.fyk': 'Donatı Akma fyk (MPa)', 'beam.params.dprime': "Paspayı d' (cm)", 'beam.params.useVc': 'Vc (Beton Kesme Katkısı) Kullanılsın',
    'beam.combos.hint': 'Taranacak tasarım kombinasyonlarını seçin (her kiriş için kritik değer kullanılır).',
    'beam.table.beam': 'Kiriş', 'beam.table.h': 'h (cm)',
    'beam.status.allPass': 'Tüm kirişler kontrolü sağlıyor.', 'beam.status.selected': '{count} kiriş modelde seçildi.',
    'beam.error.noData': 'Seçili kombinasyonlar için kiriş element force verisi bulunamadı.',
    'beamShear.selectFailing': 'Kurtarmayan Kirişleri Modelde Seç',
    'beamShear.table.vd': 'Vd (kN)', 'beamShear.table.legs': 'Etriye (kol)', 'beamShear.table.phi': 'Çap φ (mm)',
    'beamShear.table.spacing': 'Aralık s (cm)', 'beamShear.table.vr': 'Vr (kN)',
    'beamShear.status.passed': 'Tüm kirişler kesme güvenliğini sağlıyor.', 'beamShear.status.failed': '{count} kiriş kesme güvenliğini sağlamıyor!',
    'beamAxial.params.limit': 'Sınır oran', 'beamAxial.selectFailing': 'Kolon Gibi Donatılacakları Modelde Seç',
    'beamAxial.status.passed': 'Tüm kirişler eksenel yük sınırında.', 'beamAxial.status.failed': '{count} kiriş kolon gibi donatılmalı!',
  }
};

const $ = (selector, root = document) => root.querySelector(selector);
const $$ = (selector, root = document) => [...root.querySelectorAll(selector)];
const moduleGrid = $('#moduleGrid');
const dashboard = $('#dashboard');
const moduleView = $('#moduleView');
const toastStack = $('#toastStack');
let currentLanguage = localStorage.getItem('sea-language') === 'tr' ? 'tr' : 'en';

const AGENT_BASE = 'http://127.0.0.1:5218';
const defaultSetupPanelHtml = $('#setupPanel').innerHTML;
const defaultResultsPanelHtml = $('#resultsPanel').innerHTML;

// Module id -> renderer. Populated with function declarations (hoisted), used by setActiveView.
const moduleRenderers = {
  spectrum: renderSpectrumModule,
  increment: renderIncrementModule,
  drift: renderDriftModule,
  pdelta: renderPdeltaModule,
  'column-axial': renderColumnAxialModule,
  'beam-shear': renderBeamShearModule,
  'beam-axial': renderBeamAxialModule,
  'wall-axial': renderWallAxialModule,
  'wall-shear': renderWallShearModule
};

// Shared across beam checks: unique frame name -> { section, h, b } (h/b in model length units).
async function fetchFrameSectionMap() {
  const res = await fetchAgentJson('/api/etabs/frame-sections', 20000);
  if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
  const map = new Map();
  for (const s of res.sections || []) map.set(s.unique, s);
  return map;
}

// Find a column index in an ETABS display table by normalized header name (case/space/dot-insensitive).
function tableIndex(fields, ...names) {
  const norm = s => String(s).toUpperCase().replace(/[\s.]/g, '');
  const wanted = names.map(norm);
  return fields.findIndex(f => wanted.includes(norm(f)));
}

function t(key, values = {}) {
  const value = translations[currentLanguage][key] ?? translations.en[key] ?? key;
  return Object.entries(values).reduce((text, [name, replacement]) => text.replaceAll(`{${name}}`, replacement), value);
}

function renderModules() {
  const expanded = moduleGrid.classList.contains('expanded');
  moduleGrid.innerHTML = moduleDefinitions.map((module, index) => `
    <button class="module-card ${index >= 6 ? 'extra' : ''} ${module.ready ? 'ready' : ''}" type="button" data-module="${module.id}">
      <span class="module-icon">${module.icon}</span>
      <strong>${t(`module.${module.key}.title`)}</strong>
      <small>${t(module.categoryKey)} · ${t(module.ready ? 'status.ready' : 'status.uiOnly')}</small>
    </button>`).join('');
  moduleGrid.classList.toggle('expanded', expanded);
}

// Top-right toast notifications. Kept the name log() so existing call sites are unchanged.
function log(message, type = 'info') {
  if (!toastStack) return;
  const toast = document.createElement('div');
  toast.className = `toast ${type}`;
  const symbol = type === 'ok' ? '✓' : type === 'error' ? '!' : 'i';
  toast.innerHTML = `<span class="toast-icon">${symbol}</span><p></p>`;
  toast.querySelector('p').textContent = message;
  toast.addEventListener('click', () => dismissToast(toast));
  toastStack.appendChild(toast);
  requestAnimationFrame(() => toast.classList.add('show'));
  setTimeout(() => dismissToast(toast), type === 'error' ? 6500 : 4000);
}

function dismissToast(toast) {
  if (!toast.isConnected) return;
  toast.classList.remove('show');
  setTimeout(() => toast.remove(), 350);
}

// Dashboard "Last check" stat card.
// Accepts a module id (e.g. "column-axial"); resolves it to the moduleDefinitions translation
// key (e.g. "columnAxial") since the two only coincide for the simple analysis modules.
function recordLastCheck(moduleId) {
  const module = moduleDefinitions.find(m => m.id === moduleId);
  const key = module ? module.key : moduleId;
  localStorage.setItem('sea-last-check', JSON.stringify({ key, at: Date.now() }));
  renderLastCheck();
}

function renderLastCheck() {
  const stat = $('#lastCheckStat');
  const sub = $('#lastCheckSub');
  const raw = localStorage.getItem('sea-last-check');
  if (!stat || !sub || !raw) return;
  try {
    const { key, at } = JSON.parse(raw);
    stat.textContent = t(`module.${key}.title`);
    sub.removeAttribute('data-i18n');
    sub.textContent = new Intl.DateTimeFormat(currentLanguage === 'tr' ? 'tr-TR' : 'en-GB',
      { day: '2-digit', month: '2-digit', hour: '2-digit', minute: '2-digit' }).format(new Date(at));
  } catch { /* ignore malformed record */ }
}

function updateShowAllLabel() {
  $('#showAllModules').textContent = t(moduleGrid.classList.contains('expanded') ? 'action.showLess' : 'action.showAll');
}

function applyTranslationsToDom(root = document) {
  $$('[data-i18n]', root).forEach(element => { element.textContent = t(element.dataset.i18n); });
  $$('[data-i18n-title]', root).forEach(element => { element.title = t(element.dataset.i18nTitle); });
  $$('[data-i18n-aria-label]', root).forEach(element => { element.setAttribute('aria-label', t(element.dataset.i18nAriaLabel)); });
}

function applyLanguage(language) {
  currentLanguage = language === 'tr' ? 'tr' : 'en';
  localStorage.setItem('sea-language', currentLanguage);
  document.documentElement.lang = currentLanguage;
  applyTranslationsToDom();
  document.title = t('brand.name');
  // Version badge is driven by version.js so it can never drift from the source of truth.
  const badge = $('#versionBadge');
  if (badge) badge.textContent = `v${APP_VERSION}`;
  $('.brand').setAttribute('aria-label', t('brand.home'));
  $('#themeToggle').setAttribute('aria-label', currentLanguage === 'tr' ? 'Açık / koyu tema değiştir' : 'Switch light / dark mode');
  $('#languageToggle').setAttribute('aria-label', currentLanguage === 'tr' ? 'İngilizce / Türkçe değiştir' : 'Switch English / Turkish');
  $('#languageToggle').setAttribute('aria-pressed', String(currentLanguage === 'tr'));
  renderModules();
  updateShowAllLabel();
  renderLastCheck();
  setActiveView(location.hash.slice(1) || 'dashboard');
}

function setActiveView(id) {
  if (!id || id === 'dashboard') {
    dashboard.classList.add('active');
    moduleView.classList.remove('active');
    $$('.nav-item').forEach(item => item.classList.toggle('active', item.dataset.view === 'dashboard'));
    return;
  }

  const module = moduleDefinitions.find(item => item.id === id);
  if (!module) return setActiveView('dashboard');
  $('#moduleTitle').textContent = t(`module.${module.key}.title`);
  // Display-only casing: Turkish needs the locale-aware mapping (i→İ, not i→I).
  // Never use this on strings matched against ETABS data — see the toUpperCase()
  // calls in the combo/direction filters, which must stay locale-invariant.
  $('#moduleCategory').textContent = currentLanguage === 'tr'
    ? t(module.categoryKey).toLocaleUpperCase('tr-TR')
    : t(module.categoryKey).toUpperCase();
  $('#moduleDescription').textContent = t(`module.${module.key}.description`);
  dashboard.classList.remove('active');
  moduleView.classList.add('active');
  $$('.nav-item').forEach(item => item.classList.toggle('active', item.dataset.view === id));
  window.scrollTo({ top: 0, behavior: 'smooth' });

  const renderer = moduleRenderers[id];
  if (renderer) {
    renderer();
  } else {
    // UI-only shell: no calculation is implemented yet, so the connect button would
    // promise something the module cannot deliver. Disable it and say so plainly
    // instead of leaving a live control that leads nowhere.
    $('#setupPanel').innerHTML = defaultSetupPanelHtml;
    $('#resultsPanel').innerHTML = defaultResultsPanelHtml;
    applyTranslationsToDom($('#setupPanel'));
    applyTranslationsToDom($('#resultsPanel'));
    const connectBtn = $('[data-connect]', $('#setupPanel'));
    if (connectBtn) {
      connectBtn.disabled = true;
      connectBtn.textContent = t('module.notImplemented.button');
      connectBtn.setAttribute('aria-disabled', 'true');
    }
    const waiting = $('.empty-state h3', $('#setupPanel'));
    if (waiting) waiting.textContent = t('module.notImplemented.title');
    const note = $('.empty-state p', $('#setupPanel'));
    if (note) note.textContent = t('module.notImplemented.text');
  }
}

function setConnectButtonsLoading(loading) {
  const buttons = [$('#connectButton'), ...$$('[data-connect]')];
  buttons.forEach(button => {
    button.disabled = loading;
    const label = $('[data-i18n="action.connect"]', button) || button;
    label.textContent = loading ? t('action.searching') : t('action.connect');
  });
}

async function connectToEtabs() {
  setConnectButtonsLoading(true);
  log(t('terminal.searching'));

  try {
    // Ask the agent what is actually running. With more than one ETABS open the user
    // picks; with exactly one we connect straight away.
    const list = await fetchAgentJson('/api/etabs/instances', 8000);
    const instances = list.instances || [];

    if (instances.length > 1) {
      setConnectButtonsLoading(false);
      showInstancePicker(instances);
      return;
    }

    const data = instances.length === 1
      ? await postAgentJson('/api/etabs/connect-to', { instanceId: instances[0].id }, 15000)
      : await fetchAgentJson('/api/health', 8000);

    applyConnectionState(data);
  } catch (error) {
    $('#architectureDialog').showModal();
    log(t('terminal.notFound'), 'error');
  } finally {
    setConnectButtonsLoading(false);
  }
}

// Paints the header/dashboard from a snapshot and opens the pre-flight panel.
function applyConnectionState(data) {
  if (!data || !data.etabsConnected) {
    log(t('terminal.etabsNotFound'), 'error');
    return;
  }
  const model = data.modelName || data.model || 'ETABS model';
  $('#connectionDot').classList.add('connected');
  $('#modelName').removeAttribute('data-i18n');
  $('#modelName').textContent = model;
  $('#activeModelStat').textContent = model;
  $('#bridgeStatus').removeAttribute('data-i18n');
  $('#bridgeStatus').textContent = t('status.connected');
  $('#bridgeStatus').classList.add('connected');
  setDisconnectVisible(true);
  log(t('terminal.connected', { model }), 'ok');
  showPreflight(data);
}

function setDisconnectVisible(on) {
  const btn = $('#disconnectButton');
  if (btn) btn.hidden = !on;
}

// Drops the agent's COM references and resets every module's fetched data, so the
// next connection cannot silently mix results from two different models.
async function disconnectFromEtabs() {
  const btn = $('#disconnectButton');
  if (btn) btn.disabled = true;
  try {
    await postAgentJson('/api/etabs/disconnect', {}, 10000);
  } catch { /* the agent may already be gone; the UI still resets */ }

  comboCachePromise = null;
  wallShearRaw = null;
  for (const st of [driftState, pdeltaState, incrementState, columnAxialState,
                    beamShearState, beamAxialState, wallAxialState, wallShearState]) {
    st.combos = [];
    st.selected = [];
    st.basementCombos = [];
    st.stories = [];
    if ('lastResults' in st) st.lastResults = [];
    if ('lastResult' in st) st.lastResult = null;
  }

  $('#connectionDot').classList.remove('connected');
  $('#modelName').textContent = t('model.waiting');
  $('#activeModelStat').textContent = '—';
  $('#bridgeStatus').textContent = t('status.offline');
  $('#bridgeStatus').classList.remove('connected');
  setDisconnectVisible(false);
  if (btn) btn.disabled = false;
  log(t('terminal.disconnected'), 'ok');
  setActiveView('dashboard');
}

// Model chooser shown when several ETABS instances are running.
function showInstancePicker(instances) {
  const dialog = $('#instanceDialog');
  if (!dialog) return;
  $('#instanceList').innerHTML = instances.map((inst, i) => `
    <label class="instance-option">
      <input type="radio" name="etabsInstance" value="${escapeHtml(inst.id)}" ${i === 0 ? 'checked' : ''}>
      <span><strong>${escapeHtml(inst.modelName)}</strong></span>
    </label>`).join('');
  dialog.showModal();
}

async function confirmInstanceChoice() {
  const picked = $('input[name="etabsInstance"]:checked');
  if (!picked) return;
  setConnectButtonsLoading(true);
  try {
    const data = await postAgentJson('/api/etabs/connect-to', { instanceId: picked.value }, 15000);
    applyConnectionState(data);
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    setConnectButtonsLoading(false);
  }
}

// --- Pre-flight check -------------------------------------------------------
// Surfaces the environment facts that silently decide whether a check is even
// meaningful — agent/web version match, model lock, whether the analysis has run,
// and above all the ACTIVE UNIT SYSTEM: every module assumes kN-m, and ETABS
// returns table values in the model's present units, so a model left in kip-in
// would produce plausible-looking but wrong numbers.
function preflightRow(state, labelKey, value, detail = '') {
  const icon = { ok: '✅', warn: '⚠️', fail: '❌', unknown: '❔' }[state];
  return `<tr class="pf-${state}">
      <td class="pf-icon">${icon}</td>
      <td class="pf-label">${t(labelKey)}</td>
      <td class="pf-value">${value}${detail ? `<small>${detail}</small>` : ''}</td>
    </tr>`;
}

function buildPreflightRows(data) {
  const rows = [];
  const unknown = t('preflight.unknown');

  rows.push(preflightRow('ok', 'preflight.agent', `v${data.agentVersion || '?'}`));

  // Web and agent ship together; a mismatch means one side was not updated.
  const webMajorMinor = APP_VERSION.split('.').slice(0, 2).join('.');
  const agentMajorMinor = String(data.agentVersion || '').split('.').slice(0, 2).join('.');
  rows.push(agentMajorMinor && webMajorMinor === agentMajorMinor
    ? preflightRow('ok', 'preflight.versionMatch', `${t('preflight.compatible')} (web v${APP_VERSION})`)
    : preflightRow('warn', 'preflight.versionMatch', t('preflight.versionMismatch', { web: APP_VERSION, agent: data.agentVersion || '?' })));

  rows.push(data.etabsVersion
    ? preflightRow('ok', 'preflight.etabsVersion', data.etabsVersion)
    : preflightRow('unknown', 'preflight.etabsVersion', unknown));

  rows.push(preflightRow('ok', 'preflight.model', data.modelName || unknown));

  // A locked model is normal after analysis; it only blocks editing, not reading.
  rows.push(data.modelLocked === null || data.modelLocked === undefined
    ? preflightRow('unknown', 'preflight.lock', unknown)
    : preflightRow('ok', 'preflight.lock', t(data.modelLocked ? 'preflight.locked' : 'preflight.unlocked'),
        data.modelLocked ? t('preflight.lockedNote') : ''));

  if (data.unitsSupported === true) {
    rows.push(preflightRow('ok', 'preflight.units', data.units));
  } else if (data.unitsSupported === false) {
    rows.push(preflightRow('fail', 'preflight.units', data.units, t('preflight.unitsWrong')));
  } else {
    rows.push(preflightRow('unknown', 'preflight.units', unknown, t('preflight.unitsUnknown')));
  }

  if (data.analysisComplete === true) rows.push(preflightRow('ok', 'preflight.analysis', t('preflight.analysisDone')));
  else if (data.analysisComplete === false) rows.push(preflightRow('warn', 'preflight.analysis', t('preflight.analysisStale'), t('preflight.analysisNote')));
  else rows.push(preflightRow('unknown', 'preflight.analysis', unknown));

  return rows.join('');
}

function showPreflight(data) {
  const dialog = $('#preflightDialog');
  if (!dialog) return;
  $('#preflightRows').innerHTML = buildPreflightRows(data);
  const blocking = data.unitsSupported === false;
  const verdict = $('#preflightVerdict');
  verdict.textContent = t(blocking ? 'preflight.verdict.blocked' : 'preflight.verdict.ok');
  verdict.className = `status-banner ${blocking ? 'fail' : 'ok'}`;
  dialog.showModal();
}

// ---------------------------------------------------------------------------
// Interstory Drift module (pilot migration of GoreliKatOtelemesiManager, C#)
// ---------------------------------------------------------------------------

const driftState = {
  ...rigidStateDefaults(),
  params: { sdsDD2: 0, sdsDD3: 0, sd1DD2: 0, sd1DD3: 0, k: 1, tp: 0.5, esnekDerz: false, bodrum: false, bodrumKat: 0 },
  combos: [],
  stories: [],
  selected: [],
  lastResult: null
};

async function fetchAgentJson(path, timeoutMs = 8000) {
  const response = await fetch(`${AGENT_BASE}${path}`, {
    headers: { Accept: 'application/json' },
    signal: AbortSignal.timeout(timeoutMs)
  });
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  return response.json();
}

async function postAgentJson(path, body, timeoutMs = 8000) {
  const response = await fetch(`${AGENT_BASE}${path}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json', Accept: 'application/json' },
    body: JSON.stringify(body),
    signal: AbortSignal.timeout(timeoutMs)
  });
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  return response.json();
}

// Downloads a formula-backed Excel report the agent built with EPPlus, saving it under the
// filename the agent set in Content-Disposition (falls back to fallbackName).
async function downloadAgentExcel(path, body, fallbackName, timeoutMs = 15000) {
  const response = await fetch(`${AGENT_BASE}${path}`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body),
    signal: AbortSignal.timeout(timeoutMs)
  });
  if (!response.ok) throw new Error(`HTTP ${response.status}`);
  const disposition = response.headers.get('Content-Disposition') || '';
  const match = disposition.match(/filename="([^"]+)"/);
  const fileName = match ? match[1] : fallbackName;
  const blob = await response.blob();
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = fileName;
  a.click();
  URL.revokeObjectURL(url);
}

// --- Collapsible setup section ----------------------------------------------
// Wraps part of a setup panel in a roll-up/roll-down block using the site's standard
// panel typography. Open state is remembered per id so switching modules and coming
// back does not silently re-expand everything.
const sectionOpenState = {};

function setupSection(id, titleKey, contentHtml, defaultOpen = true) {
  const open = sectionOpenState[id] !== undefined ? sectionOpenState[id] : defaultOpen;
  return `<details class="setup-section" id="${id}" ${open ? 'open' : ''}>
      <summary>${t(titleKey)}</summary>
      <div class="setup-section-body">${contentHtml}</div>
    </details>`;
}

function bindSetupSections(root) {
  $$('.setup-section', root).forEach(el => {
    el.addEventListener('toggle', () => { sectionOpenState[el.id] = el.open; });
  });
}

// --- Shared rigid-basement rule ---------------------------------------------
// Stories at or below the chosen story are read from the basement combinations
// instead of the main ones. Every module that selects combinations offers this.
function rigidStateDefaults() {
  return { rijit: false, rijitStory: '', basementCombos: [], stories: [] };
}

function rigidSection(prefix, state) {
  return setupSection(`${prefix}RigidSection`, 'wallShear.section.rigid', `
    <label class="field-checkbox" for="${prefix}Rijit">
      <input type="checkbox" id="${prefix}Rijit" ${state.rijit ? 'checked' : ''}>
      ${t('wallShear.rigid.active')}
    </label>
    <div id="${prefix}RijitBody" ${state.rijit ? '' : 'hidden'}>
      <div class="field-grid">
        <div class="field">
          <label for="${prefix}RijitStory">${t('wallShear.rigid.story')}</label>
          <select id="${prefix}RijitStory"></select>
        </div>
      </div>
      <p class="combo-hint" id="${prefix}RijitNote"></p>
      ${comboPicker(`${prefix}Basement`, 'wallShear.rigid.combosHint')}
    </div>`, false);
}

async function initRigidSection(prefix, state, allCombos) {
  const toggle = $('#' + prefix + 'Rijit');
  const body = $('#' + prefix + 'RijitBody');
  const select = $('#' + prefix + 'RijitStory');
  const note = $('#' + prefix + 'RijitNote');

  const paintNote = () => {
    if (note) note.textContent = state.rijitStory ? t('wallShear.rigid.note', { story: state.rijitStory }) : '';
  };

  if (toggle) toggle.addEventListener('change', () => {
    state.rijit = toggle.checked;
    if (body) body.hidden = !toggle.checked;
  });

  initComboPicker(`${prefix}Basement`, {
    get combos() { return allCombos(); },
    set combos(v) { /* shared cache owns the list */ },
    get selected() { return state.basementCombos; },
    set selected(v) { state.basementCombos = v; }
  });

  try {
    if (!state.stories || state.stories.length === 0) {
      const res = await fetchAgentJson('/api/etabs/stories');
      if (res.etabsConnected) state.stories = (res.stories || []).slice().sort((a, b) => a.elevation - b.elevation);
    }
    if (select) {
      select.innerHTML = state.stories.slice().reverse()
        .map(s => `<option value="${escapeHtml(s.name)}">${escapeHtml(s.name)}</option>`).join('');
      if (state.rijitStory) select.value = state.rijitStory;
      else state.rijitStory = select.value;
      select.addEventListener('change', () => { state.rijitStory = select.value; paintNote(); });
    }
    paintNote();
  } catch { /* stories are optional until the rule is switched on */ }
}

// Combination list to request: main plus basement when the rule is on.
function rigidCombosToFetch(state) {
  return state.rijit && state.basementCombos.length
    ? [...new Set([...state.selected, ...state.basementCombos])]
    : state.selected;
}

// Stories the rigid rule treats as basement: everything at or below the chosen story.
// When the rule is off, the caller's existing (count-based) definition is kept.
function rigidBasementStorySet(state, stories, fallback) {
  if (!(state.rijit && state.rijitStory && state.basementCombos.length)) return fallback;
  const list = (state.stories && state.stories.length) ? state.stories : (stories || []);
  const cut = list.findIndex(x => x.name === state.rijitStory);
  if (cut < 0) return fallback;
  return new Set(list.filter((_, i) => i <= cut).map(x => x.name));
}

function rigidIsActive(state) {
  return !!(state.rijit && state.rijitStory && state.basementCombos.length);
}

// Whether a (story, combo) row counts, honouring the rigid-basement split.
function rigidRowAllowed(state, story, combo) {
  const name = (combo || '').trim();
  if (!state.rijit || !state.rijitStory || state.basementCombos.length === 0) {
    return state.selected.includes(name);
  }
  const order = s => (state.stories || []).findIndex(x => x.name === s);
  const splitIdx = order(state.rijitStory);
  const idx = order(story);
  if (idx < 0 || splitIdx < 0) return state.selected.includes(name);
  return idx > splitIdx ? state.selected.includes(name) : state.basementCombos.includes(name);
}

// --- Shared load-combination picker -----------------------------------------
// Two-list picker: everything the model defines on the left, the ones being checked
// on the right, moved with the ›/‹ buttons (or a double-click). Combinations load
// automatically when a module opens — there is no Fetch button — and the list is
// cached so opening several modules does not re-query the agent each time.
let comboCachePromise = null;

async function loadCombos(force = false) {
  if (force) comboCachePromise = null;
  if (!comboCachePromise) {
    comboCachePromise = fetchAgentJson('/api/etabs/combinations')
      .then(res => {
        if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
        return res.names || [];
      })
      .catch(err => { comboCachePromise = null; throw err; });
  }
  return comboCachePromise;
}

function comboPicker(prefix, hintKey = 'drift.combos.hint') {
  return `
    <div class="combo-dual">
      <div class="combo-dual-heading">
        <h3>${t('drift.combos.title')}</h3>
        <span class="combo-info" tabindex="0" role="note" title="${t(hintKey)}" aria-label="${t(hintKey)}">i</span>
      </div>
      <div class="combo-dual-body">
        <select class="combo-list" id="${prefix}Available" multiple size="9" aria-label="${t('combos.available')}"></select>
        <div class="combo-dual-actions">
          <button type="button" class="combo-move" id="${prefix}Add" title="${t('combos.add')}" aria-label="${t('combos.add')}">›</button>
          <button type="button" class="combo-move" id="${prefix}Remove" title="${t('combos.remove')}" aria-label="${t('combos.remove')}">‹</button>
        </div>
        <select class="combo-list" id="${prefix}Selected" multiple size="9" aria-label="${t('combos.selected')}"></select>
      </div>
      <p class="combo-status" id="${prefix}ComboStatus" aria-live="polite">${t('combos.loading')}</p>
    </div>`;
}

function paintComboPicker(prefix, state) {
  const avail = $('#' + prefix + 'Available');
  const sel = $('#' + prefix + 'Selected');
  if (!avail || !sel) return;
  const chosen = new Set(state.selected);
  avail.innerHTML = state.combos.filter(c => !chosen.has(c))
    .map(c => `<option value="${escapeHtml(c)}">${escapeHtml(c)}</option>`).join('');
  sel.innerHTML = state.selected
    .map(c => `<option value="${escapeHtml(c)}">${escapeHtml(c)}</option>`).join('');
  const status = $('#' + prefix + 'ComboStatus');
  if (status) status.textContent = t('combos.count', { total: state.combos.length, selected: state.selected.length });
}

async function initComboPicker(prefix, state, onChange) {
  const avail = $('#' + prefix + 'Available');
  const sel = $('#' + prefix + 'Selected');
  const add = $('#' + prefix + 'Add');
  const remove = $('#' + prefix + 'Remove');

  const move = (from, toSelected) => {
    const picked = [...from.selectedOptions].map(o => o.value);
    if (picked.length === 0) return;
    if (toSelected) {
      const have = new Set(state.selected);
      state.selected = [...state.selected, ...picked.filter(c => !have.has(c))];
    } else {
      const drop = new Set(picked);
      state.selected = state.selected.filter(c => !drop.has(c));
    }
    paintComboPicker(prefix, state);
    if (onChange) onChange();
  };

  if (add) add.addEventListener('click', () => move(avail, true));
  if (remove) remove.addEventListener('click', () => move(sel, false));
  if (avail) avail.addEventListener('dblclick', () => move(avail, true));
  if (sel) sel.addEventListener('dblclick', () => move(sel, false));

  paintComboPicker(prefix, state);
  if (state.combos.length === 0) {
    try {
      state.combos = await loadCombos();
      paintComboPicker(prefix, state);
    } catch (error) {
      const status = $('#' + prefix + 'ComboStatus');
      if (status) status.textContent = t('combos.loadFailed', { error: error.message });
    }
  }
}

// --- Per-module reset -------------------------------------------------------
// Clears computed results (and the combination selection) and re-renders the module
// as a clean sheet. Entered parameters are deliberately kept — the engineer is
// usually re-running the same building, not starting a different one.
function resetModule(state, rerender, extraKeys = {}) {
  state.selected = [];
  state.lastResults = [];
  state.lastResult = null;
  Object.assign(state, extraKeys);
  rerender();
  log(t('status.reset'), 'ok');
}

// --- Accessible number fields + engineering input validation ----------------
// numberField() emits a properly associated <label for>/<input id> pair plus an
// error paragraph wired via aria-describedby, so screen readers announce both the
// field name and the reason a value was rejected. Use it instead of hand-written
// <div class="field"> markup.
function numberField(id, labelKey, { step = 'any', min, max, unit } = {}) {
  const attrs = [
    `type="number"`, `step="${step}"`, `id="${id}"`,
    min !== undefined ? `min="${min}"` : '',
    max !== undefined ? `max="${max}"` : '',
    `aria-describedby="${id}-err"`
  ].filter(Boolean).join(' ');
  const label = unit ? `${t(labelKey)} <span class="field-unit">(${unit})</span>` : t(labelKey);
  return `<div class="field">
      <label for="${id}">${label}</label>
      <input ${attrs}>
      <p class="field-error" id="${id}-err" role="alert" hidden></p>
    </div>`;
}

function checkboxField(id, labelKey) {
  return `<label class="field-checkbox" for="${id}"><input type="checkbox" id="${id}"> ${t(labelKey)}</label>`;
}

function clearFieldErrors(root = document) {
  $$('.field-error', root).forEach(el => { el.hidden = true; el.textContent = ''; });
  $$('input[aria-invalid]', root).forEach(el => el.removeAttribute('aria-invalid'));
}

function showFieldError(id, message) {
  const input = $('#' + id);
  const errorEl = $(`#${id}-err`);
  if (errorEl) { errorEl.textContent = message; errorEl.hidden = false; }
  if (input) input.setAttribute('aria-invalid', 'true');
}

// Runs a list of {id, ok, message} rules. Reports EVERY failing field at once (not
// just the first) so the engineer can fix them in one pass, focuses the first one,
// and returns true only when everything passed.
function validateFields(rules, root = document) {
  clearFieldErrors(root);
  // Keep only the first failure per field, so the toast and the inline message
  // under the input always report the same reason.
  const seen = new Set();
  const failures = rules.filter(rule => {
    if (rule.ok || seen.has(rule.id)) return false;
    seen.add(rule.id);
    return true;
  });
  if (failures.length === 0) return true;
  failures.forEach(f => showFieldError(f.id, f.message));
  const first = $('#' + failures[0].id);
  if (first) first.focus();
  log(failures[0].message, 'error');
  return false;
}

const inRange = (v, lo, hi) => Number.isFinite(v) && v >= lo && v <= hi;

// --- "Calculation basis" disclosure -----------------------------------------
// Every module documents the code clause, the equations it evaluates and the
// intermediate values behind the headline number, so the engineer can re-derive
// the result by hand instead of trusting a black box.
function calcBasis(codeRef, equations, values = []) {
  const eqHtml = equations.map(e => `<li><code>${e}</code></li>`).join('');
  const valHtml = values.length
    ? `<table class="calc-values"><tbody>${values
        .map(([k, v]) => `<tr><th scope="row">${k}</th><td>${v}</td></tr>`).join('')}</tbody></table>`
    : '';
  return `<details class="calc-basis">
      <summary>${t('calcBasis.title')}</summary>
      <div class="calc-basis-body">
        <p class="calc-ref"><strong>${t('calcBasis.reference')}:</strong> ${codeRef}</p>
        <ul class="calc-eq">${eqHtml}</ul>
        ${valHtml}
        <p class="calc-note">${t('calcBasis.note')}</p>
      </div>
    </details>`;
}

// --- Excel-like column filtering for results tables -------------------------
// installTableFilter(tbody) adds a secondary header row of per-column filter
// controls: columns with a small set of distinct text values get a dropdown
// (exact match), higher-cardinality text/number columns get a "contains" text
// box, and columns whose body cells hold form controls (editable inputs, action
// buttons) get no filter. Selections persist across tbody re-renders — keyed by
// tbody id — so live cell edits and rebar re-groupings don't reset the filter.
const tableFilterState = new Map();
const FILTER_SELECT_MAX = 20;

function escapeHtml(v) {
  return String(v).replace(/[&<>"']/g, ch => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[ch]));
}

function filterCellText(cell) {
  if (!cell) return '';
  const control = cell.querySelector('input, select');
  if (control) return control.value != null ? String(control.value) : '';
  return cell.textContent.trim();
}

function isPureNumber(v) {
  return /^-?\d+(?:[.,]\d+)?%?$/.test(v);
}

function applyTableFilter(table) {
  const tbody = table.tBodies[0];
  if (!tbody) return;
  const filters = tableFilterState.get(tbody.id) || {};
  let shown = 0;
  const dataRows = [...tbody.rows].filter(r => !r.querySelector('.table-empty'));
  for (const row of dataRows) {
    let show = true;
    for (const key of Object.keys(filters)) {
      const f = filters[key];
      if (!f || !f.value) continue;
      const text = filterCellText(row.cells[Number(key)]).toLowerCase();
      const val = f.value.toLowerCase();
      if (f.type === 'select' ? text !== val : !text.includes(val)) { show = false; break; }
    }
    row.style.display = show ? '' : 'none';
    if (show) shown++;
  }
  // Surface an empty-state row when a filter hides everything.
  let noMatch = tbody.querySelector('.filter-no-match');
  if (dataRows.length && shown === 0) {
    if (!noMatch) {
      noMatch = document.createElement('tr');
      noMatch.className = 'filter-no-match';
      const cell = document.createElement('td');
      cell.colSpan = table.tHead.rows[0].cells.length;
      cell.className = 'table-empty';
      cell.textContent = t('filter.noMatch');
      noMatch.appendChild(cell);
      tbody.appendChild(noMatch);
    }
    noMatch.style.display = '';
  } else if (noMatch) {
    noMatch.style.display = 'none';
  }
}

function installTableFilter(tbody) {
  if (!tbody) return;
  const table = tbody.closest('table');
  if (!table || !table.tHead || !table.tHead.rows[0]) return;
  const headRow = table.tHead.rows[0];

  const existing = table.querySelector('.filter-row');
  const dataRows = [...tbody.rows].filter(r => !r.querySelector('.table-empty') && !r.classList.contains('filter-no-match'));
  if (dataRows.length === 0) { if (existing) existing.remove(); tableFilterState.delete(tbody.id); return; }

  const colCount = headRow.cells.length;
  const saved = tableFilterState.get(tbody.id) || {};
  const nextState = {};
  const filterRow = document.createElement('tr');
  filterRow.className = 'filter-row';

  for (let ci = 0; ci < colCount; ci++) {
    const th = document.createElement('th');
    const hasControl = dataRows.some(r => r.cells[ci] && r.cells[ci].querySelector('input, select, button'));
    if (!hasControl) {
      const values = dataRows.map(r => filterCellText(r.cells[ci])).filter(v => v !== '');
      const distinct = [...new Set(values)];
      const allNumeric = distinct.length > 0 && distinct.every(isPureNumber);
      if (distinct.length > 1 && distinct.length <= FILTER_SELECT_MAX && !allNumeric) {
        const sel = document.createElement('select');
        sel.className = 'table-filter-select';
        const sorted = distinct.sort((a, b) => a.localeCompare(b, undefined, { numeric: true }));
        sel.innerHTML = `<option value="">${t('filter.all')}</option>` +
          sorted.map(v => `<option value="${escapeHtml(v)}">${escapeHtml(v)}</option>`).join('');
        const prev = saved[ci];
        if (prev && prev.type === 'select') sel.value = prev.value;
        nextState[ci] = { type: 'select', value: sel.value };
        sel.addEventListener('change', () => { nextState[ci].value = sel.value; applyTableFilter(table); });
        th.appendChild(sel);
      } else if (distinct.length > 1) {
        const inp = document.createElement('input');
        inp.type = 'text';
        inp.className = 'table-filter-input';
        inp.placeholder = t('filter.placeholder');
        const prev = saved[ci];
        if (prev && prev.type === 'text') inp.value = prev.value;
        nextState[ci] = { type: 'text', value: inp.value };
        inp.addEventListener('input', () => { nextState[ci].value = inp.value; applyTableFilter(table); });
        th.appendChild(inp);
      }
    }
    filterRow.appendChild(th);
  }

  if (existing) existing.remove();
  table.tHead.appendChild(filterRow);
  tableFilterState.set(tbody.id, nextState);
  applyTableFilter(table);
}

// TBDY 2018 lambda: interpolates between DD-2 and DD-3 spectra depending on Tp vs TA.
function driftCalculateLambda({ sdsDD2, sdsDD3, sd1DD2, sd1DD3, tp }) {
  if (sdsDD2 === 0) return 0;
  const ta = sd1DD2 / sdsDD2;
  return tp < ta ? sdsDD3 / sdsDD2 : sd1DD3 / sd1DD2;
}

function driftCalculateLimit({ esnekDerz, k }) {
  return esnekDerz ? 0.016 * k : 0.008 * k;
}

// Basement stories = the N lowest stories by elevation, excluding "Base".
function determineBasementStories(stories, count) {
  if (!count || count <= 0) return new Set();
  return new Set(
    stories
      .filter(s => s.name.toLowerCase() !== 'base')
      .slice()
      .sort((a, b) => a.elevation - b.elevation)
      .slice(0, count)
      .map(s => s.name)
  );
}

function groupCombos(names) {
  const upper = names.map(n => n.toUpperCase());
  const pick = (mustInclude) => names.filter((_, i) => mustInclude.every(part => upper[i].includes(part)));
  return {
    xUST: pick(['X', 'UST']),
    xALT: pick(['X', 'ALT']),
    yUST: pick(['Y', 'UST']),
    yALT: pick(['Y', 'ALT'])
  };
}

// Mirrors the desktop app's per-direction, per-basement row filtering.
function filterDriftRows(rows, groups, basementNames, useBasement, rigidGroups) {
  const result = [];
  for (const row of rows) {
    const direction = row.direction.toUpperCase();
    const isBasement = basementNames.has(row.story);
    // With the rigid rule on, a basement story takes ANY of the basement combinations
    // matching its direction rather than the main list's ALT bucket.
    if (rigidGroups && isBasement) {
      const pool = direction === 'X'
        ? [...rigidGroups.xUST, ...rigidGroups.xALT]
        : [...rigidGroups.yUST, ...rigidGroups.yALT];
      if (pool.includes(row.outputCase)) result.push(row);
      continue;
    }
    if (direction === 'X') {
      if (groups.xUST.includes(row.outputCase) && (!useBasement || !isBasement)) result.push(row);
      else if (useBasement && groups.xALT.includes(row.outputCase) && isBasement) result.push(row);
    } else if (direction === 'Y') {
      if (groups.yUST.includes(row.outputCase) && (!useBasement || !isBasement)) result.push(row);
      else if (useBasement && groups.yALT.includes(row.outputCase) && isBasement) result.push(row);
    }
  }
  return result;
}

function calculateDriftItems(rows, params) {
  const lambda = driftCalculateLambda(params);
  const limit = driftCalculateLimit(params);
  const items = rows.map(row => {
    const lambdaDrift = lambda * row.drift;
    return { ...row, lambdaDrift, limit, isOk: lambdaDrift < limit };
  });
  return { lambda, limit, items, allPassed: items.length > 0 && items.every(i => i.isOk) };
}

function sortDriftItems(items) {
  return [...items].sort((a, b) =>
    (a.direction === 'X' ? 0 : 1) - (b.direction === 'X' ? 0 : 1) || a.story.localeCompare(b.story));
}

function renderDriftModule() {
  renderDriftSetupPanel();
  renderDriftResultsPanel();
}

function renderDriftSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('drift.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('driftSdsDD2', 'drift.params.sdsDD2', { min: 0 })}
      ${numberField('driftSdsDD3', 'drift.params.sdsDD3', { min: 0 })}
      ${numberField('driftSd1DD2', 'drift.params.sd1DD2', { min: 0 })}
      ${numberField('driftSd1DD3', 'drift.params.sd1DD3', { min: 0 })}
      ${numberField('driftK', 'drift.params.k', { min: 0.5, max: 1 })}
      ${numberField('driftTp', 'drift.params.tp', { min: 0, max: 10, unit: 's' })}
      ${checkboxField('driftEsnekDerz', 'drift.params.flexibleJoint')}
      ${checkboxField('driftBodrum', 'drift.params.basement')}
      ${numberField('driftBodrumKat', 'drift.params.basementCount', { step: 1, min: 0, max: 10 })}
    </div>
    ${comboPicker('drift', 'drift.combos.hint')}
    ${rigidSection('drift', driftState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="driftCalculate">${t('drift.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="driftReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>`;

  bindDriftParamInputs(panel);
  initComboPicker('drift', driftState);
  bindSetupSections(panel);
  initRigidSection('drift', driftState, () => driftState.combos);
  $('#driftCalculate', panel).addEventListener('click', runDriftCheck);
  $('#driftReset', panel).addEventListener('click', () => resetModule(driftState, renderDriftModule));
}

function bindDriftParamInputs(panel) {
  const bindNumber = (id, key) => {
    const el = $('#' + id, panel);
    el.value = driftState.params[key];
    el.addEventListener('input', () => { driftState.params[key] = parseFloat(el.value) || 0; });
  };
  bindNumber('driftSdsDD2', 'sdsDD2');
  bindNumber('driftSdsDD3', 'sdsDD3');
  bindNumber('driftSd1DD2', 'sd1DD2');
  bindNumber('driftSd1DD3', 'sd1DD3');
  bindNumber('driftK', 'k');
  bindNumber('driftTp', 'tp');

  const esnek = $('#driftEsnekDerz', panel);
  esnek.checked = driftState.params.esnekDerz;
  esnek.addEventListener('change', () => { driftState.params.esnekDerz = esnek.checked; });

  const bodrum = $('#driftBodrum', panel);
  const bodrumKat = $('#driftBodrumKat', panel);
  bodrum.checked = driftState.params.bodrum;
  bodrumKat.value = driftState.params.bodrumKat;
  bodrumKat.disabled = !bodrum.checked;
  bodrum.addEventListener('change', () => {
    driftState.params.bodrum = bodrum.checked;
    bodrumKat.disabled = !bodrum.checked;
  });
  bodrumKat.addEventListener('input', () => { driftState.params.bodrumKat = parseInt(bodrumKat.value, 10) || 0; });
}

function renderDriftResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="driftExport" ${driftState.lastResult ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
    <div class="status-banner pending" id="driftStatusBanner">${t('drift.status.pending')}</div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('drift.table.story')}</th><th>${t('drift.table.combo')}</th><th>${t('drift.table.direction')}</th>
          <th>${t('drift.table.drift')}</th><th>${t('drift.table.lambdaDrift')}</th><th>${t('drift.table.limit')}</th><th>${t('drift.table.status')}</th>
        </tr></thead>
        <tbody id="driftResultsBody"><tr><td colspan="7" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>`;

  $('#driftExport', panel).addEventListener('click', exportDriftExcel);
  if (driftState.lastResult) renderDriftResultsTable(driftState.lastResult);
}

function renderDriftResultsTable(result) {
  const body = $('#driftResultsBody');
  if (!body) return;
  const sorted = sortDriftItems(result.items);
  body.innerHTML = sorted.length
    ? sorted.map(item => `
        <tr>
          <td>${item.story}</td><td>${item.outputCase}</td><td>${item.direction}</td>
          <td>${item.drift.toFixed(5)}</td><td>${item.lambdaDrift.toFixed(5)}</td><td>${item.limit.toFixed(5)}</td>
          <td>${item.isOk ? '✅' : '❌'}</td>
        </tr>`).join('')
    : `<tr><td colspan="7" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  const banner = $('#driftStatusBanner');
  banner.textContent = t(result.allPassed ? 'drift.status.passed' : 'drift.status.failed');
  banner.className = `status-banner ${result.allPassed ? 'ok' : 'fail'}`;

  const exportBtn = $('#driftExport');
  if (exportBtn) exportBtn.disabled = false;

  const { sdsDD2, sdsDD3, sd1DD2, sd1DD3, k, tp, esnekDerz } = driftState.params;
  const taDD2 = sdsDD2 ? sd1DD2 / sdsDD2 : 0;
  const worst = sorted.reduce((a, b) => (b.lambdaDrift / b.limit > a.lambdaDrift / a.limit ? b : a), sorted[0]);
  renderCalcBasis('#driftResultsBody', 'driftBasis', calcBasis(
    'TBDY 2018 §4.9.1 (Denk. 4.32–4.33), Tablo 4.9',
    [
      'λ = SDS,DD-3 / SDS,DD-2      (Tp &lt; TA)',
      'λ = SD1,DD-3 / SD1,DD-2      (Tp ≥ TA)',
      'TA = SD1,DD-2 / SDS,DD-2',
      'δi,max / hi ≤ 0.008·κ        (' + t('drift.basis.rigid') + ')',
      'δi,max / hi ≤ 0.016·κ        (' + t('drift.basis.flexible') + ')'
    ],
    [
      ['TA = SD1/SDS (DD-2)', `${sd1DD2} / ${sdsDD2} = ${taDD2.toFixed(4)} s`],
      ['Tp', `${tp} s → ${tp < taDD2 ? 'Tp < TA' : 'Tp ≥ TA'}`],
      ['λ', `${tp < taDD2 ? `${sdsDD3} / ${sdsDD2}` : `${sd1DD3} / ${sd1DD2}`} = <strong>${result.lambda.toFixed(4)}</strong>`],
      ['κ', `${k}`],
      [t('drift.basis.limitUsed'), `${esnekDerz ? '0.016' : '0.008'} · ${k} = <strong>${result.limit.toFixed(5)}</strong>`],
      ...(worst ? [[t('drift.basis.worst'),
        `${worst.story} / ${worst.direction}: λ·δ/h = ${result.lambda.toFixed(4)} · ${worst.drift.toFixed(5)} = ${worst.lambdaDrift.toFixed(5)} ` +
        `(${(worst.lambdaDrift / worst.limit * 100).toFixed(1)}% ${t('drift.basis.ofLimit')})`]] : [])
    ]));
}

// Places a calculation-basis block right after the given table body's wrapper,
// replacing any previous one so repeated runs don't stack duplicates.
function renderCalcBasis(bodySelector, blockId, html) {
  const body = $(bodySelector);
  if (!body) return;
  const wrap = body.closest('.table-wrap') || body.closest('table');
  if (!wrap) return;
  const existing = $('#' + blockId);
  if (existing) existing.remove();
  wrap.insertAdjacentHTML('afterend', `<div id="${blockId}">${html}</div>`);
}

async function runDriftCheck() {
  const panel = $('#setupPanel');
  const btn = $('#driftCalculate', panel);
  if (driftState.selected.length === 0) {
    log(t('drift.error.noCombos'), 'error');
    return;
  }

  const { sdsDD2, sdsDD3, sd1DD2, sd1DD3, k, tp } = driftState.params;
  const validated = validateFields([
    { id: 'driftSdsDD2', ok: inRange(sdsDD2, 0.05, 4), message: t('validate.range', { field: 'SDS (DD-2)', min: '0.05 g', max: '4 g', value: sdsDD2 }) },
    { id: 'driftSdsDD3', ok: inRange(sdsDD3, 0.02, 4), message: t('validate.range', { field: 'SDS (DD-3)', min: '0.02 g', max: '4 g', value: sdsDD3 }) },
    { id: 'driftSd1DD2', ok: inRange(sd1DD2, 0.02, 3), message: t('validate.range', { field: 'SD1 (DD-2)', min: '0.02 g', max: '3 g', value: sd1DD2 }) },
    { id: 'driftSd1DD3', ok: inRange(sd1DD3, 0.01, 3), message: t('validate.range', { field: 'SD1 (DD-3)', min: '0.01 g', max: '3 g', value: sd1DD3 }) },
    // TBDY 2018 Eq. 4.33: κ is 1.0 for concrete frames, 0.5 for the masonry-infill case.
    { id: 'driftK', ok: inRange(k, 0.5, 1), message: t('validate.range', { field: 'κ', min: 0.5, max: 1, value: k }) },
    { id: 'driftTp', ok: inRange(tp, 0.05, 10), message: t('validate.range', { field: 'Tp', min: '0.05 s', max: '10 s', value: tp }) }
  ], panel);
  if (!validated) return;

  btn.disabled = true;
  try {
    if (driftState.stories.length === 0) {
      const storiesRes = await fetchAgentJson('/api/etabs/stories');
      if (!storiesRes.etabsConnected) throw new Error(storiesRes.error || t('drift.error.notConnected'));
      driftState.stories = storiesRes.stories;
    }

    const comboParam = encodeURIComponent(rigidCombosToFetch(driftState).join(','));
    const driftRes = await fetchAgentJson(`/api/etabs/story-drifts?combos=${comboParam}`);
    if (!driftRes.etabsConnected) throw new Error(driftRes.error || t('drift.error.notConnected'));

    const groups = groupCombos(driftState.selected);
    const legacyBasement = driftState.params.bodrum
      ? determineBasementStories(driftState.stories, driftState.params.bodrumKat)
      : new Set();
    const rigid = rigidIsActive(driftState);
    const basementNames = rigidBasementStorySet(driftState, driftState.stories, legacyBasement);
    const filtered = filterDriftRows(driftRes.rows || [], groups, basementNames,
      driftState.params.bodrum || rigid, rigid ? groupCombos(driftState.basementCombos) : null);
    if (filtered.length === 0) throw new Error(t('drift.error.noData'));

    const result = calculateDriftItems(filtered, driftState.params);
    driftState.lastResult = result;
    renderDriftResultsTable(result);
    recordLastCheck('drift');
    log(t(result.allPassed ? 'drift.status.passed' : 'drift.status.failed'), result.allPassed ? 'ok' : 'error');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

async function exportDriftExcel() {
  const result = driftState.lastResult;
  if (!result) return;
  const btn = $('#driftExport');
  if (btn) btn.disabled = true;
  try {
    const sorted = sortDriftItems(result.items);
    await downloadAgentExcel('/api/etabs/export/drift', {
      sdsDD2: driftState.params.sdsDD2, sdsDD3: driftState.params.sdsDD3,
      sd1DD2: driftState.params.sd1DD2, sd1DD3: driftState.params.sd1DD3,
      tp: driftState.params.tp, k: driftState.params.k, esnekDerz: driftState.params.esnekDerz,
      rows: sorted.map(item => ({ story: item.story, combo: item.outputCase, direction: item.direction, drift: item.drift }))
    }, 'GoreliKat_Sonuc.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Second-Order Effects (İkinci Mertebe) — ported from IkinciMertebeManager (C#)
// θ = (avgDriftRatio · cumulativeWeight) / storyShear ; limit = 0.12·D / (Ch·R)
// ---------------------------------------------------------------------------

const pdeltaState = {
  ...rigidStateDefaults(),
  params: { ch: 0.5, r: 8, d: 2.5, bodrum: false, bodrumKat: 0 },
  combos: [],
  stories: [],
  selected: [],
  lastResult: null
};

const pdeltaContains = (haystack, needle) => haystack.toUpperCase().includes(needle.toUpperCase());
// Bidirectional partial match, matching the C# LoadCase comparison.
const pdeltaLoadMatch = (a, b) => a === b || pdeltaContains(a, b) || pdeltaContains(b, a);

function pdeltaCalculateDirection(sortedStories, forces, drifts, mass, direction, ch, r, d) {
  const results = [];
  const limit = 0.12 * d / (ch * r);
  const uniqueCombos = [...new Set(forces.map(f => f.loadCase))];
  for (const combo of uniqueCombos) {
    let cumWeight = 0;
    for (const story of sortedStories) {
      const m = mass.find(x => x.story === story.name);
      cumWeight += m ? m.weight : 0;
      const fd = forces.find(f => f.story === story.name && pdeltaLoadMatch(f.loadCase, combo));
      const dd = drifts.find(x => x.story === story.name && pdeltaLoadMatch(x.loadCase, combo));
      if (!fd && !dd) continue;
      const v = direction === 'X' ? (fd ? fd.vx : 0) : (fd ? fd.vy : 0);
      const delta = dd ? dd.drift : 0;
      const theta = v !== 0 ? (delta * cumWeight) / v : 0;
      results.push({ story: story.name, loadCase: combo, direction, vi: v, wij: cumWeight, driftRatio: delta, theta, limit, ok: theta <= limit });
    }
  }
  return results;
}

// Split selected combos into X/Y × UST/ALT buckets, matching the desktop's loose matching.
function pdeltaBucketCombos(selected) {
  const u = s => s.toUpperCase();
  let xUST = selected.filter(c => u(c).includes('X') && (u(c).includes('UST') || u(c).includes('U')));
  let xALT = selected.filter(c => u(c).includes('X') && (u(c).includes('ALT') || u(c).includes('A')));
  let yUST = selected.filter(c => u(c).includes('Y') && (u(c).includes('UST') || u(c).includes('U')));
  let yALT = selected.filter(c => u(c).includes('Y') && (u(c).includes('ALT') || u(c).includes('A')));
  if (xUST.length === 0 && xALT.length === 0) xUST = selected.filter(c => u(c).includes('X'));
  if (yUST.length === 0 && yALT.length === 0) yUST = selected.filter(c => u(c).includes('Y'));
  return { xUST, xALT, yUST, yALT };
}

function pdeltaComputeResult(forces, drifts, mass, stories, selected, params) {
  const nonBase = stories.filter(s => s.name.toLowerCase() !== 'base');
  const basementNames = new Set();
  if (params.bodrum && params.bodrumKat > 0) {
    [...nonBase].sort((a, b) => a.elevation - b.elevation).slice(0, params.bodrumKat).forEach(s => basementNames.add(s.name));
  }

  let { xUST, xALT, yUST, yALT } = pdeltaBucketCombos(selected);
  // Rigid rule: basement stories are defined by the chosen story and are driven by the
  // basement combination list, replacing the count-based definition above.
  if (rigidIsActive(pdeltaState)) {
    const set = rigidBasementStorySet(pdeltaState, stories, basementNames);
    basementNames.clear();
    set.forEach(n => basementNames.add(n));
    const b = pdeltaBucketCombos(pdeltaState.basementCombos);
    xALT = [...b.xUST, ...b.xALT];
    yALT = [...b.yUST, ...b.yALT];
  }
  const anyMatch = (list, loadCase) => list.some(c => pdeltaContains(loadCase, c));

  const xForces = [], yForces = [], xDrifts = [], yDrifts = [];
  for (const f of forces) {
    const isB = basementNames.has(f.story);
    if (isB ? anyMatch(xALT, f.loadCase) : anyMatch(xUST, f.loadCase)) xForces.push(f);
    if (isB ? anyMatch(yALT, f.loadCase) : anyMatch(yUST, f.loadCase)) yForces.push(f);
  }
  for (const dd of drifts) {
    const isB = basementNames.has(dd.story);
    if (dd.direction.toUpperCase() === 'X') {
      if (isB ? anyMatch(xALT, dd.loadCase) : anyMatch(xUST, dd.loadCase)) xDrifts.push(dd);
    } else if (dd.direction.toUpperCase() === 'Y') {
      if (isB ? anyMatch(yALT, dd.loadCase) : anyMatch(yUST, dd.loadCase)) yDrifts.push(dd);
    }
  }

  const sorted = [...nonBase].sort((a, b) => b.elevation - a.elevation); // top -> base
  let results = [];
  if (xForces.length) results = results.concat(pdeltaCalculateDirection(sorted, xForces, xDrifts, mass, 'X', params.ch, params.r, params.d));
  if (yForces.length) results = results.concat(pdeltaCalculateDirection(sorted, yForces, yDrifts, mass, 'Y', params.ch, params.r, params.d));

  const order = sorted.map(s => s.name);
  results.sort((a, b) =>
    a.direction.localeCompare(b.direction) ||
    order.indexOf(a.story) - order.indexOf(b.story) ||
    a.loadCase.localeCompare(b.loadCase));

  return { items: results, allOk: results.length > 0 && results.every(x => x.ok) };
}

function parseTableRows(res, mapFn) {
  if (!res || !res.rows) return [];
  return res.rows.map(mapFn).filter(Boolean);
}

async function pdeltaFetchAndCompute() {
  const comboParam = encodeURIComponent(rigidCombosToFetch(pdeltaState).join(','));
  const [massRes, forcesRes, driftsRes, storiesRes] = await Promise.all([
    fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Mass Summary by Story')}`),
    fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Story Forces')}&combos=${comboParam}`),
    fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Story Max Over Avg Drifts')}&combos=${comboParam}`),
    fetchAgentJson('/api/etabs/stories')
  ]);
  for (const r of [massRes, forcesRes, driftsRes, storiesRes])
    if (!r.etabsConnected) throw new Error(r.error || t('drift.error.notConnected'));

  // Mass -> weight (kN) = UX · 9.81
  const mF = massRes.fields;
  const mStory = tableIndex(mF, 'Story'), mUX = tableIndex(mF, 'UX', 'MassX');
  const mass = parseTableRows(massRes, row => {
    const story = row[mStory];
    if (!story || story.toLowerCase() === 'base') return null;
    return { story, weight: (parseFloat(row[mUX]) || 0) * 9.81 };
  });

  // Story Forces -> Location == Bottom, V = |VX|/|VY|
  const fF = forcesRes.fields;
  const fStory = tableIndex(fF, 'Story'), fCase = tableIndex(fF, 'OutputCase', 'LoadCase', 'Case');
  const fLoc = tableIndex(fF, 'Location'), fVX = tableIndex(fF, 'VX'), fVY = tableIndex(fF, 'VY');
  const forces = parseTableRows(forcesRes, row => {
    if (fLoc >= 0 && (row[fLoc] || '').toLowerCase() !== 'bottom') return null;
    if (fStory < 0 || fCase < 0) return null;
    return {
      story: row[fStory], loadCase: row[fCase],
      vx: Math.abs(parseFloat(row[fVX]) || 0), vy: Math.abs(parseFloat(row[fVY]) || 0)
    };
  });

  // Story Max Over Avg Drifts -> Avg Drift column (ratio)
  const dF = driftsRes.fields;
  const dStory = tableIndex(dF, 'Story'), dCase = tableIndex(dF, 'OutputCase', 'LoadCase', 'Case');
  const dDir = tableIndex(dF, 'Direction');
  let dDrift = tableIndex(dF, 'AvgDrift');
  if (dDrift < 0) dDrift = 6; // desktop fallback: fixed Avg-Drift column index
  const drifts = parseTableRows(driftsRes, row => {
    if (dStory < 0 || dCase < 0) return null;
    return {
      story: row[dStory], loadCase: row[dCase],
      direction: dDir >= 0 ? (row[dDir] || '') : '',
      drift: parseFloat(row[dDrift]) || 0
    };
  });

  const stories = (storiesRes.stories || []).map(s => ({ name: s.name, elevation: s.elevation }));
  return pdeltaComputeResult(forces, drifts, mass, stories, pdeltaState.selected, pdeltaState.params);
}

function renderPdeltaModule() {
  renderPdeltaSetupPanel();
  renderPdeltaResultsPanel();
}

function renderPdeltaSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('pdelta.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('pdCh', 'pdelta.params.ch', { min: 0.1, max: 3 })}
      ${numberField('pdR', 'pdelta.params.r', { min: 1, max: 10 })}
      ${numberField('pdD', 'pdelta.params.d', { min: 1, max: 4 })}
      ${checkboxField('pdBodrum', 'drift.params.basement')}
      ${numberField('pdBodrumKat', 'drift.params.basementCount', { step: 1, min: 0, max: 10 })}
    </div>
    ${comboPicker('pd', 'pdelta.combos.hint')}
    ${rigidSection('pd', pdeltaState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="pdCalculate">${t('drift.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="pdReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>`;

  const bindNumber = (id, key, isInt = false) => {
    const el = $('#' + id, panel);
    el.value = pdeltaState.params[key];
    el.addEventListener('input', () => { pdeltaState.params[key] = (isInt ? parseInt(el.value, 10) : parseFloat(el.value)) || 0; });
  };
  bindNumber('pdCh', 'ch');
  bindNumber('pdR', 'r');
  bindNumber('pdD', 'd');
  bindNumber('pdBodrumKat', 'bodrumKat', true);

  const bodrum = $('#pdBodrum', panel);
  const bodrumKat = $('#pdBodrumKat', panel);
  bodrum.checked = pdeltaState.params.bodrum;
  bodrumKat.disabled = !bodrum.checked;
  bodrum.addEventListener('change', () => {
    pdeltaState.params.bodrum = bodrum.checked;
    bodrumKat.disabled = !bodrum.checked;
  });

  initComboPicker('pd', pdeltaState);
  bindSetupSections(panel);
  initRigidSection('pd', pdeltaState, () => pdeltaState.combos);
  $('#pdCalculate', panel).addEventListener('click', runPdeltaCheck);
  $('#pdReset', panel).addEventListener('click', () => resetModule(pdeltaState, renderPdeltaModule));
}

function renderPdeltaResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="pdExport" ${pdeltaState.lastResult ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
    <div class="status-banner pending" id="pdStatusBanner">${t('drift.status.pending')}</div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('drift.table.story')}</th><th>${t('drift.table.combo')}</th><th>${t('drift.table.direction')}</th>
          <th>${t('pdelta.table.vi')}</th><th>${t('pdelta.table.wij')}</th><th>${t('drift.table.drift')}</th>
          <th>${t('pdelta.table.theta')}</th><th>${t('drift.table.limit')}</th><th>${t('drift.table.status')}</th>
        </tr></thead>
        <tbody id="pdResultsBody"><tr><td colspan="9" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>`;

  $('#pdExport', panel).addEventListener('click', exportPdeltaExcel);
  if (pdeltaState.lastResult) renderPdeltaResultsTable(pdeltaState.lastResult);
}

function renderPdeltaResultsTable(result) {
  const body = $('#pdResultsBody');
  if (!body) return;
  body.innerHTML = result.items.length
    ? result.items.map(item => `
        <tr>
          <td>${item.story}</td><td>${item.loadCase}</td><td>${item.direction}</td>
          <td>${item.vi.toFixed(2)}</td><td>${item.wij.toFixed(2)}</td><td>${item.driftRatio.toFixed(6)}</td>
          <td>${item.theta.toFixed(6)}</td><td>${item.limit.toFixed(4)}</td><td>${item.ok ? '✅' : '❌'}</td>
        </tr>`).join('')
    : `<tr><td colspan="9" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  const banner = $('#pdStatusBanner');
  banner.textContent = t(result.allOk ? 'pdelta.status.passed' : 'pdelta.status.failed');
  banner.className = `status-banner ${result.allOk ? 'ok' : 'fail'}`;
  const exportBtn = $('#pdExport');
  if (exportBtn) exportBtn.disabled = false;

  const { ch, r, d } = pdeltaState.params;
  const limit = result.items.length ? result.items[0].limit : 0;
  const worst = result.items.reduce((a, b) => (b.theta > a.theta ? b : a), result.items[0]);
  renderCalcBasis('#pdResultsBody', 'pdBasis', calcBasis(
    'TBDY 2018 §4.9.2 (Denk. 4.34)',
    [
      'θi = (Δi · Σ Wj) / (Vi · hi)',
      t('pdelta.basis.driftNote'),
      'θi ≤ 0.12 · D / (Ch · R)'
    ],
    [
      ['Ch / R / D', `${ch} / ${r} / ${d}`],
      [t('pdelta.basis.limitFormula'), `0.12 · ${d} / (${ch} · ${r}) = <strong>${limit.toFixed(5)}</strong>`],
      ...(worst ? [[t('pdelta.basis.worst'),
        `${worst.story} / ${worst.direction} / ${worst.loadCase}`],
        ['Σ Wj', `${worst.wij.toFixed(2)} kN`],
        ['Vi', `${worst.vi.toFixed(2)} kN`],
        ['Δi/hi', `${worst.driftRatio.toFixed(6)}`],
        ['θi', `${worst.driftRatio.toFixed(6)} · ${worst.wij.toFixed(2)} / ${worst.vi.toFixed(2)} = <strong>${worst.theta.toFixed(6)}</strong> ` +
          `(${limit ? (worst.theta / limit * 100).toFixed(1) : '—'}% ${t('pdelta.basis.ofLimit')})`]] : [])
    ]));
}

async function runPdeltaCheck() {
  const btn = $('#pdCalculate');
  if (pdeltaState.selected.length === 0) {
    log(t('drift.error.noCombos'), 'error');
    return;
  }

  const { ch, r, d } = pdeltaState.params;
  const validated = validateFields([
    { id: 'pdCh', ok: inRange(ch, 0.1, 3), message: t('validate.range', { field: 'Ch', min: 0.1, max: 3, value: ch }) },
    { id: 'pdR', ok: inRange(r, 1, 10), message: t('validate.range', { field: 'R', min: 1, max: 10, value: r }) },
    { id: 'pdD', ok: inRange(d, 1, 4), message: t('validate.range', { field: 'D', min: 1, max: 4, value: d }) },
    { id: 'pdD', ok: !(d > 0 && r > 0 && d > r), message: t('validate.dOverR', { d, r }) }
  ], $('#setupPanel'));
  if (!validated) return;

  btn.disabled = true;
  try {
    const result = await pdeltaFetchAndCompute();
    if (result.items.length === 0) throw new Error(t('drift.error.noData'));
    pdeltaState.lastResult = result;
    renderPdeltaResultsTable(result);
    recordLastCheck('pdelta');
    log(t(result.allOk ? 'pdelta.status.passed' : 'pdelta.status.failed'), result.allOk ? 'ok' : 'error');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

async function exportPdeltaExcel() {
  const result = pdeltaState.lastResult;
  if (!result) return;
  const btn = $('#pdExport');
  if (btn) btn.disabled = true;
  try {
    await downloadAgentExcel('/api/etabs/export/pdelta', {
      ch: pdeltaState.params.ch, r: pdeltaState.params.r, d: pdeltaState.params.d,
      rows: result.items.map(item => ({ story: item.story, combo: item.loadCase, direction: item.direction, vi: item.vi, wij: item.wij, driftRatio: item.driftRatio }))
    }, 'IkinciMertebe_Sonuc.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Design Spectrum (Tasarım Spektrumu) — ported from SpectrumManager (C#), TBDY 2018.
// Pure client-side math; result is shared with the Increment module.
// ---------------------------------------------------------------------------

function loadSpectrumState() {
  try {
    return JSON.parse(localStorage.getItem('sea-spectrum')) || { sds: 0, sd1: 0, r: 8, d: 3, i: 1, periods: [], accelerations: [] };
  } catch {
    return { sds: 0, sd1: 0, r: 8, d: 3, i: 1, periods: [], accelerations: [] };
  }
}

const spectrumState = loadSpectrumState();

function spectrumCompute(sds, sd1, r, d, i) {
  const ta = 0.2 * sd1 / sds;
  const tb = sd1 / sds;
  const round3 = x => Math.round(x * 1000) / 1000;
  const periods = [0, ta / 3, ta / 2, ta];
  for (let t = ta + 0.01; t <= tb; t += 0.01) periods.push(round3(t));
  periods.push(tb);
  for (let t = tb + 0.05; t <= 8.0; t += 0.05) periods.push(round3(t));
  const uniqueSorted = [...new Set(periods)].sort((a, b) => a - b);
  const accelerations = uniqueSorted.map(T => {
    const se = T <= ta ? sds * (0.4 + 0.6 * T / ta)
      : T <= tb ? sds
      : T <= 6.0 ? sd1 / T
      : sd1 * 6 / (T * T);
    const reff = T <= tb ? d + ((r / i) - d) * (T / tb) : r / i;
    return 9.81 * (se / reff);
  });
  return { ta, tb, periods: uniqueSorted, accelerations };
}

// SaR interpolated at a given period (shared with the Increment module).
function spectrumSaAt(period) {
  const { periods, accelerations } = spectrumState;
  if (!periods.length) return 0;
  for (let i = 0; i < periods.length; i++)
    if (Math.abs(periods[i] - period) < 0.0001) return accelerations[i];
  for (let i = 0; i < periods.length - 1; i++)
    if (period >= periods[i] && period <= periods[i + 1]) {
      const t1 = periods[i], t2 = periods[i + 1], a1 = accelerations[i], a2 = accelerations[i + 1];
      return a1 + (a2 - a1) * (period - t1) / (t2 - t1);
    }
  return accelerations[accelerations.length - 1];
}

function renderSpectrumModule() {
  renderSpectrumSetupPanel();
  renderSpectrumResultsPanel();
}

function renderSpectrumSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('spectrum.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('spSds', 'spectrum.params.sds', { min: 0 })}
      ${numberField('spSd1', 'spectrum.params.sd1', { min: 0 })}
      ${numberField('spR', 'spectrum.params.r', { min: 1, max: 10 })}
      ${numberField('spD', 'spectrum.params.d', { min: 1, max: 4 })}
      ${numberField('spI', 'spectrum.params.i', { min: 1, max: 1.5 })}
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="spCalculate">${t('drift.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="spReset" style="margin-top:8px">${t('action.reset')}</button>
      <button class="button button-secondary full-width" type="button" id="spDownload" style="margin-top:8px" ${spectrumState.periods.length ? '' : 'disabled'}>${t('spectrum.download')}</button>
    </div>`;

  const bind = (id, key) => {
    const el = $('#' + id, panel);
    el.value = spectrumState[key];
    el.addEventListener('input', () => { spectrumState[key] = parseFloat(el.value) || 0; });
  };
  bind('spSds', 'sds');
  bind('spSd1', 'sd1');
  bind('spR', 'r');
  bind('spD', 'd');
  bind('spI', 'i');

  $('#spCalculate', panel).addEventListener('click', runSpectrumCalc);
  $('#spReset', panel).addEventListener('click', () => resetModule(spectrumState, renderSpectrumModule, { periods: [], accelerations: [] }));
  $('#spDownload', panel).addEventListener('click', downloadSpectrumTxt);
}

function renderSpectrumResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('spectrum.chart.title')}</h2><p>${t('spectrum.chart.subtitle')}</p></div></div></div>
    <div class="spectrum-summary" id="spSummary"></div>
    <div class="spectrum-chart" id="spChart">${spectrumState.periods.length ? spectrumChartSvg() : `<p class="table-empty">${t('spectrum.status.pending')}</p>`}</div>`;
  if (spectrumState.periods.length) renderSpectrumSummary();
}

function spectrumChartSvg() {
  const { periods, accelerations } = spectrumState;
  const W = 560, H = 320, padL = 46, padR = 16, padT = 16, padB = 36;
  const xMax = 6, yMax = Math.max(1, Math.ceil(Math.max(...accelerations)));
  const px = t => padL + (Math.min(t, xMax) / xMax) * (W - padL - padR);
  const py = a => H - padB - (a / yMax) * (H - padT - padB);
  const pts = periods.filter(t => t <= xMax).map((t, i) => `${px(t).toFixed(1)},${py(accelerations[i]).toFixed(1)}`).join(' ');

  const xTicks = [];
  for (let x = 0; x <= xMax; x++)
    xTicks.push(`<line x1="${px(x)}" y1="${padT}" x2="${px(x)}" y2="${H - padB}" class="grid"/><text x="${px(x)}" y="${H - padB + 16}" class="axl" text-anchor="middle">${x}</text>`);
  const yTicks = [];
  for (let y = 0; y <= yMax; y++)
    yTicks.push(`<line x1="${padL}" y1="${py(y)}" x2="${W - padR}" y2="${py(y)}" class="grid"/><text x="${padL - 6}" y="${py(y) + 4}" class="axl" text-anchor="end">${y}</text>`);

  return `<svg viewBox="0 0 ${W} ${H}" class="spectrum-svg" role="img" aria-label="${t('spectrum.chart.title')}">
    ${yTicks.join('')}${xTicks.join('')}
    <polyline points="${pts}" class="spectrum-line"/>
    <text x="${padL + (W - padL - padR) / 2}" y="${H - 4}" class="axt" text-anchor="middle">${t('spectrum.chart.x')}</text>
    <text x="14" y="${padT + (H - padT - padB) / 2}" class="axt" text-anchor="middle" transform="rotate(-90 14 ${padT + (H - padT - padB) / 2})">${t('spectrum.chart.y')}</text>
  </svg>`;
}

function renderSpectrumSummary() {
  const el = $('#spSummary');
  if (!el) return;
  const { sds, sd1, r, d, i, periods, accelerations } = spectrumState;
  const ta = 0.2 * sd1 / sds, tb = sd1 / sds;
  const peak = Math.max(...accelerations);
  const peakIdx = accelerations.indexOf(peak);
  const peakT = periods[peakIdx];

  // Re-derive the peak point term by term so the engineer can check the headline
  // number against the equations shown right below it.
  const peakSae = peakT <= ta ? sds * (0.4 + 0.6 * peakT / ta)
    : peakT <= tb ? sds
    : peakT <= 6 ? sd1 / peakT
    : 6 * sd1 / (peakT * peakT);
  const peakRa = peakT <= tb ? d + ((r / i) - d) * (peakT / tb) : r / i;

  el.innerHTML = `
    <span><small>TA</small><strong>${ta.toFixed(3)} s</strong></span>
    <span><small>TB</small><strong>${tb.toFixed(3)} s</strong></span>
    <span><small>${t('spectrum.summary.peak')}</small><strong>${peak.toFixed(3)} m/s²</strong></span>
    <span><small>${t('spectrum.summary.points')}</small><strong>${accelerations.length}</strong></span>`;

  const basis = calcBasis(
    'TBDY 2018 §2.3.4 (Sae), §4.3.4 / Denk. 4.1 (Ra), §2.3.8',
    [
      'T ≤ TA  :  Sae(T) = SDS · (0.4 + 0.6·T/TA)',
      'TA &lt; T ≤ TB  :  Sae(T) = SDS',
      'TB &lt; T ≤ 6 s  :  Sae(T) = SD1 / T',
      'T &gt; 6 s  :  Sae(T) = 6·SD1 / T²',
      'TA = 0.2·SD1/SDS ,  TB = SD1/SDS',
      'T ≤ TB  :  Ra(T) = D + (R/I − D)·T/TB',
      'T &gt; TB  :  Ra(T) = R / I',
      'SaR(T) = g · Sae(T) / Ra(T)   ,  g = 9.81 m/s²'
    ],
    [
      ['SDS / SD1', `${sds} g / ${sd1} g`],
      ['R / I / D', `${r} / ${i} / ${d}`],
      ['TA = 0.2·SD1/SDS', `0.2 · ${sd1} / ${sds} = ${ta.toFixed(4)} s`],
      ['TB = SD1/SDS', `${sd1} / ${sds} = ${tb.toFixed(4)} s`],
      [t('spectrum.basis.peakAt'), `T = ${peakT.toFixed(3)} s`],
      ['Sae(T)', `${peakSae.toFixed(4)} g`],
      ['Ra(T)', `${peakRa.toFixed(4)}`],
      [t('spectrum.basis.peakCheck'), `9.81 · ${peakSae.toFixed(4)} / ${peakRa.toFixed(4)} = <strong>${peak.toFixed(3)} m/s²</strong>`]
    ]);

  const chartWrap = $('#spChart');
  if (chartWrap) {
    const existing = $('#spBasis');
    if (existing) existing.remove();
    chartWrap.insertAdjacentHTML('afterend', `<div id="spBasis">${basis}</div>`);
  }
}

function runSpectrumCalc() {
  const { sds, sd1, r, d, i } = spectrumState;
  const tb = sds > 0 ? sd1 / sds : 0;
  const ok = validateFields([
    { id: 'spSds', ok: inRange(sds, 0.05, 4), message: t('validate.range', { field: 'SDS', min: '0.05 g', max: '4 g', value: sds }) },
    { id: 'spSd1', ok: inRange(sd1, 0.02, 3), message: t('validate.range', { field: 'SD1', min: '0.02 g', max: '3 g', value: sd1 }) },
    { id: 'spR', ok: inRange(r, 1, 10), message: t('validate.range', { field: 'R', min: 1, max: 10, value: r }) },
    { id: 'spD', ok: inRange(d, 1, 4), message: t('validate.range', { field: 'D', min: 1, max: 4, value: d }) },
    { id: 'spI', ok: inRange(i, 1, 1.5), message: t('validate.range', { field: 'I', min: 1, max: 1.5, value: i }) },
    // TBDY 2018 Table 4.1 never pairs an overstrength factor above the behaviour factor.
    { id: 'spD', ok: !(d > 0 && r > 0 && d > r), message: t('validate.dOverR', { d, r }) },
    // A wild TB almost always means SDS/SD1 came from different hazard levels or wrong units.
    { id: 'spSd1', ok: !(sds > 0 && sd1 > 0) || inRange(tb, 0.1, 1.5), message: t('validate.cornerPeriod', { tb: tb.toFixed(3) }) }
  ], $('#setupPanel'));
  if (!ok) return;

  const result = spectrumCompute(sds, sd1, r, d, i);
  spectrumState.periods = result.periods;
  spectrumState.accelerations = result.accelerations;
  try { localStorage.setItem('sea-spectrum', JSON.stringify(spectrumState)); } catch { /* quota */ }

  const chart = $('#spChart');
  if (chart) chart.innerHTML = spectrumChartSvg();
  renderSpectrumSummary();
  const dl = $('#spDownload');
  if (dl) dl.disabled = false;
  recordLastCheck('spectrum');
  log(t('spectrum.status.done'), 'ok');
}

function downloadSpectrumTxt() {
  const { periods, accelerations, r, d, i } = spectrumState;
  if (!periods.length) return;
  const lines = periods.map((tp, idx) => `${tp.toFixed(3)}\t${accelerations[idx].toFixed(4)}`);
  const blob = new Blob([lines.join('\r\n')], { type: 'text/plain;charset=utf-8;' });
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = `R${r}_D${d}_I${i}.txt`;
  a.click();
  URL.revokeObjectURL(url);
}

// ---------------------------------------------------------------------------
// Base Shear Amplification (Taban Kesme Kuvveti Büyütmesi) — ported from ArtirimHesabiUI (C#).
// β = 0.9 · max(SaR(T)·mt, 0.04·SDS·g·I·mt) / Vt ; Tmax = Hn^0.75 · Ct · 1.4
// Depends on the Design Spectrum module's shared spectrumState (SDS, I, SaR curve).
// ---------------------------------------------------------------------------

const incrementState = {
  ...rigidStateDefaults(),
  mt: 0, hn: 0, ct: 0.07,
  bodrum: false, bodrumKat: 0,
  combos: [], selected: [],
  tx: 0, vtX: 0, ty: 0, vtY: 0,
  modalTopX: [], modalTopY: [],
  resultX: null, resultY: null
};

// Pulls mass and both modal periods without the user asking. Failures are logged by
// the individual fetchers; nothing here should block the module from rendering.
async function incrementAutoFetchModelData() {
  try { await incrementFetchMass(true); } catch { /* reported by the fetcher */ }
  try { await incrementFetchPeriod('X', true); } catch { /* reported by the fetcher */ }
  try { await incrementFetchPeriod('Y', true); } catch { /* reported by the fetcher */ }
  await incrementAutoFetchVt();
}

// Vt needs a combination naming the direction, so this is a no-op until one is picked.
async function incrementAutoFetchVt() {
  for (const dir of ['X', 'Y']) {
    const hasCombo = incrementState.selected.some(c => c.toUpperCase().includes(dir));
    if (!hasCombo) continue;
    try { await incrementFetchVt(dir, true); } catch { /* reported by the fetcher */ }
  }
}

async function incrementFetchMass(silent = false) {
  const btn = null;
  try {
    const [massRes, storiesRes] = await Promise.all([
      fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Mass Summary by Story')}`),
      fetchAgentJson('/api/etabs/stories')
    ]);
    if (!massRes.etabsConnected) throw new Error(massRes.error || t('drift.error.notConnected'));

    const excluded = new Set(['base']);
    if (incrementState.bodrum && incrementState.bodrumKat > 0) {
      const basement = determineBasementStories(storiesRes.stories || [], incrementState.bodrumKat);
      basement.forEach(name => excluded.add(name.toLowerCase()));
    }

    const f = massRes.fields;
    const sIdx = tableIndex(f, 'Story'), uxIdx = tableIndex(f, 'UX', 'MassX');
    let total = 0;
    for (const row of massRes.rows) {
      const story = row[sIdx];
      if (!story || excluded.has(story.toLowerCase())) continue;
      total += parseFloat(row[uxIdx]) || 0;
    }
    incrementState.mt = total;
    $('#incMt').value = total.toFixed(2);
    if (!silent) log(t('increment.status.massFetched'), 'ok');
  } catch (error) {
    if (!silent) log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

async function incrementFetchPeriod(direction, silent = false) {
  const btn = null;
  try {
    const res = await fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Modal Participating Mass Ratios')}`);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));

    const f = res.fields;
    const caseIdx = tableIndex(f, 'Case', 'OutputCase');
    const modeIdx = tableIndex(f, 'Mode', 'StepNum');
    const periodIdx = tableIndex(f, 'Period');
    const uIdx = tableIndex(f, direction === 'X' ? 'UX' : 'UY');

    const modal = res.rows
      .filter(row => (row[caseIdx] || '').toLowerCase() === 'modal-ust')
      .map(row => ({ mode: row[modeIdx], period: parseFloat(row[periodIdx]) || 0, ratio: parseFloat(row[uIdx]) || 0 }));

    if (modal.length === 0) throw new Error(t('increment.error.noModal'));

    // Dedupe by mode (defensive; matches the desktop's GroupBy(Mode).First()), then take the top 2 by ratio.
    const byMode = new Map();
    for (const m of modal) if (!byMode.has(m.mode)) byMode.set(m.mode, m);
    const top2 = [...byMode.values()].sort((a, b) => b.ratio - a.ratio).slice(0, 2);
    const best = top2[0];

    if (direction === 'X') {
      incrementState.tx = best.period;
      incrementState.modalTopX = top2;
      $('#incTx').value = best.period.toFixed(3);
    } else {
      incrementState.ty = best.period;
      incrementState.modalTopY = top2;
      $('#incTy').value = best.period.toFixed(3);
    }
    incrementRenderModalInfo(direction);
    if (!silent) log(t(direction === 'X' ? 'increment.status.periodFetchedX' : 'increment.status.periodFetchedY'), 'ok');
  } catch (error) {
    if (!silent) log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function incrementRenderModalInfo(direction) {
  const el = $(direction === 'X' ? '#incModalInfoX' : '#incModalInfoY');
  if (!el) return;
  const top = direction === 'X' ? incrementState.modalTopX : incrementState.modalTopY;
  const col = direction === 'X' ? 'UX' : 'UY';
  el.textContent = top.map(m => `${t('increment.modal.mode')} ${m.mode}: T=${m.period.toFixed(3)}s, ${col}=${m.ratio.toFixed(4)}`).join(' · ');
}

async function incrementFetchVt(direction, silent = false) {
  const btn = null;
  const dirFilter = direction === 'X' ? 'X' : 'Y';
  // The target story is counted from the bottom, so with the rigid rule on it sits in
  // the basement and must be read from the basement combination list.
  const targetIsBasement = rigidIsActive(incrementState);
  const pool = targetIsBasement ? incrementState.basementCombos : incrementState.selected;
  const matchingCombo = pool.find(c => c.toUpperCase().includes(dirFilter))
    || incrementState.selected.find(c => c.toUpperCase().includes(dirFilter));
  if (!matchingCombo) {
    if (!silent) log(t('increment.error.noComboForDirection', { direction }), 'error');
    return;
  }

  try {
    const res = await fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Story Forces')}&combos=${encodeURIComponent(matchingCombo)}`);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));

    const f = res.fields;
    const sIdx = tableIndex(f, 'Story'), caseIdx = tableIndex(f, 'OutputCase', 'LoadCase', 'Case');
    const locIdx = tableIndex(f, 'Location'), vxIdx = tableIndex(f, 'VX'), vyIdx = tableIndex(f, 'VY');

    const perStory = [];
    for (const row of res.rows) {
      const caseVal = row[caseIdx] || '';
      const loc = row[locIdx] || '';
      const caseMatch = caseVal === matchingCombo || caseVal.toUpperCase().includes(matchingCombo.toUpperCase());
      if (caseMatch && loc.toLowerCase() === 'bottom') {
        perStory.push({ story: row[sIdx], vx: parseFloat(row[vxIdx]) || 0, vy: parseFloat(row[vyIdx]) || 0 });
      }
    }
    if (perStory.length === 0) throw new Error(t('increment.error.noStoryForces', { combo: matchingCombo }));

    perStory.reverse(); // table order is top->bottom; desktop reverses to bottom->top
    const bodrumKat = incrementState.bodrum ? incrementState.bodrumKat : 0;
    const targetRow = Math.min(bodrumKat, perStory.length - 1);
    const target = perStory[targetRow];
    const vt = Math.abs(direction === 'X' ? target.vx : target.vy);

    if (direction === 'X') { incrementState.vtX = vt; $('#incVtX').value = vt.toFixed(2); }
    else { incrementState.vtY = vt; $('#incVtY').value = vt.toFixed(2); }
    if (!silent) log(t(direction === 'X' ? 'increment.status.vtFetchedX' : 'increment.status.vtFetchedY'), 'ok');
  } catch (error) {
    if (!silent) log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function incrementCalculate(direction) {
  if (spectrumState.periods.length === 0) {
    log(t('increment.error.noSpectrum'), 'error');
    return;
  }

  const mt = incrementState.mt;
  const t0 = direction === 'X' ? incrementState.tx : incrementState.ty;
  const vt = direction === 'X' ? incrementState.vtX : incrementState.vtY;
  const periodId = direction === 'X' ? 'incTx' : 'incTy';
  const shearId = direction === 'X' ? 'incVtX' : 'incVtY';
  const hn = incrementState.hn, ct = incrementState.ct;

  if (!validateFields([
    { id: 'incMt', ok: mt > 0, message: t('validate.zeroMass') },
    { id: shearId, ok: vt > 0, message: t('validate.zeroShear') },
    { id: periodId, ok: inRange(t0, 0.05, 10), message: t('validate.range', { field: `T (${direction})`, min: '0.05 s', max: '10 s', value: t0 }) },
    { id: 'incHn', ok: hn === 0 || inRange(hn, 1.5, 500), message: t('validate.storyHeight', { value: hn }) }
  ], $('#setupPanel'))) return;

  let period = t0, warning = '';
  if (hn > 0 && ct > 0) {
    const tMax = Math.pow(hn, 0.75) * ct * 1.4;
    if (period > tMax) {
      warning = t('increment.warning.periodCapped', { period: period.toFixed(3), tMax: tMax.toFixed(3) });
      period = tMax;
    }
  }

  const g = 9.81;
  const sae = spectrumSaAt(period);
  const sds = spectrumState.sds, i = spectrumState.i;
  const wt = sae * mt;
  const vtMax = 0.04 * sds * g * i * mt;
  const vtHesap = Math.max(wt, vtMax);
  const beta = 0.9 * vtHesap / vt;

  const result = { period, warning, sae, wt, vtMax, beta };
  if (direction === 'X') incrementState.resultX = result; else incrementState.resultY = result;
  incrementRenderResult(direction);
  recordLastCheck('increment');
  log(t('increment.status.calculated', { direction }), 'ok');
}

function incrementRenderResult(direction) {
  const el = $(direction === 'X' ? '#incResultX' : '#incResultY');
  if (!el) return;
  const result = direction === 'X' ? incrementState.resultX : incrementState.resultY;
  if (!result) { el.textContent = t('increment.status.pending'); return; }
  el.innerHTML = `
    <p>${t('increment.result.period', { direction })}: <strong>${result.period.toFixed(3)} s</strong></p>
    ${result.warning ? `<p class="increment-warning">${result.warning}</p>` : ''}
    <p>SAE: <strong>${result.sae.toFixed(4)} m/s²</strong></p>
    <p>Wt: <strong>${result.wt.toFixed(2)} kN</strong></p>
    <p>VTmax: <strong>${result.vtMax.toFixed(2)} kN</strong></p>
    <p class="increment-beta">${t('increment.result.beta')}: <strong>${result.beta.toFixed(3)}</strong></p>`;

  const vt = direction === 'X' ? incrementState.vtX : incrementState.vtY;
  const { mt, hn, ct } = incrementState;
  el.insertAdjacentHTML('beforeend', calcBasis(
    'TBDY 2018 §4.7.3 (Denk. 4.19), Tablo 4.3 / §4.7.2',
    [
      'Tp,max = 1.4 · Ct · Hn^0.75',
      'Wt  = SaR(T) · mt',
      'VTmax = 0.04 · SDS · g · I · mt',
      'VT,hesap = max(Wt , VTmax)',
      'β = 0.9 · VT,hesap / VT,analiz'
    ],
    [
      ['mt', `${mt} t`],
      ...(hn > 0 && ct > 0 ? [['Tp,max', `1.4 · ${ct} · ${hn}^0.75 = ${(Math.pow(hn, 0.75) * ct * 1.4).toFixed(3)} s`]] : []),
      [t('increment.result.period', { direction }), `${result.period.toFixed(3)} s${result.warning ? ' ⚠' : ''}`],
      ['SaR(T)', `${result.sae.toFixed(4)} m/s²`],
      ['Wt', `${result.sae.toFixed(4)} · ${mt} = ${result.wt.toFixed(2)} kN`],
      ['VTmax', `0.04 · ${spectrumState.sds} · 9.81 · ${spectrumState.i} · ${mt} = ${result.vtMax.toFixed(2)} kN`],
      ['VT,analiz', `${vt} kN`],
      ['β', `0.9 · ${Math.max(result.wt, result.vtMax).toFixed(2)} / ${vt} = <strong>${result.beta.toFixed(3)}</strong>`]
    ]));
}

function renderIncrementModule() {
  renderIncrementSetupPanel();
  renderIncrementResultsPanel();
}

function renderIncrementSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('increment.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${checkboxField('incBodrum', 'drift.params.basement')}
      ${numberField('incBodrumKat', 'drift.params.basementCount', { step: 1, min: 0, max: 10 })}
      ${numberField('incMt', 'increment.params.mt', { min: 0 })}
      ${numberField('incHn', 'increment.params.hn', { min: 0 })}
      ${numberField('incCt', 'increment.params.ct', { min: 0 })}
    </div>
    ${comboPicker('inc', 'increment.combos.hint')}
    ${rigidSection('inc', incrementState)}
    <div class="increment-direction">
      <h3 class="increment-direction-title x">${t('increment.direction.x')}</h3>
      <div class="field-grid two">
        ${numberField('incTx', 'increment.params.tx', { min: 0, unit: 's' })}
        ${numberField('incVtX', 'increment.params.vtx', { min: 0, unit: 'kN' })}
      </div>
      <p class="increment-modal-info" id="incModalInfoX"></p>
      <button class="button button-primary full-width" type="button" id="incCalcX">${t('increment.calculate', { direction: 'X' })}</button>
    </div>
    <div class="increment-direction">
      <h3 class="increment-direction-title y">${t('increment.direction.y')}</h3>
      <div class="field-grid two">
        ${numberField('incTy', 'increment.params.ty', { min: 0, unit: 's' })}
        ${numberField('incVtY', 'increment.params.vty', { min: 0, unit: 'kN' })}
      </div>
      <p class="increment-modal-info" id="incModalInfoY"></p>
      <button class="button button-primary full-width" type="button" id="incCalcY">${t('increment.calculate', { direction: 'Y' })}</button>
    </div>
    <div class="panel-actions">
      <button class="button button-secondary full-width" type="button" id="incReset">${t('action.reset')}</button>
    </div>`;

  const bind = (id, key, isInt = false) => {
    const el = $('#' + id, panel);
    el.value = incrementState[key];
    el.addEventListener('input', () => { incrementState[key] = (isInt ? parseInt(el.value, 10) : parseFloat(el.value)) || 0; });
  };
  bind('incMt', 'mt');
  bind('incHn', 'hn');
  bind('incCt', 'ct');
  bind('incBodrumKat', 'bodrumKat', true);

  const bodrum = $('#incBodrum', panel);
  const bodrumKat = $('#incBodrumKat', panel);
  bodrum.checked = incrementState.bodrum;
  bodrumKat.disabled = !bodrum.checked;
  bodrum.addEventListener('change', () => {
    incrementState.bodrum = bodrum.checked;
    bodrumKat.disabled = !bodrum.checked;
  });

  // Mass and modal periods come straight from the model when the module opens.
  // Vt additionally needs a direction-matching combination, so it is (re)read
  // whenever the selection changes as well as once on open.
  initComboPicker('inc', incrementState, incrementAutoFetchVt);
  bindSetupSections(panel);
  initRigidSection('inc', incrementState, () => incrementState.combos);
  incrementAutoFetchModelData();

  $('#incCalcX', panel).addEventListener('click', () => incrementCalculate('X'));
  $('#incCalcY', panel).addEventListener('click', () => incrementCalculate('Y'));
  $('#incReset', panel).addEventListener('click', () => resetModule(incrementState, renderIncrementModule, { resultX: null, resultY: null }));

  incrementRenderModalInfo('X');
  incrementRenderModalInfo('Y');
}

function renderIncrementResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div></div>
    <div class="increment-results">
      <div class="increment-result-block">
        <h3 class="increment-direction-title x">${t('increment.direction.x')}</h3>
        <div id="incResultX">${t('increment.status.pending')}</div>
      </div>
      <div class="increment-result-block">
        <h3 class="increment-direction-title y">${t('increment.direction.y')}</h3>
        <div id="incResultY">${t('increment.status.pending')}</div>
      </div>
    </div>`;
  if (incrementState.resultX) incrementRenderResult('X');
  if (incrementState.resultY) incrementRenderResult('Y');
}

// ---------------------------------------------------------------------------
// Column Axial Load (Kolon Eksenel Yük) — ported from KolonEksenelYukManager (C#).
// Ratio = |P| / (Ac·fck) ; joins Element Forces (Station~0, StepType=Min) to Frame Assignments
// (Type=Column) by Story+Label. First module with a write-back (select failing columns in the
// model) and a formula-backed Excel export.
// ---------------------------------------------------------------------------

const columnAxialState = {
  ...rigidStateDefaults(),
  fck: 30, limit: 0.40, bodrum: false, bodrumKat: 0,
  combos: [], selected: [],
  frameAssignments: [], columnForces: [], stories: [],
  lastResults: []
};

function extractStoryNumber(name) {
  const m = String(name || '').match(/\d+/);
  return m ? parseInt(m[0], 10) : null;
}

function columnAxialBasementStories(stories, isBodrum, bodrumKat) {
  const set = new Set();
  if (!isBodrum || bodrumKat <= 0 || stories.length === 0) return set;
  const hasValidElevations = stories.some(s => Math.abs(s.elevation) > 0.001);
  const sorted = hasValidElevations
    ? [...stories].sort((a, b) => a.elevation - b.elevation)
    : [...stories].sort((a, b) => (extractStoryNumber(a.name) ?? 0) - (extractStoryNumber(b.name) ?? 0));
  for (let i = 0; i < bodrumKat && i < sorted.length; i++) set.add(sorted[i].name.toLowerCase());
  return set;
}

function parseSectionDims(sectionName) {
  const m = String(sectionName || '').match(/(\d+(?:[.,]\d+)?)\s*[xX*]\s*(\d+(?:[.,]\d+)?)/);
  if (!m) return null;
  return { b: parseFloat(m[1].replace(',', '.')), d: parseFloat(m[2].replace(',', '.')) };
}

// Mirrors KolonEksenelYukManager.Calculate 1:1, including the letter-based (not word-based)
// AH/UH combo filter: it checks whether the load case name contains "A" or "U" anywhere.
function columnAxialCalculate(forces, assignments, stories, fck, limit, isBodrum, bodrumKat) {
  const bodrumStories = columnAxialBasementStories(stories, isBodrum, bodrumKat);

  const assignDict = new Map();
  for (const a of assignments) {
    const key = `${a.story}||${a.label}`.toLowerCase();
    if (!assignDict.has(key)) assignDict.set(key, a);
  }

  const results = [];
  for (const force of forces) {
    const key = `${force.story}||${force.column}`.toLowerCase();
    const frame = assignDict.get(key);
    if (!frame) continue;

    const dims = parseSectionDims(frame.section);
    const bCm = dims ? dims.b : 0;
    const dCm = dims ? dims.d : 0;
    const acCm2 = bCm * dCm;
    const acMm2 = acCm2 * 100;

    const isCurrentStoryBodrum = bodrumStories.has(force.story.toLowerCase());
    const loadCaseUpper = force.loadCase.toUpperCase();
    const hasA = loadCaseUpper.includes('A');
    const hasU = loadCaseUpper.includes('U');
    let hideRow = false;
    if (isBodrum) {
      if (isCurrentStoryBodrum) { if (hasU) hideRow = true; }
      else if (hasA) hideRow = true;
    } else if (hasA) hideRow = true;
    if (hideRow) continue;

    const absNd = Math.abs(force.p);
    const acFckKN = (acMm2 * fck) / 1000;
    const ratio = acFckKN > 0 ? absNd / acFckKN : 0;

    results.push({
      story: force.story, column: force.column,
      uniqueName: force.uniqueName || frame.uniqueName || '',
      loadCase: force.loadCase, location: force.location,
      nd: absNd, section: frame.section, b: bCm, d: dCm,
      ac: acCm2, acFck: acFckKN, limit, fck,
      ndRatio: ratio, isOk: ratio <= limit
    });
  }

  results.sort((x, y) => y.story.localeCompare(x.story) || x.column.localeCompare(y.column) || x.loadCase.localeCompare(y.loadCase));
  return results;
}

function renderColumnAxialModule() {
  renderColumnAxialSetupPanel();
  renderColumnAxialResultsPanel();
}

function renderColumnAxialSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('columnAxial.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('caFck', 'columnAxial.params.fck', { min: 10, max: 90 })}
      ${numberField('caLimit', 'columnAxial.params.limit', { min: 0.1, max: 1 })}
      ${checkboxField('caBodrum', 'drift.params.basement')}
      ${numberField('caBodrumKat', 'drift.params.basementCount', { step: 1, min: 0, max: 10 })}
    </div>
    ${comboPicker('ca', 'columnAxial.combos.hint')}
    ${rigidSection('ca', columnAxialState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="caCalculate">${t('columnAxial.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="caReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>
    <div class="panel-actions two-up">
      <button class="button button-secondary" type="button" id="caSelectFailing">${t('columnAxial.selectFailing')}</button>
      <button class="button button-secondary" type="button" id="caExport">${t('columnAxial.export')}</button>
    </div>`;

  const bind = (id, key, isInt = false) => {
    const el = $('#' + id, panel);
    el.value = columnAxialState[key];
    el.addEventListener('input', () => { columnAxialState[key] = (isInt ? parseInt(el.value, 10) : parseFloat(el.value)) || 0; });
  };
  bind('caFck', 'fck');
  bind('caLimit', 'limit');
  bind('caBodrumKat', 'bodrumKat', true);

  const bodrum = $('#caBodrum', panel);
  const bodrumKat = $('#caBodrumKat', panel);
  bodrum.checked = columnAxialState.bodrum;
  bodrumKat.disabled = !bodrum.checked;
  bodrum.addEventListener('change', () => {
    columnAxialState.bodrum = bodrum.checked;
    bodrumKat.disabled = !bodrum.checked;
  });

  initComboPicker('ca', columnAxialState);
  bindSetupSections(panel);
  initRigidSection('ca', columnAxialState, () => columnAxialState.combos);
  $('#caCalculate', panel).addEventListener('click', runColumnAxialCheck);
  $('#caReset', panel).addEventListener('click', () => resetModule(columnAxialState, renderColumnAxialModule));
  $('#caSelectFailing', panel).addEventListener('click', columnAxialSelectFailing);
  $('#caExport', panel).addEventListener('click', columnAxialExportExcel);
}

function renderColumnAxialResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div></div>
    <div class="status-banner pending" id="caStatusBanner">${t('columnAxial.status.pending')}</div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('drift.table.story')}</th><th>${t('columnAxial.table.column')}</th><th>${t('drift.table.combo')}</th>
          <th>${t('columnAxial.table.location')}</th><th>${t('columnAxial.table.p')}</th><th>${t('columnAxial.table.section')}</th>
          <th>${t('columnAxial.table.b')}</th><th>${t('columnAxial.table.d')}</th>
          <th>${t('columnAxial.table.ac')}</th><th>${t('columnAxial.table.acFck')}</th>
          <th>${t('columnAxial.table.ratio')}</th><th>${t('drift.table.limit')}</th><th>${t('drift.table.status')}</th>
        </tr></thead>
        <tbody id="caResultsBody"><tr><td colspan="13" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>
    <div class="column-axial-failed">
      <h3>${t('columnAxial.failed.title')}</h3>
      <p id="caFailedList">${t('columnAxial.failed.none')}</p>
    </div>`;
  if (columnAxialState.lastResults.length) renderColumnAxialResultsTable();
}

function renderColumnAxialResultsTable() {
  const body = $('#caResultsBody');
  if (!body) return;
  const results = columnAxialState.lastResults;
  body.innerHTML = results.length
    ? results.map((item, i) => `
        <tr data-index="${i}" class="${item.isOk ? '' : 'row-fail'}">
          <td>${item.story}</td><td>${item.column}</td><td>${item.loadCase}</td>
          <td>${item.location}</td><td>${item.nd.toFixed(2)}</td><td>${item.section}</td>
          <td><input type="number" step="any" class="ca-edit ca-edit-b" data-index="${i}" value="${item.b}"></td>
          <td><input type="number" step="any" class="ca-edit ca-edit-d" data-index="${i}" value="${item.d}"></td>
          <td class="ca-ac">${item.ac.toFixed(2)}</td><td class="ca-acfck">${item.acFck.toFixed(2)}</td>
          <td class="ca-ratio">${item.ndRatio.toFixed(3)}</td><td>${item.limit.toFixed(2)}</td>
          <td class="ca-status">${item.isOk ? '✅' : '❌'}</td>
        </tr>`).join('')
    : `<tr><td colspan="13" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  $$('.ca-edit', body).forEach(input => {
    input.addEventListener('input', () => columnAxialRecalcRow(parseInt(input.dataset.index, 10)));
  });

  installTableFilter(body);
  columnAxialUpdateSummary();

  if (results.length) {
    const worst = results.reduce((a, b) => (b.ndRatio > a.ndRatio ? b : a), results[0]);
    renderCalcBasis('#caResultsBody', 'caBasis', calcBasis(
      'TBDY 2018 §7.3.1 (Denk. 7.3)',
      [
        'Ac = b · d',
        'Nd,max ≤ 0.40 · Ac · fck   →   Nd / (Ac·fck) ≤ 0.40',
        t('columnAxial.basis.signNote')
      ],
      [
        ['fck', `${columnAxialState.fck} MPa`],
        [t('columnAxial.params.limit'), `${columnAxialState.limit}`],
        [t('columnAxial.basis.worst'), `${worst.story} / ${worst.column} / ${worst.loadCase}`],
        ['b × d', `${worst.b} × ${worst.d} cm`],
        ['Ac', `${worst.b} · ${worst.d} = ${worst.ac.toFixed(2)} cm²`],
        ['Ac·fck', `${worst.ac.toFixed(2)} · ${columnAxialState.fck} / 10 = ${worst.acFck.toFixed(2)} kN`],
        ['Nd', `${worst.nd.toFixed(2)} kN`],
        ['Nd/(Ac·fck)', `${worst.nd.toFixed(2)} / ${worst.acFck.toFixed(2)} = <strong>${worst.ndRatio.toFixed(4)}</strong> ` +
          `(${t('columnAxial.params.limit')} ${worst.limit.toFixed(2)} → ${worst.isOk ? 'OK' : 'NOT OK'})`]
      ]));
  }
}

function columnAxialRecalcRow(index) {
  const item = columnAxialState.lastResults[index];
  const row = $(`tr[data-index="${index}"]`, $('#caResultsBody'));
  if (!item || !row) return;

  const b = parseFloat($('.ca-edit-b', row).value) || 0;
  const d = parseFloat($('.ca-edit-d', row).value) || 0;
  item.b = b;
  item.d = d;
  item.ac = b * d;
  const acMm2 = item.ac * 100;
  item.acFck = (acMm2 * columnAxialState.fck) / 1000;
  item.ndRatio = item.acFck > 0 ? item.nd / item.acFck : 0;
  item.isOk = item.ndRatio <= columnAxialState.limit;

  $('.ca-ac', row).textContent = item.ac.toFixed(2);
  $('.ca-acfck', row).textContent = item.acFck.toFixed(2);
  $('.ca-ratio', row).textContent = item.ndRatio.toFixed(3);
  $('.ca-status', row).textContent = item.isOk ? '✅' : '❌';
  row.classList.toggle('row-fail', !item.isOk);

  columnAxialUpdateSummary();
}

function columnAxialUpdateSummary() {
  const results = columnAxialState.lastResults;
  const banner = $('#caStatusBanner');
  const failedList = $('#caFailedList');
  if (!banner || !failedList) return;

  const failedLabels = [...new Set(results.filter(r => !r.isOk).map(r => `${r.column} (${r.story})`))].sort();
  if (failedLabels.length > 0) {
    banner.textContent = t('columnAxial.status.failed', { count: failedLabels.length });
    banner.className = 'status-banner fail';
    failedList.textContent = failedLabels.join(', ');
    failedList.classList.add('fail-text');
  } else if (results.length > 0) {
    banner.textContent = t('columnAxial.status.passed');
    banner.className = 'status-banner ok';
    failedList.textContent = t('columnAxial.failed.none');
    failedList.classList.remove('fail-text');
  } else {
    banner.textContent = t('columnAxial.status.pending');
    banner.className = 'status-banner pending';
    failedList.textContent = t('columnAxial.failed.none');
    failedList.classList.remove('fail-text');
  }
}

// Pure fetch: pulls column frame assignments from ETABS and returns them (throws on error).
async function columnAxialLoadFrameAssignments() {
  const res = await fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Frame Assignments - Summary')}`);
  if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));

  const f = res.fields;
  const storyIdx = tableIndex(f, 'Story');
  const labelIdx = tableIndex(f, 'Label');
  const uniqueIdx = tableIndex(f, 'Unique Name', 'UniqueName');
  const typeIdx = tableIndex(f, 'Design Type', 'Type');
  const analysisSectIdx = tableIndex(f, 'AnalysisSect', 'Analysis Section');
  const designSectIdx = tableIndex(f, 'DesignSect', 'Design Section');

  return res.rows
    .filter(row => typeIdx < 0 || (row[typeIdx] || '').toLowerCase().includes('column'))
    .map(row => {
      let section = analysisSectIdx >= 0 ? row[analysisSectIdx] : '';
      if (!section && designSectIdx >= 0) section = row[designSectIdx];
      return {
        story: row[storyIdx] || '', label: row[labelIdx] || '',
        uniqueName: uniqueIdx >= 0 ? row[uniqueIdx] : '',
        section: section || ''
      };
    });
}

// Pure fetch: pulls the column element forces for the selected combos (Station≈0, StepType=Min).
async function columnAxialLoadElementForces() {
  const comboParam = encodeURIComponent(rigidCombosToFetch(columnAxialState).join(','));
  const res = await fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Element Forces - Columns')}&combos=${comboParam}`);
  if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));

  const f = res.fields;
  const storyIdx = tableIndex(f, 'Story');
  const columnIdx = tableIndex(f, 'Column', 'Label');
  const uniqueIdx = tableIndex(f, 'Unique Name', 'UniqueName');
  const caseIdx = tableIndex(f, 'OutputCase', 'LoadCase', 'Case');
  const stepTypeIdx = tableIndex(f, 'StepType');
  const stationIdx = tableIndex(f, 'Station', 'Location');
  const pIdx = tableIndex(f, 'P');

  // Rigid basement: rows are accepted per story, so the combination that counts
  // depends on whether the story sits above or below the rigid cut-off.
  return res.rows
    .filter(row => {
      const loadCase = (row[caseIdx] || '').toUpperCase();
      if (!rigidRowAllowed(columnAxialState, row[storyIdx] || '', row[caseIdx] || '')) return false;
      const station = parseFloat(row[stationIdx]);
      if (!Number.isNaN(station) && Math.abs(station) > 0.0001) return false;
      const stepType = row[stepTypeIdx] || '';
      return !stepType || stepType.toLowerCase() === 'min';
    })
    .map(row => ({
      story: row[storyIdx] || '', column: row[columnIdx] || '',
      uniqueName: uniqueIdx >= 0 ? row[uniqueIdx] : '',
      loadCase: row[caseIdx] || '', location: stationIdx >= 0 ? row[stationIdx] : '0',
      p: parseFloat(row[pIdx]) || 0
    }));
}

// Single "Hesapla" action: fetches the frame assignments, element forces and stories fresh from
// ETABS, then runs the check — so the user never has to pull the tables manually.
async function runColumnAxialCheck() {
  if (columnAxialState.selected.length === 0) {
    log(t('drift.error.noCombos'), 'error');
    return;
  }

  const { fck, limit } = columnAxialState;
  if (!validateFields([
    { id: 'caFck', ok: inRange(fck, 10, 90), message: t('validate.fck', { value: fck }) },
    { id: 'caLimit', ok: inRange(limit, 0.1, 1), message: t('validate.range', { field: t('columnAxial.params.limit'), min: 0.1, max: 1, value: limit }) }
  ], $('#setupPanel'))) return;

  const btn = $('#caCalculate');
  if (btn) btn.disabled = true;
  try {
    const [frames, forces, storiesRes] = await Promise.all([
      columnAxialLoadFrameAssignments(),
      columnAxialLoadElementForces(),
      fetchAgentJson('/api/etabs/stories')
    ]);
    if (!storiesRes.etabsConnected) throw new Error(storiesRes.error || t('drift.error.notConnected'));

    columnAxialState.frameAssignments = frames;
    columnAxialState.columnForces = forces;
    columnAxialState.stories = storiesRes.stories || [];

    if (forces.length === 0 || frames.length === 0) throw new Error(t('columnAxial.error.noFrameData'));

    const results = columnAxialCalculate(
      columnAxialState.columnForces, columnAxialState.frameAssignments, columnAxialState.stories,
      columnAxialState.fck, columnAxialState.limit, columnAxialState.bodrum, columnAxialState.bodrumKat
    );
    columnAxialState.lastResults = results;
    renderColumnAxialResultsTable();
    recordLastCheck('column-axial');

    const failCount = new Set(results.filter(r => !r.isOk).map(r => `${r.column}|${r.story}`)).size;
    log(failCount > 0 ? t('columnAxial.status.failed', { count: failCount }) : t('columnAxial.status.passed'), failCount > 0 ? 'error' : 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

async function columnAxialSelectFailing() {
  const failing = columnAxialState.lastResults.filter(r => !r.isOk);
  if (failing.length === 0) {
    log(t('columnAxial.status.passed'), 'ok');
    return;
  }
  const items = [...new Map(failing.map(r => [`${r.story}|${r.column}`, { story: r.story, label: r.column }])).values()];
  try {
    const res = await postAgentJson('/api/etabs/select-frames', { items }, 90000);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    log(t('columnAxial.status.selected', { count: res.selectedCount }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  }
}

async function columnAxialExportExcel() {
  const results = columnAxialState.lastResults;
  if (results.length === 0) {
    log(t('columnAxial.error.noFrameData'), 'error');
    return;
  }
  const btn = $('#caExport');
  if (btn) btn.disabled = true;
  try {
    await downloadAgentExcel('/api/etabs/export/column-axial', {
      fck: columnAxialState.fck, limit: columnAxialState.limit,
      rows: results.map(r => ({
        story: r.story, column: r.column, uniqueName: r.uniqueName || '', loadCase: r.loadCase,
        section: r.section, b: r.b, d: r.d, p: r.nd
      }))
    }, 'Kolon_Eksenel_Raporu.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Beam Shear (Kiriş Kesme) — ported from KirisKesmeUI (C#).
// Vr = Vw + Vcr (capped at Vrmax) vs Vd (max |V2| per beam). One "Hesapla" fetches everything.
// ---------------------------------------------------------------------------

const beamShearState = {
  ...rigidStateDefaults(),
  fck: 30, fyk: 420, dprime: 5, useVc: false,
  combos: [], selected: [], lastResults: []
};

function beamShearComputeRow(vd, bM, hM, fck, fyk, dprime, useVc, n, phi, s) {
  const fyd = fyk / 1.15;
  const fctd = 0.35 * Math.sqrt(fck) / 1.5;
  const dM = hM - dprime / 100;
  const vrmax = 0.85 * bM * hM * Math.sqrt(fck) * 1000;
  const vc = useVc ? 0.65 * fctd * bM * dM * 1000 : 0;
  const vcr = 0.8 * vc;
  const sSafe = s > 0 ? s : 10;
  const aswS = n * Math.PI * Math.pow(phi / 10, 2) / 4 / sSafe;
  const vw = aswS * (dM * 100) * fyd * 0.1;
  let vr = vw + vcr;
  if (vr > vrmax) vr = vrmax;
  return { d: dM * 100, vr, ok: vd <= vr };
}

function beamShearCalculate(beams, sectionMap, p) {
  const results = [];
  for (const beam of beams) {
    const sec = sectionMap.get(beam.unique);
    const bM = sec ? sec.b : 0;
    const hM = sec ? sec.h : 0;
    const n = bM > 0.45 ? 4 : 2;
    const phi = 10, s = 10;
    const c = beamShearComputeRow(beam.vd, bM, hM, p.fck, p.fyk, p.dprime, p.useVc, n, phi, s);
    results.push({
      story: beam.story, label: beam.label, unique: beam.unique, case: beam.case,
      section: sec ? sec.section : '', b: bM * 100, h: hM * 100, d: c.d,
      vd: beam.vd, n, phi, s, vr: c.vr, ok: c.ok
    });
  }
  return results.sort((a, b) => b.vd - a.vd);
}

async function loadBeamElementForces(selected, valueField) {
  const comboParam = encodeURIComponent(selected.join(','));
  const res = await fetchAgentJson(`/api/etabs/table?name=${encodeURIComponent('Element Forces - Beams')}&combos=${comboParam}`, 20000);
  if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
  const f = res.fields;
  const storyIdx = tableIndex(f, 'Story');
  const labelIdx = tableIndex(f, 'Beam', 'Label');
  const uniqueIdx = tableIndex(f, 'UniqueName', 'Unique');
  const caseIdx = tableIndex(f, 'OutputCase', 'Case');
  const valIdx = tableIndex(f, valueField);
  return { rows: res.rows, storyIdx, labelIdx, uniqueIdx, caseIdx, valIdx };
}

function renderBeamShearModule() {
  renderBeamShearSetupPanel();
  renderBeamShearResultsPanel();
}

function renderBeamShearSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('beam.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('bsFck', 'beam.params.fck', { min: 10, max: 90 })}
      ${numberField('bsFyk', 'beam.params.fyk', { min: 200, max: 700 })}
      ${numberField('bsDprime', 'beam.params.dprime', { min: 1, max: 15 })}
      ${checkboxField('bsUseVc', 'beam.params.useVc')}
    </div>
    ${comboPicker('bs', 'beam.combos.hint')}
    ${rigidSection('bs', beamShearState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="bsCalculate">${t('columnAxial.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="bsReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>
    <div class="panel-actions two-up">
      <button class="button button-secondary" type="button" id="bsSelectFailing">${t('beamShear.selectFailing')}</button>
      <button class="button button-secondary" type="button" id="bsExport">${t('columnAxial.export')}</button>
    </div>`;

  const bind = (id, key) => {
    const el = $('#' + id, panel);
    el.value = beamShearState[key];
    el.addEventListener('input', () => { beamShearState[key] = parseFloat(el.value) || 0; });
  };
  bind('bsFck', 'fck');
  bind('bsFyk', 'fyk');
  bind('bsDprime', 'dprime');
  const useVc = $('#bsUseVc', panel);
  useVc.checked = beamShearState.useVc;
  useVc.addEventListener('change', () => { beamShearState.useVc = useVc.checked; });

  initComboPicker('bs', beamShearState);
  bindSetupSections(panel);
  initRigidSection('bs', beamShearState, () => beamShearState.combos);
  $('#bsCalculate', panel).addEventListener('click', runBeamShearCheck);
  $('#bsReset', panel).addEventListener('click', () => resetModule(beamShearState, renderBeamShearModule));
  $('#bsSelectFailing', panel).addEventListener('click', () => beamSelectFailing(beamShearState.lastResults));
  $('#bsExport', panel).addEventListener('click', beamShearExportExcel);
}

async function beamSelectFailing(results) {
  const failing = results.filter(r => !r.ok);
  if (failing.length === 0) { log(t('beam.status.allPass'), 'ok'); return; }
  const items = [...new Map(failing.map(r => [`${r.story}|${r.label}`, { story: r.story, label: r.label }])).values()];
  try {
    const res = await postAgentJson('/api/etabs/select-frames', { items }, 90000);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    log(t('beam.status.selected', { count: res.selectedCount }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  }
}

async function runBeamShearCheck() {
  if (beamShearState.selected.length === 0) { log(t('drift.error.noCombos'), 'error'); return; }

  const { fck, fyk, dprime } = beamShearState;
  if (!validateFields([
    { id: 'bsFck', ok: inRange(fck, 10, 90), message: t('validate.fck', { value: fck }) },
    { id: 'bsFyk', ok: inRange(fyk, 200, 700), message: t('validate.fyk', { value: fyk }) },
    { id: 'bsDprime', ok: inRange(dprime, 1, 15), message: t('validate.range', { field: "d'", min: '1 cm', max: '15 cm', value: dprime }) }
  ], $('#setupPanel'))) return;

  const btn = $('#bsCalculate');
  if (btn) btn.disabled = true;
  try {
    const [forces, sectionMap] = await Promise.all([
      loadBeamElementForces(rigidCombosToFetch(beamShearState), 'V2'),
      fetchFrameSectionMap()
    ]);
    const { rows, storyIdx, labelIdx, uniqueIdx, caseIdx, valIdx } = forces;

    // Group by story+label, keep the governing (max |V2|) row.
    const byKey = new Map();
    for (const row of rows) {
      const story = row[storyIdx] || '', label = row[labelIdx] || '';
      if (!rigidRowAllowed(beamShearState, story, row[caseIdx] || '')) continue;
      const key = `${story}_${label}`;
      const vd = Math.abs(parseFloat(row[valIdx]) || 0);
      const existing = byKey.get(key);
      if (!existing) byKey.set(key, { story, label, unique: row[uniqueIdx] || '', case: row[caseIdx] || '', vd });
      else if (vd > existing.vd) { existing.vd = vd; existing.case = row[caseIdx] || ''; }
    }

    const beams = [...byKey.values()];
    if (beams.length === 0) throw new Error(t('beam.error.noData'));

    beamShearState.lastResults = beamShearCalculate(beams, sectionMap, beamShearState)
      .sort((x, y) => y.story.localeCompare(x.story) || x.label.localeCompare(y.label) || x.case.localeCompare(y.case));
    renderBeamShearResultsTable();
    recordLastCheck('beam-shear');
    const failCount = beamShearState.lastResults.filter(r => !r.ok).length;
    log(failCount > 0 ? t('beamShear.status.failed', { count: failCount }) : t('beamShear.status.passed'), failCount > 0 ? 'error' : 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function renderBeamShearResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div></div>
    <div class="status-banner pending" id="bsStatusBanner">${t('columnAxial.status.pending')}</div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('drift.table.story')}</th><th>${t('beam.table.beam')}</th><th>${t('columnAxial.table.section')}</th>
          <th>${t('beamShear.table.vd')}</th><th>${t('columnAxial.table.b')}</th><th>${t('beam.table.h')}</th><th>${t('columnAxial.table.d')}</th>
          <th>${t('beamShear.table.legs')}</th><th>${t('beamShear.table.phi')}</th><th>${t('beamShear.table.spacing')}</th>
          <th>${t('beamShear.table.vr')}</th><th>${t('drift.table.status')}</th>
        </tr></thead>
        <tbody id="bsResultsBody"><tr><td colspan="12" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>`;
  if (beamShearState.lastResults.length) renderBeamShearResultsTable();
}

function renderBeamShearResultsTable() {
  const body = $('#bsResultsBody');
  if (!body) return;
  const results = beamShearState.lastResults;
  body.innerHTML = results.length
    ? results.map((item, i) => `
        <tr data-index="${i}" class="${item.ok ? '' : 'row-fail'}">
          <td>${item.story}</td><td>${item.label}</td><td>${item.section}</td>
          <td>${item.vd.toFixed(2)}</td><td>${item.b.toFixed(1)}</td><td>${item.h.toFixed(1)}</td><td>${item.d.toFixed(1)}</td>
          <td><input type="number" step="1" class="bs-edit bs-edit-n" data-index="${i}" value="${item.n}"></td>
          <td><input type="number" step="1" class="bs-edit bs-edit-phi" data-index="${i}" value="${item.phi}"></td>
          <td><input type="number" step="any" class="bs-edit bs-edit-s" data-index="${i}" value="${item.s}"></td>
          <td class="bs-vr">${item.vr.toFixed(2)}</td><td class="bs-status">${item.ok ? '✅' : '❌'}</td>
        </tr>`).join('')
    : `<tr><td colspan="12" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  $$('.bs-edit', body).forEach(input => input.addEventListener('input', () => beamShearRecalcRow(parseInt(input.dataset.index, 10))));
  installTableFilter(body);
  beamShearUpdateBanner();

  if (results.length) {
    const { fck, fyk, useVc } = beamShearState;
    const fyd = fyk / 1.15, fctd = 0.35 * Math.sqrt(fck) / 1.5;
    const worst = results.reduce((a, b) => (b.vd / b.vr > a.vd / a.vr ? b : a), results[0]);
    const bM = worst.b / 100, dM = worst.d / 100;
    const vc = useVc ? 0.65 * fctd * bM * dM * 1000 : 0;
    const aswS = worst.n * Math.PI * Math.pow(worst.phi / 10, 2) / 4 / (worst.s || 1);
    renderCalcBasis('#bsResultsBody', 'bsBasis', calcBasis(
      'TS 500 §8.1.3–8.1.5, TBDY 2018 §7.4.5',
      [
        'fyd = fyk / 1.15 ,  fctd = 0.35·√fck / 1.5',
        'Vc  = 0.65 · fctd · b · d        Vcr = 0.80 · Vc',
        '(Asw/s) = n · π · Ø² / 4 / s',
        'Vw  = (Asw/s) · d · fyd',
        'Vr  = Vw + Vcr      ≤  Vmax = 0.85 · b · h · √fck',
        'Vd ≤ Vr'
      ],
      [
        ['fck / fyk', `${fck} / ${fyk} MPa`],
        ['fyd', `${fyk} / 1.15 = ${fyd.toFixed(2)} MPa`],
        ['fctd', `0.35·√${fck} / 1.5 = ${fctd.toFixed(4)} MPa`],
        [t('beam.basis.worst'), `${worst.story} / ${worst.label} (${worst.section})`],
        ['b × h → d', `${worst.b.toFixed(1)} × ${worst.h.toFixed(1)} → ${worst.d.toFixed(1)} cm`],
        ['Vcr', useVc ? `0.80 · 0.65 · ${fctd.toFixed(4)} · ${bM.toFixed(3)} · ${dM.toFixed(3)} · 1000 = ${(0.8 * vc).toFixed(2)} kN` : t('beam.basis.vcOff')],
        ['Asw/s', `${worst.n} · π · ${worst.phi}²/100 / 4 / ${worst.s} = ${aswS.toFixed(4)} cm²/cm`],
        ['Vr', `<strong>${worst.vr.toFixed(2)} kN</strong>`],
        ['Vd', `${worst.vd.toFixed(2)} kN → ${worst.ok ? 'OK' : 'NOT OK'} (Vd/Vr = ${(worst.vd / worst.vr).toFixed(3)})`]
      ]));
  }
}

function beamShearRecalcRow(index) {
  const item = beamShearState.lastResults[index];
  const row = $(`tr[data-index="${index}"]`, $('#bsResultsBody'));
  if (!item || !row) return;
  item.n = parseInt($('.bs-edit-n', row).value, 10) || 0;
  item.phi = parseInt($('.bs-edit-phi', row).value, 10) || 0;
  item.s = parseFloat($('.bs-edit-s', row).value) || 0;
  const c = beamShearComputeRow(item.vd, item.b / 100, item.h / 100, beamShearState.fck, beamShearState.fyk, beamShearState.dprime, beamShearState.useVc, item.n, item.phi, item.s);
  item.vr = c.vr;
  item.ok = c.ok;
  $('.bs-vr', row).textContent = item.vr.toFixed(2);
  $('.bs-status', row).textContent = item.ok ? '✅' : '❌';
  row.classList.toggle('row-fail', !item.ok);
  beamShearUpdateBanner();
}

function beamShearUpdateBanner() {
  const banner = $('#bsStatusBanner');
  if (!banner) return;
  const failCount = beamShearState.lastResults.filter(r => !r.ok).length;
  if (failCount > 0) { banner.textContent = t('beamShear.status.failed', { count: failCount }); banner.className = 'status-banner fail'; }
  else if (beamShearState.lastResults.length) { banner.textContent = t('beamShear.status.passed'); banner.className = 'status-banner ok'; }
  else { banner.textContent = t('columnAxial.status.pending'); banner.className = 'status-banner pending'; }
}

async function beamShearExportExcel() {
  const results = beamShearState.lastResults;
  if (results.length === 0) { log(t('beam.error.noData'), 'error'); return; }
  const btn = $('#bsExport');
  if (btn) btn.disabled = true;
  try {
    await downloadAgentExcel('/api/etabs/export/beam-shear', {
      fck: beamShearState.fck, fyk: beamShearState.fyk, useVc: beamShearState.useVc,
      rows: results.map(r => ({ story: r.story, label: r.label, section: r.section, vd: r.vd, b: r.b, h: r.h, d: r.d, n: r.n, phi: r.phi, s: r.s }))
    }, 'Kiris_Kesme_Raporu.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Beam Axial (Kiriş Eksenel) — ported from KirisEksenelYukUI (C#).
// ratio = |P| / (Ac·fck/10) vs 0.1 ; > 0.1 => beam must be detailed as a column.
// ---------------------------------------------------------------------------

const beamAxialState = {
  ...rigidStateDefaults(),
  fck: 30, limit: 0.1,
  combos: [], selected: [], lastResults: []
};

function beamAxialComputeRow(b, d, p, fck, limit) {
  const ac = b * d;
  const capacity = (ac * fck) / 10;
  const ratio = capacity !== 0 ? p / capacity : 0;
  return { ac, capacity, ratio, ok: ratio <= limit };
}

function renderBeamAxialModule() {
  renderBeamAxialSetupPanel();
  renderBeamAxialResultsPanel();
}

function renderBeamAxialSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('beam.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('baFck', 'beam.params.fck', { min: 10, max: 90 })}
      ${numberField('baLimit', 'beamAxial.params.limit', { min: 0.01, max: 1 })}
    </div>
    ${comboPicker('ba', 'beam.combos.hint')}
    ${rigidSection('ba', beamAxialState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="baCalculate">${t('columnAxial.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="baReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>
    <div class="panel-actions two-up">
      <button class="button button-secondary" type="button" id="baSelectFailing">${t('beamAxial.selectFailing')}</button>
      <button class="button button-secondary" type="button" id="baExport">${t('columnAxial.export')}</button>
    </div>`;

  const fck = $('#baFck', panel), limit = $('#baLimit', panel);
  fck.value = beamAxialState.fck; limit.value = beamAxialState.limit;
  fck.addEventListener('input', () => { beamAxialState.fck = parseFloat(fck.value) || 0; });
  limit.addEventListener('input', () => { beamAxialState.limit = parseFloat(limit.value) || 0; });

  initComboPicker('ba', beamAxialState);
  bindSetupSections(panel);
  initRigidSection('ba', beamAxialState, () => beamAxialState.combos);
  $('#baCalculate', panel).addEventListener('click', runBeamAxialCheck);
  $('#baReset', panel).addEventListener('click', () => resetModule(beamAxialState, renderBeamAxialModule));
  $('#baSelectFailing', panel).addEventListener('click', () => beamSelectFailing(beamAxialState.lastResults));
  $('#baExport', panel).addEventListener('click', beamAxialExportExcel);
}

async function runBeamAxialCheck() {
  if (beamAxialState.selected.length === 0) { log(t('drift.error.noCombos'), 'error'); return; }

  const { fck, limit } = beamAxialState;
  if (!validateFields([
    { id: 'baFck', ok: inRange(fck, 10, 90), message: t('validate.fck', { value: fck }) },
    { id: 'baLimit', ok: inRange(limit, 0.01, 1), message: t('validate.range', { field: t('beamAxial.params.limit'), min: 0.01, max: 1, value: limit }) }
  ], $('#setupPanel'))) return;

  const btn = $('#baCalculate');
  if (btn) btn.disabled = true;
  try {
    const [forces, sectionMap] = await Promise.all([
      loadBeamElementForces(rigidCombosToFetch(beamAxialState), 'P'),
      fetchFrameSectionMap()
    ]);
    const { rows, storyIdx, labelIdx, uniqueIdx, caseIdx, valIdx } = forces;

    // Group by unique, keep the governing (max |P|) row.
    const byUnique = new Map();
    for (const row of rows) {
      if (!rigidRowAllowed(beamAxialState, row[storyIdx] || '', row[caseIdx] || '')) continue;
      const unique = row[uniqueIdx] || '';
      const p = Math.abs(parseFloat(row[valIdx]) || 0);
      const existing = byUnique.get(unique);
      if (!existing) byUnique.set(unique, { story: row[storyIdx] || '', label: row[labelIdx] || '', unique, case: row[caseIdx] || '', p });
      else if (p > existing.p) { existing.p = p; existing.case = row[caseIdx] || ''; }
    }

    const beams = [...byUnique.values()];
    if (beams.length === 0) throw new Error(t('beam.error.noData'));

    beamAxialState.lastResults = beams.map(beam => {
      const sec = sectionMap.get(beam.unique);
      const b = (sec ? sec.b : 0) * 100;
      const d = (sec ? sec.h : 0) * 100;
      const c = beamAxialComputeRow(b, d, beam.p, beamAxialState.fck, beamAxialState.limit);
      return { story: beam.story, label: beam.label, unique: beam.unique, case: beam.case, section: sec ? sec.section : '', b, d, p: beam.p, ...c };
    }).sort((x, y) => y.story.localeCompare(x.story) || x.label.localeCompare(y.label) || x.case.localeCompare(y.case));

    renderBeamAxialResultsTable();
    recordLastCheck('beam-axial');
    const failCount = beamAxialState.lastResults.filter(r => !r.ok).length;
    log(failCount > 0 ? t('beamAxial.status.failed', { count: failCount }) : t('beamAxial.status.passed'), failCount > 0 ? 'error' : 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function renderBeamAxialResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div></div>
    <div class="status-banner pending" id="baStatusBanner">${t('columnAxial.status.pending')}</div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('drift.table.story')}</th><th>${t('beam.table.beam')}</th><th>${t('drift.table.combo')}</th><th>${t('columnAxial.table.section')}</th>
          <th>${t('columnAxial.table.b')}</th><th>${t('columnAxial.table.d')}</th><th>${t('columnAxial.table.ac')}</th><th>${t('columnAxial.table.acFck')}</th>
          <th>${t('columnAxial.table.p')}</th><th>${t('columnAxial.table.ratio')}</th><th>${t('drift.table.status')}</th>
        </tr></thead>
        <tbody id="baResultsBody"><tr><td colspan="11" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>`;
  if (beamAxialState.lastResults.length) renderBeamAxialResultsTable();
}

function renderBeamAxialResultsTable() {
  const body = $('#baResultsBody');
  if (!body) return;
  const results = beamAxialState.lastResults;
  body.innerHTML = results.length
    ? results.map((item, i) => `
        <tr data-index="${i}" class="${item.ok ? '' : 'row-fail'}">
          <td>${item.story}</td><td>${item.label}</td><td>${item.case}</td><td>${item.section}</td>
          <td><input type="number" step="any" class="ba-edit ba-edit-b" data-index="${i}" value="${item.b.toFixed(1)}"></td>
          <td><input type="number" step="any" class="ba-edit ba-edit-d" data-index="${i}" value="${item.d.toFixed(1)}"></td>
          <td class="ba-ac">${item.ac.toFixed(1)}</td><td class="ba-cap">${item.capacity.toFixed(1)}</td>
          <td>${item.p.toFixed(2)}</td><td class="ba-ratio">${item.ratio.toFixed(3)}</td><td class="ba-status">${item.ok ? '✅' : '❌'}</td>
        </tr>`).join('')
    : `<tr><td colspan="11" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  $$('.ba-edit', body).forEach(input => input.addEventListener('input', () => beamAxialRecalcRow(parseInt(input.dataset.index, 10))));
  installTableFilter(body);
  beamAxialUpdateBanner();

  if (results.length) {
    const worst = results.reduce((a, b) => (b.ratio > a.ratio ? b : a), results[0]);
    renderCalcBasis('#baResultsBody', 'baBasis', calcBasis(
      'TBDY 2018 §7.3 — ' + t('beamAxial.basis.clause'),
      [
        'Ac = b · d',
        'Nd / (Ac · fck) ≤ 0.10',
        t('beamAxial.basis.rule')
      ],
      [
        ['fck', `${beamAxialState.fck} MPa`],
        [t('beamAxial.params.limit'), `${beamAxialState.limit}`],
        [t('beam.basis.worst'), `${worst.story} / ${worst.label} (${worst.section})`],
        ['b × d', `${worst.b.toFixed(1)} × ${worst.d.toFixed(1)} cm`],
        ['Ac', `${worst.ac.toFixed(1)} cm²`],
        ['Ac·fck', `${worst.ac.toFixed(1)} · ${beamAxialState.fck} / 10 = ${worst.capacity.toFixed(1)} kN`],
        ['Nd', `${worst.p.toFixed(2)} kN`],
        ['Nd/(Ac·fck)', `${worst.p.toFixed(2)} / ${worst.capacity.toFixed(1)} = <strong>${worst.ratio.toFixed(4)}</strong> → ${worst.ok ? 'OK' : t('beamAxial.basis.asColumn')}`]
      ]));
  }
}

function beamAxialRecalcRow(index) {
  const item = beamAxialState.lastResults[index];
  const row = $(`tr[data-index="${index}"]`, $('#baResultsBody'));
  if (!item || !row) return;
  item.b = parseFloat($('.ba-edit-b', row).value) || 0;
  item.d = parseFloat($('.ba-edit-d', row).value) || 0;
  const c = beamAxialComputeRow(item.b, item.d, item.p, beamAxialState.fck, beamAxialState.limit);
  item.ac = c.ac; item.capacity = c.capacity; item.ratio = c.ratio; item.ok = c.ok;
  $('.ba-ac', row).textContent = item.ac.toFixed(1);
  $('.ba-cap', row).textContent = item.capacity.toFixed(1);
  $('.ba-ratio', row).textContent = item.ratio.toFixed(3);
  $('.ba-status', row).textContent = item.ok ? '✅' : '❌';
  row.classList.toggle('row-fail', !item.ok);
  beamAxialUpdateBanner();
}

function beamAxialUpdateBanner() {
  const banner = $('#baStatusBanner');
  if (!banner) return;
  const failCount = beamAxialState.lastResults.filter(r => !r.ok).length;
  if (failCount > 0) { banner.textContent = t('beamAxial.status.failed', { count: failCount }); banner.className = 'status-banner fail'; }
  else if (beamAxialState.lastResults.length) { banner.textContent = t('beamAxial.status.passed'); banner.className = 'status-banner ok'; }
  else { banner.textContent = t('columnAxial.status.pending'); banner.className = 'status-banner pending'; }
}

async function beamAxialExportExcel() {
  const results = beamAxialState.lastResults;
  if (results.length === 0) { log(t('beam.error.noData'), 'error'); return; }
  const btn = $('#baExport');
  if (btn) btn.disabled = true;
  try {
    await downloadAgentExcel('/api/etabs/export/beam-axial', {
      fck: beamAxialState.fck, limit: beamAxialState.limit,
      rows: results.map(r => ({ story: r.story, label: r.label, unique: r.unique, loadCase: r.case, section: r.section, b: r.b, d: r.d, p: r.p }))
    }, 'Kiris_Eksenel_Raporu.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Wall Axial Load (Perde Eksenel) — ported from perde_eksenel.cs.
// Same check as Column Axial, with two differences: the limit is 0.35 (not 0.40),
// and "d" is the wall LENGTH end-to-end (lw) rather than a section depth. Pier data
// does not exist in any ETABS display table — it comes from the dedicated
// Results.PierForce and PierLabel.GetSectionProperties calls the agent wraps.
// ---------------------------------------------------------------------------

const wallAxialState = {
  ...rigidStateDefaults(),
  fck: 30, limit: 0.35, combos: [], selected: [], lastResults: []
};

// Ac = bw·lw (cm²); capacity = Ac·fck/10 (kN); ratio = |P| / capacity.
function wallAxialComputeRow(bw, lw, p, fck, limit) {
  const ac = bw * lw;
  const capacity = ac * fck * 0.1;
  const ratio = capacity > 0 ? Math.abs(p) / capacity : 0;
  return { ac, capacity, ratio, ok: capacity > 0 && ratio <= limit };
}

function renderWallAxialModule() {
  renderWallAxialSetupPanel();
  renderWallAxialResultsPanel();
}

function renderWallAxialSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('wallAxial.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="field-grid">
      ${numberField('waFck', 'columnAxial.params.fck', { min: 10, max: 90 })}
      ${numberField('waLimit', 'columnAxial.params.limit', { min: 0.05, max: 1 })}
    </div>
    ${comboPicker('wa', 'wallAxial.combos.hint')}
    ${rigidSection('wa', wallAxialState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="waCalculate">${t('drift.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="waReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>
    <div class="panel-actions two-up">
      <button class="button button-secondary" type="button" id="waSelectFailing">${t('wallAxial.selectFailing')}</button>
      <button class="button button-secondary" type="button" id="waExport">${t('columnAxial.export')}</button>
    </div>`;

  const bind = (id, key) => {
    const el = $('#' + id, panel);
    el.value = wallAxialState[key];
    el.addEventListener('input', () => { wallAxialState[key] = parseFloat(el.value) || 0; });
  };
  bind('waFck', 'fck');
  bind('waLimit', 'limit');

  initComboPicker('wa', wallAxialState);
  bindSetupSections(panel);
  initRigidSection('wa', wallAxialState, () => wallAxialState.combos);
  $('#waCalculate', panel).addEventListener('click', runWallAxialCheck);
  $('#waReset', panel).addEventListener('click', () => resetModule(wallAxialState, renderWallAxialModule));
  $('#waExport', panel).addEventListener('click', wallAxialExportExcel);
  $('#waSelectFailing', panel).addEventListener('click', wallAxialSelectFailing);
}

function renderWallAxialResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div></div>
    <div class="status-banner pending" id="waStatusBanner">${t('columnAxial.status.pending')}</div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('drift.table.story')}</th><th>${t('wallAxial.table.pier')}</th><th>${t('drift.table.combo')}</th>
          <th>${t('columnAxial.params.fck')}</th><th>${t('columnAxial.table.b')}</th><th>${t('wallAxial.table.lw')}</th>
          <th>${t('columnAxial.table.p')}</th><th>${t('columnAxial.table.ac')}</th>
          <th>${t('columnAxial.table.ratio')}</th><th>${t('drift.table.status')}</th>
        </tr></thead>
        <tbody id="waResultsBody"><tr><td colspan="10" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>`;
  if (wallAxialState.lastResults.length) renderWallAxialResultsTable();
}

function renderWallAxialResultsTable() {
  const body = $('#waResultsBody');
  if (!body) return;
  const results = wallAxialState.lastResults;
  body.innerHTML = results.length
    ? results.map(item => `
        <tr class="${item.ok ? '' : 'row-fail'}">
          <td>${item.story}</td><td>${item.pier}</td><td>${item.loadCase}</td>
          <td>${wallAxialState.fck}</td><td>${item.bw.toFixed(0)}</td><td>${item.lw.toFixed(0)}</td>
          <td>${item.p.toFixed(0)}</td><td>${item.ac.toFixed(0)}</td>
          <td>${item.ratio.toFixed(2)}</td><td>${item.ok ? 'OK' : 'NOT OK'}</td>
        </tr>`).join('')
    : `<tr><td colspan="10" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  installTableFilter(body);

  const banner = $('#waStatusBanner');
  const failCount = results.filter(r => !r.ok).length;
  if (failCount > 0) { banner.textContent = t('wallAxial.status.failed', { count: failCount }); banner.className = 'status-banner fail'; }
  else if (results.length) { banner.textContent = t('wallAxial.status.passed'); banner.className = 'status-banner ok'; }

  if (results.length) {
    const worst = results.reduce((a, b) => (b.ratio > a.ratio ? b : a), results[0]);
    renderCalcBasis('#waResultsBody', 'waBasis', calcBasis(
      'TBDY 2018 §7.6 — ' + t('wallAxial.basis.clause'),
      [
        'Ac = bw · lw',
        t('wallAxial.basis.lwNote'),
        'Nd / (Ac · fck) ≤ 0.35',
        t('wallAxial.basis.pNote')
      ],
      [
        ['fck', `${wallAxialState.fck} MPa`],
        [t('columnAxial.params.limit'), `${wallAxialState.limit}`],
        [t('wallAxial.basis.worst'), `${worst.story} / ${worst.pier} / ${worst.loadCase}`],
        ['bw × lw', `${worst.bw.toFixed(1)} × ${worst.lw.toFixed(1)} cm`],
        ['Ac', `${worst.bw.toFixed(1)} · ${worst.lw.toFixed(1)} = ${worst.ac.toFixed(1)} cm²`],
        ['Ac·fck', `${worst.ac.toFixed(1)} · ${wallAxialState.fck} · 0.1 = ${worst.capacity.toFixed(1)} kN`],
        ['Nd', `${Math.abs(worst.p).toFixed(2)} kN`],
        ['Nd/(Ac·fck)', `${Math.abs(worst.p).toFixed(2)} / ${worst.capacity.toFixed(1)} = <strong>${worst.ratio.toFixed(4)}</strong> ` +
          `(${t('columnAxial.params.limit')} ${wallAxialState.limit} → ${worst.ok ? 'OK' : 'NOT OK'})`]
      ]));
  }
}

async function runWallAxialCheck() {
  if (wallAxialState.selected.length === 0) { log(t('drift.error.noCombos'), 'error'); return; }

  const { fck, limit } = wallAxialState;
  if (!validateFields([
    { id: 'waFck', ok: inRange(fck, 10, 90), message: t('validate.fck', { value: fck }) },
    { id: 'waLimit', ok: inRange(limit, 0.05, 1), message: t('validate.range', { field: t('columnAxial.params.limit'), min: 0.05, max: 1, value: limit }) }
  ], $('#setupPanel'))) return;

  const btn = $('#waCalculate');
  if (btn) btn.disabled = true;
  try {
    const comboParam = encodeURIComponent(rigidCombosToFetch(wallAxialState).join(','));
    const [forcesRes, sectionsRes] = await Promise.all([
      fetchAgentJson(`/api/etabs/pier-forces?combos=${comboParam}`, 30000),
      fetchAgentJson('/api/etabs/pier-sections', 30000)
    ]);
    if (!forcesRes.etabsConnected) throw new Error(forcesRes.error || t('drift.error.notConnected'));
    if (forcesRes.rows.length === 0) throw new Error(t('wallAxial.error.noPierForces'));

    // Geometry lookup keyed by pier+story.
    const geom = new Map();
    for (const s of (sectionsRes.rows || [])) geom.set(`${s.pier}__${s.story}`, s);

    // Keep the governing (max |P|) result per story+pier, mirroring the desktop.
    const byKey = new Map();
    for (const row of forcesRes.rows) {
      const key = `${row.story}__${row.pier}`;
      const existing = byKey.get(key);
      if (!existing || Math.abs(row.p) > Math.abs(existing.p)) {
        byKey.set(key, { story: row.story, pier: row.pier, loadCase: row.loadCase, p: row.p });
      }
    }

    const results = [];
    for (const item of byKey.values()) {
      const g = geom.get(`${item.pier}__${item.story}`);
      const bw = g ? g.bw : 0;
      const lw = g ? g.lw : 0;
      const c = wallAxialComputeRow(bw, lw, item.p, fck, limit);
      results.push({ ...item, p: Math.abs(item.p), bw, lw, ...c });
    }
    results.sort((a, b) => a.pier.localeCompare(b.pier) || a.story.localeCompare(b.story));

    wallAxialState.lastResults = results;
    renderWallAxialResultsTable();
    recordLastCheck('wall-axial');
    const failCount = results.filter(r => !r.ok).length;
    log(failCount > 0 ? t('wallAxial.status.failed', { count: failCount }) : t('wallAxial.status.passed'), failCount > 0 ? 'error' : 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// Marks the failing piers in the ETABS model. Piers are Area objects, so this uses the
// agent's AreaObj-based select-piers endpoint rather than select-frames.
async function wallAxialSelectFailing() {
  const failing = wallAxialState.lastResults.filter(r => !r.ok);
  if (failing.length === 0) { log(t('wallAxial.status.passed'), 'ok'); return; }
  const btn = $('#waSelectFailing');
  if (btn) btn.disabled = true;
  try {
    const items = failing.map(r => ({ story: r.story, pier: r.pier }));
    const res = await postAgentJson('/api/etabs/select-piers', { items }, 90000);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    log(t('wallAxial.status.selected', { count: res.selectedCount }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

async function wallAxialExportExcel() {
  const results = wallAxialState.lastResults;
  if (results.length === 0) { log(t('columnAxial.error.noFrameData'), 'error'); return; }
  const btn = $('#waExport');
  if (btn) btn.disabled = true;
  try {
    await downloadAgentExcel('/api/etabs/export/wall-axial', {
      fck: wallAxialState.fck,
      limit: wallAxialState.limit,
      rows: results.map(r => ({ story: r.story, pier: r.pier, loadCase: r.loadCase, b: r.bw, d: r.lw, p: r.p }))
    }, 'Perde_Eksenel_Raporu.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Wall Shear (Perde Kesme) — ported from perde_kesme.cs (WallShearLogic).
// Vd is max|V2| per story+pier. Three optional rules modify it:
//   • Bodur perde (short wall): Hw/lw_bottom ≤ 2 → EQ amplified by 3/(1+Hw/lw)
//     clamped to [1,2], plus the soil sum.
//   • 0.5V: the pier's GLOBAL max EQ shear × 0.5 (not per story) plus the soil sum.
//   • Rijit bodrum: stories at/below the chosen story use the basement combinations.
// The governing Vd is max(Vd_after_short_rule, 0.5V). Rebar is then chosen from the
// n/φ/s candidates, and — as in the desktop — φ and n may never decrease going down
// the building, which is why a wall usually carries one layout for many stories.
// ---------------------------------------------------------------------------

const wallShearState = {
  fck: 30, fyd: 365,
  fckUpper: 30, secondaryFck: false, splitStory: '',
  phiOpts: [12], sOpts: [15], nOpts: [2],
  combos: [], selected: [],
  shortEqCombos: [], shortSoilCombos: [],
  rule05: true, v05EqCombos: [], v05SoilCombos: [], show05: true,
  rijit: false, rijitStory: '', basementCombos: [],
  stories: [], overrides: {},
  lastResults: [], detail05: [], detailShort: [], activeDetail: null
};

const WS_PHI_CHOICES = [8, 10, 12, 14, 16, 18, 20, 22, 24, 25, 26, 28, 30];
const WS_S_CHOICES = [10, 15, 20, 25];
const WS_N_CHOICES = [2, 3, 4, 5, 6];

// Transverse steel ratio of one n/φ/s candidate, in cm²/cm.
function wsComboArea(n, phi, s) {
  return s > 0 ? n * (Math.PI * Math.pow(phi * 0.1, 2) / 4) / s : 0;
}

// TS 500 minimum horizontal web reinforcement: 0.25 % of the wall thickness.
function wsMeetsMinRebar(n, phi, s, bw) {
  return s > 0 && n * (Math.PI * Math.pow(phi / 10, 2) / 4) * (100 / s) >= 0.25 * bw;
}

function wsShortCoeff(hwLw) {
  return hwLw > 0 ? Math.max(1, Math.min(3 / (1 + hwLw), 2)) : 1;
}

// Story order: index 0 = lowest. Mirrors the desktop's Story.Order.
function wsStoryOrder(name) {
  const idx = wallShearState.stories.findIndex(s => s.name === name);
  return idx < 0 ? -1 : idx;
}

function wsStoryInfo(name) {
  return wallShearState.stories.find(s => s.name === name) || null;
}

// Per-pier geometry: bw/lw for each story it spans, its total height Hw, and the
// lw at its lowest story (which is what the short-wall ratio is measured against).
function wsBuildGeometry(sectionRows) {
  const piers = {};
  for (const row of sectionRows) {
    if (!piers[row.pier]) piers[row.pier] = { stories: {}, hw: 0, bottomLw: 0 };
    piers[row.pier].stories[row.story] = { bw: row.bw, lw: row.lw };
  }
  for (const info of Object.values(piers)) {
    let minBottom = Infinity, maxTop = -Infinity, lowest = Infinity;
    for (const [story, geo] of Object.entries(info.stories)) {
      const s = wsStoryInfo(story);
      if (!s) continue;
      const topZ = s.elevation;
      const bottomZ = s.elevation - (s.height || 0);
      if (bottomZ < minBottom) minBottom = bottomZ;
      if (topZ > maxTop) maxTop = topZ;
      if (bottomZ < lowest) { lowest = bottomZ; info.bottomLw = geo.lw; }
    }
    info.hw = (maxTop > minBottom && minBottom < Infinity) ? (maxTop - minBottom) : 0;
  }
  return piers;
}

// Groups raw pier-force rows into the per-(story,pier) EQ maximum and soil sum that
// both the 0.5V and short-wall rules are built from.
function wsGroupEqSoil(rows, eqCombos, soilCombos) {
  const eqSet = new Set(eqCombos);
  const soilSet = new Set(soilCombos);
  const map = new Map();
  for (const r of rows) {
    const load = (r.loadCase || '').trim();
    if (!eqSet.has(load) && !soilSet.has(load)) continue;
    const key = `${r.story}::${r.pier}`;
    if (!map.has(key)) map.set(key, { story: r.story, pier: r.pier, eq: 0, eqCombo: '-', soil: {} });
    const entry = map.get(key);
    const val = Math.abs(r.v2);
    if (eqSet.has(load)) {
      if (val > entry.eq) { entry.eq = val; entry.eqCombo = load; }
    } else if (val > (entry.soil[load] || 0)) {
      entry.soil[load] = val;
    }
  }
  return map;
}

function wsSoilParts(soil) {
  const keys = Object.keys(soil).sort();
  return {
    sum: keys.reduce((a, k) => a + soil[k], 0),
    s1Name: keys[0] || '-', s1Val: keys[0] ? soil[keys[0]] : 0,
    s2Name: keys[1] || '-', s2Val: keys[1] ? soil[keys[1]] : 0
  };
}

// 0.5V rule. The earthquake part is the pier's GLOBAL maximum across all its stories
// (halved), not the local story value — this is deliberate in the desktop and is what
// makes an upper story inherit a demand driven by a lower one.
function wsCompute05V(rows) {
  const map = wsGroupEqSoil(rows, wallShearState.v05EqCombos, wallShearState.v05SoilCombos);
  const pierMax = {};
  for (const e of map.values()) {
    const local = e.eq * 0.5;
    if (local > (pierMax[e.pier] || 0)) pierMax[e.pier] = local;
  }
  const detail = [];
  const values = {};
  for (const e of map.values()) {
    const globalEq = pierMax[e.pier] || 0;
    const parts = wsSoilParts(e.soil);
    const total = globalEq + parts.sum;
    values[`${e.story}::${e.pier}`] = total;
    detail.push({
      story: e.story, pier: e.pier, eqCombo: e.eqCombo, rawEq: e.eq, local05: e.eq * 0.5,
      globalEq, s1Name: parts.s1Name, s1Val: parts.s1Val, s2Name: parts.s2Name, s2Val: parts.s2Val, total
    });
  }
  return { values, detail };
}

// Short-wall (bodur perde) rule: amplify the EQ shear by the Hw/lw coefficient, then
// add the soil sum. Only piers that actually qualify as short are processed.
function wsComputeShort(rows, geometry, shortPiers) {
  const map = wsGroupEqSoil(rows, wallShearState.shortEqCombos, wallShearState.shortSoilCombos);
  const values = {};
  const detail = [];
  for (const e of map.values()) {
    if (shortPiers.size > 0 && !shortPiers.has(e.pier)) continue;
    const g = geometry[e.pier];
    if (!g) continue;
    const geo = g.stories[e.story];
    const hwLw = geo && geo.lw > 0 ? (g.hw * 100) / geo.lw : 0;
    if (!shortPiers.has(e.pier) && hwLw > 2) continue;
    const coeff = wsShortCoeff(hwLw);
    const parts = wsSoilParts(e.soil);
    const ampEq = e.eq * coeff;
    const total = ampEq + parts.sum;
    values[`${e.story}::${e.pier}`] = total;
    detail.push({
      story: e.story, pier: e.pier, hwLw, coeff, eqCombo: e.eqCombo, eqVal: e.eq, ampEq,
      s1Name: parts.s1Name, s1Val: parts.s1Val, s2Name: parts.s2Name, s2Val: parts.s2Val, total
    });
  }
  return { values, detail };
}

// Main design pass. Walks each pier from the top story down, choosing the lightest
// n/φ/s candidate that covers the required steel and the minimum-reinforcement rule.
// φ and n carry over as floors to the story below, so reinforcement never gets lighter
// as you descend — matching the desktop's minF/minN behaviour.
function wallShearCalculate(pierData, geometry, v05Values, shortValues, shortPiers) {
  const { fck, fyd, fckUpper, secondaryFck, splitStory, rule05 } = wallShearState;
  const splitOrder = secondaryFck && splitStory ? wsStoryOrder(splitStory) : -1;

  const candidates = [];
  for (const n of wallShearState.nOpts)
    for (const phi of wallShearState.phiOpts)
      for (const s of wallShearState.sOpts)
        candidates.push({ n, phi, s, c: wsComboArea(n, phi, s) });
  candidates.sort((a, b) => a.n - b.n || a.c - b.c);

  const results = [];
  const piers = [...new Set(Object.keys(pierData).map(k => k.split('::')[1]))].sort();

  for (const pier of piers) {
    const g = geometry[pier] || { stories: {}, hw: 0, bottomLw: 0 };
    const isShort = shortPiers.has(pier);
    let minPhi = 0, minN = 0;

    // Top story first.
    const storyNames = Object.keys(g.stories)
      .filter(st => pierData[`${st}::${pier}`])
      .sort((a, b) => wsStoryOrder(b) - wsStoryOrder(a));

    for (const story of storyNames) {
      const key = `${story}::${pier}`;
      const data = pierData[key];
      const ovr = wallShearState.overrides[key] || {};
      const geo = g.stories[story] || { bw: 0, lw: 0 };
      const bw = ovr.bw != null ? ovr.bw : geo.bw;
      const lw = ovr.lw != null ? ovr.lw : geo.lw;
      const isCoupled = !!ovr.coupled;
      const currFck = (secondaryFck && splitOrder >= 0 && wsStoryOrder(story) >= splitOrder) ? fckUpper : fck;
      const fctd = 0.35 * Math.sqrt(currFck) / 1.5;
      const hwLw = lw > 0 ? (g.hw * 100) / lw : 0;

      const snap = {
        story, pier, bw, lw, fckUsed: currFck, isCoupled, isShort, hwLw,
        vdRaw: data.vd, coeff: 1, source: 'Vd'
      };
      if (bw <= 0 || lw <= 0) {
        results.push({ ...snap, vmax: 0, vc: 0, vr: 0, vd: 0, v05: 0, n: 0, phi: 0, s: 0,
          statusText: 'GEO ERR', ok: false, kapVal: 0, purVal: 0 });
        continue;
      }

      // Short-wall rule replaces Vd outright when a fetched value exists.
      let vdFinal = data.vd;
      if (isShort && hwLw > 0) {
        snap.coeff = wsShortCoeff(hwLw);
        if (shortValues[key] != null) { vdFinal = shortValues[key]; snap.source = 'Bodur'; }
        else { vdFinal *= snap.coeff; snap.source = 'Bodur (katsayılı)'; }
      }
      const v05 = (rule05 && !isShort && v05Values[key] != null) ? v05Values[key] : 0;
      const vdDesign = Math.max(vdFinal, v05);
      if (v05 > vdFinal) snap.source += ' | 0.5V';

      const vmax = (isCoupled ? 0.065 : 0.085) * bw * lw * Math.sqrt(currFck);
      const vc = 0.065 * bw * lw * fctd;
      const cReq = (vdDesign - vc) > 0 ? (vdDesign - vc) / (lw * fyd * 0.1) : 0;

      // Pick the lightest candidate that satisfies both the demand and the minimum.
      const usable = candidates.filter(c => c.phi >= minPhi && c.n >= minN);
      let chosen = usable.find(c => c.c >= cReq && wsMeetsMinRebar(c.n, c.phi, c.s, bw));
      if (!chosen) chosen = usable.slice().sort((a, b) => b.c - a.c)[0]
                        || candidates.slice().sort((a, b) => b.c - a.c)[0];
      if (!chosen) continue;
      minPhi = chosen.phi; minN = chosen.n;

      const vw = chosen.c * lw * fyd * 0.1;
      const vr = vc + vw;
      let statusText = 'O.K.';
      if (vdDesign > vmax) statusText = 'NOT O.K. (Vd > Vmax)';
      else if (vdDesign > vr) statusText = 'NOT O.K. (Vd > Vr)';
      else if (vr > vmax) statusText = 'NOT O.K. (Vr > Vmax)';
      else if (!wsMeetsMinRebar(chosen.n, chosen.phi, chosen.s, bw)) statusText = 'NOT OK Min. Donatı';

      results.push({
        ...snap, vmax, vc, vr, vd: vdDesign, v05, vdFinal,
        n: chosen.n, phi: chosen.phi, s: chosen.s, vw,
        statusText, ok: statusText === 'O.K.',
        purVal: vr > 0 ? vdDesign / vr : 0,
        kapVal: vmax > 0 ? vdDesign / vmax : 0
      });
    }
  }

  return results.sort((a, b) => a.pier.localeCompare(b.pier) || wsStoryOrder(b.story) - wsStoryOrder(a.story));
}

// Chip row for a multi-select numeric option set (φ / s / n).
function wsChips(id, choices, selected, unit = '') {
  return `<div class="ws-chips" id="${id}" role="group">` + choices.map(v =>
    `<button type="button" class="ws-chip${selected.includes(v) ? ' on' : ''}" data-val="${v}"
       aria-pressed="${selected.includes(v)}">${unit}${v}</button>`).join('') + '</div>';
}

function renderWallShearModule() {
  renderWallShearSetupPanel();
  renderWallShearResultsPanel();
}

function renderWallShearSetupPanel() {
  const panel = $('#setupPanel');
  const st = wallShearState;
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('wallShear.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>

    ${setupSection('wsMaterialSection', 'wallShear.section.material', `
      <div class="field-grid">
        ${numberField('wsFck', 'wallShear.params.fck', { min: 10, max: 90 })}
        ${numberField('wsFyd', 'wallShear.params.fyd', { min: 100, max: 700 })}
      </div>
      <label class="field-checkbox" for="wsSecondaryFck">
        <input type="checkbox" id="wsSecondaryFck" ${st.secondaryFck ? 'checked' : ''}>
        ${t('wallShear.params.secondaryFck')}
      </label>
      <div class="field-grid" id="wsSecondaryFckBody" ${st.secondaryFck ? '' : 'hidden'}>
        ${numberField('wsFckUpper', 'wallShear.params.fckUpper', { min: 10, max: 90 })}
        <div class="field"><label for="wsSplitStory">${t('wallShear.params.splitStory')}</label>
          <select id="wsSplitStory"></select></div>
      </div>`)}

    ${setupSection('wsCombosSection', 'wallShear.section.combos', comboPicker('ws', 'wallShear.combos.hint'))}

    ${setupSection('wsRebarSection', 'wallShear.section.rebar', `
      <p class="ws-chip-label">${t('wallShear.params.phi')}</p>
      ${wsChips('wsPhiChips', WS_PHI_CHOICES, st.phiOpts, 'ø')}
      <p class="ws-chip-label">${t('wallShear.params.spacing')}</p>
      ${wsChips('wsSChips', WS_S_CHOICES, st.sOpts)}
      <p class="ws-chip-label">${t('wallShear.params.legs')}</p>
      ${wsChips('wsNChips', WS_N_CHOICES, st.nOpts)}
      <p class="combo-hint">${t('wallShear.rebar.hint')}</p>`)}

    ${setupSection('wsShortSection', 'wallShear.section.short', `
      <p class="ws-chip-label">${t('wallShear.short.eq')}</p>
      ${comboPicker('wsShortEq', 'wallShear.short.eqHint')}
      <p class="ws-chip-label">${t('wallShear.short.soil')}</p>
      ${comboPicker('wsShortSoil', 'wallShear.short.soilHint')}
      <div class="panel-actions">
        <button class="button button-secondary full-width" type="button" id="wsShortDetail">${t('wallShear.short.detail')}</button>
      </div>`, false)}

    ${setupSection('wsV05Section', 'wallShear.section.v05', `
      <label class="field-checkbox" for="wsRule05">
        <input type="checkbox" id="wsRule05" ${st.rule05 ? 'checked' : ''}>
        ${t('wallShear.v05.active')}
      </label>
      <div id="wsRule05Body" ${st.rule05 ? '' : 'hidden'}>
        <p class="ws-chip-label">${t('wallShear.v05.eq')}</p>
        ${comboPicker('wsV05Eq', 'wallShear.v05.eqHint')}
        <p class="ws-chip-label">${t('wallShear.v05.soil')}</p>
        ${comboPicker('wsV05Soil', 'wallShear.v05.soilHint')}
        <label class="field-checkbox" for="wsShow05"><input type="checkbox" id="wsShow05" ${st.show05 ? 'checked' : ''}> ${t('wallShear.v05.show')}</label>
        <div class="panel-actions">
          <button class="button button-secondary full-width" type="button" id="wsV05Detail">${t('wallShear.v05.detail')}</button>
        </div>
      </div>`, false)}

    ${rigidSection('ws', st)}

    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="wsCalculate">${t('columnAxial.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="wsReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>
    <div class="panel-actions">
      <button class="button button-secondary full-width" type="button" id="wsExport">${t('columnAxial.export')}</button>
    </div>`;

  bindSetupSections(panel);

  const bind = (id, key) => {
    const el = $('#' + id, panel);
    if (!el) return;
    el.value = st[key];
    el.addEventListener('input', () => { st[key] = parseFloat(el.value) || 0; });
  };
  bind('wsFck', 'fck');
  bind('wsFyd', 'fyd');
  bind('wsFckUpper', 'fckUpper');

  const chipGroup = (id, key) => {
    const wrap = $('#' + id, panel);
    if (!wrap) return;
    wrap.addEventListener('click', e => {
      const chip = e.target.closest('.ws-chip');
      if (!chip) return;
      const val = Number(chip.dataset.val);
      const idx = st[key].indexOf(val);
      if (idx >= 0) { if (st[key].length === 1) return; st[key].splice(idx, 1); }
      else st[key].push(val);
      st[key].sort((a, b) => a - b);
      chip.classList.toggle('on');
      chip.setAttribute('aria-pressed', String(chip.classList.contains('on')));
    });
  };
  chipGroup('wsPhiChips', 'phiOpts');
  chipGroup('wsSChips', 'sOpts');
  chipGroup('wsNChips', 'nOpts');

  const secFck = $('#wsSecondaryFck', panel);
  if (secFck) secFck.addEventListener('change', () => {
    st.secondaryFck = secFck.checked;
    const body = $('#wsSecondaryFckBody', panel);
    if (body) body.hidden = !secFck.checked;
  });

  const rule05 = $('#wsRule05', panel);
  if (rule05) rule05.addEventListener('change', () => {
    st.rule05 = rule05.checked;
    const body = $('#wsRule05Body', panel);
    if (body) body.hidden = !rule05.checked;
  });

  const show05 = $('#wsShow05', panel);
  if (show05) show05.addEventListener('change', () => {
    st.show05 = show05.checked;
    if (st.lastResults.length) renderWallShearResultsTable();
  });

  $('#wsCalculate', panel).addEventListener('click', runWallShearCheck);
  $('#wsReset', panel).addEventListener('click', () => resetModule(wallShearState, renderWallShearModule,
    { detail05: [], detailShort: [], activeDetail: null, overrides: {} }));
  $('#wsExport', panel).addEventListener('click', wallShearExportExcel);
  $('#wsShortDetail', panel).addEventListener('click', () => wallShearShowDetail('short'));
  $('#wsV05Detail', panel).addEventListener('click', () => wallShearShowDetail('v05'));

  const shared = sel => ({
    get combos() { return st.combos; }, set combos(v) { st.combos = v; },
    get selected() { return st[sel]; }, set selected(v) { st[sel] = v; }
  });
  initComboPicker('ws', st);
  initComboPicker('wsShortEq', shared('shortEqCombos'));
  initComboPicker('wsShortSoil', shared('shortSoilCombos'));
  initComboPicker('wsV05Eq', shared('v05EqCombos'));
  initComboPicker('wsV05Soil', shared('v05SoilCombos'));
  initRigidSection('ws', st, () => st.combos);

  wallShearLoadSplitStory();
}

// The "different fck above" split needs the same story list the rigid rule loads.
async function wallShearLoadSplitStory() {
  try {
    if (wallShearState.stories.length === 0) {
      const res = await fetchAgentJson('/api/etabs/stories');
      if (!res.etabsConnected) return;
      wallShearState.stories = (res.stories || []).slice().sort((a, b) => a.elevation - b.elevation);
    }
    const sel = $('#wsSplitStory');
    if (!sel) return;
    sel.innerHTML = wallShearState.stories.slice().reverse()
      .map(s => `<option value="${escapeHtml(s.name)}">${escapeHtml(s.name)}</option>`).join('');
    if (wallShearState.splitStory) sel.value = wallShearState.splitStory;
    else wallShearState.splitStory = sel.value;
    sel.addEventListener('change', () => { wallShearState.splitStory = sel.value; });
  } catch { /* optional until the rule is switched on */ }
}

function renderWallShearResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div></div>
    <div class="status-banner pending" id="wsStatusBanner">${t('columnAxial.status.pending')}</div>
    <div id="wsResultsWrap"></div>`;
  if (wallShearState.lastResults.length) renderWallShearResultsTable();
}

function renderWallShearResultsTable() {
  const wrap = $('#wsResultsWrap');
  if (!wrap) return;
  const st = wallShearState;
  const results = st.lastResults;
  const show05 = st.show05;

  if (results.length === 0) {
    wrap.innerHTML = `<div class="table-wrap"><table><tbody><tr><td class="table-empty">${t('drift.table.empty')}</td></tr></tbody></table></div>`;
    return;
  }

  const headers = [
    t('drift.table.story'), t('wallAxial.table.pier'), 'bw (cm)', 'lw (cm)', t('columnAxial.params.fck'),
    'n', 'φ', 's (cm)', 'Vmax (kN)', 'Vr (kN)', ...(show05 ? ['0.5V (kN)'] : []), 'Vd (kN)',
    t('drift.table.status'), t('wallShear.table.rebarCap'), t('wallShear.table.sectionCap'),
    t('wallShear.table.coupled')
  ];

  // Rows are grouped under a header per pier, matching the reference report.
  const byPier = new Map();
  for (const r of results) {
    if (!byPier.has(r.pier)) byPier.set(r.pier, []);
    byPier.get(r.pier).push(r);
  }

  let body = '';
  for (const [pier, rows] of byPier) {
    body += `<tr class="ws-group"><td colspan="${headers.length}">${t('wallShear.table.pierGroup', { pier })}</td></tr>`;
    body += rows.map((r, i) => {
      const idx = results.indexOf(r);
      const capClass = v => v > 1 ? 'ws-cap-bad' : v > 0.75 ? 'ws-cap-warn' : 'ws-cap-ok';
      return `<tr data-index="${idx}" class="${r.ok ? '' : 'row-fail'}">
        <td>${r.story}</td><td>${r.pier}</td>
        <td><input type="number" step="any" class="ws-edit ws-edit-bw" data-index="${idx}" value="${r.bw.toFixed(1)}"></td>
        <td><input type="number" step="any" class="ws-edit ws-edit-lw" data-index="${idx}" value="${r.lw.toFixed(1)}"></td>
        <td>${r.fckUsed}</td><td>${r.n}</td><td>${r.phi}</td><td>${r.s}</td>
        <td>${r.vmax.toFixed(0)}</td><td>${r.vr.toFixed(0)}</td>
        ${show05 ? `<td>${r.v05.toFixed(0)}</td>` : ''}
        <td>${r.vd.toFixed(0)}</td>
        <td class="${r.ok ? '' : 'ws-status-bad'}">${r.statusText}</td>
        <td class="${capClass(r.purVal)}">${r.purVal.toFixed(2)}</td>
        <td class="${capClass(r.kapVal)}">${r.kapVal.toFixed(2)}</td>
        <td><input type="checkbox" class="ws-edit-coupled" data-index="${idx}" ${r.isCoupled ? 'checked' : ''}
             title="${t('wallShear.table.coupledHint')}"></td>
      </tr>`;
    }).join('');
  }

  wrap.innerHTML = `<div class="table-wrap"><table>
      <thead><tr>${headers.map(h => `<th>${h}</th>`).join('')}</tr></thead>
      <tbody id="wsResultsBody">${body}</tbody>
    </table></div>`;

  // Editing geometry or the coupled flag re-runs the design from the stored inputs.
  $$('.ws-edit, .ws-edit-coupled', wrap).forEach(el => {
    el.addEventListener('change', () => {
      const r = results[Number(el.dataset.index)];
      if (!r) return;
      const key = `${r.story}::${r.pier}`;
      const ovr = st.overrides[key] || (st.overrides[key] = {});
      if (el.classList.contains('ws-edit-bw')) ovr.bw = parseFloat(el.value) || 0;
      else if (el.classList.contains('ws-edit-lw')) ovr.lw = parseFloat(el.value) || 0;
      else ovr.coupled = el.checked;
      wallShearRecalculate();
    });
  });

  const banner = $('#wsStatusBanner');
  const failCount = results.filter(r => !r.ok).length;
  if (failCount > 0) { banner.textContent = t('wallShear.status.failed', { count: failCount }); banner.className = 'status-banner fail'; }
  else { banner.textContent = t('wallShear.status.passed'); banner.className = 'status-banner ok'; }

  const worst = results.reduce((a, b) => (b.purVal > a.purVal ? b : a), results[0]);
  renderCalcBasis('#wsResultsBody', 'wsBasis', calcBasis(
    'TBDY 2018 §7.6, TS 500 §8.3',
    [
      'fctd = 0.35·√fck / 1.5',
      'Vmax = (0.085 | 0.065 bağlantı kirişli) · bw · lw · √fck',
      'Vc   = 0.065 · bw · lw · fctd',
      '(Asw/s) = n · π·(φ/10)² / 4 / s',
      'Vw   = (Asw/s) · lw · fyd · 0.1',
      'Vr   = Vc + Vw',
      t('wallShear.basis.minRebar'),
      t('wallShear.basis.order')
    ],
    [
      ['fck / fyd', `${st.fck} / ${st.fyd} MPa`],
      [t('wallShear.basis.worst'), `${worst.pier} / ${worst.story}`],
      ['bw × lw', `${worst.bw.toFixed(1)} × ${worst.lw.toFixed(1)} cm`],
      [t('wallShear.basis.rebar'), `${worst.n} × φ${worst.phi} / ${worst.s} cm`],
      ['Vc', `0.065 · ${worst.bw.toFixed(1)} · ${worst.lw.toFixed(1)} · ${(0.35 * Math.sqrt(worst.fckUsed) / 1.5).toFixed(4)} = ${worst.vc.toFixed(0)} kN`],
      ['Vw', `${worst.vw ? worst.vw.toFixed(0) : 0} kN`],
      ['Vr = Vc + Vw', `<strong>${worst.vr.toFixed(0)} kN</strong>`],
      ['Vmax', `${worst.vmax.toFixed(0)} kN`],
      [t('wallShear.basis.vdSource'), `${worst.source} → Vd = <strong>${worst.vd.toFixed(0)} kN</strong>`],
      ['Vd/Vr · Vd/Vmax', `${worst.purVal.toFixed(3)} · ${worst.kapVal.toFixed(3)} → ${worst.statusText}`]
    ]));

  if (st.activeDetail) renderWallShearDetail(st.activeDetail);
}

// Detail tables expose the intermediate EQ/soil terms behind the 0.5V and short-wall rules.
function wallShearShowDetail(kind) {
  wallShearState.activeDetail = wallShearState.activeDetail === kind ? null : kind;
  renderWallShearDetail(wallShearState.activeDetail);
}

function renderWallShearDetail(kind) {
  const existing = $('#wsDetailBlock');
  if (existing) existing.remove();
  if (!kind) return;

  const st = wallShearState;
  const rows = kind === 'v05' ? st.detail05 : st.detailShort;
  const title = kind === 'v05' ? t('wallShear.v05.detail') : t('wallShear.short.detail');
  if (!rows || rows.length === 0) {
    log(t('wallShear.detail.empty'), 'error');
    st.activeDetail = null;
    return;
  }

  const head = kind === 'v05'
    ? [t('drift.table.story'), t('wallAxial.table.pier'), t('wallShear.detail.eqCombo'), 'EQ (kN)',
       '0.5·EQ (kN)', t('wallShear.detail.globalEq'), t('wallShear.detail.soil1'), t('wallShear.detail.soil2'), 'Toplam (kN)']
    : [t('drift.table.story'), t('wallAxial.table.pier'), 'Hw/lw', t('wallShear.detail.coeff'),
       t('wallShear.detail.eqCombo'), 'EQ (kN)', t('wallShear.detail.ampEq'), t('wallShear.detail.soil1'), t('wallShear.detail.soil2'), 'Toplam (kN)'];

  const body = rows.map(r => kind === 'v05'
    ? `<tr><td>${r.story}</td><td>${r.pier}</td><td>${r.eqCombo}</td><td>${r.rawEq.toFixed(0)}</td>
        <td>${r.local05.toFixed(0)}</td><td>${r.globalEq.toFixed(0)}</td>
        <td>${r.s1Name}: ${r.s1Val.toFixed(0)}</td><td>${r.s2Name}: ${r.s2Val.toFixed(0)}</td>
        <td><strong>${r.total.toFixed(0)}</strong></td></tr>`
    : `<tr><td>${r.story}</td><td>${r.pier}</td><td>${r.hwLw.toFixed(2)}</td><td>${r.coeff.toFixed(3)}</td>
        <td>${r.eqCombo}</td><td>${r.eqVal.toFixed(0)}</td><td>${r.ampEq.toFixed(0)}</td>
        <td>${r.s1Name}: ${r.s1Val.toFixed(0)}</td><td>${r.s2Name}: ${r.s2Val.toFixed(0)}</td>
        <td><strong>${r.total.toFixed(0)}</strong></td></tr>`).join('');

  $('#wsResultsWrap').insertAdjacentHTML('afterend', `
    <div id="wsDetailBlock" class="ws-detail">
      <div class="ws-detail-head"><h3>${title}</h3>
        <button class="text-button" type="button" id="wsDetailClose">${t('action.close')}</button></div>
      <div class="table-wrap"><table>
        <thead><tr>${head.map(h => `<th>${h}</th>`).join('')}</tr></thead>
        <tbody>${body}</tbody>
      </table></div>
    </div>`);
  $('#wsDetailClose').addEventListener('click', () => wallShearShowDetail(kind));
}

// Raw fetched data is kept so table edits can re-run the design without re-querying ETABS.
let wallShearRaw = null;

async function runWallShearCheck() {
  const st = wallShearState;
  if (st.selected.length === 0) { log(t('drift.error.noCombos'), 'error'); return; }

  if (!validateFields([
    { id: 'wsFck', ok: inRange(st.fck, 10, 90), message: t('validate.fck', { value: st.fck }) },
    { id: 'wsFyd', ok: inRange(st.fyd, 100, 700), message: t('validate.fyk', { value: st.fyd }) },
    { id: 'wsFckUpper', ok: !st.secondaryFck || inRange(st.fckUpper, 10, 90), message: t('validate.fck', { value: st.fckUpper }) }
  ], $('#setupPanel'))) return;

  if (st.phiOpts.length === 0 || st.sOpts.length === 0 || st.nOpts.length === 0) {
    log(t('wallShear.error.noRebarOptions'), 'error');
    return;
  }

  const btn = $('#wsCalculate');
  if (btn) btn.disabled = true;
  try {
    // Rigid basement splits the model: upper stories use the main combinations,
    // stories at or below the chosen story use the basement ones.
    const mainCombos = rigidCombosToFetch(st);
    const ruleCombos = [...new Set([...st.shortEqCombos, ...st.shortSoilCombos, ...st.v05EqCombos, ...st.v05SoilCombos])];

    const [mainRes, ruleRes, sectionsRes] = await Promise.all([
      fetchAgentJson(`/api/etabs/pier-forces?combos=${encodeURIComponent(mainCombos.join(','))}`, 60000),
      ruleCombos.length
        ? fetchAgentJson(`/api/etabs/pier-forces?combos=${encodeURIComponent(ruleCombos.join(','))}`, 60000)
        : Promise.resolve({ etabsConnected: true, rows: [] }),
      fetchAgentJson('/api/etabs/pier-sections', 30000)
    ]);
    if (!mainRes.etabsConnected) throw new Error(mainRes.error || t('drift.error.notConnected'));
    if (!mainRes.rows.length) throw new Error(t('wallAxial.error.noPierForces'));

    if (st.stories.length === 0) {
      const storiesRes = await fetchAgentJson('/api/etabs/stories');
      st.stories = (storiesRes.stories || []).slice().sort((a, b) => a.elevation - b.elevation);
    }

    wallShearRaw = { mainRows: mainRes.rows, ruleRows: ruleRes.rows || [], sections: sectionsRes.rows || [] };
    wallShearRecalculate();
    recordLastCheck('wall-shear');
    const failCount = st.lastResults.filter(r => !r.ok).length;
    log(failCount > 0 ? t('wallShear.status.failed', { count: failCount }) : t('wallShear.status.passed'),
        failCount > 0 ? 'error' : 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function wallShearRecalculate() {
  if (!wallShearRaw) return;
  const st = wallShearState;
  const { mainRows, ruleRows, sections } = wallShearRaw;

  const geometry = wsBuildGeometry(sections);

  // Vd = max |V2| per story+pier, honouring the rigid-basement combination split.
  const pierData = {};
  for (const r of mainRows) {
    if (!rigidRowAllowed(st, r.story, r.loadCase)) continue;
    const key = `${r.story}::${r.pier}`;
    const v = Math.abs(r.v2);
    if (!pierData[key] || v > pierData[key].vd) pierData[key] = { vd: v };
  }

  // A pier is short if its overall Hw/lw at the base is ≤ 2, or the user marked it.
  const shortPiers = new Set();
  for (const [pier, g] of Object.entries(geometry)) {
    const manual = Object.entries(st.overrides).some(([k, v]) => k.endsWith(`::${pier}`) && v.short);
    let baseLw = g.bottomLw;
    for (const [k, v] of Object.entries(st.overrides)) {
      if (k.endsWith(`::${pier}`) && v.lw > 0 && wsStoryOrder(k.split('::')[0]) === 0) baseLw = v.lw;
    }
    if (manual || (baseLw > 0 && (g.hw * 100) / baseLw <= 2)) shortPiers.add(pier);
  }

  const v05 = st.rule05 && st.v05EqCombos.length ? wsCompute05V(ruleRows) : { values: {}, detail: [] };
  const shortData = st.shortEqCombos.length ? wsComputeShort(ruleRows, geometry, shortPiers) : { values: {}, detail: [] };

  st.detail05 = v05.detail.sort((a, b) => a.pier.localeCompare(b.pier) || wsStoryOrder(b.story) - wsStoryOrder(a.story));
  st.detailShort = shortData.detail.sort((a, b) => a.pier.localeCompare(b.pier) || wsStoryOrder(b.story) - wsStoryOrder(a.story));
  st.lastResults = wallShearCalculate(pierData, geometry, v05.values, shortData.values, shortPiers);
  renderWallShearResultsTable();
}

async function wallShearExportExcel() {
  const results = wallShearState.lastResults;
  if (results.length === 0) { log(t('columnAxial.error.noFrameData'), 'error'); return; }
  const btn = $('#wsExport');
  if (btn) btn.disabled = true;
  try {
    await downloadAgentExcel('/api/etabs/export/wall-shear', {
      fck: wallShearState.fck,
      fyd: wallShearState.fyd,
      rows: results.map(r => ({
        story: r.story, pier: r.pier, fck: r.fckUsed, bw: r.bw, lw: r.lw,
        n: r.n, phi: r.phi, s: r.s, vd: r.vd, isCoupled: r.isCoupled
      }))
    }, 'Perde_Kesme_Raporu.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

moduleGrid.addEventListener('click', event => {
  const card = event.target.closest('[data-module]');
  if (card) location.hash = card.dataset.module;
});

$$('.nav-item').forEach(item => item.addEventListener('click', () => setActiveView(item.dataset.view)));
$$('[data-back-dashboard]').forEach(button => button.addEventListener('click', () => { location.hash = 'dashboard'; }));
$('#connectButton').addEventListener('click', connectToEtabs);
$$('[data-connect]').forEach(button => button.addEventListener('click', connectToEtabs));
$('#connectionHelp').addEventListener('click', () => $('#architectureDialog').showModal());

$('#showAllModules').addEventListener('click', () => {
  moduleGrid.classList.toggle('expanded');
  updateShowAllLabel();
});

const preferredTheme = localStorage.getItem('sea-theme') || (matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
document.documentElement.dataset.theme = preferredTheme;
$('#themeToggle').setAttribute('aria-pressed', String(preferredTheme === 'dark'));
$('#themeToggle').addEventListener('click', () => {
  const next = document.documentElement.dataset.theme === 'dark' ? 'light' : 'dark';
  document.documentElement.dataset.theme = next;
  $('#themeToggle').setAttribute('aria-pressed', String(next === 'dark'));
  localStorage.setItem('sea-theme', next);
});

$('#disconnectButton').addEventListener('click', disconnectFromEtabs);
$('#instanceConfirm').addEventListener('click', confirmInstanceChoice);
$('#languageToggle').addEventListener('click', () => applyLanguage(currentLanguage === 'en' ? 'tr' : 'en'));
window.addEventListener('hashchange', () => setActiveView(location.hash.slice(1)));
applyLanguage(currentLanguage);
