using DataAccess.Models;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BasketRepository : RepositoryBase<Basket>, IBaskerRepository
    {
        public BasketRepository(pharmacy199Context repositoryContext)
            :base(repositoryContext) 
        { 
        }
    }
}
