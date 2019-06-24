using System;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using System.Threading.Tasks;

namespace DocIT.Core.Services
{
    public interface IProjectService
    {
        Task<ListViewModel<ProjectViewModel>> ListAll(Guid userId,string email, string query,string orderBy);
        Task<ListViewModel<ProjectViewModel>> ListSubProjects(Guid parentId);
        Task<ProjectViewModel> GetProject(Guid id,Guid? userId,string email);
        Task<ProjectViewModel> CreateProject(ProjectPayload payload,Guid userId);
        Task<ProjectViewModel> UpdateProject(UpdateProjectPayload payload,Guid userId);
        Task<ProjectViewModel> GetProjectByLink(string link, Guid userId,string email);
        Task<ProjectViewModel> GenerateProjectLink(string link);
        Task<ProjectViewModel> DeleteProjectLink(string link);
        Task DeleteProject(Guid id,Guid userId); 
    }
}
