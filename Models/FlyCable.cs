using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class FlyCable
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double start_lattitude { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double start_longitude { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double end_lattitude { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double end_longitude { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double jarak { get; set; }
        [JsonConverter(typeof(DateTimeFromStringJsonConverter))]
        public DateTime created_at { get; set; }
        [JsonConverter(typeof(DateTimeFromStringJsonConverter))]
        public DateTime updated_at { get; set; }
    }
}
