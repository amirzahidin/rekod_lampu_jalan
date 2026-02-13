using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Utils
{
    public class ConstantValue
    {
        public const string ApiUrl = "https://vertbyte.com/rekod_lampu_jalan/";
        public const string ImageUrl = "https://vertbyte.com/rekod_lampu_jalan/uploads/";
        public const string GoogleApiKey = "AIzaSyCMnvgz6mvIdF8-_gzdJJeag1DmE8IEaBs";
        public const string mapPinTag = "streetLamp_";
        public const string feederPillarTag = "feederPillar_";
        public static readonly List<string> feederPillarAttachmentList = new List<string> { "image1", "image2", "image3", "image4" };
        public static readonly List<string> mapPinAttachmentList = new List<string> {"jenis_item","jenis_lampu","tinggi_tiang",
        "material_tiang","jenis_pintu_penyelenggaraan","saiz_pintu_penyelenggaraan","panjang_lengan_lampu","jenis_tiang","jenama_lampu","jenama_tiang" };
    }
}
