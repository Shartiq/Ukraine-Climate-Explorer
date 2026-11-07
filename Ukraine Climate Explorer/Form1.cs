using LiveChartsCore.SkiaSharpView.WinForms;
using System.Globalization;
using Ukraine_Climate_Explorer.Analysis;
using Ukraine_Climate_Explorer.Models;
using Ukraine_Climate_Explorer.Services;

namespace Ukraine_Climate_Explorer
{
    public partial class Form1 : Form
    {
        private readonly NasaApiService nasaService = new();
        private readonly CartesianChart chartTemperature = new();
        private (string CityName, Dictionary<string, double?> Temperatures)? loadedPrimary;
        private (string CityName, Dictionary<string, double?> Temperatures)? loadedComparison;

        public Form1()
        {
            InitializeComponent();
            InitializeCities();
            InitializeChart();
            InitializeInputFields();
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
            btnResetAll.Enabled = false;
            btnLoadData.Text = "Loading...";

            try
            {
                var temperatures = await nasaService.GetTemperaturesAsync(city, startDate, endDate);

                ClimateStatistics stats = ClimateAnalyzer.Analyze(temperatures);

                (string CityName, Dictionary<string, double?> Temperatures)? comparison = null;
                ClimateStatistics? comparisonStats = null;

                if (cmbCompareCity.SelectedIndex > 0 && cmbCompareCity.SelectedItem is City compareCity)
                {
                    var compareTemperatures = await nasaService.GetTemperaturesAsync(
                        compareCity, startDate, endDate);

                    if (compareTemperatures.Values.Any(value => value.HasValue))
                    {
                        comparisonStats = ClimateAnalyzer.Analyze(compareTemperatures);
                    }

                    comparison = (compareCity.Name, compareTemperatures);
                }

                loadedPrimary = (city.Name, temperatures);
                loadedComparison = comparison;

                DisplayResults(stats, city.Name, startDate, endDate, 
                    comparison?.CityName, comparisonStats);
                DisplayChart();
                ResetChartZoom();
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnLoadData_Click: " + ex.Message);
            }
            finally
            {
                btnLoadData.Enabled = true;
                btnResetAll.Enabled = true;
                btnLoadData.Text = "Load Data";
                btnResetAll.Text = "Reset All";
            }
        }

        private void DisplayResults(
            ClimateStatistics stats, string cityName,
            DateTime startDate, DateTime endDate,
            string? comparisonCityName, 
            ClimateStatistics? comparisonStats)
        {
            rtbResult.Clear();

            rtbResult.AppendText(
                $"Period: {startDate:dd-MM-yyyy} – {endDate:dd-MM-yyyy}" +
                $"{Environment.NewLine}{Environment.NewLine}");

            AppendStatistics(cityName, stats);

            if (comparisonCityName is not null)
            {
                rtbResult.AppendText(Environment.NewLine);

                if (comparisonStats is not null)
                    AppendStatistics(comparisonCityName, comparisonStats);
                else
                    rtbResult.AppendText($"{comparisonCityName}: No data{Environment.NewLine}");
            }

            int totalDays = (endDate.Date - startDate.Date).Days + 1;

            dccPrimary.SetCoverage(cityName, stats.DaysWithData, totalDays);
            dccPrimary.Visible = true;

            dccComparison.Visible = comparisonCityName is not null;
            if (comparisonCityName is not null)
            {
                dccComparison.SetCoverage(
                    comparisonCityName, comparisonStats?.DaysWithData ?? 0, totalDays);
            }
        }

        private void AppendStatistics(string cityName, ClimateStatistics stats)
        {
            rtbResult.AppendText($"{cityName}{Environment.NewLine}");

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

            var compareCities = new List<City>
            {
                new() { Name = "None" }
            };
            compareCities.AddRange(cities);

            cmbCity.DisplayMember = nameof(City.Name);
            cmbCity.DataSource = cities;

            cmbCompareCity.DisplayMember = nameof(City.Name);
            cmbCompareCity.DataSource = compareCities;
            cmbCompareCity.SelectedIndex = 0;
        }

        private void InitializeInputFields()
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Today;
            cmbCity.SelectedIndex = 0;
            cmbChartMode.SelectedIndex = 0;
            cmbCompareCity.SelectedIndex = 0;
            dccPrimary.Visible = false;
            dccComparison.Visible = false;
        }

        private static string FormatDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(
                date, "yyyyMMdd", CultureInfo.InvariantCulture
            );

            return parsedDate.ToString("dd-MM-yyyy");
        }

        private void ResetInputFields()
        {
            loadedPrimary = null;
            loadedComparison = null;
            InitializeInputFields();
            rtbResult.Clear();
            chartTemperature.Series = [];
            ResetChartZoom();
        }

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            ResetInputFields();
        }
    }
}