using Microsoft.AspNetCore.Mvc;

namespace OPENAI.Controllers
{
    public class ImageGenerationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
