
-- =============================================
-- Author:		ENevado
-- Create date: 2020-05-11
-- Description:	
-- ============================================================================================
--	CÓDIGO	FECHA		AUTOR		OBSERVACIÓN
-- 001		2020-09-03	andy.diaz	Adicionar los campos de fecha y hora de inicio y fin
-- 002		2020-09-14	ENevado		Adicionar el campo codigo tipo grupo evaluacion
-- =========================================================================================
ALTER PROCEDURE ADM_GrupoAdmisionVirtual_actualizar 
	@codigo_gru INT,
	@codigo_cco VARCHAR(MAX),
	@codigo VARCHAR(10),
	@nombre VARCHAR(100),
	@aula_activa BIT,
	@estado INT,
	@codigo_amb INT,
	@capacidad INT,
	@codigo_per INT,
	@codigo_tge INT, --> 002
	@fechaHoraInicio_gru DATETIME = NULL, --001
    @fechaHoraFin_gru DATETIME = NULL --001
AS
BEGIN
	BEGIN TRY
		UPDATE dbo.ADM_GrupoAdmisionVirtual
		SET codigo = @codigo,
		nombre = @nombre,
		codigo_amb = @codigo_amb,
		capacidad = @capacidad,
        fechaHoraInicio_gru = @fechaHoraInicio_gru, --001
        fechaHoraFin_gru = @fechaHoraFin_gru, --001
        codigo_tge = @codigo_tge,  --> 002
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_gru = @codigo_gru
		SELECT @codigo_gru
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