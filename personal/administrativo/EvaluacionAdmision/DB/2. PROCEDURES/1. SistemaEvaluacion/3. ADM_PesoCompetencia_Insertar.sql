-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_PesoCompetencia_Insertar 
	@codigo_cac int,
	@codigo_cpf int,
	@codigo_com int,
	@peso_pcom numeric(8,2),
	@aplicar_facultad bit,
	@codigo_per int
AS
BEGIN
	BEGIN TRY 
	
		DECLARE @id int = 0
		INSERT INTO dbo.ADM_PesoCompetencia(codigo_cac,codigo_cpf,codigo_com,peso_pcom,codigo_per_reg,fecha_reg,estado_pcom)
		VALUES(@codigo_cac,@codigo_cpf,@codigo_com,@peso_pcom,@codigo_per,GETDATE(),1)
		SET @id = SCOPE_IDENTITY() 
		
		IF @aplicar_facultad = 1
		BEGIN
		
			DECLARE @codigo_fac INT = 0
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
					EXEC ADM_PesoCompetencia_Actualizar @codigo_pcom_aux, @peso_pcom, 0, @codigo_per
				END
				ELSE
				BEGIN
					INSERT INTO dbo.ADM_PesoCompetencia(codigo_cac,codigo_cpf,codigo_com,peso_pcom,codigo_per_reg,fecha_reg,estado_pcom)
					VALUES(@codigo_cac,@codigo_cpf_aux,@codigo_com,@peso_pcom,@codigo_per,GETDATE(),1)
				END
				
				
				FETCH NEXT FROM ProgEst INTO @codigo_cpf_aux
			END
			CLOSE ProgEst
			DEALLOCATE ProgEst
			
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

GRANT EXECUTE ON ADM_PesoCompetencia_Insertar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_PesoCompetencia_Insertar TO usuariogeneral
GO