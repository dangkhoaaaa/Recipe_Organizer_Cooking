using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Services.Models.Authentication;

namespace Services.Models
    //Services.Models.Recipe_OrganizerContext
{
	public partial class Recipe_OrganizerContext : IdentityDbContext<AppUser>
    {
        public Recipe_OrganizerContext()
        {
        }

        public Recipe_OrganizerContext(DbContextOptions<Recipe_OrganizerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Day> Days { get; set; } = null!;
        public virtual DbSet<Direction> Directions { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<MealPlanning> MealPlannings { get; set; } = null!;
        public virtual DbSet<Media> Media { get; set; } = null!;
        public virtual DbSet<Metadata> MetaData { get; set; } = null!;
        public virtual DbSet<ParentCategory> ParentCategories { get; set; } = null!;
        public virtual DbSet<Recipe> Recipes { get; set; } = null!;
        public virtual DbSet<RecipeHasCategory> RecipeHasCategories { get; set; } = null!;
        public virtual DbSet<RecipeHasTag> RecipeHasTags { get; set; } = null!;
        public virtual DbSet<Session> Sessions { get; set; } = null!;
        public virtual DbSet<SessionHasRecipe> SessionHasRecipes { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        //public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Recipe_Organizer;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_Parent_Category");
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("Collection");

                entity.HasKey(e => e.CollectionId);

                entity.Property(e => e.CollectionId).HasColumnName("id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collection_Recipe1");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collection_User");
            });

            modelBuilder.Entity<Day>(entity =>
            {
                entity.ToTable("Day");

                entity.Property(e => e.DayId).HasColumnName("day_id");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("day_of_week");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Days)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Day_MealPlanning");
            });

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.ToTable("Direction");

                entity.Property(e => e.DirectionId).HasColumnName("direction_id");

                entity.Property(e => e.Direction1)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("direction");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.Step).HasColumnName("step");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Directions)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Direction_Recipe");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.IngredientName)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("ingredient_name");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredient_Recipe");
            });

            modelBuilder.Entity<MealPlanning>(entity =>
            {
                entity.HasKey(e => e.PlanId);

                entity.ToTable("MealPlanning");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.WeekStartDate)
					.HasMaxLength(10)
					.IsUnicode(false)
					.HasColumnName("week_start_date");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MealPlannings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MealPlanning_User");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasKey(e => e.MediaId);

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Filelocation)
                    .HasColumnType("text")
                    .HasColumnName("filelocation");
            });

            modelBuilder.Entity<Metadata>(entity =>
            {
                entity.HasKey(e => e.MetadataId);

                entity.Property(e => e.MetadataId).HasColumnName("metadata_id");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK_MetaData_Feedback");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.MediaId)
                    .HasConstraintName("FK_MetaData_Media");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK_MetaData_Recipe");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_MetaData_User");
            });

            modelBuilder.Entity<ParentCategory>(entity =>
            {
                entity.HasKey(e => e.ParentId)
                    .HasName("PK_Sub_Category");

                entity.ToTable("Parent_Category");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("Recipe");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.NumberShare).HasColumnName("number_share");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.AvgRate).HasColumnName("avg_rate");

                entity.Property(e => e.Image)
                    .HasColumnType("text")
                    .HasColumnName("image");
            });

            modelBuilder.Entity<RecipeHasCategory>(entity =>
            {

                entity.ToTable("Recipe_has_Category");

                entity.HasKey(e => e.RecipeHasCategoryId);

                entity.Property(e => e.RecipeHasCategoryId).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_has_Category_Category1");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_has_Category_Recipe");
            });

            modelBuilder.Entity<RecipeHasTag>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("Recipe_has_Tags");

                entity.HasKey(e => e.RecipeHasTagId);

                entity.Property(e => e.RecipeHasTagId).HasColumnName("id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_has_Tags_Recipe");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_has_Tags_Tag");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.DayId).HasColumnName("day_id");

                entity.Property(e => e.SessionName)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("session_name");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_Day");
            });

            modelBuilder.Entity<SessionHasRecipe>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("Session_has_Recipe");

                entity.HasKey(e => e.SessionHasRecipeId);

                entity.Property(e => e.SessionHasRecipeId).HasColumnName("id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.HasOne(d => d.Recipe)
                    .WithMany()
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_has_Recipe_Recipe");

                entity.HasOne(d => d.Session)
                    .WithMany()
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Session_has_Recipe_Session");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.TagName)
                    .HasMaxLength(50)
                    .HasColumnName("tag_name")
                    .IsFixedLength();
            });

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.ToTable("User");

            //    entity.Property(e => e.UserId).HasColumnName("user_id");

            //    entity.Property(e => e.Avatar)
            //        .HasMaxLength(200)
            //        .IsUnicode(false)
            //        .HasColumnName("avatar");

            //    entity.Property(e => e.Birthday)
            //        .HasColumnType("datetime")
            //        .HasColumnName("birthday");

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(100)
            //        .IsUnicode(false)
            //        .HasColumnName("email");

            //    entity.Property(e => e.FirstName)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("first_name");

            //    entity.Property(e => e.LastName)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("last_name");

            //    entity.Property(e => e.Password)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("password");

            //    entity.Property(e => e.Role)
            //        .HasMaxLength(20)
            //        .IsUnicode(false)
            //        .HasColumnName("role");

            //    entity.Property(e => e.Status).HasColumnName("status");

            //    entity.Property(e => e.Username)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasColumnName("username");
            //});


            //foreach (var entityType in builder.Model.GetEntityTypes())
            //{
            //    var tableName = entityType.GetTableName();
            //    if (tableName.StartsWith("AspNet"))
            //    {
            //        entityType.SetTableName(tableName.Substring(6));
            //    }
            //}


            OnModelCreatingPartial(modelBuilder);

            
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
