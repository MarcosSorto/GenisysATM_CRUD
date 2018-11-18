USE GenisysATM_V2
GO

--Procedimiento Almacenado para la inserción de cuentas de cliete
CREATE PROCEDURE [dbo].[sp_InsertarCuenta](
@numero CHAR(14),
@cliente NVARCHAR(100),
@saldo DECIMAL(10,2),
@pin CHAR(4)
)
AS
BEGIN
	DECLARE @codigoCliente INT
	SET @codigoCliente =(SELECT id FROM ATM.Cliente WHERE nombres=@cliente);

	INSERT INTO ATM.CuentaCliente(numero,idCliente,saldo,pin) VALUES (@numero,@codigoCliente,@saldo,@pin);

END
GO
--Procedimiento Almacenado Para actualizar informacion de la cuenta
CREATE PROCEDURE sp_ActualizarCuentaCliente(
@numero CHAR(14),
@cliente NVARCHAR(100),
@saldo DECIMAL(10,2),
@pin CHAR(4),
@nuevoNumero CHAR(14)
)
AS
BEGIN
	DECLARE @codigoCliente INT
	SET @codigoCliente =(SELECT id FROM ATM.Cliente WHERE nombres=@cliente);

	UPDATE ATM.CuentaCliente SET saldo=@saldo, pin=@pin, numero=@nuevoNumero WHERE numero=@numero;

END
GO
--Procedimiento Para Eliminar una cuenta

CREATE PROCEDURE sp_EliminarCuenta(
@numero CHAR(14)
)
AS
BEGIN
	DELETE FROM ATM.CuentaCliente WHERE numero=@numero;
END
GO