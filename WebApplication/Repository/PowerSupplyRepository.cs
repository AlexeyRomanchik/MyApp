using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class PowerSupplyRepository : RepositoryBase<PowerSupply>, IPowerSupplyRepository
    {
        public PowerSupplyRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<PowerSupply> FindAll()
        {
            return RepositoryContext.Set<PowerSupply>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .Include("Product.Ratings")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

        public override IQueryable<PowerSupply> FindByCondition(Expression<Func<PowerSupply, bool>> expression)
        {
            return RepositoryContext.Set<PowerSupply>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("Product.Ratings")
                .Include("Product.Ratings.User")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }
    }
}
