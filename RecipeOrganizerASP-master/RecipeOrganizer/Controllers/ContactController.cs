using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
	public class ContactController : Controller
	{

		public IActionResult SendContact()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SendEmail(ContactFormModel model)
		{
			if (ModelState.IsValid)
			{
				// Access the form data from the model
				string name = model.Name;
				string email = model.Email;
				string address = model.Address;
				string message = model.Message;

				// Send email using your preferred email sending method or library

				return RedirectToAction("Success"); // Redirect to a success page
			}

			// If the model state is not valid, return to the same view to show validation errors
			return View("SendContact", model);
		}
	}
}
