using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeOrganizer.Data;
using RecipeOrganizer.Infrastructure;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using System.Drawing.Printing;

namespace RecipeOrganizer.Controllers
{
    public class PlanController : Controller
    {
        private readonly MealPlaningRepository _mealPlaningRepository;
        private readonly DayRepository _dayRepository;
        private readonly SessionRepository _sessionRepository;
        private readonly SessionHasRecipeRepository _hasRecipeRepository;
        private readonly UserManager<AppUser> _userManager;
		private readonly Slot _slot;
        private readonly RecipeRepository _recipeRepository;

		public PlanController(UserManager<AppUser> userManager)
        {
            _mealPlaningRepository = new MealPlaningRepository();
            _dayRepository = new DayRepository();
            _sessionRepository = new SessionRepository();
            _userManager = userManager;
            _hasRecipeRepository = new SessionHasRecipeRepository();
			_slot = new Slot();
            _recipeRepository = new RecipeRepository();
		}
        public async Task<IActionResult> ViewPlan()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var weekNow = _mealPlaningRepository.WeekNow();
                MealPlanning mealPlanning = new MealPlanning();
                mealPlanning = _mealPlaningRepository.GetPlanID(user.Id, weekNow);
                //            List<Day> days = _dayRepository.getDayByPlan(mealPlanning);
                //List<Session> sessions = new List<Session>();
                List<SessionHasRecipe> hasRecipes = new List<SessionHasRecipe>();
                mealPlanning.Days = _dayRepository.getDayByPlan(mealPlanning);

                //foreach (Day day in days)
                //{
                //	sessions = _sessionRepository.getSessionByDay(day);
                //                mealPlanning.Days.Add(day);
                //	foreach (Session session in sessions)
                //	{
                //                    day.Sessions.Add(session);
                //		hasRecipes.AddRange(_hasRecipeRepository.getSessionHasRecipeBySession(session));

                //	}
                //}

                foreach (Day day1 in mealPlanning.Days)
                {
                    day1.Sessions = _sessionRepository.getSessionByDay(day1);
                    if (day1.DayOfWeek.Equals("Tuesday")){
                        foreach (Session session in day1.Sessions)
                        {
                            if (session.SessionName.Equals("br"))
                            {
                                foreach (SessionHasRecipe sessionHas in _hasRecipeRepository.getSessionHasRecipeBySession(session))
                                {
                                    Console.WriteLine(sessionHas.RecipeId);
                                    
                                }
                            }
                            else Console.WriteLine("ko co mon an");
                        }
                    } else Console.WriteLine("ko co thu 2");
				}






			}
            return View();

        }

		public Slot? Slot { get; set; }


		public IActionResult Index()
		{
			return View("ViewPlan", HttpContext.Session.GetJson<Slot>("cart"));
		}
		public IActionResult AddToSlot(int recipeID, int slotNow)
		{
            //lay recipe co id giong id can tim
            Recipe? recipe = _recipeRepository.GetAll()
			.FirstOrDefault(p => p.RecipeId == recipeID);
			if (recipe != null && slotNow >= 1 && slotNow <= 21)
			{
				Slot = HttpContext.Session.GetJson<Slot>("cart") ?? new Slot();
				Slot.AddItemToSlot(recipe, slotNow);
				HttpContext.Session.SetJson("cart", Slot);
			}
			return View("ViewPLan", Slot);
		}

		//public IActionResult UpdateToCart(int recipeID, int slotNow)
		//{
		//	Product? product = _context.Products
		//	.FirstOrDefault(p => p.ProductId == productId);
		//	if (product != null)
		//	{
		//		Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
		//		Cart.AddItem(product, -1);
		//		HttpContext.Session.SetJson("cart", Cart);
		//	}
		//	return View("Cart", Cart);
		//}


		public IActionResult RemoveFromCart(int recipeID, int slotNow)
		{
			Recipe? recipe = _recipeRepository.GetAll()
            .FirstOrDefault(p => p.RecipeId == recipeID );
			if (recipe != null)
			{
                Slot = HttpContext.Session.GetJson<Slot>("cart");
                Slot.RemoveSlot(slotNow, recipe);
				HttpContext.Session.SetJson("cart", Slot);
			}
			return View("ViewPLan", Slot);
		}

		int PageSize = 8;
		public IActionResult ListRecipe(string keyword, int productPage = 1, int slotNow=1)
		{
			keyword = "Recipe";
			ViewBag.Keyword = keyword;
			ViewBag.slotNow = slotNow;
			List<Recipe> results = null;
			List<Recipe> recipesSearchAll = _recipeRepository.getRecipeByKeyword(keyword);



			if (keyword != null && recipesSearchAll.Count() > 0)
			{
				results = _recipeRepository.getRecipeByKeywordWitPaging(keyword, productPage, PageSize, recipesSearchAll);
			}
			else
			{
				ViewBag.notfound = "Not Found Recipe";
			}


			return View(new RecipeListDisplayWithPaging
			{
				Recipes = results
					,
				PagingInfo = new PagingInfo
				{
					ItemsPerPage = PageSize,
					CurrentPage = productPage,
					TotalItems = recipesSearchAll.Count()

				}
			});
		}





	}
}
