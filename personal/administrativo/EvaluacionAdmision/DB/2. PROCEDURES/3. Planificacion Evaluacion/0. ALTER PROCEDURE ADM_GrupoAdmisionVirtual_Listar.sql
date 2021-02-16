-- ================================================================================
-- Author:		ENevado
-- Create date: 2020-05-11
-- Description:	
-- =================================================================================================================
--    CODIGO  FECHA			DESARROLLADOR   DESCRIPCIÓN                   
-- =================================================================================================================
--    001     2020-08-06	ENevado			Filtrar por tipo de estudio: (1) Admision; (5) PostGrado
--    002     2020-08-06	ENevado			Optimizacion de consultas: quitar uso de token (case) en los filtros 
--    003     2020-09-03	andy.diaz       Agrego las columnas fechaHoraInicio y fechaHoraFin
--    004     2020-09-03	andy.diaz       Agrego las columnas fechaHoraInicio y fechaHoraFin
-- ==================================================================================================================
ALTER PROCEDURE ADM_GrupoAdmisionVirtual_Listar 
	@tipoOperacion VARCHAR(2) = '',
	@codigo_gru INT = -1,
	@codigo_cco VARCHAR(MAX) = '',
	@codigo_tge INT = -1
AS
BEGIN
	-- Listar Grupos de Admisión por Centro de Costos
	IF @tipoOperacion = ''
	BEGIN
		-- Obtener Centros de Costos para filtros -------------------------------------------------------------------------------------
		DECLARE @rs_codigos_cco AS TABLE(codigo_cco INT);  
        INSERT INTO @rs_codigos_cco (codigo_cco) (SELECT * from dbo.fnSplit(@codigo_cco, ','));  
        -- Setear Codigo de Grupo para listar -----------------------------------------------------------------------------------------
        IF @codigo_gru = -1 --> 002 
        BEGIN
			SET @codigo_gru = 0
		END
        -- Ejecutar Consulta Principal -------------------------------------------------------------------------------------------------
		SELECT gav.codigo_gru, gav.codigo_cco, gav.codigo, gav.nombre, gav.aula_activa, gav.estado, gav.idcourse, gav.idcourserole, gav.idcoursecontext,
		gav.codigo_per_reg, gav.fecha_reg, gav.codigo_per_act, gav.fecha_act, '' centrocosto, ISNULL(te.total, 0) cant_estudiante,
		gav.codigo_amb, amb.descripcionReal_Amb ambiente, ISNULL(amb.virtual_amb, 0) virtual_amb, ISNULL(gav.capacidad, 0) capacidad,
		(CONVERT(VARCHAR,ISNULL(te.total, 0)) + ' / ' + CONVERT(VARCHAR,ISNULL(gav.capacidad, 0))) cap_dis,
         fechaHoraInicio_gru,  fechaHoraFin_gru, --003
		(tam.descripcion_Tam + ' - ' + amb.descripcionReal_Amb) nombre_amb,
		ISNULL(gav.codigo_tge,-1) codigo_tge, ISNULL(tge.nombre_tge,'') nombre_tge --> 004
		FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav 
		INNER JOIN (SELECT gcc.codigo_gru, COUNT(gcc.codigo_cco) total_cco FROM dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc
					INNER JOIN @rs_codigos_cco cco ON gcc.codigo_cco  = cco.codigo_cco 
					WHERE gcc.estado_gcc = 1 GROUP BY gcc.codigo_gru
					HAVING COUNT(gcc.codigo_cco) = (SELECT COUNT(*) FROM @rs_codigos_cco)) AS cc ON gav.codigo_gru = cc.codigo_gru
		INNER JOIN dbo.Ambiente(NOLOCK) amb ON gav.codigo_amb = amb.codigo_Amb 
		INNER JOIN dbo.TipoAmbiente(NOLOCK) tam ON amb.codigo_Tam = tam.codigo_Tam
		LEFT JOIN (SELECT ga.codigo_gru, COUNT(ga.codigo_gru) total FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga 
		WHERE ga.estado <> 0 GROUP BY ga.codigo_gru) AS te ON gav.codigo_gru = te.codigo_gru
		LEFT JOIN dbo.ADM_TipoGrupoEvaluacion(NOLOCK) tge ON gav.codigo_tge = tge.codigo_tge --> 004
		WHERE --gav.codigo_gru = (CASE @codigo_gru WHEN -1 THEN gav.codigo_gru ELSE @codigo_gru END) --> 002 
		(gav.codigo_gru = @codigo_gru OR (gav.codigo_gru = gav.codigo_gru - @codigo_gru)) --> 002 
		AND (gav.codigo_tge = @codigo_tge OR (gav.codigo_tge = gav.codigo_tge - @codigo_tge)) --> 004
		AND gav.estado <> 0
	END
	ELSE
	-- Listar Grupos de Admisión por Tipo de Estudio [1,5] ----------------------------------------------------------------------------
	IF @tipoOperacion = 'LT'
	BEGIN
		SELECT DISTINCT gav.codigo_gru, ('[' + gav.codigo + '] - ' + gav.nombre + ' (' + ISNULL(tge.nombre_tge,'') + ')') descripcion_gru 
		FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav
		INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON gav.codigo_gru = gcc.codigo_gru
		INNER JOIN dbo.CentroCostos(NOLOCK) cc on gcc.codigo_cco = cc.codigo_Cco  --> 001 
		INNER JOIN dbo.DatosEvento(NOLOCK) de on cc.codigo_Cco = de.codigo_cco  --> 001 
		LEFT JOIN dbo.ADM_TipoGrupoEvaluacion(NOLOCK) tge ON gav.codigo_tge = tge.codigo_tge --> 004
		WHERE gav.estado <> 0 AND gcc.estado_gcc = 1 
		AND de.codigo_test = CONVERT(int,@codigo_cco) --> 001 
	END
	ELSE
	-- Obtener información principal de Grupo de Admisión ----------------------------------------------------------------------------
	IF @tipoOperacion = 'GN'
	BEGIN
		SELECT gav.codigo_gru, gav.nombre, a.descripcionReal_Amb, gav.capacidad capacidad_Amb, ISNULL(a.virtual_amb,0) virtual, 
		tam.descripcion_Tam, COUNT(alu.codigo_alu) asignado,
		ISNULL(gav.codigo_tge,-1) codigo_tge, ISNULL(tge.nombre_tge,'') nombre_tge  --> 004
		FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav
		INNER JOIN dbo.Ambiente(NOLOCK) a ON gav.codigo_amb = a.codigo_Amb
		INNER JOIN dbo.TipoAmbiente(NOLOCK) tam ON a.codigo_Tam = tam.codigo_Tam
		LEFT JOIN dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) alu ON gav.codigo_gru = alu.codigo_gru AND alu.estado <> 0
		LEFT JOIN dbo.ADM_TipoGrupoEvaluacion(NOLOCK) tge ON gav.codigo_tge = tge.codigo_tge --> 004
		WHERE gav.codigo_gru = @codigo_gru
		GROUP BY gav.codigo_gru, gav.nombre, a.descripcionReal_Amb, gav.capacidad , ISNULL(a.virtual_amb,0), tam.descripcion_Tam, 
		ISNULL(gav.codigo_tge,-1), tge.nombre_tge --> 004
	END
END

GO