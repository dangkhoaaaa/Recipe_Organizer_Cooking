namespace RecipeOrganizer.Areas.Admin.Models
{
    public class CategoryViewModel
    {

        public int category_id { get; set; }
        public int parent_id { get; set; }

        public string? IMG { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public string? parent_name { get; set; }
    
    }
}
