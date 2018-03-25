using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaultTest.Controllers
{
    [Route("test")]
    public class TestController: Controller
    {
        [HttpGet]
        public IActionResult Get([FromServices] IConfiguration configuration)
        {
            return Json(configuration["some_secret"]);
        }
    }
}
