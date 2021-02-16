
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_TipoEvaluacion_Indicador_Listar 
	@tipoOpe varchar(2),
	@codigo_tev int,
	@codigo_ind int,
	@codigo_tei int
AS
BEGIN
	-- Listar Tipo de Evaluacion Indicador
	IF @tipoOpe = ''
	BEGIN
		SELECT tei.codigo_tei, tei.codigo_tev, tei.codigo_ind, tei.cantidad_preguntas_tei, tei.estado_tei
		FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei
		WHERE tei.estado_tei = 1
	END
	ELSE
	-- Listar Tipo de Evaluacion Indicador por Tipo Evaluacion para Asignacion de preguntas
	IF @tipoOpe = '1'
	BEGIN
	
		DECLARE @row INT = 0;

		CREATE TABLE #Preguntas (id INT,codigo_tei INT, codigo_tev int, codigo_ind int, nro_item int)  
		
		DECLARE @codigo_tei_aux AS INT, @codigo_ind_aux AS INT, @cant_item AS INT
		DECLARE teiAux CURSOR FOR SELECT tei.codigo_tei, tei.codigo_ind, tei.cantidad_preguntas_tei
										FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei 
										INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
										INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
										INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
										WHERE tei.codigo_tev = @codigo_tev AND tei.estado_tei = 1 
										AND ind.estado_ind = 1 AND scom.estado_scom = 1 AND com.estado_com = 1
										ORDER BY com.codigo_com, ind.nombre_ind
		OPEN teiAux
		FETCH NEXT FROM teiAux INTO @codigo_tei_aux, @codigo_ind_aux, @cant_item
		WHILE @@fetch_status = 0
		BEGIN
			
			DECLARE @for INT = 0;

			WHILE @for < @cant_item
			BEGIN
				SET @row = @row + 1
				INSERT INTO #Preguntas(id,codigo_tei,codigo_tev,codigo_ind,nro_item)
				VALUES(@row,@codigo_tei_aux,@codigo_tev,@codigo_ind_aux,@for+1)
				SET @for = @for + 1;
			END
			
			FETCH NEXT FROM teiAux INTO @codigo_tei_aux, @codigo_ind_aux, @cant_item
		END
		CLOSE teiAux
		DEALLOCATE teiAux	
	
		SELECT tei.codigo_tei, tei.codigo_tev, tei.codigo_ind, tei.cantidad_preguntas_tei, tei.estado_tei, 
		ind.nombre_ind, ind.descripcion_ind, ind.codigo_scom, scom.nombre_scom, scom.codigo_com, com.nombre_com, com.nombre_corto_com,
		p.id nro_item
		FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei
		INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
		INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
		INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
		INNER JOIN #Preguntas p ON tei.codigo_tei = p.codigo_tei
		WHERE tei.estado_tei = 1 AND ind.estado_ind = 1 AND scom.estado_scom = 1 AND com.estado_com = 1
		AND tei.codigo_tev = @codigo_tev
		ORDER BY com.codigo_com
		
		drop table #Preguntas
		
	END
	--ELSE
	--IF @tipoOpe = '2'
	--BEGIN
	--	SELECT pcom.codigo_pcom, pcom.codigo_cac, pcom.codigo_cpf, pcom.codigo_com, pcom.peso_pcom, pcom.estado_pcom
	--	FROM dbo.ADM_PesoCompetencia(NOLOCK) pcom
	--	WHERE pcom.estado_pcom = 1 AND pcom.codigo_cac = @codigo_cac
	--END
	ELSE
	-- Listar Tipo de Evaluacion Indicador por Tipo Evaluacion
	IF @tipoOpe = '3'
	BEGIN
		SELECT ISNULL(tei.codigo_tei, -1) codigo_tei, ISNULL(tei.codigo_tev, @codigo_tev) codigo_tev, ISNULL(tei.codigo_ind,ind.codigo_ind) codigo_ind, 
		ISNULL(tei.cantidad_preguntas_tei, 0) cantidad_preguntas_tei, tei.estado_tei, ind.nombre_ind, scom.nombre_scom, com.nombre_com 
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com
		INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON com.codigo_com = scom.codigo_com
		INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON scom.codigo_scom = ind.codigo_scom 
		LEFT JOIN dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei ON ind.codigo_ind = tei.codigo_ind AND tei.codigo_tev = @codigo_tev AND tei.estado_tei = 1
		WHERE com.admision_com = 1 AND com.estado_com = 1 AND scom.estado_scom = 1 AND ind.estado_ind = 1
		ORDER BY com.codigo_com, scom.codigo_scom
	END
END
GO

GRANT EXECUTE ON ADM_TipoEvaluacion_Indicador_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_TipoEvaluacion_Indicador_Listar TO usuariogeneral
GO