-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Indicador_Actualizar
	@codigo_ind INT,
	@nombre_ind VARCHAR(50),
	@descripcion_ind VARCHAR(500),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_Indicador
		SET nombre_ind = @nombre_ind,
		descripcion_ind = @descripcion_ind,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_ind = @codigo_ind
		
		SELECT @codigo_ind  id
		
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

GRANT EXECUTE ON ADM_Indicador_Actualizar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Indicador_Actualizar TO usuariogeneral
GO