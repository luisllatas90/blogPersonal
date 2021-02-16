-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-24	ENevado			Adicionar un campo para virtual
-- ==================================================================
CREATE PROCEDURE ADM_Evaluacion_Actualizar
	@codigo_evl INT,
	@nombre_evl VARCHAR(250),
	@estadovalidacion_evl CHAR(1),
	@codigo_per INT,
	@virtual_evl bit -- 001
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_Evaluacion 
		SET nombre_evl = @nombre_evl,
		estadovalidacion_evl = @estadovalidacion_evl,
		codigo_per_act = @codigo_per,
		virtual_evl = @virtual_evl, -- 001
		fecha_act = GETDATE()
		WHERE codigo_evl = @codigo_evl
		
		SELECT @codigo_evl id
		
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

GRANT EXECUTE ON ADM_Evaluacion_Actualizar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Evaluacion_Actualizar TO usuariogeneral
GO