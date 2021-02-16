using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriangulationAPI.DTOs
{
    public class DeviceDTO
    {
        public string MACAdress { get; set; }
        public double DistanceA { get; set; }
        public double DistanceB { get; set; }
    }
}
