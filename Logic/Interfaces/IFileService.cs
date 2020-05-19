using Microsoft.AspNetCore.Http;

namespace Logic.Interfaces
{
    public interface IFileService
    {
        void SaveUploadedFile(IFormFile uploadedFile, string path);
        void DeleteFile(string path);
    }
}