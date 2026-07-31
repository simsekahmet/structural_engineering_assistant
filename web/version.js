// Single source of truth for the WEB interface version.
//
// The agent version is independent and comes from the running agent itself
// (csproj <Version> -> AssemblyInformationalVersion -> /api/health), so the two
// are reported separately in the pre-flight panel and never hand-copied.
//
// The `?v=` query strings on styles.css / app.js in index.html are cache-busting
// tokens that must match this value; scripts/check-version.sh fails CI if they drift.
const APP_VERSION = '1.7.0';
