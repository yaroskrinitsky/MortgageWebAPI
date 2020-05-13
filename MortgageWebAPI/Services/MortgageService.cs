using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MortgageWebAPI.Data;
using MortgageWebAPI.Data.Entities;
using MortgageWebAPI.DTO;

namespace MortgageWebAPI.Services
{
    public class MortgageService : IMortgageService
    {
        private readonly IRepository<MortgageRate> _mortgageRateRepository;
        
        public MortgageService(IRepository<MortgageRate> mortgageRateRepository)
        {
            this._mortgageRateRepository = mortgageRateRepository;
        }

        public IReadOnlyCollection<MortgageRateDto> GetMortgageRates()
        {
            return this._mortgageRateRepository.GetAll().Select(rate => new MortgageRateDto
            {
                InterestRate = rate.InterestRate,
                LastUpdate = rate.LastUpdate,
                MaturityPeriod = rate.MaturityPeriod,
            })
                .ToImmutableList();
        }

        public MortgageCheckDto CheckMortgage(double income, int maturityPeriod, double loanValue, double homeValue)
        {
            var rate = this._mortgageRateRepository.GetAll().FirstOrDefault(r => r.MaturityPeriod == maturityPeriod);
            if (rate == null)
                return null;

            var monthlyCost = calculateMonthlyCost(maturityPeriod, rate.InterestRate, loanValue);

            var isFeasible = checkFeasibility(monthlyCost, maturityPeriod, homeValue, income);

            if (!isFeasible)
            {
                return new MortgageCheckDto
                {
                    IsFeasible = false
                };
            }

            return new MortgageCheckDto
            {
                IsFeasible = true,
                MonthlyCost = monthlyCost
            };
        }

        private double calculateMonthlyCost(int maturityPeriod, double interestRate, double loanValue)
        {
            // https://en.wikipedia.org/wiki/Mortgage_calculator#Monthly_payment_formula
            
            // n - the number of monthly payments, called the loan's term
            var n = maturityPeriod * 12;
            
            // mi - the monthly interest rate, expressed as a decimal, not a percentage.
            var mi = interestRate / 12 / 100;

            double monthlyCost;
            if (Math.Abs(interestRate) < 0.00001)
            {
                // interest rate equals 0 case
                monthlyCost = loanValue / n;
            }
            else
            {
                monthlyCost = loanValue * (mi / (1 - Math.Pow(mi + 1, -n)));
            }

            return monthlyCost;
        }

        private bool checkFeasibility(double monthlyCost, int maturityPeriod, double homeValue, double income)
        {
            var mortgageLoan = monthlyCost * maturityPeriod * 12;

            return mortgageLoan <= homeValue && mortgageLoan <= (4 * income);
        }
    }
}