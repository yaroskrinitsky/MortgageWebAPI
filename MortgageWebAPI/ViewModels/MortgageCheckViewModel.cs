using System;

namespace MortgageWebAPI.ViewModels
{
    public class MortgageCheckViewModel
    {
        private double _monthlyCost;
        public bool IsFeasible { get; set; }

        public double MonthlyCost
        {
            get => Math.Round(_monthlyCost, 2, MidpointRounding.AwayFromZero);
            set => _monthlyCost = value;
        }
    }
}