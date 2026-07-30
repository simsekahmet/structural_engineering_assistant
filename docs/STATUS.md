# Module implementation status

Last updated for **web v1.6.0 / agent v1.6.0**.

"Migrated" means the calculation itself runs against the live ETABS model and has
been checked against an independent hand calculation. "UI only" means the screen
exists but no calculation is wired up — those modules disable their connect button.

| Module | Status | Source (desktop) | Notes |
| --- | --- | --- | --- |
| Design Spectrum (Tasarım Spektrumu) | ✅ Migrated | `SpectrumManager.cs` | Pure TBDY 2018 math, no ETABS read. Feeds the Scaling module. |
| Scaling Calculation (Artırım Hesabı) | ✅ Migrated | `artirim_hesabi.cs` | Needs the Spectrum module to have been run first. |
| Interstory Drift (Göreli Kat Ötelemesi) | ✅ Migrated | `goreli_kat_otelemesi.cs` | |
| Second-Order Effects (İkinci Mertebe) | ✅ Migrated | `ikinci_mertebe.cs` | |
| Column Axial Load (Kolon Eksenel) | ✅ Migrated | `kolon_eksenel_yuk_kontrolu.cs` | Editable b/d, select-failing-in-model, formula-backed Excel. |
| Beam Shear (Kiriş Kesme) | ✅ Migrated | `kiris_kesme.cs` | Editable n/φ/s. |
| Beam Axial Load (Kiriş Eksenel) | ✅ Migrated | `kiris_eksenel_yuk.cs` | |
| Column Schedule (Kolon Donesi) | ✅ Migrated | `kolon_donesi.cs`, `kolon_dwg_export.cs` | Plan view, type grouping, TBDY ρ check, Excel + DXF export. |
| Wall Shear (Perde Kesme) | ⬜ UI only | `perde_kesme.cs` | Area/Pier-object based; needs an Area equivalent of `select-frames`. |
| Wall Axial Load (Perde Eksenel) | ⬜ UI only | `perde_eksenel.cs` | Same as above. |
| Wall Schedule (Perde Donesi) | ⬜ UI only | — | Empty placeholder in the desktop app too — nothing to migrate. |
| Beam Schedule (Kiriş Donesi) | ⬜ UI only | — | Empty placeholder in the desktop app too. |
| Slab Schedule (Döşeme Donesi) | ⬜ UI only | — | Empty placeholder in the desktop app too. |
| Foundation Schedule (Temel Donesi) | ⬜ UI only | — | Empty placeholder in the desktop app too. |

## Supported environment

| Item | Supported |
| --- | --- |
| ETABS | v22 (developed and tested against ETABS 22 with `ETABSv1.dll`). Other v1-API versions may work but are untested. |
| Operating system | Windows 10 / 11 x64 (the agent is a .NET 8 WinForms tray app). |
| Unit system | **kN, m, C only.** ETABS returns table values in the model's *present* units and the modules assume kN-m, so any other unit system produces wrong numbers. The pre-flight check blocks on this. |
| Browsers | Any current Chromium, Firefox or Edge build. The agent accepts requests only from `https://simsekahmet.github.io` and `http://localhost:4173`. |

## Roadmap

1. Wall Shear and Wall Axial — the last two real calculations still on the desktop.
   Requires extending the agent's write endpoint from `FrameObj` to `AreaObj`/Pier
   objects before "select failing in model" can work for walls.
2. Validation cases for the member checks (see `VALIDATION.md`) — currently the
   analysis modules have documented hand-checks, the member checks do not.
3. PDF reporting alongside the existing Excel/DXF exports.

There is no committed delivery date for any of these.
