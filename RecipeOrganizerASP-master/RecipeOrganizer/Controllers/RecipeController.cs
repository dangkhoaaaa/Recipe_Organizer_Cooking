using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using Services.Data;
using Microsoft.AspNetCore.Authorization;
using Services;

namespace RecipeOrganizer.Controllers
{
	[Authorize]
	public class RecipeController : Controller
	{
		private readonly RecipeRepository _recipeRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;
		private readonly CategoryRepository _categoryRepository;
		private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
		private readonly MetadataRepository _metadataRepository;
		private readonly MediaRepository _mediaRepository;
		private readonly CollectionRepository _collectionRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly FireBaseService _fireBaseService;
		private readonly NotificationRepository _notificationRepository;
		private readonly FeedbackRepository _feedbackRepository;

		public RecipeController(UserManager<AppUser> userManager)
		{
			_recipeRepository = new RecipeRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_tagRepository = new TagRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
			_categoryRepository = new CategoryRepository();
			_recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_collectionRepository = new CollectionRepository();
			_userManager = userManager;
			_fireBaseService = new FireBaseService();
			_notificationRepository = new NotificationRepository();
			_feedbackRepository = new FeedbackRepository();
		}

		public IActionResult Index()
		{
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AddNewRecipe()
		{
			var categories = _categoryRepository.GetAllCategories();
			RecipeData data = new RecipeData();
			data.Categories = categories;
			return View(data);
		}

		public IActionResult AccessDenied()
		{
			return View();
			//return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> AddNewRecipe(RecipeData recipe, List<IFormFile> files)
		{
			//if (ModelState.IsValid)
			//{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				// Assign the user ID to the recipe
				recipe.Status = recipe.Status.ToString();
				recipe.NumberShare = 0;

				Recipe data = new Recipe();

				// Title
				if (string.IsNullOrEmpty(recipe.Title))
				{
					data.Title = "null";
				}
				else
				{
					data.Title = recipe.Title.Trim();
				}

				// Description
				if (string.IsNullOrEmpty(recipe.Description))
				{
					data.Description = "Wishing you a delicious and satisfying meal!";
				}
				else
				{
					data.Description = recipe.Description;
				}

				data.Date = DateTime.Now;
				data.NumberShare = recipe.NumberShare;
				data.Status = recipe.Status;
				int notificationId = _notificationRepository.addNotification("New recipe has been created");

				// Media
				if (files != null && files.Count > 0)
				{
					var imageLinkTask = _fireBaseService.UploadImageSingle(files);
					var imageLink = await imageLinkTask;
					int mediaId = _mediaRepository.addMedia(imageLink);
					data.Image = imageLink;
					_recipeRepository.Add(data);

					Metadata metadata = new Metadata
					{
						RecipeId = data.RecipeId,
						UserId = user.Id,
						MediaId = mediaId,
						NotificationId = notificationId
					};
					_metadataRepository.Add(metadata);
				}
				else//danh dau
				{
					_recipeRepository.Add(data);
					// If there is no media file, just create a new Metadata object
					Metadata metadata = new Metadata
					{
						RecipeId = data.RecipeId,
						UserId = user.Id,
						NotificationId = notificationId
					};
					_metadataRepository.Add(metadata);
				}

				// Ingredients
				if (!string.IsNullOrEmpty(recipe.IngredientsInput))
				{
					_ingredientRepository.addIngredient(recipe.IngredientsInput, data.RecipeId);
				}

				// Directions
				if (!string.IsNullOrEmpty(recipe.DirectionsInput))
				{
					_directionRepository.addDirection(recipe.DirectionsInput, data.RecipeId);
				}

				// Categories
				if (recipe.CategoryInput != null && recipe.CategoryInput.Any())
				{
					_recipeHasCategoryRepository.AddCategory(recipe.CategoryInput, data.RecipeId);
				}

				// Tags
				if (!string.IsNullOrEmpty(recipe.TagsInput))
				{
					_tagRepository.AddTags(recipe.TagsInput, data.RecipeId);
				}

				//}
				return RedirectToAction("UsrPendingRecipe", "Recipe", new { id = data.RecipeId });
			}
			return View();
		}

		public async Task<IActionResult> UsrPendingRecipe(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				Recipe? recipe = _recipeRepository.GetRecipeByAuthor(id, user.Id);
				if (recipe != null)
				{
					RecipeData data = ConvertToRecipeData(recipe);
					return View(data);
				}
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> UsrPendingRecipeNoti(int id, int noti)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				Recipe? recipe = _recipeRepository.GetRecipeByAuthor(id, user.Id);
				var notification = _notificationRepository.GetNotification(noti);

				if (recipe != null && notification != null)
				{
					_notificationRepository.updateIsRead(notification);
					RecipeData data = ConvertToRecipeData(recipe);
					return View("UsrPendingRecipe", data);
				}
			}
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public async Task<IActionResult> RecipeDetail(int id)
		{
			Recipe? recipe = _recipeRepository.GetById(id, "public");
			if (recipe != null)
			{
				RecipeData data = new RecipeData();
				data.RecipeId = id;
				data.Title = recipe.Title;
				data.Description = recipe.Description;
				data.Status = recipe.Status;
				data.Date = recipe.Date;
				List<Ingredient> ingredient = _ingredientRepository.GetByRecipeId(recipe.RecipeId);
				data.Ingredients = ingredient;
				List<Direction> direction = _directionRepository.GetByRecipeId(recipe.RecipeId);
				data.Directions = direction;
				List<Tag> tags = _recipeHasTagRepository.GetTagsByRecipeId(recipe.RecipeId);
				data.Tags = tags;
				data.Img = recipe.Image;
				data.Imgs = GetImgs(recipe.RecipeId);
				data.NumberShare = recipe.NumberShare;
				var categories = _recipeHasCategoryRepository.GetCategoryByRecipeId(recipe.RecipeId);
				data.Categories = categories;
				var AvgRating = _feedbackRepository.CalculateAverageRating(recipe.RecipeId);
				data.AvgRate = AvgRating;

				var user = await _userManager.GetUserAsync(User);
				if (user != null)
				{
					bool isSavedInCollection = _collectionRepository.IsRecipeSaved(id, user.Id);
					bool hasReviewed = _metadataRepository.IsReviewed(id, user.Id);

					if (isSavedInCollection && hasReviewed)
					{
						data.Collection = true;
						data.Review = true;
					}
					else if (isSavedInCollection)
					{
						data.Collection = true;
						data.Review = false;
					}
					else if (hasReviewed)
					{
						data.Collection = false;
						data.Review = true;
					}
					else
					{
						data.Collection = false;
						data.Review = false;
					}
				}

				return View(data);
			}
			return RedirectToAction("PageNotFound", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> ToggleCollection(int recipeId)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				_collectionRepository.ToggleCollection(recipeId, user.Id);
			}

			return RedirectToAction("RecipeDetail", "Recipe", new { id = recipeId });
		}

		public async Task<IActionResult> EditRecipe(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				Recipe? recipe = _recipeRepository.GetByIdForEdit(id);
				if (recipe != null)
				{
					RecipeData recipeData = ConvertToRecipeData(recipe);

					var categories = _categoryRepository.GetAllCategories();
					recipeData.Categories = categories;

					// Retrieve the selected category IDs for the recipe
					List<int> selectedCategoryIds = _recipeHasCategoryRepository.GetSelectedCategoryIds(recipe.RecipeId);

					// Set the SelectedCategories property with the retrieved category IDs
					recipeData.SelectedCategories = selectedCategoryIds;

					return View(recipeData);
				}
				else
				{
					return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = id });
				}
			}
			return RedirectToAction("AccessDenied", "Recipe");
		}


		public async Task<IActionResult> TrashRecipe(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				Recipe? recipe = _recipeRepository.GetRecipeByAuthor(id, user.Id);
				if (recipe != null)
				{
					_recipeRepository.ChangeStatusRecipe(id, "trash");
					return RedirectToAction("UsrPendingRecipe", "Recipe");
				}
			}
			return RedirectToAction("AccessDenied", "Recipe");
		}

		private RecipeData ConvertToRecipeData(Recipe recipe)
		{
			RecipeData data = new RecipeData();
			data.RecipeId = recipe.RecipeId;
			data.Title = recipe.Title;
			data.Description = recipe.Description;
			data.Status = recipe.Status;
			data.Img = recipe.Image;
			data.NumberShare = recipe.NumberShare;
			data.Imgs = GetImgs(recipe.RecipeId);
			var AvgRating = _feedbackRepository.CalculateAverageRating(recipe.RecipeId);
			data.AvgRate = AvgRating;

			if (recipe.AvgRate == null)
			{
				recipe.AvgRate = 0.0;
			}
			data.AvgRate = recipe.AvgRate;

			List<Ingredient> ingredients = _ingredientRepository.GetByRecipeId(recipe.RecipeId);
			List<Direction> directions = _directionRepository.GetByRecipeId(recipe.RecipeId);
			List<Tag> tags = _recipeHasTagRepository.GetTagsByRecipeId(recipe.RecipeId);
			var categories = _recipeHasCategoryRepository.GetCategoryByRecipeId(recipe.RecipeId);
			data.Categories = categories;

			if (ingredients != null)
			{
				data.Ingredients = ingredients.ToList();
			}

			if (directions != null)
			{
				data.Directions = directions.ToList();
			}

			if (tags != null)
			{
				data.Tags = tags.ToList();
			}

			return data;
		}

		[HttpPost]
		public async Task<IActionResult> EditRecipe(RecipeData recipe, string Action, List<IFormFile> files)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				Recipe? existingRecipe = _recipeRepository.GetByIdForEdit(recipe.RecipeId);
				if (existingRecipe != null)
				{
					if (Action == "save")
					{
						// Update the recipe details
						existingRecipe.Title = string.IsNullOrEmpty(recipe.Title) ? "null" : recipe.Title;
						existingRecipe.Description = string.IsNullOrEmpty(recipe.Description) ? "Wishing you a delicious and satisfying meal!" : recipe.Description;
						existingRecipe.Date = DateTime.Now;

						// Update the recipe status
						if (!string.IsNullOrEmpty(recipe.Status))
						{
							existingRecipe.Status = recipe.Status;
						}

						// Update the ingredients
						if (!string.IsNullOrEmpty(recipe.IngredientsInput))
						{
							_ingredientRepository.UpdateIngredients(recipe.IngredientsInput, existingRecipe.RecipeId);
						}

						// Update the directions
						if (!string.IsNullOrEmpty(recipe.DirectionsInput))
						{
							_directionRepository.UpdateDirections(recipe.DirectionsInput, existingRecipe.RecipeId);
						}

						// Update the media
						if (files != null && files.Count > 0)
						{
							var imageLinkTask = _fireBaseService.UploadImageSingle(files);
							var imageLink = await imageLinkTask;
							int mediaId = _mediaRepository.addMedia(imageLink);

							existingRecipe.Image = imageLink;

							Metadata metadata = new Metadata
							{
								RecipeId = existingRecipe.RecipeId,
								UserId = user.Id,
								MediaId = mediaId
							};
							_metadataRepository.Add(metadata);
						}

						// Update the categories
						if (recipe.CategoryInput != null)
						{
							_recipeHasCategoryRepository.UpdateRecipeCategories(recipe.CategoryInput, recipe.RecipeId);
						}

						// Update the tags
						if (!string.IsNullOrEmpty(recipe.TagsInput))
						{
							_tagRepository.UpdateTags(recipe.TagsInput, existingRecipe.RecipeId);
						}

						_recipeRepository.Update(existingRecipe);
						return RedirectToAction("UsrPendingRecipe", "Recipe", new { id = existingRecipe.RecipeId });

					}
					else if (Action == "trash")
					{
						existingRecipe.Status = "trash";
						_recipeRepository.Update(existingRecipe);
						return RedirectToAction("UsrPendingRecipe", "Recipe", new { id = existingRecipe.RecipeId });
					}
				}
				else
				{
					return RedirectToAction("PageNotFound", "Home");
				}
			}

			return RedirectToAction("AccessDenied", "Recipe");
		}

		[AllowAnonymous]
		public IActionResult PrintRecipe(int recipeId)
		{
			Recipe? recipe = _recipeRepository.GetById(recipeId, "public");
			if (recipe != null)
			{
				RecipeData data = ConvertToRecipeData(recipe);
				return View(data);
			}

			return RedirectToAction("AccessDenied", "Recipe");
		}

		[AllowAnonymous]
		public IActionResult ShareRecipe(int recipeId)
		{
			_recipeRepository.IncreaseNumberShare(recipeId);

			return View();
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult IncreaseNumberShare(int recipeId)
		{
			_recipeRepository.IncreaseNumberShare(recipeId);

			return Json(new { success = true });
		}

		public List<string> GetImgs(int id)
		{
			List<Media> imgList = _mediaRepository.GetImgsByRecipeId(id);
			List<string> imgs = new List<string>();

			foreach (var img in imgList)
			{
				imgs.Add(img.Filelocation);
			}

			return imgs;
		}

	}
}
