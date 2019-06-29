using System;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using System.Threading.Tasks;
using System.IO;

namespace DocIT.Core.Services
{
    public interface IGitConfigurationService
    {
        ListViewModel<GitConfigViewModel> ListAllForUser(Guid userId);
        Task<GitConfigViewModel> CreateNewAsync(Guid userId, GitConfigPayload payload);
        Task DeleteAsync(Guid userId, Guid itemId);
        Task<GitConfigViewModel> GetById(Guid id, Guid userId);
        Task<GitConfigViewModel> UpdateAsync(Guid userId, Guid itemId,GitConfigPayload payload);
        Task<string> GetTokenForProject(GitTokenPayload payload);
        Task<Stream> GetProjectSwaggerFileFromToken(string token,string type);
    }
}
