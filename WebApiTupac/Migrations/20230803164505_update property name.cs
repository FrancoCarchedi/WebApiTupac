using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiTupac.Migrations
{
    /// <inheritdoc />
    public partial class updatepropertyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdMateria",
                table: "Materias",
                newName: "MateriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MateriaId",
                table: "Materias",
                newName: "IdMateria");
        }
    }
}
