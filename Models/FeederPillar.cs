using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class FeederPillar
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        public string no_meter { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double nilai_amp { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double nilai_phase_r { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double nilai_phase_y { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double nilai_phase_b { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double lattitude { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double longitude { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int alamat { get; set; }
        public alamatFull alamatFull { get; set; }
        public string status_pintu { get; set; } = "";
        public string footing { get; set; } = "";
        public string komen { get; set; } = "";
        public string attachment { get; set; }
        [JsonConverter(typeof(DateTimeFromStringJsonConverter))]
        public DateTime created_at { get; set; }
        [JsonConverter(typeof(DateTimeFromStringJsonConverter))]
        public DateTime updated_at { get; set; }
        public List<AttachmentImage> attachmentFile { get; set; } = new();
        public AlamatInsert alamatInsert { get; set; }
    }
}
