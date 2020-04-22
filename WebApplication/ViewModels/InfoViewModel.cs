using System.Collections.Generic;

namespace WebApplication.ViewModels
{
    public class InfoViewModel<TProduct>
    {
        public TProduct Product { get; set; }
        public List<TProduct> PopularGoods { get; set; }
    }
}
