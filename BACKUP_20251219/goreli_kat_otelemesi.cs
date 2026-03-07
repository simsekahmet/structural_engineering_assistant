using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EtabsTools
{
    // Story Drift veri yapısı
    public class StoryDriftData
    {
        public string Story { get; set; }
        public string OutputCase { get; set; }
        public string Direction { get; set; }
        public double Drift { get; set; }
        public double LambdaDrift { get; set; }
        public double Limit { get; set; }
        public bool IsOK { get; set; }
    }

    // Hesap sonucu yapısı
    public class GoreliKatResult
    {
        public List<StoryDriftData> Items { get; set; } = new List<StoryDriftData>();
        public double Lambda { get; set; }
        public double Limit { get; set; }
        public bool AllPassed => Items.TrueForAll(x => x.IsOK);
    }

    // Göreli Kat Ötelemesi Hesap Manager
    public class GoreliKatOtelemesiManager
    {
        // DD-2 Parametreleri
        public double SDS_DD2 { get; set; }
        public double SD1_DD2 { get; set; }

        // DD-3 Parametreleri
        public double SDS_DD3 { get; set; }
        public double SD1_DD3 { get; set; }

        // Diğer Parametreler
        public double K { get; set; } = 1.0;
        public double Tp { get; set; } = 0.5;
        public bool EsnekDerz { get; set; } = false;
        public bool BodrumKabulu { get; set; } = false;
        public int BodrumKatSayisi { get; set; } = 0;

        public GoreliKatOtelemesiManager() { }

        public GoreliKatOtelemesiManager(double sds_dd2, double sds_dd3, double sd1_dd2, double sd1_dd3,
                                          double k, double tp, bool esnekDerz, bool bodrumKabulu, int bodrumKatSayisi)
        {
            SDS_DD2 = sds_dd2; SDS_DD3 = sds_dd3;
            SD1_DD2 = sd1_dd2; SD1_DD3 = sd1_dd3;
            K = k; Tp = tp;
            EsnekDerz = esnekDerz;
            BodrumKabulu = bodrumKabulu;
            BodrumKatSayisi = bodrumKatSayisi;
        }

        // Kat numarası çıkarma (Story1 -> 1, Story10 -> 10)
        public static int? ExtractStoryNumber(string storyName)
        {
            if (string.IsNullOrEmpty(storyName)) return null;
            var match = Regex.Match(storyName, @"\d+");
            return match.Success ? int.Parse(match.Value) : (int?)null;
        }

        // Lambda hesabı (TBDY 2018)
        public double CalculateLambda()
        {
            if (SDS_DD2 == 0) return 0;
            double TA = SD1_DD2 / SDS_DD2;
            return Tp < TA ? SDS_DD3 / SDS_DD2 : SD1_DD3 / SD1_DD2;
        }

        // Drift limit hesabı
        public double CalculateLimit()
        {
            return EsnekDerz ? 0.016 * K : 0.008 * K;
        }

        // Ana hesaplama
        public GoreliKatResult Calculate(List<StoryDriftData> driftData)
        {
            double lambda = CalculateLambda();
            double limit = CalculateLimit();

            var result = new GoreliKatResult
            {
                Lambda = lambda,
                Limit = limit
            };

            foreach (var item in driftData)
            {
                item.LambdaDrift = lambda * item.Drift;
                item.Limit = limit;
                item.IsOK = item.LambdaDrift < limit;
                result.Items.Add(item);
            }

            return result;
        }

        // Eksik deprem kombinasyonu belirleme (bodrum durumuna göre)
        public string DetermineLoadCase(string direction, int? storyNumber)
        {
            string dir = direction.ToUpper();
            if (BodrumKabulu && storyNumber.HasValue && storyNumber.Value <= BodrumKatSayisi)
                return $"RS{dir}ALT";
            return $"RS{dir}UST";
        }
    }
}
