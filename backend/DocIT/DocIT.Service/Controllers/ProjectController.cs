using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DocIT.Core.Services;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Services.Exceptions;
using DocIT.Core.Data.ViewModels;

namespace DocIT.Service.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectService service;
        private readonly IGitConfigurationService gitService;

        public ProjectController(IProjectService service,IGitConfigurationService gitService)
        {
            this.service = service;
            this.gitService = gitService;
        }

        /// <summary>
        /// Lists all projects for a user including projects he was invited to
        /// </summary>
        /// <param name="query">A project name to search for</param>
        /// <param name="skip">start showing from</param>
        /// <param name="limit">total record to return</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ListViewModel<ProjectViewModel>>> GetAll(string query = "", int skip = 0, int limit = 30) => Ok(await service.ListAll(this.UserId, this.UserEmailAddress, query,"", skip, limit));

        /// <summary>
        /// Lists all sub projects under a project
        /// </summary>
        /// <param name="id">The parent project id</param>
        /// <returns></returns>
        [HttpGet("parent/{id}")]
        public async Task<ActionResult<ListViewModel<ProjectViewModel>>> GetChildren(Guid id) => Ok(await service.ListSubProjects(id, this.UserId, this.UserEmailAddress));

        /// <summary>
        /// Returns a project by the link or ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectViewModel>> Get(string id)
        {
            try
            {
                Core.Data.ViewModels.ProjectViewModel project = null;
                if (Guid.TryParse(id, out Guid pid)) project = await service.GetProject(pid, this.UserId, this.UserEmailAddress);
                else project = await service.GetProjectByLink(id, this.UserId, this.UserEmailAddress);
                return Ok(project);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Creates a project, the Swagger Url should be gotten from either /file or /gitconfig/token this is generally
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProjectViewModel>> Post([FromBody]ProjectPayload value)
        {
            try
            {
                var result = await service.CreateProject(value, this.UserId);
                return Ok(result);
            }
            catch (ProjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updates a project, maybe name or replacing the SwaggerUrl etc
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectViewModel>> Put(Guid id, [FromBody]UpdateProjectPayload value)
        {
            try
            {
                var result = await service.UpdateProject(value, id, this.UserId);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProjectException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// This generates a preview link, with which a project can be viewed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("link/{id}")]
        public async Task<ActionResult<ProjectViewModel>> PostLink(Guid id)
        {
            try
            {
                var result = await service.GenerateProjectLink(id, this.UserId);
                return Ok(result);
            }
            catch (ProjectException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// This deletes a project's link
        /// </summary>
        /// <param name="id">the project id</param>
        /// <param name="link">the link</param>
        /// <returns></returns>
        [HttpDelete("link/{id}")]
        public async Task<IActionResult> DeleteLink(Guid id,string link)
        {
            try
            {
                if (string.IsNullOrEmpty(link)) return NotFound("Please provide the link in the query");
                await service.DeleteProjectLink(id, this.UserId,link);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="id">the project id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await service.DeleteProject(id, this.UserId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProjectException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// This renders the swagger.json file, you supply a preview link or Project Id
        /// </summary>
        /// <param name="identifier">preview link or project id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("render/{identifier}")]
        public async Task<IActionResult> RenderProject(string identifier)
        {

            try
            {
                ProjectViewModel project = null;
                if (Guid.TryParse(identifier, out Guid id)) project = await service.GetProject(id, this.UserId, this.UserEmailAddress);
                else project = await service.GetProjectWithoutCredential(identifier);
                if(Uri.TryCreate(project.SwaggerUrl,UriKind.Relative,out Uri uri))
                {
                    if (!System.IO.File.Exists(project.SwaggerUrl)) return NotFound("The project swagger url has expired, please reupload");
                    return PhysicalFile(project.SwaggerUrl, "application/json");
                }
                else
                {
                    var data = await gitService.GetProjectSwaggerFileFromToken(project.SwaggerUrl,"Github");
                    return File(data, "application/json");
                }
            }
            catch(GitResolverException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
