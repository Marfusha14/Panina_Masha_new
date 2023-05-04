using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using DataAccess.Repositories;
using Domain.Wrapper;
using Domain.Interfaces;
using DataAccess.Models;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private pharmacy199Context _repoContext;
        private IUserRepository _user;
        private IBaskerRepository _basket;
        private IFilterrRepository _filterr;
        private IOrderrRepository _order;
        private IProductRepository _product;
        private ISavedadressRepository _adress;


        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IBaskerRepository Basket
        {
            get
            {
                if (_basket == null)
                {
                    _basket = new BasketRepository(_repoContext);
                }
                return _basket;
            }
        }
        public IFilterrRepository Filterr
        {
            get
            {
                if (_filterr == null)
                {
                    _filterr = new FilterrRepository(_repoContext);
                }
                return _filterr;
            }
        }
        public IOrderrRepository Orderr
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderssRepository(_repoContext);
                }
                return _order;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repoContext);
                }
                return _product;
            }
        }
        public ISavedadressRepository SevedAdress
        {
            get
            {
                if (_adress == null)
                {
                    _adress = new SaveadressRepository(_repoContext);
                }
                return _adress;
            }
        }

        public RepositoryWrapper(pharmacy199Context repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public async Task Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
