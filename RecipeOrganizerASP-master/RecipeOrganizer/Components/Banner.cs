using Microsoft.AspNetCore.Mvc;

namespace RecipeOrganizer.Components
{
    public class Banner : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
