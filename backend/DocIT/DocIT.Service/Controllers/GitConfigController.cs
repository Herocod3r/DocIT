using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DocIT.Core.Services;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;
using DocIT.Core.Services.Exceptions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocIT.Service.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class GitConfigController : BaseController
    {
        private readonly IGitConfigurationService service;

        public GitConfigController(IGitConfigurationService service)
        {
            this.service = service;
        }

       /// <summary>
       /// Fetches all git configurations for a user
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        public ActionResult<ListViewModel<GitConfigViewModel>> Get()
        {
            return Ok(service.ListAllForUser(this.UserId));
        }

        /// <summary>
        /// gets a single git configuration for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GitConfigViewModel>> Get(Guid id)
        {
            try
            {
                return Ok(await service.GetById(id, this.UserId));
            }
            catch (GitConfigException ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Takes a Git token payload and generates a token for getting a file from a github repo
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<ActionResult<string>> PostTokenPayload([FromBody] GitTokenPayload payload)
        {
            try
            {
                return Ok(await service.GetTokenForProject(payload));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (GitResolverException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        

        /// <summary>
        /// This endpoint creates a git configuration object for getting a file from a users github account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GitConfigViewModel>> Post([FromBody]GitConfigPayload value)
        {
            try
            {

                return Ok(await service.CreateNewAsync(this.UserId, value));
            }
            catch (GitConfigException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<GitConfigViewModel>> Put(Guid id, [FromBody]GitConfigPayload value)
        {
            try
            {

                return Ok(await service.UpdateAsync(this.UserId,id, value));
            }
            catch (GitConfigException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await service.DeleteAsync(this.UserId, id);
                return Ok();
            }
            catch (GitConfigException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
