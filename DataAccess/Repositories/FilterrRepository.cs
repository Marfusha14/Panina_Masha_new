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
    public class FilterrRepository : RepositoryBase<Filterr>, IFilterrRepository
    {
        public FilterrRepository(pharmacy199Context repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
