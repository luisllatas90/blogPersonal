-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ConfiguracionEvaluacionEvento_Peso_Actualizar
	@codigo_ceep INT,
	@peso_ceep NUMERIC(8,2),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_ConfiguracionEvaluacionEvento_Peso
		SET peso_ceep = @peso_ceep,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_ceep = @codigo_ceep
		
		SELECT @codigo_ceep id
		
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

GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Actualizar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Actualizar TO usuariogeneral
GO