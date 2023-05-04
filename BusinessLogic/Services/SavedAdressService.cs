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
    public class SavedAdressService : ISavedAdressService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public SavedAdressService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<SavedAddress>> GetAll()
        {
            return await _repositoryWrapper.SevedAdress.FindAll();
        }
        public async Task<SavedAddress> GetById(int id)
        {
            var adres = await _repositoryWrapper.SevedAdress
            .FindByCondition(x => x.AddressId == id);
            return adres.First();
        }
        public async Task Create(SavedAddress model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.City))
            {
                throw new ArgumentException(nameof(model.City));
            }
            if (string.IsNullOrEmpty(model.Street))
            {
                throw new ArgumentException(nameof(model.Street));
            }
            if (string.IsNullOrEmpty(model.AddressName))
            {
                throw new ArgumentException(nameof(model.AddressName));
            }
            await _repositoryWrapper.SevedAdress.Create(model);
            await _repositoryWrapper.Save();
        }
        public async Task Update(SavedAddress model)
        {
            await _repositoryWrapper.SevedAdress.Update(model);
            await _repositoryWrapper.Save();
        }
        public async Task Delete(int id)
        {
            var adres = await _repositoryWrapper.SevedAdress
            .FindByCondition(x => x.AddressId == id);
            await _repositoryWrapper.SevedAdress.Delete(adres.First());
            await _repositoryWrapper.Save();
        }
    }
}
