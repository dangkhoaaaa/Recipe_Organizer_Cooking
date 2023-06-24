using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using Services.Data;
using Microsoft.AspNetCore.Authorization;
using Services;
using Microsoft.EntityFrameworkCore;

namespace RecipeOrganizer.Controllers
{
	[Authorize]
	public class RecipeController : Controller
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

		public RecipeController(UserManager<AppUser> userManager)
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

		public IActionResult AddNewRecipe()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddNewRecipe(RecipeData recipe, List<IFormFile> mediaFiles)
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
				_recipeRepository.Add(data);

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

				// Media
				if (mediaFiles != null)
				{
					// Save the file to the server
					//var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.FileName);
					//var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", user.Id, fileName);

					// Save the file to the filePath
					//using (var stream = new FileStream(filePath, FileMode.Create))
					//{
					//	await mediaFile.CopyToAsync(stream);
					//}

					string[] filePaths = _fireBaseService.UploadImage(mediaFiles).ToString().Split(new[] { "ygbygyn34897gnygytfrfr" }, StringSplitOptions.RemoveEmptyEntries);
					
					//_mediaRepository.addMedia(filePath);

					foreach (var imgUrl in filePaths)
					{
						Media media = new Media
						{
							Filelocation = imgUrl,
							Date = DateTime.Now
						};
						_mediaRepository.Add(media);

						Metadata metadata = new Metadata
						{
							RecipeId = data.RecipeId,
							MediaId = media.MediaId,
							UserId = user.Id
						};

						// Save the Metadata object to the database
						_metadataRepository.Add(metadata);
					}
				}
				else if (mediaFiles == null || mediaFiles.Count == 0)
				{
					// If there is no media file, just create a new Metadata object
					Metadata metadata = new Metadata
					{
						RecipeId = data.RecipeId,
						UserId = user.Id
					};
					_metadataRepository.Add(metadata);
				}

				// Tags
				if (!string.IsNullOrEmpty(recipe.TagsInput))
				{
					string[] tags = recipe.TagsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
					foreach (string tagName in tags)
					{
						// Check if the tag already exists in the Tag table
						Tag existingTag = _tagRepository.GetByName(tagName.Trim());
						if (existingTag != null)
						{
							// Tag already exists, add a record to RecipeHasTags table
							RecipeHasTag recipeHasTag = new RecipeHasTag
							{
								RecipeId = data.RecipeId,
								TagId = existingTag.TagId
							};
							_recipeHasTagRepository.Add(recipeHasTag);
						}
						else
						{
							// Tag does not exist, create a new tag and add a record to RecipeHasTags table
							Tag newTag = new Tag
							{
								TagName = tagName.Trim()
							};
							_tagRepository.Add(newTag);

							RecipeHasTag recipeHasTag = new RecipeHasTag
							{
								RecipeId = data.RecipeId,
								TagId = newTag.TagId
							};
							_recipeHasTagRepository.Add(recipeHasTag);
						}
					}
				}
				//}
				return RedirectToAction("EditRecipe", "Recipe", new { id = data.RecipeId });
			}
			return View();
		}

		[AllowAnonymous]
		public async Task<IActionResult> RecipeDetail(int id)
		{
			Recipe recipe = _recipeRepository.GetById(id, "public");
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

				var user = await _userManager.GetUserAsync(User);
				if (user != null)
				{
					var checkCollectionSave = _collectionRepository.IsRecipeSaved(id, user.Id);
					if (checkCollectionSave)
					{
						data.Collection = true;
					}
					else
					{
						data.Collection = false;
					}
				}

				return View(data);
			}
			return RedirectToAction("Index", "Home");
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

		public IActionResult EditRecipe(int id)
		{
			Recipe recipe = _recipeRepository.GetByIdForEdit(id);
			if (recipe != null)
			{
				RecipeData recipeData = ConvertToRecipeData(recipe);
				return View(recipeData);
			}
			else
			{
				return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = id });
			}
		}

		private RecipeData ConvertToRecipeData(Recipe recipe)
		{
			RecipeData data = new RecipeData();
			data.RecipeId = recipe.RecipeId;
			data.Title = recipe.Title;
			data.Description = recipe.Description;
			data.Status = recipe.Status;
			data.Image = recipe.Image;
            if (recipe.AvgRate == null)
            {
                recipe.AvgRate = 0.0;
            }
            data.AvgRate = recipe.AvgRate;
			List<Ingredient> ingredients = _ingredientRepository.GetByRecipeId(recipe.RecipeId);
			List<Direction> directions = _directionRepository.GetByRecipeId(recipe.RecipeId);
			List<Tag> tags = _recipeHasTagRepository.GetTagsByRecipeId(recipe.RecipeId);

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
		public async Task<IActionResult> EditRecipe(RecipeData recipe, string Action, IFormFile mediaFile)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				Recipe existingRecipe = _recipeRepository.GetByIdForEdit(recipe.RecipeId);
				if (existingRecipe != null)
				{
					if (Action == "save")
					{
						// Update the recipe details
						existingRecipe.Title = string.IsNullOrEmpty(recipe.Title) ? "null" : recipe.Title;
						existingRecipe.Description = string.IsNullOrEmpty(recipe.Description) ? "Wishing you a delicious and satisfying meal!" : recipe.Description;
						existingRecipe.Date = DateTime.Now;

						// Update the recipe status
						existingRecipe.Status = recipe.Status;

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

						// Update the tags


						_recipeRepository.Update(existingRecipe);
						return RedirectToAction("RecipeDetail", "Recipe", new { id = existingRecipe.RecipeId });

					}
					else if (Action == "trash")
					{
						existingRecipe.Status = "trash";
						_recipeRepository.Update(existingRecipe);
					}
				}
				else
				{
					return RedirectToAction("Index", "Home");
				}
			}
			else
			{
				return View(recipe);
			}

			return RedirectToAction("UserNotFound");
		}

	}
}
