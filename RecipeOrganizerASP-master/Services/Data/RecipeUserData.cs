using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
	public class RecipeUserData
	{
		//public List<Recipe> Recipes { get; set; } = new List<Recipe>();

		public int RecipeId { get; set; }
		public string Title { get; set; } = null!;
		public DateTime Date { get; set; }
		public string Status { get; set; } = null!;
		public double? AvgRate { get; set; }
		public string? Image { get; set; }
		public bool Collection { get; set; }
	
	}
}
