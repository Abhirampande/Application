using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
