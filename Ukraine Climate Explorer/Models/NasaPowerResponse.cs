using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Ukraine_Climate_Explorer.Models
{
    public class NasaPowerResponse
    {
        [JsonPropertyName("properties")]
        public PowerProperties Properties { get; set; } = new();
    }
}
