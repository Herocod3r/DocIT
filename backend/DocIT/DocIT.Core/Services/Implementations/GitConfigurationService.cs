using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Repositories;
using DocIT.Core.Services.Exceptions;
using System.Linq;
using System.IO;

namespace DocIT.Core.Services.Implementations
{
    public class GitConfigurationService : BaseService, IGitConfigurationService
    {
        private readonly IGitConfigurationRepository repository;

        public GitConfigurationService(IGitConfigurationRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GitConfigViewModel> CreateNewAsync(Guid userId, GitConfigPayload payload)
        {
            var item = this.Map<GitConnectionConfig, GitConfigPayload>(payload);
            item.UserId = userId;
            item = await Task.Run(() => repository.CreateNew(item));

            var result = Map<GitConfigViewModel, GitConnectionConfig>(item);
            return result;
        }

        public async Task DeleteAsync(Guid userId, Guid itemId)
        {
            var item = await Task.Run(() => repository.GetById(itemId));
            if (item is null || item.UserId != userId) throw new GitConfigException("Couldnt find item");
            await Task.Run(() => repository.Delete(item));
        }

        public async Task<GitConfigViewModel> GetById(Guid id, Guid userId)
        {
            var item = await Task.Run(() => repository.GetById(id));
            if (item is null || item.UserId != userId) throw new GitConfigException("Couldnt find item");
            var result = Map<GitConfigViewModel, GitConnectionConfig>(item);
            return result;
        }

        public async Task<Stream> GetProjectSwaggerFileFromToken(string token, string type)
        {
            var resolver = Git.GitFactory.GetResolver(type);
            return await resolver.GetFileData(token);
        }

        public Task<string> GetTokenForProject(GitTokenPayload payload)
        {
            var config = this.repository.GetById(payload.GitConfigId);
            if (config is null) throw new ArgumentException("Git configuration not found");
            var resolver = Git.GitFactory.GetResolver(config.Type);
            return resolver.GetFileIdentifier(new GitResolverItem { Branch = payload.Branch, FilePath = payload.GitPathToFile, GitConnection = config, RepoName = payload.GitRepositoryName });

        }

        public ListViewModel<GitConfigViewModel> ListAllForUser(Guid userId)
        {

            return new ListViewModel<GitConfigViewModel>
            {
                Result = repository.QueryAsync().Where(x => x.UserId == userId).ToList().Select(x => Map<GitConfigViewModel, GitConnectionConfig>(x)).ToList(),
                Total = repository.QueryAsync().Where(x => x.UserId == userId).Count()
            };
           
        }

        public async Task<GitConfigViewModel> UpdateAsync(Guid userId, Guid itemId, GitConfigPayload payload)
        {
            var item = await Task.Run(() => repository.GetById(itemId));
            if (item is null || item.UserId != userId) throw new GitConfigException("Couldnt find item");
            item.AccountName = payload.AccountName;
            item.PersonalToken = payload.PersonalToken;
            item.Type = payload.Type;
            await Task.Run(() => repository.Update(item));
            return Map<GitConfigViewModel, GitConnectionConfig>(item);
        }
    }
}
