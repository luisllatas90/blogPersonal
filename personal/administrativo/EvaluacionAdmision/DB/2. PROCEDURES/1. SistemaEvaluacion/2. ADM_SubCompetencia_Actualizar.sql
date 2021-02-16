-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_SubCompetencia_Actualizar
	@codigo_scom INT,
	@nombre_scom VARCHAR(500),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_SubCompetencia
		SET nombre_scom = @nombre_scom,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_scom = @codigo_scom
		
		SELECT @codigo_scom  id
		
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

GRANT EXECUTE ON ADM_SubCompetencia_Actualizar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_SubCompetencia_Actualizar TO usuariogeneral
GO