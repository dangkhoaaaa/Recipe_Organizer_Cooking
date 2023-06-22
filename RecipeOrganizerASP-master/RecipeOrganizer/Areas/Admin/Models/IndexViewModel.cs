namespace RecipeOrganizer.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Role { get; set; }
        public DateTime? Birthday { get; set; }
        public int TotalRecipe { get; set; }
        public bool Status { get; set; }

    }
}
