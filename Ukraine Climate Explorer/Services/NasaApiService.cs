using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Ukraine_Climate_Explorer.Models;

namespace Ukraine_Climate_Explorer.Services
{
    public class NasaApiService
    {
        private readonly HttpClient client = new();

        readonly string url =
            "https://power.larc.nasa.gov/api/temporal/daily/point?parameters=T2M&community=RE&longitude=30.5234&latitude=50.4501&start=20250101&end=20250131&format=JSON".Trim();

        public async Task<Dictionary<string, double>> GetTemperaturesAsync()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();

            NasaPowerResponse? nasaData =
                    JsonSerializer.Deserialize<NasaPowerResponse>(data, options) ??
                        throw new InvalidOperationException("Failed to deserialize NASA Power API response.");

            return nasaData.Properties.Parameter.T2M;
        }
    }
}
