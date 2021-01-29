using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Almocherifado.ServerHosted.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermoController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public TermoController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }


        public IActionResult Get([FromQuery] string filepath)
        {
            var stream = new FileStream( filepath, FileMode.Open);
            return File(stream, "application/pdf", "FileDownloadName.pdf");
        }
    }
}
