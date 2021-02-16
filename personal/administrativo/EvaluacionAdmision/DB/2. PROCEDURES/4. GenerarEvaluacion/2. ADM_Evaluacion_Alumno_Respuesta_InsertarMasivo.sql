-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Evaluacion_Alumno_Respuesta_InsertarMasivo
    @codigo_elu INT,
	@respuesta_ear VARCHAR(MAX)
AS
BEGIN
	BEGIN TRY

		DECLARE @sql varchar(MAX) = ''

        UPDATE ADM_Evaluacion_Alumno_Respuesta
        SET estado_ear = 0
        WHERE codigo_elu = @codigo_elu

		SET @sql = 'INSERT INTO dbo.ADM_Evaluacion_Alumno_Respuesta(codigo_elu,codigo_evd,codigo_ale,orden_evd,orden_ale,respuesta_ear,correcta_ear,codigo_per_reg,fecha_reg,estado_ear)'
		SET @sql += ' OUTPUT inserted.codigo_ear VALUES '
		SET @sql += @respuesta_ear

		EXEC(@sql)

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE()
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
		DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
		DECLARE @ErrorState INT = ERROR_STATE()
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
	END CATCH
END
GO

GRANT EXECUTE ON ADM_Evaluacion_Alumno_Respuesta_InsertarMasivo TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Evaluacion_Alumno_Respuesta_InsertarMasivo TO usuariogeneral
GO