
namespace WebApplication.Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; set; }
        IRamRepository RamRepository { get; set; }
        IPowerSupplyRepository PowerSupplyRepository { get; set; }
        IGraphicsCardRepository GraphicsCardRepository  { get; set; }
        ICpuRepository CpuRepository { get; set; }
        void Save();
    }
}
