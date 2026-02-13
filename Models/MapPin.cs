using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class MapPin
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int jenis_item { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int jenis_lampu { get; set; } = -1;
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double lattitude { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double longitude { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int tinggi_tiang { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int material_tiang { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int jenis_pintu_penyelenggaraan { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int saiz_pintu_penyelenggaraan { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int panjang_lengan_lampu { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int jenis_tiang { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int jenama_lampu { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int jenama_tiang { get; set; } = -1;
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int status { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int alamat { get; set; }
        public string komen { get; set; } = "";
        public alamatFull alamatFull { get; set; }
        public string attachment { get; set; }
        [JsonConverter(typeof(DateTimeFromStringJsonConverter))]
        public DateTime created_at { get; set; }
        [JsonConverter(typeof(DateTimeFromStringJsonConverter))]
        public DateTime updated_at { get; set; }

        public List<AttachmentImage> attachmentFile { get; set; } = new();
        public AlamatInsert alamatInsert { get; set; }

    }
    public class AttachmentImage
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string imageType { get; set; }
        public bool newItem {  get; set; }
    }
    public class MapPinPopUpData
    {
        public MapPin mapPin { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
    public class FeederPillarPopUpData
    {
        public FeederPillar feederPillar { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }
    }
}
