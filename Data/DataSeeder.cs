using System;
using MortgageWebAPI.Data.Entities;

namespace MortgageWebAPI.Data
{
    public static class DataSeeder
    {
        public static void Seed(this MortgageDbContext context)
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
    }
}