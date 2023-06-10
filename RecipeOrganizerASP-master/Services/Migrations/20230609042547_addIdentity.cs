using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class addIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    feedback_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.feedback_id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    media_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filelocation = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.media_id);
                });

            migrationBuilder.CreateTable(
                name: "Parent_Category",
                columns: table => new
                {
                    parent_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sub_Category", x => x.parent_id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    number_share = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.recipe_id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag_name = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealPlanning",
                columns: table => new
                {
                    plan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    week_start_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPlanning", x => x.plan_id);
                    table.ForeignKey(
                        name: "FK_MealPlanning_User",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.category_id);
                    table.ForeignKey(
                        name: "FK_Category_Parent_Category",
                        column: x => x.parent_id,
                        principalTable: "Parent_Category",
                        principalColumn: "parent_id");
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Collection_Recipe1",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                    table.ForeignKey(
                        name: "FK_Collection_User",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Direction",
                columns: table => new
                {
                    direction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    step = table.Column<int>(type: "int", nullable: false),
                    direction = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direction", x => x.direction_id);
                    table.ForeignKey(
                        name: "FK_Direction_Recipe",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ingredient_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ingredient_name = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ingredient_id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Recipe",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                columns: table => new
                {
                    metadata_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    recipe_id = table.Column<int>(type: "int", nullable: true),
                    media_id = table.Column<int>(type: "int", nullable: true),
                    feedback_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.metadata_id);
                    table.ForeignKey(
                        name: "FK_MetaData_Feedback",
                        column: x => x.feedback_id,
                        principalTable: "Feedback",
                        principalColumn: "feedback_id");
                    table.ForeignKey(
                        name: "FK_MetaData_Media",
                        column: x => x.media_id,
                        principalTable: "Media",
                        principalColumn: "media_id");
                    table.ForeignKey(
                        name: "FK_MetaData_Recipe",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                    table.ForeignKey(
                        name: "FK_MetaData_User",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Recipe_has_Tags",
                columns: table => new
                {
                    recipe_id = table.Column<int>(type: "int", nullable: false),
                    tag_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Recipe_has_Tags_Recipe",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                    table.ForeignKey(
                        name: "FK_Recipe_has_Tags_Tag",
                        column: x => x.tag_id,
                        principalTable: "Tag",
                        principalColumn: "tag_id");
                });

            migrationBuilder.CreateTable(
                name: "Day",
                columns: table => new
                {
                    day_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plan_id = table.Column<int>(type: "int", nullable: false),
                    day_of_week = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.day_id);
                    table.ForeignKey(
                        name: "FK_Day_MealPlanning",
                        column: x => x.plan_id,
                        principalTable: "MealPlanning",
                        principalColumn: "plan_id");
                });

            migrationBuilder.CreateTable(
                name: "Recipe_has_Category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Recipe_has_Category_Category1",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK_Recipe_has_Category_Recipe",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day_id = table.Column<int>(type: "int", nullable: false),
                    session_name = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.session_id);
                    table.ForeignKey(
                        name: "FK_Session_Day",
                        column: x => x.day_id,
                        principalTable: "Day",
                        principalColumn: "day_id");
                });

            migrationBuilder.CreateTable(
                name: "Session_has_Recipe",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "int", nullable: false),
                    recipe_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Session_has_Recipe_Recipe",
                        column: x => x.recipe_id,
                        principalTable: "Recipe",
                        principalColumn: "recipe_id");
                    table.ForeignKey(
                        name: "FK_Session_has_Recipe_Session",
                        column: x => x.session_id,
                        principalTable: "Session",
                        principalColumn: "session_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_parent_id",
                table: "Category",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_recipe_id",
                table: "Collection",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_user_id",
                table: "Collection",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Day_plan_id",
                table: "Day",
                column: "plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_Direction_recipe_id",
                table: "Direction",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_recipe_id",
                table: "Ingredient",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_MealPlanning_user_id",
                table: "MealPlanning",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_feedback_id",
                table: "MetaData",
                column: "feedback_id");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_media_id",
                table: "MetaData",
                column: "media_id");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_recipe_id",
                table: "MetaData",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_user_id",
                table: "MetaData",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_has_Category_category_id",
                table: "Recipe_has_Category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_has_Category_recipe_id",
                table: "Recipe_has_Category",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_has_Tags_recipe_id",
                table: "Recipe_has_Tags",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_has_Tags_tag_id",
                table: "Recipe_has_Tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_Session_day_id",
                table: "Session",
                column: "day_id");

            migrationBuilder.CreateIndex(
                name: "IX_Session_has_Recipe_recipe_id",
                table: "Session_has_Recipe",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Session_has_Recipe_session_id",
                table: "Session_has_Recipe",
                column: "session_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Direction");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "MetaData");

            migrationBuilder.DropTable(
                name: "Recipe_has_Category");

            migrationBuilder.DropTable(
                name: "Recipe_has_Tags");

            migrationBuilder.DropTable(
                name: "Session_has_Recipe");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Parent_Category");

            migrationBuilder.DropTable(
                name: "Day");

            migrationBuilder.DropTable(
                name: "MealPlanning");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
