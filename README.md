# Structural Engineering Assistant

ETABS modelleri için web tabanlı tahkik, veri inceleme ve raporlama arayüzü.

## Web önizlemesi

Arayüz bağımlılıksız statik HTML/CSS/JavaScript olarak `web/` altında bulunur. Yerelde çalıştırmak için:

```powershell
cd web
python -m http.server 4173
```

Ardından `http://localhost:4173` adresini açın.

`main` dalına gönderilen `web/` değişiklikleri GitHub Actions ile GitHub Pages'a dağıtılır.

## ETABS bağlantı mimarisi

Bir web tarayıcısı ETABS COM API nesnelerine doğrudan erişemez. Planlanan yapı üç parçadan oluşur:

1. GitHub Pages üzerinde çalışan web arayüzü.
2. Kullanıcının Windows bilgisayarında çalışan, yalnızca `localhost` üzerinden erişilen .NET yerel köprüsü.
3. Yerel köprünün ETABS 22 `CSiAPIv1` / `ETABSv1` COM API bağlantısı.

Web arayüzü şu anda `https://localhost:5218/api/health` uç noktasını kontrol eder. Köprü sonraki geliştirme adımında; izinli komut listesi, CORS kısıtı, istek doğrulama ve model kilidi kontrolleriyle eklenecektir.

## Mevcut masaüstü kodu

Depo kökündeki WinForms/.NET Framework 4.8 uygulaması geçiş sürecinde referans uygulama olarak korunmaktadır. Modüller web arayüzüne taşındıkça hesap mantığı kullanıcı arayüzünden ayrılarak test edilebilir servis katmanına alınacaktır.
