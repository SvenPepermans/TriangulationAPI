using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriangulationAPI.Models
{
    public class Device
    {
        public string MACAdress { get; set; }
        public double DistanceA { get; set; }
        public double DistanceB { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Device()
        {

        }
    }
}
