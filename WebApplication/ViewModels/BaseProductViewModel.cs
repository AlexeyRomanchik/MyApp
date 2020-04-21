using System.Collections.Generic;

namespace WebApplication.ViewModels
{
    public class BaseProductViewModel<T>
    {
        public List<T> PopularGoods { get; set; }
        public List<T> NewItems { get; set; }
        public List<T> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
