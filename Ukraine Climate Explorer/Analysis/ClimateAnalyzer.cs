using System.Globalization;
using Ukraine_Climate_Explorer.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            return CalculatePeriodAverages(
                temperatures,
                date => new DateTime(date.Year, date.Month, 1));
        }

        public static Dictionary<DateTime, double?> CalculateWeeklyAverages(
            Dictionary<string, double?> temperatures)
        {
            return CalculatePeriodAverages(temperatures, date =>
            {
                int daysSinceMonday = ((int)date.DayOfWeek + 6) % 7;
                return date.AddDays(-daysSinceMonday);
            });
        }

        public static Dictionary<string, double?> CalculateTemperatureDifferences(
            Dictionary<string, double?> primary, Dictionary<string, double?> comparison)
        {
            Dictionary<string, double?> differences = [];

            foreach (var date in primary.Keys.Union(comparison.Keys))
            {
                primary.TryGetValue(date, out double? primaryValue);
                comparison.TryGetValue(date, out double? comparisonValue);

                differences[date] = primaryValue is double first && comparisonValue is double second
                    ? second - first
                    : null;
            }

            return differences;
        }

        private static Dictionary<DateTime, double?> CalculatePeriodAverages(
            Dictionary<string, double?> temperatures,
            Func<DateTime, DateTime> getPeriodStart)
        {
            Dictionary<DateTime, double?> dailyData = [];

            foreach (var item in temperatures)
            {
                DateTime parsedDate = DateTime.ParseExact(
                    item.Key, "yyyyMMdd", CultureInfo.InvariantCulture);

                dailyData.Add(parsedDate, item.Value);
            }

            return dailyData
                .GroupBy(t => getPeriodStart(t.Key))
                .ToDictionary(
                    group => group.Key,
                    group => group.Average(t => t.Value));
        }
    }
}
