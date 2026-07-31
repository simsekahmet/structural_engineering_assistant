# Module implementation status

Last updated for **web v1.10.0 / agent v1.10.0**.

"Migrated" means the calculation itself runs against the live ETABS model and has
been checked against an independent hand calculation. "UI only" means the screen
exists but no calculation is wired up — those modules disable their connect button.

| Module | Status | Source (desktop) | Notes |
| --- | --- | --- | --- |
| Design Spectrum (Tasarım Spektrumu) | ✅ Migrated | `SpectrumManager.cs` | Pure TBDY 2018 math, no ETABS read. Feeds the Scaling module. |
| Base Shear Amplification (Taban Kesme Kuvveti Büyütmesi) | ✅ Migrated | `artirim_hesabi.cs` | Needs the Spectrum module to have been run first. |
| Interstory Drift (Göreli Kat Ötelemesi) | ✅ Migrated | `goreli_kat_otelemesi.cs` | |
| Second-Order Effects (İkinci Mertebe) | ✅ Migrated | `ikinci_mertebe.cs` | |
| Column Axial Load (Kolon Eksenel) | ✅ Migrated | `kolon_eksenel_yuk_kontrolu.cs` | Editable b/d, select-failing-in-model, formula-backed Excel. |
| Beam Shear (Kiriş Kesme) | ✅ Migrated | `kiris_kesme.cs` | Editable n/φ/s. |
| Beam Axial Load (Kiriş Eksenel) | ✅ Migrated | `kiris_eksenel_yuk.cs` | |
| Wall Shear (Perde Kesme) | ✅ Migrated | `perde_kesme.cs` | Chooses the rebar layout per story; short-wall, 0.5V and rigid-basement rules with their own detail tables; editable bw/lw and coupled flag. |
| Wall Axial Load (Perde Eksenel) | ✅ Migrated | `perde_eksenel.cs` | Limit 0.35 (not 0.40); d is the wall length end-to-end. Pier data comes from `Results.PierForce` + `PierLabel.GetSectionProperties`, not a display table. Includes "select failing walls in model" via the AreaObj-based select-piers endpoint. |
| Column Schedule (Kolon Donesi) | ⬜ UI only | `kolon_donesi.cs`, `kolon_dwg_export.cs` | Was migrated in v1.3–v1.5 and **withdrawn in v1.7.0**: schedules are deferred until every analysis and member check ahead of them is verified. |
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

Work is deliberately ordered: **everything that comes before the schedules must be
complete and verified first.** Schedule ("done") modules are only revisited after that.

1. **Validation cases for the four undocumented checks** — Interstory Drift,
   Second-Order Effects, Beam Shear, Beam Axial (see `VALIDATION.md`).
2. **Story grouping** for Wall Shear — applying one rebar layout to a group of
   stories rather than choosing per story. Deferred from the initial migration.
3. **Schedules (done modules)** — Column Schedule was built and then withdrawn in
   v1.7.0 so it can be reintroduced on a verified foundation. Wall/Beam/Slab/
   Foundation schedules are empty in the desktop app too and need their scope
   defined before anything can be migrated.
4. PDF reporting alongside the existing Excel exports.

There is no committed delivery date for any of these.
