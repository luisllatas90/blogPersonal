-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-23	ENevado			Adicionar un campo para virtual
-- ==================================================================
CREATE PROCEDURE ADM_TipoEvaluacion_Insertar 
	@nombre_tev VARCHAR(250),
	@peso_basica_tev numeric(8,2),
	@peso_intermedia_tev numeric(8,2),
	@peso_avanzada_tev numeric(8,2),
	@codigo_per int,
	@virtual_tev bit -- 001
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_TipoEvaluacion(nombre_tev,peso_basica_tev,peso_intermedia_tev,peso_avanzada_tev,codigo_per_reg,fecha_reg,estado_tev,virtual_tev)
		VALUES(@nombre_tev,@peso_basica_tev,@peso_intermedia_tev,@peso_avanzada_tev,@codigo_per,GETDATE(),1,@virtual_tev)
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

GRANT EXECUTE ON ADM_TipoEvaluacion_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_TipoEvaluacion_Insertar TO usuariogeneral
GO