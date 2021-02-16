-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Indicador_Insertar 
	@codigo_scom int,
	@nombre_ind VARCHAR(50),
	@descripcion_ind VARCHAR(500),
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_Indicador(codigo_scom,nombre_ind,descripcion_ind,codigo_per_reg,fecha_reg,estado_ind)
		VALUES(@codigo_scom,@nombre_ind,@descripcion_ind,@codigo_per,GETDATE(),1)
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

GRANT EXECUTE ON ADM_Indicador_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Indicador_Insertar TO usuariogeneral
GO