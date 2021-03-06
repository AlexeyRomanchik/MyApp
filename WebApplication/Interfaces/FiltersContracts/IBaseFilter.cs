﻿using System.Linq;
using WebApplication.ViewModels.FilterViewModels;

namespace WebApplication.Interfaces.FiltersContracts
{
    public interface IBaseFilter<T>
    {
        IQueryable<T> ApplyBaseFilter(BaseFilterViewModel filterViewModel, IQueryable<T> products);
    }
}