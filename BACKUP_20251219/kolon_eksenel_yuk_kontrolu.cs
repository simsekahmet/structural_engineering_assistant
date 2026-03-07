using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace EtabsTools
{
    // --- Veri Yapıları ---
    public class ColumnForceData
    {
        public string Story { get; set; }
        public string Column { get; set; }
        public string UniqueName { get; set; } // Eşleştirme Anahtarı
        public string LoadCase { get; set; }
        public string Location { get; set; }
        public double P { get; set; }
    }

    public class FrameAssignmentData
    {
        public string UniqueName { get; set; } // Eşleştirme Anahtarı
        public string Story { get; set; }
        public string Label { get; set; }
        public string SectionName { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Area { get; set; }
    }

    public class KolonEksenelYukResult
    {
        public string Story { get; set; }
        public string Column { get; set; }
        public string UniqueName { get; set; } // Excel'deki gibi Unique Name sütunu
        public string LoadCase { get; set; }
        public string Location { get; set; }
        public double Nd { get; set; }         // P (kN)
        public string Section { get; set; }
        public double B { get; set; }
        public double D { get; set; }
        public double Ac { get; set; }
        public double AcFck { get; set; }
        public double Limit { get; set; }
        public double Fck { get; set; }
        public double NdRatio { get; set; }
        public bool IsOK { get; set; }
        public string Status { get; set; }
    }

    // --- MANAGER SINIFI ---
    public class KolonEksenelYukManager
    {
        public double Fck { get; set; }
        public double Limit { get; set; }
        public bool IsBodrum { get; set; }
        public int BodrumKatCount { get; set; }

        public KolonEksenelYukManager(double fck, double limit, bool isBodrum, int bodrumCount)
        {
            Fck = fck;
            Limit = limit;
            IsBodrum = isBodrum;
            BodrumKatCount = bodrumCount;
        }

        public List<KolonEksenelYukResult> Calculate(
        List<ColumnForceData> allForces,
        List<FrameAssignmentData> assignments,
        List<StoryData> stories)
    {
        var results = new List<KolonEksenelYukResult>();

        // 0. Bodrum Katlarını Belirle (Elevation Sıralaması)
        // Eğer stories null ise boş liste kabul et
        if (stories == null) stories = new List<StoryData>();

        var sortedStories = stories.OrderBy(s => s.Elevation).ToList();
        var bodrumStories = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        if (IsBodrum && BodrumKatCount > 0)
        {
            for (int i = 0; i < sortedStories.Count; i++)
            {
                if (i < BodrumKatCount)
                {
                    bodrumStories.Add(sortedStories[i].Name);
                }
            }
        }

        // 1. Frame Assignment Verilerini Story + Label'a göre sözlüğe al
        // Unique Name boş veya bozuk olduğu için Story + Label en güvenilir anahtardır.
        var assignmentsDict = assignments
            .GroupBy(a => new { a.Story, a.Label }) // Story ve Label'a göre grupla
            .ToDictionary(g => g.Key, g => g.First()); // İlkini al

        // 2. ANA DÖNGÜ: Filtrelenmiş Kuvvet Listesi (Element Forces)
        foreach (var force in allForces)
        {
            // A. Anahtar oluştur (Force tablosundaki Story ve Column)
            var key = new { Story = force.Story, Label = force.Column };

            // Bu kuvvete ait kolon özelliklerini bul
            if (!assignmentsDict.TryGetValue(key, out FrameAssignmentData frame))
            {
                // Eşleşme yoksa atla
                continue; 
            }

            // B. Kesit Boyutlarını Parse Et (C40X90 -> 40, 90)
            double b_cm = 0;
            double d_cm = 0;

            // Öncelik Frame Assignment'dan alınan kesit isminde
            string sectionName = frame.SectionName ?? ""; 
            
            var match = Regex.Match(sectionName, @"(\d+(?:[.,]\d+)?)\s*[xX*]\s*(\d+(?:[.,]\d+)?)");
            if (match.Success)
            {
                string val1 = match.Groups[1].Value.Replace(",", ".");
                string val2 = match.Groups[2].Value.Replace(",", ".");
                b_cm = double.Parse(val1, CultureInfo.InvariantCulture);
                d_cm = double.Parse(val2, CultureInfo.InvariantCulture);
            }
            else
            {
                // Regex bulamazsa Frame tablosundaki nümerik width/height değerlerini kullan (mm -> cm)
                b_cm = frame.Width / 10.0;
                d_cm = frame.Height / 10.0;
            }

            double Ac_cm2 = b_cm * d_cm;
            double Ac_mm2 = Ac_cm2 * 100; // mm²

            // C. AH / UH Kombinasyon Filtresi
            bool isCurrentStoryBodrum = bodrumStories.Contains(force.Story);
            string loadCase = force.LoadCase.ToUpper();
            bool hideRow = false;

            // KURAL: 
            // 1. Bodrum Kabulü VARSA (IsBodrum = true):
            //    - Bodrum Katları: "U" içerenleri gizle (A ve Nötr görünsün).
            //    - Üst Katlar: "A" içerenleri gizle (U ve Nötr görünsün).
            // 2. Bodrum Kabulü YOKSA (IsBodrum = false):
            //    - HİÇBİR ŞEY GİZLEME (Hangi kombinasyon seçilirse seçilsin hepsi görünsün).

            bool hasAH = loadCase.Contains("A");
            bool hasUH = loadCase.Contains("U");

            if (IsBodrum)
            {
                if (isCurrentStoryBodrum)
                {
                    // Bodrumdayız -> UH İSTEMİYORUZ
                    if (hasUH) hideRow = true;
                }
                else
                {
                    // Üst kattayız -> AH İSTEMİYORUZ
                    if (hasAH) hideRow = true;
                }
            }
            else
            {
                // Bodrum yok -> A (Alt) içerenleri GİZLE, diğerlerini (U/Nötr) göster.
                if (hasAH) hideRow = true;
            }

            if (hideRow) continue;

            // D. Hesaplama
            // P değeri Element Forces tablosundan gelir (force.P)
            double Nd = force.P; // Orijinal işaretli değer (Excel uyumu için)
            double AbsNd = Math.Abs(Nd); // Hesap için mutlak değer

            // Ac * fck (N -> kN)
            // Ac_mm2 * Fck(N/mm2) = N
            // / 1000 => kN
            double AcFck_kN = (Ac_mm2 * Fck) / 1000.0;

            double ratio = 0;
            if (AcFck_kN > 0) ratio = AbsNd / AcFck_kN;

            // E. Sonuç Satırı
            string status = (ratio > Limit) ? "Limiti Aşıyor" : "Uygun";

            // Sonuç listesine ekle
            results.Add(new KolonEksenelYukResult
            {
                Story = force.Story,       // Force tablosundaki Story
                Column = force.Column,     // Force tablosundaki Column Adı
                UniqueName = force.UniqueName,
                LoadCase = force.LoadCase, // Force tablosundaki Kombinasyon
                Location = force.Location, // Force tablosundaki Station
                Nd = AbsNd,                // İSTEK: Mutlak değer yazdır
                Section = sectionName,     // Frame Assignment'dan gelen kesit
                B = b_cm,
                D = d_cm,
                Ac = Ac_cm2,
                AcFck = AcFck_kN,
                Limit = Limit,
                Fck = Fck,
                NdRatio = ratio,
                IsOK = ratio <= Limit,
                Status = status
            });
        }

        // Listeyi Sırala (Kat, Kolon Adı ve LoadCase'e göre)
        return results
            .OrderByDescending(r => r.Story)
            .ThenBy(r => r.Column)
            .ThenBy(r => r.LoadCase)
            .ToList();
    }    }
}