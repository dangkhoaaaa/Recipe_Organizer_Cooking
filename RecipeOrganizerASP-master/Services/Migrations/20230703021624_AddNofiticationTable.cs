using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class AddNofiticationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tag",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Session_has_Recipe",
                newName: "Session_has_Recipe",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Session",
                newName: "Session",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Recipe_has_Tags",
                newName: "Recipe_has_Tags",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Recipe_has_Category",
                newName: "Recipe_has_Category",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipe",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Parent_Category",
                newName: "Parent_Category",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MetaData",
                newName: "MetaData",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Media",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "MealPlanning",
                newName: "MealPlanning",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredient",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Feedback",
                newName: "Feedback",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Direction",
                newName: "Direction",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Day",
                newName: "Day",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collection",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Category",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UserTokens",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UserRole",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UserLogin",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UserClaim",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Role",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "UserRoleClaims",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "dbo",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "dbo",
                table: "UserLogin",
                newName: "IX_UserLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "dbo",
                table: "UserClaim",
                newName: "IX_UserClaim_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "dbo",
                table: "UserRoleClaims",
                newName: "IX_UserRoleClaims_RoleId");

            migrationBuilder.AddColumn<int>(
                name: "notification_id",
                schema: "dbo",
                table: "MetaData",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "dbo",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                schema: "dbo",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                schema: "dbo",
                table: "UserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaim",
                schema: "dbo",
                table: "UserClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleClaims",
                schema: "dbo",
                table: "UserRoleClaims",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_notification_id",
                schema: "dbo",
                table: "MetaData",
                column: "notification_id");

            migrationBuilder.AddForeignKey(
                name: "FK_MetaData_Notification",
                schema: "dbo",
                table: "MetaData",
                column: "notification_id",
                principalSchema: "dbo",
                principalTable: "Notification",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserRole",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "dbo",
                table: "UserRole",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleClaims_Role_RoleId",
                schema: "dbo",
                table: "UserRoleClaims",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetaData_Notification",
                schema: "dbo",
                table: "MetaData");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleClaims_Role_RoleId",
                schema: "dbo",
                table: "UserRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "dbo",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_MetaData_notification_id",
                schema: "dbo",
                table: "MetaData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "dbo",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleClaims",
                schema: "dbo",
                table: "UserRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                schema: "dbo",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                schema: "dbo",
                table: "UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaim",
                schema: "dbo",
                table: "UserClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "dbo",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "notification_id",
                schema: "dbo",
                table: "MetaData");

            migrationBuilder.RenameTable(
                name: "Tag",
                schema: "dbo",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "Session_has_Recipe",
                schema: "dbo",
                newName: "Session_has_Recipe");

            migrationBuilder.RenameTable(
                name: "Session",
                schema: "dbo",
                newName: "Session");

            migrationBuilder.RenameTable(
                name: "Recipe_has_Tags",
                schema: "dbo",
                newName: "Recipe_has_Tags");

            migrationBuilder.RenameTable(
                name: "Recipe_has_Category",
                schema: "dbo",
                newName: "Recipe_has_Category");

            migrationBuilder.RenameTable(
                name: "Recipe",
                schema: "dbo",
                newName: "Recipe");

            migrationBuilder.RenameTable(
                name: "Parent_Category",
                schema: "dbo",
                newName: "Parent_Category");

            migrationBuilder.RenameTable(
                name: "MetaData",
                schema: "dbo",
                newName: "MetaData");

            migrationBuilder.RenameTable(
                name: "Media",
                schema: "dbo",
                newName: "Media");

            migrationBuilder.RenameTable(
                name: "MealPlanning",
                schema: "dbo",
                newName: "MealPlanning");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                schema: "dbo",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Feedback",
                schema: "dbo",
                newName: "Feedback");

            migrationBuilder.RenameTable(
                name: "Direction",
                schema: "dbo",
                newName: "Direction");

            migrationBuilder.RenameTable(
                name: "Day",
                schema: "dbo",
                newName: "Day");

            migrationBuilder.RenameTable(
                name: "Collection",
                schema: "dbo",
                newName: "Collection");

            migrationBuilder.RenameTable(
                name: "Category",
                schema: "dbo",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "dbo",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "dbo",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "UserRoleClaims",
                schema: "dbo",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "UserRole",
                schema: "dbo",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogin",
                schema: "dbo",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaim",
                schema: "dbo",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "dbo",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogin_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaim_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
