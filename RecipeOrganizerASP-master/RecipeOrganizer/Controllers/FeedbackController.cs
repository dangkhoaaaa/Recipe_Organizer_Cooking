﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;

using RecipeOrganizer.Areas.Identity.Models.Manage;

using RecipeOrganizer.Areas.Data;

using Services.Models;
using Services.Models.Authentication;
using Services.Repository;
using System.Net;
using Microsoft.VisualBasic;
using Services;

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
		private readonly NotificationRepository _notificationRepository;
		private readonly FireBaseService _fireBaseService;

		public FeedbackController(UserManager<AppUser> userManager)
		{
			_feedbackRepository = new FeedbackRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_recipeRepository = new RecipeRepository();
			_userManager = userManager;
			_fireBaseService = new FireBaseService();
			_notificationRepository = new NotificationRepository();
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

		[HttpPost]
		public async Task<IActionResult> AddFeedback(Feedback feedback, int id, List<IFormFile> files)
		{
			var user = await _userManager.GetUserAsync(User);
			if (user != null)
			{
				if (string.IsNullOrEmpty(feedback.Title) || string.IsNullOrEmpty(feedback.Description))
				{
					ModelState.AddModelError(string.Empty, "Please enter a title and description.");
					return View(feedback);
				}
				bool hasReviewed = _metadataRepository.IsReviewed(id, user.Id);
				if (!hasReviewed)
				{
					// Create a new instance of Feedback
					var newFeedback = new Feedback
					{
						Title = feedback.Title,
						Description = feedback.Description,
						Date = DateTime.Now,
						Rating = feedback.Rating,
						Status = true
					};

					// Save the new feedback to the database
					_feedbackRepository.Add(newFeedback);

					int notificationId = _notificationRepository.addNotification("New review has been added");

					// Media
					if (files != null && files.Count > 0)
					{
						var imageLinkTask = _fireBaseService.UploadImageSingle(files);
						var imageLink = await imageLinkTask;
						int mediaId = _mediaRepository.addMedia(imageLink);

						Metadata metadata = new Metadata
						{
							RecipeId = id,
							UserId = user.Id,
							MediaId = mediaId,
							FeedbackId = newFeedback.FeedbackId,
							NotificationId = notificationId
						};
						_metadataRepository.Add(metadata);
					}
					else
					{
						// If there is no media file, just create a new Metadata object
						Metadata metadata = new Metadata
						{
							RecipeId = id,
							UserId = user.Id,
							FeedbackId = newFeedback.FeedbackId,
							NotificationId = notificationId
						};
						_metadataRepository.Add(metadata);
					}

					// Redirect to a success page or perform other actions
					return RedirectToAction("RecipeDetail", "Recipe", new { id }); // Redirect to a success page
				}
				else
				{
				}
			}

			return RedirectToAction("Index", "Recipe");
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

		//[HttpPost]
		//public async Task<IActionResult> AddFeedback(FeedbackViewModel feedbackViewModel)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await _userManager.FindByEmailAsync(User.Identity.Name);

		//		if (user.UserName != feedbackViewModel.UserName || user.Email != feedbackViewModel.Email)
		//		{
		//			ModelState.AddModelError(string.Empty, "Fullname or email does not match the ones in your Personal Profile.");
		//			return View(feedbackViewModel);
		//		}

		//		var feedback = new Feedback
		//		{
		//			Title = feedbackViewModel.Title,
		//			Description = feedbackViewModel.Description,
		//			Date = DateTime.Now,
		//			Rating = feedbackViewModel.Rating,
		//			Status = feedbackViewModel.Status,
		//		};

		//		_feedbackRepository.Add(feedback);

		//		var listRecipe = _recipeRepository.GetAll().ToList();

		//		var metadata = new Metadata
		//		{
		//			UserId = user.Id,
		//			RecipeId = feedbackViewModel.RecipeId,
		//			FeedbackId = feedback.FeedbackId,
		//		};

		//		_metadataRepository.Add(metadata);

		//		await _context.SaveChangesAsync();

		//		TempData["SuccessMessage"] = "Thank you for your feedback! We will review it shortly.";

		//		return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = feedbackViewModel.RecipeId });
		//	}

		//	return View(feedbackViewModel);
		//}


		//[HttpPost]
		//public async Task<IActionResult> AddFeedback(FeedbackViewModel feedbackViewModel)
		//{
		//    if (ModelState.IsValid)
		//    {
		//        var user = await _userManager.FindByEmailAsync(User.Identity.Name);

		//        if (user.UserName != feedbackViewModel.UserName || user.Email != feedbackViewModel.Email)
		//        {
		//            ModelState.AddModelError(string.Empty, "Username or email does not match the ones in your Personal Profile.");
		//            return View(feedbackViewModel);
		//        }

		//        var feedback = new Feedback
		//        {
		//            Title = feedbackViewModel.Title,
		//            Description = feedbackViewModel.Description,
		//            Date = DateTime.Now,
		//            Rating = feedbackViewModel.Rating,
		//            Status = feedbackViewModel.Status,
		//        };

		//        _feedbackRepository.Add(feedback);

		//        var listRecipe = _recipeRepository.GetAll().ToList();

		//        var metadata = new Metadata
		//        {
		//            UserId = user.Id,
		//            RecipeId = feedbackViewModel.RecipeId,
		//            FeedbackId = feedback.FeedbackId,
		//        };

		//        _metadataRepository.Add(metadata);

		//        await _context.SaveChangesAsync();

		//        TempData["SuccessMessage"] = "Thank you for your feedback! We will review it shortly.";

		//        return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = feedbackViewModel.RecipeId });
		//    }

		//    return View(feedbackViewModel);
		//}


		//[HttpPost]
		//public async Task<IActionResult> AddFeedback1( var user,  )
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await _userManager.FindByEmailAsync(User.Identity.Name);

		//		if (user.UserName != feedbackViewModel.UserName || user.Email != feedbackViewModel.Email)
		//		{
		//			ModelState.AddModelError(string.Empty, "Username or email does not match the ones in your Personal Profile.");
		//			return View(feedbackViewModel);
		//		}

		//		var feedback = new Feedback
		//		{
		//			Title = feedbackViewModel.Title,
		//			Description = feedbackViewModel.Description,
		//			Date = DateTime.Now,
		//			Rating = feedbackViewModel.Rating,
		//			Status = feedbackViewModel.Status,
		//		};

		//		_feedbackRepository.Add(feedback);

		//		var listRecipe = _recipeRepository.GetAll().ToList();

		//		var metadata = new Metadata
		//		{
		//			UserId = user.Id,
		//			RecipeId = feedbackViewModel.RecipeId,
		//			FeedbackId = feedback.FeedbackId,
		//		};

		//		_metadataRepository.Add(metadata);

		//		await _context.SaveChangesAsync();

		//		TempData["SuccessMessage"] = "Thank you for your feedback! We will review it shortly.";

		//		return RedirectToAction("RecipeDetail", "Recipe", new { recipeId = feedbackViewModel.RecipeId });
		//	}

		//	return View(feedbackViewModel);
		//}



	}
}
