using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace kolveniershofBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BestandController : ControllerBase
    {
        private readonly string[] aanvaardeBestandExtenties;
        public BestandController()
        {
            aanvaardeBestandExtenties = new string[] { "image/png", "image/jpeg" };
        }

        [HttpPost("{folder}/{bestandNaam}")]
        public async Task<ActionResult> UploadBestand(string folder, string bestandNaam, [FromForm(Name = "bestand")]IFormFile bestand)
        {
            if (!aanvaardeBestandExtenties.Contains(bestand.ContentType))
            {
                return BadRequest();
            }
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder);
            if (!Directory.Exists(folderPad))
            {
                Directory.CreateDirectory(folderPad);
            }
            var bestandPad = Path.Combine(folderPad, bestandNaam);
            using (var fileStream = new FileStream(bestandPad, FileMode.Create))
            {
                await bestand.CopyToAsync(fileStream);
            }
            return Ok();
        }

        [HttpGet("{folder}/{bestandNaam}")]
        public IActionResult Get(string folder, string bestandNaam)
        {
            Byte[] b;
            var folderPad = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folder);
            if (!Directory.Exists(folderPad))
            {
                return NoContent();
            }
            var bestandPad = Path.Combine(folderPad, bestandNaam);
            if (!System.IO.File.Exists(bestandPad))
            {
                return NoContent();
            }
            new FileExtensionContentTypeProvider().TryGetContentType(bestandNaam, out string contentType);
            b = System.IO.File.ReadAllBytes(bestandPad);
            return File(b, contentType);
        }
        
    }
}