using System;
using System.Collections.Generic;

namespace EtabsTools
{
    /// <summary>
    /// TBDY 2018 - Deprem Artırım Katsayısı Hesabı
    /// </summary>
    public class ArtirimHesabiManager
    {
        /// <summary>
        /// Artırım katsayısı (β) hesapla
        /// </summary>
        /// <param name="mt">Yapı toplam kütlesi (ton)</param>
        /// <param name="sae">Spektral ivme (g cinsinden)</param>
        /// <param name="g">Yerçekimi ivmesi (m/s²)</param>
        /// <param name="vtAnaliz">Modal analizden gelen Vt (kN)</param>
        /// <returns>Hesap sonuçları</returns>
        public ArtirimHesabiResult Calculate(double mt, double sae, double g, double vtAnaliz)
        {
            // mt: ton → kg (1000 ile çarp)
            double mtKg = mt * 1000;

            // Vt eşdeğer deprem yükü (kN)
            double vtEdy = (mtKg * sae * g) / 1000;

            // Ölçek katsayısı β
            double beta = vtEdy / vtAnaliz;

            // Durum kontrolü
            string durum = "";
            bool isOK = true;

            if (beta > 1.4)
            {
                durum = $"β = {beta:0.000} ";
                isOK = false;
            }
            else if (beta < 0.9)
            {
                durum = $"β = {beta:0.000} ";
                isOK = false;
            }
            else
            {
                durum = $"β = {beta:0.000} ";
                isOK = true;
            }

            return new ArtirimHesabiResult
            {
                Mt = mt,
                Sae = sae,
                G = g,
                VtAnaliz = vtAnaliz,
                VtEdy = vtEdy,
                Beta = beta,
                Durum = durum,
                IsOK = isOK
            };
        }
    }

    /// <summary>
    /// Artırım hesabı sonuç sınıfı
    /// </summary>
    public class ArtirimHesabiResult
    {
        public double Mt { get; set; }          // Yapı toplam kütlesi (ton)
        public double Sae { get; set; }         // Spektral ivme (g)
        public double G { get; set; }           // Yerçekimi ivmesi (m/s²)
        public double VtAnaliz { get; set; }    // Modal analizden gelen Vt (kN)
        public double VtEdy { get; set; }       // Eşdeğer deprem yükü Vt (kN)
        public double Beta { get; set; }        // Ölçek katsayısı β
        public string Durum { get; set; }       // Durum mesajı
        public bool IsOK { get; set; }          // Kontrol sonucu
    }
}
