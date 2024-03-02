using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons
{
    public class JwtResponse
    {
        public string token { get; set; }
        public int role { get; set; }
    }
}
