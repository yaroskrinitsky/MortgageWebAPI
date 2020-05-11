using System.Collections.Generic;
using System.Linq;
using MortgageWebAPI.Data.Entities;

namespace MortgageWebAPI.Data
{

   /* public interface IMortgateRepository : IRepository<MortgageRate>
    {
        void someMethod();
    }
    */
    public class MortgageRateRepository : GenericRepository<MortgageRate> //, IMortgateRepository
    {
        /*public void someMethod()
        {
            
        }*/
        
        public MortgageRateRepository(MortgageDbContext context) : base(context)
        {
        }

        /*public override IEnumerable<MortgageRate> GetAll()
        {
            return base.GetAll().Where(x => x.InterestRate > 1).ToList();
        }*/
    }
}