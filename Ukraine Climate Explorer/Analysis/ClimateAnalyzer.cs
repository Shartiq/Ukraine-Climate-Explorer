using System.Globalization;
using Ukraine_Climate_Explorer.Models;

namespace Ukraine_Climate_Explorer.Analysis
{
    public class ClimateAnalyzer
    {
        public static ClimateStatistics Analyze(Dictionary<string, double> temperatures)
        {
            var validTemperatures = temperatures.Where(t => t.Value != -999).ToList();

            if (validTemperatures.Count == 0)
                throw new InvalidOperationException("No valid temperature data found.");

            double average = validTemperatures.Average(t => t.Value);

            var minimum = validTemperatures.MinBy(t => t.Value);
            var maximum = validTemperatures.MaxBy(t => t.Value);

            int belowFreezing = validTemperatures.Count(t => t.Value < 0);

            return new ClimateStatistics
            {
                AverageTemperature = average,

                MinimumTemperature = minimum.Value,
                MinimumTemperatureDate = minimum.Key,

                MaximumTemperature = maximum.Value,
                MaximumTemperatureDate = maximum.Key,

                BelowFreezingDays = belowFreezing
            };
        }

        public static Dictionary<DateTime, double> CalculateMonthlyAverages(
            Dictionary<string, double> temperatures)
        {
            Dictionary<DateTime, double> dailyData = [];

            foreach (var item in temperatures.Where(t => t.Value != -999))
            {
                DateTime parsedDate = DateTime.ParseExact(
                    item.Key, "yyyyMMdd", CultureInfo.InvariantCulture
                );
                dailyData.Add(parsedDate, item.Value);
            }

            return dailyData
                .GroupBy(t => new DateTime(t.Key.Year, t.Key.Month, 1))
                .ToDictionary(
                    group => group.Key,
                    group => group.Average(t => t.Value));
        }
    }
}
