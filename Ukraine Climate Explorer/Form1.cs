using Microsoft.VisualBasic.Logging;
using System.Globalization;
using System.Text.Json;
using Ukraine_Climate_Explorer.Analysis;
using Ukraine_Climate_Explorer.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Ukraine_Climate_Explorer
{
    public partial class Form1 : Form
    {
        private readonly JsonSerializerOptions options;
        private readonly HttpClient client = new();

        readonly string url = 
            "https://power.larc.nasa.gov/api/temporal/daily/point?parameters=T2M&community=RE&longitude=30.5234&latitude=50.4501&start=20250101&end=20250131&format=JSON".Trim();

        public Form1()
        {
            InitializeComponent();

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        private async void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    NasaPowerResponse? nasaData = 
                        JsonSerializer.Deserialize<NasaPowerResponse>(data) ?? 
                            throw new Exception("Failed to deserialize NASA Power API response.");

                    var temperatures = nasaData.Properties.Parameter.T2M;

                    ClimateStatistics stats = ClimateAnalyzer.Analyze(temperatures);

                    rtbResult.Clear();

                    rtbResult.AppendText($"Average Temperature: {stats.AverageTemperature:F2} °C {Environment.NewLine}");
                    rtbResult.AppendText($"Minimum Temperature: {stats.MinimumTemperature:F2} °C {Environment.NewLine}");
                    rtbResult.AppendText($"Maximum Temperature: {stats.MaximumTemperature:F2} °C {Environment.NewLine}");
                    rtbResult.AppendText($"Below Freezing Days: {stats.BelowFreezingDays} {Environment.NewLine}");

                    rtbResult.AppendText($"All temperatures in Kyiv through January 2025 (°C): {Environment.NewLine}");

                    foreach (var item in temperatures.Where(t => t.Value != -999))
                    {
                        rtbResult.AppendText($"{FormateDate(item.Key)}: {item.Value} °C{Environment.NewLine}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnLoadData_Click: " + ex.Message);
            }
        }

        private string FormateDate(string date)
        {
            DateTime parsedDate = DateTime.ParseExact(
                date, "yyyyMMdd", CultureInfo.InvariantCulture
            );

            return parsedDate.ToString("dd-MM-yyyy");
        }
    }
}
