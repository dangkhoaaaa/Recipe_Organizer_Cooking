using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    public class TagController : Controller
    {
        private readonly RecipeHasTagRepository _recipeHasTagRepository;
        private readonly TagRepository _tagRepository;
        private readonly UserManager<AppUser> _userManager;

        public TagController(UserManager<AppUser> userManager)
        {
            _recipeHasTagRepository = new RecipeHasTagRepository();
            _tagRepository = new TagRepository();
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
        public async Task<IActionResult> AddNewRecipe(string TagsInput, int recipeId)
        {
            //if (ModelState.IsValid)
            //{
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Tags
                if (!string.IsNullOrEmpty(TagsInput))
                {
                    string[] tags = TagsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string tagName in tags)
                    {
                        // Check if the tag already exists in the Tag table
                        Tag existingTag = _tagRepository.GetByName(tagName.Trim());
                        if (existingTag == null)
                        {
                            // Tag already exists, add a record to RecipeHasTags table
                            RecipeHasTag recipeHasTag = new RecipeHasTag
                            {
                                RecipeId = recipeId,
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
                                RecipeId = recipeId,
                                TagId = newTag.TagId
                            };
                            _recipeHasTagRepository.Add(recipeHasTag);
                        }
                    }
                }
            }
            //}
            return View();
        }
    }
}
