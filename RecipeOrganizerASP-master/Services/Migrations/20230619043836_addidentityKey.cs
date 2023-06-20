using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class addidentityKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Session_has_Recipe",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Recipe_has_Tags",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Recipe_has_Category",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Collection",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Session_has_Recipe",
                table: "Session_has_Recipe",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_has_Tags",
                table: "Recipe_has_Tags",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe_has_Category",
                table: "Recipe_has_Category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Session_has_Recipe",
                table: "Session_has_Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_has_Tags",
                table: "Recipe_has_Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe_has_Category",
                table: "Recipe_has_Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Session_has_Recipe");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Recipe_has_Tags");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Recipe_has_Category");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");
        }
    }
}
