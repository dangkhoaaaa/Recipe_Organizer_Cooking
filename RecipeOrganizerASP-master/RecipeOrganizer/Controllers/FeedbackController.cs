using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using RecipeOrganizer.Areas.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private readonly FeedbackRepository _feedbackRepository;
        private readonly MetadataRepository _metadataRepository;
        private readonly MediaRepository _mediaRepository;
        private readonly UserManager<AppUser> _userManager;

        public FeedbackController(UserManager<AppUser> userManager)
        {
            _feedbackRepository = new FeedbackRepository();
            _metadataRepository = new MetadataRepository();
            _mediaRepository = new MediaRepository();
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ICollection<Feedback> Products { get; set; } = new List<Feedback>();

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
    }


}
