using WebApplication.Contracts.SortContracts;

namespace WebApplication.Services.SortServices
{
    public class SortServiceWrapper : ISortServiceWrapper
    {
        private IRamSortService _ramSortService;
        private ICpuSortService _cpuSortService;
        private IPowerSupplySortService _powerSupplySortService;
        private IHddSortService _hddSortService;
        private IMotherboardSortService _motherboardSortService;
        private IGraphicsCardSortService _graphicsCardSortService;

        public IRamSortService RamSortService
        {
            get { return _ramSortService ??= new RamSortService(); }
            set => throw new System.NotImplementedException();
        }

        public ICpuSortService CpuSortService
        {
            get { return _cpuSortService ??= new CpuSortService(); }
            set => throw new System.NotImplementedException();
        }

        public IPowerSupplySortService PowerSupplySortService
        {
            get { return _powerSupplySortService ??= new PowerSupplyService(); }
            set => throw new System.NotImplementedException();
        }

        public IHddSortService HddSortService
        {
            get { return _hddSortService ??= new HddSortService(); }
            set => throw new System.NotImplementedException();
        }

        public IMotherboardSortService MotherboardSortService
        {
            get { return _motherboardSortService ??= new MotherboardSortService(); }
            set => throw new System.NotImplementedException();
        }

        public IGraphicsCardSortService GraphicsCardSortService
        {
            get { return _graphicsCardSortService ??= new GraphicsCardSortService(); }
            set => throw new System.NotImplementedException();
        }
    }
}
