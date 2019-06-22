using System;
using DocIT.Core.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace DocIT.Core.Repositories
{
    public interface ITeamInviteRepository : IBaseRepository<ProjectTeamInvite,ProjectTeamInviteItem,Guid>
    {
       (List<ProjectTeamInviteItem>,long) GetUserInvites(Guid userId, int skip, int limit = 30);
       (List<Project>,long) GetUserInvitedProjects(Guid userId,int skip,int limit = 30);
       (List<Project>, long) GetUserProjectsShared(Guid userId, int skip, int limit = 30);
    }
}
