using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using Services.Data;
using Microsoft.AspNetCore.Authorization;

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
		public async Task<IActionResult> AddNewRecipe(RecipeData recipe, IFormFile mediaFile)
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
					data.Title = recipe.Title;
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
				if (mediaFile != null && mediaFile.Length > 0)
				{
					// Save the file to the server
					var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", user.Id, fileName);

					// Save the file to the filePath
					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await mediaFile.CopyToAsync(stream);
					}

					// Create a new Media object and assign values to the fields
					Media media = new Media
					{
						Filelocation = filePath,
						Date = DateTime.Now
					};

					// Save the Media object to the database
					_mediaRepository.Add(media);

					// Create a new Metadata object and assign values to the fields
					Metadata metadata = new Metadata
					{
						RecipeId = data.RecipeId,
						MediaId = media.MediaId,
						UserId = user.Id
					};

					// Save the Metadata object to the database
					_metadataRepository.Add(metadata);
				}
				else
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
		public ActionResult RecipeDetail(int id)
		{
			Recipe recipe = _recipeRepository.GetById(id);
			if (recipe != null)
			{
				RecipeData data = new RecipeData();
				data.Title = recipe.Title;
				data.Description = recipe.Description;
				data.Status = recipe.Status;
				data.Date = recipe.Date;
				List<Ingredient> ingredient = _ingredientRepository.GetByRecipeId(recipe.RecipeId);
				data.Ingredients = ingredient;
				List<Direction> direction = _directionRepository.GetByRecipeId(recipe.RecipeId);
				data.Directions = direction;

				ViewBag.CollectionRepository = _collectionRepository;

				return View(data);
			}
			return RedirectToAction("Index", "Home");
		}

		public IActionResult EditRecipe(int id)
		{
			Recipe recipe = _recipeRepository.GetById(id);
			if (recipe != null)
			{
				RecipeData recipeData = ConvertToRecipeData(recipe);
				return View(recipeData);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		[HttpPost]
		public async Task<IActionResult> EditRecipe(RecipeData recipe, string Action, IFormFile mediaFile)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				if (Action == "save")
				{
					Recipe existingRecipe = _recipeRepository.GetById(recipe.RecipeId);
					if (existingRecipe != null)
					{
						// Update the recipe details
						existingRecipe.Title = string.IsNullOrEmpty(recipe.Title) ? "null" : recipe.Title;
						existingRecipe.Description = string.IsNullOrEmpty(recipe.Description) ? "Wishing you a delicious and satisfying meal!" : recipe.Description;
						existingRecipe.Date = DateTime.Now;

						// Update the recipe status


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

						//// Update the media
						//if (mediaFile != null && mediaFile.Length > 0)
						//{
						//	// Save the file to the server
						//	var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.FileName);
						//	var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", user.Id, fileName);

						//	// Save the file to the filePath
						//	using (var stream = new FileStream(filePath, FileMode.Create))
						//	{
						//		await mediaFile.CopyToAsync(stream);
						//	}

						//	// Check if the recipe already has a media file
						//	Media existingMedia = _mediaRepository.GetByRecipeId(existingRecipe.RecipeId);
						//	if (existingMedia != null)
						//	{
						//		// Update the existing media file
						//		existingMedia.Filelocation = filePath;
						//		existingMedia.Date = DateTime.Now;
						//		_mediaRepository.Update(existingMedia);
						//	}
						//	else
						//	{
						//		// Create a new Media object and assign values to the fields
						//		Media media = new Media
						//		{
						//			Filelocation = filePath,
						//			Date = DateTime.Now
						//		};

						//		// Save the Media object to the database
						//		_mediaRepository.Add(media);

						//		// Create a new Metadata object and assign values to the fields
						//		Metadata metadata = new Metadata
						//		{
						//			RecipeId = existingRecipe.RecipeId,
						//			MediaId = media.MediaId,
						//			UserId = user.Id
						//		};

						//		// Save the Metadata object to the database
						//		_metadataRepository.Add(metadata);
						//	}
						//}

						//// Update the tags
						//if (!string.IsNullOrEmpty(recipe.TagsInput))
						//{
						//	string[] tags = recipe.TagsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						//	List<int> existingTagIds = _tagRepository.GetTagIdsByRecipeId(existingRecipe.RecipeId);

						//	foreach (string tagName in tags)
						//	{
						//		// Check if the tag already exists in the Tag table
						//		Tag existingTag = _tagRepository.GetByName(tagName.Trim());
						//		if (existingTag != null)
						//		{
						//			// Tag already exists, add/update a record to RecipeHasTags table
						//			if (existingTagIds.Contains(existingTag.TagId))
						//			{
						//				// Update the existing record
						//				RecipeHasTag existingRecipeHasTag = _recipeHasTagRepository.GetByRecipeIdAndTagId(existingRecipe.RecipeId, existingTag.TagId);
						//				if (existingRecipeHasTag != null)
						//				{
						//					// Update the existing record
						//					existingRecipeHasTag.TagId = existingTag.TagId;
						//					_recipeHasTagRepository.Update(existingRecipeHasTag);
						//				}
						//			}
						//			else
						//			{
						//				// Add a new record
						//				RecipeHasTag recipeHasTag = new RecipeHasTag
						//				{
						//					RecipeId = existingRecipe.RecipeId,
						//					TagId = existingTag.TagId
						//				};
						//				_recipeHasTagRepository.Add(recipeHasTag);
						//			}
						//		}
						//		else
						//		{
						//			// Tag does not exist, create a new tag and add a record to RecipeHasTags table
						//			Tag newTag = new Tag
						//			{
						//				TagName = tagName.Trim()
						//			};
						//			_tagRepository.Add(newTag);

						//			RecipeHasTag recipeHasTag = new RecipeHasTag
						//			{
						//				RecipeId = existingRecipe.RecipeId,
						//				TagId = newTag.TagId
						//			};
						//			_recipeHasTagRepository.Add(recipeHasTag);
						//		}
						//	}

						//	// Remove tags that are not included in the updated tags
						//	List<int> updatedTagIds = _tagRepository.GetTagIdsByRecipeId(existingRecipe.RecipeId);
						//	List<int> tagsToRemove = existingTagIds.Except(updatedTagIds).ToList();
						//	foreach (int tagId in tagsToRemove)
						//	{
						//		RecipeHasTag recipeHasTag = _recipeHasTagRepository.GetByRecipeIdAndTagId(existingRecipe.RecipeId, tagId);
						//		if (recipeHasTag != null)
						//		{
						//			_recipeHasTagRepository.Remove(recipeHasTag);
						//		}
						//	}
						//}

						_recipeRepository.Update(existingRecipe);
						return RedirectToAction("RecipeDetail", "Recipe", new { id = existingRecipe.RecipeId });
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else if (Action == "trash")
				{
					// Xử lý khi nhấn nút "Trash"
				}
			}
			else
			{
				return View(recipe);
			}

			return RedirectToAction("UserNotFound");
		}

		private RecipeData ConvertToRecipeData(Recipe recipe)
		{
			RecipeData data = new RecipeData();
			data.RecipeId = recipe.RecipeId;
			data.Title = recipe.Title;
			data.Description = recipe.Description;
			List<Ingredient> ingredients = _ingredientRepository.GetByRecipeId(recipe.RecipeId);
			List<Direction> directions = _directionRepository.GetByRecipeId(recipe.RecipeId);

			if (ingredients != null)
			{
				data.Ingredients = ingredients.ToList();
			}

			if (directions != null)
			{
				data.Directions = directions.ToList();
			}

			return data;
		}

		[HttpPost]
		public IActionResult ToggleCollection(int recipeId, int userId)
		{
			_collectionRepository.ToggleCollection(recipeId, userId);

			return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = recipeId });
		}



	}
}
