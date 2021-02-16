select * from dbo.ADM_Evaluacion
select * from dbo.ADM_EvaluacionDetalle where codigo_evl = 1 and estado_evd = 1
select * from dbo.ADM_EvaluacionDetalle_Observacion

select * from dbo.ADM_Evaluacion_Alumno

alter table ADM_Evaluacion_Alumno add respuesta_elu varchar(max)

select * from dbo.ADM_Evaluacion_Alumno_Respuesta

--INSERT INTO dbo.ADM_Evaluacion_Alumno_Respuesta(codigo_elu,codigo_evd,codigo_ale,orden_evd,orden_ale,respuesta_ear,codigo_per_reg,fecha_reg,estado_ear )
--OUTPUT inserted.codigo_ear
--values(1,1,1,1,1,'',1,GETDATE(),1)

select * from dbo.TablaArchivo 

select * from dbo.ArchivoCompartido where IdTabla = 15 order by IdArchivosCompartidos desc


--exec ADM_ProcesarResultados_Test 2, 'C:\Usat\ProcesarExamen\eva01.csv', 684, 0, ''

--sp_helptext ADM_CargarExcelNotas

select * from dbo.ADM_GrupoAdmision_CentroCosto where codigo_cco = 7376

select * from dbo.ADM_GrupoAdmisionVirtual_Alumno where codigo_gru = 31

select * from vstalumno where codigo_cco = 7376 and estadoActual_Alu = 1

--update dbo.ADM_Evaluacion set nombre_evl = 'Evaluacion A' where codigo_evl = 1

select * from dbo.ADM_PreguntaEvaluacion 

select * from dbo.ADM_NivelComplejidadPregunta

SELECT evd.codigo_evl, prv.tipo_prv, COUNT(evd.codigo_evd) cant_total, 
SUM(CASE evd.estadovalidacion_evd WHEN 'P' THEN 1 ELSE 0 END) cant_pendiente,
SUM(CASE evd.estadovalidacion_evd WHEN 'C' THEN 1 ELSE 0 END) cant_conforme,
SUM(CASE evd.estadovalidacion_evd WHEN 'O' THEN 1 ELSE 0 END) cant_observada
FROM dbo.ADM_EvaluacionDetalle(NOLOCK) evd
INNER JOIN dbo.ADM_PreguntaEvaluacion(NOLOCK) prv ON evd.codigo_prv = prv.codigo_prv
WHERE evd.estado_evd = 1
GROUP BY evd.codigo_evl, prv.tipo_prv

--update dbo.ADM_EvaluacionDetalle set estado_evd = 0 where codigo_evl = 1

--update dbo.ADM_EvaluacionDetalle set estado_evd = 0 where codigo_evd = 1
--update dbo.ADM_EvaluacionDetalle set estado_evd = 1 where codigo_evd = 2
--update dbo.ADM_EvaluacionDetalle set estado_evd = 1 where codigo_evd = 3
--update dbo.ADM_EvaluacionDetalle set estado_evd = 0 where codigo_evd = 8

select * from dbo.ADM_TipoEvaluacion_Indicador

select * from dbo.ADM_PreguntaEvaluacion
select * from dbo.ADM_Indicador where estado_ind = 1

select * from ADM_EvaluacionDetalle

--EXEC sp_RENAME 'ADM_EvaluacionDetalle.estadovalidacion_evl', 'estadovalidacion_evd', 'COLUMN'

--update dbo.ADM_Evaluacion set estado_evl = 1

select * from CompetenciaAprendizaje where admision_com = 1
select * from dbo.PerfilIngreso where codigo_com in (1,2,3,354) and estado_pIng = 1

--alter table CompetenciaAprendizaje add nombre_corto_com varchar(500)

select * from ADM_Componente 
select * from ADM_Componente_CompetenciaAprendizaje where estado_cca = 0

--update ADM_Componente set estado_cmp = 0 where codigo_cmp in (3,4,5)

--update ADM_Componente_CompetenciaAprendizaje set estado_cca = 1, codigo_cmp = 1, codigo_com = 1 where codigo_cca = 1
--update ADM_Componente_CompetenciaAprendizaje set estado_cca = 1, codigo_cmp = 1, codigo_com = 2 where codigo_cca = 2
--update ADM_Componente_CompetenciaAprendizaje set estado_cca = 1, codigo_cmp = 2, codigo_com = 3 where codigo_cca = 3

select * from ADM_SubCompetencia
select * from ADM_Indicador 
select * from ADM_PesoCompetencia

select * from ADM_GrupoAdmisionVirtual

select * from ADM_TipoEvaluacion_OrdenCompetencia

select * from ADM_TipoEvaluacion_Indicador where codigo_tev = 5 and estado_tei = 1

select * from ADM_ConfiguracionEvaluacionEvento where codigo_tev = 5

exec ADM_ReporteEstructuraTipoEvaluacion 7376, 24, 5

SELECT tev.codigo_tev, tev.nombre_tev, f.codigo_Fac, f.nombre_Fac, cp.codigo_Cpf, cp.nombre_Cpf, cee.codigo_cee, cee.cantidad_cee, 
	cco.codigo_Cco, cco.descripcion_Cco, ceep.nro_orden_ceep, ceep.peso_ceep, --cmp.codigo_cmp, cmp.nombre_cmp, 
	--com.codigo_com, com.nombre_com,
	--scom.codigo_scom, scom.nombre_scom, 
	tei.codigo_ind, --ind.codigo_ind, ind.nombre_ind, ind.descripcion_ind, 
	tei.cantidad_preguntas_tei--, pcom.peso_pcom, (pcom.peso_pcom * tev.peso_basica_tev) niv_base
	--, (pcom.peso_pcom * tev.peso_intermedia_tev) niv_intermedia, (pcom.peso_pcom * tev.peso_avanzada_tev) niv_avanzada--,
	--p.nro_item
	FROM dbo.ADM_TipoEvaluacion(NOLOCK) tev
	INNER JOIN dbo.ADM_ConfiguracionEvaluacionEvento(NOLOCK) cee ON tev.codigo_tev = cee.codigo_tev
	INNER JOIN dbo.CentroCostos(NOLOCK) cco ON cee.codigo_cco = cco.codigo_Cco
	INNER JOIN dbo.ADM_DatosEventoAdmision(NOLOCK) dea ON cee.codigo_cco = dea.codigo_cco
	INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON cee.codigo_cpf = cp.codigo_Cpf
	INNER JOIN dbo.Facultad(NOLOCK) f ON cp.codigo_Fac = f.codigo_Fac
	INNER JOIN dbo.ADM_ConfiguracionEvaluacionEvento_Peso(NOLOCK) ceep ON cee.codigo_cee = ceep.codigo_cee
	INNER JOIN dbo.ADM_TipoEvaluacion_Indicador(NOLOCK) tei ON tev.codigo_tev = tei.codigo_tev
	--INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON tei.codigo_ind = ind.codigo_ind
	--INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
	--INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
	--INNER JOIN dbo.ADM_Componente_CompetenciaAprendizaje(NOLOCK) cca ON com.codigo_com = cca.codigo_com 
	--INNER JOIN dbo.ADM_Componente(NOLOCK) cmp ON cca.codigo_cmp = cmp.codigo_cmp
	--INNER JOIN dbo.ADM_PesoCompetencia(NOLOCK) pcom ON com.codigo_com = pcom.codigo_com AND cp.codigo_Cpf = pcom.codigo_cpf AND pcom.codigo_cac = dea.codigo_cac
	--INNER JOIN #Preguntas p ON tei.codigo_tei = p.codigo_tei
	WHERE tev.estado_tev = 1 AND cee.estado_cee = 1 AND ceep.estado_ceep = 1 AND tei.estado_tei = 1
	--AND ind.estado_ind = 1 --AND scom.estado_scom = 1 --AND com.estado_com = 1 --AND pcom.estado_pcom = 1 
	AND dea.estado_dea = 1
	--AND cca.estado_cca = 1 AND cmp.estado_cmp = 1
	AND cee.codigo_cco = 7376 AND cee.codigo_cpf = 24 AND cee.codigo_tev = 5
	order by --cmp.codigo_cmp, com.codigo_com, ind.codigo_ind
	tei.codigo_ind 

--alter table ADM_Indicador alter column descripcion_ind varchar(500) 

select * from ADM_ConfiguracionEvaluacionEvento_Peso

--update ADM_ConfiguracionEvaluacionEvento_Peso set estado_ceep = 0 where codigo_ceep in (11,12)

select * from dbo.CarreraProfesional 

select * from vstCursoProgramado where codigo_Cac = 75 and codigo_Cpf = 4 and Refcodigo_Cup = codigo_Cup 

select * from vstalumno where apellidoPat_Alu = 'NEVADO'
select * from dbo.TramiteAlumno where codigo_alu = 86296


SELECT cca.codigo_cca, cmp.codigo_cmp, cmp.nombre_cmp, com.codigo_com, com.nombre_com 
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com 
		INNER JOIN dbo.ADM_Componente_CompetenciaAprendizaje(NOLOCK) cca ON com.codigo_com = cca.codigo_com
		INNER JOIN dbo.ADM_Componente(NOLOCK) cmp ON cca.codigo_cmp = cmp.codigo_cmp
		WHERE com.admision_com = 1 AND cca.estado_cca = 1 AND cmp.estado_cmp = 1

select * from ADM_TipoEvaluacion 
select * from ADM_TipoEvaluacion_Indicador

select * from ADM_ConfiguracionEvaluacionEvento 
select * from ADM_ConfiguracionEvaluacionEvento_Peso

--update ADM_ConfiguracionEvaluacionEvento set estado_cee = 0 where codigo_cee = 12

--update ADM_ConfiguracionEvaluacionEvento_Peso set nro_orden_ceep = 1

--update ADM_PesoCompetencia set codigo_com = 2 where codigo_pcom = 2

select * from ADM_Componente_CompetenciaAprendizaje

select *, dbo.ADM_ConfigEvalEventoPeso(codigo_cee) from dbo.ADM_ConfiguracionEvaluacionEvento where estado_cee = 1

SELECT *
FROM sys.sql_modules
WHERE OBJECT_NAME(OBJECT_ID) like '%importar%'

select LEN(convert(varchar,texto_prv)) from dbo.ADM_PreguntaEvaluacion 



exec COM_ListarFechaSesion 2753, 3119, 633856, -1, 'GR'
exec COM_ListarFechaSesion 2795, 3120, 633857, -1, 'GR'

select * from dbo.FechaSesion where codigo_cup = 633857

select * from dbo.DiseñoAsignatura where codigo_cac = 75 and codigo_cur = 3120

select * from vstCursoProgramado where codigo_Cac = 75 and codigo_Cpf = 24 and Refcodigo_Cup = codigo_Cup  and modular_pcu = 1