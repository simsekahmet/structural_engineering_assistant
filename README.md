# Structural Engineering Assistant

Structural Engineering Assistant combines the existing ETABS desktop checks, a bilingual web interface, and a local Windows connection agent in a single repository.

## Live web application

The static interface is stored in `web/` and deployed to GitHub Pages from the `main` branch:

https://simsekahmet.github.io/structural_engineering_assistant/

## Connecting to ETABS

1. Install ETABS 22 or later and open the model you want to inspect.
2. Download `StructuralEngineeringAssistant.Agent.exe` from the latest GitHub release.
3. Run the agent. It stays available in the Windows notification area.
4. Open the web application and select **Connect to ETABS**.

**Your ETABS model must be in kN, m, C units.** ETABS returns table values in the model's present units and every module assumes kN-m, so any other unit system produces wrong numbers. The pre-flight panel shown after connecting checks this, along with the agent/web version match, the ETABS version, the model lock state and whether the analysis has finished.

### What the agent does and does not do

The agent binds to `127.0.0.1:5218` only, so it is not reachable from the local network or the internet. It requires an `Origin` header on every request and accepts only the deployed site and local development origins — requests without an Origin (curl and other non-browser clients) are rejected. Every accepted call is logged to `%TEMP%\StructuralEngineeringAssistant.Agent.log` with a READ/WRITE marker. Two endpoints write to the model, `/api/etabs/select-frames` and `/api/etabs/select-piers`; both only change the selection — never geometry, sections or design data.

There is no authentication token and no per-action confirmation prompt: any program already running under your Windows account can call the agent. That is not a meaningful escalation, since such a program could drive the ETABS COM API directly, but the agent is a local convenience rather than a security boundary. Close it when you are not using it.

## Documentation

- [Module implementation status, supported ETABS versions and roadmap](docs/STATUS.md)
- [Validation cases](docs/VALIDATION.md) — hand calculations reproducing the tool's output
- [Known issues and limitations](docs/KNOWN-ISSUES.md)
- [Release notes](https://github.com/simsekahmet/structural_engineering_assistant/releases)

Every migrated module also carries an in-app **Calculation basis** panel citing the TBDY 2018 / TS 500 clause, the equations used and the intermediate values, so results can be re-derived by hand.

Load combinations, story mass, modal periods and base shear are read from the model automatically when a module opens — there are no Fetch buttons. Combinations are chosen in a two-list picker (available on the left, selected on the right), and each module has a **Reset results** button that clears its computed output.

Every module also offers a **rigid basement** rule: stories at or below a chosen story are read from a separate basement combination list instead of the superstructure one.

If more than one ETABS instance is open, **Connect to ETABS** lists them and lets you pick the model. A **Disconnect** button releases the model and clears everything fetched from it.

## Reporting

The **Report** module builds the calculation report a design office delivers: an ordered list of steps with per-step progress, auto-composed texts that leave empty project variables out (each one can be taken over manually), image slots filled individually or in bulk from a folder by code name, a live cover preview, and template save/load as a `.json` file. It is being built step by step against the report outline — only the cover step is implemented so far, and every other step is listed but marked "to be defined" rather than guessed at. Report data is kept in your own browser and is never sent anywhere. See the *Reporting* section of [docs/STATUS.md](docs/STATUS.md).

## Repository structure

- `web/`: English/Turkish GitHub Pages interface (`version.js` is the single source of the web version).
- `agent/`: .NET 8 Windows tray agent and local ETABS bridge (version comes from the `.csproj`).
- `docs/`: status, validation cases, known issues.
- `scripts/check-version.sh`: fails CI if the cache-bust tokens drift from `version.js`.
- Repository root: existing .NET Framework 4.8 WinForms engineering application.
- `.github/workflows/pages.yml`: web deployment.
- `.github/workflows/agent-release.yml`: single-file Windows agent release build.

## Local development

Run the web interface:

```powershell
cd web
python -m http.server 4173
```

Build the Windows agent:

```powershell
dotnet publish agent/StructuralEngineeringAssistant.Agent.csproj --configuration Release --runtime win-x64 --self-contained true -p:PublishSingleFile=true
```

The active ETABS model must be open in the same Windows user session as the agent.

> Engineering results must always be reviewed and approved by the responsible structural engineer.
