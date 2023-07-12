using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Components
{
	public class LoadFeedback : ViewComponent
	{

		private readonly FeedbackRepository _feedbackRepository;

		public LoadFeedback()
		{
			_feedbackRepository = new FeedbackRepository();

		}
		public IViewComponentResult Invoke(int RecipeId)
		{
			List<FeedBackOnOnceRecipeModel> feedbacks = new List<FeedBackOnOnceRecipeModel>();
			feedbacks = _feedbackRepository.GetAllFeedBackByRecipe(RecipeId);
			return View(feedbacks);
		}
	}
}
