using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Ukraine_Climate_Explorer.Models
{
    public class PowerParameters
    {
        [JsonPropertyName("T2M")]
        public Dictionary<string, double> T2M { get; set; } = [];
    }
}