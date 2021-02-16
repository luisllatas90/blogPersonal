-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_TipoEvaluacion_Indicador_Insertar 
	@codigo_tev int,
	@codigo_ind int,
	@cantidad_preguntas_tei int,
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_TipoEvaluacion_Indicador(codigo_tev,codigo_ind,cantidad_preguntas_tei,codigo_per_reg,fecha_reg,estado_tei)
		VALUES(@codigo_tev,@codigo_ind,@cantidad_preguntas_tei,@codigo_per,GETDATE(),1)
		SET @id = SCOPE_IDENTITY() 
		SELECT @id  id
		
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

GRANT EXECUTE ON ADM_TipoEvaluacion_Indicador_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_TipoEvaluacion_Indicador_Insertar TO usuariogeneral
GO