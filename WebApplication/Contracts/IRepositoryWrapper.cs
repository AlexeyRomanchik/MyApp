namespace WebApplication.Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; set; }
        IRamRepository RamRepository { get; set; }
        IPowerSupplyRepository PowerSupplyRepository { get; set; }
        IGraphicsCardRepository GraphicsCardRepository { get; set; }
        ICpuRepository CpuRepository { get; set; }
        IHddRepository HddRepository { get; set; }
        IMotherboardRepository MotherboardRepository { get; set; }
        IManufacturerRepository ManufacturerRepository { get; set; }
        IRatingRepository RatingRepository { get; set; }
        IReviewRepository ReviewRepository { get; set; }
        IOrderRepository OrderRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IVerificationCodeRepository VerificationCodeRepository { get; set; }
        ICartItemRepository CartItemRepository { get; set; }
        void Save();
    }
}