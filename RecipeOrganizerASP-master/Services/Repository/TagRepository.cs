using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class TagRepository : RepositoryBase<Tag>
	{
		public Tag? GetByName(string tagName)
		{
			return _dbSet.FirstOrDefault(t => t.TagName == tagName);
		}
		public Tag? GetById(int id)
		{
			return _dbSet.Where(d => d.TagId == id).FirstOrDefault();
		}

		public void AddTags(string tagsInput, int recipeId)
		{
			string[] tags = tagsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string tagName in tags)
			{
				string trimmedTagName = tagName.Trim();

				// Check if the tag already exists in the Tag table
				Tag existingTag = GetByName(trimmedTagName);
				if (existingTag != null)
				{
					// Create an instance of RecipeHasTagRepository
					var recipeHasTagRepository = new RecipeHasTagRepository();

					// Tag already exists, add a record to RecipeHasTags table
					recipeHasTagRepository.AddByRecipeId(recipeId, existingTag.TagId);
				}
				else
				{
					// Tag does not exist, create a new tag and add a record to RecipeHasTags table
					Tag newTag = new Tag
					{
						TagName = trimmedTagName
					};
					Add(newTag);

					// Create an instance of RecipeHasTagRepository
					var recipeHasTagRepository = new RecipeHasTagRepository();

					recipeHasTagRepository.AddByRecipeId(recipeId, newTag.TagId);
				}
			}
		}

		public void UpdateTags(string tagsInput, int recipeId)
		{
			if (recipeId != 0)
			{
				// Remove existing tags associated with the recipe
				var recipeHasTagRepository = new RecipeHasTagRepository();
				recipeHasTagRepository.DeleteByRecipeId(recipeId);

				// Add new tags
				AddTags(tagsInput, recipeId);
			}
		}


	}
}
