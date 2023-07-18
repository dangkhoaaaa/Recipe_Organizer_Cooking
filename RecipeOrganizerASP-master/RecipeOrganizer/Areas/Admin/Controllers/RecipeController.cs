using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using RecipeOrganizer.Areas.Admin.Models.RecipeManage;
using RecipeOrganizer.Areas.Data;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
	[Authorize(Roles = RoleName.Administrator)]
	[Area("Admin")]
	[Route("/Recipe/[action]")]
	public class RecipeController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		private readonly ILogger<ManageController> _logger;

		private UserRepository _userRepository;
		private RecipeRepository _recipeRepository;
		private MetadataRepository _metadataRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;
		private readonly NotificationRepository _notificationRepository;
		private readonly MediaRepository _mediaRepository;

		public RecipeController(
		UserManager<AppUser> userManager,
		SignInManager<AppUser> signInManager,
		RoleManager<IdentityRole> roleManager,
		IEmailSender emailSender,
		ILogger<ManageController> logger
		)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_logger = logger;
			_recipeRepository = new RecipeRepository();
			_userRepository = new UserRepository();
			_metadataRepository = new MetadataRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_tagRepository = new TagRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
            _notificationRepository = new NotificationRepository();
			_mediaRepository = new MediaRepository();

        }

		[HttpGet("/manage/recipe")]
		public ActionResult Index(int pg = 1)
		{
			var model = _recipeRepository.GetRecipesWithMetadataOrderByDate();
			const int pageSize = 10;
			if (pg < 1)
				pg = 1;
			int recsCount = model.Count();
			var pager = new Pager(recsCount, pg, pageSize);
			int recSkip = (pg - 1) * pageSize;
			var data = model.Skip(recSkip).Take(pager.PageSize).ToList();
			data = data.OrderByDescending(x => x.Recipe.Date).ToList();
            pager.AspController = "manage";
            pager.AspAction = "recipe";

            this.ViewBag.Pager = pager;
			return View(data);
		}

		[HttpPost]
        public ActionResult Index(int recipeID, string status, string searchRecipe)
        {
            _recipeRepository.UpdateApprovalStatus(recipeID, status);
            return RedirectToAction("SearchRecipe", new { keyword = searchRecipe });
        }


        //HttpGet Recipe/SearchRecipe
        public ActionResult SearchRecipe(string keyword)
		{
			ViewBag.RecipeKeyword = keyword;
			var model = _recipeRepository.GetRecipesWithMetadataOrderByDate();
			if (keyword == null)
			{
                    ViewBag.NotFind = "";
					return View("Index", model);

            }
            var listSearchRecipe = model.Where(p => p.Recipe.Title.Contains(keyword.Trim()) || p.User.UserName.Contains(keyword.Trim())).ToList();
            if (listSearchRecipe.Count == 0)
            {

                ViewBag.NotFind = "No result match the keyword";
                return View("Index", listSearchRecipe);

            }
            return View("Index", listSearchRecipe);
        }

        // GET: /Recipe/Censhorship
        public ActionResult PendingRecipe()
        {
            var model = _recipeRepository.GetRecipesByStatusWithMetadata("pending");
            return View(model);
        }

        [HttpPost]
        public ActionResult PendingRecipe(int recipeID, string status, string searchRecipe)
        {
            _recipeRepository.UpdateApprovalStatus(recipeID, status);
            return RedirectToAction("SearchPendingRecipe", new {keyword = searchRecipe });
        }

		//HttpGet Recipe/SearchPendingRecipe
		public ActionResult SearchPendingRecipe(string keyword)
		{
			ViewBag.RecipeKeyword = keyword;
			var model = _recipeRepository.GetRecipesByStatusWithMetadata("pending");
			if (keyword == null)
			{
				ViewBag.NotFind = "";
				return View("PendingRecipe", model);

			}
			var listSearchRecipe = model.Where(p => p.Recipe.Title.Contains(keyword.Trim()) || p.User.UserName.Contains(keyword.Trim())).ToList();
			if (listSearchRecipe.Count == 0)
			{

				ViewBag.NotFind = "No result match the keyword";
				return View("PendingRecipe", listSearchRecipe);

			}
			return View("PendingRecipe", listSearchRecipe);
		}

		//HttpGet Recipe/RejectRecipe
		public ActionResult RejectRecipe()
        {
            var model = _recipeRepository.GetRecipesByStatusWithMetadata("rejected");
            return View(model);
        }

        [HttpPost]
        public ActionResult RejectRecipe(int recipeID, string status, string searchRecipe)
        {
            _recipeRepository.UpdateApprovalStatus(recipeID, status);
            return RedirectToAction("SearchPendingRecipe", new { keyword = searchRecipe });
        }

        //HttpGet Recipe/SearchPendingRecipe
        public async Task<IActionResult> SearchRejectRecipe(string keyword)
        {
            ViewBag.RecipeKeyword = keyword;
            var model = _recipeRepository.GetRecipesByStatusWithMetadata("rejected");
            if (keyword == null)
            {
                ViewBag.NotFind = "";
                return View("RejectRecipe", model);

            }
            var listSearchRecipe = model.Where(p => p.Recipe.Title.Contains(keyword.Trim()) || p.User.UserName.Contains(keyword.Trim())).ToList();
            if (listSearchRecipe.Count == 0)
            {

                ViewBag.NotFind = "No result match the keyword";
                return View("RejectRecipe", listSearchRecipe);

            }
            return View("RejectRecipe", listSearchRecipe);
        }

        // GET: /Recipe/RecipeDetails
        public async Task<IActionResult> RecipeDetails(int recipeID)
		{
			var model = _recipeRepository.GetRecipesWithID(recipeID);
			if (model == null)
				return RedirectToAction("Index");
			model.Ingredients = _ingredientRepository.GetByRecipeId(recipeID);
			model.Directions = _directionRepository.GetByRecipeId(recipeID);
			model.Tags = _recipeHasTagRepository.GetTagsByRecipeId(recipeID);
			return View(model);
		}

		// GET: /Recipe/RecipeDetails
		[HttpPost]
        public async Task<IActionResult> RecipeDetails(int recipeID, string status)
		{
			_recipeRepository.UpdateApprovalStatus(recipeID, status);
			sendNotification(recipeID, status);
			var model = _recipeRepository.GetRecipesWithID(recipeID);
			model.Medias = _mediaRepository.GetImgsByRecipeId(recipeID);
			model.Ingredients = _ingredientRepository.GetByRecipeId(recipeID);
			model.Directions = _directionRepository.GetByRecipeId(recipeID);
			model.Tags = _recipeHasTagRepository.GetTagsByRecipeId(recipeID);
			return View(model);
		}

		public void sendNotification(int recipeID, string status)
		{
            int notificationId = _notificationRepository.addNotification("Your recipe has been " + status);
            AppUser user = _userRepository.GetUserByRecipe(recipeID);
            Metadata metadata = new Metadata
            {
                RecipeId = recipeID,
                UserId = user.Id,
                NotificationId = notificationId
            };
            _metadataRepository.Add(metadata);
        }






		//public List<RecipeViewModel> GetRecipesWithMetadata()
		//{
		//    var query = from m in _metadataRepository._dbSet
		//                join r in _recipeRepository._dbSet on m.RecipeId equals r.RecipeId
		//                join u in _userRepository._dbSet on m.UserId equals u.Id
		//                select new RecipeViewModel
		//                {
		//                    UserId = u.Id,
		//                    RecipeId = r.RecipeId,
		//                    UserName = u.UserName,
		//                    RecipeTitle = r.Title,
		//                    RecipeDescription = r.Description,
		//                    CreateDate = r.Date,
		//                    Status = r.Status,
		//                    NumberShare = r.NumberShare,
		//                    RecipeImage = r.Image
		//                };

		//    return query.ToList();
		//}
	}
}
