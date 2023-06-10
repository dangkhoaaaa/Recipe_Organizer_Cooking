using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using Services.Data;

namespace RecipeOrganizer.Controllers
{
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
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
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
					data.Title = recipe.Title;
					data.Description = recipe.Description;
					data.Date = DateTime.Now;
					data.NumberShare = recipe.NumberShare;
					data.Status = recipe.Status;
					_recipeRepository.Add(data);

					// Ingredients
					if (!string.IsNullOrEmpty(recipe.IngredientsInput))
					{
						string[] ingredients = recipe.IngredientsInput.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
						foreach (var ingredientName in ingredients)
						{
							Ingredient ingredient = new Ingredient
							{
								RecipeId = data.RecipeId,
								IngredientName = ingredientName
							};
							_ingredientRepository.Add(ingredient);
						}
					}

					// Directions
					if (!string.IsNullOrEmpty(recipe.DirectionsInput))
					{
						string[] steps = recipe.DirectionsInput.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
						for (int i = 0; i < steps.Length; i++)
						{
							Direction direction = new Direction
							{
								RecipeId = data.RecipeId,
								Step = i + 1,
								Direction1 = steps[i]
							};
							_directionRepository.Add(direction);
						}
					}

					// Tags
					if (!string.IsNullOrEmpty(recipe.TagsInput))
					{
						string[] tags = recipe.TagsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						foreach (string tagName in tags)
						{
							// Check if the tag already exists in the Tag table
							Tag existingTag = _tagRepository.GetByName(tagName.Trim());
							if (existingTag == null)
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
									RecipeId = recipe.RecipeId,
									TagId = newTag.TagId
								};
								_recipeHasTagRepository.Add(recipeHasTag);
							}
						}
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
				}
			//}
			return View(recipe);
		}
	}
}
