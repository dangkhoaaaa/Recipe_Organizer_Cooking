using Services.Data;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public static class RecipeServices
    {
        private static readonly IngredientRepository _ingredientRepository = new IngredientRepository();
        private static readonly DirectionRepository _directionRepository = new DirectionRepository();
        private static readonly RecipeHasCategoryRepository _recipeHasCategoryRepository = new RecipeHasCategoryRepository();
        private static readonly RecipeHasTagRepository _recipeHasTagRepository = new RecipeHasTagRepository();
        private static readonly MediaRepository _mediaRepository = new MediaRepository();

        public static RecipeData ConvertToRecipeData(Recipe recipe)
        {
            RecipeData data = new RecipeData();
            data.RecipeId = recipe.RecipeId;
            data.Title = recipe.Title;
            data.Description = recipe.Description;
            data.Status = recipe.Status;
            data.Img = recipe.Image;
            data.NumberShare = recipe.NumberShare;
            data.Imgs = GetImgs(recipe.RecipeId);

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

        private static List<string> GetImgs(int id)
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
