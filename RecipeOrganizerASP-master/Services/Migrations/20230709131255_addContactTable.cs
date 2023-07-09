using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    public partial class addContactTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "dbo",
                columns: table => new
                {
                    contact_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contact_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contact_email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    contact_address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    contact_message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    contact_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.contact_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact",
                schema: "dbo");
        }
    }
}
