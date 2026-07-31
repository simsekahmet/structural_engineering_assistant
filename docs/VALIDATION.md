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

## VC-06 — Interstory Drift

**Module:** Interstory Drift · **Code:** TBDY 2018 §4.9.1 (Denk. 4.32–4.33), Tablo 4.9

| Input | Value |
| --- | --- |
| SDS (DD-2) / SDS (DD-3) | 1.20 g / 0.48 g |
| SD1 (DD-2) / SD1 (DD-3) | 0.60 g / 0.18 g |
| κ | 1.0 |
| Tp | 0.70 s |
| Infill | Rigid (0.008·κ limit) |

**Hand calculation**

```
TA = SD1,DD-2 / SDS,DD-2 = 0.60 / 1.20 = 0.5000 s
Tp = 0.70 s ≥ TA        → λ = SD1,DD-3 / SD1,DD-2 = 0.18 / 0.60 = 0.300
Limit = 0.008 · κ       = 0.008 · 1.0            = 0.00800

Story2:  δ/h = 0.00350 → λ·δ/h = 0.300 · 0.00350 = 0.001050   (13.1 % of limit)
Story1:  δ/h = 0.00900 → λ·δ/h = 0.300 · 0.00900 = 0.002700   (33.8 % of limit)
```

**Tool output:** λ = 0.300, limit = 0.00800, λ·δ/h = 0.001050 and 0.002700, both OK.

**Result: PASS.**

---

## VC-07 — Second-Order Effects

**Module:** Second-Order Effects · **Code:** TBDY 2018 §4.9.2 (Denk. 4.34)

| Input | Value |
| --- | --- |
| Ch / R / D | 1.0 / 8 / 3 |
| Story2 | W = 4200 kN, Vi = 520 kN, Δ/h = 0.00420 |
| Story1 | W = 5400 kN, Vi = 980 kN, Δ/h = 0.00610 |

**Hand calculation**

```
Limit = 0.12 · D / (Ch · R) = 0.12 · 3 / (1.0 · 8) = 0.045

ΣWj accumulates from the top down:
Story2:  ΣWj = 4200 kN
         θ = (Δ/h)·ΣWj / Vi = 0.00420 · 4200 / 520 = 0.033923   (75.4 %)  → OK
Story1:  ΣWj = 4200 + 5400 = 9600 kN
         θ = 0.00610 · 9600 / 980            = 0.059755   (132.8 %) → NOT OK
```

**Tool output:** limit = 0.045; θ = 0.033923 (OK) and 0.059755 (NOT OK), with ΣWj = 4200 and
9600 kN respectively — confirming the cumulative weight is summed downward.

**Result: PASS.** The failing story is reported as failing, so the check is exercised in
both directions.

---

## VC-08 — Beam Shear

**Module:** Beam Shear · **Code:** TS 500 §8.1.3–8.1.5, TBDY 2018 §7.4.5

Two cases: one in the normal range, one that forces the Vmax cap to bind.

### VC-08a — normal range

| Input | Value |
| --- | --- |
| fck / fyk | 25 / 420 MPa |
| b × h, d' | 25 × 50 cm, 5 cm |
| Stirrups | 2 legs, Ø8, s = 10 cm |
| Vc contribution | On |
| Vd | 180 kN |

```
fyd   = fyk / 1.15          = 420 / 1.15            = 365.217 MPa
fctd  = 0.35·√fck / 1.5     = 0.35·5 / 1.5          = 1.16667 MPa
d     = h − d'              = 50 − 5                = 45 cm
Vc    = 0.65·fctd·b·d        = 0.65·1.16667·0.25·0.45·1000 =  85.31 kN
Vcr   = 0.80·Vc                                      =  68.25 kN
Asw/s = n·π(Ø/10)²/4 / s     = 2·π·0.64/4 / 10      = 0.100531 cm²/cm
Vw    = (Asw/s)·d·fyd·0.1    = 0.100531·45·365.217·0.1 = 165.22 kN
Vr    = Vw + Vcr                                     = 233.47 kN
Vmax  = 0.85·b·h·√fck        = 0.85·0.25·0.50·5·1000  = 531.25 kN  (not binding)
Vd = 180 ≤ 233.47 → OK
```
*(Unit note: cm² · MPa · 0.1 = kN, since 1 cm²·MPa = 100 mm² · N/mm² = 100 N = 0.1 kN.)*

**Tool output:** Vr = 233.47 kN, OK.

### VC-08b — Vmax cap binding

Same section, but 4 legs of Ø16 at s = 10 cm and Vd = 600 kN.

```
Asw/s = 4·π·(1.6)²/4 / 10 = 0.80425 cm²/cm
Vw    = 0.80425·45·365.217·0.1 = 1321.8 kN
Vw + Vcr = 1321.8 + 68.25      = 1390.0 kN
Vmax                            =  531.25 kN
Vr = min(1390.0 , 531.25)       =  531.25 kN   ← cap governs
Vd = 600 > 531.25 → NOT OK
```

**Tool output:** Vr = 531.25 kN (capped), NOT OK.

**Result: PASS** for both cases — the reinforcement path and the Vmax cap are both confirmed.

---

## VC-09 — Beam Axial Load

**Module:** Beam Axial Load · **Code:** TBDY 2018 §7.3

| Input | Value |
| --- | --- |
| fck | 25 MPa |
| b × d | 25 × 50 cm |
| Nd | 210 kN |
| Limit ratio | 0.10 |

```
Ac      = b · d          = 25 · 50        = 1250 cm²
Ac·fck  = Ac · fck / 10   = 1250 · 25 / 10 = 3125 kN
Oran    = Nd / (Ac·fck)  = 210 / 3125     = 0.0672
0.0672 ≤ 0.10 → OK (no column-style detailing required)
```

**Tool output:** Ac = 1250 cm², capacity = 3125 kN, ratio = 0.0672, OK.

**Result: PASS.**

---

## Observed behaviour worth knowing

At **exactly** the limit the two drift-type checks disagree: Interstory Drift uses a strict
comparison (`λ·δ/h < limit`) and reports NOT OK, while Second-Order Effects uses an inclusive
one (`θ ≤ limit`) and reports OK. Verified with λ·δ/h = limit = 0.008 and θ = limit = 0.045.
This mirrors the desktop application and has been left as-is; it only matters for values
landing exactly on the limit.

---

## Not yet validated

- Wall Shear's short-wall, 0.5V and rigid-basement rules (the core Vr/Vmax path is VC-05)
- The **data-extraction** side of every module — which ETABS table or API call a demand
  value is read from, and how combinations are matched. The cases above validate the
  check arithmetic given stated inputs; VC-02 to VC-05 additionally reproduce values
  taken from real models or reference reports.

Treat anything in this list as you would any unvalidated tool: check it before use.
