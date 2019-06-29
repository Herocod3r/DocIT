using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DocIT.Core.Services;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Services.Exceptions;


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
        
        [HttpGet]
        public async Task<IActionResult> GetAll(string query = "", int skip = 0, int limit = 30) => Ok(await service.ListAll(this.UserId, this.UserEmailAddress, query,"", skip, limit));

        [HttpGet("parent/{id}")]
        public async Task<IActionResult> GetChildren(Guid id) => Ok(await service.ListSubProjects(id, this.UserId, this.UserEmailAddress));
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
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

       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProjectPayload value)
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

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]UpdateProjectPayload value)
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

        [HttpPost("link/{id}")]
        public async Task<IActionResult> PostLink(Guid id)
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

        [AllowAnonymous]
        [HttpGet("render/{link}")]
        public async Task<IActionResult> RenderProject(string link)
        {
            try
            {
                var project = await service.GetProjectWithoutCredential(link);
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
