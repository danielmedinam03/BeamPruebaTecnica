using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    public partial class sp_login : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"

                CREATE PROCEDURE AddLoginEvent
	                @Id UNIQUEIDENTIFIER,
                    @UserId INT,
	                @HoraIngreso DATETIME2(7),
	                @Resultado BIT
                AS
                BEGIN
    
                INSERT INTO [dbo].[LoginEvent]
                           ([Id]
		                   ,[UserId]
                           ,[HoraIngreso]
                           ,[Resultado])
                     VALUES
                           (@Id,@UserId,@HoraIngreso,@Resultado);
                END;
            
            ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AddLoginEvent");
        }
    }
}
