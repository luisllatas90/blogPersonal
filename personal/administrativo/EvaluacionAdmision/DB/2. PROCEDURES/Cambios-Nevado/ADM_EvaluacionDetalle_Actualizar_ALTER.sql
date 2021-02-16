
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- ==============================================================================================
-- HISTORIAL DE CAMBIOS
-- ==============================================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-12-01	ENevado			Acutalizar datos de indicador y nivel de complejidad
-- ==============================================================================================
ALTER PROCEDURE ADM_EvaluacionDetalle_Actualizar
	@codigo_evd INT,
	@codigo_prv INT,
	@estadovalidadcion_evd CHAR(1),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @codigo_ind int = 0, @codigo_ncp int = 0 -- 001
		SELECT @codigo_ind=codigo_ind, @codigo_ncp=codigo_ncp FROM dbo.ADM_PreguntaEvaluacion WHERE codigo_prv = @codigo_prv -- 001
	
		UPDATE dbo.ADM_EvaluacionDetalle
		SET codigo_prv = @codigo_prv,
		estadovalidacion_evd = @estadovalidadcion_evd,
		codigo_per_act = @codigo_per,
		codigo_ind = @codigo_ind, -- 001
		codigo_ncp = @codigo_ncp, -- 001
		fecha_act = GETDATE()
		WHERE codigo_evd = @codigo_evd
		
		IF @estadovalidadcion_evd = 'C'
		BEGIN
			UPDATE dbo.ADM_EvaluacionDetalle_Observacion
			SET estado_edo = 0,
			codigo_per_act = @codigo_per,
			fecha_act = GETDATE()
			WHERE codigo_evd = @codigo_evd
			AND estado_edo = 1
		END
		
		SELECT @codigo_evd id
		
	END TRY  
	BEGIN CATCH  
		PRINT ERROR_MESSAGE()  
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()  
		DECLARE @ErrorSeverity INT = ERROR_SEVERITY()  
		DECLARE @ErrorState INT = ERROR_STATE()  
		RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)  
	END CATCH 
END

