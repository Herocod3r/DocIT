using System;
using System.IO;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
namespace DocIT.Core.Services.Git
{
    public interface IGitResolver
    {
        Task<string> GetFileIdentifier(GitResolverItem item);
        Task<Stream> GetFileData(string fileIdentifier);
    }
}
