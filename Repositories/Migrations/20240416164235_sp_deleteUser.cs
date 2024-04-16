using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_deleteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
            
                CREATE PROCEDURE DeleteUser
	                @Id INT,
	                @FechaEliminacion DATETIME2(7)
                AS
                BEGIN
                    UPDATE [dbo].[User]
	                   SET [Eliminado] = 1
		                  ,[FechaEliminacion] = @FechaEliminacion
                          ,[Estado] = 0
	                 WHERE UserId = @Id

                END;
            
            ";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteUser");
        }
    }
}
