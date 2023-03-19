using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWC_NightOwlProject.Migrations
{
    /// <inheritdoc />
    public partial class WebAppContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Material_Fk_Template",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "Material_Fk_World",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK__World__3214EC27495DB421",
                table: "World");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Template__3214EC27F3675E38",
                table: "Template");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Material__3214EC2785581C70",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_TemplateID",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_WorldID",
                table: "Material");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Template",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Material",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK__World__3214EC2718A658A3",
                table: "World",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Template__3214EC272597D55B",
                table: "Template",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Material__3214EC277DF42520",
                table: "Material",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__World__3214EC2718A658A3",
                table: "World");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Template__3214EC272597D55B",
                table: "Template");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Material__3214EC277DF42520",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Material");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "Template",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddPrimaryKey(
                name: "PK__World__3214EC27495DB421",
                table: "World",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Template__3214EC27F3675E38",
                table: "Template",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Material__3214EC2785581C70",
                table: "Material",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_TemplateID",
                table: "Material",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_Material_WorldID",
                table: "Material",
                column: "WorldID");

            migrationBuilder.AddForeignKey(
                name: "Material_Fk_Template",
                table: "Material",
                column: "TemplateID",
                principalTable: "Template",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "Material_Fk_World",
                table: "Material",
                column: "WorldID",
                principalTable: "World",
                principalColumn: "ID");
        }
    }
}
