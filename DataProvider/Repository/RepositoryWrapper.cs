﻿using System;
using DataProvider.Data;
using DataProvider.Interfaces;

namespace DataProvider.Repository
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
        private IRatingRepository _ratingRepository;
        private IReviewRepository _reviewRepository;
        private IOrderRepository _orderRepository;
        private IUserRepository _userRepository;
        private IVerificationCodeRepository _verificationCodeRepository;
        private ICartItemRepository _cartItemRepository;
        private IUserFavoriteProductsRepository _userFavoriteProductsRepository;

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

        public IRatingRepository RatingRepository
        {
            get { return _ratingRepository ??= new RatingRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IOrderRepository OrderRepository
        {
            get { return _orderRepository ??= new OrderRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }
        public IReviewRepository ReviewRepository
        {
            get { return _reviewRepository ??= new ReviewRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ??= new UserRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IVerificationCodeRepository VerificationCodeRepository
        {
            get { return _verificationCodeRepository ??= new VerificationCodeRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }
        public ICartItemRepository CartItemRepository
        {
            get { return _cartItemRepository ??= new CartItemRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public IUserFavoriteProductsRepository UserFavoriteProductsRepository
        {
            get { return _userFavoriteProductsRepository ??= new UserFavoriteProductsRepository(_applicationContext); }
            set => throw new NotImplementedException();
        }

        public void Save()
        {
            _applicationContext.SaveChanges();
        }
    }
}