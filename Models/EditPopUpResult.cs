using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class EditPopUpResult
    {
        public string _Jenis { get; set; }
        public string _JenisLampu { get; set; }
        public string _TinggiTiang { get; set; }
        public string _MaterialTiang { get; set; }
        public string _JenisTiang { get; set; }
        public string _PintuPenyelenggaraan { get; set; }
        public string _PanjangLenganLampu { get; set; }
        public string _AttachImagePath { get; set; }
        public bool _DeleteFlag {  get; set; }
    }
}
