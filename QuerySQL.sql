create database BaseAutos
use BaseAutos

CREATE TABLE Autos (
    IdAuto INT PRIMARY KEY IDENTITY,
    Marca VARCHAR(50),
    Cilindraje DECIMAL(10,2),
    Color VARCHAR(50),
    Propietario VARCHAR(50),
    Estado BIT DEFAULT 0 -- Campo para eliminación lógica (0: Activo, 1: Eliminado)
);

-- Insertar datos aleatorios en la tabla Autos
INSERT INTO Autos (Marca, Cilindraje, Color, Propietario, Estado) VALUES ('Toyota', 1.6, 'Rojo', 'Juan Perez', 0);
INSERT INTO Autos (Marca, Cilindraje, Color, Propietario, Estado) VALUES ('Ford', 2.0, 'Azul', 'Maria Lopez', 0);

select * from Autos

create proc sp_listaAutos
as 
begin 
	select
	idAuto,	
	Marca,
	Cilindraje,
	Color,
	Propietario,
	Estado
	from 
	Autos

end
go


create proc sp_obtenerAuto
(@idAuto int)
as 
begin 
	select
	idAuto,	
	Marca,
	Cilindraje,
	Color,
	Propietario,
	Estado
	from		
	Autos where idAuto = @idAuto

end
go

create proc sp_crearAuto
(@Marca varchar(50),
@Cilindraje decimal(10,2),
@Color varchar (50),
@Propietario varchar (50)
)
as
begin 
INSERT INTO Autos (Marca, Cilindraje, Color, Propietario, Estado)
values(@Marca, @Cilindraje, @Color, @Propietario,0)


end
GO

CREATE PROCEDURE sp_editarAuto
   ( @IdAuto INT,
    @Marca VARCHAR(50),
    @Cilindraje DECIMAL(10,2),
    @Color VARCHAR(50),
    @Propietario VARCHAR(50),
    @Estado BIT)
AS
BEGIN
    UPDATE Autos
    SET Marca = @Marca,
        Cilindraje = @Cilindraje,
        Color = @Color,
        Propietario = @Propietario,
        Estado = @Estado
    WHERE IdAuto = @IdAuto;
END

GO
CREATE PROCEDURE sp_eliminarAuto
    (@IdAuto INT)
AS
BEGIN
    
    delete from Autos where IdAuto = @IdAuto;
END
GO


ALTER PROCEDURE sp_listaAutos
AS 
BEGIN 
    SELECT
        IdAuto,	
        Marca,
        Cilindraje,
        Color,
        Propietario,
        Estado
    FROM 
        Autos
    WHERE 
        Estado = 0; -- Filtra solo los registros no eliminados
END
GO
ALTER PROCEDURE sp_obtenerAuto
    @idAuto INT
AS 
BEGIN 
    SELECT
        IdAuto,	
        Marca,
        Cilindraje,
        Color,
        Propietario,
        Estado
    FROM 
        Autos 
    WHERE 
        IdAuto = @idAuto
        AND Estado = 0; -- Asegura que solo se seleccionen los registros no eliminados
END
GO