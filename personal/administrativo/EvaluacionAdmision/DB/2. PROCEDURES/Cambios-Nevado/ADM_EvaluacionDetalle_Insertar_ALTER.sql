
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
ALTER PROCEDURE ADM_EvaluacionDetalle_Insertar 
	@codigo_evl int,
	@codigo_prv int,
	@orden_evl int,
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @codigo_ind int = 0, @codigo_ncp int = 0 -- 001
		SELECT @codigo_ind=codigo_ind, @codigo_ncp=codigo_ncp FROM dbo.ADM_PreguntaEvaluacion WHERE codigo_prv = @codigo_prv -- 001
		
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_EvaluacionDetalle(codigo_evl,codigo_prv,orden_evd,codigo_per_reg,fecha_reg,estado_evd,estadovalidacion_evd,codigo_ind,codigo_ncp)
		VALUES(@codigo_evl,@codigo_prv,@orden_evl,@codigo_per,GETDATE(),1, 'P',@codigo_ind,@codigo_ncp)
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

