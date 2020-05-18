using DataProvider.Data;
using DataProvider.Interfaces;
using Models.Authentication;

namespace DataProvider.Repository
{
    public class VerificationCodeRepository : RepositoryBase<VerificationCode>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(ApplicationContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
