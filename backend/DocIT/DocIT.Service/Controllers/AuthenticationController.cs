using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DocIT.Core.Services;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Services.Exceptions;
using DocIT.Service.Authentication;
using DocIT.Service.Models;

namespace DocIT.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService auth;

        public AuthenticationController(IAuthenticationService auth)
        {
            this.auth = auth;
        }



        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterPayload reg)
        {
            try
            {
                var res = await auth.RegisterAsync(reg);
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
                var res = await auth.LoginAsync(loginPayload);
                return Ok(res);
            }
            catch (AuthException ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
