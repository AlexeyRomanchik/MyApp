using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class GraphicsCardViewModel
    {
        public List<GraphicsCard> PopularGoods { get; set; }
        public List<GraphicsCard> NewItems { get; set; }
        public List<GraphicsCard> GraphicsCards { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
