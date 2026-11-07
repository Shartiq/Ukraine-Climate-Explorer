using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Globalization;
using Ukraine_Climate_Explorer.Analysis;

namespace Ukraine_Climate_Explorer
{
    public partial class Form1
    {
        private enum ChartMode
        {
            Daily,
            Weekly,
            Monthly,
            Difference
        }

        private void DisplayChart()
        {
            if (loadedPrimary is not { } primary)
                return;

            if (cmbChartMode.SelectedItem is not ChartMode mode)
                return;

            if (mode == ChartMode.Difference)
            {
                if (loadedComparison is not { } second)
                {
                    chartTemperature.Series = [];
                    return;
                }

                var differences = ClimateAnalyzer.CalculateTemperatureDifferences(
                    primary.Temperatures, second.Temperatures);

                string seriesName = $"{second.CityName} − {primary.CityName}";
                var differenceSeries = CreateTemperatureSeries(differences, seriesName, mode);

                differenceSeries.Stroke = new SolidColorPaint(SKColors.Goldenrod)
                {
                    StrokeThickness = 3
                };
                differenceSeries.GeometryStroke = new SolidColorPaint(SKColors.Goldenrod)
                {
                    StrokeThickness = 2
                };

                chartTemperature.Series = [differenceSeries];
                return;
            }

            var primarySeries = CreateTemperatureSeries(primary.Temperatures, primary.CityName, mode);

            chartTemperature.Series = loadedComparison is { } comparison
                ? [primarySeries, CreateTemperatureSeries(comparison.Temperatures, comparison.CityName, mode)]
                : [primarySeries];
        }

        private void InitializeChart()
        {
            chartTemperature.Dock = DockStyle.Fill;
            pnlChart.Controls.Add(chartTemperature);
            chartTemperature.ZoomMode = ZoomAndPanMode.X;
            chartTemperature.LegendPosition = LegendPosition.Top;

            chartTemperature.LegendTextPaint = new SolidColorPaint(SKColors.Black)
            {
                SKTypeface = SKTypeface.FromFamilyName("Microsoft Sans Serif")
            };
            chartTemperature.LegendTextSize = 13;

            cmbChartMode.DataSource = Enum.GetValues<ChartMode>();
            cmbChartMode.SelectedIndex = 0;

            chartTemperature.XAxes =
            [
                new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd.MM.yyyy"))
                {
                    Name = "Date"
                }
            ];

            chartTemperature.YAxes =
            [
                new Axis
                {
                    Name = "Temperature, °C"
                }
            ];
        }

        private static List<DateTimePoint?> CreateTemperaturePoints(Dictionary<string, double?> temperatures)
        {
            List<DateTimePoint?> points = [];

            foreach (var item in temperatures.OrderBy(t => t.Key))
            {
                if (item.Value is not double temperature)
                {
                    points.Add(null);
                    continue;
                }

                DateTime parsedDate = DateTime.ParseExact(
                    item.Key, "yyyyMMdd", CultureInfo.InvariantCulture);

                points.Add(new DateTimePoint(parsedDate, temperature));
            }
            return points;
        }

        private LineSeries<DateTimePoint?> CreateTemperatureSeries(
            Dictionary<string, double?> temperatures, 
            string cityName, ChartMode mode)
        {
            var (points, description) = mode switch
            {
                ChartMode.Weekly => (CreateAverageTemperaturePoints(
                    ClimateAnalyzer.CalculateWeeklyAverages(temperatures)), "weekly average"),

                ChartMode.Monthly => (CreateAverageTemperaturePoints(
                    ClimateAnalyzer.CalculateMonthlyAverages(temperatures)), "monthly average"),

                _ => (CreateTemperaturePoints(temperatures), "daily temperature")
            };

            return new LineSeries<DateTimePoint?>
            {
                YToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:F2} °C",
                GeometrySize = points.Count <= 60 ? 6 : 1,
                Name = mode == ChartMode.Difference ? cityName : $"{cityName} — {description}",
                EnableNullSplitting = true,
                LineSmoothness = 0.5,
                Values = points,
                Fill = null
            };
        }

        private static List<DateTimePoint?> CreateAverageTemperaturePoints(
            Dictionary<DateTime, double?> averages)
        {
            List<DateTimePoint?> points = [];

            foreach (var item in averages.OrderBy(t => t.Key))
            {
                points.Add(item.Value is double temperature
                    ? new DateTimePoint(item.Key, temperature)
                    : null);
            }

            return points;
        }

        private void ResetChartZoom()
        {
            foreach (var axis in chartTemperature.XAxes)
            {
                axis.MinLimit = null;
                axis.MaxLimit = null;
            }
        }

        private void btnResetZoom_Click(object sender, EventArgs e)
        {
            ResetChartZoom();
        }

        private void cmbChartMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loadedPrimary is null)
                return;

            DisplayChart();
            ResetChartZoom();
        }
    }
}
