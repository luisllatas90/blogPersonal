
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ConfiguracionEvaluacionEvento_Peso_Listar 
	@tipoOpe varchar(2),
	@codigo_cee int,
	@codigo_ceep int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT ceep.codigo_ceep, ceep.codigo_cee, ceep.nro_orden_ceep, ceep.peso_ceep, ceep.estado_ceep 
		FROM dbo.ADM_ConfiguracionEvaluacionEvento_Peso(NOLOCK) ceep
		WHERE ceep.estado_ceep = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		SELECT ceep.codigo_ceep, ceep.codigo_cee, ceep.nro_orden_ceep, ceep.peso_ceep, ceep.estado_ceep, 
		'Evaluación N° ' + CONVERT(VARCHAR(3),ceep.nro_orden_ceep) descripcion_ceep
		FROM dbo.ADM_ConfiguracionEvaluacionEvento_Peso(NOLOCK) ceep
		WHERE ceep.estado_ceep = 1 and ceep.codigo_cee = @codigo_cee
		ORDER BY nro_orden_ceep
	END
	ELSE
	IF @tipoOpe = '2'
	BEGIN
		SELECT -1 codigo_ceep, -1 codigo_cee, 1 nro_orden_ceep, 1.0 peso_ceep, 1 estado_ceep, 'Evaluación N° 1'  descripcion_ceep
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

GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Peso_Listar TO usuariogeneral
GO