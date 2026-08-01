# Module implementation status

Last updated for **web v1.12.0 / agent v1.12.0**.

"Migrated" means the calculation itself runs against the live ETABS model and has
been checked against an independent hand calculation. Every module listed below performs a real calculation; the schedule ("done") screens
were removed in v1.11.0.

| Module | Status | Source (desktop) | Notes |
| --- | --- | --- | --- |
| Reduced Design Spectrum (Azaltılmış Tasarım Spektrumu) | ✅ Migrated | `SpectrumManager.cs` | Pure TBDY 2018 math, no ETABS read. Feeds the Scaling module. |
| Base Shear Amplification (Taban Kesme Kuvveti Büyütmesi) | ✅ Migrated | `artirim_hesabi.cs` | Needs the Spectrum module to have been run first. |
| Interstory Drift (Göreli Kat Ötelemesi) | ✅ Migrated | `goreli_kat_otelemesi.cs` | |
| Second-Order Effects (İkinci Mertebe) | ✅ Migrated | `ikinci_mertebe.cs` | |
| Column Axial Load (Kolon Eksenel) | ✅ Migrated | `kolon_eksenel_yuk_kontrolu.cs` | Editable b/d, select-failing-in-model, formula-backed Excel. |
| Beam Shear (Kiriş Kesme) | ✅ Migrated | `kiris_kesme.cs` | Editable n/φ/s. |
| Beam Axial Load (Kiriş Eksenel) | ✅ Migrated | `kiris_eksenel_yuk.cs` | |
| Wall Shear (Perde Kesme) | ✅ Migrated | `perde_kesme.cs` | Chooses the rebar layout per story; short-wall, 0.5V and rigid-basement rules with their own detail tables; editable bw/lw and coupled flag. |
| Wall Axial Load (Perde Eksenel) | ✅ Migrated | `perde_eksenel.cs` | Limit 0.35 (not 0.40); d is the wall length end-to-end. Pier data comes from `Results.PierForce` + `PierLabel.GetSectionProperties`, not a display table. Includes "select failing walls in model" via the AreaObj-based select-piers endpoint. |

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

Every migrated calculation now has a documented validation case (VC-01 to VC-09 in
`VALIDATION.md`). What remains:

1. **Wall Shear's short-wall, 0.5V and rigid-basement rules** — the core Vr/Vmax path
   is validated as VC-05, but those three optional rules still need a model that
   exercises them.
2. PDF reporting alongside the existing Excel exports.

Explicitly **not** planned: the schedule ("done") modules — removed from the
application in v1.11.0; story grouping for Wall Shear; and Column Shear.

There is no committed delivery date for any of these.
