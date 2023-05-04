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
    public class BasketService : IBasketService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public BasketService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<Basket>> GetAll()
        {
            return await _repositoryWrapper.Basket.FindAll();
        }
        public async Task<Basket> GetById(int id)
        {
            var basket = await _repositoryWrapper.Basket
            .FindByCondition(x => x.ProductId == id);
            return basket.First();
        }
        public async Task Create(Basket model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await _repositoryWrapper.Basket.Create(model);
            await _repositoryWrapper.Save();

        }
        public async Task Update(Basket model)
        {
            await _repositoryWrapper.Basket.Update(model);
            await _repositoryWrapper.Save();
        }
        public async Task Delete(int id)
        {
            var basket = await _repositoryWrapper.Basket
            .FindByCondition(x => x.ProductId == id);
            await _repositoryWrapper.Basket.Delete(basket.First());
            await _repositoryWrapper.Save();
        }
    }
}
