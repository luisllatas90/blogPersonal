DECLARE @codigo_per INT = 648, @codigo_apl int = 32, @codigo_alu INT = 104978


DECLARE @codigo_env int = 0, @codigo_ecm int = 0, @codigo_not int = 0, @correoasunto VARCHAR(250) = '', 
	@correocuerpo VARCHAR(MAX) = '', @profile_name VARCHAR(50) = '', @correodestino VARCHAR(500) = '',
	@nombreAlu VARCHAR(MAX), @escuelaAlu VARCHAR(MAX)
	
SELECT @nombreAlu = (apellidoPat_Alu+' '+apellidoMat_Alu+' '+nombres_Alu), @escuelaAlu = nombre_cpf, @correodestino = eMail_Alu  
FROM vstalumno where codigo_Alu = @codigo_alu
	
--SET @correodestino  = 'enevado@usat.edu.pe'
--SET @correodestino  = 'enevado@usat.edu.pe;andy.diaz@usat.edu.pe'
SET @correodestino  = 'enevado@usat.edu.pe;andy.diaz@usat.edu.pe;esaavedra@usat.edu.pe'

DECLARE @EnvNoti AS TABLE (codigo_env INT)

INSERT INTO @EnvNoti
exec CINS_EnvioNotificacionIUD 'I', @codigo_per, 0, @codigo_per, 1, @codigo_apl

SELECT @codigo_env = codigo_env FROM @EnvNoti

SELECT @codigo_env

--SET @codigo_env = 1138
--1138

DECLARE @Noti AS TABLE (codigo_not INT, tipo_not VARCHAR(10), clasificacion_not VARCHAR(4), nombre_not VARCHAR(50), abreviatura_not VARCHAR(4), 
						version_not VARCHAR(4), asunto_not VARCHAR(250), cuerpo_not VARCHAR(MAX), profile_name VARCHAR(50))
INSERT INTO @Noti
exec CINS_NotificacionListar 'GEN', '', 'EMAIL', 'ADMS', 'EAIN', '1'

SELECT @codigo_not = codigo_not, @correoasunto = asunto_not, @correocuerpo = cuerpo_not, @profile_name = profile_name FROM @Noti

set @correoasunto = REPLACE(@correoasunto , '@PARAMETRO_000' , @nombreAlu)
set @correocuerpo = REPLACE(@correocuerpo , '@parametro_001' , @nombreAlu)
set @correocuerpo = REPLACE(@correocuerpo , '@parametro_002' , @escuelaAlu)

DECLARE @EnvCorMas AS TABLE (codigo_ecm INT)

INSERT INTO @EnvCorMas
exec CINS_EnvioCorreosMasivoIUD 'I', @codigo_per, 0, 'codigo_per', @codigo_per, @codigo_apl, @correodestino, '', @correoasunto, @correocuerpo, 
								'', '01/01/1901', 0, 0, @profile_name
								
SELECT @codigo_ecm = codigo_ecm FROM @EnvCorMas

SELECT @codigo_ecm
--275576
--SET @codigo_ecm = 275576

--select * from EnvioCorreosMasivo where enviado = 0 order by codigo_ecm desc

--update CINS_EnvioNotificacionDetalle set estado = 0 where codigo_env = 1156

--update cins_envionotificacion set estado = 0 where codigo_env =  1156

--update EnvioCorreosMasivo set estado = 0 where codigo_ecm in (275616,275615,275614,275613,275612)

DECLARE @EnvNotDet AS TABLE (codigo_end INT)

INSERT INTO @EnvNotDet
EXEC CINS_EnvioNotificacionDetalleIUD 'I', @codigo_per, 0, @codigo_env, @codigo_not, 'codigo_ecm', @codigo_ecm, 'codigo_per', @codigo_per, @correodestino, 
									'', @correoasunto, @correocuerpo
									
SELECT  * FROM @EnvNotDet

--43610 43611

--select * from CINS_EnvioNotificacionDetalle  order by codigo_end desc

select * from CINS_EnvioNotificacion where estado = 1 order by codigo_env desc
select * from CINS_EnvioNotificacionDetalle where estado = 1  order by codigo_end desc
select * from EnvioCorreosMasivo where enviado = 0 and estado = 1 order by codigo_ecm desc

--exec CINS_EnviarMail


exec ADM_Noti_Ingresante 'I', 0,32,3,0,684,0,'' 

exec ADM_Noti_Ingresante 'A', 0,32,3,0,684,0,'' 

exec ADM_Noti_PagoMatricula 'I', 0,32,3,104971,684,0,''
 
exec ADM_Noti_Nivelacion 'I', 0,32,3,105066,684,0,''