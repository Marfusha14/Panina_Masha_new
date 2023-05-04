using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Wrapper
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        Task Save();
        IBaskerRepository Basket { get; }
        

        IFilterrRepository Filterr { get; }
        

        IOrderrRepository Orderr { get; }
        

        IProductRepository Product { get; }
        

        ISavedadressRepository SevedAdress { get; }
        






    }
}
