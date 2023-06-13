using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
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
    }
}
