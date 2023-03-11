using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWCNightOwlProject.Migrations
{
    /// <inheritdoc />
    public partial class nameAppConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "World",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "NAME",
                table: "World",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Material",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NAME",
                table: "World");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Material");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "World",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);
        }
    }
}
