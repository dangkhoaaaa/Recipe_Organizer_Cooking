using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using Services.Data;

namespace RecipeOrganizer.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly RecipeRepository _recipeRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
		private readonly MetadataRepository _metadataRepository;
		private readonly MediaRepository _mediaRepository;
		private readonly CollectionRepository _collectionRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly FireBaseService _fireBaseService;

		public UserController(UserManager<AppUser> userManager)
		{
			_recipeRepository = new RecipeRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
			_tagRepository = new TagRepository();
			_recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_collectionRepository = new CollectionRepository();
			_userManager = userManager;
			_fireBaseService = new FireBaseService();
		}

		public IActionResult Index()
		{
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> UserRecipeList()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				List<Recipe> recipeList = _recipeRepository.GetByAuthor(user.Id);
				if (recipeList != null)
				{
					List<RecipeUserData> dataList = new List<RecipeUserData>();
					foreach (var recipe in recipeList)
					{
						string image = recipe.Image ?? "https://media.istockphoto.com/id/1316145932/photo/table-top-view-of-spicy-food.jpg?s=612x612&w=0&k=20&c=eaKRSIAoRGHMibSfahMyQS6iFADyVy1pnPdy1O5rZ98=";
						RecipeUserData userData = new RecipeUserData
						{
							RecipeId = recipe.RecipeId,
							Title = recipe.Title,
							Date = recipe.Date,
							Image = image,
							Status = recipe.Status,
						};
						dataList.Add(userData);
					}
					// Pass the dataList to the view
					return View(dataList);
				}
			}
			return View(Index);
		}

		public async Task<IActionResult> UserCollectionList()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				List<Recipe> recipeList = _collectionRepository.GetUserCollection(user.Id);
				if (recipeList != null)
				{
					List<RecipeUserData> dataList = new List<RecipeUserData>();
					foreach (var recipe in recipeList)
					{
						string image = recipe.Image ?? "https://media.istockphoto.com/id/1316145932/photo/table-top-view-of-spicy-food.jpg?s=612x612&w=0&k=20&c=eaKRSIAoRGHMibSfahMyQS6iFADyVy1pnPdy1O5rZ98=";
                        if (recipe.AvgRate == null)
                        {
                            recipe.AvgRate = 0.0;
                        }
                        RecipeUserData userData = new RecipeUserData
						{
							RecipeId = recipe.RecipeId,
							Title = recipe.Title,
							Date = recipe.Date,
							Image = image,
							AvgRate = recipe.AvgRate
						};
						dataList.Add(userData);
					}
					// Pass the dataList to the view
					return View(dataList);
				}
			}
			return View(Index);
		}

		[HttpGet]
		public async Task<IActionResult> ToggleCollection(int recipeId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				_collectionRepository.ToggleCollection(recipeId, user.Id);
			}

			return RedirectToAction("UserCollectionList", "User");
		}


	}
}

//        // GET: User
//        public async Task<IActionResult> Index()
//        {
//              return _context.Users != null ? 
//                          View(await _context.Users.ToListAsync()) :
//                          Problem("Entity set 'Recipe_OrganizerContext.Users'  is null.");
//        }

//        // GET: User/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }

//        //HTTP get /Home/Register
//        [HttpGet]
//        public ActionResult Register()
//        {
//            //kojijijiji
//            return View();
//        }

//        //HTTP get /Home/Register
//        [HttpPost]
//        public ActionResult Register(User user)
//        {
//            user.Status = true;
//            user.Role = "Admin";
//            user.Email = "test";
//            _context.Users.Add(user);
//            _context.SaveChanges();
//            return RedirectToAction("Login");
//        }

//        //HTTP post /User/Login
//        [HttpGet]
//        public ActionResult Login()
//        {
//            //if (Session["User"] != null)
//            //{
//            //    return View("Index");
//            //}

//            return View();
//        }


//        [HttpPost]
//        public ActionResult Login(User user)
//        {
//            var username = user.Username;
//            var password = user.Password;
//            var userCheck = _context.Users.SingleOrDefault(x => x.Username.Equals(username)
//                                                    && x.Password.Equals(password));
//            if (userCheck != null)
//            {
//               // Session["User"] = userCheck;
//                return RedirectToAction("Index", "Home");
//            }
//            else
//            {
//                ViewBag.LoginFail = "Login fail. Invalid username or password";
//                return View("Login");
//            }
//        }



//        //[HttpGet]
//        //public ActionResult Logout()
//        //{
//        //    Session["User"] = null;
//        //    return RedirectToAction("Login", "Home");
//        //}


//        // POST: User/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserId,Username,Password,Email,FirstName,LastName,Birthday,Avatar,Role,Status")] User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(user);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(user);
//        }

//        // GET: User/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            return View(user);
//        }

//        // POST: User/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("UserId,Username,Password,Email,FirstName,LastName,Birthday,Avatar,Role,Status")] User user)
//        {
//            if (id != user.UserId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(user);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserExists(user.UserId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(user);
//        }

//        // GET: User/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.Users == null)
//            {
//                return NotFound();
//            }

//            var user = await _context.Users
//                .FirstOrDefaultAsync(m => m.UserId == id);
//            if (user == null)
//            {
//                return NotFound();
//            }

//            return View(user);
//        }

//        // POST: User/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            if (_context.Users == null)
//            {
//                return Problem("Entity set 'Recipe_OrganizerContext.Users'  is null.");
//            }
//            var user = await _context.Users.FindAsync(id);
//            if (user != null)
//            {
//                _context.Users.Remove(user);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserExists(int id)
//        {
//          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
//        }
//    }
//}
