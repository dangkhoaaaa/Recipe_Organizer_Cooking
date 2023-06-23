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
		public string Status { get; set; } = null!;
		public string IngredientsInput { get; set; }
		public List<Ingredient> Ingredients { get; set; }
		public string DirectionsInput { get; set; }
		public List<Direction> Directions { get; set; }
		public string TagsInput { get; set; }
		public List<Tag> Tags { get; set; }
		public List<Media> MediaFiles { get; set; }
		public List<Category> categories { get; set; }
		public double? AvgRate { get; set; }
		public string? Image { get; set; }
		public bool Collection { get; set; }

	}
}
