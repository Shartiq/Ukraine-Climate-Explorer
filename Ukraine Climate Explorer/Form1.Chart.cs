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
        private void DisplayChart()
        {
            if (loadedPrimary is not { } primary)
                return;

            var primarySeries = CreateTemperatureSeries(primary.Temperatures, primary.CityName);

            chartTemperature.Series = loadedComparison is { } comparison
                ? [primarySeries, CreateTemperatureSeries(comparison.Temperatures, comparison.CityName)]
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

            cmbChartMode.Items.AddRange(["Daily", "Monthly"]);
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
            Dictionary<string, double?> temperatures, string cityName)
        {
            List<DateTimePoint?> points = cmbChartMode.SelectedIndex == 1
                ? CreateMonthlyTemperaturePoints(temperatures)
                : CreateTemperaturePoints(temperatures);

            return new LineSeries<DateTimePoint?>
            {
                YToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:F2} °C",
                GeometrySize = points.Count <= 60 ? 6 : 1,
                Name = cmbChartMode.SelectedIndex == 1
                    ? $"{cityName} — monthly average"
                    : $"{cityName} — daily temperature",
                EnableNullSplitting = true,
                LineSmoothness = 0,
                Values = points,
                Fill = null
            };
        }

        private static List<DateTimePoint?> CreateMonthlyTemperaturePoints(
            Dictionary<string, double?> temperatures)
        {
            List<DateTimePoint?> points = [];

            Dictionary<DateTime, double?> monthlyAverage =
                ClimateAnalyzer.CalculateMonthlyAverages(temperatures);

            foreach (var item in monthlyAverage.OrderBy(t => t.Key))
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
