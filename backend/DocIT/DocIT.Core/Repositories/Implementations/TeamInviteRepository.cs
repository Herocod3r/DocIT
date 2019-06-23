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
            //var query = ProjectedSource.Where(x => x.InvitedUserId == userId);
            //return (query.Skip(skip).Take(limit).Select(x => x.Project).ToList(), query.Count());
            return (null, 0);
        }

        public (List<ProjectTeamInviteItem>, long) GetUserInvites(Guid userId, int skip, int limit = 30)
        {
            //var query = ProjectedSource.Where(x => x.Inviter.Id == userId).OrderByDescending(x => x.Invite.InvitedAt);
            //return (query.Skip(skip).Take(limit).ToList(), query.Count());
            return (null, 0);
        }

        public (List<Project>, long) GetUserProjectsShared(Guid userId, int skip, int limit = 30)
        {
            //var query = ProjectedSource.Join(projectRepository.QueryAsync(),(arg) => ;).Where(x => x.Inviter.Id == userId).OrderByDescending(x => x.Invite.InvitedAt);
            //return (query.Skip(skip).Take(limit).Select(x => x.Project).ToList(), query.Count());
            return (null, 0);
        }
        protected override IQueryable<ProjectTeamInviteItem> ProjectedSource => internalQuery;
        private IQueryable<ProjectTeamInviteItem> GetQuery()
        {
            try
            {
                var query = from x in this.Collection.AsQueryable()
                                join y in this.userRepository.QueryAsync()
                                on x.Email equals y.Email
                            //join z in this.projectRepository.QueryAsync()
                            //on x.ProjectId equals z.Id
                            select new { x, y };

                var v = query.FirstOrDefault();
            }
            catch (Exception ex)
            {

            }
           
            //var query = Collection.AsQueryable().Join(userRepository.QueryAsync(), (arg) => arg.Email, (arg) => arg.Email, (arg1, arg2) => new ProjectTeamInviteItem
            //{
            //    Accepted = arg1.Accepted,
            //    DateCreated = arg1.DateCreated,
            //    DateDeleted = arg1.DateDeleted,
            //    Email = arg1.Email,
            //    Id = arg1.Id,
            //    InvitedAt = arg1.InvitedAt,
            //    InvitedByUserId = arg1.InvitedByUserId,
            //    InvitedUserId = arg2.Id,
            //    ProjectId = arg1.ProjectId,
            //    InvitedUserName = arg2.Name
            //});
             //.Join(projectRepository.QueryAsync(),(arg) => arg.ProjectId,(arg) => arg.Id,(arg1, arg2) => new ProjectTeamInviteItem {

             //    Accepted = arg1.Accepted,
             //    DateCreated = arg1.DateCreated,
             //    DateDeleted = arg1.DateDeleted,
             //    Email = arg1.Email,
             //    Id = arg1.Id,
             //    InvitedAt = arg1.InvitedAt,
             //    InvitedByUserId = arg1.InvitedByUserId,
             //    InvitedUserId = arg1.InvitedByUserId,
             //    ProjectId = arg1.ProjectId,
             //    InvitedUserName = arg1.InvitedUserName,
             //    InviteeUserName = arg1.InviteeUserName,
             //    ProjectName = arg2.Name
             //});
                 //Join(userRepository.QueryAsync(), (arg) => arg.Invite.InvitedByUserId, (arg) => arg.Id, (arg1, arg2) => new ProjectTeamInviteItem { Invite = arg1.Invite, Invited = arg1.Invited, Inviter = arg2 });
                //Join(projectRepository.QueryAsync(), (arg) => arg.Invite.ProjectId, (arg) => arg.Id, (arg1, arg2) => new ProjectTeamInviteItem { Invite = arg1.Invite, Invited = arg1.Invited, Inviter = arg1.Inviter, Project = arg2 }).
            return null;
        }
    }
}
