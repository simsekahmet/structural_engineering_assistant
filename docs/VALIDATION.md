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

## VC-02 — Scaling Calculation (β), Tmax cap

**Module:** Scaling Calculation · **Code:** TBDY 2018 §4.7.3, Eq. 4.19

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

## Not yet validated

- Interstory Drift (λ and the 0.008κ / 0.016κ limits)
- Second-Order Effects (θ and the 0.12·D/(Ch·R) limit)
- Beam Shear (Vr, and the Vmax cap)
- Beam Axial Load

These were ported from the desktop implementation and spot-checked while migrating,
but no documented hand calculation exists for them yet. Treat their output as you
would any unvalidated tool: check it before use.
