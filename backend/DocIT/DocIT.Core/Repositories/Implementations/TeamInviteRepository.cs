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
    public class TeamInviteRepository : BaseRepository<ProjectTeamInvite,ProjectTeamInviteItem,Guid>,ITeamInviteRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IProjectRepository projectRepository;
        private readonly IQueryable<ProjectTeamInviteItem> internalQuery;

        public TeamInviteRepository(IMongoDatabase database,IUserRepository userRepository,IProjectRepository projectRepository) : base(database)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
            internalQuery = this.GetQuery();
        }

        public (List<Project>, long) GetUserInvitedProjects(Guid userId, int skip, int limit = 30)
        {
            var query = ProjectedSource.Where(x => x.Invited.Id == userId).OrderByDescending(x => x.Invite.InvitedAt);
            return (query.Skip(skip).Take(limit).Select(x => x.Project).ToList(), query.Count());
        }

        public (List<ProjectTeamInviteItem>, long) GetUserInvites(Guid userId, int skip, int limit = 30)
        {
            var query = ProjectedSource.Where(x => x.Inviter.Id == userId).OrderByDescending(x => x.Invite.InvitedAt);
            return (query.Skip(skip).Take(limit).ToList(), query.Count());
        }

        public (List<Project>, long) GetUserProjectsShared(Guid userId, int skip, int limit = 30)
        {
            var query = ProjectedSource.Where(x => x.Inviter.Id == userId).OrderByDescending(x => x.Invite.InvitedAt);
            return (query.Skip(skip).Take(limit).Select(x => x.Project).ToList(), query.Count());
        }
        protected override IQueryable<ProjectTeamInviteItem> ProjectedSource => internalQuery;
        private IQueryable<ProjectTeamInviteItem> GetQuery()
        {
            var query = Collection.AsQueryable().Join(userRepository.QueryAsync(), (arg) => arg.Email, (arg) => arg.Email, (arg1, arg2) => new ProjectTeamInviteItem { Invite = arg1, Invited = arg2 }).
                 Join(userRepository.QueryAsync(), (arg) => arg.Invite.InvitedByUserId, (arg) => arg.Id, (arg1, arg2) => new ProjectTeamInviteItem { Invite = arg1.Invite, Invited = arg1.Invited, Inviter = arg2 }).
                 Join(projectRepository.QueryAsync(), (arg) => arg.Invite.ProjectId, (arg) => arg.Id, (arg1, arg2) => new ProjectTeamInviteItem { Invite = arg1.Invite, Invited = arg1.Invited, Inviter = arg1.Inviter, Project = arg2 });
            return query;
        }
    }
}
