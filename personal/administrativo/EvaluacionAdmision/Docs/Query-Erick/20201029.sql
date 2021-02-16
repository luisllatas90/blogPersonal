select * from vstCursoProgramado where codigo_Cac = 75 and nombre_Cur like '%derechos reales%'

select * from TablaArchivo where IdTabla = 33
select * from ArchivoCompartido where IdTabla = 33

declare @token VARCHAR(10)
exec @token =  retornatoken
select @token

--VWCJ2L63GR

--INSERT TablaArchivo (NombreTabla,PkTabla,Estado,RootPath,tokenTabla)
--VALUES ('EvaluacionPortada','codigo_evl','A','C:/Documentos/EvaluacionAdmision/Evaluacion/','VWCJ2L63GR')