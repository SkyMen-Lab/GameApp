using Microsoft.AspNetCore.Mvc;

namespace GameApp.Controllers
{
    [ApiController]
    [Route("/v1a/")]
    public class MainController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok("Hello");
        }
    }
}