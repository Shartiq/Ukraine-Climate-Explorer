using System;
using System.Collections.Generic;
using System.Text;
using Ukraine_Climate_Explorer.Models;

namespace Ukraine_Climate_Explorer.Analysis
{
    public class ClimateAnalyzer
    {
        public static ClimateStatistics Analyze(Dictionary<string, double> temperatures)
        {
            var validTemperatures = temperatures.Values.Where(v => v != -999).ToList();

            if (validTemperatures.Count == 0)
                throw new InvalidOperationException("No valid temperature data found.");

            double average = validTemperatures.Average();
            double minimum = validTemperatures.Min();
            double maximum = validTemperatures.Max();
            int belowFreezing = validTemperatures.Count(v => v < 0);

            return new ClimateStatistics
            {
                AverageTemperature = average,
                MinimumTemperature = minimum,
                MaximumTemperature = maximum,
                BelowFreezingDays = belowFreezing
            };
        }
    }
}
