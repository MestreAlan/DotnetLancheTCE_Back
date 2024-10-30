using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LancheTCE.Migrations
{
    /// <inheritdoc />
    public partial class AlanMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contato",
                table: "Usuarios",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contato",
                table: "Usuarios");
        }
    }
}
