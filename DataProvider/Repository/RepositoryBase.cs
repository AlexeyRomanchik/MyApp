using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataProvider.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryBase(ApplicationContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        protected ApplicationContext RepositoryContext { get; set; }


        public virtual IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }

        public virtual void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }
    }
}