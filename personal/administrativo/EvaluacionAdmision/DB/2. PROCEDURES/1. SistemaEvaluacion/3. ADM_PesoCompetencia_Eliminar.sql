-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_PesoCompetencia_Eliminar
	@codigo_pcom INT,
	@aplicar_facultad bit,
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
	
		UPDATE dbo.ADM_PesoCompetencia
		SET estado_pcom = 0,
		codigo_per_act = @codigo_per,
		fecha_act = GETDATE()
		WHERE codigo_pcom = @codigo_pcom
		
		IF @aplicar_facultad = 1
		BEGIN
			
			DECLARE @codigo_fac INT = 0, @codigo_cpf INT = 0, @codigo_cac INT = 0, @codigo_com INT = 0
			SELECT @codigo_cpf = pc.codigo_cpf, @codigo_cac = pc.codigo_cac, @codigo_com = pc.codigo_com
			FROM dbo.ADM_PesoCompetencia(NOLOCK) pc WHERE pc.codigo_pcom = @codigo_pcom
			SELECT @codigo_fac = cp.codigo_fac FROM dbo.CarreraProfesional(NOLOCK) cp WHERE cp.codigo_Cpf = @codigo_cpf
			
			DECLARE @codigo_cpf_aux AS INT
			DECLARE ProgEst CURSOR FOR SELECT cp.codigo_cpf FROM dbo.CarreraProfesional(NOLOCK) cp 
											WHERE cp.codigo_fac = @codigo_fac AND cp.codigo_cpf <> @codigo_cpf and cp.vigencia_Cpf = 1 and cp.eliminado_cpf = 0
			OPEN ProgEst
			FETCH NEXT FROM ProgEst INTO @codigo_cpf_aux
			WHILE @@fetch_status = 0
			BEGIN
			
				DECLARE @count INT = 0
				SELECT @count = COUNT(pc.codigo_pcom) FROM dbo.ADM_PesoCompetencia(NOLOCK) pc 
				WHERE pc.codigo_cpf = @codigo_cpf_aux AND pc.codigo_com = @codigo_com AND pc.codigo_cac = @codigo_cac AND pc.estado_pcom = 1 
			
				IF @count > 0
				BEGIN
					DECLARE @codigo_pcom_aux INT = 0
					SELECT @codigo_pcom_aux = pc.codigo_pcom FROM dbo.ADM_PesoCompetencia(NOLOCK) pc 
					WHERE pc.codigo_cpf = @codigo_cpf_aux AND pc.codigo_com = @codigo_com AND pc.codigo_cac = @codigo_cac AND pc.estado_pcom = 1 
					UPDATE dbo.ADM_PesoCompetencia
					SET estado_pcom = 0,
					codigo_per_act = @codigo_per,
					fecha_act = GETDATE()
					WHERE codigo_pcom = @codigo_pcom_aux
				END
				
				FETCH NEXT FROM ProgEst INTO @codigo_cpf_aux
			END
			CLOSE ProgEst
			DEALLOCATE ProgEst 
			
			
		END
		
		SELECT @codigo_pcom  id
		
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

GRANT EXECUTE ON ADM_PesoCompetencia_Eliminar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_PesoCompetencia_Eliminar TO usuariogeneral
GO