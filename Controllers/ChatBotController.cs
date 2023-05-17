using Microsoft.AspNetCore.Mvc;

namespace OPENAI.Controllers
{
    public class ChatBotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
