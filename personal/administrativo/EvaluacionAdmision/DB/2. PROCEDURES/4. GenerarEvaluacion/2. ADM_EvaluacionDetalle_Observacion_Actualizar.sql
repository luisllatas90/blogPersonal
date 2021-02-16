-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_EvaluacionDetalle_Observacion_Actualizar
	@codigo_edo INT,
	@descripcion_edo VARCHAR(250),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_EvaluacionDetalle_Observacion
		SET descripcion_edo = @descripcion_edo,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_edo = @codigo_edo
		
		SELECT @codigo_edo id
		
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

GRANT EXECUTE ON ADM_EvaluacionDetalle_Observacion_Actualizar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_EvaluacionDetalle_Observacion_Actualizar TO usuariogeneral
GO