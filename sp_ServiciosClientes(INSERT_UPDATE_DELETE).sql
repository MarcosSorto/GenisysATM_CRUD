USE GenisysATM_V2
GO
--Procedimiento almacenado para insertar un servicio a un cliente.
CREATE PROCEDURE sp_InsertarServicioCliente(
@cliente NVARCHAR(100),
@servicio NVARCHAR(100),
@saldo DECIMAL(10,2)
)
AS 
BEGIN
	DECLARE @codigoCliente INT
	DECLARE @codigoServicio INT

	SET @codigoCliente = (SELECT id FROM ATM.Cliente WHERE nombres = @cliente);
	SET @codigoServicio = (SELECT id FROM ATM.ServicioPublico WHERE descripcion=@servicio);

	INSERT INTO ATM.ServicioCliente(idCliente,idServicio,saldo) VALUES(@codigoCliente,@codigoServicio,@saldo);
END
GO
--Procedimiento almacenado para actualizar un servicioCliente
CREATE PROCEDURE sp_ActualizarServicioCliente(
@cliente NVARCHAR(100),
@servicio NVARCHAR(100),
@saldo DECIMAL(10,2)
)
AS 
BEGIN
	DECLARE @codigoCliente INT
	DECLARE @codigoServicio INT
	DECLARE @codigo INT

	SET @codigoCliente = (SELECT id FROM ATM.Cliente WHERE nombres = @cliente);
	SET @codigoServicio = (SELECT id FROM ATM.ServicioPublico WHERE descripcion=@servicio);

	SET @codigo = (SELECT id FROM ATM.ServicioCliente WHERE idCliente=@codigoCliente and idServicio=@codigoServicio);

	UPDATE ATM.ServicioCliente SET idCliente=@codigoCliente,idServicio=@codigoServicio,saldo=@saldo WHERE id=@codigo;
END
GO

--Procedimiento almacenado para Eliminar un servicioCliente
CREATE PROCEDURE sp_EliminarServicioCliente(
@cliente NVARCHAR(100),
@servicio NVARCHAR(100)
)
AS 
BEGIN
	DECLARE @codigoCliente INT
	DECLARE @codigoServicio INT
	DECLARE @codigo INT

	SET @codigoCliente = (SELECT id FROM ATM.Cliente WHERE nombres = @cliente);
	SET @codigoServicio = (SELECT id FROM ATM.ServicioPublico WHERE descripcion=@servicio);

	SET @codigo = (SELECT id FROM ATM.ServicioCliente WHERE idCliente=@codigoCliente and idServicio=@codigoServicio);

	DELETE FROM ATM.ServicioCliente WHERE id=@codigo;
END
GO
