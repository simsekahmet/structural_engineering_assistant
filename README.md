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

The agent listens only on `127.0.0.1:5218`. It does not expose ETABS to the local network or the internet. The API currently reports agent health, the active model name, model path, and lock status. Engineering calculation modules will be migrated incrementally from the existing WinForms application.

## Repository structure

- `web/`: English/Turkish GitHub Pages interface.
- `agent/`: .NET 8 Windows tray agent and local ETABS bridge.
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
