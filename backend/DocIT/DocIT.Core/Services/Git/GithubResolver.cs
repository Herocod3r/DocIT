using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using System.Net.Http;
using DocIT.Core.Services.Exceptions;
using System.Net;
using System.IO;
using static DocIT.Core.Utils.StringCompressor;
using static DocIT.Core.Utils.StringEncryptor;

namespace DocIT.Core.Services.Git
{
    internal class GithubResolver : IGitResolver
    {
        public GithubResolver()
        {
        }

        public async Task<Stream> GetFileData(string fileIdentifier)
        {
            var url = Decrypt(DecompressString(fileIdentifier));
            return await GetFile(url);
        }

        private async Task<Stream> GetFile(string fileUrl)
        {
            
            var username = fileUrl.Substring(fileUrl.IndexOf('/') + 2, fileUrl.IndexOf('@') - (fileUrl.IndexOf('/') + 2));
            var callUrl = fileUrl.Replace(username+"@", "");
          
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:")));
                        var result = await client.GetStreamAsync(callUrl);
                   
                        return result;
                    }
                    catch (System.Net.Http.HttpRequestException)
                    {
                        throw new GitResolverException("Document not found, make sure your connection string is correct and your file path string is also correct");
                    }
                }
        }

        public async Task<string> GetFileIdentifier(GitResolverItem item)
        {
            var url = GetUrl(item);
            await GetFile(url);
            var encryptString = Encrypt(url);
            var compressedString = CompressString(encryptString);
            return compressedString;
        }

        private string GetUrl(GitResolverItem item) => $"https://{item.GitConnection.PersonalToken}@raw.githubusercontent.com/{item.GitConnection.AccountName}/{item.RepoName}/{item.Branch}/{item.FilePath.TrimStart('/')}";


    }
}
