using System;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using System.Threading.Tasks;

namespace DocIT.Core.Services
{
    public interface IProjectService
    {
        Task<ListViewModel<ProjectViewModel>> ListAll(Guid userId, string query,string orderBy);
        Task<ListViewModel<ProjectViewModel>> ListSubProjects(Guid parentId);
        Task<ProjectViewModel> GetProject(Guid id,Guid? userId);
        Task<ProjectViewModel> CreateProject(ProjectPayload payload,Guid userId);
        Task<ProjectViewModel> UpdateProject(UpdateProjectPayload payload,Guid userId);
        Task DeleteProject(Guid id,Guid userId); 
    }
}
