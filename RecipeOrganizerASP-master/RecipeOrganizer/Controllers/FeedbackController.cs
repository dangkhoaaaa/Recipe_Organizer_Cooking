using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;


using RecipeOrganizer.Areas.Data;

using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using System.Net;
using Services.Data;
using System.Data.SqlClient;

namespace RecipeOrganizer.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly FeedbackRepository _feedbackRepository;
        private readonly MetadataRepository _metadataRepository;
        private readonly MediaRepository _mediaRepository;
        private readonly UserManager<AppUser> _userManager;
		private readonly RecipeRepository _recipeRepository;
		private readonly DbContext _context;
		


		public FeedbackController(UserManager<AppUser> userManager)
        {
            _feedbackRepository = new FeedbackRepository();
            _metadataRepository = new MetadataRepository();
            _mediaRepository = new MediaRepository();
			_recipeRepository = new RecipeRepository();
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ICollection<Metadata> Products { get; set; } = new List<Metadata>();

        public async Task<IActionResult> UserFeedback()
        {
			var listFeedback = _feedbackRepository.GetAll().ToList();
			var listMetadata = _metadataRepository.GetAll().ToList();
			var listMedia = _mediaRepository.GetAll().ToList();
			// Get the user ID of the logged-in user
			var user = await _userManager.GetUserAsync(User);
            string UserId = user.Id;
			// Retrieve the feedback data for the user ID
			var feedbackData = (from f in listFeedback
								join m in listMetadata on f.FeedbackId equals m.FeedbackId
								join mm in listMedia on m.MediaId equals mm.MediaId
								where m.UserId == UserId
								select new Feedback
								{
									FeedbackId = f.FeedbackId,
									Title = f.Title,
									Description = f.Description,
									Date = f.Date,
									Rating = f.Rating,
									Status = f.Status,

								}).ToList();
                return View(feedbackData);
            
        }

        //	public async Task<IActionResult> EditUserFeedback(int? id)
        //	{
        //		var listFeedback = _feedbackRepository.GetAll().ToList();
        //		var listMetadata = _metadataRepository.GetAll().ToList();
        //		var listMedia = _mediaRepository.GetAll().ToList();
        //		var user = await _userManager.GetUserAsync(User);
        //		string UserId = user.Id;
        //		// Retrieve the feedback data from the database
        //		var feedback = listFeedback.FirstOrDefault(f => f.FeedbackId == id);

        //		// Check if the feedback exists and is associated with the current user
        //		if (feedback == null)
        //		{
        //			return HttpNotFound();
        //		}
        //		else if (feedback.MetaData.UserId != User.Identity.GetUserId())
        //		{
        //			return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        //		}

        //		// Create a view model that includes the feedback data and pass it to the view
        //		UpdateFeedbackViewModel model = new UpdateFeedbackViewModel();
        //		model.FeedbackId = feedback.feedback_id;
        //		model.Title = feedback.Title;
        //		model.Description = feedback.Description;
        //		model.Rating = feedback.Rating;
        //		return View(model);
        //	}

        //	private IActionResult HttpNotFound()
        //	{
        //		throw new NotImplementedException();
        //	}


        [HttpPost]
        public async Task<IActionResult> AddFeedback(RecipeData recipeFb)
        {
            
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);

			//if (user.UserName != recipeFb.UserName || user.Email != recipeFb.Email)
			//{
			//    ModelState.AddModelError(string.Empty, "Username or email does not match the ones in your Personal Profile.");
			//    return View(recipeFb);
			//}

				// Check if user has already given feedback for this recipe
				if (HasFeedback(recipeFb.FeedbackId, recipeFb.UserId))
				{
					ViewBag.Message = "You have already given feedback for this recipe.";
					return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = recipeFb.RecipeId });
			}

				var feedback = new Feedback
                {
                    Title = recipeFb.TitleFb,
                    Description = recipeFb.DescriptionFb,
                    Date = DateTime.Now,
                    Rating = recipeFb.Rating,
                    Status = recipeFb.StatusFb,
                };

                _feedbackRepository.Add(feedback);

                var listRecipe = _recipeRepository.GetAll().ToList();

                var metadata = new Metadata
                {
                    UserId = user.Id,
                    RecipeId = recipeFb.RecipeId,
                    FeedbackId = feedback.FeedbackId,
                };

                _metadataRepository.Add(metadata);

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thank you for your feedback! We will review it shortly.";

                return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = recipeFb.RecipeId });
    
		}


		private bool HasFeedback(int recipeId, int userId)
		{
			string connectionString = "server=.; database=Recipe_Organizer;uid=sa;pwd=12345;TrustServerCertificate=True;";
			string selectQuery = "SELECT COUNT(*) FROM MetaData WHERE recipe_id = @RecipeId AND user_id = @UserId";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(selectQuery, connection);
				command.Parameters.AddWithValue("@RecipeId", recipeId);
				command.Parameters.AddWithValue("@UserId", userId);

				connection.Open();
				int result = (int)command.ExecuteScalar();
				if (result > 0)
				{
					return true;
				}
				return false;
			}
		}

		private bool CheckUserProfile(int userId, string username, string email)
		{
			// Check if username and email match with the user's personal profile
			string connectionString = "server=.; database=Recipe_Organizer;uid=sa;pwd=12345;TrustServerCertificate=True;";
			string selectQuery = "SELECT COUNT(*) FROM AspNetUsers WHERE Id = @UserId AND UserName = @Username AND Email = @Email";

			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand command = new SqlCommand(selectQuery, connection);
				command.Parameters.AddWithValue("@UserId", userId);
				command.Parameters.AddWithValue("@Username", username);
				command.Parameters.AddWithValue("@Email", email);

				connection.Open();
				int result = (int)command.ExecuteScalar();
				if (result > 0)
				{
					return true;
				}
				return false;
			}
		}
	}
}
