using WebApplication.Contracts;

namespace WebApplication.Services
{
    public class SortServiceWrapper : ISortServiceWrapper
    {
        private IRamSortService _ramSortService;
        public IRamSortService RamSortService
        {
            get { return _ramSortService ??= new RamSortService(); }
            set => throw new System.NotImplementedException();
        }
    }
}
