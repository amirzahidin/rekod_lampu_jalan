using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RekodLampuJalan.Models
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
    }

    public class LoginResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public User user { get; set; }
    }
}
