
namespace WebApplication.Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; set; }
        IRamRepository RamRepository { get; set; }
        IPowerSupplyRepository PowerSupplyRepository { get; set; }
        void Save();
    }
}
