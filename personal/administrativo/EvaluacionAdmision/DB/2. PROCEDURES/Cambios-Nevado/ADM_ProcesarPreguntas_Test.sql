
-- =============================================
-- Author:		ENevado
-- Create date: 2020-11-30
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ProcesarPreguntas_Test
	@codigo_evl INT,
	@ruta AS VARCHAR(500),
	@idArchivo AS BIGINT,
	@codigo_per INT,
	@rpta AS INT OUTPUT,
    @msg AS VARCHAR(500) OUTPUT
AS
BEGIN
	DECLARE @trancount BIT = 0
	BEGIN TRY
		IF @@TRANCOUNT = 0
            BEGIN
                BEGIN TRANSACTION
                SET @trancount = 1
            END

        DECLARE @sql NVARCHAR(max)

        DECLARE	@nro_item int,
				@pregunta VARCHAR(10),
				@indicador VARCHAR(10),
				@nivel_complejidad varchar(10),
				@PROCESADO bit,
				@codigo_ind int,
				@codigo_ncp int

        IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL
        BEGIN
            Truncate TABLE #TempTable
        END
        ELSE
        BEGIN
            CREATE TABLE #TempTable (
                    NRO_ITEM int,
                    PREGUNTA VARCHAR(10),
                    INDICADOR varchar(10),
                    NIVEL_COMPLEJIDAD varchar(10),
                    PROCESADO bit,
                );
        END

        SET @sql='BULK INSERT #TempTable
		FROM '''+ @ruta +
		''' WITH
		(
			FIRSTROW = 2,
			MAXERRORS = 0,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR = ''\n'',
			CODEPAGE = ''ACP''
		)' /* ROWS_PER_BATCH : NUMERO DE FILAS POR ARCHIVO QUE PERMITE INSERTAR. */

        EXEC(@sql)

        -- Verifico que todos los indicadores
        IF not exists(SELECT ind.codigo_ind
                  FROM #TempTable tt
                       JOIN dbo.ADM_Indicador ind WITH (NOLOCK) ON RTRIM(LTRIM(tt.INDICADOR)) = ind.nombre_ind
                  )
            BEGIN
                SET @rpta = 0;
                SET @msg =
                        N'El archivo de carga contiene indicadores que no se encuentran registrados en el sistema de evaluación, por favor verificar';
                IF @trancount = 1
                    ROLLBACK
                RETURN
            END
        ------
        
        -- Verifico que todos los niveles de complejidad
        IF not exists(SELECT ncp.codigo_ncp
                  FROM #TempTable tt
                       JOIN dbo.ADM_NivelComplejidadPregunta ncp WITH (NOLOCK) ON RTRIM(LTRIM(tt.NIVEL_COMPLEJIDAD)) = ncp.abreviatura_ncp
                  )
            BEGIN
                SET @rpta = 0;
                SET @msg =
                        N'El archivo de carga contiene niveles de complejidad que no se encuentran registrados en el sistema de evaluación, por favor verificar';
                IF @trancount = 1
                    ROLLBACK
                RETURN
            END
        ------
        
        DECLARE @codigo_tev INT
        SELECT @codigo_tev = evl.codigo_tev
        FROM ADM_Evaluacion evl WITH (NOLOCK)
        WHERE evl.codigo_evl = @codigo_evl;

        UPDATE dbo.ADM_Evaluacion SET idArchivoPreguntas = @idArchivo WHERE codigo_evl = @codigo_evl
        
        UPDATE dbo.ADM_TipoEvaluacion_Indicador SET estado_tei = 0, codigo_per_act = @codigo_per, fecha_act = GETDATE() 
        WHERE codigo_tev = @codigo_tev AND estado_tei = 1
        
        UPDATE
			edo
		SET
			edo.estado_edo = 0,
			edo.codigo_per_act = @codigo_per,
			edo.fecha_act = GETDATE()
		FROM
			dbo.ADM_EvaluacionDetalle_Observacion edo
			INNER JOIN dbo.ADM_EvaluacionDetalle evd
				ON evd.codigo_evd = edo.codigo_evd 
		WHERE
			evd.codigo_evl = @codigo_evl AND evd.estado_evd = 1 AND edo.estado_edo = 1
        
        UPDATE dbo.ADM_EvaluacionDetalle SET estado_evd = 0, codigo_per_act = @codigo_per, fecha_act = GETDATE() 
        WHERE codigo_evl = @codigo_evl AND estado_evd = 1

        UPDATE
			ear
		SET
			ear.estado_ear = 0,
			ear.codigo_per_act = @codigo_per,
			ear.fecha_act = GETDATE()
		FROM
			dbo.ADM_Evaluacion_Alumno_Respuesta ear
			INNER JOIN dbo.ADM_Evaluacion_Alumno elu
				ON ear.codigo_elu = elu.codigo_elu
		WHERE
			elu.codigo_evl = @codigo_evl AND elu.estado_elu = 1 AND ear.estado_ear = 1

        UPDATE dbo.ADM_Evaluacion_Alumno SET estado_elu = 0, codigo_per_act = @codigo_per, fecha_act = GETDATE() 
        WHERE codigo_evl = @codigo_evl AND estado_elu = 1

        DECLARE preg_cursor CURSOR FOR
            SELECT tt.NRO_ITEM,
                    tt.PREGUNTA,
                    tt.INDICADOR,
                    tt.NIVEL_COMPLEJIDAD,
                    tt.PROCESADO,
                    ind.codigo_ind,
                    ncp.codigo_ncp
            FROM #TempTable tt 
            INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tt.INDICADOR = ind.nombre_ind AND ind.estado_ind = 1
            INNER JOIN dbo.ADM_NivelComplejidadPregunta(NOLOCK) ncp ON tt.NIVEL_COMPLEJIDAD = ncp.abreviatura_ncp AND ncp.estado_ncp = 1
            WHERE tt.PROCESADO = 0
            ORDER BY tt.NRO_ITEM

        DECLARE @cont AS INT = 0

        OPEN preg_cursor
            FETCH NEXT FROM preg_cursor
                INTO @nro_item,
					@pregunta ,
					@indicador ,
					@nivel_complejidad ,
					@PROCESADO ,
					@codigo_ind ,
					@codigo_ncp 

			WHILE @@FETCH_STATUS = 0
				BEGIN

					INSERT INTO dbo.ADM_EvaluacionDetalle(codigo_evl,codigo_prv,orden_evd,codigo_per_reg,fecha_reg,
							estado_evd,estadovalidacion_evd,codigo_ind,codigo_ncp)
					VALUES(@codigo_evl,NULL,@nro_item,@codigo_per,GETDATE(),1,'P',@codigo_ind,@codigo_ncp)

					SET @cont = @cont + 1

                    FETCH NEXT FROM preg_cursor
                        INTO @nro_item,
						@pregunta ,
						@indicador ,
						@nivel_complejidad ,
						@PROCESADO ,
						@codigo_ind ,
						@codigo_ncp 

				END
		CLOSE preg_cursor
        DEALLOCATE preg_cursor
        
        INSERT INTO dbo.ADM_TipoEvaluacion_Indicador(codigo_tev,codigo_ind,cantidad_preguntas_tei,codigo_per_reg,fecha_reg,estado_tei)
        SELECT @codigo_tev,ind.codigo_ind, COUNT(ind.codigo_ind),@codigo_per,GETDATE(),1
            FROM #TempTable tt 
            INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tt.INDICADOR = ind.nombre_ind
            WHERE tt.PROCESADO = 0
            GROUP BY ind.codigo_ind

        --SELECT * FROM #TempTable

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

GRANT EXECUTE ON ADM_ProcesarPreguntas_Test TO iusrvirtualsistema
GRANT EXECUTE ON ADM_ProcesarPreguntas_Test TO usuariogeneral
GO


