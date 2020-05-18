using System;
using System.Linq;
using System.Linq.Expressions;
using DataProvider.Data;
using DataProvider.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Product;

namespace DataProvider.Repository
{
    public class MotherboardRepository : RepositoryBase<Motherboard>, IMotherboardRepository
    {
        public MotherboardRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }

        public override IQueryable<Motherboard> FindAll()
        {
            return RepositoryContext.Set<Motherboard>().
                Include(x => x.Product)
                .ThenInclude(x => x.Manufacturer)
                .Include("SocketType")
                .Include("Product.Ratings")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

        public override IQueryable<Motherboard> FindByCondition(Expression<Func<Motherboard, bool>> expression)
        {
            return RepositoryContext.Set<Motherboard>()
                .Where(expression)
                .Include(x => x.Product)
                .Include("Product.Manufacturer")
                .Include("Product.Category")
                .Include("MotherboardInterfaces")
                .Include("MotherboardInterfaces.Interface")
                .Include("SocketType")
                .Include("Product.Ratings")
                .Include("Product.Ratings.User")
                .Include("Product.Reviews")
                .Include("Product.Reviews.User")
                .AsNoTracking();
        }

    }
}
