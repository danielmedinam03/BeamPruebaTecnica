using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"

                CREATE PROCEDURE AddUser
                    @Nombre NVARCHAR(50),
                    @Apellido NVARCHAR(50),
	                @NombreUsuario NVARCHAR(50),
	                @Contrasena NVARCHAR(max),
	                @FechaNacimiento DATETIME2(7),
	                @Celular BIGINT,
	                @Estado BIT,
	                @Rol INT,
	                @Eliminado BIT
                AS
                BEGIN
                    INSERT INTO [User] (Nombre, Apellidos, NombreUsuario,Contraseña,FechaNacimiento,Celular,Estado,RolId,Eliminado,FechaEliminacion)
                    VALUES (@Nombre, @Apellido, @NombreUsuario, @Contrasena, @FechaNacimiento, @Celular, @Estado, @Rol, @Eliminado, null);
                END;

            ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AddUser");

        }
    }
}
