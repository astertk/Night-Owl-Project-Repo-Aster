using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWCNightOwlProject.Migrations
{
    /// <inheritdoc />
    public partial class AppConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "date", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Template__3214EC27F3675E38", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "World",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "date", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__World__3214EC27495DB421", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "date", nullable: false),
                    WorldID = table.Column<int>(type: "int", nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Completion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TemplateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Material__3214EC2785581C70", x => x.ID);
                    table.ForeignKey(
                        name: "Material_Fk_Template",
                        column: x => x.TemplateID,
                        principalTable: "Template",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "Material_Fk_World",
                        column: x => x.WorldID,
                        principalTable: "World",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_TemplateID",
                table: "Material",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_WorldID",
                table: "Material",
                column: "WorldID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "World");
        }
    }
}
