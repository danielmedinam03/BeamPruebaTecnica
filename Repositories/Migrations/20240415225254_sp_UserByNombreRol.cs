using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_UserByNombreRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
            
                CREATE PROCEDURE spUsuariosPorNombreRol
                    @NombreRol NVARCHAR(100)
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT CAST(u.UserId AS BIGINT) as Id, 
		                u.Nombre, 
		                u.Apellidos, 
		                u.NombreUsuario, 
		                u.FechaNacimiento, 
		                u.Celular, 
		                u.Estado, 
                        u.Contraseña,
		                r.Nombre AS Rol

                    FROM [User] AS u
                    JOIN Rol AS r ON u.RolId = r.Id

                    WHERE r.Nombre = @NombreRol AND u.Estado = 1 AND u.Eliminado = 0;
                END;
                GO
            ";
            migrationBuilder.Sql(sp);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS spUsuariosPorNombreRol");

        }
    }
}
