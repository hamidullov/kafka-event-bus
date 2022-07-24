using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlgValidation.Microservice.Migrations
{
    public partial class AddedErrorMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "error_message",
                table: "studies",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "error_message",
                table: "studies");
        }
    }
}
