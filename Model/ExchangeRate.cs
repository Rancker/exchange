
using System.Text.Json.Serialization;

namespace ExchangeApi.Model
{
    public class ExchangeRate
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("time_last_updated")]
        public double LastUpdatedTime { get; set; }

        [JsonPropertyName("rates")]
        public Rates Rates { get; set; }
    }
}
