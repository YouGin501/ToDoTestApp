using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoTestApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "MyTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "MyTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
