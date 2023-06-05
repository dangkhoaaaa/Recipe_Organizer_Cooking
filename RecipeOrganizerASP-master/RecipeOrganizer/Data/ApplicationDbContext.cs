using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeOrganizer.Models;

namespace RecipeOrganizer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       // public DbSet<Test> Tests { get; set; }
        //public DbSet<Role> Roles { get; set; }
        /*
        public DbSet<User> Users { get; set; }
        public DbSet<User_Like_Recipe> User_Like_Recipes { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<MealPlanning> MealPlannings { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Media_has_Recipe> Media_has_Recipes { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Recipe_has_Category> Recipe_has_Categorys { get; set; }
        */
    }
}