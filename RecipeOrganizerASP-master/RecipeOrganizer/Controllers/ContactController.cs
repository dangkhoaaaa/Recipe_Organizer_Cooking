using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Models;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
	public class ContactController : Controller
	{
		public ContactFormModel _contactFormModel = new ContactFormModel(); 

		public IActionResult SendContact()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SendEmail(ShowContact model)
		{
			model.Contact.Date = DateTime.Now;
			model.ErrorNum = _contactFormModel.CheckForm(model.Contact);

			return View("SendContact", model);
		}
	}
}
