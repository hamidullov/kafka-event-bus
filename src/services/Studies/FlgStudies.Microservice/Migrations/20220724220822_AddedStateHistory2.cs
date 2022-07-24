using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlgStudies.Microservice.Migrations
{
    public partial class AddedStateHistory2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_study_state_history_studies_state_id",
                table: "study_state_history");

            migrationBuilder.DropIndex(
                name: "ix_study_state_history_state_id",
                table: "study_state_history");

            migrationBuilder.DropColumn(
                name: "state_id",
                table: "study_state_history");

            migrationBuilder.AddColumn<int>(
                name: "state",
                table: "study_state_history",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "study_id",
                table: "study_state_history",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_study_state_history_study_id",
                table: "study_state_history",
                column: "study_id");

            migrationBuilder.AddForeignKey(
                name: "fk_study_state_history_studies_study_id",
                table: "study_state_history",
                column: "study_id",
                principalTable: "studies",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_study_state_history_studies_study_id",
                table: "study_state_history");

            migrationBuilder.DropIndex(
                name: "ix_study_state_history_study_id",
                table: "study_state_history");

            migrationBuilder.DropColumn(
                name: "state",
                table: "study_state_history");

            migrationBuilder.DropColumn(
                name: "study_id",
                table: "study_state_history");

            migrationBuilder.AddColumn<Guid>(
                name: "state_id",
                table: "study_state_history",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_study_state_history_state_id",
                table: "study_state_history",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                name: "fk_study_state_history_studies_state_id",
                table: "study_state_history",
                column: "state_id",
                principalTable: "studies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
