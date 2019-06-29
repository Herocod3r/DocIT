using System;
using System.Threading.Tasks;
using System.IO;

namespace DocIT.Core.Services
{
    public interface IFileUploader
    {
        Task<string> UploadFileAsync(Stream stream,string ext);
        Task<string> UploadFileAsync(byte[] file,string ext);
    }
}
