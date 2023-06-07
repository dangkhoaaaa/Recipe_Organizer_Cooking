﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Services.Models;

#nullable disable

namespace Services.Migrations
{
    [DbContext(typeof(Recipe_OrganizerContext))]
    partial class Recipe_OrganizerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("Services.Models.Authentication.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Services.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("description");

                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Services.Models.Collection", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("collection_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectionId"), 1L, 1);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("CollectionId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Collection", (string)null);
                });

            modelBuilder.Entity("Services.Models.Day", b =>
                {
                    b.Property<int>("DayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("day_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DayId"), 1L, 1);

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("day_of_week");

                    b.Property<int>("PlanId")
                        .HasColumnType("int")
                        .HasColumnName("plan_id");

                    b.HasKey("DayId");

                    b.HasIndex("PlanId");

                    b.ToTable("Day", (string)null);
                });

            modelBuilder.Entity("Services.Models.Direction", b =>
                {
                    b.Property<int>("DirectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("direction_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirectionId"), 1L, 1);

                    b.Property<string>("Direction1")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("direction");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.Property<int>("Step")
                        .HasColumnType("int")
                        .HasColumnName("step");

                    b.HasKey("DirectionId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Direction", (string)null);
                });

            modelBuilder.Entity("Services.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("feedback_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("description");

                    b.Property<int>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("FeedbackId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("Services.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ingredient_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"), 1L, 1);

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("ingredient_name");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.HasKey("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredient", (string)null);
                });

            modelBuilder.Entity("Services.Models.MealPlanning", b =>
                {
                    b.Property<int>("PlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("plan_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanId"), 1L, 1);

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<DateTime>("WeekStartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("week_start_date");

                    b.HasKey("PlanId");

                    b.HasIndex("UserId");

                    b.ToTable("MealPlanning", (string)null);
                });

            modelBuilder.Entity("Services.Models.Media", b =>
                {
                    b.Property<int>("MediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("media_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MediaId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<string>("Filelocation")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("filelocation");

                    b.HasKey("MediaId");

                    b.ToTable("Media");
                });

            modelBuilder.Entity("Services.Models.Metadata", b =>
                {
                    b.Property<int>("MetadataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("metadata_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetadataId"), 1L, 1);

                    b.Property<int?>("FeedbackId")
                        .HasColumnType("int")
                        .HasColumnName("feedback_id");

                    b.Property<int?>("MediaId")
                        .HasColumnType("int")
                        .HasColumnName("media_id");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("MetadataId");

                    b.HasIndex("FeedbackId");

                    b.HasIndex("MediaId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("MetaData");
                });

            modelBuilder.Entity("Services.Models.ParentCategory", b =>
                {
                    b.Property<int>("ParentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ParentId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("ParentId")
                        .HasName("PK_Sub_Category");

                    b.ToTable("Parent_Category", (string)null);
                });

            modelBuilder.Entity("Services.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1000)")
                        .HasColumnName("description");

                    b.Property<int>("NumberShare")
                        .HasColumnType("int")
                        .HasColumnName("number_share");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipe", (string)null);
                });

            modelBuilder.Entity("Services.Models.RecipeHasCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Recipe_has_Category", (string)null);
                });

            modelBuilder.Entity("Services.Models.RecipeHasTag", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.Property<int>("TagId")
                        .HasColumnType("int")
                        .HasColumnName("tag_id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("TagId");

                    b.ToTable("Recipe_has_Tags", (string)null);
                });

            modelBuilder.Entity("Services.Models.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("session_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SessionId"), 1L, 1);

                    b.Property<int>("DayId")
                        .HasColumnType("int")
                        .HasColumnName("day_id");

                    b.Property<string>("SessionName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("session_name");

                    b.HasKey("SessionId");

                    b.HasIndex("DayId");

                    b.ToTable("Session", (string)null);
                });

            modelBuilder.Entity("Services.Models.SessionHasRecipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("recipe_id");

                    b.Property<int>("SessionId")
                        .HasColumnType("int")
                        .HasColumnName("session_id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("SessionId");

                    b.ToTable("Session_has_Recipe", (string)null);
                });

            modelBuilder.Entity("Services.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("tag_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("tag_name")
                        .IsFixedLength();

                    b.HasKey("TagId");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("Services.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Avatar")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("avatar");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime")
                        .HasColumnName("birthday");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("role");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("status");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Services.Models.Authentication.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Services.Models.Authentication.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Services.Models.Authentication.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Services.Models.Authentication.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Services.Models.Category", b =>
                {
                    b.HasOne("Services.Models.ParentCategory", "Parent")
                        .WithMany("Categories")
                        .HasForeignKey("ParentId")
                        .IsRequired()
                        .HasConstraintName("FK_Category_Parent_Category");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Services.Models.Collection", b =>
                {
                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany("Collections")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_Collection_Recipe1");

                    b.HasOne("Services.Models.User", "User")
                        .WithMany("Collections")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Collection_User");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Services.Models.Day", b =>
                {
                    b.HasOne("Services.Models.MealPlanning", "Plan")
                        .WithMany("Days")
                        .HasForeignKey("PlanId")
                        .IsRequired()
                        .HasConstraintName("FK_Day_MealPlanning");

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("Services.Models.Direction", b =>
                {
                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany("Directions")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_Direction_Recipe");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Services.Models.Ingredient", b =>
                {
                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_Ingredient_Recipe");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Services.Models.MealPlanning", b =>
                {
                    b.HasOne("Services.Models.User", "User")
                        .WithMany("MealPlannings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_MealPlanning_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Services.Models.Metadata", b =>
                {
                    b.HasOne("Services.Models.Feedback", "Feedback")
                        .WithMany("MetaData")
                        .HasForeignKey("FeedbackId")
                        .HasConstraintName("FK_MetaData_Feedback");

                    b.HasOne("Services.Models.Media", "Media")
                        .WithMany("MetaData")
                        .HasForeignKey("MediaId")
                        .HasConstraintName("FK_MetaData_Media");

                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany("MetaData")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("FK_MetaData_Recipe");

                    b.HasOne("Services.Models.User", "User")
                        .WithMany("MetaData")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_MetaData_User");

                    b.Navigation("Feedback");

                    b.Navigation("Media");

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Services.Models.RecipeHasCategory", b =>
                {
                    b.HasOne("Services.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK_Recipe_has_Category_Category1");

                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_Recipe_has_Category_Recipe");

                    b.Navigation("Category");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Services.Models.RecipeHasTag", b =>
                {
                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_Recipe_has_Tags_Recipe");

                    b.HasOne("Services.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .IsRequired()
                        .HasConstraintName("FK_Recipe_has_Tags_Tag");

                    b.Navigation("Recipe");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Services.Models.Session", b =>
                {
                    b.HasOne("Services.Models.Day", "Day")
                        .WithMany("Sessions")
                        .HasForeignKey("DayId")
                        .IsRequired()
                        .HasConstraintName("FK_Session_Day");

                    b.Navigation("Day");
                });

            modelBuilder.Entity("Services.Models.SessionHasRecipe", b =>
                {
                    b.HasOne("Services.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .IsRequired()
                        .HasConstraintName("FK_Session_has_Recipe_Recipe");

                    b.HasOne("Services.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .IsRequired()
                        .HasConstraintName("FK_Session_has_Recipe_Session");

                    b.Navigation("Recipe");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("Services.Models.Day", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Services.Models.Feedback", b =>
                {
                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("Services.Models.MealPlanning", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("Services.Models.Media", b =>
                {
                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("Services.Models.ParentCategory", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Services.Models.Recipe", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("Directions");

                    b.Navigation("Ingredients");

                    b.Navigation("MetaData");
                });

            modelBuilder.Entity("Services.Models.User", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("MealPlannings");

                    b.Navigation("MetaData");
                });
#pragma warning restore 612, 618
        }
    }
}
