-- =============================================
-- Author:		ENevado
-- Create date: 2020-05-18
-- Description:	
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-30	andy.diaz		Quito el tipo función al participante
-- ==================================================================
ALTER PROCEDURE dbo.ADM_GrupoAdmision_Responsable_quitar
	@codigo_gre INT,
	@codigo_usu INT
AS
BEGIN
	BEGIN TRY
	
		DECLARE @codigo_per INT, @codigo_gru INT, @aula_activa BIT
		DECLARE @codigo_apl INT = 32, @codigo_tfu INT --001
		
		SELECT @codigo_per = codigo_per, @codigo_gru = codigo_gru FROM dbo.ADM_GrupoAdmision_Responsable(NOLOCK) WHERE codigo_gre = @codigo_gre
		SELECT  @aula_activa = aula_activa FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) WHERE codigo_gru = @codigo_gru
		
		UPDATE dbo.ADM_GrupoAdmision_Responsable 
		SET estado_gre = 0,
		codigo_per_act = @codigo_usu,
		fecha_act = GETDATE()
		WHERE codigo_gre = @codigo_gre --AND estado_gre = 1
		
		IF @aula_activa = 1
	   BEGIN
		exec Moodle_ActualizarParticipante_Admision @codigo_gru  , 'P',@codigo_per,'E' 
	   END

		--001
        select @codigo_Tfu = codigo_tfu from TipoFuncion where abreviatura_Tfu = 'APEVAD';

		IF EXISTS(SELECT codigo_uap FROM UsuarioAplicacion u with(nolock) WHERE u.codigo_Apl = @codigo_apl and u.codigo_Tfu = @codigo_tfu and u.codigo_uap = @codigo_per)
        begin
            delete from UsuarioAplicacion
            where codigo_Apl = @codigo_apl
            and codigo_Tfu = @codigo_tfu
            and codigo_uap = @codigo_per
        end
		--/001
		
		SELECT @codigo_gre id
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

