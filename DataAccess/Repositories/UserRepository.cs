using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using DataAccess.Models;
using Domain.Models;

namespace DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(pharmacy199Context repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            var result = await base.FindByCondition(x => x.Mail == email && x.PhoneNumber == password);
            if (result == null || result.Count == 0)
            {
                return null;
            }
            return result[0];
        }

    }
}
