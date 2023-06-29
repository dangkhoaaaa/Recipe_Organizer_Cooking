using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
	public class RecipeData
	{
		public int RecipeId { get; set; }
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DateTime Date { get; set; }
		public int NumberShare { get; set; }
		public string? Status { get; set; } = null!;
		public string? IngredientsInput { get; set; }
		public List<Ingredient>? Ingredients { get; set; }
		public string? DirectionsInput { get; set; }
		public List<Direction>? Directions { get; set; }
		public string? TagsInput { get; set; }
		public List<Tag>? Tags { get; set; }
		public string? Img { get; set; }
		public List<string>? Imgs { get; set; }
		public List<int> CategoryInput { get; set; }
		public List<int> SelectedCategories { get; set; }
		public List<Category>? Categories { get; set; }
		public double? AvgRate { get; set; }
		public bool Collection { get; set; }
		public string? Author { get; set; }

	}
}
