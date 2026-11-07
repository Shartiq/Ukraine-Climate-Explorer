using System.Globalization;
using System.Text.Json;
using Ukraine_Climate_Explorer.Models;

namespace Ukraine_Climate_Explorer.Services
{
    public class NasaApiService
    {
        private readonly HttpClient client = new();
        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Dictionary<string, double?>> GetTemperaturesAsync(
            City city, DateTime startDate, DateTime endDate)
        {
            string latitude = city.Latitude.ToString(CultureInfo.InvariantCulture);
            string longitude = city.Longitude.ToString(CultureInfo.InvariantCulture);

            string start = startDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string end = endDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

            string url =
                $"https://power.larc.nasa.gov/api/temporal/daily/point" +
                $"?parameters=T2M" +
                $"&community=RE" +
                $"&longitude={longitude}" +
                $"&latitude={latitude}" +
                $"&start={start}" +
                $"&end={end}" +
                $"&format=JSON";

            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();

            NasaPowerResponse? nasaData =
                    JsonSerializer.Deserialize<NasaPowerResponse>(data, jsonOptions) ??
                        throw new InvalidOperationException("Failed to deserialize NASA Power API response.");


            return nasaData.Properties.Parameter.T2M
                .ToDictionary(
                    item => item.Key,
                    item => item.Value == -999 ? (double?)null : item.Value);
        }
    }
}
