using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class Alamat
    {
        public List<negeri_> negeri {  get; set; }
        public List<poskod_> poskod { get; set; }
        public List<daerah_> daerah { get; set; }
        public List<bandar_> bandar { get; set; }
        public List<jalan_> jalan { get; set; }

    }
    public class alamatFull
    {
        public negeri_ negeri { get; set; }
        public poskod_ poskod { get; set; }
        public daerah_ daerah { get; set; }
        public bandar_ bandar { get; set; }
        public jalan_ jalan { get; set; }
    }
    public class negeri_
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        public string nama { get; set; }
    }
    public class poskod_
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        public string nama { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int negeri_id { get; set; }
    }
    public class daerah_
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        public string nama { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int poskod_id { get; set; }
    }
    public class bandar_
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        public string nama { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int daerah_id { get; set; }
    }
    public class jalan_
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int id { get; set; }
        public string nama { get; set; }
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int bandar_id { get; set; }
    }
    public class AlamatRoot
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Alamat data { get; set; }
    }
}
