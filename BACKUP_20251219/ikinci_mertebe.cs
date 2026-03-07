using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtabsTools
{
    public class IkinciMertebeManager
    {
        public List<IkinciMertebeResult> CalculateDirection(
            List<StoryData> sortedStories, 
            List<ForceData> forces, 
            List<DriftData> drifts, 
            List<MassData> massDataList,
            string direction, 
            double Ch, 
            double R, 
            double D)
        {
            var results = new List<IkinciMertebeResult>();
            
            // Kombinasyonları belirle
            var uniqueCombos = forces.Select(f => f.LoadCase).Distinct().ToList();

            foreach (var combo in uniqueCombos)
            {
                // Kümülatif ağırlık hesabı için
                double cumWeight = 0;

                // En üst kattan aşağıya doğru iniyoruz (sortedStories: Top -> Base olmalı)
                foreach (var story in sortedStories)
                {
                    // 1. Kütle Bul
                    var massData = massDataList.FirstOrDefault(m => m.Story == story.Name);
                    double weight = massData?.Weight ?? 0;
                    
                    cumWeight += weight;
                    
                    // 2. Kuvvet Bul (LoadCase partial match)
                    var forceData = forces.FirstOrDefault(f => f.Story == story.Name && 
                        (f.LoadCase == combo || f.LoadCase.IndexOf(combo, StringComparison.OrdinalIgnoreCase) >= 0 || combo.IndexOf(f.LoadCase, StringComparison.OrdinalIgnoreCase) >= 0));
                    double V = direction == "X" ? forceData?.VX ?? 0 : forceData?.VY ?? 0;

                    // 3. Drift Bul (LoadCase partial match)
                    var driftData = drifts.FirstOrDefault(d => d.Story == story.Name && 
                        (d.LoadCase == combo || d.LoadCase.IndexOf(combo, StringComparison.OrdinalIgnoreCase) >= 0 || combo.IndexOf(d.LoadCase, StringComparison.OrdinalIgnoreCase) >= 0));
                    double delta = driftData?.Drift ?? 0; // Avg Drift (Ratio)

                    // V==0 olan katları da göster ama hesap yapamazlarsa Theta=0 olur
                    // Sadece forceData veya driftData bulunamazsa atla
                    if (forceData == null && driftData == null) continue;

                    // Formül: Theta = (AvgDrift_Ratio * Wij) / Vi
                    // ETABS AvgDrift genellikle Ratio'dur. (mm/mm)
                    double theta = V != 0 ? (delta * cumWeight) / V : 0;
                    double limit = 0.12 * D / (Ch * R);
                    
                    // NOT: Bodrum/Üst kat ayrımı Form1.cs'de ALT/UST kombinasyon filtrelemesi ile yapılıyor.
                    // Bu nedenle burada IsBodrum kontrolü yapmıyoruz. 

                    results.Add(new IkinciMertebeResult
                    {
                        Story = story.Name,
                        LoadCase = combo,
                        Direction = direction,
                        Vi = V,
                        Wij = cumWeight,
                        DriftRatio = delta,
                        Theta = theta,
                        Limit = limit,
                        Status = theta <= limit ? "OK" : "NOT OK"
                    });
                }
            }
            return results;
        }
    }

    // --- Data Models ---

    public class StoryData
    {
        public string Name { get; set; }
        public double Height { get; set; } // m
        public double Elevation { get; set; } // m
        public bool IsBodrum { get; set; }
    }

    public class MassData
    {
        public string Story { get; set; }
        public double Mass { get; set; } // Mass
        public double Weight { get; set; } // kN
    }

    public class ForceData
    {
        public string Story { get; set; }
        public string LoadCase { get; set; }
        public double VX { get; set; }
        public double VY { get; set; }
    }

    public class DriftData
    {
        public string Story { get; set; }
        public string LoadCase { get; set; }
        public string Direction { get; set; }
        public double Drift { get; set; }
    }

    public class IkinciMertebeResult
    {
        public string Story { get; set; }
        public string LoadCase { get; set; }
        public string Direction { get; set; }
        public double Vi { get; set; }
        public double Wij { get; set; }
        public double DriftRatio { get; set; }
        public double Theta { get; set; }
        public double Limit { get; set; }
        public string Status { get; set; }
    }
}
