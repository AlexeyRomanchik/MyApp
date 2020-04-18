﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class GraphicsCardController : Controller
    {
        private const int PageSize = 20;

        private readonly IGraphicsCardRepository _graphicsCardRepository;

        public GraphicsCardController(IRepositoryWrapper repositoryWrapper)
        {
            _graphicsCardRepository = repositoryWrapper.GraphicsCardRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var powerSupplies = _graphicsCardRepository.FindAll();

            var count = await powerSupplies.CountAsync();

            var items = await powerSupplies.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var graphicsCardViewModel = new GraphicsCardViewModel()
            {
                GraphicsCards = items,
                PageViewModel = pageViewModel,
                NewItems = powerSupplies
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(graphicsCardViewModel);
        }
    }
}