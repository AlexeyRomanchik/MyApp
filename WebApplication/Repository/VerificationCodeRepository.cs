using WebApplication.Contracts;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Repository
{
    public class VerificationCodeRepository : RepositoryBase<VerificationCode>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
