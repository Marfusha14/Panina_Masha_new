using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISavedAdressService
    {
        Task<List<SavedAddress>> GetAll();
        Task<SavedAddress> GetById(int id);
        Task Create(SavedAddress model);
        Task Update(SavedAddress model);
        Task Delete(int id);
    }
}
