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
    [Route("[controller]")]
    [Authorize]
    public class InviteController : BaseController
    {
        private readonly IInviteService inviteService;

        public InviteController(IInviteService inviteService)
        {
            this.inviteService = inviteService;
        }


        /// <summary>
        /// Get a user invites, more like notifications
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public  async Task<ActionResult<ListViewModel<InviteViewModel>>> Get(int skip = 0, int limit = 30) => Ok(await inviteService.GetUserInvites(this.UserEmailAddress, skip, limit));

       

        /// <summary>
        /// Creates an invite on a project 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<InviteViewModel>> Post([FromBody]InvitePayload value)
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


       /// <summary>
       /// Deletes an invited user from a project
       /// </summary>
       /// <param name="payload"></param>
       /// <returns></returns>
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
