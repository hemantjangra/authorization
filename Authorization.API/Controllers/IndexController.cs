using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    public class IndexController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return Ok();
        }
    }
}