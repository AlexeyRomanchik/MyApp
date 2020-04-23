using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using WebApplication.Contracts;

namespace WebApplication.Services
{
    public class FileService : IFileService
    {
        private readonly string _path;

        public FileService(string path)
        {
            _path = path;
        }

        public async void SaveUploadedFile(IFormFile uploadedFile)
        {
            if (uploadedFile == null) throw new ArgumentNullException(nameof(uploadedFile));

            await using var fileStream = new FileStream(_path, FileMode.Create);
            uploadedFile.CopyTo(fileStream);
        }
    }
}
