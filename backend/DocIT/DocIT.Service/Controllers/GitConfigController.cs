using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DocIT.Core.Services;
using DocIT.Core.Data.Payloads;
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

       
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(service.ListAllForUser(this.UserId));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
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

        [HttpPost("token")]
        public async Task<IActionResult> PostTokenPayload([FromBody] GitTokenPayload payload)
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


        


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GitConfigPayload value)
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
        public async Task<IActionResult> Put(Guid id, [FromBody]GitConfigPayload value)
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
