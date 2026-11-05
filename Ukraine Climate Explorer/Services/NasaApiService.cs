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
        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Dictionary<string, double>> GetTemperaturesAsync(
            DateTime startDate, DateTime endDate)
        {

            string start = startDate.ToString("yyyyMMdd");
            string end = endDate.ToString("yyyyMMdd");

            string url =
                $"https://power.larc.nasa.gov/api/temporal/daily/point" +
                $"?parameters=T2M" +
                $"&community=RE" +
                $"&longitude=30.5234" +
                $"&latitude=50.4501" +
                $"&start={start}" +
                $"&end={end}" +
                $"&format=JSON";

            using var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string data = await response.Content.ReadAsStringAsync();

            NasaPowerResponse? nasaData =
                    JsonSerializer.Deserialize<NasaPowerResponse>(data, jsonOptions) ??
                        throw new InvalidOperationException("Failed to deserialize NASA Power API response.");

            return nasaData.Properties.Parameter.T2M;
        }
    }
}
