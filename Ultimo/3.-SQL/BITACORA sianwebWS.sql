
---------------------------------------------
CREATE DATABASE 
sianwebWS 
GO
---------------------------------------------
USE sianweb 
GO
---------------------------------------------

Create Table CatalgoBD
(
 idBD int,
 Descripcion nvarchar(100)
)
GO
---------------------------------------------
Insert into CatalgoBD
Select 1,'sianweb'
union 
Select 2,'sianweb_'
---------------------------------------------

Create Table BitacoraWS
(
id int identity(1,1),
Metodo nvarchar(100),
idBD int,
Fecha datetime,
HoraIni time,
HoraTer time null
)
GO
---------------------------------------------
CREATE Procedure InsertBitacoraWS
@Metodo nvarchar(100),@idBD int
AS
BEGIN
	Insert into BitacoraWS
	Select @Metodo,@idBD,GETDATE(),convert(time,CONVERT(VARCHAR(8),GETDATE(),108)),null
	
	Declare @id int=(Select Max(id) from BitacoraWS)
	
	IF EXISTS(Select Descripcion from CatalgoBD Where idBD=@idBD)
	BEGIN
		Select Descripcion,@id as id from CatalgoBD Where idBD=@idBD
	END
	ELSE
	BEGIN
		Select 'Desconocida' as Descripcion,@id as id
	END	
END
GO
---------------------------------------------
CREATE Procedure UpdateBitacoraWS
@id int,@idBD int
AS
BEGIN

	Declare @TiempoTer time
	SET @TiempoTer=convert(time,CONVERT(VARCHAR(8),GETDATE(),108))
		
	Update BitacoraWS
	Set HoraTer=@TiempoTer
	Where id=@id and idBd=@idBD

END
GO
---------------------------------------------

ALTER Procedure ConsultarBitacoraWS
@Metodo nvarchar(100)=NULL,
@idBD int=NULL
AS
BEGIN
	Select 
	bws.id,
	bws.Metodo,
	bws.idBD,
	Isnull(cbd.Descripcion,'BD no existe en la Lista') as Base_de_Datos,
	bws.Fecha,
	ISNULL(DATEDIFF(MINUTE, bws.HoraIni,bws.HoraTer),0) AS Minutos,
	ISNULL(DATEDIFF(SECOND, bws.HoraIni,bws.HoraTer),0) AS Segundos
	From BitacoraWS bws
	Left Join CatalgoBD cbd
	on bws.idBD=cbd.idBD
	Where bws.Metodo=coalesce(@Metodo,bws.Metodo) and bws.idBD=coalesce(@idBD,bws.idBD)
	Order By bws.id
END
GO
---------------------------------------------