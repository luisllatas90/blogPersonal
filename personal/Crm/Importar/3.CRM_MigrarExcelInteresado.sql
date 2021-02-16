--DROP PROCEDURE [MigrarExcelInteresado]

/*    
* Fecha Crea: 16/08/2018 - 11:23:00am     
* Modificado por: HCano    
* Descripción: Leer Archivo Excel e Importar Datos de acuerdo a consulta con OLDBConnection
 /* Historial de Cambios */    
 -- Codigo Fecha  Usuario  Detalle    
*/  
ALTER PROCEDURE [dbo].[CRM_MigrarExcelInteresado]
@ruta varchar(500),
@id_evento int,
@idarchivocompartido int,
@codigo_per int
AS
BEGIN

--DECLARE @codigo_per int = 684
--DECLARE @idarchivocompartido int = 1531
--declare @ruta varchar(500) ='C:\MyExcel.xls'
BEGIN TRY
DECLARE @sql NVARCHAR(max)
DECLARE @mensaje varchar(500)
DECLARE @rpta int
DECLARE @EsMiTransaccion bit, @Resultado int    
SELECT @EsMiTransaccion = 0, @Resultado = 0    
     
IF @@TRANCOUNT = 0    
BEGIN -- Como no hay transacción, la inicio yo    
	BEGIN TRANSACTION    
	SELECT @EsMiTransaccion = 1    
END    
ELSE-- Ya hay una transacción, no inicio otra    
SELECT @EsMiTransaccion = 0   
/*
	SET @sql='INSERT INTO [InteresadoExcelImportado_CRM]
				SELECT [FECHA],[ID_TIPODOCUMENTO],CAST([NRO_DOCUMENTO] AS BIGINT),[APELLIDO_PATERNO],[APELLIDO_MATERNO],[NOMBRES],CAST([CELULAR] AS BIGINT),[EMAIL],[DIRECCION]
					,[ID_DISTRITO],[DISTRITO],[PROVINCIA],[ID_COLEGIO],[COLEGIO],[ID_NIVEL],[NIVEL],[ID_CARRERA],[CARRERA]
					,' + CAST(@id_evento AS VARCHAR(20)) + ',[CATEGORIA],[TIPO],0 AS PROCESADO,GETDATE() AS FECHA_REGISTRO,'+ 
				CAST(@codigo_per AS VARCHAR(20))+' AS codigo_per,'+ CAST(@idarchivocompartido AS VARCHAR(10)) +' AS ID_ARCHIVO
				FROM OPENROWSET(
				   ''Microsoft.ACE.OLEDB.12.0'',
				   ''Excel 12.0;HDR=YES;IMEX=1;Database=' + @ruta + ''',
				   ''SELECT  [FECHA],[ID_TIPODOCUMENTO],[NRO_DOCUMENTO],[APELLIDO_PATERNO],[APELLIDO_MATERNO],[NOMBRES],[CELULAR],[EMAIL],[DIRECCION]
					,[ID_DISTRITO],[DISTRITO],[PROVINCIA],[ID_COLEGIO],[COLEGIO],[ID_NIVEL],[NIVEL],[ID_CARRERA],[CARRERA]
					,[CATEGORIA],[TIPO]
					FROM [BASE GENERAL$] WHERE [ID_TIPODOCUMENTO] IS NOT NULL AND [NRO_DOCUMENTO] IS NOT NULL AND [NOMBRES] <> "" AND [APELLIDO_PATERNO] <> ""'')'

	exec(@sql)*/
	SET @sql='BULK INSERT InteresadoExcelImportado_CRM
		FROM '''+ @ruta +
		''' WITH 
		( 
			FIRSTROW = 2, 
			MAXERRORS = 0, 
			FIELDTERMINATOR = '','', 
			ROWTERMINATOR = ''\n'',
			CODEPAGE = ''ACP''
		)' /* ROWS_PER_BATCH : NUMERO DE FILAS POR ARCHIVO QUE PERMITE INSERTAR. */
	    
	EXEC(@SQL)


--DECLARE @nro_filas int = (SELECT COUNT(*) FROM InteresadoExcelImportado_CRM WHERE IdArchivosCompartidos=@idarchivocompartido)
DECLARE @nro_filas int = (SELECT COUNT(*) FROM InteresadoExcelImportado_CRM WHERE PROCESADO=0)
DECLARE @contador int = 0
--exec PER_ConsultarPeriodoLaborableVigente 
DECLARE @codigo_int int = 0
DECLARE @codigo_Reg int,@id_tipodoc int,@nro_doc varchar(50),@fecha datetime,@apepat varchar(200),@apemat varchar(200),@nombres varchar(400),@celular  varchar(50)
,@correo varchar(100),@direccion varchar(1000),@id_distrito int ,@id_colegio int ,@id_nivel VARCHAR(10) ,@id_carrera int ,@cod_evento int

DECLARE Cursor_InsercionInteresado CURSOR FOR
   
SELECT codigo_reg,fecha,id_tipodocumento,nro_documento,apellido_paterno,apellido_materno,nombres,celular,email,direccion,id_distrito,id_colegio,id_nivel,id_carrera,@id_evento
--FROM InteresadoExcelImportado_CRM WHERE IdArchivosCompartidos=@idarchivocompartido
FROM InteresadoExcelImportado_CRM WHERE procesado=0

OPEN Cursor_InsercionInteresado 
FETCH Cursor_InsercionInteresado   
INTO @codigo_Reg,@fecha,@id_tipodoc,@nro_doc,@apepat,@apemat,@nombres,@celular,@correo,@direccion,@id_distrito,@id_colegio,@id_nivel,@id_carrera,@cod_evento
WHILE @@FETCH_STATUS = 0  
      BEGIN
		-- VERIFICAMOS SI INTERESADO EXISTE
			SET @codigo_int = ISNULL((SELECT codigo_int FROM Interesado_CRM WHERE codigo_doci=@id_tipodoc AND numerodoc_int=@nro_doc),0)
			
			--SI NO EXISTE CREAMOS EL INTERESADO
			IF @codigo_int=0
			BEGIN
				INSERT INTO [BDUSAT].[dbo].[Interesado_CRM]
				   ([codigo_doci],[numerodoc_int],[apepaterno_int],[apematerno_int],[nombres_int],[fechanacimiento_int],[Grado_int]
				   ,[codigo_ied],[codigo_cpf],[codigo_sin],[estado_int],[usuario_reg],[fecha_reg],[linkfb_int])
				VALUES
					(@id_tipodoc,@nro_doc,UPPER(@apepat),UPPER(ISNULL(@apemat,'')),UPPER(@nombres),NULL,@id_nivel,@id_colegio,@id_carrera,1,1,@codigo_per,GETDATE(),NULL)
				
				--Asignamos Nuevo Codigo
				SET @codigo_int = @@IDENTITY
			END
			
			--AGREGAMOS DIRECCIÓN
			IF @direccion IS NOT NULL AND (SELECT COUNT(*) FROM [DireccionInteresado_CRM] WHERE codigo_int=@codigo_int AND estado_din=1 AND direccion_din=@direccion AND codigo_dis=@id_distrito)=0
			BEGIN
				--Quitamos Vigencia a Direcciones Anteriores.
				UPDATE [DireccionInteresado_CRM] SET vigencia_din=0 WHERE codigo_int=@codigo_int
				--Agregamos Nueva Dirección como vigente.
				INSERT INTO [BDUSAT].[dbo].[DireccionInteresado_CRM]
				   ([codigo_int],[codigo_dep],[codigo_pro],[codigo_dis],[direccion_din],[vigencia_din],[estado_din],[usuario_reg],[fecha_reg])
				VALUES
					(@codigo_int,NULL,NULL,@id_distrito,UPPER(@direccion),1,1,@codigo_per,GETDATE())
			END
			
			--AGREGAMOS TELEFONO
			IF @celular IS NOT NULL AND (SELECT COUNT(*) FROM [TelefonoInteresado_CRM] WHERE codigo_int=@codigo_int AND estado_tei=1 AND numero_tei=@celular)=0
			BEGIN
				--Quitamos Vigencia a Telefonos Anteriores.
				UPDATE [TelefonoInteresado_CRM] SET [vigencia_tei]=0 WHERE codigo_int=@codigo_int
				--Agregamos Nuevo telefono como vigente.
				INSERT INTO [BDUSAT].[dbo].[TelefonoInteresado_CRM]
				   ([codigo_int],[tipotel_tei],[numero_tei],[detalle_tei],[vigencia_tei],[estado_tei],[usuario_reg],[fecha_reg])
				VALUES
					(@codigo_int,'CELULAR',@celular,NULL,1,1,@codigo_per,GETDATE())
			END

			--AGREGAMOS CORREO
			IF @correo IS NOT NULL AND (SELECT COUNT(*) FROM [Emailinteresado_CRM] WHERE codigo_int=@codigo_int AND estado_emi=1 AND descripcion_emi=@correo)=0
			BEGIN
				--Quitamos Vigencia a Telefonos Anteriores.
				UPDATE [Emailinteresado_CRM] SET [vigencia_emi]=0 WHERE codigo_int=@codigo_int
				--Agregamos Nuevo telefono como vigente.
				INSERT INTO [BDUSAT].[dbo].[EmailInteresado_CRM]
					([codigo_int],[tipo_emi],[descripcion_emi],[detalle_emi],[vigencia_emi],[estado_emi],[usuario_reg],[fecha_reg])
				VALUES
					(@codigo_int,'PERSONAL',@correo,NULL,1,1,@codigo_per,GETDATE())
			END
			
			--AGREGAMOS CARRERA PROFESIONAL
			IF @id_carrera IS NOT NULL AND (SELECT COUNT(*) FROM CarreraProfesionalInteresado_CRM WHERE codigo_int=@codigo_int AND estado_cpi=1 AND codigo_cpf=@id_carrera AND codigo_eve=@cod_evento)=0
			BEGIN
				--Agregamos Nuevo Carrera Profesional.
				INSERT INTO [BDUSAT].[dbo].[CarreraProfesionalInteresado_CRM]
					([codigo_int],[codigo_cpf],[codigo_eve],[detalle_cpi],[prioridad_cpi],[estado_cpi],[usuario_reg],[fecha_reg])
				VALUES
					(@codigo_int,@id_carrera,@cod_evento,NULL,1,1,@codigo_per,GETDATE())
				--Reordenamos y ponemos como Prioridad 1 la ultima Registrada
				UPDATE i
					SET i.prioridad_cpi= i.Row#
				FROM (
					SELECT ROW_NUMBER() OVER(ORDER BY prioridad_cpi ASC, FECHA_reg desc) AS Row#,cpf.nombre_cpf,cp.*
					FROM CarreraProfesionalInteresado_CRM cp
					INNER JOIN CarreraProfesional cpf ON cpf.codigo_Cpf=cp.codigo_cpf
					WHERE codigo_int=@codigo_int AND codigo_eve=@cod_evento AND 
					estado_cpi=1
				) AS i
			END
			
			--INSCRIBIMOS INTERESADO EN EVENTO SI NO ESTUVIERA INSCRITO
			IF @cod_evento IS NOT NULL AND (SELECT COUNT(*) FROM EventoInteresado_CRM WHERE codigo_eve=@cod_evento AND codigo_int=@codigo_int AND estado_ein=1) =0
			BEGIN
				INSERT INTO [BDUSAT].[dbo].[EventoInteresado_CRM]
					([codigo_eve],[codigo_int],[estado_ein],[usuario_reg],[fecha_reg])
				VALUES
					(@cod_evento,@codigo_int,1,@codigo_per,GETDATE())
			END
			
			-- COLOCAMOS COMO PROCESADO EL REGISTRO
			UPDATE InteresadoExcelImportado_CRM SET PROCESADO=1 WHERE CODIGO_REG=@codigo_Reg
			SET @contador = @contador + 1
			
      FETCH Cursor_InsercionInteresado   
      INTO @codigo_Reg,@fecha,@id_tipodoc,@nro_doc,@apepat,@apemat,@nombres,@celular,@correo,@direccion,@id_distrito,@id_colegio,@id_nivel,@id_carrera,@cod_evento
      END
CLOSE Cursor_InsercionInteresado  
DEALLOCATE Cursor_InsercionInteresado  


	IF @EsMiTransaccion = 1
	BEGIN
		commit transaction
		SET @mensaje='Importación Correcta de ' + CAST(@nro_filas AS VARCHAR(50)) + ' Interesados, Interesados Procesados Correctamente: '+ CAST(@contador AS VARCHAR(50)) + '.'
		SET @rpta=1
	END
END TRY      
BEGIN CATCH
	IF @EsMiTransaccion = 1
	BEGIN
		rollback transaction
		--SET @mensaje = 'Importación No se pudo Completar, Verifique Archivo y su Estructura, Campos Obligatorios para Importación: DNI,Apellido Paterno, Apellido Materno y Nombres.' --ERROR
		SET @mensaje = ERROR_MESSAGE() --ERROR
		SET @rpta=0
	END
END CATCH;
    
SELECT @mensaje Mensaje,@rpta Respuesta    


END



GO

GRANT EXECUTE ON [dbo].[CRM_MigrarExcelInteresado] to usuariogeneral
GRANT EXECUTE ON [dbo].[CRM_MigrarExcelInteresado] to IusrReporting
GRANT EXECUTE ON [dbo].[CRM_MigrarExcelInteresado] to IUsrVirtualSistema

