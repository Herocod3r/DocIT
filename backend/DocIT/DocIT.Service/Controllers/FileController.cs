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

        /// <summary>
        /// Uploads a swagger file and return a relative path
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Post(IFormFile file) => Ok(await uploader.UploadFileAsync(file.OpenReadStream(), System.IO.Path.GetExtension(file.FileName)));

        
        
    }
}
