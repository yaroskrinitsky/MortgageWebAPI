using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MortgageWebAPI.ApiModels;
using MortgageWebAPI.Controllers;
using MortgageWebAPI.DTO;
using MortgageWebAPI.Services;
using MortgageWebAPI.ViewModels;
using Xunit;

namespace MortgageWebAPITest
{
    public class MortgageWebApiControllerTests
    {
        class MortgageServiceMock : IMortgageService
        {
            public IReadOnlyCollection<MortgageRateDto> GetMortgageRates()
            {
                return new List<MortgageRateDto>
                {
                    new MortgageRateDto
                    {
                        InterestRate = 1.0,
                        LastUpdate = DateTime.Now,
                        MaturityPeriod = 1
                    },
                    new MortgageRateDto
                    {
                        InterestRate = 2.0,
                        LastUpdate = DateTime.Now,
                        MaturityPeriod = 2
                    }
                };
            }

            public MortgageCheckDto CheckMortgage(double income, int maturityPeriod, double loanValue, double homeValue)
            {
                var rate = this.GetMortgageRates().FirstOrDefault(r => r.MaturityPeriod == maturityPeriod);
                if (rate == null)
                    return null;

                return new MortgageCheckDto();
            }
        }

        [Fact]
        public void InterestRates_ReturnsRatesList()
        {
            var mortgageServiceMock = new MortgageServiceMock();
            var controller = new MortgageController(mortgageServiceMock);
            var response = controller.InterestRates();

            Assert.True(response != null || response!.Count() == 2);
        }

        [Fact]
        public void MortgageCheck_Returns404IfNoSuchMaturityPeriod()
        {
            var mortgageServiceMock = new MortgageServiceMock();
            var controller = new MortgageController(mortgageServiceMock);
            var response = controller.MortgageCheck(new MortgageCheckApiModel
            {
                MaturityPeriod = 99
            }) as ObjectResult;
            
            Assert.NotNull(response);
            Assert.True(response.StatusCode == StatusCodes.Status404NotFound);
        }
        
        [Fact]
        public void MortgageCheck_ReturnsFeasibilityAndMonthlyCost()
        {
            var mortgageServiceMock = new MortgageServiceMock();
            var controller = new MortgageController(mortgageServiceMock);
            var response = controller.MortgageCheck(new MortgageCheckApiModel{MaturityPeriod = 1}) as ObjectResult;
            
            Assert.NotNull(response);
            Assert.True(response.StatusCode == StatusCodes.Status200OK);
            Assert.NotNull(response.Value as MortgageCheckViewModel);
        }
    }
}