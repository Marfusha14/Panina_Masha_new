using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBasketService
    {
        Task<List<Basket>> GetAll();
        Task<Basket> GetById(int id);
        Task Create(Basket model);
        Task Update(Basket model);
        Task Delete(int id);
    }
}
