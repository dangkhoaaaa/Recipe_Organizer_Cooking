using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostApiController : ControllerBase
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

		public PostApiController(UserManager<AppUser> userManager)
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


		[Produces("application/json")]
		[HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var postTitle = _recipeRepository.getListTitleRecipeByKeyword(term);
                return Ok(postTitle);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
		[HttpGet("search1")]
        public IActionResult Search1()
		{

			try
			{

				string term = HttpContext.Request.Query["term"].ToString();
				var postTitle = _recipeRepository.getListTitleRecipeByKeyword(term);
				//  var postTitle = new string[] { "Iphone", "Samsung", "Nokia" };


				return Ok(postTitle);

			}
			catch
			{

				return BadRequest();
			}




		}
	}
}
