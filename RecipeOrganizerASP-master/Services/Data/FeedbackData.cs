using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
	public class FeedbackData
	{
		//public List<Feedback>? feedbacks { get; set; }
		public int FeedbackId { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public DateTime Date { get; set; }
		public int Rating { get; set; }
		public List<Media>? Images { get; set; }
		public bool Status { get; set; }
		public int RecipeId { get; set; }
		public string RecipeTitle { get; set; }
	}
}
