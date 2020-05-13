using System.Collections.Generic;
using MortgageWebAPI.DTO;

namespace MortgageWebAPI.Services
{
    public interface IMortgageService
    {
        IReadOnlyCollection<MortgageRateDto> GetMortgageRates();

        MortgageCheckDto CheckMortgage(double income, int maturityPeriod, double loanValue, double homeValue);
    }
}