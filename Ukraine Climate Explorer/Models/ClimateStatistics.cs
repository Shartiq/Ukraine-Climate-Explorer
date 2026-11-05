using System;
using System.Collections.Generic;
using System.Text;

namespace Ukraine_Climate_Explorer.Models
{
    public class ClimateStatistics
    {
        public double AverageTemperature { get; set; }

        public double MinimumTemperature { get; set; }
        public string MinimumTemperatureDate { get; set; } = "";

        public double MaximumTemperature { get; set; }
        public string MaximumTemperatureDate { get; set; } = "";

        public int BelowFreezingDays { get; set; }
    }
}
