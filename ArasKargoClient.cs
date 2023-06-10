using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using static ArasKargoAPI.ArasKargoClient;

namespace ArasKargoAPI
{
    public class ArasKargoClient
    {

        public enum Dil
        {
            [Description("Türkçe")]
            tr,
            [Description("English")]
            en
        }

        private static string _takip_kodu;
        private static Dil _dil;

        /// <summary>
        ///  Client'i hazırlar.
        /// </summary>
        /// <param name="TakipKodu">Kargo takip kodu.</param>
        public ArasKargoClient(string TakipKodu, Dil dil = Dil.tr)
        {
            _takip_kodu = TakipKodu;
            _dil = dil;
        }

        /// <summary>
        ///  Kargo bilgilerini alır.
        /// </summary>
        /// <returns>Kargo bilgileri.</returns>
        public KargoBilgileri Al()
        {

            if (_takip_kodu == null)
            {
                throw new Exception("Önce client'i hazırlamalısın!");
            }

            #region HTTP 

            var hClient = new HttpClient();
            var data = "{\r\n    \"TrackingNumber\": \"" + _takip_kodu + "\",\r\n    \"LanguageCode\": \"" + _dil + "\"\r\n}";
            var contentPost = new StringContent(data, Encoding.UTF8, "application/json");
            var response = hClient.PostAsync("https://kurumsalwebservice.araskargo.com.tr/api/getCargoTransactionByTrackingNumber", contentPost).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            #endregion

            #region Json Parsing

            ArasResponse aras;
            try
            {
                aras = JsonConvert.DeserializeObject<ArasResponse>(content);
            }
            catch (Exception e)
            {
                throw e;
            }

            // 200 değil? Hata var demektir.
            if (aras.Code != 200)
            {
                throw new Exception(aras.Message);
            }

            #endregion

            #region Haritalama

            var cikis = new KargoBilgileri
            {
                Islemler = aras.Responses
            };

            #endregion

            #region Gönderici / Alıcı / Kaynak bilgilerini alma

            // Bu iki değişken captcha görevi görüyor olmadan alamıyoruz.
            // Ne zeman expire yiyor? bende bilmiyorum aslında.
            // Expire yiyene kadar bunları kullanmaya devam.
            // Unique ID ile matchlenmiş bi kod olabilir.
            const string captcha_code = "6o6fva";
            const string uniq_id = "cf5197a9-9445-4e85-923a-9c999be7db73";

            var postData2 = "{\"TrackingNumber\":\"" + _takip_kodu + "\",\"IsWeb\":true,\"UniqueCode\":\"" + uniq_id + "\",\"SecretKey\":\"" + captcha_code + "\",\"LanguageCode\":\"" + _dil + "\"}";
            var contentPost2 = new StringContent(postData2, Encoding.UTF8, "application/json");
            var response2 = hClient.PostAsync("https://kurumsalwebservice.araskargo.com.tr/api/getShipmentByTrackingNumber", contentPost2).Result;
            var content2 = response2.Content.ReadAsStringAsync().Result;

            var c2Dynamic = JsonConvert.DeserializeObject<dynamic>(content2);
            var kBilgileriNodeFirst = Convert.ToString(c2Dynamic.Responses.First);

            var gonderiBilgileri = JsonConvert.DeserializeObject<GonderiBilgileri>(kBilgileriNodeFirst);
            cikis.Gonderi = gonderiBilgileri;

            #endregion

            return cikis;

        }


    }
}