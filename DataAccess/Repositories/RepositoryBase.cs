
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
//using Domain.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected pharmacy199Context RepositoryContext { get; set; }
        public RepositoryBase(pharmacy199Context repositiryContext)
        {
            RepositoryContext = repositiryContext;
        }
        public async Task<List<T>> FindAll() => await RepositoryContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression) =>
             await RepositoryContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();

        public async Task Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public async Task Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public async Task Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);


        //public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
        //public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        //    RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        //public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        //public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        //public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}
