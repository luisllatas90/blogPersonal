-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-24	ENevado			Adicionar un campo para virtual
-- ==================================================================
CREATE PROCEDURE ADM_Evaluacion_Listar 
	@tipoOpe varchar(2),
	@codigo_evl int,
	@codigo_cco int,
	@codigo_tev int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT evl.codigo_evl, evl.codigo_cco, evl.codigo_tev, evl.nombre_evl, evl.estadovalidacion_evl, evl.estado_evl,
		ISNULL(evl.virtual_evl, 0) virtual_evl -- 001 
		FROM dbo.ADM_Evaluacion(NOLOCK) evl
		WHERE evl.estado_evl = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		SELECT evl.codigo_evl, evl.codigo_cco, evl.codigo_tev, evl.nombre_evl, evl.estadovalidacion_evl, evl.estado_evl,
		cco.descripcion_Cco, tev.nombre_tev, ISNULL(evl.idArchivoCompartido, -1)  idArchivoCompartido,
		ISNULL(evl.virtual_evl, 0) virtual_evl -- 001  
		FROM dbo.ADM_Evaluacion(NOLOCK) evl
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON evl.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON evl.codigo_tev = tev.codigo_tev
		WHERE evl.estado_evl = 1 AND tev.estado_tev = 1
		AND evl.codigo_cco = @codigo_cco
		ORDER BY evl.codigo_cco, evl.codigo_tev
	END
	ELSE
	IF @tipoOpe = '2'
	BEGIN
		SELECT evl.codigo_evl, evl.codigo_cco, evl.codigo_tev, evl.nombre_evl, evl.estadovalidacion_evl, evl.estado_evl,
		cco.descripcion_Cco, tev.nombre_tev, Tab.tipo_prv, Tab.cant_total, Tab.cant_pendiente, Tab.cant_conforme, Tab.cant_observada,
		(CASE Tab.tipo_prv WHEN 'U' THEN 'ÚNICA' WHEN 'A' THEN 'AGRUPADA' ELSE '' END) tipo_prv_descripcion,
		ISNULL(evl.virtual_evl, 0) virtual_evl -- 001  
		FROM dbo.ADM_Evaluacion(NOLOCK) evl
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON evl.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON evl.codigo_tev = tev.codigo_tev
		LEFT JOIN (SELECT evd.codigo_evl, prv.tipo_prv, COUNT(evd.codigo_evd) cant_total, 
					SUM(CASE evd.estadovalidacion_evd WHEN 'P' THEN 1 ELSE 0 END) cant_pendiente,
					SUM(CASE evd.estadovalidacion_evd WHEN 'C' THEN 1 ELSE 0 END) cant_conforme,
					SUM(CASE evd.estadovalidacion_evd WHEN 'O' THEN 1 ELSE 0 END) cant_observada
					FROM dbo.ADM_EvaluacionDetalle(NOLOCK) evd
					INNER JOIN dbo.ADM_PreguntaEvaluacion(NOLOCK) prv ON evd.codigo_prv = prv.codigo_prv
					WHERE evd.estado_evd = 1
					GROUP BY evd.codigo_evl, prv.tipo_prv) AS Tab ON evl.codigo_evl = Tab.codigo_evl
		WHERE evl.estado_evl = 1 AND tev.estado_tev = 1
		AND evl.codigo_cco = @codigo_cco
	END
	ELSE
	IF @tipoOpe = '3'
	BEGIN
		SELECT evl.codigo_evl, evl.codigo_cco, evl.codigo_tev, evl.nombre_evl, evl.estadovalidacion_evl, evl.estado_evl,
		cco.descripcion_Cco, tev.nombre_tev, ISNULL(ac.IdArchivosCompartidos,-1) idArchivoCompartido, ISNULL(ac.NombreArchivo,'') NombreArchivo,
		ISNULL(evl.virtual_evl, 0) virtual_evl -- 001 
		FROM dbo.ADM_Evaluacion(NOLOCK) evl
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON evl.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON evl.codigo_tev = tev.codigo_tev
		LEFT JOIN dbo.ArchivoCompartido(NOLOCK) ac ON evl.idArchivoCompartido = ac.IdArchivosCompartidos
		--LEFT JOIN (SELECT elu.codigo_evl, COUNT(elu.codigo_elu) cantidad 
		--			FROM dbo.ADM_Evaluacion_Alumno(NOLOCK) elu
		--			WHERE elu.estado_elu = 1
		--			GROUP BY elu.codigo_evl) AS Tab ON evl.codigo_evl = Tab.codigo_evl
		WHERE evl.estado_evl = 1 AND tev.estado_tev = 1
		AND evl.codigo_cco = @codigo_cco
	END
	ELSE
	IF @tipoOpe = '4'
	BEGIN
		SELECT evl.codigo_evl, evl.codigo_cco, evl.codigo_tev, evl.nombre_evl, evl.estadovalidacion_evl, evl.estado_evl,
		cco.descripcion_Cco, tev.nombre_tev, Tab.cant_total, Tab.cant_pendiente, Tab.cant_conforme, Tab.cant_observada,
		ISNULL(evl.virtual_evl, 0) virtual_evl -- 001  
		FROM dbo.ADM_Evaluacion(NOLOCK) evl
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON evl.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.ADM_TipoEvaluacion(NOLOCK) tev ON evl.codigo_tev = tev.codigo_tev
		LEFT JOIN (SELECT elu.codigo_evl, COUNT(elu.codigo_elu) cant_total, 
					SUM(CASE elu.estadoverificacion_elu WHEN 'P' THEN 1 ELSE 0 END) cant_pendiente,
					SUM(CASE elu.estadoverificacion_elu WHEN 'C' THEN 1 ELSE 0 END) cant_conforme,
					SUM(CASE elu.estadoverificacion_elu WHEN 'O' THEN 1 ELSE 0 END) cant_observada
					FROM dbo.ADM_Evaluacion_Alumno(NOLOCK) elu
					WHERE elu.estado_elu = 1
					GROUP BY elu.codigo_evl) AS Tab ON evl.codigo_evl = Tab.codigo_evl
		WHERE evl.estado_evl = 1 AND tev.estado_tev = 1
		AND evl.codigo_cco = @codigo_cco
	END
	IF @tipoOpe = '5' --Por cco para reportes
	BEGIN
	SELECT codigo_evl, nombre_evl
	FROM (SELECT evl.codigo_evl, evl.nombre_evl
		FROM dbo.ADM_Evaluacion (NOLOCK) evl
		WHERE evl.estado_evl = 1
			AND evl.codigo_cco = @codigo_cco
		UNION
		SELECT 0 AS codigo_evl, 'TODOS' AS nombre_evl) dt
	ORDER BY CASE WHEN codigo_evl = 0 THEN codigo_evl ELSE 1 END, nombre_evl
	END
END
GO

GRANT EXECUTE ON ADM_Evaluacion_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Evaluacion_Listar TO usuariogeneral
GO