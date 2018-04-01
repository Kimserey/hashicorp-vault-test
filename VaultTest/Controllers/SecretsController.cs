using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace VaultTest.Controllers
{
    [Route("Secrets")]
    public class SecretsController: Controller
    {
        [HttpGet]
        public IActionResult Get([FromServices] IOptions<VaultSecrets> secrets)
        {
            return Json(secrets.Value);
        }
    }
}
