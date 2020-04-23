using System.Collections.Generic;

namespace WebApplication.ViewModels
{
    public class CrudViewModel<T>
    {
        public List<T> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
