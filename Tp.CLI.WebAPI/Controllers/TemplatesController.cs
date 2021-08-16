using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tp.CLI.WebAPI.Model;

namespace Tp.CLI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : Controller
    {
        [HttpPost]
        public IActionResult Index([FromBody] Template template)
        {
            var TEMPLATE_NAME = template.TemplateName;
            var TEMPLATE_VERSION = template.TemplateVersion;

            if (!Directory.Exists($"Templates\\{TEMPLATE_NAME}"))
            {
                return BadRequest("Template Not Found!");
            }

            if (TEMPLATE_VERSION == null)
            {
                TEMPLATE_VERSION = template.GetLatestVersion();
            }

            try
            {
                const string contentType = "application/zip";
                HttpContext.Response.ContentType = contentType;
                var zipFile = new FileContentResult(System.IO.File.ReadAllBytes($"Templates\\{TEMPLATE_NAME}\\{TEMPLATE_VERSION}\\MyProjectName.zip"), contentType)
                {
                    FileDownloadName = $"{TEMPLATE_NAME}-v{TEMPLATE_VERSION}.zip"
                };
                return zipFile;
            }
            catch
            {
                const string contentType = "text/plain";
                HttpContext.Response.ContentType = contentType;
                return BadRequest("Version Not Found!");
            }
        }

        [Route("version")]
        [HttpPost]
        public IActionResult GetLatestVersion([FromBody] Template template)
        {
            var TEMPLATE_NAME = template.TemplateName;

            if (!Directory.Exists($"Templates\\{TEMPLATE_NAME}"))
            {
                return BadRequest("Template Not Found!");
            }
            return Ok(template.GetLatestVersion());
        }
    }
}
