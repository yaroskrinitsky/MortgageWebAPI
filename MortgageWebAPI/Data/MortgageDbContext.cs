using Microsoft.EntityFrameworkCore;
using MortgageWebAPI.Data.Entities;

namespace MortgageWebAPI.Data
{
    public class MortgageDbContext : DbContext
    {
        public MortgageDbContext(DbContextOptions<MortgageDbContext> options)
            : base(options) { }

        public DbSet<MortgageRate> MortgageRates { get; set; }
    }
}