using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class MainData
    {
        public List<MapPin>? map_pin { get; set; }
        public List<FlyCable>? fly_cable { get; set; }
        public List<FeederPillar>? feeder_pillar { get; set; }
    }
    public class deleteRespond
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
