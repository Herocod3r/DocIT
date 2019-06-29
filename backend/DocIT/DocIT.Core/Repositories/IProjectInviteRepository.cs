using System;
using System.Collections.Generic;
using DocIT.Core.Data.Models;

namespace DocIT.Core.Repositories
{
    public interface IProjectInviteRepository : IBaseRepository<Project,InviteItem,Guid>
    {
        (List<InviteItem>,long) GetUserInvites(string email,int skip,int limit);
        Invite GetInviteByEmail(string email,Guid projectId);
        InviteItem CreateInvite(Invite invite,Guid projectId,Guid userId);
        void DeleteInvite(Invite invite, Guid projectId, Guid userId);
    }
}
