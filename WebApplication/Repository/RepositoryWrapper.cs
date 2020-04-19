using WebApplication.Contracts;
using WebApplication.Data;

namespace WebApplication.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationContext _applicationContext;
        private IProductRepository _productRepository;
        private IRamRepository _ramRepository;
        private IPowerSupplyRepository _powerSupplyRepository;
        private IGraphicsCardRepository _graphicsCardRepository;
        private ICpuRepository _cpuRepository;

        public IProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_applicationContext); }
            set => throw new System.NotImplementedException();
        }

        public IRamRepository RamRepository
        {
            get { return _ramRepository ??= new RamRepository(_applicationContext); }
            set => throw new System.NotImplementedException();
        }

        public IPowerSupplyRepository PowerSupplyRepository
        {
            get { return _powerSupplyRepository ??= new PowerSupplyRepository(_applicationContext); }
            set => throw new System.NotImplementedException();
        }

        public IGraphicsCardRepository GraphicsCardRepository
        {
            get { return _graphicsCardRepository ??= new GraphicsCardRepository(_applicationContext); }
            set => throw new System.NotImplementedException();
        }

        public ICpuRepository CpuRepository
        {
            get { return _cpuRepository ??= new CpuRepository(_applicationContext); }
            set => throw new System.NotImplementedException();
        }

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Save()
        {
            _applicationContext.SaveChangesAsync();
        }
    }
}
