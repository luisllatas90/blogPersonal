-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_TipoEvaluacion_Indicador_Eliminar
	@codigo_tei INT,
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_TipoEvaluacion_Indicador 
		SET estado_tei = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_tei = @codigo_tei
		
		SELECT @codigo_tei  id
		
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

GRANT EXECUTE ON ADM_TipoEvaluacion_Indicador_Eliminar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_TipoEvaluacion_Indicador_Eliminar TO usuariogeneral
GO