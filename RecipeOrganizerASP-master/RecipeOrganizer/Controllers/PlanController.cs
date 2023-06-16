using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    public class PlanController : Controller
    {
        private readonly MealPlaningRepository _mealPlaningRepository;
        private readonly DayRepository _dayRepository;
        private readonly SessionRepository _sessionRepository;
        private readonly SessionHasRecipeRepository _hasRecipeRepository;
        private readonly UserManager<AppUser> _userManager;
        public PlanController(UserManager<AppUser> userManager)
        {
            _mealPlaningRepository = new MealPlaningRepository();
            _dayRepository = new DayRepository();
            _sessionRepository = new SessionRepository();
            _userManager = userManager;
            _hasRecipeRepository = new SessionHasRecipeRepository();


        }
        public async Task<IActionResult> ViewPlan()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var weekNow = _mealPlaningRepository.WeekNow();
                MealPlanning mealPlanning = new MealPlanning();
                mealPlanning = _mealPlaningRepository.GetPlanID(user.Id, weekNow);
                List<Day> days = _dayRepository.getDayByPlan(mealPlanning);
				List<Session> sessions = new List<Session>();
                List<SessionHasRecipe> hasRecipes = new List<SessionHasRecipe>();

				foreach (Day day in days)
				{
					sessions = _sessionRepository.getSessionByDay(day);
					foreach (Session session in sessions)
					{
						hasRecipes.AddRange(_hasRecipeRepository.getSessionHasRecipeBySession(session));
					}
				}








			}
            return View();

        }
    }
}
