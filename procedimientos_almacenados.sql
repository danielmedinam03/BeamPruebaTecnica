USE Colegios;
go
--SP para lsitar los usuarios activos
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
--SP para listar los nombres de los usuarios
CREATE PROCEDURE AllName
AS
BEGIN

	
    SELECT u.UserId as Id, CONCAT(u.Nombre, ' ',u.Apellidos) as Nombre FROM [User] as u
        where u.Eliminado = 0; 
END;
GO

--SP para consultar los usuarios por nombre del rol
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
-- SP para crear un usuario (Registro)
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

--SP para eliminar un usuario (Realmente no lo elimina del todo, hay un campo en la tabla que se llama eliminado, lo que hace es actualizar el campo e inhabilita el usuario)
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

--SP para llevar una traza del Login de cada uno de los usuarios
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
--SP para actualizar un usuario
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
            