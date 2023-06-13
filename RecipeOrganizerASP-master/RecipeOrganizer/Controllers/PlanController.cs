using Microsoft.AspNetCore.Mvc;

namespace RecipeOrganizer.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
