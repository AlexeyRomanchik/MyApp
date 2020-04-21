using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class RamViewModel : BaseProductViewModel<Ram>
    {
        public SortBaseViewModel SortBaseViewModel { get; set; }
    }
}
