using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MortgageWebAPI.ApiModels
{
    public class MortgageCheckApiModel
    {
        [Required]
        [JsonPropertyName("income")]
        public double Income { get; set; }
        [Required]
        [JsonPropertyName("maturityPeriod")]
        public int MaturityPeriod { get; set; }
        [Required]
        [JsonPropertyName("loanValue")]
        public double LoanValue { get; set; }
        [Required]
        [JsonPropertyName("homeValue")]
        public double HomeValue { get; set; }
    }
}