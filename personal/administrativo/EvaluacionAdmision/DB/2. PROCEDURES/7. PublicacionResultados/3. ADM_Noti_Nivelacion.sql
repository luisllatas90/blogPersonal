
-- =============================================
-- Author:		ENevado
-- Create date: 2020-10-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Noti_Nivelacion
	@tipo VARCHAR(4),
	@codigo_cco INT,
	@codigo_apl INT,
	@codigo_evl INT ,
	@codigo_alu INT,
	@codigo_per INT,
	@rpta AS INT OUTPUT,
    @msg AS VARCHAR(200) OUTPUT
AS
BEGIN
	DECLARE @trancount BIT = 0
	BEGIN TRY
		DECLARE @codigo_env int = 0, @codigo_ecm int = 0, @codigo_not int = 0, @correoasunto VARCHAR(250) = '', 
				@correocuerpo VARCHAR(MAX) = '', @profile_name VARCHAR(50) = '', @correodestino VARCHAR(500) = '',
				@nombreAlu VARCHAR(MAX), @escuelaAlu VARCHAR(MAX), @abrev_not VARCHAR(4)
		
		DECLARE @EnvNoti AS TABLE (codigo_env INT)

		INSERT INTO @EnvNoti
		EXEC CINS_EnvioNotificacionIUD 'I', @codigo_per, 0, @codigo_per, 1, @codigo_apl

		SELECT @codigo_env = codigo_env FROM @EnvNoti
		
		IF @tipo='I'
			SET @abrev_not = 'IRNV'
		--ELSE IF @tipo='A'
		--	SET @abrev_not = 'EAAC'
		--ELSE
		--	SET @abrev_not = 'EANI'
		
		DECLARE @Noti AS TABLE (codigo_not INT, tipo_not VARCHAR(10), clasificacion_not VARCHAR(4), nombre_not VARCHAR(50), abreviatura_not VARCHAR(4), 
							version_not VARCHAR(4), asunto_not VARCHAR(250), cuerpo_not VARCHAR(MAX), profile_name VARCHAR(50))
		INSERT INTO @Noti
		EXEC CINS_NotificacionListar 'GEN', '', 'EMAIL', 'ADMS', @abrev_not, '1'

		
		DECLARE @cont AS INT = 0
		
		DECLARE ingresantes CURSOR FOR
			SELECT elu.codigo_alu, alu.apellidoPat_Alu+' '+alu.apellidoMat_Alu+' '+nombres_Alu Alumno, alu.nombre_cpf Carrera, alu.eMail_Alu Correo 
			FROM dbo.ADM_Evaluacion(NOLOCK) evl 
			INNER JOIN dbo.ADM_Evaluacion_Alumno(NOLOCK) elu ON evl.codigo_evl = elu.codigo_evl
			INNER JOIN vstAlumno alu ON elu.codigo_alu = alu.codigo_Alu
			INNER JOIN (SELECT ean.codigo_elu, COUNT(ean.codigo_ean) cant FROM
						dbo.ADM_Evaluacion_Alumno_Nivelacion(NOLOCK) ean
						WHERE ean.estado_ean = 1 AND ean.necesita_nivelacion_ean = 1
						GROUP BY ean.codigo_elu) AS tab ON elu.codigo_elu = tab.codigo_elu
			WHERE evl.estado_evl = 1 AND elu.estado_elu = 1 --AND elu.condicion_ingreso_elu = @tipo
			AND (evl.codigo_cco = @codigo_cco OR (evl.codigo_cco = evl.codigo_cco - @codigo_cco)) 
			AND (evl.codigo_evl = @codigo_evl OR (evl.codigo_evl = evl.codigo_evl - @codigo_evl))
			AND (elu.codigo_alu = @codigo_alu OR (elu.codigo_alu = elu.codigo_alu - @codigo_alu))
		
		OPEN ingresantes
			FETCH NEXT FROM ingresantes
				INTO @codigo_alu, @nombreAlu, @escuelaAlu, @correodestino
				
			WHILE @@FETCH_STATUS = 0
				BEGIN
				
					SELECT @codigo_not = codigo_not, @correoasunto = asunto_not, @correocuerpo = cuerpo_not, @profile_name = profile_name FROM @Noti
					
					DECLARE @competenciaNiv VARCHAR(MAX) = ''
					
					SELECT @competenciaNiv = @competenciaNiv + com.nombre_com + ' - ' 
					FROM dbo.ADM_Evaluacion_Alumno_Nivelacion(NOLOCK) ean
					INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON ean.codigo_com = com.codigo_com
					INNER JOIN dbo.ADM_Evaluacion_Alumno(NOLOCK) elu ON ean.codigo_elu = elu.codigo_elu
					WHERE elu.estado_elu = 1 AND ean.estado_ean = 1 AND ean.necesita_nivelacion_ean = 1 
					AND elu.codigo_alu = @codigo_alu AND elu.codigo_evl = @codigo_evl
					order by com.codigo_com
					
					SET @competenciaNiv = LEFT(@competenciaNiv, LEN(@competenciaNiv) - 2)
					
					--SELECT @competenciaNiv
					
					set @correoasunto = REPLACE(@correoasunto , '@PARAMETRO_000' , @nombreAlu)
					
					set @correocuerpo = REPLACE(@correocuerpo , '@parametro_001' , @nombreAlu)
					set @correocuerpo = REPLACE(@correocuerpo , '@parametro_002' , @escuelaAlu)
					set @correocuerpo = REPLACE(@correocuerpo , '@parametro_003' , @competenciaNiv)
					
					SET @correodestino  = 'enevado@usat.edu.pe'
					--SET @correodestino  = 'enevado@usat.edu.pe;andy.diaz@usat.edu.pe'
					--SET @correodestino  = 'enevado@usat.edu.pe;andy.diaz@usat.edu.pe;esaavedra@usat.edu.pe'
					
					DECLARE @EnvCorMas AS TABLE (codigo_ecm INT)

					INSERT INTO @EnvCorMas
					EXEC CINS_EnvioCorreosMasivoIUD 'I', @codigo_per, 0, 'codigo_alu', @codigo_alu, @codigo_apl, @correodestino, '', @correoasunto, 
													@correocuerpo, '', '01/01/1901', 0, 0, @profile_name
													
					SELECT @codigo_ecm = codigo_ecm FROM @EnvCorMas
					
					EXEC CINS_EnvioNotificacionDetalleIUD 'I', @codigo_per, 0, @codigo_env, @codigo_not, 'codigo_ecm', @codigo_ecm, 'codigo_alu', 
														@codigo_alu, @correodestino, '', @correoasunto, @correocuerpo
														
					SET @cont = @cont + 1
					
					FETCH NEXT FROM ingresantes
						INTO @codigo_alu, @nombreAlu, @escuelaAlu, @correodestino
				END
		CLOSE ingresantes
		DEALLOCATE ingresantes
	    
		SET @rpta = 1
		SET @msg = 'Operación realizada correctamente, registros procesados: ' + CAST(@cont AS VARCHAR(50))
	    
		IF @trancount = 1
			COMMIT
		
	END TRY	
	BEGIN CATCH
        IF @trancount = 1
            ROLLBACK

        SET @rpta = -1;
        SET @msg = 'Ocurrió un error en la transacción'

        PRINT ERROR_MESSAGE()

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
			
END
GO

--GRANT EXECUTE ON ADM_Noti_Nivelacion TO iusrvirtualsistema
--GRANT EXECUTE ON ADM_Noti_Nivelacion TO usuariogeneral
--GO
