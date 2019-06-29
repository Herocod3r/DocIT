using System;
using System.Threading.Tasks;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Repositories;
using DocIT.Core.Services.Exceptions;
using System.Linq;
using System.IO;

namespace DocIT.Core.Services.Implementations
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IProjectRepository repository;

        public ProjectService(IProjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProjectViewModel> CreateProject(ProjectPayload payload, Guid userId)
        {
            var project = this.Map<Project, ProjectPayload>(payload);
            project.DateCreated = DateTime.Now;
            project.CreatedByUserId = userId;
            project.PreviewLinks = new System.Collections.Generic.List<string>();
            await Task.Run(()=> this.repository.CreateNew(project));
            var item = this.repository.QueryAsync().FirstOrDefault(x => x.Id == project.Id);
            if (item is null) throw new ProjectException("Failed to create project, please check back later");
            return this.Map<ProjectViewModel,ProjectListItem>(item);

        }

        public async Task DeleteProject(Guid id, Guid userId)
        {
            var project = this.repository.GetById(id);
            if (project?.CreatedByUserId != userId) throw new ProjectException("Project not found");
            await Task.Run(() => this.repository.Delete(project));
        }

        public async Task<ProjectViewModel> DeleteProjectLink(Guid projectId, Guid userId, string link)
        {
            var project = this.repository.ObjectQuery.FirstOrDefault(x => x.Id == projectId && x.CreatedByUserId == userId);
            if (project is null) throw new ProjectException("Project not found");
            var index = project.PreviewLinks.IndexOf(link);
            if(index < 0) throw new ProjectException("Project link does not exist");
            project.PreviewLinks.RemoveAt(index);
            this.repository.Update(project);
            return this.Map<ProjectViewModel, ProjectListItem>(this.repository.QueryAsync().FirstOrDefault(x => x.Id == project.Id));

        }

        public async Task<ProjectViewModel> GenerateProjectLink(Guid projectId,Guid userId)
        {
            var project = this.repository.ObjectQuery.FirstOrDefault(x => x.Id == projectId && x.CreatedByUserId == userId);
            if (project is null) throw new ArgumentException("Unable to find the project");
            var link = await Task.Run(() => GenerateUniqueLink());
           
            if (string.IsNullOrEmpty(link)) throw new ProjectException("Unable to generate new link, please try again later");
            project.PreviewLinks.Add(link);
            this.repository.Update(project);
            return this.Map<ProjectViewModel, ProjectListItem>(this.repository.QueryAsync().FirstOrDefault(x => x.Id == project.Id));

        }

        private string GenerateUniqueLink()
        {
            var counter = 0;
            while (true)
            {
                if (counter >= 20) return null;
                var random = GetRandomString();
                var count = this.repository.ObjectQuery.Count(x => x.PreviewLinks.Any(c => c == random));
                if (count == 0)
                {
                    return random;
                }
                counter += 1;
            }
        }


        private string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }


        public async Task<ProjectViewModel> GetProject(Guid id, Guid userId, string email)
        {
            var project = this.repository.QueryAsync().FirstOrDefault(x => (x.CreatedByUserId == userId || x.Invites.Any(z => z.Email.ToLower() == email.ToLower())) && x.Id == id);
            if (project is null) throw new ArgumentException("Unable to find the project");
            return this.Map<ProjectViewModel, ProjectListItem>(project);

        }

        public async Task<ProjectViewModel> GetProjectByLink(string link, Guid userId, string email)
        {
             var project = await Task.Run(()=> this.repository.QueryAsync().FirstOrDefault(x => (x.Invites.Any(c=>c.Email.ToLower() == email.ToLower()) || x.CreatedByUserId == userId) && x.PreviewLinks.Any(c=>c == link)));
            if (project is null) throw new ArgumentException("Unable to find the project");
            return this.Map<ProjectViewModel, ProjectListItem>(project);
        }

        public async Task<ListViewModel<ProjectViewModel>> ListAll(Guid userId, string email, string query, string orderBy, int skip, int limit)
        {
            var (items,count) = await Task.Run(()=> this.repository.GetAllForUser(skip, limit, userId, email, query));
            return new ListViewModel<ProjectViewModel>
            {
                Total = count,
                Result = items.Select(x => Map<ProjectViewModel, ProjectListItem>(x)).ToList()
            };
        }

        public async Task<ListViewModel<ProjectViewModel>> ListSubProjects(Guid parentId, Guid userId,string email)
        {
            var projects = await Task.Run(() => this.repository.QueryAsync().Where(x => (x.CreatedByUserId == userId || x.Invites.Any(z => z.Email.ToLower() == email.ToLower())) && x.Id == parentId).ToList());
            return new ListViewModel<ProjectViewModel>
            {
                Total = projects.Count,
                Result = projects.Select(x => Map<ProjectViewModel, ProjectListItem>(x)).ToList()
            };
        }

        public async Task<ProjectViewModel> UpdateProject(UpdateProjectPayload payload, Guid projectId, Guid userId)
        {
            var project = this.repository.ObjectQuery.FirstOrDefault(x => x.Id == projectId);
            if (project is null) throw new ArgumentException("Unable to find the master project");
            project.Name = payload.Name;
            project.Description = payload.Description;
            project.SwaggerUrl = payload.SwaggerUrl;
            this.repository.Update(project);
            return this.Map<ProjectViewModel, ProjectListItem>(this.repository.QueryAsync().FirstOrDefault(x => x.Id == project.Id));

        }

        public async Task<ProjectViewModel> GetProjectWithoutCredential(string link)
        {
            var project = await Task.Run(() => this.repository.QueryAsync().FirstOrDefault(x => x.PreviewLinks.Any(c => c == link)));
            if (project is null) throw new ArgumentException("Unable to find the project");
            return this.Map<ProjectViewModel, ProjectListItem>(project);
        }
    }
}
