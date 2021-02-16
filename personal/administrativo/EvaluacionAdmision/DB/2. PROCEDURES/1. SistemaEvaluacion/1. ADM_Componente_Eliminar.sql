-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Componente_Eliminar
	@codigo_cmp INT,
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_Componente
		SET estado_cmp = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_cmp = @codigo_cmp
		
		UPDATE dbo.ADM_Componente_CompetenciaAprendizaje
		SET estado_cca = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_cmp = @codigo_cmp AND estado_cca = 1
		
		SELECT @codigo_cmp  id
		
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

GRANT EXECUTE ON ADM_Componente_Eliminar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Componente_Eliminar TO usuariogeneral
GO