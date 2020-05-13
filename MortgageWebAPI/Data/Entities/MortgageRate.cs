using System;
using System.ComponentModel.DataAnnotations;

namespace MortgageWebAPI.Data.Entities
{
    public class MortgageRate
    {
        [Key]
        public int Id { get; set; }
        public int MaturityPeriod { get; set; }
        public double InterestRate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}