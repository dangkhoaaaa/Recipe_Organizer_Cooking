using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace RecipeOrganizer.Controllers
{
    public class PlanController : Controller
    {
        public IActionResult ViewPlan()
        {
            
            
            return View();
        }
    }
}
