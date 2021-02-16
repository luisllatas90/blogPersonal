
-- =============================================  
-- Author:  ENevado  
-- Create date: 2020-05-11  
-- Description:   
-- ============================================================================================
--	CÓDIGO	FECHA		AUTOR		OBSERVACIÓN
-- 001		2020-09-03	andy.diaz	Adicionar los campos de fecha y hora de inicio y fin
-- 002		2020-09-14	ENevado		Adicionar el campo codigo de tipo grupo evaluacion
-- =========================================================================================
ALTER PROCEDURE dbo.ADM_GrupoAdmisionVirtual_insertar   
 @codigo_cco INT,  
 @codigo VARCHAR(10),  
 @nombre VARCHAR(100),  
 @aula_activa BIT,  
 @estado INT,  
 @codigo_amb INT,  
 @capacidad INT,  
 @codigo_per INT,
 @codigo_tge INT,
 @fechaHoraInicio_gru DATETIME = NULL, --001
 @fechaHoraFin_gru DATETIME = NULL --001
AS  
BEGIN  
 BEGIN TRY  
  
  declare @id int = 0, @virtual BIT 
  
  SELECT  @virtual = isnull(virtual_amb,0) FROM dbo.ambiente(NOLOCK) WHERE codigo_Amb  = @codigo_amb

  --INSERT INTO dbo.ADM_GrupoAdmisionVirtual(codigo_cco, codigo, nombre, aula_activa, estado, codigo_per_reg, fecha_reg, codigo_amb, capacidad, fechaHoraInicio_gru, fechaHoraFin_gru)
  --VALUES (@codigo_cco, @codigo, @nombre, @aula_activa, @estado, @codigo_per, GETDATE(), @codigo_amb, @capacidad, @fechaHoraInicio_gru, @fechaHoraFin_gru) --001
  
  INSERT INTO dbo.ADM_GrupoAdmisionVirtual(codigo_cco, codigo, nombre, aula_activa, estado, codigo_per_reg, fecha_reg, codigo_amb, capacidad, codigo_tge, fechaHoraInicio_gru, fechaHoraFin_gru)
  VALUES (@codigo_cco, @codigo, @nombre, @aula_activa, @estado, @codigo_per, GETDATE(), @codigo_amb, @capacidad, @codigo_tge, @fechaHoraInicio_gru, @fechaHoraFin_gru) --002
  
  SET @id = SCOPE_IDENTITY() 
  
  IF @virtual = 1
   BEGIN
	exec [Moodle_CrearCurso_Admision] @id
   END
      
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

