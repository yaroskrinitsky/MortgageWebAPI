using System.Collections.Generic;
using System.Linq;
using MortgageWebAPI.Data.Entities;

namespace MortgageWebAPI.Data
{
    public class MortgageRateRepository : GenericRepository<MortgageRate>
    {
        public MortgageRateRepository(MortgageDbContext context) : base(context)
        {
        }
    }
}