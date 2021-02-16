
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ConfiguracionEvaluacionEvento_Listar 
	@tipoOpe varchar(2),
	@codigo_cco int,
	@codigo_cpf int,
	@codigo_tev int,
	@codigo_cee int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		--SELECT -1 codigo_tev, '' nombre_tev 
		SELECT cee.codigo_cee, cee.codigo_cco, cee.codigo_cpf, cee.codigo_tev, cee.cantidad_cee , cee.estado_cee 
		FROM dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee
		WHERE cee.estado_cee = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		SELECT cee.codigo_cee, cee.codigo_cco, cee.codigo_cpf, cee.codigo_tev, cee.cantidad_cee , cee.estado_cee 
		FROM dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee
		WHERE cee.estado_cee = 1 and cee.codigo_cee = @codigo_cee
	END
	ELSE
	IF @tipoOpe = '2'
	BEGIN
		SELECT cee.codigo_cee, cee.codigo_cco, cee.codigo_cpf, cee.codigo_tev, cee.cantidad_cee , cee.estado_cee, 
		cco.descripcion_Cco, cpf.nombre_Cpf, tev.nombre_tev, dbo.ADM_ConfigEvalEventoPeso(cee.codigo_cee) total_peso 
		FROM dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON cee.codigo_cco = cco.codigo_Cco 
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cpf ON cee.codigo_cpf = cpf.codigo_Cpf
		INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON cee.codigo_tev = tev.codigo_tev
		WHERE cee.estado_cee = 1 AND cee.codigo_cco  = @codigo_cco 
		ORDER BY cee.codigo_cco, cee.codigo_tev, cpf.nombre_Cpf
	END
	ELSE
	IF @tipoOpe = '3'
	BEGIN
		SELECT cpf.codigo_Cpf, cpf.nombre_Cpf, cpf.nombreOficial_cpf 
		FROM dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cpf ON cee.codigo_cpf = cpf.codigo_Cpf
		WHERE cee.estado_cee = 1 AND cee.codigo_cco = @codigo_cco
		ORDER BY cpf.nombre_Cpf
	END
	ELSE
	IF @tipoOpe = '4'
	BEGIN
		SELECT tev.codigo_tev, tev.nombre_tev 
		FROM dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cpf ON cee.codigo_cpf = cpf.codigo_Cpf
		INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON cee.codigo_tev = tev.codigo_tev
		WHERE cee.estado_cee = 1 AND tev.estado_tev = 1 
		AND cee.codigo_cco = @codigo_cco AND cee.codigo_cpf = @codigo_cpf
	END
END
GO

GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Listar TO usuariogeneral
GRANT EXECUTE ON ADM_ConfiguracionEvaluacionEvento_Listar TO IusrReporting
GO