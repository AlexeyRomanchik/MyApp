﻿using Microsoft.AspNetCore.Http;

namespace WebApplication.Contracts
{
    public interface IFileService
    {
        void SaveUploadedFile(IFormFile uploadedFile, string path);
        void DeleteFile(string path);
    }
}