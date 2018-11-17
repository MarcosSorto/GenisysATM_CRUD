USE GenisysATM_V2
GO

--procedimiento almacenado para la inserción de una nueva configuración
CREATE PROCEDURE sp_InsertarConfiguracion(
@nombre NCHAR(50),
@descripcion NCHAR(200),
@valor NCHAR(50)
)
AS
BEGIN
	INSERT INTO ATM.Configuracion(appKey,valor,descripcion) VALUES (@nombre,@valor,@descripcion);
END
GO

--Procedimiento almacenado para la actualizacion de una configuración.
CREATE PROCEDURE sp_ActualizarConfiguracion(
@nombre NCHAR(50),
@nombrenuevo NCHAR(50),
@descripcion NCHAR(200),
@valor NCHAR(50)
)
AS
BEGIN
	DECLARE @codigo INT
	SET @codigo = (SELECT id FROM ATM.Configuracion WHERE appKey =@nombre); 
	UPDATE ATM.Configuracion SET appKey=@nombrenuevo,descripcion=@descripcion,valor=@valor WHERE id = @codigo;
END
GO

-- Procedimiento para eliminar una configuración
CREATE PROCEDURE sp_EliminarConfiguracion(
@nombre NCHAR(50)

)
AS
BEGIN
	DECLARE @codigo INT
	SET @codigo = (SELECT id FROM ATM.Configuracion WHERE appKey =@nombre); 
	DELETE FROM ATM.Configuracion WHERE id=@codigo;
END
GO