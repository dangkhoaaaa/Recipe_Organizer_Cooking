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
		private readonly CollectionRepository _collectionRepository;
		public PlanController(UserManager<AppUser> userManager)
        {
            _mealPlaningRepository = new MealPlaningRepository();
            _dayRepository = new DayRepository();
            _sessionRepository = new SessionRepository();
            _userManager = userManager;
            _hasRecipeRepository = new SessionHasRecipeRepository();
			_slot = new Slot();
            _recipeRepository = new RecipeRepository();
            _collectionRepository = new CollectionRepository();
		}

		public async Task<IActionResult> SearchKeyWordFitler( string filter,string slotNow, string week, string keyword = "", int productPage = 1)
		{
			ViewBag.slotNow = slotNow;
			ViewBag.Week = week;
			ViewBag.Keyword = keyword;
			ViewBag.filter = filter;
			List<Recipe> results = null;

			List<Recipe> recipesSearchAll = _recipeRepository.SearchAllTitleWithFilter(filter, keyword);



			if (keyword != null && recipesSearchAll.Count() > 0)
			{
				results = _recipeRepository.getRecipeByKeywordWitPaging(keyword, productPage, PageSize, recipesSearchAll);
				var user = await _userManager.GetUserAsync(User);
				if (user != null)
				{
					var checkRecipeSave = _collectionRepository.CollectionList(recipesSearchAll, user.Id);
					ViewBag.CheckCollectionSave = checkRecipeSave;
				}
			}
			else
			{
				ViewBag.notfound = "Not Found Recipe";
			}


			return View("ListRecipe", new RecipeListDisplayWithPaging
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
		public async Task<IActionResult> ViewPlan(string week)
        {
            

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Slot = HttpContext.Session.GetJson<Slot>("cart");
                var weekNow = "";
                if (week == null)
                {
                    
                    weekNow = _mealPlaningRepository.WeekNow();
                    ViewBag.week = weekNow;
                    _mealPlaningRepository.showPlan(weekNow, user.Id);
                    Slot = _mealPlaningRepository.showPlan(weekNow, user.Id);

                } else
                {
                    ViewBag.week = week;
                     
                    Slot = _mealPlaningRepository.showPlan(week, user.Id);

                }
                HttpContext.Session.SetJson("cart", Slot);

                //            MealPlanning mealPlanning = new MealPlanning();
                //            mealPlanning = _mealPlaningRepository.GetPlanID(user.Id, weekNow);
                //            //            List<Day> days = _dayRepository.getDayByPlan(mealPlanning);
                //            //List<Session> sessions = new List<Session>();
                //            List<SessionHasRecipe> hasRecipes = new List<SessionHasRecipe>();
                //            mealPlanning.Days = _dayRepository.getDayByPlan(mealPlanning);

                //            //foreach (Day day in days)
                //            //{
                //            //	sessions = _sessionRepository.getSessionByDay(day);
                //            //                mealPlanning.Days.Add(day);
                //            //	foreach (Session session in sessions)
                //            //	{
                //            //                    day.Sessions.Add(session);
                //            //		hasRecipes.AddRange(_hasRecipeRepository.getSessionHasRecipeBySession(session));

                //            //	}
                //            //}

                //            foreach (Day day1 in mealPlanning.Days)
                //            {
                //                day1.Sessions = _sessionRepository.getSessionByDay(day1);
                //                if (day1.DayOfWeek.Equals("Tuesday")){
                //                    foreach (Session session in day1.Sessions)
                //                    {
                //                        if (session.SessionName.Equals("br"))
                //                        {
                //                            foreach (SessionHasRecipe sessionHas in _hasRecipeRepository.getSessionHasRecipeBySession(session))
                //                            {
                //                                Console.WriteLine(sessionHas.RecipeId);

                //                            }
                //                        }
                //                        else Console.WriteLine("ko co mon an");
                //                    }
                //                } else Console.WriteLine("ko co thu 2");
                //}



                return View("ViewPlan", Slot);


            }  return RedirectToAction("Index", "Home");
            

        }

		public Slot? Slot { get; set; }


		public IActionResult Index()
		{
			return View("ViewPlan", HttpContext.Session.GetJson<Slot>("cart"));
		}
        public async Task<IActionResult> AddToSlotAsync(int recipeID, int slotNow, string week)
        {
            //lay recipe co id giong id can tim
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Recipe? recipe = _recipeRepository.GetAll()
            .FirstOrDefault(p => p.RecipeId == recipeID);
                if (recipe != null && slotNow >= 1 && slotNow <= 21)
                {
					ViewBag.week = week;
					Slot = HttpContext.Session.GetJson<Slot>("cart") ?? new Slot();
                    Slot.AddItemToSlot(recipe, slotNow, user.Id, week);
                    HttpContext.Session.SetJson("cart", Slot);
                }
                return View("ViewPLan", Slot);
            }
            return RedirectToAction("Index", "Home");
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


            public async Task<IActionResult> RemoveFromCart(int recipeID, int slotNow, string week)
		{
			var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Recipe? recipe = _recipeRepository.GetAll()
            .FirstOrDefault(p => p.RecipeId == recipeID);
                if (recipe != null)
                {
					ViewBag.week = week;
					Slot = HttpContext.Session.GetJson<Slot>("cart");
                    Slot.RemoveRecipe(slotNow, recipe, user.Id, week);
                    HttpContext.Session.SetJson("cart", Slot);
                }
                return View("ViewPLan", Slot);
            }
			return RedirectToAction("Index", "Home");
		}

        public async Task<IActionResult> NewWeek(string week)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                
                

                ViewBag.week = week;


                Slot = HttpContext.Session.GetJson<Slot>("cart");
               
                Slot = _mealPlaningRepository.showPlan(week, user.Id);

                HttpContext.Session.SetJson("cart", Slot);



                return View("ViewPlan", Slot) ;
            }

          return RedirectToAction("Index", "Home");


    }

    int PageSize = 8;
		public IActionResult ListRecipe(string keyword="", int productPage = 1, int slotNow=1, string week = "")
		{
			
			ViewBag.Keyword = keyword;
			ViewBag.slotNow = slotNow;
			ViewBag.Week = week;
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
        public async Task<IActionResult> SavePlan(string week)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewBag.week = week;
            if (user != null)
            {
                Slot = HttpContext.Session.GetJson<Slot>("cart");
                List<CartLine> cartLines = new List<CartLine>();
                foreach (var line in Slot.Lines)
                {
                    if (line.Week == week && line.UserID == user.Id)
                    {
                        cartLines.Add(line);
                    }
                }
                
               

                    
                    _mealPlaningRepository.AddPlan(cartLines, week, user.Id);

                   Slot = _mealPlaningRepository.showPlan(week, user.Id);
               
                //    Recipe? recipe = _recipeRepository.GetAll()
                //.FirstOrDefault(p => p.RecipeId == recipeID);
                //    if (recipe != null)
                //    {
                //        ViewBag.week = week;
                //        Slot = HttpContext.Session.GetJson<Slot>("cart");
                //        Slot.RemoveSlot(slotNow, recipe, user.Id, week);
                //        HttpContext.Session.SetJson("cart", Slot);
                //    }

                HttpContext.Session.SetJson("cart", Slot);

                return View("ViewPLan", Slot);
            }
            return RedirectToAction("Index", "Home");
        }





    }
}
