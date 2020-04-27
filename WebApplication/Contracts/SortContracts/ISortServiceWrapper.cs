namespace WebApplication.Contracts.SortContracts
{
    public interface ISortServiceWrapper
    {
        IRamSortService RamSortService { get; set; }
         ICpuSortService CpuSortService { get; set; }
         IPowerSupplySortService PowerSupplySortService { get; set; }
         IHddSortService HddSortService { get; set; }
         IMotherboardSortService MotherboardSortService { get; set; }
         IGraphicsCardSortService GraphicsCardSortService { get; set; }
    }
}