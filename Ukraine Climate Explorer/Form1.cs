using System.Globalization;
using Ukraine_Climate_Explorer.Analysis;
using Ukraine_Climate_Explorer.Models;
using Ukraine_Climate_Explorer.Services;

namespace Ukraine_Climate_Explorer
{
    public partial class Form1 : Form
    {
        private readonly NasaApiService nasaService = new();

        public Form1()
        {
            InitializeComponent();
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

            btnLoadData.Enabled = false;

            try
            {
                var temperatures = await nasaService.GetTemperaturesAsync(startDate, endDate);

                ClimateStatistics stats = ClimateAnalyzer.Analyze(temperatures);

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
                    $"Temperatures in Kyiv from {startDate:dd-MM-yyyy} " +
                    $"to {endDate:dd-MM-yyyy} (°C):" +
                    $"{Environment.NewLine}");

                foreach (var item in temperatures.Where(t => t.Value != -999))
                {
                    rtbResult.AppendText(
                        $"{FormatDate(item.Key)}: {item.Value} " +
                        $"°C{Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnLoadData_Click: " + ex.Message);
            }
            finally
            {
                btnLoadData.Enabled = true;
            }
        }

        private static string FormatDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(
                date, "yyyyMMdd", CultureInfo.InvariantCulture
            );

            return parsedDate.ToString("dd-MM-yyyy");
        }
    }
}
