-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Competencia_Actualizar
	@codigo_com INT,
	@nombre_com VARCHAR(MAX),
	@nombre_corto_com VARCHAR(50),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.CompetenciaAprendizaje
		SET nombre_com = @nombre_com,
		nombre_corto_com = @nombre_corto_com,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_com = @codigo_com
		
		SELECT @codigo_com id
		
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

GRANT EXECUTE ON ADM_Competencia_Actualizar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Competencia_Actualizar TO usuariogeneral
GO