using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
    public class MediaController : Controller
    {
        private readonly MetadataRepository _metadataRepository;
        private readonly MediaRepository _mediaRepository;
        private readonly UserManager<AppUser> _userManager;

        public MediaController(UserManager<AppUser> userManager)
        {
            _metadataRepository = new MetadataRepository();
            _mediaRepository = new MediaRepository();
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddMedia()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMedia(IFormFile mediaFile, int recipe_Id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (mediaFile != null && mediaFile.Length > 0)
                {
                    // Save the file to the server
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", user.Id, fileName);

                    // Save the file to the filePath
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await mediaFile.CopyToAsync(stream);
                    }

                    // Create a new Media object and assign values to the fields
                    Media media = new Media
                    {
                        Filelocation = filePath,
                        Date = DateTime.Now
                    };

                    // Save the Media object to the database
                    _mediaRepository.Add(media);

                    // Create a new Metadata object and assign values to the fields
                    Metadata metadata = new Metadata
                    {
                        RecipeId = recipe_Id,
                        MediaId = media.MediaId,
                        UserId = user.Id
                    };

                    // Save the Metadata object to the database
                    _metadataRepository.Add(metadata);
                }
                else
                {
                    // If there is no media file, just create a new Metadata object
                    Metadata metadata = new Metadata
                    {
                        RecipeId = recipe_Id,
                        UserId = user.Id
                    };
                    _metadataRepository.Add(metadata);
                }
            }
            return View();
        }
    }
}