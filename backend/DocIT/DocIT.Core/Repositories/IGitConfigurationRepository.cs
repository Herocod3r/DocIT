using System;
using DocIT.Core.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace DocIT.Core.Repositories
{
    public interface IGitConfigurationRepository : IBaseRepository<GitConnectionConfig,GitConnectionConfig,Guid>
    {
        Task<(List<GitConnectionConfig>, Int64)> GetAllForUser(Guid userId);
    }
}
