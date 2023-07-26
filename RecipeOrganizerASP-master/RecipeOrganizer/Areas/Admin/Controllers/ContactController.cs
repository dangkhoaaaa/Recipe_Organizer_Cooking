using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Areas.Admin.Controllers
{
	[Authorize]
	public class ContactController : Controller
	{
		private readonly ILogger<ManageController> _logger;
		private readonly ContactRepository _contactRepository;



		public ContactController(
		ILogger<ManageController> logger
		)
		{
			_logger = logger;
			_contactRepository = new ContactRepository();
		}

		public IActionResult DeleteContact(int contactID)
		{
			try
			{
				string.IsNullOrEmpty(contactID + "");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return RedirectToAction("Contact", "Admin");
			}
			var contactDeleted = _contactRepository.GetAll().Where(p => p.ContactId.Equals(contactID)).FirstOrDefault();
			_contactRepository.Delete(contactDeleted);
			return RedirectToAction("Contact", "Admin");
        }
	}
}
