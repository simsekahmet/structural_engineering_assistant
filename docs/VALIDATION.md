# Validation cases

Each case below is a calculation this tool produced, re-derived independently so the
numbers can be audited. Reproduce any of them by entering the inputs and opening the
module's **Calculation basis** panel, which prints the same intermediate values.

This file records only checks that were actually carried out. Modules without an entry
have **not** been formally validated yet — that gap is listed in `STATUS.md`.

---

## VC-01 — Design Spectrum, peak SaR

**Module:** Design Spectrum · **Code:** TBDY 2018 §2.3.4, §4.3.4

| Input | Value |
| --- | --- |
| SDS | 1.0 g |
| SD1 | 0.5 g |
| R | 8 |
| I | 1 |
| D | 3 |

**Hand calculation**

```
TA = 0.2 · SD1/SDS = 0.2 · 0.5/1.0        = 0.100 s
TB =       SD1/SDS =       0.5/1.0        = 0.500 s

The reduced spectrum peaks at T = TA (Sae is already at its plateau value while
Ra is still near its minimum).

At T = TA = 0.100 s:
  Sae = SDS · (0.4 + 0.6·T/TA) = 1.0 · (0.4 + 0.6·0.100/0.100) = 1.000 g
  Ra  = D + (R/I − D)·T/TB     = 3 + (8/1 − 3)·0.100/0.500     = 4.000
  SaR = g · Sae/Ra             = 9.81 · 1.000/4.000            = 2.4525 m/s²
```

**Tool output:** Peak SaR = **2.453 m/s²** at T = 0.100 s, Sae = 1.0000 g, Ra = 4.0000.

**Cross-check:** an independent sweep of T from 0 to 8 s in 0.001 s steps, evaluating
the piecewise Sae/Ra definitions directly, returns a maximum of 2.452 m/s² at
T = 0.100 s — the 0.001 difference is the sweep's step granularity, not a discrepancy.

**Result: PASS.**

---

## VC-02 — Base Shear Amplification (β), Tmax cap

**Module:** Base Shear Amplification · **Code:** TBDY 2018 §4.7.3, Eq. 4.19

Verified during migration against the desktop `artirim_hesabi.cs` on a live model:
the three raw ETABS reads (mass from "Mass Summary by Story" excluding base and
basement stories, period from "Modal Participating Mass Ratios" taking the top two
modes by UX/UY, base shear from "Story Forces" at Location = Bottom read bottom-up)
and the Tmax-capped β formula were recomputed independently and matched bit-for-bit.

**Result: PASS.** A written-out worked example is still to be added here.

---

## VC-03 — Column Axial Load

**Module:** Column Axial Load · **Code:** TBDY 2018 §7.3.1, Eq. 7.3

Verified during migration on a live model: 54 real columns were read, of which 2
(C20 and C22 at Story1) failed the Nd/(Ac·fck) ≤ 0.40 limit. The governing column's
Ac, Ac·fck and ratio were reproduced by hand and matched the tool.

**Result: PASS.** A written-out worked example is still to be added here.

---

## VC-04 — Wall Axial Load

**Module:** Wall Axial Load · **Code:** TBDY 2018 §7.6 (wall limit 0.35)

Reproduces a row from the reference desktop report:

| Input | Value |
| --- | --- |
| Story / Pier / Combo | Story11 / P1 / ENVE_EQU |
| fck | 40 MPa |
| bw (thickness) | 63 cm |
| lw (wall length, end to end) | 545 cm |
| Nd (governing \|P\|) | 1312 kN |

**Hand calculation**

```
Ac      = bw · lw        = 63 · 545        = 34 335 cm²
Ac·fck  = Ac · fck · 0.1 = 34 335 · 40 · 0.1 = 137 340 kN
Oran    = Nd / (Ac·fck)  = 1312 / 137 340   = 0.0096
Durum   = 0.0096 ≤ 0.35  → OK
```

**Tool output:** Ac = 34 335 cm², ratio = 0.0096 → displayed as **0.01**, **OK**.

**Note on Ac:** the reference report prints Ac = 34 200 cm² because its b and d columns
are rounded for display (34 200 / 545 = 62.75 cm, not exactly 63). Using the rounded
inputs shown gives 0.00955 and using the reference Ac gives 0.00959 — both round to the
same displayed 0.01, so the difference is presentation, not method.

**Result: PASS.**

---

## VC-05 — Wall Shear

**Module:** Wall Shear · **Code:** TBDY 2018 §7.6, TS 500 §8.3

Reproduces five rows of the reference desktop report (fck = 35 MPa, fyd = 365 MPa):

```
fctd = 0.35·√fck / 1.5
Vmax = 0.085·bw·lw·√fck            (0.065 for coupled walls)
Vc   = 0.065·bw·lw·fctd
Vw   = (n·π(φ/10)²/4 / s)·lw·fyd·0.1
Vr   = Vc + Vw
```

| Row | bw × lw | Rebar | Vd | Vmax | Vr | Vd/Vr | Vd/Vmax |
| --- | --- | --- | --- | --- | --- | --- | --- |
| CATI / P1 | 114.7 × 675 | 2×φ18/15 | 1815 | 38 922 | 15 304 | 0.12 | 0.05 |
| 5K / P1 | 105.9 × 765 | 2×φ18/15 | 2075 | 40 732 | 16 742 | 0.12 | 0.05 |
| 3B / P1 | 105.9 × 765 | 2×φ18/15 | 8924 | 40 732 | 16 742 | 0.53 | 0.22 |
| 2B / P9 | 40 × 195 | 2×φ12/15 | 953 | 3 922 | 1 773 | 0.54 | 0.24 |
| 3B / P9 | 40 × 195 | 2×φ12/15 | 1365 | 3 922 | 1 773 | 0.77 | 0.35 |

Every Vmax, Vr and both capacity ratios match the reference report to the printed
precision. Note that Vd here is the **governing** value, `max(Vd, 0.5V)` — the
reference report's own "Vd" column shows the raw value while its capacity columns
use the governing one, which is why e.g. CATI/P1 shows 881 kN raw but 0.12 = 1815/15304.

**Result: PASS.**

---

## Not yet validated

- Interstory Drift (λ and the 0.008κ / 0.016κ limits)
- Second-Order Effects (θ and the 0.12·D/(Ch·R) limit)
- Beam Shear (Vr, and the Vmax cap)
- Beam Axial Load
- Wall Shear's short-wall, 0.5V and rigid-basement rules (the core Vr/Vmax path is VC-05)

These were ported from the desktop implementation and spot-checked while migrating,
but no documented hand calculation exists for them yet. Treat their output as you
would any unvalidated tool: check it before use.
