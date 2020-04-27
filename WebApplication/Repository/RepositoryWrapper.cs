using System;
using WebApplication.Contracts;
using WebApplication.Data;

namespace WebApplication.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly ApplicationContext _applicationContext;
        private ICpuRepository _cpuRepository;
        private IGraphicsCardRepository _graphicsCardRepository;
        private IHddRepository _hddRepository;
        private IMotherboardRepository _motherboardRepository;
        private IPowerSupplyRepository _powerSupplyRepository;
        private IProductRepository _productRepository;
        private IRamRepository _ramRepository;
        private IManufacturerRepository _manufacturerRepository;

        public RepositoryWrapper(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ??= new ProductRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IRamRepository RamRepository
        {
            get { return _ramRepository ??= new RamRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IPowerSupplyRepository PowerSupplyRepository
        {
            get { return _powerSupplyRepository ??= new PowerSupplyRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IGraphicsCardRepository GraphicsCardRepository
        {
            get { return _graphicsCardRepository ??= new GraphicsCardRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public ICpuRepository CpuRepository
        {
            get { return _cpuRepository ??= new CpuRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IHddRepository HddRepository
        {
            get { return _hddRepository ??= new HddRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IMotherboardRepository MotherboardRepository
        {
            get { return _motherboardRepository ??= new MotherboardRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IManufacturerRepository ManufacturerRepository
        {
            get { return _manufacturerRepository ??= new ManufacturerRepository(_applicationContext);}
            set => throw new NotImplementedException();
        }

        public void Save()
        {
            _applicationContext.SaveChanges();
        }
    }
}