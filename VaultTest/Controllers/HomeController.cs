using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace VaultTest.Controllers
{
    [Route("Home")]
    public class HomeController: Controller
    {
        [HttpGet]
        public IActionResult Get([FromServices] IOptions<VaultSecrets> secrets)
        {
            return Json(secrets.Value);
        }
    }
}
