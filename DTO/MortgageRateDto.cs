using System;

namespace MortgageWebAPI.DTO
{
    public class MortgageRateDto
    {
        public int MaturityPeriod { get; set; }
        public double InterestRate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}