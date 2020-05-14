﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
        public override IQueryable<CartItem> FindAll()
        {
            return RepositoryContext.Set<CartItem>()
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .AsNoTracking();
        }
    }
}
