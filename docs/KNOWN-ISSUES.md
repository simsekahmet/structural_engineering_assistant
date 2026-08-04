# Known issues and limitations

Current as of **web v1.14.0 / agent v1.13.0**. Release notes live on the
[Releases page](https://github.com/simsekahmet/structural_engineering_assistant/releases).

## Limitations you must be aware of before relying on a result

- **Unit system must be kN, m, C.** ETABS returns table values in the model's
  *present* units, and every module assumes kN-m. A model left in kip-in produces
  plausible-looking but wrong numbers. The pre-flight check flags this, but it
  cannot fix it — change the units in ETABS.
- **Wall Shear's short-wall, 0.5V and rigid-basement rules have no documented
  validation case yet** (the core Vr/Vmax path is validated as VC-05). See
  `VALIDATION.md`.
- **The Report module is under construction.** Steps 1–4 of the Introduction
  process are implemented; the remaining steps and the appendix processes are
  listed but marked "to be defined", and there is no document output yet. See the
  *Reporting* section of `STATUS.md`.
- **Material and slab reads are a starting point, not an answer.** A model can hold
  several concrete or rebar materials; the agent quotes the governing (highest)
  strength and infers the class from the material name where it carries one. Check
  the pre-selected row against the model before signing the report — the reference
  table is clickable precisely so it can be corrected.
- **The plan extent is the point envelope of a middle storey.** It includes anything
  modelled at that level, so a projecting balcony or an isolated point moves it.
  Compare it with the formwork plan before quoting it.
- **Report data lives in this browser.** It is stored locally and never sent
  anywhere, which also means clearing browser data or switching machine loses it.
  Use *Save template* to keep a copy. Cover images are downscaled to 1600 px on the
  long edge before they are stored.
- **Results are not a design.** Every output must be reviewed and approved by the
  responsible structural engineer. The tool reproduces code equations; it does not
  exercise engineering judgement.

## Functional issues

- **Combination matching is letter-based, not word-based.** Combos are bucketed into
  X/Y and ÜST/ALT by looking for those characters in the combination name. A combo
  named e.g. `COMBO_MAX` can therefore land in an unintended bucket. This mirrors the
  desktop application's behaviour and was ported deliberately rather than "fixed", so
  the two tools agree. Check the combination list you select.
- **`select-frames` is O(n) over model frames.** On a ~166-frame model a "select in
  model" call takes a few hundred ms warm, but the first call after connecting has to
  enumerate every frame and can take noticeably longer.
- **The schedule ("done") modules were removed in v1.11.0.** Column Schedule had been
  migrated in v1.3–v1.5 and withdrawn in v1.7.0; the remaining schedule screens were
  never implemented. The application now covers analysis checks and member checks only.
- **Analysis-status detection is best-effort.** If ETABS does not expose
  `Analyze.GetCaseStatus` the pre-flight row shows "Unknown" rather than failing.

## Environment issues

- **Run the agent from one place.** If an older agent build is still running it keeps
  port 5218 and newer builds silently fail to bind, so the UI talks to the stale one.
  Check the version in the pre-flight panel; kill stray processes if it looks wrong.
- **Administrator mismatch.** If ETABS runs elevated, the agent must run elevated too,
  otherwise it cannot see the running instance.
- **Agent must be restarted after an update.** It does not self-update.

## Security posture — what is and is not true

The agent binds to `127.0.0.1:5218` only, so it is not reachable from the local
network or the internet. It requires an `Origin` header on every request and accepts
only `https://simsekahmet.github.io` and the local development origins; requests with
no Origin (curl and other non-browser clients) are rejected. Every accepted call is
appended to `%TEMP%\StructuralEngineeringAssistant.Agent.log` with a READ/WRITE
marker. Two endpoints touch the model — `/api/etabs/select-frames` and
`/api/etabs/select-piers` — and both only change the selection; neither modifies
geometry, sections or design data.

What is **not** implemented: there is no authentication token and no per-action user
confirmation. Any program already running on your machine under your account can call
the agent by sending an allowed Origin header. That is not a meaningful escalation —
such a program could drive the ETABS COM API directly anyway — but it means the agent
should be treated as a local convenience, not a security boundary. Close it when you
are not using it.
