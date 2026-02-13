using RekodLampuJalan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class DropDownItem
    {
        public List<LookupItemString>? Jenis_Item { get; set; }
        public List<LookupItemString>? Jenis_Lampu { get; set; }
        public List<LookupItemDouble>? Tinggi_Tiang { get; set; }
        public List<LookupItemString>? Material_Tiang { get; set; }
        public List<LookupItemString>? Jenis_Pintu_Penyelenggaraan { get; set; }
        public List<LookupItemString>? Saiz_Pintu_Penyelenggaraan { get; set; }
        public List<LookupItemDouble>? Panjang_Lengan_Lampu { get; set; }
        public List<LookupItemString>? Jenis_Tiang { get; set; }
        public List<LookupItemString>? Jenama_Lampu { get; set; }
        public List<LookupItemString>? Jenama_Tiang { get; set; }
    }
    public class DropDownSelectedItem
    {
        public LookupItemString Jenis_Item { get; set; }
        public LookupItemString Jenis_Lampu { get; set; }
        public LookupItemDouble Tinggi_Tiang { get; set; }
        public LookupItemString Material_Tiang { get; set; }
        public LookupItemString Jenis_Pintu_Penyelenggaraan { get; set; }
        public LookupItemString Saiz_Pintu_Penyelenggaraan { get; set; }
        public LookupItemDouble Panjang_Lengan_Lampu { get; set; }
        public LookupItemString Jenis_Tiang { get; set; }
        public LookupItemString Jenama_Lampu { get; set; }
        public LookupItemString Jenama_Tiang { get; set; }
    }
    public class DropDownStringItem
    {
        public string Jenis_Item { get; set; }
        public string Jenis_Lampu { get; set; }
        public double Tinggi_Tiang { get; set; }
        public string Material_Tiang { get; set; }
        public string Jenis_Pintu_Penyelenggaraan { get; set; }
        public string Saiz_Pintu_Penyelenggaraan { get; set; }
        public double Panjang_Lengan_Lampu { get; set; }
        public string Jenis_Tiang { get; set; }
        public string Jenama_Lampu { get; set; }
        public string Jenama_Tiang { get; set; }
    }
    public class LookupItemString
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int Id { get; set; }
        public string Item { get; set; }
    }
    public class LookupItemDouble
    {
        [JsonConverter(typeof(IntFromStringJsonConverter))]
        public int Id { get; set; }
        [JsonConverter(typeof(DoubleFromStringJsonConverter))]
        public double? Item { get; set; }
    }
    public class newItem
    {
        public string table { get; set; }
        public string item { get; set; }
    }

}
