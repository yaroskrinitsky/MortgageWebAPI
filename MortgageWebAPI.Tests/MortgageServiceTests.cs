﻿using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MortgageWebAPI.Data;
using MortgageWebAPI.Data.Entities;
using MortgageWebAPI.Services;
using Xunit;

namespace MortgageWebAPITest
{
    public class MortgageServiceTests
    {
        public MortgageServiceTests()
        {
            
        }
        void Seed(MortgageDbContext context)
        {
            context.MortgageRates.AddRange(
                new MortgageRate
                {
                    InterestRate = 1.24,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 1,
                },
                new MortgageRate
                {
                    InterestRate = 1.24,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 2,
                },
                new MortgageRate
                {
                    InterestRate = 1.24,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 3,
                },
                new MortgageRate
                {
                    InterestRate = 1.27,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 5,
                },
                new MortgageRate
                {
                    InterestRate = 1.27,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 6,
                },
                new MortgageRate
                {
                    InterestRate = 1.28,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 7,
                },
                new MortgageRate
                {
                    InterestRate = 1.34,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 10,
                },
                new MortgageRate
                {
                    InterestRate = 1.57,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 12,
                },
                new MortgageRate
                {
                    InterestRate = 1.65,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 15,
                },
                new MortgageRate
                {
                    InterestRate = 1.65,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 17,
                },
                new MortgageRate
                {
                    InterestRate = 1.75,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 20
                },
                new MortgageRate
                {
                    InterestRate = 1.99,
                    LastUpdate = DateTime.Now,
                    MaturityPeriod = 30,
                }
                );

            context.SaveChanges();
        }
        
        [Fact]
        public void GetMortgageRates_ReturnsRatesList()
        {
            var options = new DbContextOptionsBuilder<MortgageDbContext>().UseInMemoryDatabase(databaseName:"MortgageDbTest1").Options;
            var dbContext = new MortgageDbContext(options);
            Seed(dbContext);
            
            var service = new MortgageService(new MortgageRateRepository(dbContext));
            
            var rates = service.GetMortgageRates();

            Assert.NotEmpty(rates);
            Assert.Equal(12, rates.Count);
        }

        [Fact]
        public void CheckMortgage_MaturityPeriodNotFoundReturnsNull()
        {
            var options = new DbContextOptionsBuilder<MortgageDbContext>().UseInMemoryDatabase(databaseName:"MortgageDbTest2").Options;
            var dbContext = new MortgageDbContext(options);
            Seed(dbContext);
            
            var service = new MortgageService(new MortgageRateRepository(dbContext));
            
            var res = service.CheckMortgage(0, 99, 0, 0);
            Assert.Null(res);
        }

        [Fact]
        public void CheckMortgage_ReturnsNotFeasibleResult_IncomeNotEnough()
        {
            var options = new DbContextOptionsBuilder<MortgageDbContext>().UseInMemoryDatabase(databaseName:"MortgageDbTest3").Options;
            var dbContext = new MortgageDbContext(options);
            Seed(dbContext);
            
            var service = new MortgageService(new MortgageRateRepository(dbContext));
            
            var res = service.CheckMortgage(12000, 1, 50000, 150000);
            Assert.NotNull(res);
            Assert.False(res.IsFeasible);
        }

        [Fact]
        public void CheckMortgage_ReturnsNotFeasibleResult_MortgageMoreThanHome()
        {
            var options = new DbContextOptionsBuilder<MortgageDbContext>().UseInMemoryDatabase(databaseName:"MortgageDbTest4").Options;
            var dbContext = new MortgageDbContext(options);
            Seed(dbContext);
            
            var service = new MortgageService(new MortgageRateRepository(dbContext));
            
            var res = service.CheckMortgage(200000, 10, 150000, 160000);
            Assert.NotNull(res);
            Assert.False(res.IsFeasible);
        }
        
        [Fact]
        public void CheckMortgage_IsFeasible()
        {
            var options = new DbContextOptionsBuilder<MortgageDbContext>().UseInMemoryDatabase(databaseName:"MortgageDbTest5").Options;
            var dbContext = new MortgageDbContext(options);
            Seed(dbContext);
            
            var service = new MortgageService(new MortgageRateRepository(dbContext));
            
            var res = service.CheckMortgage(60000, 10, 200000, 250000);
            Assert.NotNull(res);
            Assert.True(res.IsFeasible);
            Assert.True(Math.Abs(1781.75546 - res.MonthlyCost) < 0.00001);
            //Assert.Equal(1781.7554685308316,res.MonthlyCost);
        }
    }
}