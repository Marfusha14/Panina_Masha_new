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
