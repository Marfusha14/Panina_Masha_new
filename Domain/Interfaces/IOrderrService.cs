using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderrService
    {
        Task<List<Orderr>> GetAll();
        Task<Orderr> GetById(int id);
        Task Create(Orderr model);
        Task Update(Orderr model);
        Task Delete(int id);
    }
}
