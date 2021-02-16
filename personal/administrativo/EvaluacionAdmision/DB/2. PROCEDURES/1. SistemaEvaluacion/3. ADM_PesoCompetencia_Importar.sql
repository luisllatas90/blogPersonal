
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-28
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_PesoCompetencia_Importar
	@codigo_cac INT,
	@codigo_cac_import INT,
	@codigo_per INT
AS
BEGIN
	BEGIN TRY 
		/****************************************************************************************************************************************************
		* Validar
		****************************************************************************************************************************************************/
		DECLARE @count AS INT
		
		SELECT @count = COUNT(pcom.codigo_pcom) FROM dbo.ADM_PesoCompetencia(NOLOCK) pcom WHERE pcom.codigo_cac = @codigo_cac_import AND pcom.estado_pcom = 1
		
		/****************************************************************************************************************************************************
		* Pesos de Competencias
		****************************************************************************************************************************************************/
		IF @count > 0
		BEGIN
			DECLARE @codigo_cpf AS INT, @codigo_com AS INT, @peso_pcom AS NUMERIC(8,2)
			DECLARE importar CURSOR FOR SELECT pcom.codigo_cpf, pcom.codigo_com, pcom.peso_pcom
											FROM dbo.ADM_PesoCompetencia(NOLOCK) pcom WHERE pcom.codigo_cac = @codigo_cac_import AND pcom.estado_pcom = 1
			OPEN importar
			FETCH NEXT FROM importar INTO @codigo_cpf, @codigo_com, @peso_pcom
			WHILE @@fetch_status = 0
			BEGIN
				INSERT INTO dbo.ADM_PesoCompetencia(codigo_cac,codigo_cpf,codigo_com,peso_pcom,codigo_per_reg,fecha_reg,estado_pcom)
				VALUES(@codigo_cac,@codigo_cpf,@codigo_com,@peso_pcom,@codigo_per,GETDATE(),1)
				FETCH NEXT FROM importar INTO @codigo_cpf, @codigo_com, @peso_pcom
			END
			CLOSE importar
			DEALLOCATE importar
			
			SELECT @codigo_cac id
		END
		ELSE
		BEGIN
			SELECT -1 id
		END
	
	--PRINT ' -- OK DISEÑO CLONADO -- '
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

GRANT EXECUTE ON ADM_PesoCompetencia_Importar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_PesoCompetencia_Importar TO usuariogeneral
GO

