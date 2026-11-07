using System.Globalization;
using Ukraine_Climate_Explorer.Models;

namespace Ukraine_Climate_Explorer.Analysis
{
    public class ClimateAnalyzer
    {
        public static ClimateStatistics Analyze(Dictionary<string, double?> temperatures)
        {
            var validTemperatures = temperatures
                .Where(t => t.Value.HasValue)
                .Select(t => (t.Key, t.Value!.Value))
                .ToList();

            if (validTemperatures.Count == 0)
                throw new InvalidOperationException("No valid temperature data found.");

            double average = validTemperatures.Average(t => t.Value);

            var (KeyMin, ValueMin) = validTemperatures.MinBy(t => t.Value);
            var (KeyMax, ValueMax) = validTemperatures.MaxBy(t => t.Value);

            int belowFreezing = validTemperatures.Count(t => t.Value < 0);

            return new ClimateStatistics
            {
                AverageTemperature = average,

                MinimumTemperature = ValueMin,
                MinimumTemperatureDate = KeyMin,

                MaximumTemperature = ValueMax,
                MaximumTemperatureDate = KeyMax,

                BelowFreezingDays = belowFreezing,

                DaysWithData = validTemperatures.Count,
            };
        }

        public static Dictionary<DateTime, double?> CalculateMonthlyAverages(
            Dictionary<string, double?> temperatures)
        {
            Dictionary<DateTime, double?> dailyData = [];

            foreach (var item in temperatures)
            {
                DateTime parsedDate = DateTime.ParseExact(
                    item.Key, "yyyyMMdd", CultureInfo.InvariantCulture);

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
