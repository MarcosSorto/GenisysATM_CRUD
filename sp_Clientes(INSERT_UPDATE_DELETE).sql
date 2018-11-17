USE GenisysATM_V2
GO

CREATE PROCEDURE sp_InsertarCliente(
@identidad CHAR(13),
@nombre NVARCHAR(100),
@apellido NVARCHAR(100),
@direccion NVARCHAR(2000),
@telefono CHAR(9),
@celular CHAR(9)
)
AS
BEGIN
	INSERT INTO ATM.Cliente(identidad,nombres,apellidos,direccion,telefono,celular)
	VALUES(@identidad,@nombre,@apellido,@direccion,@telefono,@celular)
END
GO

CREATE PROCEDURE sp_ActualizarCliente(
@identidad CHAR(13),
@nombre NVARCHAR(100),
@apellido NVARCHAR(100),
@direccion NVARCHAR(2000),
@telefono CHAR(9),
@celular CHAR(9)
)
AS
BEGIN

	 UPDATE ATM.Cliente SET nombres=@nombre,apellidos=@apellido,direccion=@direccion,telefono=@telefono,celular=@celular
	 WHERE identidad=@identidad;
END
GO


CREATE PROCEDURE sp_EliminarCliente(
@nombre NVARCHAR(100),
@identidad CHAR(13)
)
AS
BEGIN
	DECLARE @codigo int;
	SET @codigo = (SELECT id FROM ATM.Cliente WHERE nombres=@nombre and identidad=@identidad);

	DELETE FROM ATM.Cliente WHERE id = @codigo;
END