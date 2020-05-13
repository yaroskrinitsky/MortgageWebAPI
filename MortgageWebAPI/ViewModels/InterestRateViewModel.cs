using System;
using System.Text.Json.Serialization;

namespace MortgageWebAPI.ViewModels
{
    public class InterestRateViewModel
    {
        [JsonPropertyName("maturityPeriod")]
        public int MaturityPeriod { get; set; }
        [JsonPropertyName("interestRate")]
        public double InterestRate { get; set; }
        [JsonPropertyName("lastUpdate")]
        public DateTime LastUpdate { get; set; }
    }
}