using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasKargoAPI
{

    internal class ArasResponse
    {
        [JsonProperty("Code")]
        public int Code { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Responses")]
        public List<KargoIslem> Responses { get; set; }

        [JsonProperty("ContactInfo")]
        public string ContactInfo { get; set; }
    }


    public class KargoIslem
    {
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("TransactionDate")]
        public DateTime? Tarih { get; set; }

        [JsonProperty("UnitName")]
        public string Bolge { get; set; }

        [JsonProperty("ShipmentLineTransType")]
        public string Tur { get; set; }

        [JsonProperty("Description")]
        public string Aciklama { get; set; }
    }

    public class GonderiBilgileri
    {
        [JsonProperty("TrackingNumber")]
        public string TakipNo { get; set; }

        [JsonProperty("DocumentSerial")]
        public string DokumenSeriNo { get; set; }

        [JsonProperty("DocumentNumber")]
        public string DokumanNumarasi { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("WaybillDate")]
        public DateTime GonderiTarihi { get; set; }

        [JsonProperty("SenderAccountAddressName")]
        public string GondericiIsim { get; set; }

        [JsonProperty("ReceiverAccountAddressName")]
        public string AliciIsim { get; set; }

        [JsonProperty("TotalVolume")]
        public string Desi { get; set; }

        [JsonProperty("PieceCount")]
        public string Adet { get; set; }

        [JsonProperty("LovShipmentStatusId")]
        public string LovShipmentStatusId { get; set; }

        [JsonProperty("LovShipmentStatusName")]
        public string SonDurum { get; set; }

        [JsonProperty("SourceUnitName")]
        public string CikisSubesi { get; set; }

        [JsonProperty("DeliveryUnitName")]
        public string VarisSubesi { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("PlannedDeliveryDate")]
        public DateTime TahminiTeslimTarihi { get; set; }

        [JsonProperty("LovPayorTypeName")]
        public string OdemeTuru { get; set; }

        [JsonProperty("LovPackTypeName")]
        public string PaketTuru { get; set; }

        [JsonProperty("SourceCity")]
        public string CikisIl { get; set; }

        [JsonProperty("DeliveryCity")]
        public string VarisIl { get; set; }

        [JsonProperty("SourceTown")]
        public string CikisIlce { get; set; }

        [JsonProperty("DeliveryTown")]
        public string VarisIlce { get; set; }

        [JsonProperty("LovShipmentTypeName")]
        public string GonderimTuru { get; set; }
    }

    public class KargoBilgileri
    {

        /// <summary>
        ///  Kargo işlemleri (işlem tarihleri, gönderi durumları vb.)
        /// </summary>
        public List<KargoIslem> Islemler { get; set; }

        /// <summary>
        ///  Kargo bilgileri (gönderici, alıcı, kargo türü vb.)
        /// </summary>
        public GonderiBilgileri Gonderi { get; set; }

    }

}
