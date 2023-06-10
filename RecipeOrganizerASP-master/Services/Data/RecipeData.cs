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
		public int NumberShare { get; set; }
		public string Status { get; set; } = null!;

		public string IngredientsInput { get; set; }

		public string DirectionsInput { get; set; }
		public string TagsInput { get; set; }

		public virtual Media FilePath { get; set; }




	}
}
