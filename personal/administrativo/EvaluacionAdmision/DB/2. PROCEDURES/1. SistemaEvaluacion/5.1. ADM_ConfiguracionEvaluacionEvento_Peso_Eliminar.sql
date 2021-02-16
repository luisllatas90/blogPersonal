-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ConfiguracionEvaluacionEvento_Peso_Eliminar
	@codigo_ceep INT,
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_ConfiguracionEvaluacionEvento_Peso
		SET estado_ceep = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_ceep = @codigo_ceep
		
		SELECT @codigo_ceep  id
		
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

GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Eliminar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Eliminar TO usuariogeneral
GO