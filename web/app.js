const moduleDefinitions = [
  { id: 'spectrum', key: 'spectrum', icon: '⌁', categoryKey: 'category.analysis', ready: true },
  { id: 'increment', key: 'increment', icon: '↟', categoryKey: 'category.analysis', ready: true },
  { id: 'drift', key: 'drift', icon: '↔', categoryKey: 'category.analysis', ready: true },
  { id: 'pdelta', key: 'pdelta', icon: 'ϑ', categoryKey: 'category.analysis', ready: true },
  { id: 'column-axial', key: 'columnAxial', icon: '▥', categoryKey: 'category.memberChecks', ready: true },
  { id: 'wall-shear', key: 'wallShear', icon: '▤', categoryKey: 'category.memberChecks' },
  { id: 'wall-axial', key: 'wallAxial', icon: '▯', categoryKey: 'category.memberChecks' },
  { id: 'beam-shear', key: 'beamShear', icon: '═', categoryKey: 'category.memberChecks', ready: true },
  { id: 'beam-axial', key: 'beamAxial', icon: '⇥', categoryKey: 'category.memberChecks', ready: true },
  { id: 'column-schedule', key: 'columnSchedule', icon: '▦', categoryKey: 'category.schedules', ready: true },
  { id: 'wall-schedule', key: 'wallSchedule', icon: '▧', categoryKey: 'category.schedules' },
  { id: 'beam-schedule', key: 'beamSchedule', icon: '▭', categoryKey: 'category.schedules' },
  { id: 'slab-schedule', key: 'slabSchedule', icon: '▱', categoryKey: 'category.schedules' },
  { id: 'foundation-schedule', key: 'foundationSchedule', icon: '▰', categoryKey: 'category.schedules' }
];

const translations = {
  en: {
    'brand.subtitle': 'ETABS checks and reporting platform',
    'brand.home': 'Structural Engineering Assistant home', 'nav.aria': 'Application menu',
    'model.activeTitle': 'Active ETABS model', 'model.active': 'Active model', 'model.waiting': 'Waiting for connection',
    'action.connect': 'Connect to ETABS', 'action.clear': 'Clear', 'action.showAll': 'Show all →', 'action.showLess': 'Show less ↑',
    'action.viewArchitecture': 'View connection architecture', 'action.dashboard': '← Dashboard', 'action.close': 'Close', 'action.understood': 'Understood',
    'action.searching': 'Searching for bridge…', 'action.downloadAgent': 'Download Windows Agent',
    'nav.general': 'GENERAL', 'nav.analysis': 'ANALYSIS & CHECKS', 'nav.memberChecks': 'MEMBER CHECKS', 'nav.schedules': 'SCHEDULES & OUTPUTS',
    'category.analysis': 'Analysis & Checks', 'category.memberChecks': 'Member Checks', 'category.schedules': 'Schedules & Outputs',
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
    'about.purpose.title': 'Engineering workspace', 'about.purpose.text': 'Structural Engineering Assistant brings ETABS checks, member schedules, results, and exports into one web interface.',
    'about.connection.title': 'Local ETABS bridge', 'about.connection.text': 'Because browsers cannot access the ETABS COM API directly, a secure Windows agent will connect this interface to the model open on your computer.',
    'about.status.title': 'Current version', 'about.status.text': 'The interface and ETABS connection agent are available, and engineering checks run against the active model through the local bridge.',
    'about.note.label': 'Important:', 'about.note.text': 'Engineering results must be reviewed and approved by the responsible structural engineer.',
    'moduleData.title': 'Model Data', 'moduleData.description': 'Dataset to be read from the ETABS model',
    'moduleData.waiting': 'Waiting for ETABS connection', 'moduleData.note': 'Module inputs will be retrieved securely from the active ETABS model through the local bridge.',
    'results.title': 'Check Results', 'results.description': 'Summary metrics and member-level results',
    'table.member': 'Member', 'table.story': 'Story', 'table.demand': 'Demand', 'table.capacity': 'Capacity', 'table.ratio': 'Ratio', 'table.status': 'Status',
    'table.empty': 'Results will appear here after a connection is established.',
    'moduleLog.title': 'Module Log', 'moduleLog.ready': 'Module shell is ready for web migration.',
    'dialog.title': 'ETABS Web Connection', 'dialog.subtitle': 'Recommended secure local bridge architecture', 'architecture.web': 'Web Interface',
    'dialog.note': 'A browser cannot access COM objects directly. The local Windows tray agent reads the active ETABS model and returns approved data to the web interface as JSON.',
    'module.spectrum.title': 'Design Spectrum', 'module.spectrum.description': 'Create the horizontal elastic design spectrum using TBDY 2018 parameters and transfer it to the ETABS model.',
    'module.increment.title': 'Scaling Calculation', 'module.increment.description': 'Calculate dynamic scaling factors from modal results and base shear forces.',
    'module.drift.title': 'Interstory Drift', 'module.drift.description': 'Calculate effective interstory drifts and compare them with TBDY 2018 limits.',
    'module.pdelta.title': 'Second-Order Effects', 'module.pdelta.description': 'Evaluate story stability coefficients and second-order amplification requirements.',
    'module.columnAxial.title': 'Column Axial Load', 'module.columnAxial.description': 'Check column axial load demands against section capacities and code limits.',
    'module.wallShear.title': 'Wall Shear', 'module.wallShear.description': 'Review wall shear demands, capacities, and governing load combinations by story.',
    'module.wallAxial.title': 'Wall Axial Load', 'module.wallAxial.description': 'Evaluate wall axial load ratios for governing combinations.',
    'module.beamShear.title': 'Beam Shear', 'module.beamShear.description': 'Check beam shear safety by member and story.',
    'module.beamAxial.title': 'Beam Axial Load', 'module.beamAxial.description': 'Filter and report axial force effects in beams.',
    'module.columnSchedule.title': 'Column Schedule', 'module.columnSchedule.description': 'Group column sections and reinforcement layouts for drawing output.',
    'module.wallSchedule.title': 'Wall Schedule', 'module.wallSchedule.description': 'Convert wall section and reinforcement data into an organized schedule.',
    'module.beamSchedule.title': 'Beam Schedule', 'module.beamSchedule.description': 'Prepare beam reinforcement and section data for drawing production.',
    'module.slabSchedule.title': 'Slab Schedule', 'module.slabSchedule.description': 'Compile slab geometry and reinforcement data in one schedule.',
    'module.foundationSchedule.title': 'Foundation Schedule', 'module.foundationSchedule.description': 'Prepare foundation members and design data for drawing and reporting.',
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
    'columnSchedule.params.title': 'Settings and Data Fetch', 'columnSchedule.fetch': 'Fetch Model Data', 'columnSchedule.reset': 'Reset to Original',
    'columnSchedule.story': 'Story', 'columnSchedule.applyWholeType': 'Apply rebar edits to the whole type (all stories)',
    'columnSchedule.hint': 'Columns at the same (X,Y) location across stories are grouped into one Type. Editing a Type\'s rebar re-groups types automatically.',
    'columnSchedule.selectType': 'Select type in model', 'columnSchedule.selectOne': 'Select',
    'columnSchedule.table.type': 'Type', 'columnSchedule.table.h': 'h/⌀ (cm)', 'columnSchedule.table.shape': 'Shape',
    'columnSchedule.table.rebar': 'Rebar', 'columnSchedule.table.ratio': 'Ratio',
    'columnSchedule.shape.rect': 'Rectangular', 'columnSchedule.shape.circle': 'Circular',
    'columnSchedule.status.fetched': '{count} columns fetched and grouped into types.',
    'columnSchedule.status.reset': 'Reverted to the originally fetched values.',
    'columnSchedule.status.selected': '{count} column(s) selected in the model.',
    'columnSchedule.promptRebar': 'Current rebar: {current}\nEnter new rebar (e.g. 16φ20):',
    'columnSchedule.exportDxf': 'Download DXF (plan + rebar schedule)',
    'columnSchedule.status.dxfSaved': 'DXF file generated.'
  },
  tr: {
    'brand.subtitle': 'ETABS tahkik ve raporlama platformu',
    'brand.home': 'Structural Engineering Assistant ana sayfa', 'nav.aria': 'Uygulama menüsü',
    'model.activeTitle': 'Aktif ETABS modeli', 'model.active': 'Aktif model', 'model.waiting': 'Bağlantı bekleniyor',
    'action.connect': "ETABS'a Bağlan", 'action.clear': 'Temizle', 'action.showAll': 'Tümünü göster →', 'action.showLess': 'Daha az göster ↑',
    'action.viewArchitecture': 'Bağlantı mimarisini görüntüle', 'action.dashboard': '← Dashboard', 'action.close': 'Kapat', 'action.understood': 'Anladım',
    'action.searching': 'Köprü aranıyor…', 'action.downloadAgent': 'Windows Agent’ı İndir',
    'nav.general': 'GENEL', 'nav.analysis': 'ANALİZ & KONTROL', 'nav.memberChecks': 'ELEMAN TAHKİKLERİ', 'nav.schedules': 'DONE & ÇIKTILAR',
    'category.analysis': 'Analiz & Kontrol', 'category.memberChecks': 'Eleman Tahkikleri', 'category.schedules': 'Done & Çıktılar',
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
    'about.purpose.title': 'Mühendislik çalışma alanı', 'about.purpose.text': 'Structural Engineering Assistant; ETABS tahkiklerini, eleman donelerini, sonuçları ve dışa aktarımları tek bir web arayüzünde birleştirir.',
    'about.connection.title': 'Yerel ETABS köprüsü', 'about.connection.text': 'Tarayıcılar ETABS COM API’ye doğrudan erişemediği için güvenli bir Windows agent bu arayüzü bilgisayarınızda açık olan modele bağlar.',
    'about.status.title': 'Mevcut sürüm', 'about.status.text': 'Arayüz ve ETABS bağlantı agent’ı kullanılabilir; mühendislik tahkikleri yerel köprü üzerinden aktif model üzerinde çalışır.',
    'about.note.label': 'Önemli:', 'about.note.text': 'Mühendislik sonuçları sorumlu inşaat mühendisi tarafından kontrol edilmeli ve onaylanmalıdır.',
    'moduleData.title': 'Model Verisi', 'moduleData.description': 'ETABS modelinden okunacak veri seti',
    'moduleData.waiting': 'ETABS bağlantısı bekleniyor', 'moduleData.note': 'Modül girdileri, yerel köprü üzerinden aktif ETABS modelinden güvenli biçimde alınacak.',
    'results.title': 'Tahkik Sonuçları', 'results.description': 'Özet metrikler ve eleman bazlı sonuçlar',
    'table.member': 'Eleman', 'table.story': 'Kat', 'table.demand': 'Talep', 'table.capacity': 'Kapasite', 'table.ratio': 'Oran', 'table.status': 'Durum',
    'table.empty': 'Bağlantı kurulduktan sonra sonuçlar burada görüntülenecek.',
    'moduleLog.title': 'Modül Günlüğü', 'moduleLog.ready': 'Modül web uyarlaması için hazırlandı.',
    'dialog.title': 'ETABS Web Bağlantısı', 'dialog.subtitle': 'Önerilen güvenli yerel köprü mimarisi', 'architecture.web': 'Web Arayüzü',
    'dialog.note': "Tarayıcı COM nesnelerine doğrudan erişemez. Yerel Windows tray agent aktif ETABS modelini okur ve izin verilen verileri JSON olarak web arayüzüne döndürür.",
    'module.spectrum.title': 'Tasarım Spektrumu', 'module.spectrum.description': 'TBDY 2018 parametreleriyle yatay elastik tasarım spektrumunu oluşturun ve ETABS modeline aktarın.',
    'module.increment.title': 'Artırım Hesabı', 'module.increment.description': 'Modal sonuçlar ve taban kesme kuvvetleri üzerinden dinamik büyütme katsayılarını hesaplayın.',
    'module.drift.title': 'Göreli Kat Ötelemesi', 'module.drift.description': 'Etkin göreli kat ötelemelerini hesaplayın ve TBDY 2018 sınırlarıyla karşılaştırın.',
    'module.pdelta.title': 'İkinci Mertebe Etkileri', 'module.pdelta.description': 'Kat stabilite katsayılarını ve ikinci mertebe büyütme gereksinimini değerlendirin.',
    'module.columnAxial.title': 'Kolon Eksenel Yük', 'module.columnAxial.description': 'Kolon eksenel yük taleplerini kesit kapasiteleri ve yönetmelik sınırlarıyla tahkik edin.',
    'module.wallShear.title': 'Perde Kesme', 'module.wallShear.description': 'Perde kesme taleplerini, kapasitelerini ve kritik yük birleşimlerini kat bazında inceleyin.',
    'module.wallAxial.title': 'Perde Eksenel Yük', 'module.wallAxial.description': 'Perde eksenel yük oranlarını kritik kombinasyonlar için değerlendirin.',
    'module.beamShear.title': 'Kiriş Kesme', 'module.beamShear.description': 'Kiriş kesme güvenliğini eleman ve kat bazında tahkik edin.',
    'module.beamAxial.title': 'Kiriş Eksenel Yük', 'module.beamAxial.description': 'Kirişlerdeki eksenel kuvvet etkilerini filtreleyin ve raporlayın.',
    'module.columnSchedule.title': 'Kolon Done', 'module.columnSchedule.description': 'Kolon kesitlerini ve donatı düzenlerini gruplayarak pafta verisi üretin.',
    'module.wallSchedule.title': 'Perde Done', 'module.wallSchedule.description': 'Perde kesit ve donatı verilerini düzenli bir done çıktısına dönüştürün.',
    'module.beamSchedule.title': 'Kiriş Done', 'module.beamSchedule.description': 'Kiriş donatı ve kesit bilgilerini pafta üretimine hazırlayın.',
    'module.slabSchedule.title': 'Döşeme Done', 'module.slabSchedule.description': 'Döşeme geometri ve donatı verilerini tek tabloda derleyin.',
    'module.foundationSchedule.title': 'Temel Done', 'module.foundationSchedule.description': 'Temel elemanlarını ve tasarım verilerini çizim ve rapor formatına hazırlayın.',
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
    'increment.result.period': 'Kullanılan {direction} periyodu', 'increment.result.beta': 'Artırım Katsayısı β',
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
    'columnSchedule.params.title': 'Ayarlar ve Veri Çekme', 'columnSchedule.fetch': 'Model Bilgilerini Çek', 'columnSchedule.reset': 'Orijinale Dön',
    'columnSchedule.story': 'Kat Seçimi', 'columnSchedule.applyWholeType': 'Donatı değişikliğini tipin tamamına uygula (tüm katlar)',
    'columnSchedule.hint': 'Katlar boyunca aynı (X,Y) konumundaki kolonlar tek bir Tip olarak gruplanır. Bir tipin donatısını değiştirmek tipleri otomatik yeniden gruplar.',
    'columnSchedule.selectType': 'Tipi Modelde Seç', 'columnSchedule.selectOne': 'Seç',
    'columnSchedule.table.type': 'Tip', 'columnSchedule.table.h': 'h/⌀ (cm)', 'columnSchedule.table.shape': 'Şekil',
    'columnSchedule.table.rebar': 'Donatı', 'columnSchedule.table.ratio': 'Oran',
    'columnSchedule.shape.rect': 'Dikdörtgen', 'columnSchedule.shape.circle': 'Dairesel',
    'columnSchedule.status.fetched': '{count} kolon çekildi ve tiplere gruplandı.',
    'columnSchedule.status.reset': 'Orijinal çekilen değerlere dönüldü.',
    'columnSchedule.status.selected': '{count} kolon modelde seçildi.',
    'columnSchedule.promptRebar': 'Güncel donatı: {current}\nYeni donatıyı girin (örn: 16φ20):',
    'columnSchedule.exportDxf': 'DXF İndir (plan + donatı donesi)',
    'columnSchedule.status.dxfSaved': 'DXF dosyası oluşturuldu.'
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
  'column-schedule': renderColumnScheduleModule
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
  document.title = 'Structural Engineering Assistant';
  applyTranslationsToDom();
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
  $('#moduleCategory').textContent = t(module.categoryKey).toUpperCase();
  $('#moduleDescription').textContent = t(`module.${module.key}.description`);
  dashboard.classList.remove('active');
  moduleView.classList.add('active');
  $$('.nav-item').forEach(item => item.classList.toggle('active', item.dataset.view === id));
  window.scrollTo({ top: 0, behavior: 'smooth' });

  const renderer = moduleRenderers[id];
  if (renderer) {
    renderer();
  } else {
    $('#setupPanel').innerHTML = defaultSetupPanelHtml;
    $('#resultsPanel').innerHTML = defaultResultsPanelHtml;
    applyTranslationsToDom($('#setupPanel'));
    applyTranslationsToDom($('#resultsPanel'));
    const connectBtn = $('[data-connect]', $('#setupPanel'));
    if (connectBtn) connectBtn.addEventListener('click', connectToEtabs);
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
    const response = await fetch('http://127.0.0.1:5218/api/health', { headers: { Accept: 'application/json' }, signal: AbortSignal.timeout(5000) });
    if (!response.ok) throw new Error(`HTTP ${response.status}`);
    const data = await response.json();
    if (!data.etabsConnected) {
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
    log(t('terminal.connected', { model }), 'ok');
  } catch (error) {
    $('#architectureDialog').showModal();
    log(t('terminal.notFound'), 'error');
  } finally {
    setConnectButtonsLoading(false);
  }
}

// ---------------------------------------------------------------------------
// Interstory Drift module (pilot migration of GoreliKatOtelemesiManager, C#)
// ---------------------------------------------------------------------------

const driftState = {
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
function filterDriftRows(rows, groups, basementNames, useBasement) {
  const result = [];
  for (const row of rows) {
    const direction = row.direction.toUpperCase();
    const isBasement = basementNames.has(row.story);
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
      <div class="field"><label>${t('drift.params.sdsDD2')}</label><input type="number" step="any" id="driftSdsDD2"></div>
      <div class="field"><label>${t('drift.params.sdsDD3')}</label><input type="number" step="any" id="driftSdsDD3"></div>
      <div class="field"><label>${t('drift.params.sd1DD2')}</label><input type="number" step="any" id="driftSd1DD2"></div>
      <div class="field"><label>${t('drift.params.sd1DD3')}</label><input type="number" step="any" id="driftSd1DD3"></div>
      <div class="field"><label>${t('drift.params.k')}</label><input type="number" step="any" id="driftK"></div>
      <div class="field"><label>${t('drift.params.tp')}</label><input type="number" step="any" id="driftTp"></div>
      <label class="field-checkbox"><input type="checkbox" id="driftEsnekDerz"> ${t('drift.params.flexibleJoint')}</label>
      <label class="field-checkbox"><input type="checkbox" id="driftBodrum"> ${t('drift.params.basement')}</label>
      <div class="field"><label>${t('drift.params.basementCount')}</label><input type="number" min="0" id="driftBodrumKat"></div>
    </div>
    <div class="combo-picker">
      <div class="combo-picker-heading"><h3>${t('drift.combos.title')}</h3>
        <button class="button button-secondary" type="button" id="driftFetchCombos">${t('drift.combos.fetch')}</button>
      </div>
      <select class="combo-select" id="driftComboSelect" multiple></select>
      <p class="combo-hint">${t('drift.combos.hint')}</p>
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="driftCalculate">${t('drift.calculate')}</button>
    </div>`;

  bindDriftParamInputs(panel);
  populateComboSelect();
  $('#driftFetchCombos', panel).addEventListener('click', fetchDriftCombosAndStories);
  $('#driftCalculate', panel).addEventListener('click', runDriftCheck);
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

function populateComboSelect() {
  const select = $('#driftComboSelect');
  if (!select) return;
  select.innerHTML = driftState.combos
    .map(name => `<option value="${name}" ${driftState.selected.includes(name) ? 'selected' : ''}>${name}</option>`)
    .join('');
  select.addEventListener('change', () => {
    driftState.selected = [...select.selectedOptions].map(o => o.value);
  });
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
}

async function fetchDriftCombosAndStories() {
  const panel = $('#setupPanel');
  const btn = $('#driftFetchCombos', panel);
  btn.disabled = true;
  try {
    const [combosRes, storiesRes] = await Promise.all([
      fetchAgentJson('/api/etabs/combinations'),
      fetchAgentJson('/api/etabs/stories')
    ]);
    if (!combosRes.etabsConnected) throw new Error(combosRes.error || t('drift.error.notConnected'));
    driftState.combos = combosRes.names;
    driftState.stories = storiesRes.stories || [];
    populateComboSelect();
    log(t('drift.combos.fetched', { count: driftState.combos.length }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

async function runDriftCheck() {
  const panel = $('#setupPanel');
  const btn = $('#driftCalculate', panel);
  if (driftState.selected.length === 0) {
    log(t('drift.error.noCombos'), 'error');
    return;
  }

  btn.disabled = true;
  try {
    if (driftState.stories.length === 0) {
      const storiesRes = await fetchAgentJson('/api/etabs/stories');
      if (!storiesRes.etabsConnected) throw new Error(storiesRes.error || t('drift.error.notConnected'));
      driftState.stories = storiesRes.stories;
    }

    const comboParam = encodeURIComponent(driftState.selected.join(','));
    const driftRes = await fetchAgentJson(`/api/etabs/story-drifts?combos=${comboParam}`);
    if (!driftRes.etabsConnected) throw new Error(driftRes.error || t('drift.error.notConnected'));

    const groups = groupCombos(driftState.selected);
    const basementNames = driftState.params.bodrum
      ? determineBasementStories(driftState.stories, driftState.params.bodrumKat)
      : new Set();
    const filtered = filterDriftRows(driftRes.rows || [], groups, basementNames, driftState.params.bodrum);
    if (filtered.length === 0) throw new Error(t('drift.error.noData'));

    const result = calculateDriftItems(filtered, driftState.params);
    driftState.lastResult = result;
    renderDriftResultsTable(result);
    recordLastCheck('drift');
    log(t(result.allPassed ? 'drift.status.passed' : 'drift.status.failed'), result.allPassed ? 'ok' : 'error');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
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

  const { xUST, xALT, yUST, yALT } = pdeltaBucketCombos(selected);
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
  const comboParam = encodeURIComponent(pdeltaState.selected.join(','));
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
      <div class="field"><label>${t('pdelta.params.ch')}</label><input type="number" step="any" id="pdCh"></div>
      <div class="field"><label>${t('pdelta.params.r')}</label><input type="number" step="any" id="pdR"></div>
      <div class="field"><label>${t('pdelta.params.d')}</label><input type="number" step="any" id="pdD"></div>
      <label class="field-checkbox"><input type="checkbox" id="pdBodrum"> ${t('drift.params.basement')}</label>
      <div class="field"><label>${t('drift.params.basementCount')}</label><input type="number" min="0" id="pdBodrumKat"></div>
    </div>
    <div class="combo-picker">
      <div class="combo-picker-heading"><h3>${t('drift.combos.title')}</h3>
        <button class="button button-secondary" type="button" id="pdFetchCombos">${t('drift.combos.fetch')}</button>
      </div>
      <select class="combo-select" id="pdComboSelect" multiple></select>
      <p class="combo-hint">${t('pdelta.combos.hint')}</p>
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="pdCalculate">${t('drift.calculate')}</button>
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

  pdeltaPopulateComboSelect();
  $('#pdFetchCombos', panel).addEventListener('click', pdeltaFetchCombos);
  $('#pdCalculate', panel).addEventListener('click', runPdeltaCheck);
}

function pdeltaPopulateComboSelect() {
  const select = $('#pdComboSelect');
  if (!select) return;
  select.innerHTML = pdeltaState.combos
    .map(name => `<option value="${name}" ${pdeltaState.selected.includes(name) ? 'selected' : ''}>${name}</option>`)
    .join('');
  select.addEventListener('change', () => {
    pdeltaState.selected = [...select.selectedOptions].map(o => o.value);
  });
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
}

async function pdeltaFetchCombos() {
  const btn = $('#pdFetchCombos');
  btn.disabled = true;
  try {
    const res = await fetchAgentJson('/api/etabs/combinations');
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    pdeltaState.combos = res.names;
    pdeltaPopulateComboSelect();
    log(t('drift.combos.fetched', { count: pdeltaState.combos.length }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

async function runPdeltaCheck() {
  const btn = $('#pdCalculate');
  if (pdeltaState.selected.length === 0) {
    log(t('drift.error.noCombos'), 'error');
    return;
  }
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
    btn.disabled = false;
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
      <div class="field"><label>${t('spectrum.params.sds')}</label><input type="number" step="any" id="spSds"></div>
      <div class="field"><label>${t('spectrum.params.sd1')}</label><input type="number" step="any" id="spSd1"></div>
      <div class="field"><label>${t('spectrum.params.r')}</label><input type="number" step="any" id="spR"></div>
      <div class="field"><label>${t('spectrum.params.d')}</label><input type="number" step="any" id="spD"></div>
      <div class="field"><label>${t('spectrum.params.i')}</label><input type="number" step="any" id="spI"></div>
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="spCalculate">${t('drift.calculate')}</button>
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
  const { sds, sd1, accelerations } = spectrumState;
  const ta = 0.2 * sd1 / sds, tb = sd1 / sds;
  const peak = Math.max(...accelerations);
  el.innerHTML = `
    <span><small>TA</small><strong>${ta.toFixed(3)} s</strong></span>
    <span><small>TB</small><strong>${tb.toFixed(3)} s</strong></span>
    <span><small>${t('spectrum.summary.peak')}</small><strong>${peak.toFixed(3)} m/s²</strong></span>
    <span><small>${t('spectrum.summary.points')}</small><strong>${accelerations.length}</strong></span>`;
}

function runSpectrumCalc() {
  const { sds, sd1, r, d, i } = spectrumState;
  if (sds <= 0 || sd1 <= 0 || r <= 0 || i <= 0) {
    log(t('spectrum.error.invalid'), 'error');
    return;
  }
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
// Scaling Calculation (Artırım Hesabı) — ported from ArtirimHesabiUI (C#).
// β = 0.9 · max(SaR(T)·mt, 0.04·SDS·g·I·mt) / Vt ; Tmax = Hn^0.75 · Ct · 1.4
// Depends on the Design Spectrum module's shared spectrumState (SDS, I, SaR curve).
// ---------------------------------------------------------------------------

const incrementState = {
  mt: 0, hn: 0, ct: 0.07,
  bodrum: false, bodrumKat: 0,
  combos: [], selected: [],
  tx: 0, vtX: 0, ty: 0, vtY: 0,
  modalTopX: [], modalTopY: [],
  resultX: null, resultY: null
};

async function incrementFetchCombos() {
  const btn = $('#incFetchCombos');
  btn.disabled = true;
  try {
    const res = await fetchAgentJson('/api/etabs/combinations');
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    incrementState.combos = res.names;
    incrementPopulateComboSelect();
    log(t('drift.combos.fetched', { count: incrementState.combos.length }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

function incrementPopulateComboSelect() {
  const select = $('#incComboSelect');
  if (!select) return;
  select.innerHTML = incrementState.combos
    .map(name => `<option value="${name}" ${incrementState.selected.includes(name) ? 'selected' : ''}>${name}</option>`)
    .join('');
  select.addEventListener('change', () => {
    incrementState.selected = [...select.selectedOptions].map(o => o.value);
  });
}

async function incrementFetchMass() {
  const btn = $('#incFetchMt');
  btn.disabled = true;
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
    log(t('increment.status.massFetched'), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

async function incrementFetchPeriod(direction) {
  const btn = $(direction === 'X' ? '#incFetchTx' : '#incFetchTy');
  btn.disabled = true;
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
    log(t(direction === 'X' ? 'increment.status.periodFetchedX' : 'increment.status.periodFetchedY'), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

function incrementRenderModalInfo(direction) {
  const el = $(direction === 'X' ? '#incModalInfoX' : '#incModalInfoY');
  if (!el) return;
  const top = direction === 'X' ? incrementState.modalTopX : incrementState.modalTopY;
  const col = direction === 'X' ? 'UX' : 'UY';
  el.textContent = top.map(m => `${t('increment.modal.mode')} ${m.mode}: T=${m.period.toFixed(3)}s, ${col}=${m.ratio.toFixed(4)}`).join(' · ');
}

async function incrementFetchVt(direction) {
  const btn = $(direction === 'X' ? '#incFetchVtX' : '#incFetchVtY');
  const dirFilter = direction === 'X' ? 'X' : 'Y';
  const matchingCombo = incrementState.selected.find(c => c.toUpperCase().includes(dirFilter));
  if (!matchingCombo) {
    log(t('increment.error.noComboForDirection', { direction }), 'error');
    return;
  }

  btn.disabled = true;
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
    log(t(direction === 'X' ? 'increment.status.vtFetchedX' : 'increment.status.vtFetchedY'), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
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
  if (mt <= 0 || t0 <= 0 || vt <= 0) {
    log(t('increment.error.invalidInputs'), 'error');
    return;
  }

  const hn = incrementState.hn, ct = incrementState.ct;
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
      <label class="field-checkbox"><input type="checkbox" id="incBodrum"> ${t('drift.params.basement')}</label>
      <div class="field"><label>${t('drift.params.basementCount')}</label><input type="number" min="0" id="incBodrumKat"></div>
      <div class="field"><label>${t('increment.params.mt')}</label><input type="number" step="any" id="incMt"></div>
      <div class="field"><label>&nbsp;</label><button class="button button-secondary" type="button" id="incFetchMt">${t('increment.fetch')}</button></div>
      <div class="field"><label>${t('increment.params.hn')}</label><input type="number" step="any" id="incHn"></div>
      <div class="field"><label>${t('increment.params.ct')}</label><input type="number" step="any" id="incCt"></div>
    </div>
    <div class="combo-picker">
      <div class="combo-picker-heading"><h3>${t('drift.combos.title')}</h3>
        <button class="button button-secondary" type="button" id="incFetchCombos">${t('drift.combos.fetch')}</button>
      </div>
      <select class="combo-select" id="incComboSelect" multiple></select>
      <p class="combo-hint">${t('increment.combos.hint')}</p>
    </div>
    <div class="increment-direction">
      <h3 class="increment-direction-title x">${t('increment.direction.x')}</h3>
      <div class="field-grid two">
        <div class="field"><label>${t('increment.params.tx')}</label><input type="number" step="any" id="incTx"></div>
        <div class="field"><label>&nbsp;</label><button class="button button-secondary" type="button" id="incFetchTx">${t('increment.fetch')}</button></div>
        <div class="field"><label>${t('increment.params.vtx')}</label><input type="number" step="any" id="incVtX"></div>
        <div class="field"><label>&nbsp;</label><button class="button button-secondary" type="button" id="incFetchVtX">${t('increment.fetch')}</button></div>
      </div>
      <p class="increment-modal-info" id="incModalInfoX"></p>
      <button class="button button-primary full-width" type="button" id="incCalcX">${t('increment.calculate', { direction: 'X' })}</button>
    </div>
    <div class="increment-direction">
      <h3 class="increment-direction-title y">${t('increment.direction.y')}</h3>
      <div class="field-grid two">
        <div class="field"><label>${t('increment.params.ty')}</label><input type="number" step="any" id="incTy"></div>
        <div class="field"><label>&nbsp;</label><button class="button button-secondary" type="button" id="incFetchTy">${t('increment.fetch')}</button></div>
        <div class="field"><label>${t('increment.params.vty')}</label><input type="number" step="any" id="incVtY"></div>
        <div class="field"><label>&nbsp;</label><button class="button button-secondary" type="button" id="incFetchVtY">${t('increment.fetch')}</button></div>
      </div>
      <p class="increment-modal-info" id="incModalInfoY"></p>
      <button class="button button-primary full-width" type="button" id="incCalcY">${t('increment.calculate', { direction: 'Y' })}</button>
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

  incrementPopulateComboSelect();
  $('#incFetchCombos', panel).addEventListener('click', incrementFetchCombos);
  $('#incFetchMt', panel).addEventListener('click', incrementFetchMass);
  $('#incFetchTx', panel).addEventListener('click', () => incrementFetchPeriod('X'));
  $('#incFetchTy', panel).addEventListener('click', () => incrementFetchPeriod('Y'));
  $('#incFetchVtX', panel).addEventListener('click', () => incrementFetchVt('X'));
  $('#incFetchVtY', panel).addEventListener('click', () => incrementFetchVt('Y'));
  $('#incCalcX', panel).addEventListener('click', () => incrementCalculate('X'));
  $('#incCalcY', panel).addEventListener('click', () => incrementCalculate('Y'));

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
      <div class="field"><label>${t('columnAxial.params.fck')}</label><input type="number" step="any" id="caFck"></div>
      <div class="field"><label>${t('columnAxial.params.limit')}</label><input type="number" step="any" id="caLimit"></div>
      <label class="field-checkbox"><input type="checkbox" id="caBodrum"> ${t('drift.params.basement')}</label>
      <div class="field"><label>${t('drift.params.basementCount')}</label><input type="number" min="0" id="caBodrumKat"></div>
    </div>
    <div class="combo-picker">
      <div class="combo-picker-heading"><h3>${t('drift.combos.title')}</h3>
        <button class="button button-secondary" type="button" id="caFetchCombos">${t('drift.combos.fetch')}</button>
      </div>
      <select class="combo-select" id="caComboSelect" multiple></select>
      <p class="combo-hint">${t('columnAxial.combos.hint')}</p>
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="caCalculate">${t('columnAxial.calculate')}</button>
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

  columnAxialPopulateComboSelect();
  $('#caFetchCombos', panel).addEventListener('click', columnAxialFetchCombos);
  $('#caCalculate', panel).addEventListener('click', runColumnAxialCheck);
  $('#caSelectFailing', panel).addEventListener('click', columnAxialSelectFailing);
  $('#caExport', panel).addEventListener('click', columnAxialExportExcel);
}

function columnAxialPopulateComboSelect() {
  const select = $('#caComboSelect');
  if (!select) return;
  select.innerHTML = columnAxialState.combos
    .map(name => `<option value="${name}" ${columnAxialState.selected.includes(name) ? 'selected' : ''}>${name}</option>`)
    .join('');
  select.addEventListener('change', () => {
    columnAxialState.selected = [...select.selectedOptions].map(o => o.value);
  });
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

  columnAxialUpdateSummary();
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

async function columnAxialFetchCombos() {
  const btn = $('#caFetchCombos');
  btn.disabled = true;
  try {
    const res = await fetchAgentJson('/api/etabs/combinations');
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    columnAxialState.combos = res.names;
    columnAxialPopulateComboSelect();
    log(t('drift.combos.fetched', { count: columnAxialState.combos.length }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
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
  const comboParam = encodeURIComponent(columnAxialState.selected.join(','));
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

  const selectedUpper = columnAxialState.selected.map(c => c.toUpperCase());
  return res.rows
    .filter(row => {
      const loadCase = (row[caseIdx] || '').toUpperCase();
      if (!selectedUpper.includes(loadCase)) return false;
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
  fck: 30, fyk: 420, dprime: 5, useVc: true,
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
  const selectedUpper = selected.map(c => c.toUpperCase());
  return { rows: res.rows, storyIdx, labelIdx, uniqueIdx, caseIdx, valIdx, selectedUpper };
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
      <div class="field"><label>${t('beam.params.fck')}</label><input type="number" step="any" id="bsFck"></div>
      <div class="field"><label>${t('beam.params.fyk')}</label><input type="number" step="any" id="bsFyk"></div>
      <div class="field"><label>${t('beam.params.dprime')}</label><input type="number" step="any" id="bsDprime"></div>
      <label class="field-checkbox"><input type="checkbox" id="bsUseVc"> ${t('beam.params.useVc')}</label>
    </div>
    <div class="combo-picker">
      <div class="combo-picker-heading"><h3>${t('drift.combos.title')}</h3>
        <button class="button button-secondary" type="button" id="bsFetchCombos">${t('drift.combos.fetch')}</button>
      </div>
      <select class="combo-select" id="bsComboSelect" multiple></select>
      <p class="combo-hint">${t('beam.combos.hint')}</p>
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="bsCalculate">${t('columnAxial.calculate')}</button>
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

  beamComboSelect('#bsComboSelect', beamShearState);
  $('#bsFetchCombos', panel).addEventListener('click', () => beamFetchCombos('#bsFetchCombos', '#bsComboSelect', beamShearState));
  $('#bsCalculate', panel).addEventListener('click', runBeamShearCheck);
  $('#bsSelectFailing', panel).addEventListener('click', () => beamSelectFailing(beamShearState.lastResults));
  $('#bsExport', panel).addEventListener('click', beamShearExportExcel);
}

function beamComboSelect(selector, state) {
  const select = $(selector);
  if (!select) return;
  select.innerHTML = state.combos
    .map(name => `<option value="${name}" ${state.selected.includes(name) ? 'selected' : ''}>${name}</option>`)
    .join('');
  select.addEventListener('change', () => { state.selected = [...select.selectedOptions].map(o => o.value); });
}

async function beamFetchCombos(btnSel, comboSel, state) {
  const btn = $(btnSel);
  btn.disabled = true;
  try {
    const res = await fetchAgentJson('/api/etabs/combinations');
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    state.combos = res.names;
    beamComboSelect(comboSel, state);
    log(t('drift.combos.fetched', { count: state.combos.length }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
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
  const btn = $('#bsCalculate');
  if (btn) btn.disabled = true;
  try {
    const [forces, sectionMap] = await Promise.all([
      loadBeamElementForces(beamShearState.selected, 'V2'),
      fetchFrameSectionMap()
    ]);
    const { rows, storyIdx, labelIdx, uniqueIdx, caseIdx, valIdx, selectedUpper } = forces;

    // Group by story+label, keep the governing (max |V2|) row.
    const byKey = new Map();
    for (const row of rows) {
      if (!selectedUpper.includes((row[caseIdx] || '').toUpperCase())) continue;
      const story = row[storyIdx] || '', label = row[labelIdx] || '';
      const key = `${story}_${label}`;
      const vd = Math.abs(parseFloat(row[valIdx]) || 0);
      const existing = byKey.get(key);
      if (!existing) byKey.set(key, { story, label, unique: row[uniqueIdx] || '', case: row[caseIdx] || '', vd });
      else if (vd > existing.vd) { existing.vd = vd; existing.case = row[caseIdx] || ''; }
    }

    const beams = [...byKey.values()];
    if (beams.length === 0) throw new Error(t('beam.error.noData'));

    beamShearState.lastResults = beamShearCalculate(beams, sectionMap, beamShearState);
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
  beamShearUpdateBanner();
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
      <div class="field"><label>${t('beam.params.fck')}</label><input type="number" step="any" id="baFck"></div>
      <div class="field"><label>${t('beamAxial.params.limit')}</label><input type="number" step="any" id="baLimit"></div>
    </div>
    <div class="combo-picker">
      <div class="combo-picker-heading"><h3>${t('drift.combos.title')}</h3>
        <button class="button button-secondary" type="button" id="baFetchCombos">${t('drift.combos.fetch')}</button>
      </div>
      <select class="combo-select" id="baComboSelect" multiple></select>
      <p class="combo-hint">${t('beam.combos.hint')}</p>
    </div>
    <div class="panel-actions">
      <button class="button button-primary full-width" type="button" id="baCalculate">${t('columnAxial.calculate')}</button>
    </div>
    <div class="panel-actions two-up">
      <button class="button button-secondary" type="button" id="baSelectFailing">${t('beamAxial.selectFailing')}</button>
      <button class="button button-secondary" type="button" id="baExport">${t('columnAxial.export')}</button>
    </div>`;

  const fck = $('#baFck', panel), limit = $('#baLimit', panel);
  fck.value = beamAxialState.fck; limit.value = beamAxialState.limit;
  fck.addEventListener('input', () => { beamAxialState.fck = parseFloat(fck.value) || 0; });
  limit.addEventListener('input', () => { beamAxialState.limit = parseFloat(limit.value) || 0; });

  beamComboSelect('#baComboSelect', beamAxialState);
  $('#baFetchCombos', panel).addEventListener('click', () => beamFetchCombos('#baFetchCombos', '#baComboSelect', beamAxialState));
  $('#baCalculate', panel).addEventListener('click', runBeamAxialCheck);
  $('#baSelectFailing', panel).addEventListener('click', () => beamSelectFailing(beamAxialState.lastResults));
  $('#baExport', panel).addEventListener('click', beamAxialExportExcel);
}

async function runBeamAxialCheck() {
  if (beamAxialState.selected.length === 0) { log(t('drift.error.noCombos'), 'error'); return; }
  const btn = $('#baCalculate');
  if (btn) btn.disabled = true;
  try {
    const [forces, sectionMap] = await Promise.all([
      loadBeamElementForces(beamAxialState.selected, 'P'),
      fetchFrameSectionMap()
    ]);
    const { rows, storyIdx, labelIdx, uniqueIdx, caseIdx, valIdx, selectedUpper } = forces;

    // Group by unique, keep the governing (max |P|) row.
    const byUnique = new Map();
    for (const row of rows) {
      if (!selectedUpper.includes((row[caseIdx] || '').toUpperCase())) continue;
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
    }).sort((a, b) => b.ratio - a.ratio);

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
  beamAxialUpdateBanner();
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
// Column Schedule (Kolon Donesi) — ported from KolonDonesiUI.GenerateColumnTypes
// + CalculateRebarRatio (C#). Clusters columns by (X,Y) into stacks, assigns a
// Type label per stack via subset/superset matching across stories, and
// computes the reinforcement ratio. DWG export (kolon_dwg_export.cs) is CAD
// drawing generation — out of scope for the web app's table/Excel format, so
// only the calculation + schedule table + Excel export are migrated here.
// ---------------------------------------------------------------------------

const columnScheduleState = {
  columns: [], original: [], stories: [], selectedStory: null, applyToWholeType: false
};

function extractRebarParts(rebarLabel) {
  if (!rebarLabel) return null;
  const parts = String(rebarLabel).replace(/\s/g, '').split('φ');
  if (parts.length !== 2) return null;
  const count = parseInt(parts[0], 10);
  const dia = parseFloat(parts[1]);
  return Number.isFinite(count) && Number.isFinite(dia) ? { count, dia } : null;
}

function calculateRebarRatio(col) {
  const parts = extractRebarParts(col.rebarLabel);
  if (!parts) return 0;
  const diaM = parts.dia / 1000;
  const rebarAreaM2 = parts.count * (Math.PI * diaM * diaM / 4);
  const colAreaM2 = col.width * col.depth;
  return colAreaM2 === 0 ? 0 : (rebarAreaM2 / colAreaM2) * 100;
}

// Mirrors GenerateColumnTypes 1:1: cluster by (X,Y), then assign each stack a Type via
// subset/superset matching against already-defined types (largest stacks first).
function generateColumnTypes(columns) {
  const clusterTolerance = 0.05;
  const groups = [];
  for (const col of columns) {
    let added = false;
    for (const grp of groups) {
      const rep = grp[0];
      if (Math.abs(rep.x - col.x) < clusterTolerance && Math.abs(rep.y - col.y) < clusterTolerance) {
        grp.push(col);
        added = true;
        break;
      }
    }
    if (!added) groups.push([col]);
  }

  const stackInfos = groups.map(grp => {
    const storySections = new Map();
    for (const col of grp) {
      const minDim = Math.min(col.width, col.depth);
      const maxDim = Math.max(col.width, col.depth);
      storySections.set(col.story, `${Math.round(minDim * 100)}x${Math.round(maxDim * 100)}_${col.shape}_${col.rebarLabel}`);
    }
    return { group: grp, storySections };
  });

  stackInfos.sort((a, b) => b.storySections.size - a.storySections.size);

  let typeCounter = 1;
  const definedTypes = [];
  for (const info of stackInfos) {
    let matchedType = null;
    for (const defined of definedTypes) {
      let isMatch = true;
      for (const [story, sig] of info.storySections) {
        if (defined.storySections.get(story) !== sig) { isMatch = false; break; }
      }
      if (isMatch) { matchedType = defined; break; }
    }
    const typeLabel = matchedType ? matchedType.typeLabel : `T${typeCounter++}`;
    if (!matchedType) { info.typeLabel = typeLabel; definedTypes.push(info); }
    for (const col of info.group) col.type = typeLabel;
  }
}

function columnScheduleTypeNumber(type) {
  return parseInt(String(type || '').replace(/\D/g, ''), 10) || 0;
}

function columnScheduleColorFor(rebarLabel) {
  const palette = { 16: '#f2c94c', 18: '#eb5757', 20: '#f2994a', 22: '#bb6bd9', 25: '#9b51e0' };
  const parts = extractRebarParts(rebarLabel);
  if (parts && palette[Math.round(parts.dia)]) return palette[Math.round(parts.dia)];
  let hash = 0;
  for (const ch of String(rebarLabel)) hash = (hash * 31 + ch.charCodeAt(0)) % 360;
  return `hsl(${hash}, 55%, 55%)`;
}

function renderColumnScheduleModule() {
  renderColumnScheduleSetupPanel();
  renderColumnScheduleResultsPanel();
}

function renderColumnScheduleSetupPanel() {
  const panel = $('#setupPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">1</span><div><h2>${t('columnSchedule.params.title')}</h2><p>${t('moduleData.description')}</p></div></div></div>
    <div class="panel-actions two-up">
      <button class="button button-primary" type="button" id="csFetch">${t('columnSchedule.fetch')}</button>
      <button class="button button-secondary" type="button" id="csReset">${t('columnSchedule.reset')}</button>
    </div>
    <div class="field-grid">
      <div class="field"><label>${t('columnSchedule.story')}</label><select id="csStorySelect"></select></div>
      <label class="field-checkbox"><input type="checkbox" id="csApplyWholeType"> ${t('columnSchedule.applyWholeType')}</label>
    </div>
    <p class="combo-hint">${t('columnSchedule.hint')}</p>
    <div class="panel-actions two-up">
      <button class="button button-secondary" type="button" id="csExport">${t('columnAxial.export')}</button>
      <button class="button button-secondary" type="button" id="csExportDxf">${t('columnSchedule.exportDxf')}</button>
    </div>`;

  $('#csFetch', panel).addEventListener('click', runColumnScheduleFetch);
  $('#csReset', panel).addEventListener('click', columnScheduleReset);
  $('#csExport', panel).addEventListener('click', columnScheduleExportExcel);
  $('#csExportDxf', panel).addEventListener('click', columnScheduleExportDxf);
  $('#csApplyWholeType', panel).addEventListener('change', e => { columnScheduleState.applyToWholeType = e.target.checked; });
  $('#csStorySelect', panel).addEventListener('change', e => { columnScheduleState.selectedStory = e.target.value; renderColumnSchedulePlan(); });
}

function populateColumnScheduleStorySelect() {
  const select = $('#csStorySelect');
  if (!select) return;
  select.innerHTML = columnScheduleState.stories
    .map(s => `<option value="${s}" ${s === columnScheduleState.selectedStory ? 'selected' : ''}>${s}</option>`)
    .join('');
}

function renderColumnScheduleResultsPanel() {
  const panel = $('#resultsPanel');
  panel.innerHTML = `
    <div class="panel-heading compact"><div><span class="step-number">2</span><div><h2>${t('results.title')}</h2><p>${t('results.description')}</p></div></div>
      <div class="column-schedule-type-select">
        <select id="csTypeSelect"></select>
        <button class="button button-secondary" type="button" id="csSelectType">${t('columnSchedule.selectType')}</button>
      </div>
    </div>
    <div class="column-schedule-plan" id="csPlanWrap"></div>
    <div class="table-wrap">
      <table>
        <thead><tr>
          <th>${t('columnSchedule.table.type')}</th><th>${t('drift.table.story')}</th><th>${t('columnAxial.table.section')}</th>
          <th>${t('columnAxial.table.b')}</th><th>${t('columnSchedule.table.h')}</th><th>${t('columnSchedule.table.shape')}</th>
          <th>${t('columnSchedule.table.rebar')}</th><th>${t('columnSchedule.table.ratio')}</th><th></th>
        </tr></thead>
        <tbody id="csResultsBody"><tr><td colspan="9" class="table-empty">${t('drift.table.empty')}</td></tr></tbody>
      </table>
    </div>`;

  $('#csSelectType', panel).addEventListener('click', () => {
    const type = $('#csTypeSelect').value;
    columnScheduleSelectInModel(columnScheduleState.columns.filter(c => c.type === type));
  });

  renderColumnSchedulePlan();
  renderColumnScheduleResultsTable();
}

function populateColumnScheduleTypeSelect() {
  const select = $('#csTypeSelect');
  if (!select) return;
  const types = [...new Set(columnScheduleState.columns.map(c => c.type).filter(Boolean))]
    .sort((a, b) => columnScheduleTypeNumber(a) - columnScheduleTypeNumber(b));
  select.innerHTML = types.map(tp => `<option value="${tp}">${tp}</option>`).join('');
}

function columnScheduleSortedColumns() {
  const storyOrder = columnScheduleState.stories;
  return [...columnScheduleState.columns].sort((a, b) => {
    const diff = columnScheduleTypeNumber(a.type) - columnScheduleTypeNumber(b.type);
    if (diff !== 0) return diff;
    return storyOrder.indexOf(a.story) - storyOrder.indexOf(b.story);
  });
}

function renderColumnScheduleResultsTable() {
  const body = $('#csResultsBody');
  if (!body) return;
  populateColumnScheduleTypeSelect();

  const sorted = columnScheduleSortedColumns();
  body.innerHTML = sorted.length
    ? sorted.map(col => `
        <tr data-name="${col.name}">
          <td><strong>${col.type || ''}</strong></td><td>${col.story}</td><td>${col.section}</td>
          <td>${(col.width * 100).toFixed(0)}</td><td>${(col.depth * 100).toFixed(0)}</td>
          <td>${col.shape === 2 ? t('columnSchedule.shape.circle') : t('columnSchedule.shape.rect')}</td>
          <td><input type="text" class="cs-edit-rebar" data-name="${col.name}" value="${col.rebarLabel}"></td>
          <td>${calculateRebarRatio(col).toFixed(2)}%</td>
          <td><button class="text-button cs-select-one" data-name="${col.name}" type="button">${t('columnSchedule.selectOne')}</button></td>
        </tr>`).join('')
    : `<tr><td colspan="9" class="table-empty">${t('drift.table.empty')}</td></tr>`;

  $$('.cs-edit-rebar', body).forEach(input => {
    input.addEventListener('change', () => columnScheduleApplyRebarEdit(input.dataset.name, input.value));
  });
  $$('.cs-select-one', body).forEach(btn => {
    btn.addEventListener('click', () => {
      const col = columnScheduleState.columns.find(c => c.name === btn.dataset.name);
      if (col) columnScheduleSelectInModel([col]);
    });
  });
}

function renderColumnSchedulePlan() {
  const wrap = $('#csPlanWrap');
  if (!wrap) return;
  const cols = columnScheduleState.columns.filter(c => c.story === columnScheduleState.selectedStory);
  if (cols.length === 0) {
    wrap.innerHTML = `<div class="table-empty">${t('drift.table.empty')}</div>`;
    return;
  }

  const xs = cols.map(c => c.x), ys = cols.map(c => c.y);
  const minX = Math.min(...xs), maxX = Math.max(...xs);
  const minY = Math.min(...ys), maxY = Math.max(...ys);
  const pad = 1.5;
  const dataW = Math.max(maxX - minX, 1) + pad * 2;
  const dataH = Math.max(maxY - minY, 1) + pad * 2;
  const viewW = 640, viewH = 260;
  const scale = Math.min(viewW / dataW, viewH / dataH);
  const toScreenX = x => (x - minX + pad) * scale;
  const toScreenY = y => viewH - (y - minY + pad) * scale;

  const rects = cols.map(col => {
    const cx = toScreenX(col.x), cy = toScreenY(col.y);
    const rw = Math.max(col.width * scale, 4);
    const rh = Math.max(col.depth * scale, 4);
    const color = columnScheduleColorFor(col.rebarLabel);
    return `<g transform="translate(${cx},${cy}) rotate(${col.angle})">
      <rect x="${-rw / 2}" y="${-rh / 2}" width="${rw}" height="${rh}" fill="${color}" stroke="#333" stroke-width="0.5" data-name="${col.name}" class="cs-plan-col"></rect>
      <text x="0" y="3" font-size="8" text-anchor="middle" fill="#111" style="pointer-events:none;">${col.type || ''}</text>
    </g>`;
  }).join('');

  wrap.innerHTML = `<svg viewBox="0 0 ${viewW} ${viewH}" class="column-schedule-svg">${rects}</svg>`;

  $$('.cs-plan-col', wrap).forEach(rect => {
    rect.addEventListener('click', () => {
      const col = columnScheduleState.columns.find(c => c.name === rect.dataset.name);
      if (!col) return;
      const newRebar = prompt(t('columnSchedule.promptRebar', { current: col.rebarLabel }), col.rebarLabel);
      if (newRebar && newRebar !== col.rebarLabel) columnScheduleApplyRebarEdit(col.name, newRebar);
    });
  });
}

function columnScheduleApplyRebarEdit(name, newRebar) {
  const col = columnScheduleState.columns.find(c => c.name === name);
  if (!col || !newRebar || newRebar === col.rebarLabel) return;

  if (columnScheduleState.applyToWholeType) {
    for (const c of columnScheduleState.columns.filter(c => c.type === col.type)) c.rebarLabel = newRebar;
  } else {
    col.rebarLabel = newRebar;
  }

  generateColumnTypes(columnScheduleState.columns);
  renderColumnScheduleResultsTable();
  renderColumnSchedulePlan();
}

async function columnScheduleSelectInModel(cols) {
  if (!cols || cols.length === 0) return;
  const items = cols.map(c => ({ story: c.story, label: c.label }));
  try {
    const res = await postAgentJson('/api/etabs/select-frames', { items }, 90000);
    if (!res.etabsConnected) throw new Error(res.error || t('drift.error.notConnected'));
    log(t('columnSchedule.status.selected', { count: res.selectedCount }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  }
}

async function runColumnScheduleFetch() {
  const btn = $('#csFetch');
  btn.disabled = true;
  try {
    const [colsRes, storiesRes] = await Promise.all([
      fetchAgentJson('/api/etabs/column-schedule', 30000),
      fetchAgentJson('/api/etabs/stories')
    ]);
    if (!colsRes.etabsConnected) throw new Error(colsRes.error || t('drift.error.notConnected'));

    const columns = colsRes.rows.map(r => ({
      name: r.name, label: r.label, story: r.story, x: r.x, y: r.y,
      section: r.section, width: r.width, depth: r.depth, shape: r.shape, angle: r.angle,
      rebarLabel: r.rebarLabel, type: null
    }));
    generateColumnTypes(columns);

    columnScheduleState.columns = columns;
    columnScheduleState.original = columns.map(c => ({ ...c }));

    const storyOrder = (storiesRes.stories || []).slice().sort((a, b) => b.elevation - a.elevation).map(s => s.name);
    const usedStories = [...new Set(columns.map(c => c.story))];
    columnScheduleState.stories = storyOrder.filter(s => usedStories.includes(s))
      .concat(usedStories.filter(s => !storyOrder.includes(s)));
    columnScheduleState.selectedStory = columnScheduleState.stories[0] || null;

    populateColumnScheduleStorySelect();
    renderColumnScheduleResultsTable();
    renderColumnSchedulePlan();
    recordLastCheck('column-schedule');
    log(t('columnSchedule.status.fetched', { count: columns.length }), 'ok');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

function columnScheduleReset() {
  if (columnScheduleState.original.length === 0) return;
  columnScheduleState.columns = columnScheduleState.original.map(c => ({ ...c }));
  generateColumnTypes(columnScheduleState.columns);
  renderColumnScheduleResultsTable();
  renderColumnSchedulePlan();
  log(t('columnSchedule.status.reset'), 'ok');
}

async function columnScheduleExportExcel() {
  const columns = columnScheduleState.columns;
  if (columns.length === 0) { log(t('columnAxial.error.noFrameData'), 'error'); return; }
  const btn = $('#csExport');
  if (btn) btn.disabled = true;
  try {
    const sorted = columnScheduleSortedColumns();
    await downloadAgentExcel('/api/etabs/export/column-schedule', {
      rows: sorted.map(c => {
        const parts = extractRebarParts(c.rebarLabel) || { count: 0, dia: 0 };
        return {
          type: c.type || '', story: c.story, section: c.section,
          b: c.width * 100, h: c.depth * 100, shape: c.shape,
          rebarLabel: c.rebarLabel, rebarCount: parts.count, rebarDiaMm: parts.dia
        };
      })
    }, 'Kolon_Donesi.xlsx');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    if (btn) btn.disabled = false;
  }
}

// --- Kolon Donesi DXF export -----------------------------------------------
// Hand-written minimal ASCII DXF (R2000 / AC1015) writer: LINE, CIRCLE, TEXT,
// LWPOLYLINE entities only, one flat ENTITIES section, colors via ACI (group
// code 62), no LAYER table needed (everything drawn on default layer "0").
// Geometry/formulas mirror kolon_dwg_export.cs (ZWCAD script) 1:1 where practical;
// the circular arc "çiroz"/etriye tie details are intentionally simplified to a
// closed inner stirrup rectangle instead of replicating the bulge-arc tie ties,
// since the schedule information (count/diameter/ratio) is unaffected.
function dxfNum(n) {
  return (Math.round(n * 1000) / 1000).toString();
}

function dxfText(x, y, height, text, color, halign = 0, valign = 0) {
  const lines = ['0', 'TEXT', '8', '0', '62', String(color),
    '10', dxfNum(x), '20', dxfNum(y), '30', '0',
    '40', dxfNum(height), '1', String(text).replace(/[\r\n]+/g, ' ')];
  if (halign !== 0 || valign !== 0) {
    lines.push('72', String(halign), '73', String(valign), '11', dxfNum(x), '21', dxfNum(y), '31', '0');
  }
  return lines;
}

function dxfLine(x1, y1, x2, y2, color) {
  return ['0', 'LINE', '8', '0', '62', String(color),
    '10', dxfNum(x1), '20', dxfNum(y1), '30', '0',
    '11', dxfNum(x2), '21', dxfNum(y2), '31', '0'];
}

function dxfCircle(cx, cy, r, color) {
  return ['0', 'CIRCLE', '8', '0', '62', String(color),
    '10', dxfNum(cx), '20', dxfNum(cy), '30', '0', '40', dxfNum(r)];
}

function dxfPolyline(points, closed, color, constantWidth) {
  const lines = ['0', 'LWPOLYLINE', '8', '0', '62', String(color),
    '90', String(points.length), '70', closed ? '1' : '0'];
  if (constantWidth) lines.push('43', dxfNum(constantWidth));
  for (const [x, y] of points) lines.push('10', dxfNum(x), '20', dxfNum(y));
  return lines;
}

function dxfRotate(cx, cy, dx, dy, cosA, sinA) {
  return [cx + dx * cosA - dy * sinA, cy + dx * sinA + dy * cosA];
}

function dxfDonatiAdedi(k) {
  if (k <= 35) return 3; if (k <= 50) return 4; if (k <= 60) return 5; if (k <= 70) return 6;
  if (k <= 80) return 7; if (k <= 90) return 8; if (k <= 110) return 9; if (k <= 130) return 11;
  if (k <= 160) return 13; if (k <= 180) return 15; if (k <= 190) return 17; if (k <= 240) return 19;
  if (k <= 270) return 21; return 23;
}

// Draws one "type box" (dimensioned section + rebar layout + labels), matching
// TipCiz() in kolon_dwg_export.cs. Returns nothing; appends to `out`.
function dxfDrawTypeBox(out, refX, refY, b, h, rebarDia, color, baslik) {
  const FixedBoxWidth = 200, FixedBoxHeight = 400, Paspayi = 3.5, R = 1.75;
  const boxTopY = refY, boxBottomY = refY - FixedBoxHeight;
  const centerX = refX + FixedBoxWidth / 2;
  const t1PosY = boxTopY - 20;
  const infoStartY = t1PosY - 15;
  const colTopY = boxTopY - 110;
  const insX = centerX - b / 2;
  const insY = colTopY - h;

  const adetX = dxfDonatiAdedi(b), adetY = dxfDonatiAdedi(h);
  const toplamAdet = 2 * adetX + 2 * adetY - 4;
  const cap = rebarDia > 0 ? rebarDia : 16;
  const oran = (toplamAdet * Math.PI * Math.pow(cap / 20, 2) / (b * h)) * 100;

  out.push(...dxfPolyline([[insX, insY], [insX + b, insY], [insX + b, insY + h], [insX, insY + h]], true, 1));

  out.push(...dxfLine(insX, insY, insX, insY - 20, 3));
  out.push(...dxfLine(insX + b, insY, insX + b, insY - 20, 3));
  out.push(...dxfLine(insX, insY - 15, insX + b, insY - 15, 3));
  out.push(...dxfText(insX + b / 2, insY - 18, 4.0, Math.round(b), 3, 1, 3));

  out.push(...dxfLine(insX, insY, insX - 20, insY, 3));
  out.push(...dxfLine(insX, insY + h, insX - 20, insY + h, 3));
  out.push(...dxfLine(insX - 15, insY, insX - 15, insY + h, 3));
  out.push(...dxfText(insX - 18, insY + h / 2, 4.0, Math.round(h), 3, 2, 2));

  out.push(...dxfPolyline([[insX + Paspayi, insY + Paspayi], [insX + b - Paspayi, insY + Paspayi],
    [insX + b - Paspayi, insY + h - Paspayi], [insX + Paspayi, insY + h - Paspayi]], true, 7));

  const off = Paspayi + R;
  const sx = adetX > 1 ? (b - 2 * off) / (adetX - 1) : 0;
  const sy = adetY > 1 ? (h - 2 * off) / (adetY - 1) : 0;
  for (let i = 0; i < adetX; i++) {
    out.push(...dxfCircle(insX + off + i * sx, insY + h - off, R, 7));
    out.push(...dxfCircle(insX + off + i * sx, insY + off, R, 7));
  }
  for (let j = 1; j < adetY - 1; j++) {
    out.push(...dxfCircle(insX + off, insY + off + j * sy, R, 7));
    out.push(...dxfCircle(insX + b - off, insY + off + j * sy, R, 7));
  }

  out.push(...dxfPolyline([[refX, boxBottomY], [refX + FixedBoxWidth, boxBottomY],
    [refX + FixedBoxWidth, boxTopY], [refX, boxTopY]], true, color));

  out.push(...dxfText(centerX, t1PosY, 12.0, baslik, 3, 1, 1));
  out.push(...dxfText(centerX, infoStartY, 10.0, `${toplamAdet}Ø${cap}`, 2, 1, 3));
  out.push(...dxfText(centerX, infoStartY - 20.0, 10.0, `%${oran.toFixed(2)}`, 2, 1, 3));

  return boxBottomY;
}

function buildColumnScheduleDxf() {
  const PlanScale = 100; // ETABS m -> DXF cm, matches desktop
  const priorityColors = [1, 2, 3, 4, 6, 7, 5, 30, 150, 190, 11, 68, 144, 171, 240, 115, 225, 53];
  const story = columnScheduleState.selectedStory;
  const storyColumns = columnScheduleState.columns.filter(c => c.story === story);
  const allColumns = columnScheduleState.columns;
  const storyOrder = columnScheduleState.stories;

  const typesInOrder = [...new Set(storyColumns.map(c => c.type || 'Bilinmiyor'))]
    .sort((a, b) => columnScheduleTypeNumber(a) - columnScheduleTypeNumber(b));
  const typeColorMap = {};
  typesInOrder.forEach((tp, idx) => { typeColorMap[tp] = priorityColors[idx % priorityColors.length]; });

  const out = [];

  // 1. PLAN VIEW
  out.push(...dxfText(0, 50, 5.0, `KAT PLANI - ${story}`, 4));
  for (const col of storyColumns) {
    const cx = col.x * PlanScale, cy = col.y * PlanScale;
    const w = col.width * PlanScale, d = col.depth * PlanScale;
    const rad = (col.angle || 0) * Math.PI / 180;
    const cosA = Math.cos(rad), sinA = Math.sin(rad);
    const hw = w / 2, hd = d / 2;
    const c0 = dxfRotate(cx, cy, -hw, -hd, cosA, sinA), c1 = dxfRotate(cx, cy, hw, -hd, cosA, sinA);
    const c2 = dxfRotate(cx, cy, hw, hd, cosA, sinA), c3 = dxfRotate(cx, cy, -hw, hd, cosA, sinA);
    const pad = 15, mHw = hw + pad, mHd = hd + pad;
    const m0 = dxfRotate(cx, cy, -mHw, -mHd, cosA, sinA), m1 = dxfRotate(cx, cy, mHw, -mHd, cosA, sinA);
    const m2 = dxfRotate(cx, cy, mHw, mHd, cosA, sinA), m3 = dxfRotate(cx, cy, -mHw, mHd, cosA, sinA);
    const color = typeColorMap[col.type || 'Bilinmiyor'] || 7;

    out.push(...dxfPolyline([m0, m1, m2, m3], true, color, 0.5));
    out.push(...dxfPolyline([c0, c1, c2, c3], true, 7));

    const label = col.rebarLabel || col.type || col.name;
    out.push(...dxfText(cx + mHw + 2, cy, 1.5, label, 7));
  }

  // 2. TYPE DETAIL DRAWINGS (+ story-to-story necking, matching DrawColumnTypeDetails)
  const FixedBoxWidth = 200, FixedBoxHeight = 400;
  let curX = 0, rowY = -600;
  let minX = curX, maxX = curX, minY = rowY;

  for (const tp of typesInOrder) {
    const grp = storyColumns.filter(c => c.type === tp);
    const rep = grp[0];
    const adet = grp.length;
    const parts = extractRebarParts(rep.rebarLabel) || { count: 0, dia: 0 };
    const b = rep.width * 100, h = rep.depth * 100;
    const color = typeColorMap[tp] || 7;
    const bCm = Math.min(b, h), hCm = Math.max(b, h);
    const baslik = `${tp} (${bCm.toFixed(0)}x${hCm.toFixed(0)}) (Adet:${adet})`;

    let curY = rowY;
    const boxBottom = dxfDrawTypeBox(out, curX, curY, b, h, parts.dia, color, baslik);
    if (curX < minX) minX = curX;
    if (curX + FixedBoxWidth > maxX) maxX = curX + FixedBoxWidth;
    if (boxBottom < minY) minY = boxBottom;
    curY -= (FixedBoxHeight + 50.0);

    if (storyOrder && storyOrder.length > 1) {
      const refCol = rep;
      const zincir = allColumns
        .filter(c => Math.abs(c.x - refCol.x) < 0.01 && Math.abs(c.y - refCol.y) < 0.01)
        .sort((a, c) => storyOrder.indexOf(a.story) - storyOrder.indexOf(c.story));

      let oncekiBoyut = `${bCm.toFixed(0)}x${hCm.toFixed(0)}`;
      for (const zCol of zincir) {
        const zB = zCol.width * 100, zH = zCol.depth * 100;
        const zBCm = Math.min(zB, zH), zHCm = Math.max(zB, zH);
        const yeniBoyut = `${zBCm.toFixed(0)}x${zHCm.toFixed(0)}`;
        if (yeniBoyut !== oncekiBoyut) {
          const daralmaBaslik = `${zCol.story} Daralması`;
          const zParts = extractRebarParts(zCol.rebarLabel) || parts;
          const bb = dxfDrawTypeBox(out, curX, curY, zB, zH, zParts.dia, color, daralmaBaslik);
          if (bb < minY) minY = bb;
          curY -= (FixedBoxHeight + 50.0);
          oncekiBoyut = yeniBoyut;
        }
      }
    }
    curX += (FixedBoxWidth + 35.0);
  }

  const cerceveY = minY - 100;
  out.push(...dxfPolyline([[minX - 100, cerceveY], [maxX + 100, cerceveY],
    [maxX + 100, rowY + 100], [minX - 100, rowY + 100]], true, 7, 5.0));
  out.push(...dxfText((minX + maxX) / 2 + 100, rowY + 150, 125.0, 'KOLON DONESİ', 2, 1, 1));

  const doc = ['0', 'SECTION', '2', 'HEADER', '9', '$ACADVER', '1', 'AC1021', '0', 'ENDSEC',
    '0', 'SECTION', '2', 'ENTITIES', ...out, '0', 'ENDSEC', '0', 'EOF'];
  return doc.join('\r\n');
}

function columnScheduleExportDxf() {
  if (columnScheduleState.columns.length === 0) { log(t('columnAxial.error.noFrameData'), 'error'); return; }
  const dxfText = buildColumnScheduleDxf();
  const blob = new Blob([dxfText], { type: 'application/dxf' });
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = `Kolon_Donesi_${columnScheduleState.selectedStory || 'plan'}.dxf`;
  a.click();
  URL.revokeObjectURL(url);
  log(t('columnSchedule.status.dxfSaved'), 'ok');
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

$('#languageToggle').addEventListener('click', () => applyLanguage(currentLanguage === 'en' ? 'tr' : 'en'));
window.addEventListener('hashchange', () => setActiveView(location.hash.slice(1)));
applyLanguage(currentLanguage);
