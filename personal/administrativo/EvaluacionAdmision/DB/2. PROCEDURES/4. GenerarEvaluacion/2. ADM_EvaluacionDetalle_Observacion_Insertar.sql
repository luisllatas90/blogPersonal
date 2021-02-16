-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_EvaluacionDetalle_Observacion_Insertar 
	@codigo_evd int,
	@descripcion_edo varchar(250),
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_EvaluacionDetalle_Observacion(codigo_evd,descripcion_edo,codigo_per_reg,fecha_reg,estado_edo)
		VALUES(@codigo_evd,@descripcion_edo,@codigo_per,GETDATE(),1)
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

GRANT EXECUTE ON ADM_EvaluacionDetalle_Observacion_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_EvaluacionDetalle_Observacion_Insertar TO usuariogeneral
GO