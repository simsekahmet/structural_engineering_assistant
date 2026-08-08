const moduleDefinitions = [
  { id: 'spectrum', key: 'spectrum', icon: '⌁', categoryKey: 'category.analysis', ready: true },
  { id: 'increment', key: 'increment', icon: '↟', categoryKey: 'category.analysis', ready: true },
  { id: 'drift', key: 'drift', icon: '↔', categoryKey: 'category.analysis', ready: true },
  { id: 'pdelta', key: 'pdelta', icon: 'ϑ', categoryKey: 'category.analysis', ready: true },
  { id: 'column-axial', key: 'columnAxial', icon: '▥', categoryKey: 'category.memberChecks', ready: true },
  { id: 'wall-shear', key: 'wallShear', icon: '▤', categoryKey: 'category.memberChecks', ready: true },
  { id: 'wall-axial', key: 'wallAxial', icon: '▯', categoryKey: 'category.memberChecks', ready: true },
  { id: 'beam-shear', key: 'beamShear', icon: '═', categoryKey: 'category.memberChecks', ready: true },
  { id: 'beam-axial', key: 'beamAxial', icon: '⇥', categoryKey: 'category.memberChecks', ready: true },
  { id: 'report', key: 'report', icon: '❏', categoryKey: 'category.reporting', ready: true }
];

const translations = {
  en: {
    'brand.name': 'Structural Engineering Assistant', 'brand.developedBy': 'Developed by',
    'brand.subtitle': 'ETABS checks and reporting platform',
    'brand.home': 'Structural Engineering Assistant home', 'nav.aria': 'Application menu',
    'model.activeTitle': 'Active ETABS model', 'model.active': 'Active model', 'model.waiting': 'Waiting for connection',
    'action.connect': 'Connect to ETABS', 'action.clear': 'Clear', 'action.showAll': 'Show all →', 'action.showLess': 'Show less ↑',
    'action.on': 'ON', 'action.off': 'OFF', 'action.disconnect': 'Disconnect', 'terminal.disconnected': 'Disconnected from the ETABS model.',
    'instances.title': 'Select ETABS model', 'instances.subtitle': 'More than one ETABS instance is running',
    'instances.connect': 'Connect',
    'action.viewArchitecture': 'View connection architecture', 'action.dashboard': '← Dashboard', 'action.close': 'Close', 'action.understood': 'Understood',
    'action.searching': 'Searching for bridge…', 'action.downloadAgent': 'Download Windows Agent',
    'nav.general': 'GENERAL', 'nav.analysis': 'ANALYSIS & CHECKS', 'nav.memberChecks': 'MEMBER CHECKS',
    'nav.reporting': 'REPORTING',
    'category.analysis': 'Analysis & Checks', 'category.memberChecks': 'Member Checks', 'category.reporting': 'Reporting',
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
    'about.note.label': 'Warning:', 'about.note.text': 'Engineering results must be reviewed and approved by the responsible structural engineer.',
    'moduleData.title': 'Model Data', 'moduleData.description': 'Dataset to be read from the ETABS model',
    'moduleData.waiting': 'Waiting for ETABS connection', 'moduleData.note': 'Module inputs are read from the active ETABS model through the local bridge.',
    'results.title': 'Check Results', 'results.description': 'Summary metrics and member-level results',
    'filter.all': 'All', 'filter.placeholder': 'Filter…', 'filter.clear': 'Clear filters', 'filter.noMatch': 'No rows match the current filters.',
    'combos.available': 'Available combinations', 'combos.selected': 'Combinations to check',
    'combos.add': 'Add selected', 'combos.remove': 'Remove selected',
    'combos.loading': 'Loading combinations from the model…',
    'combos.count': '{total} combinations in the model · {selected} selected',
    'combos.loadFailed': 'Combinations could not be loaded: {error}',
    'combos.needConnection': 'Connect to an ETABS model to load its combinations.',
    'terminal.needConnection': 'Connect to an ETABS model first.',
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
    'module.spectrum.title': 'Reduced Design Spectrum', 'module.spectrum.description': 'Create the horizontal elastic design spectrum using TBDY 2018 parameters and transfer it to the ETABS model.',
    'module.increment.title': 'Base Shear Amplification', 'module.increment.description': 'Calculate the base shear amplification factor from modal results and analysis base shear.',
    'module.drift.title': 'Interstory Drift', 'module.drift.description': 'Calculate effective interstory drifts and compare them with TBDY 2018 limits.',
    'module.pdelta.title': 'Second-Order Effects', 'module.pdelta.description': 'Evaluate story stability coefficients and second-order amplification requirements.',
    'module.columnAxial.title': 'Column Axial Load', 'module.columnAxial.description': 'Check column axial load demands against section capacities and code limits.',
    'module.wallShear.title': 'Wall Shear', 'module.wallShear.description': 'Review wall shear demands, capacities, and governing load combinations by story.',
    'module.wallAxial.title': 'Wall Axial Load', 'module.wallAxial.description': 'Evaluate wall axial load ratios for governing combinations.',
    'module.beamShear.title': 'Beam Shear', 'module.beamShear.description': 'Check beam shear safety by member and story.',
    'module.beamAxial.title': 'Beam Axial Load', 'module.beamAxial.description': 'Filter and report axial force effects in beams.',
    'module.report.title': 'Report', 'module.report.description': 'Build the deliverable calculation report step by step from the shared project information, the checks and their outputs.',

    'report.panel.structure.title': 'Report Structure', 'report.panel.structure.desc': 'Process, steps and template',
    'report.panel.editor.title': 'Report Content',
    'report.stepOf': '{process} PROCESS · STEP {index} / {total}',
    'report.process.intro': 'Introduction', 'report.process.beam': 'App. B — Beam',
    'report.process.column': 'App. C — Column', 'report.process.wall': 'App. D — Wall',
    'report.process.pending': 'This process will be defined in a later step.',
    'report.activeProcess': 'ACTIVE PROCESS',
    'report.count.variables': 'Variables', 'report.count.images': 'Images', 'report.count.tables': 'Tables',
    'report.badge.active': 'ACTIVE STEP', 'report.badge.done': 'COMPLETE', 'report.badge.pending': 'TO BE DEFINED',
    'report.badge.empty': '{count} EMPTY FIELD(S)',
    'report.prev': '‹ Back', 'report.next': 'Continue ›',
    'report.settings.title': 'REPORT SETTINGS', 'report.settings.fontScale': 'Global font size',
    'report.template.save': 'Save template', 'report.template.load': 'Load template',
    'report.template.saved': 'Report template saved as a file.', 'report.template.loaded': 'Report template loaded.',
    'report.template.invalid': 'This file is not a valid report template.',
    'report.reset': 'Clear the report',
    'report.reset.confirm': 'Every piece of report information you entered — including the images — will be cleared. Continue?',
    'report.reset.done': 'Report information cleared.',
    'report.saved': 'Report information is kept in this browser and is never sent anywhere.',
    'report.step.cover.title': 'Cover',
    'report.step.cover.desc': 'The cover text, the report scope and the footer are composed together from the shared project information.',
    'report.step.intro.title': '1 Introduction', 'report.step.system.title': '2 Structural System',
    'report.step.material.title': '4 Materials / 5 Soil', 'report.step.loads.title': '6 Loads',
    'report.step.seismic.title': 'Earthquake Definitions', 'report.step.design.title': '9 Design Principles',
    'report.step.model.title': '10 Analysis Model', 'report.step.baseShear.title': '10.4 Base Shear',
    'report.step.drift.title': 'Interstory Drift & Second-Order', 'report.step.review.title': 'Review',
    'report.step.pending.title': 'This step is not defined yet',
    'report.step.pending.text': 'Its fields will be added from the report template, step by step, so nothing is guessed at.',
    'report.group.project': 'SHARED PROJECT INFORMATION',
    'report.group.project.hint': 'Enter the project information; empty fields are left out of the composed texts.',
    'report.group.texts': 'REPORT TEXTS', 'report.group.image': 'COVER IMAGE', 'report.group.date': 'DATE',
    'report.optional': 'OPTIONAL',
    'report.field.il': 'Province', 'report.field.ilce': 'District', 'report.field.mahalle': 'Neighbourhood',
    'report.field.ada': 'Block no. (ada)', 'report.field.parsel': 'Parcel',
    'report.field.projectName': 'Project name', 'report.field.blockName': 'Building block name',
    'report.field.month': 'Month', 'report.field.year': 'Year',
    'report.text.cover': 'COVER TEXT', 'report.text.scope': '1 INTRODUCTION · REPORT SCOPE TEXT', 'report.text.footer': 'FOOTER',
    'report.text.manual': 'Edit manually', 'report.text.auto': 'Back to automatic',
    'report.text.empty': 'The variables of this section are empty, so this text will be printed empty in the report.',
    'report.template.cover': 'CALCULATION REPORT',
    'report.template.scope': 'This report covers the calculations and checks of the reinforced-concrete structural system of {subject}{location}, carried out in accordance with TBDY 2018 and TS 500.',
    'report.part.il': '{v} Province', 'report.part.ilce': '{v} District', 'report.part.mahalle': '{v} Neighbourhood',
    'report.part.ada': 'block (ada) {v}', 'report.part.parsel': 'parcel {v}',
    'report.part.location': ', located on {v}',
    'report.part.ilLine': '{v} PROVINCE', 'report.part.ilceLine': '{v} DISTRICT',
    'report.part.mahalleLine': '{v} NEIGHBOURHOOD', 'report.part.adaLine': 'BLOCK {v}', 'report.part.parselLine': 'PARCEL {v}',
    'report.part.projectBlock': 'block {block} of the {project} project',
    'report.part.projectBlockNamed': '{block} of the {project} project',
    'report.part.project': 'the {project} project', 'report.part.block': 'block {block}',
    'report.part.blockNamed': '{block}',
    'report.part.structure': 'the structure',
    'report.image.label': 'Cover image', 'report.image.hint': 'The image used on the cover.',
    'report.image.code': 'CODE: {code}',
    'report.image.drop': 'Drag and drop or upload', 'report.image.limits': 'PNG, JPG or WEBP · max {max} MB',
    'report.image.remove': 'Remove image', 'report.image.added': 'Image added.',
    'report.image.type': 'Only PNG, JPG and WEBP images are supported.',
    'report.image.size': 'The image is larger than {max} MB.',
    'report.image.readFailed': 'The image could not be read.',
    'report.preview.title': 'COVER PREVIEW', 'report.preview.imageArea': 'COVER IMAGE AREA',
    'report.preview.date': 'DATE', 'report.preview.footer': 'FOOTER',
    'validate.year': 'Year {value} is not plausible. Expected 2000–2100.',

    'report.step.intro.desc': 'Mark which storey is which on the model’s storey list; the counts and heights of the report sentence follow from it.',
    'report.step.system.desc': 'Plan extents measured from the model, structural system class, foundation and slab system.',
    'report.step.material.desc': 'Concrete and reinforcement come from the connected model; you choose the local soil class.',
    'report.group.storeys': 'STOREYS', 'report.group.storeys.hint': 'The storey list is read from the connected model. Mark each storey; the counts are produced from these marks.',
    'report.group.storeyHeights': 'STOREY HEIGHTS', 'report.group.storeyHeights.hint': 'Pre-filled from the model’s storey table — the height most storeys share. Change any value that is not what the report should quote.',
    'report.group.figures': 'FIGURES',
    'report.group.plan': 'STANDARD FLOOR PLAN EXTENT', 'report.group.plan.hint': 'Measured from the model: the X and Y span of a middle storey.',
    'report.group.systemClass': 'STRUCTURAL SYSTEM',
    'report.group.foundation': 'FOUNDATION SYSTEM', 'report.group.foundation.hint': 'A thickness left blank is not added to the sentence.',
    'report.group.slab': 'SLAB SYSTEM', 'report.group.slab.hint': 'The paragraph changes with the chosen system. The thickness is read from the slab sections assigned in the model.',
    'report.group.concrete': 'CONCRETE', 'report.group.concrete.hint': 'Pre-selected from the model’s concrete material. Pick a different row if the automatic read is wrong.',
    'report.group.rebar': 'REINFORCING STEEL', 'report.group.rebar.hint': 'Pre-selected from the model’s rebar material. Pick a different column if the automatic read is wrong.',
    'report.group.soil': 'LOCAL SOIL CLASS', 'report.group.soil.hint': 'Select the row of the soil class; the report highlights that row.',
    'report.role.basement': 'Basement', 'report.role.ground': 'Ground', 'report.role.normal': 'Normal',
    'report.role.roof': 'Roof', 'report.role.none': '—',
    'report.storeys.needConnection': 'Connect to the ETABS model to read its storey list.',
    'report.storeys.loading': 'Reading the storey list from the model…',
    'report.field.basementH': 'Basement storey height', 'report.field.typicalH': 'Other storey height',
    'report.field.totalH': 'Total height', 'report.field.planWidth': 'Plan extent X', 'report.field.planDepth': 'Plan extent Y',
    'report.field.foundationTower': 'Under the tower', 'report.field.foundationPark': 'Car-park zone',
    'report.field.systemClass': 'System class', 'report.field.foundationType': 'Foundation type',
    'report.field.slabSystem': 'Slab system', 'report.field.slabThickness': 'Slab thickness (cm)',
    'report.select.empty': 'Select…',
    'report.model.agentOld': 'The running Windows agent is older than this page and does not provide model data yet. Download the current agent and restart it, or enter the values by hand.',
    'report.text.identity': 'COVER · PROJECT IDENTITY',

    'report.step.loads.desc': 'Load patterns come from the model. Choose which one each figure is drawn for, group the storeys that share a drawing, and capture the views.',
    'report.group.loadBoxes': 'LOAD BOXES · 6.1–6.3',
    'report.group.loadBoxes.hint': 'The patterns listed are the ones defined in the model. Pick one per box — a figure is drawn for a single load pattern.',
    'report.group.planGroups': 'PLAN VIEWS AND THE STOREYS THEY REPRESENT',
    'report.group.planGroups.hint': 'Storeys placed in the same group share one drawing. Every storey left outside a group gets its own.',
    'report.group.soilWater': '6.5 SOIL AND WATER EFFECTS',
    'report.group.soilPressure.hint': 'Table 6.1 of the report: the pressure assumed on the basement walls, and the load patterns the 3D views are captured for.',
    'report.load.61': 'Dead loads', 'report.load.62': 'Wall loads', 'report.load.63': 'Live loads',
    'report.load.area': '6.1 · AREA LOAD', 'report.load.line': '6.2 · LINE LOAD',
    'report.load.rigidTitle': 'RIGID BASEMENT',
    'report.load.rigidNote': 'When on, the chosen storey and everything below it take the basement selection; the rest take the other one.',
    'report.load.limitStorey': 'Limit storey',
    'report.load.rigidLoads': 'Basement loads', 'report.load.otherLoads': 'Other storey loads', 'report.load.allLoads': 'Loads',
    'report.load.noPatterns': 'The model defines no load patterns.',
    'report.load.needConnection': 'Connect to the model to list its load patterns.',
    'report.pg.storeys': 'STOREYS', 'report.pg.storeysHint': '{count} drawn on their own',
    'report.pg.groups': 'GROUPS', 'report.pg.groupsHint': 'one drawing each',
    'report.pg.makeGroup': 'Add the selected storeys as a view',
    'report.pg.noGroups': 'No group yet — every storey is drawn on its own.',
    'report.pg.groupName': 'Group {n}', 'report.pg.dissolve': 'Dissolve',
    'report.pg.looseNote': '{count} storey(s) outside a group, each with its own drawing.',
    'report.pg.needOne': 'Select at least one storey.',
    'report.pg.generate': 'Generate load views',
    'report.pg.generateHint': '{count} drawing(s) × the selected load patterns.',
    'report.pg.nothingToDo': 'Select at least one load pattern first.',
    'report.soilP.tableTitle': 'TABLE 6.1 · SOIL PRESSURE ON THE BASEMENT WALLS',
    'report.soilP.cohesionless': 'Cohesionless soil', 'report.soilP.softStiff': 'Soft – medium stiff cohesive soil',
    'report.soilP.stiffHard': 'Stiff – hard cohesive soil',
    'report.soilP.fullHeight': 'Over the full height', 'report.soilP.split': 'Upper 20% / lower 80%',
    'report.soilP.static': 'Static soil load pattern', 'report.soilP.dynamic': 'Dynamic soil load pattern',
    'report.soilP.generate': 'Generate soil-load views',
    'report.soilP.generateHint': 'Wall sections whose name starts with "{prefix}" are the basement walls.',
    'report.soilP.needPattern': 'Choose a static or dynamic soil load pattern first.',
    'report.capture.title': 'VIEWS TO CAPTURE',
    'report.capture.row': '{target} · {pattern}',
    'report.capture.summary': '{done} of {total} view(s) captured.',
    'report.capture.busy': 'Generating views — ETABS is being driven, please do not use the computer.',
    'report.soilP.walls': 'Basement walls (3D)',
    'report.measure.button': 'Measure from model', 'report.measure.working': 'Measuring…',
    'report.measure.needConnection': 'Connect to a model to measure.',
    'report.measure.done': 'Plan extent measured on storey {story}.',
    'report.measure.empty': 'No plan extent could be measured — the storey has no point objects.',
    'report.model.readFailed': 'Model data could not be read: {error}. The report still works; enter the values by hand.',
    'report.auto.suggests': 'The model suggests {v}.', 'report.auto.matched': 'Matches the model ({v}).',
    'report.opt.systemClass.wallOnly': 'Ductile solid RC walls (all seismic load)',
    'report.opt.systemClass.wallOnly.phrase': 'buildings in which the whole of the earthquake effect is resisted by high-ductility solid reinforced-concrete walls',
    'report.opt.systemClass.frameWall': 'RC frame + wall (dual system)',
    'report.opt.systemClass.frameWall.phrase': 'a high-ductility dual system of reinforced-concrete frames and walls',
    'report.opt.systemClass.frame': 'RC frame',
    'report.opt.systemClass.frame.phrase': 'a high-ductility reinforced-concrete frame system',
    'report.opt.foundationType.raft': 'Raft foundation', 'report.opt.foundationType.raft.phrase': 'a raft foundation',
    'report.opt.foundationType.strip': 'Strip footing', 'report.opt.foundationType.strip.phrase': 'strip footings',
    'report.opt.foundationType.pad': 'Pad footing', 'report.opt.foundationType.pad.phrase': 'pad footings',
    'report.opt.foundationType.pile': 'Piled foundation', 'report.opt.foundationType.pile.phrase': 'a piled foundation',
    'report.opt.slabSystem.flat': 'Flat plate (beamless)', 'report.opt.slabSystem.flat.phrase': 'flat plate',
    'report.opt.slabSystem.beam': 'Beam-and-slab', 'report.opt.slabSystem.beam.phrase': 'beam-and-slab',
    'report.text.intro': '1 INTRODUCTION · BUILDING DESCRIPTION',
    'report.text.system': '2 STRUCTURAL SYSTEM · TEXT',
    'report.text.foundation': '2.1 FOUNDATION SYSTEM · TEXT',
    'report.text.slab': '2.3 SLAB SYSTEM · TEXT',
    'report.text.concrete': '4.1.1 CONCRETE PROPERTIES · TEXT',
    'report.text.rebar': '4.1.2 REINFORCING STEEL · TEXT',
    'report.part.and': 'and',
    'report.part.basementCount': '{n} basement storeys', 'report.part.groundCount': '{n} ground storey',
    'report.part.normalCount': '{n} normal storeys', 'report.part.roofCount': '{n} roof storey',
    'report.part.totalHeight': ', giving a total height of {h} m',
    'report.part.foundationTower': '{v} cm under the tower', 'report.part.foundationPark': '{v} cm in the car-park zone',
    'report.part.slabThickness': ' as {v} cm',
    'report.template.storeyMakeup': 'The building has {list}.',
    'report.template.basementHeight': 'In the part of the building formed by the {n} basement storeys extending from the top of the foundation up to ground level, the storey height is {h} m.',
    'report.template.typicalHeight': 'In every other storey the storey height is {h} m{total}.',
    'report.template.planExtent': 'The standard floor plan of the building measures approximately {w} m x {d} m.',
    'report.template.systemMembers': 'The structural system of the building consists of reinforced-concrete walls, reinforced-concrete columns and reinforced-concrete beams.',
    'report.template.systemClass': 'The structural system will be designed as {v}.',
    'report.template.slabMention': 'The building is formed of a {v} slab system.',
    'report.template.systemContinuity': 'In laying out the structural system, sufficient stiffness and strength have been provided to transfer the forces acting on the building during an earthquake continuously and safely down to the foundation soil.',
    'report.template.foundationType': 'The foundation of the building is designed as {v}.',
    'report.template.foundationThickness': 'According to the analyses carried out under the combined effect of vertical loads and earthquake, the {v} thickness is {list}.',
    'report.template.slabUsed': 'A {v} slab system is used in the floor slabs of the building.',
    'report.template.slabLimits': 'The thicknesses have been selected{thickness} in accordance with the TS 500 {v} proportioning requirements and the long-term service deflection limits, and the safe transfer of earthquake forces within the diaphragm has been ensured.',
    'report.template.concrete': 'Concrete with a 28-day characteristic cylinder compressive strength of {fck} MPa ({v}) has been used for the frame system, the core walls, the basement columns and slabs, and the foundations.',
    'report.template.rebar': '{v} class {surface} reinforcement with a minimum yield strength of {fy} MPa will be used in the reinforced-concrete structural members.',
    'report.table.concrete': 'Table 4.1 Concrete class properties', 'report.table.concrete.note': 'The selected row is highlighted in the report',
    'report.table.rebar': 'Table 4.2', 'report.table.rebar.note': 'The selected column is highlighted in the report',
    'report.table.soil': 'Table 5.1 Local soil classes', 'report.table.soil.note': 'The selected row is highlighted in the report',
    'report.col.concreteClass': 'Concrete class', 'report.col.fck': 'fck', 'report.col.fctk': 'fctk', 'report.col.e': 'E', 'report.col.g': 'G',
    'report.col.soilClass': 'Local soil class', 'report.col.soilType': 'Soil description',
    'report.col.vs30': '(Vs)30 [m/s]', 'report.col.n60': '(N60)30 [blows/30 cm]', 'report.col.cu30': '(cu)30 [kPa]',
    'report.row.surface': 'Surface type', 'report.row.re': 'Yield strength Re (N/mm²)', 'report.row.rm': 'Tensile strength Rm (N/mm²)',
    'report.row.rmRe': 'Rm / Re', 'report.row.reActNom': 'Re act / Re nom (max)',
    'report.row.a5': 'Elongation at rupture A5 (%)', 'report.row.agt': 'Total elongation Agt (%)',
    'report.surface.plain': 'Plain', 'report.surface.ribbed': 'Ribbed', 'report.surface.profiled': 'Profiled',
    'report.soil.za': 'Strong, hard rock', 'report.soil.zb': 'Slightly weathered, moderately strong rock',
    'report.soil.zc': 'Very dense sand, gravel and hard clay layers, or weathered, heavily fractured weak rock',
    'report.soil.zd': 'Medium dense to dense sand, gravel or very stiff clay layers',
    'report.soil.ze': 'Loose sand, gravel or soft to stiff clay layers',
    'report.soil.zf': 'Soils requiring site-specific investigation and evaluation',
    'report.soil.siteSpecific': 'Site-specific',
    'report.fig.s11': 'Figure 1.1 Building section', 'report.fig.s12': 'Figure 1.2 3D analysis model',
    'report.fig.s21': 'Figure 2.1 Typical floor formwork plan',
    'report.fig.hint': 'If no image is uploaded, this figure is not printed in the report.',
    'drift.params.title': 'Earthquake Parameters', 'drift.params.sdsDD2': 'SDS (DD-2)', 'drift.params.sdsDD3': 'SDS (DD-3)',
    'drift.params.sd1DD2': 'SD1 (DD-2)', 'drift.params.sd1DD3': 'SD1 (DD-3)', 'drift.params.k': 'k', 'drift.params.tp': 'Tp',
    'drift.params.flexibleJoint': 'Flexible joint present? (Yes: 0.016, No: 0.008)', 'drift.params.basement': 'Basement assumption?',
    'drift.params.basementCount': 'Number of basement stories',
    'drift.combos.title': 'Load Combinations', 'drift.combos.fetch': 'Fetch from ETABS',
    'drift.combos.hint': 'Select combinations containing direction (X/Y) and level (UST/ALT), e.g. RSXUST.',
    'drift.combos.fetched': '{count} combinations/cases found.',
    'drift.calculate': 'Calculate', 'drift.export': 'Export to Excel',
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
    'spectrum.chart.title': 'Reduced Design Spectrum', 'spectrum.chart.subtitle': 'Reduced horizontal elastic spectrum SaR(T)',
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
    'increment.error.noSpectrum': 'Calculate the Reduced Design Spectrum first.',
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
    'action.on': 'AÇIK', 'action.off': 'KAPALI', 'action.disconnect': 'Bağlantıyı Kes', 'terminal.disconnected': 'ETABS model bağlantısı kesildi.',
    'instances.title': 'ETABS Modeli Seçin', 'instances.subtitle': 'Birden fazla ETABS örneği çalışıyor',
    'instances.connect': 'Bağlan',
    'action.viewArchitecture': 'Bağlantı mimarisini görüntüle', 'action.dashboard': '← Ana Sayfa', 'action.close': 'Kapat', 'action.understood': 'Anladım',
    'action.searching': 'Köprü aranıyor…', 'action.downloadAgent': 'Windows Agent’ı İndir',
    'nav.general': 'GENEL', 'nav.analysis': 'ANALİZ & KONTROL', 'nav.memberChecks': 'ELEMAN TAHKİKLERİ',
    'nav.reporting': 'RAPORLAMA',
    'category.analysis': 'Analiz & Kontrol', 'category.memberChecks': 'Eleman Tahkikleri', 'category.reporting': 'Raporlama',
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
    'about.note.label': 'Uyarı:', 'about.note.text': 'Mühendislik sonuçları sorumlu inşaat mühendisi tarafından kontrol edilmeli ve onaylanmalıdır.',
    'moduleData.title': 'Model Verisi', 'moduleData.description': 'ETABS modelinden okunacak veri seti',
    'moduleData.waiting': 'ETABS bağlantısı bekleniyor', 'moduleData.note': 'Modül girdileri, yerel köprü üzerinden aktif ETABS modelinden okunur.',
    'results.title': 'Tahkik Sonuçları', 'results.description': 'Özet metrikler ve eleman bazlı sonuçlar',
    'filter.all': 'Tümü', 'filter.placeholder': 'Filtre…', 'filter.clear': 'Filtreleri temizle', 'filter.noMatch': 'Geçerli filtrelere uyan satır yok.',
    'combos.available': 'Mevcut kombinasyonlar', 'combos.selected': 'Tahkik edilecekler',
    'combos.add': 'Seçilenleri ekle', 'combos.remove': 'Seçilenleri çıkar',
    'combos.loading': 'Kombinasyonlar modelden yükleniyor…',
    'combos.count': 'Modelde {total} kombinasyon · {selected} seçili',
    'combos.loadFailed': 'Kombinasyonlar yüklenemedi: {error}',
    'combos.needConnection': 'Kombinasyonların yüklenmesi için bir ETABS modeline bağlanın.',
    'terminal.needConnection': 'Önce bir ETABS modeline bağlanın.',
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
    'module.spectrum.title': 'Azaltılmış Tasarım Spektrumu', 'module.spectrum.description': 'TBDY 2018 parametreleriyle yatay elastik tasarım spektrumunu oluşturun ve ETABS modeline aktarın.',
    'module.increment.title': 'Taban Kesme Kuvveti Büyütmesi', 'module.increment.description': 'Modal sonuçlar ve analiz taban kesme kuvveti üzerinden büyütme katsayısını hesaplayın.',
    'module.drift.title': 'Göreli Kat Ötelemesi', 'module.drift.description': 'Etkin göreli kat ötelemelerini hesaplayın ve TBDY 2018 sınırlarıyla karşılaştırın.',
    'module.pdelta.title': 'İkinci Mertebe Etkileri', 'module.pdelta.description': 'Kat stabilite katsayılarını ve ikinci mertebe büyütme gereksinimini değerlendirin.',
    'module.columnAxial.title': 'Kolon Eksenel Yük', 'module.columnAxial.description': 'Kolon eksenel yük taleplerini kesit kapasiteleri ve yönetmelik sınırlarıyla tahkik edin.',
    'module.wallShear.title': 'Perde Kesme', 'module.wallShear.description': 'Perde kesme taleplerini, kapasitelerini ve kritik yük birleşimlerini kat bazında inceleyin.',
    'module.wallAxial.title': 'Perde Eksenel Yük', 'module.wallAxial.description': 'Perde eksenel yük oranlarını kritik kombinasyonlar için değerlendirin.',
    'module.beamShear.title': 'Kiriş Kesme', 'module.beamShear.description': 'Kiriş kesme güvenliğini eleman ve kat bazında tahkik edin.',
    'module.beamAxial.title': 'Kiriş Eksenel Yük', 'module.beamAxial.description': 'Kirişlerdeki eksenel kuvvet etkilerini filtreleyin ve raporlayın.',
    'module.report.title': 'Rapor', 'module.report.description': 'Teslim edilecek hesap raporunu; ortak proje bilgileri, tahkikler ve çıktılarından adım adım oluşturun.',

    'report.panel.structure.title': 'Rapor Yapısı', 'report.panel.structure.desc': 'Süreç, adımlar ve şablon',
    'report.panel.editor.title': 'Rapor İçeriği',
    'report.stepOf': '{process} SÜRECİ · ADIM {index} / {total}',
    'report.process.intro': 'Giriş', 'report.process.beam': 'EK-B — Kiriş',
    'report.process.column': 'EK-C — Kolon', 'report.process.wall': 'EK-D — Perde',
    'report.process.pending': 'Bu süreç sonraki adımlarda tanımlanacak.',
    'report.activeProcess': 'AKTİF SÜREÇ',
    'report.count.variables': 'Değişken', 'report.count.images': 'Görsel', 'report.count.tables': 'Tablo',
    'report.badge.active': 'AKTİF ADIM', 'report.badge.done': 'TAMAMLANDI', 'report.badge.pending': 'TANIMLANACAK',
    'report.badge.empty': '{count} BOŞ ALAN',
    'report.prev': '‹ Geri', 'report.next': 'Devam ›',
    'report.settings.title': 'RAPOR AYARLARI', 'report.settings.fontScale': 'Genel punto',
    'report.template.save': 'Şablon kaydet', 'report.template.load': 'Şablon yükle',
    'report.template.saved': 'Rapor şablonu dosya olarak kaydedildi.', 'report.template.loaded': 'Rapor şablonu yüklendi.',
    'report.template.invalid': 'Bu dosya geçerli bir rapor şablonu değil.',
    'report.reset': 'Raporu temizle',
    'report.reset.confirm': 'Girdiğiniz tüm rapor bilgileri — görseller dâhil — silinecek. Devam edilsin mi?',
    'report.reset.done': 'Rapor bilgileri temizlendi.',
    'report.saved': 'Rapor bilgileri bu tarayıcıda saklanır, hiçbir yere gönderilmez.',
    'report.step.cover.title': 'Kapak',
    'report.step.cover.desc': 'Ortak proje bilgilerinden kapak yazısı, rapor kapsamı ve alt bilgi birlikte oluşturulur.',
    'report.step.intro.title': '1 Giriş', 'report.step.system.title': '2 Taşıyıcı Sistem',
    'report.step.material.title': '4 Malzeme / 5 Zemin', 'report.step.loads.title': '6 Yükler',
    'report.step.seismic.title': 'Deprem Tanımları', 'report.step.design.title': '9 Tasarım Esasları',
    'report.step.model.title': '10 Hesap Modeli', 'report.step.baseShear.title': '10.4 Taban Kesme',
    'report.step.drift.title': 'Göreli Kat & 2. Mertebe', 'report.step.review.title': 'Kontrol',
    'report.step.pending.title': 'Bu adım henüz tanımlanmadı',
    'report.step.pending.text': 'Alanları, rapor altlığından adım adım eklenecek; hiçbir alan tahmin edilmeyecek.',
    'report.group.project': 'ORTAK PROJE BİLGİLERİ',
    'report.group.project.hint': 'Proje bilgilerini girin; boş alanlar oluşturulan metinlere eklenmez.',
    'report.group.texts': 'RAPOR METİNLERİ', 'report.group.image': 'KAPAK GÖRSELİ', 'report.group.date': 'TARİH',
    'report.optional': 'İSTEĞE BAĞLI',
    'report.field.il': 'İl', 'report.field.ilce': 'İlçe', 'report.field.mahalle': 'Mahalle',
    'report.field.ada': 'Ada', 'report.field.parsel': 'Parsel',
    'report.field.projectName': 'Proje adı', 'report.field.blockName': 'Blok adı',
    'report.field.month': 'Ay', 'report.field.year': 'Yıl',
    'report.text.cover': 'KAPAK YAZISI', 'report.text.scope': '1 GİRİŞ · RAPOR KAPSAMI METNİ', 'report.text.footer': 'ALT BİLGİ',
    'report.text.manual': 'Manuel düzenle', 'report.text.auto': 'Otomatiğe dön',
    'report.text.empty': 'Bu bölümdeki değişkenler boş bırakıldığı için raporda bu metin boş basılacak.',
    'report.template.cover': 'HESAP RAPORU',
    'report.template.scope': 'Bu rapor, {location} {subject} ait betonarme taşıyıcı sistemin TBDY 2018 ve TS 500 uyarınca yapılan hesap ve tahkiklerini kapsamaktadır.',
    'report.part.il': '{v} İli', 'report.part.ilce': '{v} İlçesi', 'report.part.mahalle': '{v} Mahallesi',
    'report.part.ada': '{v} Ada', 'report.part.parsel': '{v} Parsel',
    'report.part.location': '{v} üzerinde yer alan',
    'report.part.ilLine': '{v} İLİ', 'report.part.ilceLine': '{v} İLÇESİ',
    'report.part.mahalleLine': '{v} MAHALLESİ', 'report.part.adaLine': '{v} ADA', 'report.part.parselLine': '{v} PARSEL',
    'report.part.projectBlock': '{project} projesinin {block} bloğuna',
    'report.part.projectBlockNamed': '{project} projesinin {block} yapısına',
    'report.part.project': '{project} projesine', 'report.part.block': '{block} bloğuna',
    'report.part.blockNamed': '{block} yapısına',
    'report.part.structure': 'yapıya',
    'report.image.label': 'Kapak görseli', 'report.image.hint': 'Kapakta kullanılacak görsel.',
    'report.image.code': 'KOD: {code}',
    'report.image.drop': 'Sürükleyip bırakın veya yükleyin', 'report.image.limits': 'PNG, JPG veya WEBP · en fazla {max} MB',
    'report.image.remove': 'Görseli kaldır', 'report.image.added': 'Görsel eklendi.',
    'report.image.type': 'Yalnızca PNG, JPG ve WEBP görseller desteklenir.',
    'report.image.size': 'Görsel {max} MB sınırından büyük.',
    'report.image.readFailed': 'Görsel okunamadı.',
    'report.preview.title': 'KAPAK ÖNİZLEMESİ', 'report.preview.imageArea': 'KAPAK GÖRSELİ ALANI',
    'report.preview.date': 'TARİH', 'report.preview.footer': 'ALT BİLGİ',
    'validate.year': '{value} yılı makul değil. 2000–2100 aralığı bekleniyor.',

    'report.step.intro.desc': 'Modelin kat listesinde hangi katın ne olduğunu işaretleyin; rapor cümlesindeki kat sayıları ve yükseklikler buradan üretilir.',
    'report.step.system.desc': 'Modelden ölçülen izdüşüm boyutları, taşıyıcı sistem sınıfı, temel ve döşeme sistemi.',
    'report.step.material.desc': 'Beton ve donatı bağlı modelden gelir; yerel zemin sınıfını siz seçersiniz.',
    'report.group.storeys': 'KATLAR', 'report.group.storeys.hint': 'Kat listesi bağlı modelden okunur. Her katı işaretleyin; sayılar bu işaretlemelerden üretilir.',
    'report.group.storeyHeights': 'KAT YÜKSEKLİKLERİ', 'report.group.storeyHeights.hint': 'Modelin kat tablosundan, katların çoğunda ortak olan yükseklik ile önden doldurulur. Raporda geçmesi gereken değer bu değilse değiştirin.',
    'report.group.figures': 'ŞEKİLLER',
    'report.group.plan': 'STANDART KAT İZDÜŞÜMÜ', 'report.group.plan.hint': 'Modelden ölçülür: bir ara katın X ve Y yönündeki açıklığı.',
    'report.group.systemClass': 'TAŞIYICI SİSTEM',
    'report.group.foundation': 'TEMEL SİSTEMİ', 'report.group.foundation.hint': 'Girilmeyen kalınlık cümleye eklenmez.',
    'report.group.slab': 'DÖŞEME SİSTEMİ', 'report.group.slab.hint': 'Paragraf seçilen sisteme göre değişir. Kalınlık, modelde atanmış döşeme kesitlerinden okunur.',
    'report.group.concrete': 'BETON', 'report.group.concrete.hint': 'Modeldeki beton malzemesinden önden seçilir. Otomatik okuma yanlışsa farklı bir satır seçin.',
    'report.group.rebar': 'DONATI ÇELİĞİ', 'report.group.rebar.hint': 'Modeldeki donatı malzemesinden önden seçilir. Otomatik okuma yanlışsa farklı bir sütun seçin.',
    'report.group.soil': 'YEREL ZEMİN SINIFINI SEÇİN', 'report.group.soil.hint': 'Zemin sınıfının satırını seçin; raporda o satır vurgulanır.',
    'report.role.basement': 'Bodrum', 'report.role.ground': 'Zemin', 'report.role.normal': 'Normal',
    'report.role.roof': 'Çatı', 'report.role.none': '—',
    'report.storeys.needConnection': 'Kat listesini okumak için ETABS modeline bağlanın.',
    'report.storeys.loading': 'Kat listesi modelden okunuyor…',
    'report.field.basementH': 'Bodrum kat yüksekliği', 'report.field.typicalH': 'Diğer kat yüksekliği',
    'report.field.totalH': 'Toplam yükseklik', 'report.field.planWidth': 'İzdüşüm X', 'report.field.planDepth': 'İzdüşüm Y',
    'report.field.foundationTower': 'Yükselen blok altında', 'report.field.foundationPark': 'Otopark bölgesinde',
    'report.field.systemClass': 'Sistem sınıfı', 'report.field.foundationType': 'Temel tipi',
    'report.field.slabSystem': 'Döşeme sistemi', 'report.field.slabThickness': 'Döşeme kalınlığı (cm)',
    'report.select.empty': 'Seçiniz…',
    'report.model.agentOld': 'Çalışan Windows agent bu sayfadan eski; model verisi uçlarını henüz sunmuyor. Güncel agent’ı indirip yeniden başlatın veya değerleri elle girin.',
    'report.text.identity': 'KAPAK · PROJE KİMLİĞİ',

    'report.step.loads.desc': 'Yük tanımları modelden gelir. Her şeklin hangi yük için çizileceğini seçin, aynı görselde gösterilecek katları gruplayın ve görünümleri yakalayın.',
    'report.group.loadBoxes': 'YÜK KUTUCUKLARI · 6.1–6.3',
    'report.group.loadBoxes.hint': 'Listelenen yükler modelde tanımlı olanlardır. Her kutuda bir tane seçin — bir şekil tek bir yük için çizilir.',
    'report.group.planGroups': 'ÇİZİLECEK PLANLAR VE TEMSİL ETTİĞİ KATLAR',
    'report.group.planGroups.hint': 'Aynı gruba alınan katlar tek bir görselde çizilir. Gruba alınmayan her kat kendi görselini alır.',
    'report.group.soilWater': '6.5 TOPRAK VE SU ETKİLERİ',
    'report.group.soilPressure.hint': 'Raporun Tablo 6.1’i: bodrum perdelerine etkiyen zemin basıncı ve 3B görünümlerin yakalanacağı yükler.',
    'report.load.61': 'Sabit Yükler', 'report.load.62': 'Duvar Yükleri', 'report.load.63': 'Hareketli Yükler',
    'report.load.area': '6.1 · ALAN YÜKÜ', 'report.load.line': '6.2 · ÇİZGİSEL YÜK',
    'report.load.rigidTitle': 'RİJİT BODRUM KAT',
    'report.load.rigidNote': 'Açıkken seçilen kat ve altındakiler bodrum seçimini, diğerleri öteki seçimi kullanır.',
    'report.load.limitStorey': 'Sınır katı',
    'report.load.rigidLoads': 'Rijit bodrum yükleri', 'report.load.otherLoads': 'Diğer kat yükleri', 'report.load.allLoads': 'Yükler',
    'report.load.noPatterns': 'Modelde tanımlı yük yok.',
    'report.load.needConnection': 'Modeldeki yükleri listelemek için bağlanın.',
    'report.pg.storeys': 'KATLAR', 'report.pg.storeysHint': '{count} tanesi ayrı çizilecek',
    'report.pg.groups': 'GRUPLAR', 'report.pg.groupsHint': 'her biri tek görsel',
    'report.pg.makeGroup': 'Seçili katları görsel olarak ekle',
    'report.pg.noGroups': 'Henüz grup yok — her kat ayrı çizilecek.',
    'report.pg.groupName': 'Grup {n}', 'report.pg.dissolve': 'Gruptan çıkar',
    'report.pg.looseNote': '{count} kat grup dışında, her biri kendi görselinde.',
    'report.pg.needOne': 'En az bir kat seçin.',
    'report.pg.generate': 'Yük görsellerini üret',
    'report.pg.generateHint': '{count} görsel × seçili yükler.',
    'report.pg.nothingToDo': 'Önce en az bir yük seçin.',
    'report.soilP.tableTitle': 'TABLO 6.1 · BODRUM PERDELERİNE ETKİYEN ZEMİN BASINÇLARI',
    'report.soilP.cohesionless': 'Kohezyonsuz zemin', 'report.soilP.softStiff': 'Yumuşak – orta katı kohezyonlu zemin',
    'report.soilP.stiffHard': 'Katı – sert kohezyonlu zemin',
    'report.soilP.fullHeight': 'Tüm yükseklik boyunca', 'report.soilP.split': 'Üst %20 boyunca / Alt %80 boyunca',
    'report.soilP.static': 'Statik toprak yükü', 'report.soilP.dynamic': 'Dinamik toprak yükü',
    'report.soilP.generate': 'Toprak yükü görsellerini üret',
    'report.soilP.generateHint': 'Adı "{prefix}" ile başlayan perde kesitleri bodrum perdesi kabul edilir.',
    'report.soilP.needPattern': 'Önce statik veya dinamik toprak yükünü seçin.',
    'report.capture.title': 'YAKALANACAK GÖRÜNÜMLER',
    'report.capture.row': '{target} · {pattern}',
    'report.capture.summary': '{total} görselden {done} tanesi üretildi.',
    'report.capture.busy': 'Görseller üretiliyor — ETABS sürülüyor, lütfen bilgisayarı kullanmayın.',
    'report.soilP.walls': 'Bodrum perdeleri (3B)',
    'report.measure.button': 'Modelden ölç', 'report.measure.working': 'Ölçülüyor…',
    'report.measure.needConnection': 'Ölçmek için bir modele bağlanın.',
    'report.measure.done': 'İzdüşüm {story} katından ölçüldü.',
    'report.measure.empty': 'İzdüşüm ölçülemedi — katta nokta nesnesi bulunamadı.',
    'report.model.readFailed': 'Model verisi okunamadı: {error}. Rapor yine çalışır; değerleri elle girebilirsiniz.',
    'report.auto.suggests': 'Model {v} öneriyor.', 'report.auto.matched': 'Modelle aynı ({v}).',
    'report.opt.systemClass.wallOnly': 'Süneklik düzeyi yüksek boşluksuz betonarme perde',
    'report.opt.systemClass.wallOnly.phrase': 'deprem etkilerinin tamamının süneklik düzeyi yüksek boşluksuz betonarme perdelerle karşılandığı binalar',
    'report.opt.systemClass.frameWall': 'Betonarme çerçeve + perde (karma)',
    'report.opt.systemClass.frameWall.phrase': 'süneklik düzeyi yüksek betonarme çerçeve ve perdelerden oluşan karma sistem',
    'report.opt.systemClass.frame': 'Betonarme çerçeve',
    'report.opt.systemClass.frame.phrase': 'süneklik düzeyi yüksek betonarme çerçeve sistemi',
    'report.opt.foundationType.raft': 'Radye temel', 'report.opt.foundationType.raft.phrase': 'radye temel',
    'report.opt.foundationType.strip': 'Sürekli temel', 'report.opt.foundationType.strip.phrase': 'sürekli temel',
    'report.opt.foundationType.pad': 'Tekil temel', 'report.opt.foundationType.pad.phrase': 'tekil temel',
    'report.opt.foundationType.pile': 'Kazıklı temel', 'report.opt.foundationType.pile.phrase': 'kazıklı temel',
    'report.opt.slabSystem.flat': 'Kirişsiz plak', 'report.opt.slabSystem.flat.phrase': 'kirişsiz plak',
    'report.opt.slabSystem.beam': 'Kirişli plak', 'report.opt.slabSystem.beam.phrase': 'kirişli plak',
    'report.text.intro': '1 GİRİŞ · BİNA TANIMI',
    'report.text.system': '2 TAŞIYICI SİSTEM · METİN',
    'report.text.foundation': '2.1 TEMEL SİSTEMİ · METİN',
    'report.text.slab': '2.3 DÖŞEME SİSTEMİ · METİN',
    'report.text.concrete': '4.1.1 BETON ÖZELLİKLERİ · METİN',
    'report.text.rebar': '4.1.2 DONATI ÇELİĞİ ÖZELLİKLERİ · METİN',
    'report.part.and': 've',
    'report.part.basementCount': '{n} bodrum kata', 'report.part.groundCount': '{n} zemin kata',
    'report.part.normalCount': '{n} normal kata', 'report.part.roofCount': '{n} çatı kata',
    'report.part.totalHeight': ' olup toplam yükseklik {h} m’dir',
    'report.part.foundationTower': 'yükselen blok altında {v} cm', 'report.part.foundationPark': 'otopark bölgesinde {v} cm',
    'report.part.slabThickness': ' {v} cm olarak',
    'report.template.storeyMakeup': 'Bina {list} sahiptir.',
    'report.template.basementHeight': 'Binanın, temel üstünden zemin seviyesine kadar uzanan {n} bodrum kattan oluşan bölümünde, kat yüksekliği {h} m’dir.',
    'report.template.typicalHeight': 'Diğer tüm katlarda ise kat yüksekliği {h} m{total}.',
    'report.template.planExtent': 'Binanın standart kat izdüşümün boyutları yaklaşık {w} m x {d} m’dir.',
    'report.template.systemMembers': 'Yapının taşıyıcı sistemi, betonarme perdelerden, betonarme kolonlar ve betonarme kirişlerden oluşmaktadır.',
    'report.template.systemClass': 'Yapının taşıyıcı sistemi {v} olarak tasarlanacaktır.',
    'report.template.slabMention': 'Yapı {v} döşeme sisteminden oluşmaktadır.',
    'report.template.systemContinuity': 'Taşıyıcı sistem teşkil edilirken, deprem sırasında binaya etki edecek kuvvetlerin temel zeminine kadar sürekli bir şekilde ve güvenli olarak aktarılmasını sağlayacak yeterli rijitlik ve dayanım sağlanmıştır.',
    'report.template.foundationType': 'Binanın temeli {v} olarak tasarlanmıştır.',
    'report.template.foundationThickness': 'Düşey yükler ve depremin ortak etkisi altında gerçekleştirilen hesaplara göre, {v} kalınlığı {list} olmaktadır.',
    'report.template.slabUsed': 'Bina kat tabliyelerinde {v} döşeme sistemi kullanılmıştır.',
    'report.template.slabLimits': 'Kalınlıklar, TS500 {v} boyutlandırma şartlarına ve servis durumundaki uzun süreli sehim limitlerine göre{thickness} seçilmiş olup, deprem kuvvetlerinin diyafram içinde güvenle aktarılması sağlanmıştır.',
    'report.template.concrete': 'Beton sınıfı 28 günlük karakteristik silindir basınç dayanımı, çerçeve sistemi ve çekirdek perdeleri, bodrum kolon ve döşemeleri ve temeller için {fck} MPa ({v}) olan beton kullanılmıştır.',
    'report.template.rebar': 'Betonarme taşıyıcı sistem elemanlarında minimum akma dayanımı {fy} MPa olan {v} sınıfı {surface} donatı kullanılacaktır.',
    'report.table.concrete': 'Tablo 4.1 Beton sınıfı özellikleri', 'report.table.concrete.note': 'Seçilen satır raporda vurgulanır',
    'report.table.rebar': 'Tablo 4.2', 'report.table.rebar.note': 'Seçilen sütun raporda vurgulanır',
    'report.table.soil': 'Tablo 5.1 Yerel zemin sınıfları', 'report.table.soil.note': 'Seçilen satır raporda vurgulanır',
    'report.col.concreteClass': 'Beton sınıfı', 'report.col.fck': 'fck', 'report.col.fctk': 'fctk', 'report.col.e': 'E', 'report.col.g': 'G',
    'report.col.soilClass': 'Yerel zemin sınıfı', 'report.col.soilType': 'Zemin cinsi',
    'report.col.vs30': '(Vs)30 [m/s]', 'report.col.n60': '(N60)30 [darbe/30 cm]', 'report.col.cu30': '(cu)30 [kPa]',
    'report.row.surface': 'Yüzey tipi', 'report.row.re': 'Akma dayanımı Re (N/mm²)', 'report.row.rm': 'Çekme dayanımı Rm (N/mm²)',
    'report.row.rmRe': 'Rm / Re', 'report.row.reActNom': 'Re act / Re nom (max)',
    'report.row.a5': 'Kopma uzaması A5 (%)', 'report.row.agt': 'Toplam uzama Agt (%)',
    'report.surface.plain': 'Düz yüzeyli', 'report.surface.ribbed': 'Nervürlü', 'report.surface.profiled': 'Profilli',
    'report.soil.za': 'Sağlam, sert kayalar', 'report.soil.zb': 'Az ayrışmış, orta sağlam kayalar',
    'report.soil.zc': 'Çok sıkı kum, çakıl ve sert kil tabakaları veya ayrışmış, çok çatlaklı zayıf kayalar',
    'report.soil.zd': 'Orta sıkı–sıkı kum, çakıl veya çok katı kil tabakaları',
    'report.soil.ze': 'Gevşek kum, çakıl veya yumuşak–katı kil tabakaları',
    'report.soil.zf': 'Sahaya özel araştırma ve değerlendirme gerektiren zeminler',
    'report.soil.siteSpecific': 'Sahaya özel',
    'report.fig.s11': 'Şekil 1.1 Bina boy kesiti', 'report.fig.s12': 'Şekil 1.2 3D analiz modeli görseli',
    'report.fig.s21': 'Şekil 2.1 Normal kat kalıp planı',
    'report.fig.hint': 'Görsel yüklenmezse bu şekil rapora basılmaz.',
    'drift.params.title': 'Deprem Parametreleri', 'drift.params.sdsDD2': 'SDS (DD-2)', 'drift.params.sdsDD3': 'SDS (DD-3)',
    'drift.params.sd1DD2': 'SD1 (DD-2)', 'drift.params.sd1DD3': 'SD1 (DD-3)', 'drift.params.k': 'k', 'drift.params.tp': 'Tp',
    'drift.params.flexibleJoint': 'Esnek derz var mı? (Var: 0.016, Yok: 0.008)', 'drift.params.basement': 'Bodrum kabulü var mı?',
    'drift.params.basementCount': 'Bodrum kat sayısı',
    'drift.combos.title': 'Yük Kombinasyonları', 'drift.combos.fetch': "ETABS'tan Getir",
    'drift.combos.hint': 'Yön (X/Y) ve seviye (ÜST/ALT) içeren kombinasyonları seçin, örn. RSXUST.',
    'drift.combos.fetched': '{count} kombinasyon/case bulundu.',
    'drift.calculate': 'Hesapla', 'drift.export': "Excel'e Aktar",
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
    'spectrum.chart.title': 'Azaltılmış Tasarım Spektrumu', 'spectrum.chart.subtitle': 'Azaltılmış yatay elastik spektrum SaR(T)',
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
    'increment.error.noSpectrum': 'Önce Azaltılmış Tasarım Spektrumu sayfasından spektrum hesaplayınız.',
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
  'wall-shear': renderWallShearModule,
  report: renderReportModule
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
  const countStat = $('#moduleCountStat');
  if (countStat) countStat.textContent = String(moduleDefinitions.length);
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
  // Module-owned controls next to the "Dashboard" button. Cleared on every view
  // change so one module's toolbar can never leak into another's heading.
  const headingSlot = $('#moduleHeadingSlot');
  if (headingSlot) headingSlot.innerHTML = '';
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
  etabsConnected = true;
  comboCachePromise = null;
  // A new connection may well be a different model — reconnecting without
  // disconnecting first used to leave the previous model's storeys in the report.
  reportForgetModelData();
  log(t('terminal.connected', { model }), 'ok');
  showPreflight(data);
  // Re-render whatever module is open so its now-available data loads.
  const current = location.hash.slice(1);
  if (current && current !== 'dashboard' && moduleRenderers[current]) moduleRenderers[current]();
}

// Everything read from a model, plus the values the user typed against it. Called on
// disconnect so the next connection cannot inherit anything from the previous one.
function resetAllModuleData() {
  comboCachePromise = null;
  wallShearRaw = null;
  reportForgetModelData();
  for (const st of [driftState, pdeltaState, incrementState, columnAxialState,
                    beamShearState, beamAxialState, wallAxialState, wallShearState]) {
    st.combos = [];
    st.selected = [];
    st.basementCombos = [];
    st.stories = [];
    st.rijit = false;
    st.rijitStory = '';
    if ('lastResults' in st) st.lastResults = [];
    if ('lastResult' in st) st.lastResult = null;
  }
  // Values read from the model rather than entered by the engineer.
  incrementState.mt = 0;
  incrementState.tx = 0;
  incrementState.ty = 0;
  incrementState.vtX = 0;
  incrementState.vtY = 0;
  incrementState.resultX = null;
  incrementState.resultY = null;
  wallShearState.detail05 = [];
  wallShearState.detailShort = [];
  wallShearState.activeDetail = null;
  wallShearState.overrides = {};
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

  etabsConnected = false;
  resetAllModuleData();

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
    log(describeError(error), 'error');
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

// Only a genuine transport failure is the bridge's fault. Anything else is an error
// from the check itself and is shown as-is, so a code or data problem is never
// mislabelled as a connection problem.
function describeError(error) {
  const transport = (error instanceof TypeError)
    || error.name === 'TimeoutError' || error.name === 'AbortError';
  return transport ? `${t('drift.error.fetchFailed')}: ${error.message}` : error.message;
}

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

// Always visible: the label on the left, the switch on the right. Turning it on reveals
// the story picker and — everywhere except Base Shear Amplification, which reuses the
// main selection — a basement combination list.
function rigidSection(prefix, state, opts = {}) {
  const withCombos = opts.combos !== false;
  return `<div class="rigid-block">
      <div class="rigid-row">
        <span class="rigid-title">${t('wallShear.section.rigid')}</span>
        <button type="button" class="switch toggle-switch" id="${prefix}Rijit"
                role="switch" aria-checked="${state.rijit}">
          <span class="switch-label off">${t('action.off')}</span>
          <span class="switch-label on">${t('action.on')}</span>
          <span class="switch-thumb" aria-hidden="true"></span>
        </button>
      </div>
      <div id="${prefix}RijitBody" ${state.rijit ? '' : 'hidden'}>
        <div class="field-grid">
          <div class="field">
            <label for="${prefix}RijitStory">${t('wallShear.rigid.story')}</label>
            <select id="${prefix}RijitStory"></select>
          </div>
        </div>
        <p class="rigid-note" id="${prefix}RijitNote"></p>
        ${withCombos ? comboPicker(`${prefix}Basement`, 'wallShear.rigid.combosHint') : ''}
      </div>
    </div>`;
}

async function initRigidSection(prefix, state, allCombos, opts = {}) {
  const withCombos = opts.combos !== false;
  const toggle = $('#' + prefix + 'Rijit');
  const body = $('#' + prefix + 'RijitBody');
  const select = $('#' + prefix + 'RijitStory');
  const note = $('#' + prefix + 'RijitNote');

  const paintNote = () => {
    if (note) note.textContent = state.rijitStory ? t('wallShear.rigid.note', { story: state.rijitStory }) : '';
  };

  if (toggle) toggle.addEventListener('click', () => {
    state.rijit = !state.rijit;
    toggle.setAttribute('aria-checked', String(state.rijit));
    if (body) body.hidden = !state.rijit;
  });

  if (withCombos) {
    initComboPicker(`${prefix}Basement`, {
      get combos() { return allCombos(); },
      set combos(v) { /* shared cache owns the list */ },
      get selected() { return state.basementCombos; },
      set selected(v) { state.basementCombos = v; }
    });
  }

  try {
    if (!etabsConnected) return;
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

// Number of stories the rigid rule marks as basement, counted from the bottom.
// Used where a module needs a count rather than a set (mass exclusion, Vt row).
function rigidBasementCount(state, stories) {
  if (!rigidIsActive(state)) return 0;
  const list = (state.stories && state.stories.length) ? state.stories : (stories || []);
  const cut = list.findIndex(x => x.name === state.rijitStory);
  if (cut < 0) return 0;
  return list.slice(0, cut + 1).filter(x => (x.name || '').toLowerCase() !== 'base').length;
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
// Nothing is read from the model until a connection exists. Modules render empty and
// fill in once the user connects, so a stale list can never look like live data.
let etabsConnected = false;
let comboCachePromise = null;

async function loadCombos(force = false) {
  if (!etabsConnected) return [];
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
  const status = $('#' + prefix + 'ComboStatus');
  if (!etabsConnected) {
    if (status) status.textContent = t('combos.needConnection');
    return;
  }
  if (state.combos.length === 0) {
    try {
      state.combos = await loadCombos();
      paintComboPicker(prefix, state);
    } catch (error) {
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

// Free-text sibling of numberField(), with the same label/error wiring. `span: 2`
// makes the field take the whole width of a two-column .field-grid.
function textField(id, labelKey, { span = 1, placeholder = '' } = {}) {
  return `<div class="field${span > 1 ? ' span-2' : ''}">
      <label for="${id}">${t(labelKey)}</label>
      <input type="text" id="${id}" ${placeholder ? `placeholder="${escapeHtml(placeholder)}"` : ''} aria-describedby="${id}-err">
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
  // Model order, highest story first — matching Second-Order Effects. driftState.stories
  // is lowest-first, so a larger index means a higher story. Falls back to the name when
  // the story list is unavailable.
  const order = (driftState.stories || []).map(s => s.name);
  const rank = name => {
    const i = order.indexOf(name);
    return i < 0 ? -1 : i;
  };
  return [...items].sort((a, b) =>
    (a.direction === 'X' ? 0 : 1) - (b.direction === 'X' ? 0 : 1) ||
    rank(b.story) - rank(a.story) ||
    b.story.localeCompare(a.story));
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
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
    // Basement stories come from the rigid-basement rule alone.
    const rigid = rigidIsActive(driftState);
    const basementNames = rigidBasementStorySet(driftState, driftState.stories, new Set());
    const filtered = filterDriftRows(driftRes.rows || [], groups, basementNames,
      rigid, rigid ? groupCombos(driftState.basementCombos) : null);
    if (filtered.length === 0) throw new Error(t('drift.error.noData'));

    const result = calculateDriftItems(filtered, driftState.params);
    driftState.lastResult = result;
    renderDriftResultsTable(result);
    recordLastCheck('drift');
    log(t(result.allPassed ? 'drift.status.passed' : 'drift.status.failed'), result.allPassed ? 'ok' : 'error');
  } catch (error) {
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
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
  // "Base" carries no story drift of its own, so it never takes part in the check.
  const nonBase = stories.filter(s => s.name.toLowerCase() !== 'base');
  const basementNames = new Set();

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
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Reduced Design Spectrum (Azaltılmış Tasarım Spektrumu) — from SpectrumManager (C#), TBDY 2018.
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
  $('#spReset', panel).addEventListener('click', () => resetModule(spectrumState, renderSpectrumModule,
    { periods: [], accelerations: [], sds: 0, sd1: 0, r: 0, d: 0, i: 0 }));
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
// Depends on the Reduced Design Spectrum module's shared spectrumState (SDS, I, SaR curve).
// ---------------------------------------------------------------------------

const incrementState = {
  ...rigidStateDefaults(),
  mt: 0, hn: 0, ct: 0.07,
  combos: [], selected: [],
  tx: 0, vtX: 0, ty: 0, vtY: 0,
  modalTopX: [], modalTopY: [],
  resultX: null, resultY: null
};

// Pulls mass and both modal periods without the user asking. Failures are logged by
// the individual fetchers; nothing here should block the module from rendering.
async function incrementAutoFetchModelData() {
  if (!etabsConnected) return;
  try { await incrementFetchMass(true); } catch { /* reported by the fetcher */ }
  try { await incrementFetchPeriod('X', true); } catch { /* reported by the fetcher */ }
  try { await incrementFetchPeriod('Y', true); } catch { /* reported by the fetcher */ }
  await incrementAutoFetchVt();
}

// Vt needs a combination naming the direction, so this is a no-op until one is picked.
async function incrementAutoFetchVt() {
  if (!etabsConnected) return;
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
    const basementCount = rigidBasementCount(incrementState, storiesRes.stories || []);
    if (basementCount > 0) {
      determineBasementStories(storiesRes.stories || [], basementCount)
        .forEach(name => excluded.add(name.toLowerCase()));
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
    if (!silent) log(describeError(error), 'error');
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
    if (!silent) log(describeError(error), 'error');
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
    const targetRow = Math.min(rigidBasementCount(incrementState, incrementState.stories), perStory.length - 1);
    const target = perStory[targetRow];
    const vt = Math.abs(direction === 'X' ? target.vx : target.vy);

    if (direction === 'X') { incrementState.vtX = vt; $('#incVtX').value = vt.toFixed(2); }
    else { incrementState.vtY = vt; $('#incVtY').value = vt.toFixed(2); }
    if (!silent) log(t(direction === 'X' ? 'increment.status.vtFetchedX' : 'increment.status.vtFetchedY'), 'ok');
  } catch (error) {
    if (!silent) log(describeError(error), 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function incrementCalculate(direction) {
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
      ${numberField('incMt', 'increment.params.mt', { min: 0 })}
      ${numberField('incHn', 'increment.params.hn', { min: 0 })}
      ${numberField('incCt', 'increment.params.ct', { min: 0 })}
    </div>
    ${comboPicker('inc', 'increment.combos.hint')}
    ${rigidSection('inc', incrementState, { combos: false })}
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

  // Model-read fields (mass, periods, base shears) show blank until they are read,
  // so an unconnected page never displays a plausible-looking zero.
  const modelRead = new Set(['mt', 'tx', 'ty', 'vtX', 'vtY']);
  const bind = (id, key, isInt = false) => {
    const el = $('#' + id, panel);
    el.value = (modelRead.has(key) && !incrementState[key]) ? '' : incrementState[key];
    el.addEventListener('input', () => { incrementState[key] = (isInt ? parseInt(el.value, 10) : parseFloat(el.value)) || 0; });
  };
  bind('incMt', 'mt');
  bind('incHn', 'hn');
  bind('incCt', 'ct');

  // Mass and modal periods come straight from the model when the module opens.
  // Vt additionally needs a direction-matching combination, so it is (re)read
  // whenever the selection changes as well as once on open.
  initComboPicker('inc', incrementState, incrementAutoFetchVt);
  bindSetupSections(panel);
  initRigidSection('inc', incrementState, () => incrementState.combos, { combos: false });
  incrementAutoFetchModelData();

  $('#incCalcX', panel).addEventListener('click', () => incrementCalculate('X'));
  $('#incCalcY', panel).addEventListener('click', () => incrementCalculate('Y'));
  $('#incReset', panel).addEventListener('click', () => {
    // Clear the model-read values too, then pull them again.
    Object.assign(incrementState, { mt: 0, tx: 0, ty: 0, vtX: 0, vtY: 0, resultX: null, resultY: null });
    resetModule(incrementState, renderIncrementModule);
  });

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
    </div>
    ${comboPicker('ca', 'columnAxial.combos.hint')}
    ${rigidSection('ca', columnAxialState)}
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="caCalculate">${t('columnAxial.calculate')}</button>
      <button class="button button-secondary full-width" type="button" id="caReset" style="margin-top:8px">${t('action.reset')}</button>
    </div>
    <div class="panel-actions">
      <button class="button button-secondary full-width" type="button" id="caSelectFailing">${t('columnAxial.selectFailing')}</button>
    </div>`;

  const bind = (id, key, isInt = false) => {
    const el = $('#' + id, panel);
    el.value = columnAxialState[key];
    el.addEventListener('input', () => { columnAxialState[key] = (isInt ? parseInt(el.value, 10) : parseFloat(el.value)) || 0; });
  };
  bind('caFck', 'fck');
  bind('caLimit', 'limit');

  initComboPicker('ca', columnAxialState);
  bindSetupSections(panel);
  initRigidSection('ca', columnAxialState, () => columnAxialState.combos);
  $('#caCalculate', panel).addEventListener('click', runColumnAxialCheck);
  $('#caReset', panel).addEventListener('click', () => resetModule(columnAxialState, renderColumnAxialModule));
  $('#caSelectFailing', panel).addEventListener('click', columnAxialSelectFailing);
}

function renderColumnAxialResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="caExport" ${columnAxialState.lastResults.length ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
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
  $('#caExport', panel).addEventListener('click', columnAxialExportExcel);
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
  const caExportBtn = $('#caExport');
  if (caExportBtn) caExportBtn.disabled = results.length === 0;

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
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
      columnAxialState.fck, columnAxialState.limit,
      rigidIsActive(columnAxialState), rigidBasementCount(columnAxialState, columnAxialState.stories)
    );
    columnAxialState.lastResults = results;
    renderColumnAxialResultsTable();
    recordLastCheck('column-axial');

    const failCount = new Set(results.filter(r => !r.isOk).map(r => `${r.column}|${r.story}`)).size;
    log(failCount > 0 ? t('columnAxial.status.failed', { count: failCount }) : t('columnAxial.status.passed'), failCount > 0 ? 'error' : 'ok');
  } catch (error) {
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
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
    <div class="panel-actions">
      <button class="button button-secondary full-width" type="button" id="bsSelectFailing">${t('beamShear.selectFailing')}</button>
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
    log(describeError(error), 'error');
  }
}

async function runBeamShearCheck() {
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
    log(describeError(error), 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function renderBeamShearResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="bsExport" ${beamShearState.lastResults.length ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
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
  $('#bsExport', panel).addEventListener('click', beamShearExportExcel);
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
  const bsExportBtn = $('#bsExport');
  if (bsExportBtn) bsExportBtn.disabled = results.length === 0;

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
    log(describeError(error), 'error');
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
    <div class="panel-actions">
      <button class="button button-secondary full-width" type="button" id="baSelectFailing">${t('beamAxial.selectFailing')}</button>
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
}

async function runBeamAxialCheck() {
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
    log(describeError(error), 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

function renderBeamAxialResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="baExport" ${beamAxialState.lastResults.length ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
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
  $('#baExport', panel).addEventListener('click', beamAxialExportExcel);
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
  const baExportBtn = $('#baExport');
  if (baExportBtn) baExportBtn.disabled = results.length === 0;

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
    log(describeError(error), 'error');
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
    <div class="panel-actions">
      <button class="button button-secondary full-width" type="button" id="waSelectFailing">${t('wallAxial.selectFailing')}</button>
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
  $('#waSelectFailing', panel).addEventListener('click', wallAxialSelectFailing);
}

function renderWallAxialResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="waExport" ${wallAxialState.lastResults.length ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
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
  $('#waExport', panel).addEventListener('click', wallAxialExportExcel);
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
  const waExportBtn = $('#waExport');
  if (waExportBtn) waExportBtn.disabled = results.length === 0;

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
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
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
`;

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
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <button class="button button-secondary" type="button" id="wsExport" ${wallShearState.lastResults.length ? '' : 'disabled'}>${t('drift.export')}</button>
    </div>
    <div class="status-banner pending" id="wsStatusBanner">${t('columnAxial.status.pending')}</div>
    <div id="wsResultsWrap"></div>`;
  $('#wsExport', panel).addEventListener('click', wallShearExportExcel);
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

  const wsExportBtn = $('#wsExport');
  if (wsExportBtn) wsExportBtn.disabled = results.length === 0;

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
  if (!etabsConnected) { log(t('terminal.needConnection'), 'error'); return; }
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
    log(describeError(error), 'error');
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
    log(describeError(error), 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// ---------------------------------------------------------------------------
// Reporting (Raporlama) — the deliverable a design office actually hands over.
//
// The report is described as DATA, not as markup: a process owns an ordered list
// of steps, a step owns groups of fields, and a field is one of text / number /
// month / image / autotext. Rendering, the per-step progress badges, the process
// counters, template save-load and the bulk image mapper all read that schema, so
// adding the next page of the report outline is a schema entry rather than new UI
// code. That is the foundation the rest of the report is built on.
//
// Only the cover step is defined so far. The remaining steps are listed under the
// names they carry in the report outline and marked "to be defined" — their fields
// are added as the outline for each one is settled, never guessed at.
// ---------------------------------------------------------------------------

const REPORT_IMAGE_MAX_MB = 15;
// Cover images are usually a render or a photo; capping the long edge keeps the
// stored data URL inside the localStorage quota without a visible quality loss.
const REPORT_IMAGE_MAX_PX = 1600;

const reportProcesses = [
  { id: 'intro', labelKey: 'report.process.intro' },
  { id: 'beam', labelKey: 'report.process.beam', pending: true },
  { id: 'column', labelKey: 'report.process.column', pending: true },
  { id: 'wall', labelKey: 'report.process.wall', pending: true }
];

// Reference tables the report prints verbatim (TS 500 Table 4.1, TBDY 2018
// Table 5.1, and the rebar table of the report template). They are data because
// the document reproduces them as-is and highlights the engineer's selection.
const REPORT_TABLES = {
  concrete: {
    kind: 'rows',
    titleKey: 'report.table.concrete', noteKey: 'report.table.concrete.note',
    headKeys: ['report.col.concreteClass', 'report.col.fck', 'report.col.fctk', 'report.col.e', 'report.col.g'],
    rows: [
      ['C25', '25', '1.75', '30250', '12604'], ['C30', '30', '1.91', '31801', '13250'],
      ['C35', '35', '2.07', '33227', '13845'], ['C40', '40', '2.21', '34555', '14398'],
      ['C45', '45', '2.34', '35802', '14917'], ['C50', '50', '2.47', '36981', '15409'],
      ['C55', '55', '2.60', '38103', '15876'], ['C60', '60', '2.71', '39174', '16323'],
      ['C65', '65', '2.82', '40202', '16751'], ['C70', '70', '2.93', '41191', '17163']
    ]
  },
  // The rebar table is transposed in the report: one COLUMN per class.
  rebar: {
    kind: 'columns',
    titleKey: 'report.table.rebar', noteKey: 'report.table.rebar.note',
    classes: ['S220', 'S420', 'B420B', 'B420C', 'B500B', 'B500C', 'B500A'],
    rowKeys: ['report.row.surface', 'report.row.re', 'report.row.rm', 'report.row.rmRe',
      'report.row.reActNom', 'report.row.a5', 'report.row.agt'],
    values: [
      ['report.surface.plain', 'report.surface.ribbed', 'report.surface.ribbed', 'report.surface.ribbed',
        'report.surface.ribbed', 'report.surface.ribbed', 'report.surface.profiled'],
      ['220', '420', '420', '420', '500', '500', '500'],
      ['340', '500', '—', '—', '—', '—', '550'],
      ['≥1.20', '≥1.15', '≥1.08', '≥1.15 / <1.35', '≥1.08', '≥1.15 / <1.35', '—'],
      ['—', '1.30', '—', '1.30', '—', '1.30', '—'],
      ['18', '10', '12', '12', '12', '12', '5'],
      ['—', '—', '5', '7.5', '5', '7.5', '2.5']
    ]
  },
  soil: {
    kind: 'rows',
    titleKey: 'report.table.soil', noteKey: 'report.table.soil.note',
    headKeys: ['report.col.soilClass', 'report.col.soilType', 'report.col.vs30', 'report.col.n60', 'report.col.cu30'],
    rows: [
      ['ZA', 'report.soil.za', '>1500', '—', '—'],
      ['ZB', 'report.soil.zb', '760–1500', '—', '—'],
      ['ZC', 'report.soil.zc', '360–760', '>50', '>250'],
      ['ZD', 'report.soil.zd', '180–360', '15–50', '70–250'],
      ['ZE', 'report.soil.ze', '<180', '<15', '<70'],
      ['ZF', 'report.soil.zf', 'report.soil.siteSpecific', 'report.soil.siteSpecific', 'report.soil.siteSpecific']
    ]
  }
};

// Option lists whose wording ends up inside a report sentence, so each carries the
// label shown in the picker and the phrase the composer splices into the text.
const REPORT_OPTIONS = {
  systemClass: ['wallOnly', 'frameWall', 'frame'],
  foundationType: ['raft', 'strip', 'pad', 'pile'],
  slabSystem: ['flat', 'beam']
};

const REPORT_STOREY_ROLES = ['basement', 'ground', 'normal', 'roof', 'none'];

const reportSteps = {
  intro: [
    {
      id: 'cover',
      titleKey: 'report.step.cover.title',
      descKey: 'report.step.cover.desc',
      groups: [
        {
          titleKey: 'report.group.project',
          hintKey: 'report.group.project.hint',
          fields: [
            { id: 'il', type: 'text' }, { id: 'ilce', type: 'text' }, { id: 'mahalle', type: 'text' },
            { id: 'ada', type: 'text' }, { id: 'parsel', type: 'text' },
            { id: 'projectName', type: 'text', span: 2 }, { id: 'blockName', type: 'text', span: 2 }
          ]
        },
        {
          titleKey: 'report.group.texts',
          fields: [
            { id: 'coverIdentity', type: 'autotext', labelKey: 'report.text.identity', lines: ['coverLine1', 'coverLine2', 'coverLine3'] },
            { id: 'coverText', type: 'autotext', labelKey: 'report.text.cover' },
            { id: 'scopeText', type: 'autotext', labelKey: 'report.text.scope' },
            { id: 'footerText', type: 'autotext', labelKey: 'report.text.footer' }
          ]
        },
        {
          titleKey: 'report.group.image',
          badgeKey: 'report.optional',
          fields: [{
            id: 'coverImage', type: 'image', code: 'KAPAK', optional: true,
            labelKey: 'report.image.label', hintKey: 'report.image.hint'
          }]
        },
        {
          titleKey: 'report.group.date',
          fields: [{ id: 'month', type: 'month' }, { id: 'year', type: 'number', min: 2000, max: 2100 }]
        }
      ]
    },
    {
      id: 'intro1',
      titleKey: 'report.step.intro.title',
      descKey: 'report.step.intro.desc',
      groups: [
        {
          titleKey: 'report.group.storeys',
          hintKey: 'report.group.storeys.hint',
          fields: [{ id: 'storeyRoles', type: 'storeyRoles' }]
        },
        {
          titleKey: 'report.group.storeyHeights',
          hintKey: 'report.group.storeyHeights.hint',
          fields: [
            { id: 'basementH', type: 'number', min: 1.5, max: 10, unit: 'm' },
            { id: 'typicalH', type: 'number', min: 1.5, max: 10, unit: 'm' },
            { id: 'totalH', type: 'number', min: 1.5, max: 500, unit: 'm' }
          ]
        },
        {
          titleKey: 'report.group.texts',
          fields: [{ id: 'introText', type: 'autotext', labelKey: 'report.text.intro' }]
        },
        {
          titleKey: 'report.group.figures',
          badgeKey: 'report.optional',
          fields: [
            { id: 'figSection', type: 'image', code: 'S1-1', optional: true, figure: 'report.fig.s11' },
            { id: 'fig3d', type: 'image', code: 'S1-2', optional: true, figure: 'report.fig.s12' }
          ]
        }
      ]
    },
    {
      id: 'system',
      titleKey: 'report.step.system.title',
      descKey: 'report.step.system.desc',
      groups: [
        {
          titleKey: 'report.group.plan',
          hintKey: 'report.group.plan.hint',
          fields: [{ id: 'planExtent', type: 'measure' }]
        },
        {
          titleKey: 'report.group.systemClass',
          fields: [
            { id: 'systemClass', type: 'select', options: 'systemClass', span: 2 },
            { id: 'systemText', type: 'autotext', labelKey: 'report.text.system' }
          ]
        },
        {
          titleKey: 'report.group.foundation',
          hintKey: 'report.group.foundation.hint',
          fields: [
            { id: 'foundationType', type: 'select', options: 'foundationType', span: 2 },
            { id: 'foundationTower', type: 'number', min: 10, max: 500, unit: 'cm' },
            { id: 'foundationPark', type: 'number', min: 10, max: 500, unit: 'cm' },
            { id: 'foundationText', type: 'autotext', labelKey: 'report.text.foundation' }
          ]
        },
        {
          titleKey: 'report.group.slab',
          hintKey: 'report.group.slab.hint',
          fields: [
            { id: 'slabSystem', type: 'select', options: 'slabSystem', span: 2 },
            { id: 'slabThickness', type: 'text', source: 'model', span: 2 },
            { id: 'slabText', type: 'autotext', labelKey: 'report.text.slab' }
          ]
        },
        {
          titleKey: 'report.group.figures',
          badgeKey: 'report.optional',
          fields: [{ id: 'figPlan', type: 'image', code: 'S2-1', optional: true, figure: 'report.fig.s21' }]
        }
      ]
    },
    {
      id: 'material',
      titleKey: 'report.step.material.title',
      descKey: 'report.step.material.desc',
      groups: [
        {
          titleKey: 'report.group.concrete',
          hintKey: 'report.group.concrete.hint',
          fields: [
            { id: 'concreteClass', type: 'tableSelect', table: 'concrete' },
            { id: 'concreteText', type: 'autotext', labelKey: 'report.text.concrete' }
          ]
        },
        {
          titleKey: 'report.group.rebar',
          hintKey: 'report.group.rebar.hint',
          fields: [
            { id: 'rebarClass', type: 'tableSelect', table: 'rebar' },
            { id: 'rebarText', type: 'autotext', labelKey: 'report.text.rebar' }
          ]
        },
        {
          titleKey: 'report.group.soil',
          hintKey: 'report.group.soil.hint',
          fields: [{ id: 'soilClass', type: 'tableSelect', table: 'soil' }]
        }
      ]
    },
    {
      id: 'loads',
      titleKey: 'report.step.loads.title',
      descKey: 'report.step.loads.desc',
      groups: [
        {
          titleKey: 'report.group.loadBoxes',
          hintKey: 'report.group.loadBoxes.hint',
          fields: [{ id: 'loadBoxes', type: 'loadBoxes' }]
        },
        {
          titleKey: 'report.group.planGroups',
          hintKey: 'report.group.planGroups.hint',
          fields: [{ id: 'planGroups', type: 'planGroups' }]
        },
        {
          titleKey: 'report.group.soilWater',
          hintKey: 'report.group.soilPressure.hint',
          fields: [{ id: 'soilPressure', type: 'soilPressure' }]
        }
      ]
    },
    { id: 'seismic', titleKey: 'report.step.seismic.title', pending: true },
    { id: 'design', titleKey: 'report.step.design.title', pending: true },
    { id: 'model', titleKey: 'report.step.model.title', pending: true },
    { id: 'baseShear', titleKey: 'report.step.baseShear.title', pending: true },
    { id: 'drift', titleKey: 'report.step.drift.title', pending: true },
    { id: 'review', titleKey: 'report.step.review.title', pending: true }
  ],
  beam: [],
  column: [],
  wall: []
};

// A report text is composed from the shared project variables. Empty variables are
// left out of the sentence rather than printed as gaps, and a text whose variables
// are all empty stays empty — which is exactly what gets printed in the report.
// The cover and the running footer print the project identity in capitals however
// it was typed. Turkish needs the locale-aware mapping (i -> İ), never toUpperCase().
function reportUpper(value) {
  return String(value ?? '').trim().toLocaleUpperCase(currentLanguage === 'tr' ? 'tr-TR' : 'en-GB');
}

const reportComposers = {
  // The three identity lines of the cover, in the report template's own wording.
  // An empty variable drops its phrase; an empty line is simply not printed.
  coverLine1: f => [
    f.il && t('report.part.ilLine', { v: reportUpper(f.il) }),
    f.ilce && t('report.part.ilceLine', { v: reportUpper(f.ilce) })
  ].filter(Boolean).join(' '),

  coverLine2: f => [
    f.mahalle && t('report.part.mahalleLine', { v: reportUpper(f.mahalle) }),
    f.ada && t('report.part.adaLine', { v: reportUpper(f.ada) }),
    f.parsel && t('report.part.parselLine', { v: reportUpper(f.parsel) })
  ].filter(Boolean).join(' '),

  coverLine3: f => [reportUpper(f.projectName), reportUpper(f.blockName)].filter(Boolean).join(' '),

  coverText: () => t('report.template.cover'),

  scopeText: f => {
    const clean = key => String(f[key] ?? '').trim();
    const place = ['il', 'ilce', 'mahalle', 'ada', 'parsel']
      .filter(clean).map(key => t(`report.part.${key}`, { v: clean(key) })).join(', ');
    const project = clean('projectName'), block = clean('blockName');
    // "A Blok" already carries the word, so appending it again would read
    // "A Blok bloğuna". Pick the phrasing that does not repeat it.
    const named = /blo[kğ]|block/i.test(block) ? 'Named' : '';
    const subject = project && block ? t(`report.part.projectBlock${named}`, { project, block })
      : project ? t('report.part.project', { project })
      : block ? t(`report.part.block${named}`, { block })
      : place ? t('report.part.structure') : '';
    if (!subject) return '';
    return t('report.template.scope', { location: place ? t('report.part.location', { v: place }) : '', subject })
      .replace(/\s+/g, ' ').replace(/\s+([.,])/g, '$1').trim();
  },

  // The running footer of the inner pages: the same three identity lines on one
  // line. The cover itself carries no footer, so it is not shown in the preview.
  footerText: f => [
    reportComposers.coverLine1(f), reportComposers.coverLine2(f), reportComposers.coverLine3(f)
  ].filter(Boolean).join(' '),

  // 1 Introduction — storey make-up and heights.
  introText: f => {
    const counts = reportStoreyCounts();
    const num = key => { const v = Number(f[key]); return Number.isFinite(v) && v > 0 ? String(v) : ''; };
    const parts = [];
    const make = [
      counts.basement && t('report.part.basementCount', { n: counts.basement }),
      counts.ground && t('report.part.groundCount', { n: counts.ground }),
      counts.normal && t('report.part.normalCount', { n: counts.normal }),
      counts.roof && t('report.part.roofCount', { n: counts.roof })
    ].filter(Boolean);
    if (make.length) parts.push(t('report.template.storeyMakeup', { list: listPhrase(make) }));
    if (counts.basement && num('basementH')) {
      parts.push(t('report.template.basementHeight', { n: counts.basement, h: num('basementH') }));
    }
    if (num('typicalH')) {
      parts.push(t('report.template.typicalHeight', {
        h: num('typicalH'),
        total: num('totalH') ? t('report.part.totalHeight', { h: num('totalH') }) : ''
      }));
    }
    return joinSentences(parts);
  },

  // 2 Structural system — plan extents, system class and slab system.
  systemText: f => {
    const parts = [];
    const w = String(f.planWidth ?? '').trim(), d = String(f.planDepth ?? '').trim();
    if (w && d) parts.push(t('report.template.planExtent', { w, d }));
    parts.push(t('report.template.systemMembers'));
    if (f.systemClass) parts.push(t('report.template.systemClass', { v: t(`report.opt.systemClass.${f.systemClass}.phrase`) }));
    if (f.slabSystem) parts.push(t('report.template.slabMention', { v: t(`report.opt.slabSystem.${f.slabSystem}.phrase`) }));
    parts.push(t('report.template.systemContinuity'));
    return joinSentences(parts);
  },

  // 2.1 Foundation — a thickness that was not entered is left out of the sentence.
  foundationText: f => {
    if (!f.foundationType) return '';
    const parts = [t('report.template.foundationType', { v: t(`report.opt.foundationType.${f.foundationType}.phrase`) })];
    const tower = String(f.foundationTower ?? '').trim(), park = String(f.foundationPark ?? '').trim();
    const zones = [
      tower && t('report.part.foundationTower', { v: tower }),
      park && t('report.part.foundationPark', { v: park })
    ].filter(Boolean);
    if (zones.length) {
      parts.push(t('report.template.foundationThickness', {
        v: t(`report.opt.foundationType.${f.foundationType}.phrase`), list: listPhrase(zones)
      }));
    }
    return joinSentences(parts);
  },

  // 2.3 Slab — the whole paragraph switches with the chosen system, exactly as the
  // report template words it for flat-plate and beam-and-slab construction.
  slabText: f => {
    if (!f.slabSystem) return '';
    const name = t(`report.opt.slabSystem.${f.slabSystem}.phrase`);
    const thickness = String(f.slabThickness ?? '').trim();
    return joinSentences([
      t('report.template.slabUsed', { v: name }),
      t('report.template.slabLimits', {
        v: name,
        thickness: thickness ? t('report.part.slabThickness', { v: thickness }) : ''
      })
    ]);
  },

  concreteText: f => {
    if (!f.concreteClass) return '';
    const row = REPORT_TABLES.concrete.rows.find(r => r[0] === f.concreteClass);
    return t('report.template.concrete', { fck: row ? row[1] : '', v: f.concreteClass });
  },

  rebarText: f => {
    if (!f.rebarClass) return '';
    const idx = REPORT_TABLES.rebar.classes.indexOf(f.rebarClass);
    const fy = idx >= 0 ? REPORT_TABLES.rebar.values[1][idx] : '';
    const surfaceKey = idx >= 0 ? REPORT_TABLES.rebar.values[0][idx] : '';
    return t('report.template.rebar', { fy, v: f.rebarClass, surface: surfaceKey ? t(surfaceKey).toLocaleLowerCase(currentLanguage === 'tr' ? 'tr-TR' : 'en-GB') : '' });
  }
};

// "a, b and c" / "a, b ve c" — the report writes these as running prose.
function listPhrase(items) {
  if (items.length <= 1) return items[0] || '';
  return items.slice(0, -1).join(', ') + ' ' + t('report.part.and') + ' ' + items[items.length - 1];
}

function joinSentences(parts) {
  return parts.filter(Boolean).join(' ').replace(/\s+/g, ' ').replace(/\s+([.,])/g, '$1').trim();
}

// Storey roles the engineer marked, counted per role.
function reportStoreyCounts() {
  const roles = reportState.storeyRoles || {};
  const counts = { basement: 0, ground: 0, normal: 0, roof: 0 };
  for (const role of Object.values(roles)) if (counts[role] !== undefined) counts[role]++;
  return counts;
}

function reportDefaults() {
  return {
    process: 'intro', step: 'cover', fields: {}, texts: {}, images: {}, fontScale: 100,
    // Storey name -> basement / ground / normal / roof / none, marked by the engineer
    // against the storey list read from the connected model.
    storeyRoles: {}, stories: [], modelHints: {}, modelError: '',
    // Load patterns and wall sections read from the model, plus the plan-view
    // groups: each group is one drawing that represents the storeys inside it.
    loadPatterns: [], wallSections: [], planGroups: [], captureLog: [], captureKind: '',
    // Fields the engineer has typed into. Model-derived defaults keep refreshing
    // until then; once a field is here, nothing overwrites it automatically.
    touched: {}
  };
}

function loadReportState() {
  try {
    const stored = JSON.parse(localStorage.getItem('sea-report'));
    return stored && typeof stored === 'object' ? { ...reportDefaults(), ...stored } : reportDefaults();
  } catch {
    return reportDefaults();
  }
}

function saveReportState() {
  try {
    localStorage.setItem('sea-report', JSON.stringify(reportState));
  } catch {
    // Images are the only thing large enough to blow the quota; the typed
    // information matters more, so keep that and drop the images from storage.
    try { localStorage.setItem('sea-report', JSON.stringify({ ...reportState, images: {} })); } catch { /* give up */ }
  }
}

const reportState = loadReportState();

function reportActiveSteps() {
  return reportSteps[reportState.process] || [];
}

function reportActiveStep() {
  const steps = reportActiveSteps();
  return steps.find(s => s.id === reportState.step) || steps[0] || { id: '', titleKey: '', pending: true };
}

function reportStepFields(step) {
  return (step.groups || []).flatMap(group => group.fields);
}

function reportFieldFilled(field) {
  if (field.type === 'image') return !!reportState.images[field.code];
  // Storey roles count as filled once at least one storey has been classified.
  if (field.type === 'storeyRoles') return Object.values(reportState.storeyRoles || {}).some(r => r && r !== 'none');
  if (field.type === 'measure') {
    return ['planWidth', 'planDepth'].every(k => String(reportState.fields[k] ?? '').trim() !== '');
  }
  return String(reportState.fields[field.id] ?? '').trim() !== '';
}

// Autotext fields are derived, so they are never counted as something to fill in.
function reportEmptyCount(step) {
  return reportStepFields(step).filter(f => f.type !== 'autotext' && !f.optional && !reportFieldFilled(f)).length;
}

function reportProcessCounts() {
  const fields = reportActiveSteps().flatMap(reportStepFields);
  const isVariable = f => !['image', 'autotext', 'tableSelect'].includes(f.type);
  return {
    // A measure field carries two numbers; storey roles carry one per storey.
    variables: fields.filter(isVariable).reduce((sum, f) => sum + (f.type === 'measure' ? 2 : 1), 0),
    images: fields.filter(f => f.type === 'image').length,
    tables: fields.filter(f => f.type === 'tableSelect').length
  };
}

function reportComposedText(id) {
  if (reportState.texts[id] !== undefined) return reportState.texts[id];
  const composer = reportComposers[id];
  return composer ? composer(reportState.fields) : '';
}

// A multi-line auto-text (the cover identity block) composes each of its lines and
// drops the empty ones, so the cover never prints a blank line.
function reportComposedLines(field) {
  if (reportState.texts[field.id] !== undefined) {
    return String(reportState.texts[field.id]).split('\n').filter(line => line.trim());
  }
  return (field.lines || []).map(id => reportComposers[id](reportState.fields)).filter(Boolean);
}

function reportMonthNames() {
  const fmt = new Intl.DateTimeFormat(currentLanguage === 'tr' ? 'tr-TR' : 'en-GB', { month: 'long' });
  return Array.from({ length: 12 }, (_, i) => fmt.format(new Date(2000, i, 1)));
}

// "AĞUSTOS 2026" — the report prints the month in capitals, so the picker shows
// it in title case but the cover does not.
function reportDateLabel() {
  const month = Number(reportState.fields.month);
  const year = String(reportState.fields.year ?? '').trim();
  const name = month >= 1 && month <= 12 ? reportUpper(reportMonthNames()[month - 1]) : '';
  return [name, year].filter(Boolean).join(' ');
}

// Report-scoped DOM ids, so a field called "year" cannot collide with a module input.
function reportDomId(fieldId) {
  return 'rep' + fieldId.charAt(0).toUpperCase() + fieldId.slice(1);
}

function renderReportModule() {
  renderReportHeadingTools();
  renderReportStructurePanel();
  renderReportEditorPanel();
  // Model-backed fields fill themselves in as soon as a model is connected; nothing
  // is read (or shown) while it is not, so a stale value can never look live.
  // Read once per connection — re-reading on every step change would re-query the
  // agent (and re-report any failure) on every click.
  if (etabsConnected && !reportModelLoaded) reportLoadModelData();
}

// Reads the storey list, the materials assigned in the model and the slab
// thicknesses in one pass. Everything it produces is a *suggestion*: the engineer's
// own entry always wins, because an automatic read can pick the wrong material.
// Drops everything the report read from a model, so the next connection cannot
// inherit it. What the engineer typed is kept — only model-derived data goes.
function reportForgetModelData() {
  reportModelLoaded = false;
  reportState.stories = [];
  reportState.loadPatterns = [];
  reportState.wallSections = [];
  reportState.modelHints = {};
  reportState.modelError = '';
}

async function reportLoadModelData() {
  if (reportModelLoading) return;
  reportModelLoading = true;
  reportState.modelError = '';
  try {
    // Always re-read: this runs once per connection, and the storeys must come
    // from the model that is connected now, not from whatever was here before.
    const res = await fetchAgentJson('/api/etabs/stories');
    if (res.etabsConnected) {
      reportState.stories = (res.stories || []).slice().sort((a, b) => a.elevation - b.elevation);
      // Roles and plan groups name storeys; drop the ones this model does not have.
      const live = new Set(reportState.stories.map(s => s.name));
      for (const name of Object.keys(reportState.storeyRoles || {})) {
        if (!live.has(name)) delete reportState.storeyRoles[name];
      }
      reportState.planGroups = (reportState.planGroups || [])
        .map(g => ({ ...g, stories: g.stories.filter(n => live.has(n)) }))
        .filter(g => g.stories.length > 0);
      reportPrefillStoreyHeights();
    }
    // Load patterns and wall sections drive the Loads step; a failure there must
    // not stop the materials read, so they are requested on their own.
    try {
      const lp = await fetchAgentJson('/api/etabs/load-patterns', 15000);
      if (lp.etabsConnected) reportState.loadPatterns = (lp.patterns || []).map(p => p.name).filter(Boolean);
      const ws = await fetchAgentJson('/api/etabs/wall-sections', 15000);
      if (ws.etabsConnected) reportState.wallSections = ws.names || [];
    } catch { /* reported through modelError below if materials fail too */ }

    const info = await fetchAgentJson('/api/etabs/model-materials', 20000);
    if (info.etabsConnected) {
      reportState.modelHints = {
        concreteClass: info.concreteClass || '',
        rebarClass: info.rebarClass || ''
      };
      if (info.slabThickness && !reportState.touched.slabThickness) {
        reportState.fields.slabThickness = info.slabThickness;
      }
      // Only pre-select a class the engineer has not overridden themselves.
      for (const key of ['concreteClass', 'rebarClass']) {
        if (reportState.modelHints[key] && !reportState.touched[key]) reportState.fields[key] = reportState.modelHints[key];
      }
    }
    reportModelLoaded = true;
    saveReportState();
    renderReportStructurePanel();
    renderReportEditorPanel();
  } catch (error) {
    // The report works perfectly well without the model, so a failed read is not a
    // toast on every click — it is a note next to the fields it would have filled.
    // A 404 means the running agent predates these endpoints and needs updating.
    reportState.modelError = /404/.test(error.message) ? t('report.model.agentOld') : describeError(error);
    reportModelLoaded = true;
    renderReportEditorPanel();
  } finally {
    reportModelLoading = false;
  }
}

let reportModelLoading = false;
let reportModelLoaded = false;

// X and Y span of a middle storey — the figure the report quotes as the standard
// floor plan extent. A middle storey is used because the base and the roof are
// usually not representative of the typical floor.
async function reportMeasurePlanExtent() {
  const button = $('#repMeasurePlan');
  const note = $('#repMeasureNote');
  if (button) button.disabled = true;
  if (note) note.textContent = t('report.measure.working');
  try {
    const res = await fetchAgentJson('/api/etabs/plan-extent', 25000);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    if (!(res.width > 0) || !(res.depth > 0)) throw new Error(t('report.measure.empty'));
    reportState.fields.planWidth = res.width.toFixed(2);
    reportState.fields.planDepth = res.depth.toFixed(2);
    saveReportState();
    renderReportEditorPanel();
    log(t('report.measure.done', { story: res.story || '' }), 'ok');
  } catch (error) {
    const message = /404/.test(error.message) ? t('report.model.agentOld') : describeError(error);
    if (note) note.textContent = message;
    log(message, 'error');
  } finally {
    const again = $('#repMeasurePlan');
    if (again) again.disabled = false;
  }
}

// --- Capturing views from ETABS ---------------------------------------------
// The v1 API has no view control and no picture export (cView offers only
// RefreshView / RefreshWindow), so ETABS cannot be driven to a plan view or told
// to show shell loads from here. What works is to photograph the window the
// engineer has set up: the queue below names each view the report needs, and each
// one is captured with a single click once ETABS is showing it.
function reportViewQueue() {
  const stories = (reportState.stories || []).slice().reverse();
  const groups = reportState.planGroups || [];
  const grouped = new Set(groups.flatMap(g => g.stories));
  const targets = [
    ...groups.map((g, i) => ({ code: `S6-G${i + 1}`, label: g.stories.join(' · '), stories: g.stories })),
    ...stories.filter(s => !grouped.has(s.name)).map(s => ({ code: `S6-${s.name}`, label: s.name, stories: [s.name] }))
  ];
  // One view per target per selected load pattern, which is what section 6 prints.
  const patterns = [...new Set(REPORT_LOAD_BOXES
    .flatMap(box => [...reportLoadSelection(`${box.id}Rigid`), ...reportLoadSelection(`${box.id}Other`)]))];
  return targets.flatMap(target => patterns.map(pattern => ({
    key: `${target.code}::${pattern}`,
    target: target.label,
    stories: target.stories,
    pattern
  })));
}

// Runs a whole queue: ETABS is driven to each view and photographed, one after the
// other. A view that fails is recorded with its reason and the run carries on, so
// one bad row cannot cost the whole batch.
let reportCaptureRunning = false;

async function reportRunCaptureQueue(items, kind) {
  if (reportCaptureRunning) return;
  if (items.length === 0) { log(t(kind === 'soil' ? 'report.soilP.needPattern' : 'report.pg.nothingToDo'), 'error'); return; }

  reportCaptureRunning = true;
  reportState.captureKind = kind;
  reportState.captureLog = items.map(item => ({ key: item.key, label: item.label, state: 'waiting' }));
  saveReportState();
  renderReportEditorPanel();

  let done = 0;
  for (let i = 0; i < items.length; i++) {
    const item = items[i];
    reportState.captureLog[i].state = 'working';
    reportPaintCaptureLog();
    try {
      const res = await postAgentJson('/api/etabs/auto-view', {
        kind,
        story: item.story || '',
        loadPattern: item.pattern || '',
        wallPrefix: REPORT_BASEMENT_WALL_PREFIX
      }, 90000);
      if (!res.etabsConnected || !res.image) throw new Error(res.error || t('drift.error.notConnected'));
      reportState.images[item.key] = res.image;
      reportState.captureLog[i].state = 'ok';
      done++;
    } catch (error) {
      reportState.captureLog[i].state = 'fail';
      reportState.captureLog[i].reason = /404/.test(error.message) ? t('report.model.agentOld') : describeError(error);
    }
    reportPaintCaptureLog();
    saveReportState();
  }

  reportCaptureRunning = false;
  renderReportModule();
  log(t('report.capture.summary', { done, total: items.length }), done === items.length ? 'ok' : 'error');
}

function reportPaintCaptureLog() {
  const host = $('#repCaptureLog');
  if (host) host.innerHTML = reportCaptureLogRows();
}

function reportGenerateLoadViews() {
  // A grouped view is captured on its first storey — the group shares one drawing.
  reportRunCaptureQueue(reportViewQueue().map(item => ({
    key: item.key, label: t('report.capture.row', { target: item.target, pattern: item.pattern }),
    story: item.stories[0], pattern: item.pattern
  })), 'loads');
}

function reportGenerateSoilViews() {
  reportRunCaptureQueue([reportState.fields.soilStatic, reportState.fields.soilDynamic]
    .filter(Boolean)
    .map(p => ({ key: `S65::${p}`, label: t('report.capture.row', { target: t('report.soilP.walls'), pattern: p }), pattern: p })),
  'soil');
}

// Report-wide controls live in the page heading, next to the Dashboard button.
function renderReportHeadingTools() {
  const slot = $('#moduleHeadingSlot');
  if (!slot) return;
  slot.innerHTML = `
    <div class="rep-toolbar">
      <div class="rep-toolbar-field">
        <label for="repFontScale">${t('report.settings.fontScale')}</label>
        <select id="repFontScale">
          ${[90, 95, 100, 105, 110].map(v => `<option value="${v}"${v === reportState.fontScale ? ' selected' : ''}>${v}%</option>`).join('')}
        </select>
      </div>
      <button class="button button-secondary" type="button" id="repSaveTemplate">${t('report.template.save')}</button>
      <button class="button button-secondary" type="button" id="repLoadTemplate">${t('report.template.load')}</button>
      <button class="button button-secondary" type="button" id="repReset">${t('report.reset')}</button>
      <input type="file" id="repTemplateFile" accept="application/json,.json" hidden>
    </div>`;

  const scale = $('#repFontScale', slot);
  scale.addEventListener('change', () => {
    reportState.fontScale = Number(scale.value) || 100;
    saveReportState();
    reportRefreshDerived();
  });

  const templateFile = $('#repTemplateFile', slot);
  $('#repSaveTemplate', slot).addEventListener('click', reportSaveTemplate);
  $('#repLoadTemplate', slot).addEventListener('click', () => templateFile.click());
  templateFile.addEventListener('change', () => {
    if (templateFile.files[0]) reportLoadTemplate(templateFile.files[0]);
    templateFile.value = '';
  });

  $('#repReset', slot).addEventListener('click', () => {
    if (!window.confirm(t('report.reset.confirm'))) return;
    Object.assign(reportState, reportDefaults());
    saveReportState();
    renderReportModule();
    log(t('report.reset.done'), 'ok');
  });
}

// --- Panel 1: the report outline -------------------------------------------
function renderReportStructurePanel() {
  const panel = $('#setupPanel');
  const steps = reportActiveSteps();
  const active = reportActiveStep();
  const counts = reportProcessCounts();
  const process = reportProcesses.find(p => p.id === reportState.process) || reportProcesses[0];

  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('report.panel.structure.title')}</h2><p>${t('report.panel.structure.desc')}</p></div></div></div>
    <div class="rep-processes">
      ${reportProcesses.map(p => `
        <button type="button" class="rep-process${p.id === reportState.process ? ' on' : ''}" data-process="${p.id}"
                ${p.pending ? `disabled title="${escapeHtml(t('report.process.pending'))}"` : ''}>${t(p.labelKey)}</button>`).join('')}
    </div>
    <div class="rep-summary">
      <p class="rep-summary-label">${t('report.activeProcess')}</p>
      <strong>${t(process.labelKey)}</strong>
      <div class="rep-counts">
        <span><strong>${counts.variables}</strong><small>${t('report.count.variables')}</small></span>
        <span><strong>${counts.images}</strong><small>${t('report.count.images')}</small></span>
        <span><strong>${counts.tables}</strong><small>${t('report.count.tables')}</small></span>
      </div>
    </div>
    <ol class="rep-steps">${steps.map((step, index) => reportStepItem(step, index, active)).join('')}</ol>
    <p class="rep-note">${t('report.saved')}</p>`;

  $$('[data-process]', panel).forEach(btn => btn.addEventListener('click', () => {
    reportState.process = btn.dataset.process;
    reportState.step = (reportActiveSteps()[0] || {}).id || '';
    saveReportState();
    renderReportModule();
  }));

  $$('[data-step]', panel).forEach(btn => btn.addEventListener('click', () => {
    reportState.step = btn.dataset.step;
    saveReportState();
    renderReportModule();
  }));

}

function reportStepItem(step, index, active) {
  const isActive = step.id === active.id;
  const empty = step.pending ? -1 : reportEmptyCount(step);
  const badge = step.pending ? t('report.badge.pending')
    : isActive ? t('report.badge.active')
    : empty > 0 ? t('report.badge.empty', { count: empty })
    : t('report.badge.done');
  const mark = step.pending ? '○' : empty === 0 ? '✓' : '•';
  return `<li>
      <button type="button" class="rep-step${isActive ? ' on' : ''}${step.pending ? ' pending' : ''}" data-step="${step.id}">
        <span class="rep-step-mark">${mark}</span>
        <span class="rep-step-text"><strong>${index + 1}. ${t(step.titleKey)}</strong><small>${badge}</small></span>
      </button>
    </li>`;
}

// --- Panel 2: the active step ------------------------------------------------
function renderReportEditorPanel() {
  const panel = $('#resultsPanel');
  const steps = reportActiveSteps();
  const step = reportActiveStep();
  const index = steps.findIndex(s => s.id === step.id);
  const processLabel = t((reportProcesses.find(p => p.id === reportState.process) || reportProcesses[0]).labelKey);

  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div>
      <h2>${t('report.panel.editor.title')}</h2>
      <p>${t('report.stepOf', {
        process: currentLanguage === 'tr' ? processLabel.toLocaleUpperCase('tr-TR') : processLabel.toUpperCase(),
        index: index + 1, total: steps.length
      })}</p>
    </div></div></div>
    <div class="rep-workspace${step.id === 'cover' ? '' : ' single'}">
      <div class="rep-form">
        <h3 class="rep-step-title">${t(step.titleKey)}</h3>
        ${step.descKey ? `<p class="rep-step-desc">${t(step.descKey)}</p>` : ''}
        ${step.pending ? reportPendingBlock() : reportStepBody(step)}
      </div>
      ${step.id === 'cover' ? `
      <aside class="rep-preview">
        <p class="rep-preview-label">${t('report.preview.title')}</p>
        <div id="repPreviewSlot">${reportCoverPreview()}</div>
      </aside>` : ''}
    </div>
    <div class="rep-nav">
      <button class="button button-secondary" type="button" id="repPrev"${index <= 0 ? ' disabled' : ''}>${t('report.prev')}</button>
      <button class="button button-primary" type="button" id="repNext"${index >= steps.length - 1 ? ' disabled' : ''}>${t('report.next')}</button>
    </div>`;

  bindReportStep(panel, step);

  $('#repPrev', panel).addEventListener('click', () => reportGoToStep(index - 1, false));
  $('#repNext', panel).addEventListener('click', () => reportGoToStep(index + 1, true));
}

function reportPendingBlock() {
  return `<div class="rep-pending">
      <strong>${t('report.step.pending.title')}</strong>
      <p>${t('report.step.pending.text')}</p>
    </div>`;
}

function reportStepBody(step) {
  return (step.groups || []).map(reportGroupHtml).join('');
}

// Field types that own their whole width and render their own block; everything
// else is packed into the shared two-column .field-grid.
const REPORT_BLOCK_TYPES = new Set([
  'autotext', 'image', 'storeyRoles', 'measure', 'tableSelect',
  'loadBoxes', 'planGroups', 'soilPressure'
]);

// The three load boxes of section 6, in the report template's own order and units.
const REPORT_LOAD_BOXES = [
  { id: 'l61', labelKey: 'report.load.61', kindKey: 'report.load.area', unit: 'kN/m²' },
  { id: 'l62', labelKey: 'report.load.62', kindKey: 'report.load.line', unit: 'kN/m' },
  { id: 'l63', labelKey: 'report.load.63', kindKey: 'report.load.area', unit: 'kN/m²' }
];

// A load box holds a LIST of patterns. Older saved reports stored a single string,
// so read through this rather than touching reportState.fields directly.
function reportLoadSelection(key) {
  const value = reportState.fields[key];
  if (Array.isArray(value)) return value;
  return value ? [value] : [];
}

// Table 6.1 of the template: the soil pressure assumed on the basement walls.
const REPORT_SOIL_PRESSURE = [
  { id: 'cohesionless', labelKey: 'report.soilP.cohesionless', overKey: 'report.soilP.fullHeight', formula: '0.2 (γ·Hb + q)' },
  { id: 'softStiff', labelKey: 'report.soilP.softStiff', overKey: 'report.soilP.split', formula: '0.2 (γ·Hb + q) / 0.3 (γ·Hb + q)' },
  { id: 'stiffHard', labelKey: 'report.soilP.stiffHard', overKey: 'report.soilP.fullHeight', formula: '0.3 (γ·Hb + q)' }
];

function reportGroupHtml(group) {
  // Order is preserved: consecutive grid fields are collected into one .field-grid,
  // and a block field flushes it, so a schema reads top-to-bottom as it renders.
  const chunks = [];
  let pending = [];
  const flush = () => {
    if (pending.length) chunks.push(`<div class="field-grid">${pending.map(reportPlainFieldHtml).join('')}</div>`);
    pending = [];
  };
  for (const field of group.fields) {
    if (!REPORT_BLOCK_TYPES.has(field.type)) { pending.push(field); continue; }
    flush();
    chunks.push(reportBlockFieldHtml(field));
  }
  flush();
  return `<section class="rep-group">
      <div class="rep-group-head">
        <h4>${t(group.titleKey)}</h4>
        ${group.badgeKey ? `<span class="rep-badge">${t(group.badgeKey)}</span>` : ''}
      </div>
      ${group.hintKey ? `<p class="rep-group-hint">${t(group.hintKey)}</p>` : ''}
      <div class="rep-group-body">${chunks.join('')}</div>
    </section>`;
}

function reportBlockFieldHtml(field) {
  if (field.type === 'image') return reportImageFieldHtml(field);
  if (field.type === 'autotext') return reportAutoTextHtml(field);
  if (field.type === 'storeyRoles') return reportStoreyRolesHtml(field);
  if (field.type === 'measure') return reportMeasureHtml(field);
  if (field.type === 'tableSelect') return reportTableSelectHtml(field);
  if (field.type === 'loadBoxes') return reportLoadBoxesHtml();
  if (field.type === 'planGroups') return reportPlanGroupsHtml();
  if (field.type === 'soilPressure') return reportSoilPressureHtml();
  return '';
}

// --- 6.1-6.3 load boxes ------------------------------------------------------
// The pattern names offered are the model's own — nothing is hard-coded. With the
// rigid-basement rule on, each load carries two selections (basement / other
// storeys); with it off, one. Exactly one pattern per selection, since the report
// figure is drawn for a single load pattern.
function reportLoadBoxesHtml() {
  const patterns = reportState.loadPatterns || [];
  const rigidOn = !!reportState.fields.loadRigid;
  const stories = (reportState.stories || []).slice().reverse();

  const chips = key => {
    const selected = reportLoadSelection(key);
    return patterns.length
      ? `<div class="rep-chips" data-chipset="${key}">
          ${patterns.map(p => {
            const on = selected.includes(p);
            return `<button type="button" class="rep-chip${on ? ' on' : ''}" data-chip="${escapeHtml(p)}" aria-pressed="${on}">${escapeHtml(p)}</button>`;
          }).join('')}
        </div>`
      : `<p class="rep-chip-empty">${t(etabsConnected ? 'report.load.noPatterns' : 'report.load.needConnection')}</p>`;
  };

  return `<div class="rep-loads">
      <div class="rigid-row rep-rigid-row">
        <span class="rigid-title">${t('report.load.rigidTitle')}</span>
        <div class="rep-rigid-right">
          ${rigidOn ? `
            <label class="rep-rigid-story" for="repLoadRigidStory">${t('report.load.limitStorey')}</label>
            <select id="repLoadRigidStory">
              <option value="">${t('report.select.empty')}</option>
              ${stories.map(s => `<option value="${escapeHtml(s.name)}"${reportState.fields.loadRigidStory === s.name ? ' selected' : ''}>${escapeHtml(s.name)}</option>`).join('')}
            </select>` : ''}
          <button type="button" class="switch toggle-switch" id="repLoadRigid" role="switch" aria-checked="${rigidOn}">
            <span class="switch-label off">${t('action.off')}</span>
            <span class="switch-label on">${t('action.on')}</span>
            <span class="switch-thumb" aria-hidden="true"></span>
          </button>
        </div>
      </div>
      <p class="rigid-note">${t('report.load.rigidNote')}</p>

      ${REPORT_LOAD_BOXES.map(box => `
        <div class="rep-load-box">
          <div class="rep-load-meta">
            <span class="rep-load-kind">${t(box.kindKey)}</span>
            <strong>${t(box.labelKey)}</strong>
            <span class="rep-load-unit">${box.unit}</span>
          </div>
          <div class="rep-load-picks">
            ${rigidOn ? `
              <div class="rep-load-pick">
                <span>${t('report.load.rigidLoads')}</span>
                ${chips(`${box.id}Rigid`)}
              </div>
              <div class="rep-load-pick">
                <span>${t('report.load.otherLoads')}</span>
                ${chips(`${box.id}Other`)}
              </div>`
            : `
              <div class="rep-load-pick">
                <span>${t('report.load.allLoads')}</span>
                ${chips(`${box.id}Other`)}
              </div>`}
          </div>
        </div>`).join('')}
    </div>`;
}

// --- Plan view groups --------------------------------------------------------
// Left: the model's storeys. Right: the groups. Storeys inside a group share one
// drawing; every storey left outside gets its own. That is the whole rule.
function reportPlanGroupsHtml() {
  const stories = (reportState.stories || []).slice().reverse();
  if (stories.length === 0) {
    return `<p class="rep-inline-note">${t(etabsConnected ? 'report.storeys.loading' : 'report.storeys.needConnection')}</p>`;
  }
  const groups = reportState.planGroups || [];
  const grouped = new Set(groups.flatMap(g => g.stories));
  const loose = stories.filter(s => !grouped.has(s.name));

  return `<div class="rep-plangroups">
      <div class="rep-pg-cols">
        <div class="rep-pg-col">
          <p class="rep-pg-head">${t('report.pg.storeys')} <small>${t('report.pg.storeysHint', { count: loose.length })}</small></p>
          <div class="rep-pg-list" id="repPgStoreys">
            ${stories.map(s => `
              <label class="rep-pg-item${grouped.has(s.name) ? ' used' : ''}">
                <input type="checkbox" value="${escapeHtml(s.name)}"${grouped.has(s.name) ? ' disabled' : ''}>
                <span>${escapeHtml(s.name)}</span>
              </label>`).join('')}
          </div>
          <button class="button button-secondary full-width" type="button" id="repPgAdd">${t('report.pg.makeGroup')}</button>
        </div>
        <div class="rep-pg-col">
          <p class="rep-pg-head">${t('report.pg.groups')} <small>${t('report.pg.groupsHint')}</small></p>
          <div class="rep-pg-groups">
            ${groups.length === 0 ? `<p class="rep-pg-empty">${t('report.pg.noGroups')}</p>` : groups.map((g, i) => `
              <div class="rep-pg-group">
                <div class="rep-pg-group-head">
                  <strong>${t('report.pg.groupName', { n: i + 1 })}</strong>
                  <button type="button" class="text-button" data-ungroup="${i}">${t('report.pg.dissolve')}</button>
                </div>
                <p>${g.stories.map(escapeHtml).join(' · ')}</p>
              </div>`).join('')}
          </div>
          <p class="rep-pg-note">${t('report.pg.looseNote', { count: loose.length })}</p>
        </div>
      </div>
      <div class="rep-pg-actions">
        <button class="button button-primary" type="button" id="repGenLoadViews"${etabsConnected ? '' : ' disabled'}>${t('report.pg.generate')}</button>
        <span class="rep-measure-note" id="repGenNote">${etabsConnected ? t('report.pg.generateHint', { count: groups.length + loose.length }) : t('report.measure.needConnection')}</span>
      </div>
      ${reportState.captureKind === 'loads' ? reportCaptureLogHtml() : ''}
    </div>`;
}

// Progress of an automatic run: one row per view, marked as it is captured.
function reportCaptureLogHtml() {
  const rows = reportState.captureLog || [];
  if (rows.length === 0) return '';
  return `<div class="rep-capture">
      <p class="rep-pg-head">${t('report.capture.title')}</p>
      <div id="repCaptureLog">${reportCaptureLogRows()}</div>
    </div>`;
}

function reportCaptureLogRows() {
  const mark = { waiting: '·', working: '…', ok: '✓', fail: '!' };
  return `<ul class="rep-capture-list">
      ${(reportState.captureLog || []).map(row => `
        <li class="cap-${row.state}">
          <span class="rep-cap-mark">${mark[row.state] || '·'}</span>
          <div class="rep-capture-info">
            <strong>${escapeHtml(row.label)}</strong>
            ${row.reason ? `<small>${escapeHtml(row.reason)}</small>` : ''}
          </div>
          ${reportState.images[row.key] ? `<img src="${reportState.images[row.key]}" alt="">` : ''}
        </li>`).join('')}
    </ul>`;
}

function reportSoilPressureHtml() {
  const current = reportState.fields.soilPressure || '';
  const patterns = reportState.loadPatterns || [];
  const select = (id, labelKey) => `<div class="field">
      <label for="${reportDomId(id)}">${t(labelKey)}</label>
      <select id="${reportDomId(id)}">
        <option value="">${t('report.select.empty')}</option>
        ${patterns.map(p => `<option value="${escapeHtml(p)}"${reportState.fields[id] === p ? ' selected' : ''}>${escapeHtml(p)}</option>`).join('')}
      </select>
    </div>`;

  return `<div class="rep-soilp">
      <p class="rep-pg-head">${t('report.soilP.tableTitle')}</p>
      <div class="rep-soilp-cards">
        ${REPORT_SOIL_PRESSURE.map(row => `
          <button type="button" class="rep-soilp-card${current === row.id ? ' on' : ''}" data-soilp="${row.id}" aria-pressed="${current === row.id}">
            <strong>${t(row.labelKey)}</strong>
            <small>${t(row.overKey)}</small>
            <code>${row.formula}</code>
          </button>`).join('')}
      </div>
      <div class="field-grid">
        ${select('soilStatic', 'report.soilP.static')}
        ${select('soilDynamic', 'report.soilP.dynamic')}
      </div>
      <div class="rep-pg-actions">
        <button class="button button-primary" type="button" id="repGenSoilViews"${etabsConnected ? '' : ' disabled'}>${t('report.soilP.generate')}</button>
        <span class="rep-measure-note" id="repSoilNote">${etabsConnected ? t('report.soilP.generateHint', { prefix: REPORT_BASEMENT_WALL_PREFIX }) : t('report.measure.needConnection')}</span>
      </div>
      ${reportState.captureKind === 'soil' ? reportCaptureListHtml(
        [reportState.fields.soilStatic, reportState.fields.soilDynamic].filter(Boolean)
          .map(p => ({ key: `S65::${p}`, target: t('report.soilP.tableTitle'), pattern: p }))) : ''}
    </div>`;
}

// Basement-wall sections are recognised by this prefix on the wall section name.
const REPORT_BASEMENT_WALL_PREFIX = 'BAP';

// Storey roles: the storey list comes from the connected model, and marking each
// one basement / ground / normal / roof is what produces the counts the report
// sentence needs. Nothing is shown until a model is connected — a made-up storey
// list would be worse than none.
function reportStoreyRolesHtml(field) {
  const stories = reportState.stories || [];
  if (!etabsConnected && stories.length === 0) {
    return `<p class="rep-inline-note">${t('report.storeys.needConnection')}</p>`;
  }
  if (stories.length === 0) return `<p class="rep-inline-note">${t('report.storeys.loading')}</p>`;
  const counts = reportStoreyCounts();
  const roles = reportState.storeyRoles || {};
  return `<div class="rep-storeys" data-field="${field.id}">
      <table class="rep-storey-table"><tbody>
        ${stories.slice().reverse().map(s => `
          <tr>
            <th scope="row">${escapeHtml(s.name)}</th>
            <td class="rep-storey-elev">${Number.isFinite(s.elevation) ? `${s.elevation.toFixed(2)} m` : ''}</td>
            <td>
              <select class="rep-storey-role" data-storey="${escapeHtml(s.name)}" aria-label="${escapeHtml(s.name)}">
                ${REPORT_STOREY_ROLES.map(role => `<option value="${role}"${(roles[s.name] || 'none') === role ? ' selected' : ''}>${t(`report.role.${role}`)}</option>`).join('')}
              </select>
            </td>
          </tr>`).join('')}
      </tbody></table>
      <div class="rep-storey-counts">
        ${['basement', 'ground', 'normal', 'roof'].map(role =>
          `<span><strong>${counts[role]}</strong><small>${t(`report.role.${role}`)}</small></span>`).join('')}
      </div>
    </div>`;
}

// Plan extents are measured from the model rather than typed: the X and Y span of
// the points on a middle storey, which is what the report quotes.
function reportMeasureHtml(field) {
  const w = reportState.fields.planWidth ?? '';
  const d = reportState.fields.planDepth ?? '';
  return `<div class="rep-measure" data-field="${field.id}">
      <div class="field-grid">
        ${numberField('repPlanWidth', 'report.field.planWidth', { step: '0.01', min: 0, unit: 'm' })}
        ${numberField('repPlanDepth', 'report.field.planDepth', { step: '0.01', min: 0, unit: 'm' })}
      </div>
      <div class="rep-measure-actions">
        <button class="button button-secondary" type="button" id="repMeasurePlan"${etabsConnected ? '' : ' disabled'}>${t('report.measure.button')}</button>
        <span class="rep-measure-note" id="repMeasureNote">${escapeHtml(
          !etabsConnected ? t('report.measure.needConnection') : (reportState.modelError || ''))}</span>
      </div>
    </div>`.replace('id="repPlanWidth"', `id="repPlanWidth" value="${escapeHtml(w)}"`)
      .replace('id="repPlanDepth"', `id="repPlanDepth" value="${escapeHtml(d)}"`);
}

// A reference table with one selectable row (or column). The selection is what the
// report highlights, so picking it here IS the input — there is no separate list.
function reportTableSelectHtml(field) {
  const spec = REPORT_TABLES[field.table];
  const current = reportState.fields[field.id] ?? '';
  const auto = reportState.modelHints && reportState.modelHints[field.id];
  const autoNote = auto
    ? `<span class="rep-auto-note">${t(auto === current ? 'report.auto.matched' : 'report.auto.suggests', { v: auto })}</span>`
    : '';

  const body = spec.kind === 'columns'
    ? `<table class="rep-ref-table columns"><tbody>
        <tr><th scope="row">${t(spec.titleKey)}</th>
          ${spec.classes.map(c => `<th class="rep-pick${c === current ? ' on' : ''}" data-pick="${c}" tabindex="0" role="button" aria-pressed="${c === current}">${c}</th>`).join('')}</tr>
        ${spec.rowKeys.map((key, r) => `<tr><th scope="row">${t(key)}</th>
          ${spec.values[r].map((v, i) => `<td class="${spec.classes[i] === current ? 'on' : ''}">${escapeHtml(v.startsWith('report.') ? t(v) : v)}</td>`).join('')}</tr>`).join('')}
      </tbody></table>`
    : `<table class="rep-ref-table"><thead><tr>${spec.headKeys.map(k => `<th>${t(k)}</th>`).join('')}</tr></thead><tbody>
        ${spec.rows.map(row => `
          <tr class="rep-pick${row[0] === current ? ' on' : ''}" data-pick="${row[0]}" tabindex="0" role="button" aria-pressed="${row[0] === current}">
            ${row.map(cell => `<td>${escapeHtml(cell.startsWith('report.') ? t(cell) : cell)}</td>`).join('')}
          </tr>`).join('')}
      </tbody></table>`;

  return `<div class="rep-table-select" data-field="${field.id}" data-table="${field.table}">
      <div class="rep-table-head">
        <h5>${t(spec.titleKey)}</h5>
        <span class="rep-table-note">${t(spec.noteKey)}</span>
      </div>
      ${autoNote}
      <div class="table-wrap rep-table-wrap">${body}</div>
    </div>`;
}

function reportPlainFieldHtml(field) {
  const id = reportDomId(field.id);
  const labelKey = field.labelKey || `report.field.${field.id}`;
  if (field.type === 'number') {
    return numberField(id, labelKey, { step: field.unit === 'm' ? '0.01' : '1', min: field.min, max: field.max, unit: field.unit });
  }
  if (field.type === 'select') {
    const current = String(reportState.fields[field.id] ?? '');
    return `<div class="field${field.span > 1 ? ' span-2' : ''}">
        <label for="${id}">${t(labelKey)}</label>
        <select id="${id}">
          <option value="">${t('report.select.empty')}</option>
          ${REPORT_OPTIONS[field.options].map(opt => `<option value="${opt}"${current === opt ? ' selected' : ''}>${t(`report.opt.${field.options}.${opt}`)}</option>`).join('')}
        </select>
        <p class="field-error" id="${id}-err" role="alert" hidden></p>
      </div>`;
  }
  if (field.type === 'month') {
    const current = String(reportState.fields[field.id] ?? '');
    return `<div class="field">
        <label for="${id}">${t(labelKey)}</label>
        <select id="${id}">
          <option value=""></option>
          ${reportMonthNames().map((name, i) => `<option value="${i + 1}"${current === String(i + 1) ? ' selected' : ''}>${escapeHtml(name)}</option>`).join('')}
        </select>
        <p class="field-error" id="${id}-err" role="alert" hidden></p>
      </div>`;
  }
  const html = textField(id, labelKey, { span: field.span || 1 });
  // A model-backed field says so only when the read actually failed — otherwise the
  // value is simply there and needs no explanation.
  return field.source === 'model' && reportState.modelError
    ? html.replace('</div>', `<p class="rep-field-note">${escapeHtml(reportState.modelError)}</p></div>`)
    : html;
}

function reportAutoTextHtml(field) {
  const manual = reportState.texts[field.id] !== undefined;
  const value = field.lines ? reportComposedLines(field).join('\n') : reportComposedText(field.id);
  const empty = !manual && !value;
  return `<div class="rep-text" data-text="${field.id}">
      <div class="rep-text-head">
        <span>${t(field.labelKey)}</span>
        <button type="button" class="text-button" data-toggle-text="${field.id}">✎ ${t(manual ? 'report.text.auto' : 'report.text.manual')}</button>
      </div>
      ${manual
        ? `<textarea class="rep-textarea" id="${reportDomId(field.id)}" rows="3" aria-label="${escapeHtml(t(field.labelKey))}">${escapeHtml(value)}</textarea>`
        : `<p class="rep-text-body${empty ? ' muted' : ''}">${empty ? t('report.text.empty') : escapeHtml(value)}</p>`}
    </div>`;
}

function reportImageFieldHtml(field) {
  const src = reportState.images[field.code];
  // Figures carry the caption the report prints ("Şekil 1.1 Bina boy kesiti"),
  // so the slot is labelled with it rather than a generic name.
  const title = field.figure ? t(field.figure) : t(field.labelKey);
  const hint = field.figure ? t('report.fig.hint') : t(field.hintKey);
  return `<div class="rep-image" data-code="${field.code}">
      <div class="rep-image-head">
        <div><strong>${title}</strong><small>${hint}</small></div>
        <span class="rep-code">${t('report.image.code', { code: field.code })}</span>
      </div>
      <div class="rep-drop${src ? ' has' : ''}" data-drop="${field.code}" tabindex="0" role="button" aria-label="${escapeHtml(t('report.image.drop'))}">
        ${src
          ? `<img src="${src}" alt="">`
          : `<span class="rep-drop-icon" aria-hidden="true">▣</span><strong>${t('report.image.drop')}</strong><small>${t('report.image.limits', { max: REPORT_IMAGE_MAX_MB })}</small>`}
      </div>
      ${src ? `<button type="button" class="text-button rep-image-remove" data-remove="${field.code}">${t('report.image.remove')}</button>` : ''}
      <input type="file" data-file="${field.code}" accept="image/png,image/jpeg,image/webp" hidden>
    </div>`;
}

// The cover as the report template lays it out: three identity lines and the
// report title in capitals, then the image, then the date. The cover page carries
// no running footer, so none is drawn here either.
function reportCoverPreview() {
  const identityField = reportStepFields(reportActiveSteps().find(s => s.id === 'cover'))
    .find(f => f.id === 'coverIdentity');
  const lines = identityField ? reportComposedLines(identityField) : [];
  const title = reportComposedText('coverText');
  const date = reportDateLabel();
  const img = reportState.images.KAPAK;
  return `<div class="rep-page" style="--rep-scale:${(reportState.fontScale || 100) / 100}">
      <div class="rep-page-head">
        ${lines.map(line => `<p>${escapeHtml(line)}</p>`).join('')}
        ${title ? `<p>${escapeHtml(title)}</p>` : ''}
      </div>
      <div class="rep-page-image">${img ? `<img src="${img}" alt="">` : `<span>${t('report.preview.imageArea')}</span>`}</div>
      <p class="rep-page-date">${escapeHtml(date) || t('report.preview.date')}</p>
    </div>`;
}

// Repaints only what a keystroke can change — the preview and the auto-composed
// texts — so typing never re-renders the form and steals focus. The step badges
// are refreshed on `change` instead, once the field is left.
function reportRefreshDerived() {
  const slot = $('#repPreviewSlot');
  if (slot) slot.innerHTML = reportCoverPreview();
  const step = reportActiveStep();
  const byId = new Map(reportStepFields(step).map(f => [f.id, f]));
  $$('.rep-text').forEach(el => {
    const id = el.dataset.text;
    if (reportState.texts[id] !== undefined) return;
    const body = $('.rep-text-body', el);
    if (!body) return;
    const field = byId.get(id);
    const value = field && field.lines ? reportComposedLines(field).join('\n') : reportComposedText(id);
    body.classList.toggle('muted', !value);
    body.textContent = value || t('report.text.empty');
  });
}

function bindReportStep(panel, step) {
  for (const field of reportStepFields(step)) {
    if (field.type === 'image') { bindReportImageField(panel, field); continue; }
    if (field.type === 'storeyRoles') { bindReportStoreyRoles(panel); continue; }
    if (field.type === 'measure') { bindReportMeasure(panel); continue; }
    if (field.type === 'tableSelect') { bindReportTableSelect(panel, field); continue; }
    if (field.type === 'loadBoxes') { bindReportLoadBoxes(panel); continue; }
    if (field.type === 'planGroups') { bindReportPlanGroups(panel); continue; }
    if (field.type === 'soilPressure') { bindReportSoilPressure(panel); continue; }
    const el = $('#' + reportDomId(field.id), panel);
    if (!el) continue;
    if (field.type !== 'autotext') el.value = reportState.fields[field.id] ?? '';
    el.addEventListener('input', () => {
      if (field.type === 'autotext') {
        reportState.texts[field.id] = el.value;
      } else {
        reportState.fields[field.id] = el.value;
        reportState.touched[field.id] = true;
      }
      saveReportState();
      reportRefreshDerived();
    });
    // A <select> only fires `change`, so the derived texts have to be repainted
    // from there too, not just the step badges.
    el.addEventListener('change', () => {
      if (field.type === 'select') {
        reportState.fields[field.id] = el.value;
        saveReportState();
        renderReportEditorPanel();
      }
      renderReportStructurePanel();
    });
  }

  $$('[data-toggle-text]', panel).forEach(btn => btn.addEventListener('click', () => {
    const id = btn.dataset.toggleText;
    if (reportState.texts[id] !== undefined) delete reportState.texts[id];
    else reportState.texts[id] = reportComposedText(id);
    saveReportState();
    renderReportEditorPanel();
  }));

}

function bindReportStoreyRoles(panel) {
  $$('.rep-storey-role', panel).forEach(select => select.addEventListener('change', () => {
    reportState.storeyRoles[select.dataset.storey] = select.value;
    reportPrefillStoreyHeights();
    saveReportState();
    renderReportModule();
  }));
}

// Heights follow from the roles: the basement storeys share one height and the rest
// another, both read from the model's storey table. Only blank fields are filled —
// a value the engineer typed is never overwritten.
function reportPrefillStoreyHeights() {
  const stories = reportState.stories || [];
  if (stories.length === 0) return;
  const roles = reportState.storeyRoles || {};
  const heightOf = s => Number(s.height) || 0;
  const pick = list => {
    const values = list.map(heightOf).filter(h => h > 0);
    if (values.length === 0) return '';
    // The height quoted in the report is the one most storeys share.
    const tally = new Map();
    for (const v of values) tally.set(v, (tally.get(v) || 0) + 1);
    return String([...tally.entries()].sort((a, b) => b[1] - a[1] || b[0] - a[0])[0][0]);
  };
  const basement = stories.filter(s => roles[s.name] === 'basement');
  const above = stories.filter(s => ['ground', 'normal', 'roof'].includes(roles[s.name]));
  // Recomputed on every change of the marks, because the answer depends on them —
  // but never over a value the engineer typed.
  const setIfUntouched = (key, value) => {
    if (!reportState.touched[key]) reportState.fields[key] = value;
  };
  setIfUntouched('basementH', pick(basement));
  setIfUntouched('typicalH', pick(above));
  const total = [...basement, ...above].reduce((sum, s) => sum + heightOf(s), 0);
  setIfUntouched('totalH', total > 0 ? String(Number(total.toFixed(2))) : '');
}

function bindReportMeasure(panel) {
  ['planWidth', 'planDepth'].forEach(key => {
    const el = $('#' + reportDomId(key), panel);
    if (!el) return;
    el.addEventListener('input', () => {
      reportState.fields[key] = el.value;
      reportState.touched[key] = true;
      saveReportState();
      reportRefreshDerived();
    });
    el.addEventListener('change', renderReportStructurePanel);
  });
  const button = $('#repMeasurePlan', panel);
  if (button) button.addEventListener('click', reportMeasurePlanExtent);
}

function bindReportTableSelect(panel, field) {
  const root = $(`[data-field="${field.id}"]`, panel);
  if (!root) return;
  const choose = value => {
    // Clicking the selected row again clears it, so a wrong pick is undoable.
    reportState.fields[field.id] = reportState.fields[field.id] === value ? '' : value;
    // An explicit pick outranks whatever the model suggests from now on.
    reportState.touched[field.id] = true;
    saveReportState();
    renderReportModule();
  };
  $$('[data-pick]', root).forEach(cell => {
    cell.addEventListener('click', () => choose(cell.dataset.pick));
    cell.addEventListener('keydown', event => {
      if (event.key === 'Enter' || event.key === ' ') { event.preventDefault(); choose(cell.dataset.pick); }
    });
  });
}

function bindReportLoadBoxes(panel) {
  const rigid = $('#repLoadRigid', panel);
  if (rigid) rigid.addEventListener('click', () => {
    reportState.fields.loadRigid = !reportState.fields.loadRigid;
    saveReportState();
    renderReportModule();
  });

  const story = $('#repLoadRigidStory', panel);
  if (story) story.addEventListener('change', () => {
    reportState.fields.loadRigidStory = story.value;
    saveReportState();
    renderReportStructurePanel();
  });

  $$('[data-chipset]', panel).forEach(set => {
    const key = set.dataset.chipset;
    $$('[data-chip]', set).forEach(chip => chip.addEventListener('click', () => {
      // Several patterns can share one box — a figure is captured per pattern.
      const current = reportLoadSelection(key);
      const name = chip.dataset.chip;
      reportState.fields[key] = current.includes(name)
        ? current.filter(p => p !== name)
        : [...current, name];
      saveReportState();
      renderReportModule();
    }));
  });
}

function bindReportPlanGroups(panel) {
  const add = $('#repPgAdd', panel);
  if (add) add.addEventListener('click', () => {
    const picked = $$('#repPgStoreys input:checked', panel).map(input => input.value);
    if (picked.length < 1) { log(t('report.pg.needOne'), 'error'); return; }
    reportState.planGroups = [...(reportState.planGroups || []), { stories: picked }];
    saveReportState();
    renderReportModule();
  });

  $$('[data-ungroup]', panel).forEach(button => button.addEventListener('click', () => {
    const index = Number(button.dataset.ungroup);
    reportState.planGroups = (reportState.planGroups || []).filter((_, i) => i !== index);
    saveReportState();
    renderReportModule();
  }));

  const generate = $('#repGenLoadViews', panel);
  if (generate) generate.addEventListener('click', reportGenerateLoadViews);
}

function bindReportSoilPressure(panel) {
  $$('[data-soilp]', panel).forEach(card => card.addEventListener('click', () => {
    reportState.fields.soilPressure = reportState.fields.soilPressure === card.dataset.soilp ? '' : card.dataset.soilp;
    saveReportState();
    renderReportModule();
  }));

  ['soilStatic', 'soilDynamic'].forEach(id => {
    const el = $('#' + reportDomId(id), panel);
    if (!el) return;
    el.addEventListener('change', () => {
      reportState.fields[id] = el.value;
      saveReportState();
      renderReportStructurePanel();
    });
  });

  const generate = $('#repGenSoilViews', panel);
  if (generate) generate.addEventListener('click', reportGenerateSoilViews);
}

function bindReportImageField(panel, field) {
  const drop = $(`[data-drop="${field.code}"]`, panel);
  const file = $(`[data-file="${field.code}"]`, panel);
  const remove = $(`[data-remove="${field.code}"]`, panel);
  if (!drop || !file) return;

  drop.addEventListener('click', () => file.click());
  drop.addEventListener('keydown', event => {
    if (event.key === 'Enter' || event.key === ' ') { event.preventDefault(); file.click(); }
  });
  ['dragenter', 'dragover'].forEach(name => drop.addEventListener(name, event => {
    event.preventDefault();
    drop.classList.add('over');
  }));
  ['dragleave', 'dragend'].forEach(name => drop.addEventListener(name, () => drop.classList.remove('over')));
  drop.addEventListener('drop', event => {
    event.preventDefault();
    drop.classList.remove('over');
    const dropped = event.dataTransfer && event.dataTransfer.files[0];
    if (dropped) reportAcceptImage(field.code, dropped);
  });
  file.addEventListener('change', () => {
    const picked = file.files[0];
    file.value = '';
    if (picked) reportAcceptImage(field.code, picked);
  });

  if (remove) remove.addEventListener('click', () => {
    delete reportState.images[field.code];
    saveReportState();
    renderReportModule();
  });
}

// Validates the file, then downscales it so the report survives a page reload.
function reportReadImage(file) {
  return new Promise((resolve, reject) => {
    if (!/^image\/(png|jpeg|webp)$/.test(file.type)) return reject(new Error(t('report.image.type')));
    if (file.size > REPORT_IMAGE_MAX_MB * 1024 * 1024) return reject(new Error(t('report.image.size', { max: REPORT_IMAGE_MAX_MB })));
    const reader = new FileReader();
    reader.onerror = () => reject(new Error(t('report.image.readFailed')));
    reader.onload = () => {
      const image = new Image();
      image.onerror = () => reject(new Error(t('report.image.readFailed')));
      image.onload = () => {
        const ratio = Math.min(1, REPORT_IMAGE_MAX_PX / Math.max(image.width, image.height));
        if (ratio === 1 && file.size < 900 * 1024) return resolve(reader.result);
        const canvas = document.createElement('canvas');
        canvas.width = Math.max(1, Math.round(image.width * ratio));
        canvas.height = Math.max(1, Math.round(image.height * ratio));
        canvas.getContext('2d').drawImage(image, 0, 0, canvas.width, canvas.height);
        // PNG is kept lossless because cover pages are often line drawings.
        resolve(canvas.toDataURL(file.type === 'image/png' ? 'image/png' : 'image/jpeg', 0.86));
      };
      image.src = reader.result;
    };
    reader.readAsDataURL(file);
  });
}

async function reportAcceptImage(code, file) {
  try {
    reportState.images[code] = await reportReadImage(file);
    saveReportState();
    renderReportModule();
    log(t('report.image.added'), 'ok');
  } catch (error) {
    log(error.message, 'error');
  }
}

function reportSaveTemplate() {
  const payload = { app: 'sea-report', version: APP_VERSION, state: reportState };
  const blob = new Blob([JSON.stringify(payload, null, 2)], { type: 'application/json;charset=utf-8;' });
  const url = URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.download = 'rapor-sablonu.json';
  link.click();
  URL.revokeObjectURL(url);
  log(t('report.template.saved'), 'ok');
}

function reportLoadTemplate(file) {
  const reader = new FileReader();
  reader.onerror = () => log(t('report.template.invalid'), 'error');
  reader.onload = () => {
    try {
      const parsed = JSON.parse(reader.result);
      const incoming = parsed && parsed.app === 'sea-report' ? parsed.state : null;
      if (!incoming || typeof incoming !== 'object') throw new Error('not a report template');
      Object.assign(reportState, reportDefaults(), incoming);
      if (!reportSteps[reportState.process]) Object.assign(reportState, { process: 'intro', step: 'cover' });
      saveReportState();
      renderReportModule();
      log(t('report.template.loaded'), 'ok');
    } catch {
      log(t('report.template.invalid'), 'error');
    }
  };
  reader.readAsText(file);
}

// Empty fields are allowed to stay empty — they simply print empty — so moving on
// is only blocked when a value that WAS entered cannot be right.
function reportValidateStep(step) {
  const year = String(reportState.fields.year ?? '').trim();
  if (step.id !== 'cover' || !year) return true;
  return validateFields(
    [{ id: reportDomId('year'), ok: inRange(Number(year), 2000, 2100), message: t('validate.year', { value: year }) }],
    $('#resultsPanel'));
}

function reportGoToStep(index, validate) {
  const steps = reportActiveSteps();
  if (index < 0 || index >= steps.length) return;
  if (validate && !reportValidateStep(reportActiveStep())) return;
  reportState.step = steps[index].id;
  saveReportState();
  renderReportModule();
  window.scrollTo({ top: 0, behavior: 'smooth' });
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
