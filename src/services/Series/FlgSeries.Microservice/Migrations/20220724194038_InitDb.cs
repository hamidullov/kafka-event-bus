using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FlgSeries.Microservice.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "studies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sop_study_id = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<int>(type: "integer", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    defined_series_id = table.Column<int>(type: "integer", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_studies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "study_series",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    series_id = table.Column<string>(type: "text", nullable: false),
                    thickness_value = table.Column<int>(type: "integer", nullable: false),
                    study_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_study_series", x => x.id);
                    table.ForeignKey(
                        name: "fk_study_series_studies_study_id",
                        column: x => x.study_id,
                        principalTable: "studies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "study_series_instance",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sop_instance_id = table.Column<string>(type: "text", nullable: false),
                    study_series_id = table.Column<int>(type: "integer", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_study_series_instance", x => x.id);
                    table.ForeignKey(
                        name: "fk_study_series_instance_study_series_study_series_id",
                        column: x => x.study_series_id,
                        principalTable: "study_series",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_studies_defined_series_id",
                table: "studies",
                column: "defined_series_id");

            migrationBuilder.CreateIndex(
                name: "ix_study_series_study_id",
                table: "study_series",
                column: "study_id");

            migrationBuilder.CreateIndex(
                name: "ix_study_series_instance_study_series_id",
                table: "study_series_instance",
                column: "study_series_id");

            migrationBuilder.AddForeignKey(
                name: "fk_studies_study_series_defined_series_id",
                table: "studies",
                column: "defined_series_id",
                principalTable: "study_series",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_studies_study_series_defined_series_id",
                table: "studies");

            migrationBuilder.DropTable(
                name: "study_series_instance");

            migrationBuilder.DropTable(
                name: "study_series");

            migrationBuilder.DropTable(
                name: "studies");
        }
    }
}
