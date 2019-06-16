using System;
using DocIT.Core.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace DocIT.Core.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project,ProjectListItem,Guid>
    {
        (List<ProjectListItem>,long) GetAllForUser(int skip,int limit,Guid userId);
       ProjectListItem GetSingleForUser(Guid id, Guid userId);
        List<ProjectListItem> GetAllSubProjects(Guid projectId);
    }
}
