
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
ALTER PROCEDURE ADM_TipoEvaluacion_Actualizar
	@codigo_tev INT,
	@nombre_tev VARCHAR(250),
	@peso_basica_tev NUMERIC(8,2),
	@peso_intermedia_tev NUMERIC(8,2),
	@peso_avanzada_tev NUMERIC(8,2),
	@codigo_per INT,
	@virtual_tev bit -- 001
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_TipoEvaluacion 
		SET nombre_tev  = @nombre_tev ,
		peso_basica_tev = @peso_basica_tev,
		peso_intermedia_tev = @peso_intermedia_tev,
		peso_avanzada_tev = @peso_avanzada_tev,
		codigo_per_act = @codigo_per,
		virtual_tev = @virtual_tev, -- 001
		fecha_act = GETDATE()
		WHERE codigo_tev = @codigo_tev
		
		SELECT @codigo_tev id
		
	END TRY  
	BEGIN CATCH  
		PRINT ERROR_MESSAGE()  
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()  
		DECLARE @ErrorSeverity INT = ERROR_SEVERITY()  
		DECLARE @ErrorState INT = ERROR_STATE()  
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)  
	END CATCH 
END

