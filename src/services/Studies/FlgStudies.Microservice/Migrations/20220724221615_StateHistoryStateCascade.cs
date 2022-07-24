using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlgStudies.Microservice.Migrations
{
    public partial class StateHistoryStateCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_study_state_history_studies_study_id",
                table: "study_state_history");

            migrationBuilder.AddForeignKey(
                name: "fk_study_state_history_studies_study_id",
                table: "study_state_history",
                column: "study_id",
                principalTable: "studies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_study_state_history_studies_study_id",
                table: "study_state_history");

            migrationBuilder.AddForeignKey(
                name: "fk_study_state_history_studies_study_id",
                table: "study_state_history",
                column: "study_id",
                principalTable: "studies",
                principalColumn: "id");
        }
    }
}
