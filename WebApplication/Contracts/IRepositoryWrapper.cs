
namespace WebApplication.Contracts
{
    public interface IRepositoryWrapper
    {
        IProductRepository ProductRepository { get; set; }
        IRamRepository RamRepository { get; set; }
        void Save();
    }
}
