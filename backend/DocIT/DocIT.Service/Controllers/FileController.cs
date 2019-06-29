using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using DocIT.Core.Services;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace DocIT.Service.Controllers
{
    [Route("[controller]")]
    public class FileController : BaseController
    {
        private readonly IFileUploader uploader;

        public FileController(IFileUploader uploader)
        {
            this.uploader = uploader;
        }
       
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file) => Ok(await uploader.UploadFileAsync(file.OpenReadStream(), System.IO.Path.GetExtension(file.FileName)));

        
        
    }
}
