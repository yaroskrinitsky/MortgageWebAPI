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

            var mi = rate.InterestRate / 12 / 100;
            var n = maturityPeriod * 12;
            var monthlyCost = loanValue * (mi / (1 - Math.Pow(mi + 1, -n)));
            var mortgageLoan = monthlyCost * n;

            if (mortgageLoan > homeValue || mortgageLoan > (4 * income))
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
    }
}