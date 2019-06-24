using System;
using System.Collections.Generic;
using DocIT.Core.Data.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DocIT.Core.Repositories.Implementations
{
    public class ProjectRepository : BaseRepository<Project,ProjectListItem,Guid>, IProjectRepository
    {
        public ProjectRepository(IMongoDatabase db) : base(db)
        {
        }

        public (List<ProjectListItem>, long) GetAllForUser(int skip, int limit, Guid userId, string email = "", string query = "")
        {
            var queryResult = ProjectedSource.Where(x => (x.CreatedByUserId == userId || x.Invites.Any(z=>z.Email.ToLower() == email.ToLower())) && x.Name.ToLower().Contains(query)).OrderByDescending(x=>x.DateCreated);
            return (queryResult.Skip(skip).Take(limit).ToList(), queryResult.Count());
        }

        public ProjectListItem GetSingleForUser(Guid id, Guid userId, string email)
        {
            return  ProjectedSource.Where(x => x.Id == id && x.CreatedByUserId == userId).FirstOrDefault();
        }

        public List<ProjectListItem> GetAllSubProjects(Guid projectId)
        {
            return ProjectedSource.Where(x => x.ParentId == projectId).ToList();
        }

        protected override IQueryable<ProjectListItem> ProjectedSource => Collection.AsQueryable().GroupJoin(Collection.AsQueryable(), (arg) => arg.Id, (arg) => arg.ParentId, (from, to) => new ProjectListItem { CreatedByUserId = from.CreatedByUserId, Id = from.Id, Name = from.Name, ParentId = from.ParentId, DateCreated = from.DateCreated, DateDeleted = from.DateDeleted, Description = from.Description, NoOfSubProjects = to.Count(), PreviewLinks = from.PreviewLinks, SwaggerUrl = from.SwaggerUrl, Invites = from.Invites });

        
    }
}
