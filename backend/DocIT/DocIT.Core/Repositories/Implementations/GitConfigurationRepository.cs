using System;
using System.Collections.Generic;
using DocIT.Core.Data.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Linq.Expressions;

namespace DocIT.Core.Repositories.Implementations
{
    public class GitConfigurationRepository : BaseRepository<GitConnectionConfig,GitConnectionConfig,Guid>, IGitConfigurationRepository
    {
        public GitConfigurationRepository(IMongoDatabase database) : base(database)
        {
        }

        public async System.Threading.Tasks.Task<(List<GitConnectionConfig>, long)> GetAllForUser(Guid userId)
        {
            var items = await Task.Run(()=> QueryAsync().Where(x => x.UserId == userId).OrderByDescending(x => x.DateCreated).ToList());
            return (items, items.Count);
        }

        protected override IQueryable<GitConnectionConfig> ProjectedSource => Collection.AsQueryable();
    }



}


