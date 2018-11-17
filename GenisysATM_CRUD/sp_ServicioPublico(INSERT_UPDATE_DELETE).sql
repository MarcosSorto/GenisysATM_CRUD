USE GenisysATM_V2
GO

--Procedimiento Almacenado para inserción de servicios
CREATE PROCEDURE sp_InsertarServicio(
@descripcion NVARCHAR(150)
)
AS
BEGIN
	INSERT INTO ATM.ServicioPublico(descripcion) VALUES (@descripcion);
END
GO
--Procedimiento Almacenado para Actualización de servicios.
CREATE PROCEDURE sp_ActualizarServicio(
@descripcion NVARCHAR(150),
@descripcionNueva NVARCHAR(150)
)
AS
BEGIN
 DECLARE @codigo INT
 SET @codigo = (SELECT id FROM ATM.ServicioPublico WHERE descripcion=@descripcion);
 UPDATE ATM.ServicioPublico SET descripcion = @descripcionNueva WHERE id = @codigo;
END
GO

--Procedimiento Almacenado para Eliminación de servicios.
CREATE PROCEDURE sp_EliminarServicio(
@descripcion NVARCHAR(150)
)
AS
BEGIN
 DECLARE @codigo INT
 SET @codigo = (SELECT id FROM ATM.ServicioPublico WHERE descripcion=@descripcion);
 DELETE FROM ATM.ServicioPublico WHERE descripcion = @descripcion;
END
GO