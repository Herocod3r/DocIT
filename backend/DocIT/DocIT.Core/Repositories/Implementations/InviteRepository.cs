using System;
using System.Collections.Generic;
using System.Linq;
using DocIT.Core.Data.Models;
using MongoDB.Driver;

namespace DocIT.Core.Repositories.Implementations
{
    public class InviteRepository : ProjectRepository, IProjectInviteRepository
    {
        private readonly IUserRepository userRepository;

        public InviteRepository(IMongoDatabase db,IUserRepository userRepository): base(db)
        {
            this.userRepository = userRepository;
        }

        public InviteItem CreateInvite(Invite invite, Guid projectId,Guid userId)
        {
            var project = ObjectQuery.Where(x => x.Id == projectId && x.CreatedByUserId == userId).FirstOrDefault();
            if (project is null) throw new ArgumentException("Project does not exist");
            if (project.Invites?.Any(x => x.Email == invite.Email) == true) throw new InvalidOperationException("Project already contains invite");
            if (project.Invites is null) project.Invites = new List<Invite> { invite };
            else project.Invites.Add(invite);

            base.Update(project);
            var item = QueryAsync().FirstOrDefault(x => x.ProjectId == projectId);
            item.Invites?.RemoveAll(x => x.Email.ToLower() != invite.Email.ToLower());
            return item;
        }

        public void DeleteInvite(Invite invite, Guid projectId, Guid userId)
        {
            var project = ObjectQuery.FirstOrDefault(x => x.Id == projectId && x.CreatedByUserId == userId);
            if (project is null) throw new ArgumentException("Project does not exist");
            if (!(project.Invites?.Any(x => x.Email == invite.Email) == true)) throw new InvalidOperationException("Project does not contain invite");
            project.Invites.RemoveAt(project.Invites.FindIndex(x => x.Email == invite.Email));
            base.Update(project);
        }

        public Invite GetInviteByEmail(string email, Guid projectId)
        {
            var project = ObjectQuery.FirstOrDefault(x => x.Id == projectId);
            if (project is null) return null;
            return project.Invites?.FirstOrDefault(x => x.Email == email);
        }

        public (List<InviteItem>, long) GetUserInvites(string email, int skip, int limit)
        {
            var items = QueryAsync().Where(x => x.Invites.Any(v => v.Email.ToLower() == email.ToLower())).OrderByDescending(x=>x.Date).Skip(skip).Take(limit).ToList();
            items.ForEach(x=>x.Invites?.RemoveAll(y=>y.Email.ToLower() != email.ToLower()));
            return (items, ObjectQuery.Count(x => x.Invites.Any(v => v.Email.ToLower() == email.ToLower())));
        }

        public new IQueryable<InviteItem> QueryAsync() => base.ObjectQuery.Join(userRepository.ObjectQuery, a => a.CreatedByUserId, x => x.Id, (a, b) => new InviteItem { Date = a.DateCreated, InviterName = b.Name, Invites = a.Invites, ProjectId = a.Id, ProjectName = a.Name  });
    }
}
