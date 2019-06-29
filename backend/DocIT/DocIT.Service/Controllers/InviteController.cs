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
    [Route("[controller]")]
    [Authorize]
    public class InviteController : BaseController
    {
        private readonly IInviteService inviteService;

        public InviteController(IInviteService inviteService)
        {
            this.inviteService = inviteService;
        }



        [HttpGet]
        public  async Task<IActionResult> Get(int skip = 0, int limit = 30) => Ok(await inviteService.GetUserInvites(this.UserEmailAddress, skip, limit));

       

        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]InvitePayload value)
        {
            try
            {
                return Ok(await inviteService.CreateInvite(value, this.UserId));
            }
            catch (InviteException ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteInvitePayload payload)
        {
            try
            {
                await inviteService.DeleteInvite(payload, this.UserId);
                return Ok();
            }
            catch (InviteException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
