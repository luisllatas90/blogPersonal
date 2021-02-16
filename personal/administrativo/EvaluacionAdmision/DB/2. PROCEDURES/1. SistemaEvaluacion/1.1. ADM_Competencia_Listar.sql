-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Competencia_Listar
	@tipoOpe varchar(2),
	@codigo_cmp int,
	@codigo_com int
AS
BEGIN
	-- Listar Competencias de Aprendizaje solo para Admisión (Generales / De ingreso)
	IF @tipoOpe = ''
	BEGIN
		SELECT com.codigo_com, com.nombre_com, isnull(com.nombre_corto_com,'') nombre_corto_com
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com
		WHERE com.codigo_tcom = 1 AND com.codigo_cat = 1 AND com.estado_com = 1
	END
	ELSE
	-- Listar Competencias de Aprendizaje por Componente y Competencia
	IF @tipoOpe = '1'
	BEGIN
		SELECT cca.codigo_cca, cmp.codigo_cmp, cmp.nombre_cmp, com.codigo_com, com.nombre_com, isnull(com.nombre_corto_com,'') nombre_corto_com
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com
		INNER JOIN dbo.ADM_Componente_CompetenciaAprendizaje(NOLOCK) cca ON com.codigo_com = cca.codigo_com
		INNER JOIN dbo.ADM_Componente(NOLOCK) cmp ON cca.codigo_cmp = cmp.codigo_cmp
		WHERE cca.estado_cca = 1 AND cmp.estado_cmp = 1
		AND cmp.codigo_cmp = @codigo_cmp AND com.codigo_com = @codigo_com AND com.estado_com = 1
	END
	ELSE
	-- Listar Competencias de Aprendizaje por Componente
	IF @tipoOpe = '2'
	BEGIN
		SELECT cca.codigo_cca, cmp.codigo_cmp, cmp.nombre_cmp, com.codigo_com, com.nombre_com, isnull(com.nombre_corto_com,'') nombre_corto_com
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com
		INNER JOIN dbo.ADM_Componente_CompetenciaAprendizaje(NOLOCK) cca ON com.codigo_com = cca.codigo_com
		INNER JOIN dbo.ADM_Componente(NOLOCK) cmp ON cca.codigo_cmp = cmp.codigo_cmp
		WHERE cca.estado_cca = 1 AND cmp.estado_cmp = 1 AND cmp.codigo_cmp = @codigo_cmp AND com.estado_com = 1
	END
	ELSE
	-- Obtener Competencia de Aprendizaje por Codigo de Competencia
	IF @tipoOpe = '3'
	BEGIN
		SELECT com.codigo_com, com.nombre_com, isnull(com.nombre_corto_com,'') nombre_corto_com
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com
		WHERE com.codigo_com = @codigo_com
	END
	ELSE
	-- Listar Competencias de Aprendizaje por Tipo de Evaluación
	IF @tipoOpe = '4'
	BEGIN
		SELECT DISTINCT com.codigo_com, com.nombre_com, isnull(com.nombre_corto_com,'') nombre_corto_com
		FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei
		INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
		INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
		INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
		WHERE com.codigo_tcom = 1 AND com.codigo_cat = 1 AND  tei.estado_tei = 1 AND ind.estado_ind = 1
		  AND scom.estado_scom = 1 AND com.estado_com = 1 AND tei.codigo_tev = @codigo_cmp
	END
END
GO

GRANT EXECUTE ON ADM_Competencia_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Competencia_Listar TO usuariogeneral
GO