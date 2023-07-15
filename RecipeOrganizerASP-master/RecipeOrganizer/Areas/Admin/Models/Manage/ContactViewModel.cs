namespace RecipeOrganizer.Areas.Admin.Models.Manage
{
	public class ContactViewModel
	{
		public int ContactId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Message { get; set; }
		public DateTime Date { get; set; }
		public bool IsRead { get; set; }
	}
}
