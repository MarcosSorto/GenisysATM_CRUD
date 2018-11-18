USE GenisysATM_V2
GO

--Procedimiento para Almacenar una nueva tarjeta de crédito para un cliente
CREATE PROCEDURE sp_InsertarTarjeta(
@descripcion NVARCHAR(100),
@monto DECIMAL(12,2),
@limite DECIMAL(12,2),
@cliente NVARCHAR(100)
)
AS
BEGIN
	DECLARE @codigoCliente INT
	SET @codigoCliente = (SELECT id FROM ATM.Cliente WHERE nombres=@cliente);

	INSERT INTO ATM.TarjetaCredito(descripcion,monto,limite,idCliente) VALUES (@descripcion,@monto,@limite,@codigoCliente);
END
GO

-- Procedimiento almacenado para actualizar la tarjeta de crédito asignada a un cliente
CREATE PROCEDURE sp_ActualizarTarjeta(
@descripcion NVARCHAR(100),
@nuevaDescripcion NVARCHAR(100),
@monto DECIMAL(12,2),
@limite DECIMAL(12,2),
@cliente NVARCHAR(100)
)
AS
BEGIN
	DECLARE @codigoCliente INT
	SET @codigoCliente = (SELECT id FROM ATM.Cliente WHERE nombres=@cliente);
	DECLARE @codigoTarjeta INT
	SET @codigoTarjeta = (SELECT id FROM ATM.TarjetaCredito WHERE idCliente=@codigoCliente and descripcion=@descripcion);

	UPDATE ATM.TarjetaCredito SET descripcion=@nuevaDescripcion,monto=@monto,limite=@limite,idCliente=@codigoCliente WHERE id=@codigoTarjeta;
END
GO

--Procedimiento almacenado para eliminar una tarjeta
CREATE PROCEDURE sp_EliminarTarjeta(
@descripcion NVARCHAR(100),
@cliente NVARCHAR(100)
)
AS
BEGIN
	DECLARE @codigoCliente INT
	SET @codigoCliente = (SELECT id FROM ATM.Cliente WHERE nombres=@cliente);
	DECLARE @codigoTarjeta INT
	SET @codigoTarjeta = (SELECT id FROM ATM.TarjetaCredito WHERE idCliente=@codigoCliente and descripcion=@descripcion);

	DELETE FROM ATM.TarjetaCredito WHERE id=@codigoTarjeta;
END
GO
