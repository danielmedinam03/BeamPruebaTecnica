using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_userActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"

                CREATE PROCEDURE UsersActive
                AS
                BEGIN

                    SELECT CAST(u.UserId AS BIGINT) as Id, 
                        u.Nombre, 
                        u.Apellidos, 
                        u.NombreUsuario, 
                        u.Contraseña, 
                        u.FechaNacimiento, 
                        CAST(u.Celular AS BIGINT) AS Celular, 
                        u.Estado, 
                        r.Nombre as Rol  
                        FROM [User] as u 
                    Join Rol as r
                        on u.RolId = r.Id
                    where u.Estado = 1 AND u.Eliminado = 0;
                END;
                GO
                ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UsersActive");

        }
    }
}
