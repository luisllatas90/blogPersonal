-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_SubCompetencia_Eliminar
	@codigo_scom INT,
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_SubCompetencia
		SET estado_scom = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_scom = @codigo_scom
		
		UPDATE dbo.ADM_Indicador
		SET estado_ind  = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_scom  = @codigo_scom  AND estado_ind  = 1
		
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

GRANT EXECUTE ON ADM_SubCompetencia_Eliminar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_SubCompetencia_Eliminar TO usuariogeneral
GO