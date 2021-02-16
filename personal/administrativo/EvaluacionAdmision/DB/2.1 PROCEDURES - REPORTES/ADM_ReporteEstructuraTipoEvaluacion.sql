
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-31
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ReporteEstructuraTipoEvaluacion 
	@codigo_cco INT,
	@codigo_cpf INT,
	@codigo_tev INT
AS
BEGIN

	DECLARE @row INT = 0;

	CREATE TABLE #Preguntas (id INT,codigo_tei INT, codigo_tev int, codigo_ind int, nro_item int)  
	
	DECLARE @codigo_tei AS INT, @codigo_ind AS INT, @cant_item AS INT
	DECLARE teiAux CURSOR FOR SELECT tei.codigo_tei, tei.codigo_ind, tei.cantidad_preguntas_tei
									FROM dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei WHERE tei.codigo_tev = @codigo_tev AND tei.estado_tei = 1
	OPEN teiAux
	FETCH NEXT FROM teiAux INTO @codigo_tei, @codigo_ind, @cant_item
	WHILE @@fetch_status = 0
	BEGIN
		
		DECLARE @for INT = 0;

		WHILE @for < @cant_item
		BEGIN
			SET @row = @row + 1
			INSERT INTO #Preguntas(id,codigo_tei,codigo_tev,codigo_ind,nro_item)
			VALUES(@row,@codigo_tei,@codigo_tev,@codigo_ind,@for+1)
			SET @for = @for + 1;
		END
		
		FETCH NEXT FROM teiAux INTO @codigo_tei, @codigo_ind, @cant_item
	END
	CLOSE teiAux
	DEALLOCATE teiAux	

	SELECT tev.codigo_tev, tev.nombre_tev, f.codigo_Fac, f.nombre_Fac, cp.codigo_Cpf, cp.nombre_Cpf, cee.codigo_cee, cee.cantidad_cee, 
	cco.codigo_Cco, cco.descripcion_Cco, 0 nro_orden_ceep, 0.0 peso_ceep, cmp.codigo_cmp, cmp.nombre_cmp, com.codigo_com, com.nombre_com,
	scom.codigo_scom, scom.nombre_scom, ind.codigo_ind, ind.nombre_ind, ind.descripcion_ind, 
	tei.cantidad_preguntas_tei, pcom.peso_pcom, (pcom.peso_pcom * tev.peso_basica_tev) niv_base
	, (pcom.peso_pcom * tev.peso_intermedia_tev) niv_intermedia, (pcom.peso_pcom * tev.peso_avanzada_tev) niv_avanzada,
	p.nro_item
	FROM dbo.ADM_TipoEvaluacion(NOLOCK) tev
	INNER JOIN dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee ON tev.codigo_tev = cee.codigo_tev
	INNER JOIN dbo.CentroCostos(NOLOCK) cco ON cee.codigo_cco = cco.codigo_Cco
	INNER JOIN dbo.ADM_DatosEventoAdmision(NOLOCK) dea ON cee.codigo_cco = dea.codigo_cco
	INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON cee.codigo_cpf = cp.codigo_Cpf
	INNER JOIN dbo.Facultad(NOLOCK) f ON cp.codigo_Fac = f.codigo_Fac
	--INNER JOIN dbo.ADM_ConfiguracionEvaluacionEvento_Peso(NOLOCK) ceep ON cee.codigo_cee = ceep.codigo_cee
	INNER JOIN dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei ON tev.codigo_tev = tei.codigo_tev
	INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
	INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
	INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
	INNER JOIN dbo.ADM_Componente_CompetenciaAprendizaje(NOLOCK) cca ON com.codigo_com = cca.codigo_com 
	INNER JOIN dbo.ADM_Componente(NOLOCK) cmp ON cca.codigo_cmp = cmp.codigo_cmp
	INNER JOIN dbo.ADM_PesoCompetencia(NOLOCK) pcom ON com.codigo_com = pcom.codigo_com AND cp.codigo_Cpf = pcom.codigo_cpf AND pcom.codigo_cac = dea.codigo_cac
	INNER JOIN #Preguntas p ON tei.codigo_tei = p.codigo_tei
	WHERE tev.estado_tev = 1 AND cee.estado_cee = 1 --AND ceep.estado_ceep = 1 
	AND tei.estado_tei = 1
	AND ind.estado_ind = 1 AND scom.estado_scom = 1 AND com.estado_com = 1 AND pcom.estado_pcom = 1 AND dea.estado_dea = 1
	AND cca.estado_cca = 1 AND cmp.estado_cmp = 1
	AND cee.codigo_cco = @codigo_cco AND cee.codigo_cpf = @codigo_cpf AND cee.codigo_tev = @codigo_tev
	
	drop table #Preguntas
	
END
GO

GRANT EXECUTE ON ADM_ReporteEstructuraTipoEvaluacion TO usuariogeneral
GRANT EXECUTE ON ADM_ReporteEstructuraTipoEvaluacion TO IusrReporting
GO


