using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MortgageWebAPI.ApiModels;
using MortgageWebAPI.Services;
using MortgageWebAPI.ViewModels;

namespace MortgageWebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class MortgageController : ControllerBase
    {
        private readonly IMortgageService _mortgageService;
        public MortgageController(IMortgageService mortgageService)
        {
            this._mortgageService = mortgageService;
        }
        [HttpGet]
        [Route("interest-rates")]
        public IEnumerable<InterestRateViewModel> InterestRates()
        {
            return this._mortgageService.GetMortgageRates().Select(r => new InterestRateViewModel
            {
                InterestRate = r.InterestRate,
                MaturityPeriod = r.MaturityPeriod,
                LastUpdate = r.LastUpdate
            });
        }
        
        [HttpPost]
        [Route("mortgage-check")]
        public MortgageCheckViewModel MortgageCheck([FromBody] MortgageCheckApiModel model)
        {
            var res = _mortgageService.CheckMortgage(model.Income, model.MaturityPeriod, model.LoanValue, model.HomeValue);

            return new MortgageCheckViewModel {IsFeasible = res.IsFeasible, MonthlyCost = res.MonthlyCost};
        }
    }
}