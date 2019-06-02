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
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserAuthenticationService auth;

        public UsersController(IUserAuthenticationService auth)
        {
            this.auth = auth;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await auth.GetUserByIdAsync(UserId);
            if (user is null) return NotFound();
            return Ok(user);
        }



        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterPayload reg)
        {
            try
            {
                var res = await auth.RegisterUserAsync(reg);
                return Ok(res);
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginPayload loginPayload)
        {
            try
            {
                var res = await auth.LoginUserAsync(loginPayload);
                return Ok(res);
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAccountPayload item)
        {
            try
            {
                await auth.UpdateUserAsync(item, this.UserId);
                return Ok();
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
