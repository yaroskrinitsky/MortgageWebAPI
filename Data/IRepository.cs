using System.Collections.Generic;

namespace MortgageWebAPI.Data
{
    public interface IRepository<T>
    {
        IList<T> GetAll();
    }
}