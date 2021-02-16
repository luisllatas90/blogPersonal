-- =============================================  
-- Author:  ENevado  
-- Create date: 2020-05-18  
-- Description:   
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-30	andy.diaz		Agrego el tipo función al participante
-- ==================================================================
ALTER PROCEDURE dbo.ADM_GrupoAdmision_Responsable_agregar   
 @codigo_gru INT,  
 @codigo_per INT,  
 @codigo_usu INT  
AS  
BEGIN  
 BEGIN TRY  
	DECLARE @codigo_gre INT = 0, @aula_activa BIT
    DECLARE @codigo_apl INT = 32, @codigo_tfu INT --001
	
	SELECT  @aula_activa = aula_activa FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) WHERE codigo_gru = @codigo_gru
   
   INSERT INTO dbo.ADM_GrupoAdmision_Responsable(codigo_gru, codigo_per, estado_gre, codigo_per_reg, fecha_reg)  
   VALUES(@codigo_gru, @codigo_per, 1, @codigo_usu, GETDATE())  
   
   set @codigo_gre = SCOPE_IDENTITY() 
   
   IF @aula_activa = 1
   BEGIN
	exec Moodle_ActualizarParticipante_Admision @codigo_gru  , 'P',@codigo_per,'A' 
   END

	--001
    select @codigo_Tfu = codigo_tfu from TipoFuncion where abreviatura_Tfu = 'APEVAD';

    if not exists(select codigo_uap from UsuarioAplicacion u with(nolock) where u.codigo_Apl = @codigo_apl and u.codigo_Tfu = @codigo_tfu and u.codigo_uap = @codigo_per)
    begin
        if not exists(select codigo_apl from FuncionUsuario with(nolock) where codigo_apl = @codigo_apl and codigo_Tfu = @codigo_tfu)
            insert into FuncionUsuario(codigo_apl, codigo_tfu) values (@codigo_apl, @codigo_tfu)

        insert into UsuarioAplicacion (tipo_uap, codigo_uap, codigo_apl, codigo_tfu, restriccion_uap, codigorestriccion_uap, fechareg_aud, usuario_aud, host_aud)
        values (1, @codigo_per, @codigo_apl, @codigo_tfu, 0, 0, getdate(), null, null)
    end
	--/001
   
   select @codigo_gre id

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

