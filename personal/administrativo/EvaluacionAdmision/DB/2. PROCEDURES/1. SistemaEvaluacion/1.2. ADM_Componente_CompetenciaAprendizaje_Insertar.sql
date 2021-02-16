-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Componente_CompetenciaAprendizaje_Insertar 
	@codigo_cmp int,
	@codigo_com int,
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_Componente_CompetenciaAprendizaje(codigo_cmp,codigo_com,codigo_per_reg,fecha_reg,estado_cca)
		VALUES(@codigo_cmp,@codigo_com,@codigo_per,GETDATE(),1)
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

GRANT EXECUTE ON ADM_Componente_CompetenciaAprendizaje_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Componente_CompetenciaAprendizaje_Insertar TO usuariogeneral
GO