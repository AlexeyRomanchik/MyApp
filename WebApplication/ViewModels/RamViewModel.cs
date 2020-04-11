using System.Linq;
using WebApplication.Models;

namespace WebApplication.ViewModels
{
    public class RamViewModel
    {
        public IQueryable<Ram> Rams { get; set; } 
    }
}
