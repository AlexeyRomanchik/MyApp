﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Contracts;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class CpuController : Controller
    {
        private const int PageSize = 20;

        private readonly ICpuRepository _cpuRepository;

        public CpuController(IRepositoryWrapper repositoryWrapper)
        {
            _cpuRepository = repositoryWrapper.CpuRepository;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            var ramProducts = _cpuRepository.FindAll();

            var count = await ramProducts.CountAsync();

            var items = await ramProducts.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var pageViewModel = new PageViewModel(count, page, PageSize);

            var cpuViewModel = new CpuViewModel
            {
                CpuList = items,
                PageViewModel = pageViewModel,
                NewItems = ramProducts
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(20).ToList()
            };

            return View(cpuViewModel);
        }

        public IActionResult Info(Guid id)
        {
            var product = _cpuRepository.
                FindByCondition(x => x.Product.Id == id).First();

            var infoViewModel = new InfoViewModel<Cpu>()
            {
                Product = product,
                PopularGoods = _cpuRepository.FindAll()
                    .OrderByDescending(x => x.Product.DateAdded)
                    .Take(4).ToList()
            };

            return View(infoViewModel);
        }
    }
}