using Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		public virtual Media FilePath { get; set; }
		public List<Media> medias { get; set; }
		public List<Category> categories { get; set; }

		[Required(ErrorMessage = "Please enter a title for your feedback.")]
		public string TitleFb { get; set; }

		[Required(ErrorMessage = "Please enter your feedback.")]
		public string DescriptionFb { get; set; }

		[Required(ErrorMessage = "Please enter your user name.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Please enter your email address.")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please rate this recipe.")]
		[Range(1, 5, ErrorMessage = "Please enter a rating between 1 and 5.")]
		public int Rating { get; set; }

		[Required(ErrorMessage = "")]
		public bool StatusFb { get; set; }
		public int FeedbackId { get; set; }
		public int UserId { get; set; }
	}
}
