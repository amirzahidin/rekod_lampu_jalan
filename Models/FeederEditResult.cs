using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class FeederEditResult
    {
        public string _NoMeter { get; set; }
        public string _NilaiAmp { get; set; }
        public string _NilaiPhaseR { get; set; }
        public string _NilaiPhaseY { get; set; }
        public string _NilaiPhaseB { get; set; }
        public string _StatusPintu { get; set; }
        public string _Komen { get; set; }
        public bool _DeleteFlag { get; set; }
        
    }
}
