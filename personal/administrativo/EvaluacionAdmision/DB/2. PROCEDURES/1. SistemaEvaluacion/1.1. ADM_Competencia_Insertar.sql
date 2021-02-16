-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Competencia_Insertar 
	@nombre_com VARCHAR(MAX),
	@nombre_corto_com VARCHAR(50),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.CompetenciaAprendizaje(nombre_com,nombre_corto_com,codigo_tcom,codigo_cat,admision_com,codigo_per_reg,fecha_reg,estado_com)
		VALUES(@nombre_com,@nombre_corto_com,1,1,1,@codigo_per,GETDATE(),1)
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

GRANT EXECUTE ON ADM_Competencia_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Competencia_Insertar TO usuariogeneral
GO