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
    public class UsersController : BaseController
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets a user's profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Core.Data.Models.User>> Get()
        {
            try
            {
                return Ok(await service.GetUserByIdAsync(this.UserId));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        /// <summary>
        /// Updates a user profile account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateAccountPayload value)
        {
            try
            {
                await service.UpdateUserAsync(value, this.UserId);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

       
    }
}
