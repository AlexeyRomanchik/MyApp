
namespace WebApplication.ViewModels
{
    public class SortBaseViewModel
    {
        public SortState PriceSortState { get; set; }
        public SortState NameSortState { get; set; }
        public SortState DateAddedSortState { get; set; }
        public SortState CurrentSortState { get; set; }

        public SortBaseViewModel(SortState sortState)
        {
            PriceSortState = sortState == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            NameSortState = sortState == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            DateAddedSortState = sortState == SortState.DateAddedAsc ? SortState.DateAddedDesc : SortState.DateAddedAsc;
            CurrentSortState = sortState;
        }
    }
}
