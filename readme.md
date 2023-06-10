# ArasKargoAPI

Aras kargo & gönderim bilgilerini çekmek için en kapsamlı kütüphane.

## Nasıl kullanılır?

DLL kütüphanesini projemize ekliyoruz. (.Net standard olduğu için mobil, web gibi projelerinize de ekleyebilirsiniz.)

```csharp
using ArasKargoAPI;
...

ArasKargoClient client = new ArasKargoClient(GonderiNumarasi); // gonderi no
KargoBilgileri Bilgiler = client.Al();

// Not: Ingilizce desteklenmektedir.

```

İle bilgileri aldıktan sonra
* `Bilgiler.Gonderi` ile teslimat şubesi, tahmini variş, gönderici/alıcı bilgilerini,
* `Bilgiler.Islemler` ile transfer bilgilerini (X tarihinde şubeye ulaştı, dağıtıma çıkarıldı vs.)

ulaşabilirsiniz.

# Örnekler

### Gönderi bilgilerini çekme

```csharp
var gonderici = Bilgiler.Gonderi.GondericiIsim;
var alici = Bilgiler.Gonderi.AliciIsim;
var cikisSubesi = Bilgiler.Gonderi.CikisSubesi;
var varisSubesi = Bilgiler.Gonderi.VarisSubesi;
var tahminiTeslim = Bilgiler.Gonderi.TahminiTeslimTarihi;
var gonderimTarihi = Bilgiler.Gonderi.GonderiTarihi;

// falan filan...
```

### Kargo transfer bilgilerini yazdırma:

```csharp
Bilgiler.Islemler.ForEach(x =>
{
    Console.WriteLine(x.Tarih + " " + x.Bolge + " " + x.Tur + " " + x.Aciklama);
});
```

Verdiği;
```markdown
9/6/2023 5:21:50 PM ORHANLI TRANSFER (YOLDA; Kargonuz çıkış transfer merkezinden varış transfer merkezine gönderiliyor.)
9/6/2023 5:25:00 PM ORHANLI TRANSFER (YOLDA; Kargonuz çıkış transfer merkezinden varış transfer merkezine gönderiliyor.)
10/6/2023 4:56:51 AM HADIMKÖY TRANSFER (YOLDA; Kargonuz varış transfer merkezine indirilmiştir.)
10/6/2023 5:00:00 AM HADIMKÖY TRANSFER (YOLDA; Kargonuz çıkış transfer merkezinden varış transfer merkezine gönderiliyor.)
10/6/2023 5:10:00 AM HADIMKÖY TRANSFER (YOLDA; Kargonuz varış transfer merkezinden varış şubesine gönderiliyor.)
10/6/2023 12:08:41 PM KUZEY MARMARA (TESLİMAT ŞUBESİNDE; Kargonuz transfer merkezimizden teslimat şubemize ulaşmıştır.)
```