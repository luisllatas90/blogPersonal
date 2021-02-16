
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-23	ENevado			Adicionar un campo para virtual
-- ==================================================================
CREATE PROCEDURE ADM_TipoEvaluacion_Listar 
	@tipoOpe varchar(2),
	@codigo_tev int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT tev.codigo_tev, tev.nombre_tev, tev.peso_basica_tev, tev.peso_intermedia_tev, tev.peso_avanzada_tev, tev.estado_tev,
		ISNULL(tev.virtual_tev, 0) virtual_tev -- 001
		FROM dbo.ADM_TipoEvaluacion(NOLOCK) tev
		WHERE tev.estado_tev = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		SELECT tev.codigo_tev, tev.nombre_tev, tev.peso_basica_tev, tev.peso_intermedia_tev, tev.peso_avanzada_tev, tev.estado_tev,
		ISNULL(tev.virtual_tev, 0) virtual_tev   -- 001
		FROM dbo.ADM_TipoEvaluacion(NOLOCK) tev
		WHERE tev.codigo_tev = @codigo_tev
	END
	ELSE
	IF @tipoOpe = '2'
	BEGIN
		SELECT tei.codigo_tev, scom.codigo_com, SUM(tei.cantidad_preguntas_tei) total
		FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei
		INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind 
		INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
		WHERE tei.estado_tei  = 1 AND ind.estado_ind = 1 AND scom.estado_scom = 1
		GROUP BY tei.codigo_tev, scom.codigo_com
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
END
GO

GRANT EXECUTE ON ADM_TipoEvaluacion_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_TipoEvaluacion_Listar TO usuariogeneral
GO