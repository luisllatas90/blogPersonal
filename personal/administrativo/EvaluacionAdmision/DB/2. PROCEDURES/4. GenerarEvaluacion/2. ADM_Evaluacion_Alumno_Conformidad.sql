-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Evaluacion_Alumno_Conformidad
	@codigo_elu INT,
	@estadoverificacion_elu CHAR(1),
	@observacion_elu VARCHAR(250),
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_Evaluacion_Alumno
		SET estadoverificacion_elu = @estadoverificacion_elu,
		observacion_elu = @observacion_elu,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_elu = @codigo_elu
		
		SELECT @codigo_elu id
		
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

GRANT EXECUTE ON ADM_Evaluacion_Alumno_Conformidad TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Evaluacion_Alumno_Conformidad TO usuariogeneral
GO