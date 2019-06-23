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

        public (List<ProjectListItem>, long) GetAllForUser(int skip, int limit, Guid userId)
        {
            var query = ProjectedSource.Where(x => x.CreatedByUserId == userId);
            return (query.Skip(skip).Take(limit).ToList(), query.Count());
        }

        public ProjectListItem GetSingleForUser(Guid id, Guid userId)
        {
            return  ProjectedSource.Where(x => x.Id == id && x.CreatedByUserId == userId).FirstOrDefault();
        }

        public List<ProjectListItem> GetAllSubProjects(Guid projectId)
        {
            return ProjectedSource.Where(x => x.ParentId == projectId).ToList();
        }

        protected override IQueryable<ProjectListItem> ProjectedSource => Collection.AsQueryable().GroupJoin(Collection.AsQueryable(), (arg) => arg.Id, (arg) => arg.ParentId, (from, to) => new ProjectListItem { CreatedByUserId = from.CreatedByUserId, Id = from.Id, Name = from.Name, ParentId = from.ParentId, DateCreated = from.DateCreated, DateDeleted = from.DateDeleted, Description = from.Description, NoOfSubProjects = to.Count(), PreviewLinks = from.PreviewLinks, SwaggerUrl = from.SwaggerUrl, Invites = from.Invites });

        //private IQueryable<ProjectListItem> BuildQuery()
        //{
        //    //MongoDB.Driver.Linq.
        //    //todo expose mongodb queryable translator
        //    var q = Collection.AsQueryable().GroupJoin(Collection.AsQueryable(), (arg) => arg.Id, (arg) => arg.ParentId, (from, to) => new ProjectListItem { CreatedByUserId = from.CreatedByUserId, Id = from.Id, Name = from.Name, ParentId = from.ParentId, DateCreated = from.DateCreated, DateDeleted = from.DateDeleted, Description = from.Description, NoOfSubProjects = to.Count, PreviewLinks = from.PreviewLinks, SwaggerUrl = from.SwaggerUrl  } );
        //    var x = new List<ProjectListItem>().AsQueryable<ProjectListItem>();


        //    var query = Collection.Aggregate().Lookup("Project", "_id", "ParentId", "Projects");
        //    var exr = new BsonDocument(new List<BsonElement>()
        //    {
        //        new BsonElement("NoOfSubProjects",new BsonDocument(new BsonElement("$size","Projects")))
        //    });
        //    BsonDocument addFields = new BsonDocument(new BsonElement("$addFields",exr));
        //    var result = query.AppendStage<BsonDocument>(addFields).Project(new BsonDocument(new BsonElement("Projects", 0))).As<ProjectListItem>();
        //    //result.tocu
        //    return null;
        //}
    }
}
