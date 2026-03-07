using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EtabsTools
{
    public class SpectrumResult
    {
        public List<double> Periods { get; set; }
        public List<double> Accelerations { get; set; }
        public string FilePath { get; set; }
    }

    public class SpectrumManager
    {
        public double SDS { get; set; }
        public double SD1 { get; set; }
        public double R { get; set; }
        public double D { get; set; }
        public double I { get; set; }

        public SpectrumManager(double sds, double sd1, double r, double d, double i)
        {
            SDS = sds; SD1 = sd1; R = r; D = d; I = i;
        }

        public SpectrumResult Calculate(string saveFolder = null)
        {
            double TA = 0.2 * SD1 / SDS, TB = SD1 / SDS;

            var TList = new List<double> { 0.0, TA / 3, TA / 2, TA };
            for (double t = TA + 0.01; t <= TB; t += 0.01) TList.Add(Math.Round(t, 3));
            TList.Add(TB);
            for (double t = TB + 0.05; t <= 8.0; t += 0.05) TList.Add(Math.Round(t, 3));
            TList = TList.Distinct().OrderBy(t => t).ToList();

            var SaRList = TList.Select(T =>
            {
                double Se = T <= TA ? SDS * (0.4 + 0.6 * T / TA) : T <= TB ? SDS : T <= 6.0 ? SD1 / T : SD1 * 6 / (T * T);
                double Reff = T <= TB ? D + ((R / I) - D) * (T / TB) : R / I;
                return 9.81 * (Se / Reff);
            }).ToList();

            string fileName = $"R{R}_D{D}_I{I}.txt";
            string folder = saveFolder ?? Path.GetTempPath();
            string filePath = Path.Combine(folder, fileName);

            using (var sw = new StreamWriter(filePath))
                for (int i = 0; i < TList.Count; i++)
                    sw.WriteLine($"{TList[i]:0.000}\t{SaRList[i]:0.0000}");

            return new SpectrumResult { Periods = TList, Accelerations = SaRList, FilePath = filePath };
        }
    }
}