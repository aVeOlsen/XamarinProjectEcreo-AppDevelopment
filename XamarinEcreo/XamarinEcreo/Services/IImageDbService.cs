﻿using System.IO;
using System.Threading.Tasks;

namespace XamarinEcreo.Services
{
    public interface IImageDbService
    {
        Task<byte[]> GetImage(string filename);
        Task UploadImageAsync(Stream image, string fileName);
    }
}