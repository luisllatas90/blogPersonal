-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ConfiguracionEvaluacionEvento_Peso_Insertar 
	@codigo_cee int,
	@nro_orden_ceep int,
	@peso_ceep numeric(8,2),
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_ConfiguracionEvaluacionEvento_Peso(codigo_cee,nro_orden_ceep,peso_ceep,codigo_per_reg,fecha_reg,estado_ceep)
		VALUES(@codigo_cee,@nro_orden_ceep,@peso_ceep,@codigo_per,GETDATE(),1)
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

GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Insertar TO usuariogeneral
GO