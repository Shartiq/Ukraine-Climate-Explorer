using System;
using System.Collections.Generic;
using System.Text;

namespace Ukraine_Climate_Explorer.Models
{
    public class City
    {
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
