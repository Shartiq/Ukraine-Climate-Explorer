using System.Globalization;
using Ukraine_Climate_Explorer.Analysis;
using Ukraine_Climate_Explorer.Models;
using Ukraine_Climate_Explorer.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.Defaults;

namespace Ukraine_Climate_Explorer
{
    public partial class Form1 : Form
    {
        private readonly NasaApiService nasaService = new();
        private readonly CartesianChart chartTemperature = new();

        public Form1()
        {
            InitializeComponent();
            InitializeCities();
            InitializeChart();
        }

        private async void btnLoadData_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be later than end date.");
                return;
            }

            if (cmbCity.SelectedItem is not City city)
            {
                MessageBox.Show("Select a city.");
                return;
            }

            btnLoadData.Enabled = false;
            btnLoadData.Text = "Loading...";

            try
            {
                var temperatures = await nasaService.GetTemperaturesAsync(city, startDate, endDate);

                ClimateStatistics stats = ClimateAnalyzer.Analyze(temperatures);

                DisplayResults(temperatures, stats, city.Name, startDate, endDate);
                DisplayChart(temperatures);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnLoadData_Click: " + ex.Message);
            }
            finally
            {
                btnLoadData.Enabled = true;
                btnLoadData.Text = "Load Data";
            }
        }

        private void DisplayChart(Dictionary<string, double> temperatures)
        {
            List<DateTimePoint> points = CreateTemperaturePoints(temperatures);

            chartTemperature.Series =
            [
                new LineSeries<DateTimePoint>
                {
                    Name = "Temperature",
                    Values = points,
                    Fill = null,
                    LineSmoothness = 0
                }
            ];
        }

        private void DisplayResults(
            Dictionary<string, double> temperatures,
            ClimateStatistics stats,
            string cityName,
            DateTime startDate, 
            DateTime endDate)
        {
            rtbResult.Clear();

            rtbResult.AppendText(
                    $"Average Temperature: {stats.AverageTemperature:F2} °C " +
                    $"{Environment.NewLine}");

            rtbResult.AppendText(
                $"Minimum Temperature: {stats.MinimumTemperature:F2} °C " +
                $"({FormatDate(stats.MinimumTemperatureDate)})" +
                $"{Environment.NewLine}");

            rtbResult.AppendText(
                $"Maximum Temperature: {stats.MaximumTemperature:F2} °C " +
                $"({FormatDate(stats.MaximumTemperatureDate)})" +
                $"{Environment.NewLine}");

            rtbResult.AppendText(
                $"Below Freezing Days: {stats.BelowFreezingDays} " +
                $"{Environment.NewLine}");

            rtbResult.AppendText(
                $"Temperatures in {cityName} from {startDate:dd-MM-yyyy} " +
                $"to {endDate:dd-MM-yyyy} (°C):" +
                $"{Environment.NewLine}");

            foreach (var item in temperatures.Where(t => t.Value != -999))
            {
                rtbResult.AppendText(
                    $"{FormatDate(item.Key)}: {item.Value} " +
                    $"°C{Environment.NewLine}");
            }
        }

        private void InitializeCities()
        {
            var cities = new List<City>
            {
                new() {
                    Name = "Kyiv",
                    Latitude = 50.4501,
                    Longitude = 30.5234
                },
                new() {
                    Name = "Lviv",
                    Latitude = 49.8397,
                    Longitude = 24.0297
                },
                new() {
                    Name = "Odesa",
                    Latitude = 46.4825,
                    Longitude = 30.7233
                },
                new() {
                    Name = "Kharkiv",
                    Latitude = 49.9935,
                    Longitude = 36.2304
                },
                new() {
                    Name = "Dnipro",
                    Latitude = 48.4647,
                    Longitude = 35.0462
                }
            };
            cmbCity.DisplayMember = nameof(City.Name);
            cmbCity.DataSource = cities;
        }

        private void InitializeChart()
        {
            chartTemperature.Dock = DockStyle.Fill;
            pnlChart.Controls.Add(chartTemperature);

            chartTemperature.XAxes =
            [
                new DateTimeAxis(
                    TimeSpan.FromDays(1),
                    date => date.ToString("dd.MM.yyyy"))
            ];
        }

        private static string FormatDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(
                date, "yyyyMMdd", CultureInfo.InvariantCulture
            );

            return parsedDate.ToString("dd-MM-yyyy");
        }

        private static List<DateTimePoint> CreateTemperaturePoints(Dictionary<string, double> temperatures)
        {
            List<DateTimePoint> points = [];

            foreach (var item in temperatures.Where(t => t.Value != -999).OrderBy(t => t.Key))
            {
                DateTime parsedDate = DateTime.ParseExact(
                    item.Key, "yyyyMMdd", CultureInfo.InvariantCulture
                );
                points.Add(new DateTimePoint(parsedDate, item.Value));
            }
            return points;
        }
    }
}
