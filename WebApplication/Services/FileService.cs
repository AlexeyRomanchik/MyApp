using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using WebApplication.Contracts;

namespace WebApplication.Services
{
    public class FileService : IFileService
    {
        public async void SaveUploadedFile(IFormFile uploadedFile, string path)
        {
            if (uploadedFile == null) throw new ArgumentNullException(nameof(uploadedFile));

            await using var fileStream = new FileStream(path, FileMode.Create);
            uploadedFile.CopyTo(fileStream);
        }
    }
}