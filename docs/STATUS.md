# Module implementation status

Last updated for **web v1.15.0 / agent v1.14.0**.

"Migrated" means the calculation itself runs against the live ETABS model and has
been checked against an independent hand calculation. Every check module listed below
performs a real calculation; the schedule ("done") screens were removed in v1.11.0.
The Report module is not a check — its status is tracked separately underneath.

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

## Reporting

The Report module (added in v1.13.0) turns the checks into the document a design
office actually delivers. It is built step by step against the report outline, and
only what has been settled is implemented — no step is filled in by guesswork.

| Part | Status | Notes |
| --- | --- | --- |
| Report framework | ✅ Implemented | Processes, ordered steps, per-step progress, bilingual labels, template save/load (`.json`), global font size in the page heading. The outline is declarative data, so a new step is a schema entry. Field types: text, number, month, select, image, auto-text, storey roles, model measurement and reference-table selection. |
| Introduction → 1. Cover | ✅ Implemented | Shared project information (province…parcel, project and block name), auto-composed cover text / report scope / footer with a manual-override switch, optional cover image (code `KAPAK`), month and year, live cover preview. |
| Introduction → 2. Introduction | ✅ Implemented | Storey list read from the model and marked basement / ground / normal / roof; the counts and heights of the report sentence follow from those marks. Figures 1.1 and 1.2 (codes `S1-1`, `S1-2`). |
| Introduction → 3. Structural system | ✅ Implemented | Plan extent measured from the model, system class, foundation type and zone thicknesses, slab system (the paragraph switches between flat-plate and beam-and-slab) with the thickness read from the slab sections assigned in the model. Figure 2.1 (code `S2-1`). |
| Introduction → 4. Materials / soil | ✅ Implemented | Concrete and rebar classes pre-selected from the model's materials and overridable on the reference table itself; local soil class selected on TBDY Table 5.1. |
| Introduction → 5. Loads | 🚧 Partly | Load patterns read from the model feed the 6.1–6.3 boxes (one pattern per box, split by the rigid-basement rule). Plan-view groups decide which storeys share a drawing. Table 6.1 soil pressure and the static/dynamic soil load patterns are selected here. Views are captured from the ETABS window — see the note below. |
| Introduction → steps 6–11 | ⬜ Not defined yet | Listed under their outline names and marked "to be defined" in the interface. |
| Appendices B (beam) / C (column) / D (wall) | ⬜ Not defined yet | Shown as processes; no steps defined. |
| Word / PDF output | ⬜ Not started | The report is composed and previewed but cannot yet be written to a document file. |

The wording of every generated paragraph is taken verbatim from the office report
template, so a sentence produced here is the sentence the report prints; only the
variables inside it change. Values a model can supply (storey heights, plan extent,
slab thickness, concrete and rebar class) are read through the agent as *suggestions*
— the engineer's own entry always wins, because an automatic read can pick the wrong
material and they sign the report.

**View capture is deliberately semi-automatic.** The ETABS v1 API exposes no view
control and no picture export — `cView` offers only `RefreshView` and
`RefreshWindow` — so ETABS cannot be driven to a plan view or told to display shell
loads from the agent. The Loads step therefore lists every view the report needs,
the engineer sets that view up in ETABS, and the agent photographs the ETABS window
on a single click. Fully automating this would mean driving ETABS menus with
synthetic input, which breaks on any menu or version change.

Report data is held in the browser's local storage on the engineer's own machine and
is never sent anywhere. Clearing browser data clears it — use *Save template* to keep
a copy.

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
2. The remaining report steps, and a printable document output alongside the existing
   Excel exports (see *Reporting* above).

Explicitly **not** planned: the schedule ("done") modules — removed from the
application in v1.11.0; story grouping for Wall Shear; and Column Shear.

There is no committed delivery date for any of these.
