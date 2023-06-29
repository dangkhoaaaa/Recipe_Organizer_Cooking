using Firebase.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RecipeOrganizer.Areas.Admin.Models;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using System.Data;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleName.Administrator)]
    [Area("Admin")]
    [Route("/Admin/[action]")]
    public class CategoryController : Controller
    {
   
      
        private readonly CategoryRepository _categoryRepository;
        private readonly Parent_CategoryRepository _parentCategoryRepository;
        private readonly RecipeHasCategoryRepository _recipeHasCategory;

        public CategoryController(UserManager<AppUser> userManager)
        {

            _parentCategoryRepository = new Parent_CategoryRepository();
            _categoryRepository = new CategoryRepository();
            _recipeHasCategory = new RecipeHasCategoryRepository();

        }


        public IActionResult Index()
        {
            var listCategory = _categoryRepository.GetAll();
            var listParentCategory = _parentCategoryRepository.GetAll();
            List<CategoryViewModel> list = new List<CategoryViewModel>();

            foreach (var user in listCategory)
            {
                var query = from rc in listParentCategory
                            
                            where rc.ParentId == user.ParentId
                            select rc.Title;
                string name = query.FirstOrDefault().ToString();
                var model = new CategoryViewModel
                {
                    category_id = user.CategoryId,
                    parent_id = user.ParentId,
                    title = user.Title,
                    description = user.Description,
                    IMG = user.Image,
                    parent_name = name,
                   
                };
                list.Add(model);
            }
            return View(list);
        }

        public IActionResult AddCategory()
        {
            var listParentCategory = _parentCategoryRepository.GetAll();
            return View(listParentCategory);
        }
        [HttpPost]
        public IActionResult AddCategory(string txtname, string txtdescription, string txtnameparent)
        {
            
            var category = new Services.Models.Category()
            {
                 
                Title = txtname,
                Description = txtdescription,
                ParentId = int.Parse(txtnameparent)
                

            };
            _categoryRepository.Add(category);
            // Lưu Category vào database
            //_dbContext.Categories.Add(category);
           // _dbContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
      
            public IActionResult UpdateCategory( int id , string name)
        {
            ViewBag.CategoryName = name;
            ViewBag.CategoryId = id;
            var listParentCategory = _parentCategoryRepository.GetAll();
            return View(listParentCategory);
        }
        [HttpPost]
        public IActionResult UpdateCategory(string txtid, string txtname, string txtdescription, string txtnameparent)
        {

            var category = new Services.Models.Category()
            {
                CategoryId = int.Parse(txtid),
                Title = txtname,
                Description = txtdescription,
                ParentId = int.Parse(txtnameparent)


            };
            _categoryRepository.Update(category);
            // Lưu Category vào database
            //_dbContext.Categories.Add(category);
            // _dbContext.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
        

             public IActionResult SearchCategory(string keyword)
        {
            List<CategoryViewModel> list = new List<CategoryViewModel>();
            if (keyword != null)
            {
                var listCategory1 = _categoryRepository.GetAll();
                var listParentCategory = _parentCategoryRepository.GetAll();

                var listCategory = from rc in listCategory1

                                   where rc.Title.Contains(keyword)
                                   select rc;
                foreach (var user in listCategory)
                {
                    var query = from rc in listParentCategory

                                where rc.ParentId == user.ParentId
                                select rc.Title;
                    string name = query.FirstOrDefault().ToString();
                    var model = new CategoryViewModel
                    {
                        category_id = user.CategoryId,
                        parent_id = user.ParentId,
                        title = user.Title,
                        description = user.Description,
                        parent_name = name,

                    };
                    list.Add(model);
                }
            }
            else
            {
                ViewBag.NotFind = "NOT HAVE CATEGORY";
            }

            if(list.Count ==0)
            {

                ViewBag.NotFind = "NOT HAVE CATEGORY";

            }
            return View("Index", list);
        }

        public IActionResult DeleteCategory(int id)
        {
            var listRecipeHasCategory = _recipeHasCategory.GetAll();
            var listHaswithID = from rc in listRecipeHasCategory

                                where rc.CategoryId == id
                               select rc;
            if (listHaswithID.Count() > 0)
            {
                foreach (var cateitem in listRecipeHasCategory)
                {

                    _recipeHasCategory.Delete(cateitem);
                }
            }
            var listcategory = _categoryRepository.GetAll();
            var listCategorywithID = (from rc in listcategory

                                     where rc.CategoryId == id
                                select rc).FirstOrDefault();

            if (listCategorywithID != null)
            {
                _categoryRepository.Delete(listCategorywithID);
            }

            return RedirectToAction("Index", "Category");
        }


    }
}
