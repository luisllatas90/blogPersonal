
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
ALTER PROCEDURE ADM_Evaluacion_Insertar 
	@codigo_cco int,
	@codigo_tev int,
	@nombre_evl varchar(250),
	@codigo_per int,
	@virtual_evl bit -- 001
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_Evaluacion(codigo_cco,codigo_tev,nombre_evl,codigo_per_reg,fecha_reg,estado_evl, estadovalidacion_evl, virtual_evl)
		VALUES(@codigo_cco,@codigo_tev,@nombre_evl,@codigo_per,GETDATE(),1, 'P', @virtual_evl)
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

