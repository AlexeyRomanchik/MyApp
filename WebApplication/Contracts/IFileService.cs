using Microsoft.AspNetCore.Http;

namespace WebApplication.Contracts
{
    public interface IFileService
    {
        void SaveUploadedFile(IFormFile uploadedFile);
    }
}