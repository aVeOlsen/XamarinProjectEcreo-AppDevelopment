﻿using EcreoLibraryStandard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinEcreo.Common;
using XamarinEcreo.Models;

namespace XamarinEcreo.Services
{
    public class ImageDbService : IImageDbService
    {

        static HttpClient _client;
        private ObservableRangeCollection<User> users { get; set; }
        private ObservableRangeCollection<UserGetModel> usersGet { get; set; }
        public ImageDbService()
        {

            _client = new HttpClient();
        }

        public async Task UploadImageAsync(Stream image, string fileName)
        {
            //Crypto.Encrypt("ImageFile",fileName);
            Uri uri = new Uri(String.Format(Constants.ImageRestUrl, String.Empty));
            HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await client.PostAsync(uri, formData);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(response);
                }
            }
        }
        public async Task<byte[]> GetImage(string filename)
        {
            //Crypto.Decrypt("ImageFile", stream);
            Uri uri = new Uri(String.Format(Constants.ImageRestUrl, filename));
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine(response);
            }
            var stream = await response.Content.ReadAsByteArrayAsync();
            return stream;

        }



    }
}
