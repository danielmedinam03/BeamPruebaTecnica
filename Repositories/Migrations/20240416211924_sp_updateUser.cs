using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_updateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			var sp = @"

            
				CREATE PROCEDURE UpdateUser
					@Id INT,
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
					UPDATE [dbo].[User]
					   SET [Nombre] = @Nombre
						  ,[Apellidos] = @Apellido
						  ,[NombreUsuario] = @NombreUsuario
						  ,[Contraseña] = @Contrasena
						  ,[FechaNacimiento] = @FechaNacimiento
						  ,[Celular] = @Celular
						  ,[Estado] = @Estado
						  ,[RolId] = @Rol
						  ,[Eliminado] = @Eliminado
						  ,[FechaEliminacion] = null
					 WHERE [UserId] = @Id
				END;
            

            ";
			migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateUser");
        }
    }
}
