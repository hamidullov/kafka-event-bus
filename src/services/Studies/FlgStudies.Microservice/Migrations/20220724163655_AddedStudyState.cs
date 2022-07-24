using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlgStudies.Microservice.Migrations
{
    public partial class AddedStudyState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "studies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "studies");
        }
    }
}
