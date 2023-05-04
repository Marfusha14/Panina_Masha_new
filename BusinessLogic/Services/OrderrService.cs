using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class OrderrService : IOrderrService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public OrderrService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<Orderr>> GetAll()
        {
            return await _repositoryWrapper.Orderr.FindAll();
        }
        public async Task<Orderr> GetById(int id)
        {
            var order = await _repositoryWrapper.Orderr
            .FindByCondition(x => x.OrderNumber == id);
            return order.First();
        }
        public async Task Create(Orderr model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }


            if (string.IsNullOrEmpty(model.Statuss))
            {
                throw new ArgumentException(nameof(model.Statuss));
            }
            await _repositoryWrapper.Orderr.Create(model);
            await _repositoryWrapper.Save();
        }
        public async Task Update(Orderr model)
        {
            await _repositoryWrapper.Orderr.Update(model);
            await _repositoryWrapper.Save();
        }
        public async Task Delete(int id)
        {
            var order = await _repositoryWrapper.Orderr
            .FindByCondition(x => x.OrderNumber == id);
            await _repositoryWrapper.Orderr.Delete(order.First());
            await _repositoryWrapper.Save();
        }
    }
}
