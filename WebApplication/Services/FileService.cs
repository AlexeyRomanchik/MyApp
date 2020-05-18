using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using WebApplication.Interfaces;

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

        public void DeleteFile(string path)
        {
            var fileInfo = new FileInfo(path);
            if(fileInfo.Exists)
                fileInfo.Delete();
        }
    }
}