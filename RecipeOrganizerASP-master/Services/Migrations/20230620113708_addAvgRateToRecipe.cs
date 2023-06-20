using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class addAvgRateToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "avg_rate",
                table: "Recipe",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "filelocation",
                table: "Media",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldUnicode: false,
                oldMaxLength: 300);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avg_rate",
                table: "Recipe");

            migrationBuilder.AlterColumn<string>(
                name: "filelocation",
                table: "Media",
                type: "varchar(300)",
                unicode: false,
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
