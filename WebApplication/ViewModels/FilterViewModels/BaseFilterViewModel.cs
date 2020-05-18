using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.ViewModels.FilterViewModels
{
    public class BaseFilterViewModel
    {
        public const string AllManufacturers = "All";
        public SelectList Manufacturers { get; set; }
        public string SelectedManufacturer { get; set; }

        public BaseFilterViewModel(List<string> manufacturers,
            string selectedManufacturer)
        {
            manufacturers.Add(AllManufacturers);
            SelectedManufacturer = selectedManufacturer;
            Manufacturers = new SelectList(manufacturers);
        }
    }
}
