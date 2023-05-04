using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFilterrService
    {
        Task<List<Filterr>> GetAll();
        Task<Filterr> GetById(int id);
        Task Create(Filterr model);
        Task Update(Filterr model);
        Task Delete(int id);
    }
}
