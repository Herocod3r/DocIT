using System;
using DocIT.Core.Services.Exceptions;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Services;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace DocIT.Service.Services
{
    public class FileUploader : IFileUploader
    {
        private readonly string rootPath;
        public FileUploader(IHostingEnvironment hostingEnv)
        {
            rootPath = hostingEnv.WebRootPath;
        }

        public async Task<string> UploadFileAsync(Stream stream, string ext)
        {
            var path = Path.Combine(rootPath, $"{GetRandomName()}.{ext.Replace(".","")}");
            using (var streamWrite = File.Open(path,FileMode.OpenOrCreate,FileAccess.ReadWrite))
            {
                await stream.CopyToAsync(streamWrite);
            }
            return path;
        }

        public Task<string> UploadFileAsync(byte[] file, string ext)
        {
            var ms = new MemoryStream(file);
            return UploadFileAsync(ms,ext);
        }

        private string GetRandomName() => Path.GetRandomFileName().Replace(".", "");
    }
}
