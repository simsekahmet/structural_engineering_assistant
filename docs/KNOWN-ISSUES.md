# Known issues and limitations

Current as of **web v1.8.0 / agent v1.8.0**. Release notes live on the
[Releases page](https://github.com/simsekahmet/structural_engineering_assistant/releases).

## Limitations you must be aware of before relying on a result

- **Unit system must be kN, m, C.** ETABS returns table values in the model's
  *present* units, and every module assumes kN-m. A model left in kip-in produces
  plausible-looking but wrong numbers. The pre-flight check flags this, but it
  cannot fix it — change the units in ETABS.
- **Four checks have no documented validation case yet** — Interstory Drift,
  Second-Order Effects, Beam Shear, Beam Axial. See `VALIDATION.md`.
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
- **All schedule ("done") modules are UI only, including Column Schedule.** Column
  Schedule was migrated in v1.3–v1.5 and deliberately withdrawn in v1.7.0: schedules
  are being deferred until every analysis and member check ahead of them is verified.
  Its screen still exists with the connect button disabled.
- **Wall modules are UI only.** Wall Shear and Wall Axial screens exist but perform no
  calculation; their connect button is disabled.
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
marker. Exactly one endpoint touches the model — `/api/etabs/select-frames` — and it
only changes the selection; it does not modify geometry, sections or design data.

What is **not** implemented: there is no authentication token and no per-action user
confirmation. Any program already running on your machine under your account can call
the agent by sending an allowed Origin header. That is not a meaningful escalation —
such a program could drive the ETABS COM API directly anyway — but it means the agent
should be treated as a local convenience, not a security boundary. Close it when you
are not using it.
