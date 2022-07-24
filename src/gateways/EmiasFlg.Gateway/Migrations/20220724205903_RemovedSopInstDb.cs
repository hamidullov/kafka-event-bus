using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmiasFlgGateway.Migrations
{
    public partial class RemovedSopInstDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sop_study_id",
                table: "studies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sop_study_id",
                table: "studies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
