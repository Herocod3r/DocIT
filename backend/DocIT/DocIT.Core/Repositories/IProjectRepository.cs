using System;
using DocIT.Core.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace DocIT.Core.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project,ProjectListItem,Guid>
    {
        (List<ProjectListItem>,long) GetAllForUser(int skip,int limit,Guid userId,string email = "",string query = "");
       ProjectListItem GetSingleForUser(Guid id, Guid userId,string email);
        List<ProjectListItem> GetAllSubProjects(Guid projectId);
    }
}
