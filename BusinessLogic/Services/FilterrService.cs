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
    public class FilterrService : IFilterrService
    {
        private IRepositoryWrapper _repositoryWrapper;
        public FilterrService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public async Task<List<Filterr>> GetAll()
        {
            return await _repositoryWrapper.Filterr.FindAll();
        }
        public async Task<Filterr> GetById(int id)
        {
            var filter = await _repositoryWrapper.Filterr
            .FindByCondition(x => x.IdCategories == id);
            return filter.First();
        }
        public async Task Create(Filterr model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }


            if (string.IsNullOrEmpty(model.ReleaseForm))
            {
                throw new ArgumentException(nameof(model.ReleaseForm));
            }
            if (string.IsNullOrEmpty(model.VacationFromThePharmacy))
            {
                throw new ArgumentException(nameof(model.VacationFromThePharmacy));
            }
            if (string.IsNullOrEmpty(model.Indications))
            {
                throw new ArgumentException(nameof(model.Indications));
            }
            if (string.IsNullOrEmpty(model.Producer))
            {
                throw new ArgumentException(nameof(model.Producer));
            }
            if (string.IsNullOrEmpty(model.ExpirationDate))
            {
                throw new ArgumentException(nameof(model.ExpirationDate));
            }
            if (string.IsNullOrEmpty(model.BrandName))
            {
                throw new ArgumentException(nameof(model.BrandName));
            }
            await _repositoryWrapper.Filterr.Create(model);
            await _repositoryWrapper.Save();
        }
        public async Task Update(Filterr model)
        {
            await _repositoryWrapper.Filterr.Update(model);
            await _repositoryWrapper.Save();
        }
        public async Task Delete(int id)
        {
            var filter = await _repositoryWrapper.Filterr
            .FindByCondition(x => x.IdCategories == id);
            await _repositoryWrapper.Filterr.Delete(filter.First());
            await _repositoryWrapper.Save();
        }
    }
}

