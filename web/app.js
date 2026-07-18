const moduleDefinitions = [
  { id: 'spectrum', key: 'spectrum', icon: '⌁', categoryKey: 'category.analysis' },
  { id: 'increment', key: 'increment', icon: '↟', categoryKey: 'category.analysis' },
  { id: 'drift', key: 'drift', icon: '↔', categoryKey: 'category.analysis' },
  { id: 'pdelta', key: 'pdelta', icon: 'ϑ', categoryKey: 'category.analysis' },
  { id: 'column-axial', key: 'columnAxial', icon: '▥', categoryKey: 'category.memberChecks' },
  { id: 'wall-shear', key: 'wallShear', icon: '▤', categoryKey: 'category.memberChecks' },
  { id: 'wall-axial', key: 'wallAxial', icon: '▯', categoryKey: 'category.memberChecks' },
  { id: 'beam-shear', key: 'beamShear', icon: '═', categoryKey: 'category.memberChecks' },
  { id: 'beam-axial', key: 'beamAxial', icon: '⇥', categoryKey: 'category.memberChecks' },
  { id: 'column-schedule', key: 'columnSchedule', icon: '▦', categoryKey: 'category.schedules' },
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
    'stat.connectionRequired': 'ETABS connection required', 'stat.readyModules': 'Interface modules', 'stat.migrationDefined': 'Calculation engines not migrated yet', 'status.uiOnly': 'UI only',
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
    'about.purpose.title': 'Engineering workspace', 'about.purpose.text': 'Structural Engineering Assistant brings ETABS checks, member schedules, results, and exports into one bilingual web interface.',
    'about.connection.title': 'Local ETABS bridge', 'about.connection.text': 'Because browsers cannot access the ETABS COM API directly, a secure Windows agent will connect this interface to the model open on your computer.',
    'about.status.title': 'Current version', 'about.status.text': 'The bilingual interface and ETABS connection agent are available. Engineering calculation modules are being migrated incrementally.',
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
    'drift.calculate': 'Calculate', 'drift.export': 'Download CSV',
    'drift.table.story': 'Story', 'drift.table.combo': 'Combination', 'drift.table.direction': 'Direction',
    'drift.table.drift': 'Drift', 'drift.table.lambdaDrift': 'λδi/hi', 'drift.table.limit': 'Limit', 'drift.table.status': 'Status',
    'drift.table.empty': 'Fetch combinations, select the ones to check, then Calculate.',
    'drift.status.pending': 'Waiting for calculation…',
    'drift.status.passed': 'Interstory drift check is satisfied.',
    'drift.status.failed': 'Interstory drift check is NOT satisfied.',
    'drift.error.notConnected': 'Connect to ETABS first.',
    'drift.error.noCombos': 'Select at least one combination.',
    'drift.error.noData': 'No story drift data was returned for the selected combinations.',
    'drift.error.fetchFailed': 'Could not reach the local ETABS bridge'
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
    'stat.connectionRequired': 'ETABS bağlantısı gerekli', 'stat.readyModules': 'Arayüz modülü', 'stat.migrationDefined': 'Hesap motorları henüz taşınmadı', 'status.uiOnly': 'Yalnızca arayüz',
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
    'about.purpose.title': 'Mühendislik çalışma alanı', 'about.purpose.text': 'Structural Engineering Assistant; ETABS tahkiklerini, eleman donelerini, sonuçları ve dışa aktarımları çift dilli tek bir web arayüzünde birleştirir.',
    'about.connection.title': 'Yerel ETABS köprüsü', 'about.connection.text': 'Tarayıcılar ETABS COM API’ye doğrudan erişemediği için güvenli bir Windows agent bu arayüzü bilgisayarınızda açık olan modele bağlayacaktır.',
    'about.status.title': 'Mevcut sürüm', 'about.status.text': 'Çift dilli arayüz ve ETABS bağlantı agent’ı kullanılabilir. Mühendislik hesap modülleri kademeli olarak taşınmaktadır.',
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
    'drift.calculate': 'Hesapla', 'drift.export': 'CSV İndir',
    'drift.table.story': 'Kat', 'drift.table.combo': 'Kombinasyon', 'drift.table.direction': 'Yön',
    'drift.table.drift': 'Drift', 'drift.table.lambdaDrift': 'λδi/hi', 'drift.table.limit': 'Limit', 'drift.table.status': 'Durum',
    'drift.table.empty': 'Kombinasyonları getirin, tahkik edilecekleri seçin, ardından Hesaplayın.',
    'drift.status.pending': 'Hesaplama bekleniyor…',
    'drift.status.passed': 'Göreli kat ötelemesi tahkiki sağlanmıştır.',
    'drift.status.failed': 'Göreli kat ötelemesi tahkiki sağlanmamıştır.',
    'drift.error.notConnected': "Önce ETABS'a bağlanın.",
    'drift.error.noCombos': 'En az bir kombinasyon seçin.',
    'drift.error.noData': 'Seçili kombinasyonlar için story drift verisi bulunamadı.',
    'drift.error.fetchFailed': 'Yerel ETABS köprüsüne ulaşılamadı'
  }
};

const $ = (selector, root = document) => root.querySelector(selector);
const $$ = (selector, root = document) => [...root.querySelectorAll(selector)];
const moduleGrid = $('#moduleGrid');
const dashboard = $('#dashboard');
const moduleView = $('#moduleView');
const terminal = $('#terminal');
let currentLanguage = localStorage.getItem('sea-language') === 'tr' ? 'tr' : 'en';

const AGENT_BASE = 'http://127.0.0.1:5218';
const defaultSetupPanelHtml = $('#setupPanel').innerHTML;
const defaultResultsPanelHtml = $('#resultsPanel').innerHTML;

function t(key, values = {}) {
  const value = translations[currentLanguage][key] ?? translations.en[key] ?? key;
  return Object.entries(values).reduce((text, [name, replacement]) => text.replaceAll(`{${name}}`, replacement), value);
}

function renderModules() {
  const expanded = moduleGrid.classList.contains('expanded');
  moduleGrid.innerHTML = moduleDefinitions.map((module, index) => `
    <button class="module-card ${index >= 6 ? 'extra' : ''}" type="button" data-module="${module.id}">
      <span class="module-icon">${module.icon}</span>
      <strong>${t(`module.${module.key}.title`)}</strong>
      <small>${t(module.categoryKey)} · ${t('status.uiOnly')}</small>
    </button>`).join('');
  moduleGrid.classList.toggle('expanded', expanded);
}

function timestamp() {
  return new Intl.DateTimeFormat(currentLanguage === 'tr' ? 'tr-TR' : 'en-GB', { hour: '2-digit', minute: '2-digit', second: '2-digit' }).format(new Date());
}

function log(message, type = 'info') {
  const symbol = type === 'ok' ? '✓' : type === 'error' ? '!' : 'i';
  terminal.insertAdjacentHTML('beforeend', `<p><time>${timestamp()}</time><span class="${type}">${symbol}</span> ${message}</p>`);
  terminal.scrollTop = terminal.scrollHeight;
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

  if (id === 'drift') {
    renderDriftSetupPanel();
    renderDriftResultsPanel();
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

  $('#driftExport', panel).addEventListener('click', exportDriftCsv);
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
    log(t(result.allPassed ? 'drift.status.passed' : 'drift.status.failed'), result.allPassed ? 'ok' : 'error');
  } catch (error) {
    log(`${t('drift.error.fetchFailed')}: ${error.message}`, 'error');
  } finally {
    btn.disabled = false;
  }
}

function exportDriftCsv() {
  const result = driftState.lastResult;
  if (!result) return;
  const sorted = sortDriftItems(result.items);
  const lines = ['Kat;Kombinasyon;Dogrultu;Drift;LambdaDrift;Limit;Durum'];
  for (const item of sorted) {
    lines.push([
      item.story, item.outputCase, item.direction,
      item.drift.toFixed(5), item.lambdaDrift.toFixed(5), item.limit.toFixed(5),
      item.isOk ? 'OK' : 'FAIL'
    ].join(';'));
  }
  const blob = new Blob(['﻿' + lines.join('\r\n')], { type: 'text/csv;charset=utf-8;' });
  const url = URL.createObjectURL(blob);
  const a = document.createElement('a');
  a.href = url;
  a.download = `GoreliKat_Sonuc_${new Date().toISOString().slice(0, 10).replace(/-/g, '')}.csv`;
  a.click();
  URL.revokeObjectURL(url);
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
$('#clearTerminal').addEventListener('click', () => { terminal.innerHTML = `<p><time>${timestamp()}</time><span class="info">i</span> ${t('terminal.cleared')}</p>`; });

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
