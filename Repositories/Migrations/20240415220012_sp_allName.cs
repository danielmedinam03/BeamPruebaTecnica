using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_allName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"

            CREATE PROCEDURE AllName
            AS
            BEGIN

	
	            SELECT u.UserId as Id, CONCAT(u.Nombre, ' ',u.Apellidos) as Nombre FROM [User] as u
                    where u.Eliminado = 0; 
            END;
            GO

            ";
            migrationBuilder.Sql(sp);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AllName");

        }
    }
}
