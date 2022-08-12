using Microsoft.AspNetCore.Mvc;

namespace Planitly.Backend.Controllers
{
    [Route("/")]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetServerStatus()
        {
            return Ok("Server is running!");
        }
    }
}