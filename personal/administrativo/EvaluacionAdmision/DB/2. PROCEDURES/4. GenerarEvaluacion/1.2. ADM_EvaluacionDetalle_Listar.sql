-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- ==============================================================================================
-- HISTORIAL DE CAMBIOS
-- ==============================================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-30	ENevado			Acutalizar listado de evaluacion detalle por indicador
-- 002		2020-12-01	ENevado			Obtener tipo evaluacion virtual
-- ==============================================================================================
CREATE PROCEDURE ADM_EvaluacionDetalle_Listar
	@tipoOpe varchar(2),
	@codigo_evd int,
	@codigo_evl int,
	@codigo_prv int,
	@orden_evd int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT evd.codigo_evd, evd.codigo_evl, evd.codigo_prv, evd.orden_evd, evd.estado_evd
		FROM dbo.ADM_EvaluacionDetalle(NOLOCK) evd
		WHERE evd.estado_evd = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		DECLARE @row INT = 0;

		CREATE TABLE #Preguntas (id INT,codigo_tei INT, codigo_tev int, codigo_ind int, nro_item int, virtual_prv bit) -- 002	

		DECLARE @codigo_tei_aux AS INT, @codigo_ind_aux AS INT, @cant_item AS INT, @virtual_prv AS BIT -- 002	
		DECLARE teiAux CURSOR FOR SELECT tei.codigo_tei, tei.codigo_ind, tei.cantidad_preguntas_tei, ISNULL(tev.virtual_tev,0) virtual_tev -- 002	
										FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei
										INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON tei.codigo_tev = tev.codigo_tev
										INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
										INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
										INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
										WHERE tei.codigo_tev = @orden_evd AND tei.estado_tei = 1
										AND ind.estado_ind = 1 AND scom.estado_scom = 1 AND com.estado_com = 1
										ORDER BY com.codigo_com, ind.nombre_ind
		OPEN teiAux
		FETCH NEXT FROM teiAux INTO @codigo_tei_aux, @codigo_ind_aux, @cant_item, @virtual_prv
		WHILE @@fetch_status = 0
		BEGIN

			DECLARE @for INT = 0;

			WHILE @for < @cant_item
			BEGIN
				SET @row = @row + 1
				INSERT INTO #Preguntas(id,codigo_tei,codigo_tev,codigo_ind,nro_item,virtual_prv)
				VALUES(@row,@codigo_tei_aux,@orden_evd,@codigo_ind_aux,@for+1,@virtual_prv)
				SET @for = @for + 1;
			END

			FETCH NEXT FROM teiAux INTO @codigo_tei_aux, @codigo_ind_aux, @cant_item, @virtual_prv
		END
		CLOSE teiAux
		DEALLOCATE teiAux

		SELECT tei.codigo_tei, tei.codigo_tev, tei.codigo_ind, tei.cantidad_preguntas_tei, tei.estado_tei, ind.nombre_ind,
		ind.descripcion_ind, ind.codigo_scom, scom.nombre_scom, scom.codigo_com, com.nombre_com, com.nombre_corto_com, p.id nro_item,
		ISNULL(Tab.codigo_prv, -1) codigo_prv, ISNULL(Tab.identificador_prv,'') identificador_prv, ISNULL(Tab.texto_prv, '') texto_prv,
		ISNULL(Tab.codigo_evd, -1) codigo_evd, ISNULL(Tab.codigo_evl, -1) codigo_evl, ISNULL(Tab.nombre_ncp, '') nombre_ncp,
		ISNULL(Tab.codigo_ale, -1) codigo_ale, ISNULL(Tab.orden_ale, -1) orden_ale, ISNULL(Tab.texto_ale, '') texto_ale,
		ISNULL(p.virtual_prv,0) virtual_prv
		FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei
		INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
		INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
		INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
		INNER JOIN #Preguntas p ON tei.codigo_tei = p.codigo_tei
		LEFT JOIN (SELECT evd.codigo_evd, evd.codigo_evl, prv.codigo_prv, 
				ISNULL(prv.codigo_ind,evd.codigo_ind) codigo_ind, -- 002
				prv.identificador_prv, prv.texto_prv,
				evd.orden_evd, ncp.nombre_ncp, ae.codigo_ale, ae.orden_ale, ae.texto_ale
				FROM dbo.ADM_EvaluacionDetalle(NOLOCK) evd
				LEFT JOIN dbo.ADM_PreguntaEvaluacion(NOLOCK) prv ON prv.codigo_prv = evd.codigo_prv AND prv.estado_prv = 1
				LEFT JOIN dbo.ADM_NivelComplejidadPregunta(NOLOCK) ncp ON evd.codigo_ncp = ncp.codigo_ncp -- 002
				LEFT JOIN dbo.ADM_AlternativaEvaluacion(NOLOCK) ae ON prv.codigo_prv = ae.codigo_prv AND ae.correcta_ale = 1 AND ae.estado_ale = 1
				WHERE evd.estado_evd = 1  AND evd.codigo_evl = @codigo_evl) AS Tab
				ON p.id = Tab.orden_evd AND p.codigo_ind = Tab.codigo_ind
		WHERE tei.estado_tei = 1 AND ind.estado_ind = 1 AND scom.estado_scom = 1 AND com.estado_com = 1
		AND tei.codigo_tev = @orden_evd
		ORDER BY com.codigo_com

		drop table #Preguntas

	END
	ELSE
	IF @tipoOpe = '2'
	BEGIN
		SELECT evd.codigo_evd, evd.codigo_evl, evd.codigo_prv, evd.orden_evd, evd.estado_evd, evd.estadovalidacion_evd,
		ISNULL(edo.codigo_edo, -1) codigo_edo, ISNULL(edo.descripcion_edo, '') descripcion_edo
		FROM dbo.ADM_EvaluacionDetalle(NOLOCK) evd
		LEFT JOIN dbo.ADM_EvaluacionDetalle_Observacion(NOLOCK) edo ON evd.codigo_evd = edo.codigo_evd AND edo.estado_edo = 1
		WHERE evd.estado_evd = 1
		AND evd.codigo_evl = @codigo_evl
	END
	--ELSE
	--IF @tipoOpe = '3'
	--BEGIN
	--	SELECT ISNULL(pcom.codigo_pcom, -1) codigo_pcom, pcom.codigo_cac, ISNULL(pcom.codigo_cpf, @codigo_cpf) codigo_cpf,
	--	ISNULL(pcom.codigo_com,com.codigo_com) codigo_com, ISNULL(pcom.peso_pcom, 0) peso_pcom, pcom.estado_pcom, com.nombre_com
	--	FROM dbo.CompetenciaAprendizaje(NOLOCK) com
	--	LEFT JOIN dbo.ADM_PesoCompetencia(NOLOCK) pcom ON com.codigo_com = pcom.codigo_com AND pcom.codigo_cac = @codigo_cac AND pcom.codigo_cpf = @codigo_cpf AND pcom.estado_pcom = 1
	--	WHERE com.admision_com = 1 AND com.estado_com = 1
	--END
	ELSE
    IF @tipoOpe = '4'
        BEGIN
            SELECT
                evd.codigo_evd
              , evd.codigo_evl
              , evd.codigo_prv
              , evd.orden_evd
              , evd.estado_evd
              , evd.estadovalidacion_evd
              , ind.codigo_ind
              , scom.codigo_scom
              , scom.codigo_com
            FROM dbo.ADM_EvaluacionDetalle (NOLOCK) evd
                 --JOIN ADM_PreguntaEvaluacion prv WITH (NOLOCK) ON evd.codigo_prv = prv.codigo_prv 
                 JOIN ADM_Indicador ind WITH (NOLOCK) ON evd.codigo_ind = ind.codigo_ind -- 001
                 JOIN ADM_SubCompetencia scom WITH (NOLOCK) ON ind.codigo_scom = scom.codigo_scom
            WHERE evd.estado_evd = 1
              AND evd.codigo_evl = @codigo_evl
        END
END
GO

GRANT EXECUTE ON ADM_EvaluacionDetalle_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_EvaluacionDetalle_Listar TO usuariogeneral
GO