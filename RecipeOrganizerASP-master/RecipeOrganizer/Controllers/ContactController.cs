using Microsoft.AspNetCore.Mvc;
using Services.Data;
using Services.Models;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
	public class ContactController : Controller
	{
		public ContactRepository _contactFormModel = new ContactRepository(); 

		public IActionResult SendContact()
		{
			return View();
		}
       
        [HttpPost]
		public ActionResult SendEmail(ShowContact model)
		{
            model.Contact.Date = DateTime.Now;
            model.ErrorNum = _contactFormModel.CheckForm(model.Contact);

            if (model.ErrorNum.Count > 0)
            {
                // If there are errors, show the error modal
                ModelState.AddModelError("", "There are errors in the form.");
            }
            else
            {
                // Reset the name field only if there are no errors
                model.Contact.Name = "";
                model.Contact.Email = "";
                model.Contact.Address = "";
                model.Contact.Message = "";
            }

            return View("SendContact", model);
        }

			
	}
}
