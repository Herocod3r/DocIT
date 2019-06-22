using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocIT.Service.Controllers
{

    public class BaseController : Controller
    {
       protected Guid UserId =>  Guid.Parse(User.Claims.FirstOrDefault(a => a.Type == "ID").Value);

        protected string UserEmailAddress => User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
    }
}
