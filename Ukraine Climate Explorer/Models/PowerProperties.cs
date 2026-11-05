using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Ukraine_Climate_Explorer.Models
{
    public class PowerProperties
    {
        [JsonPropertyName("parameter")]
        public PowerParameters Parameter { get; set; } = new PowerParameters();
    }
}
