using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApplication.Contracts;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Services
{
    public abstract class ProductSortService<TProduct> : IProductSortService<TProduct>
    {
        public abstract IQueryable<TProduct> SortBy(SortState sortState, IQueryable<TProduct> products);
    }
}
