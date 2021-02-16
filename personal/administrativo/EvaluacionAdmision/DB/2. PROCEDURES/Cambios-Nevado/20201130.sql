select * from dbo.ADM_Evaluacion_Alumno 

select * from dbo.ADM_Evaluacion_Alumno_Respuesta 

select * from dbo.ADM_Evaluacion 

--alter table dbo.ADM_Evaluacion add idArchivoPreguntas bigint

select * from dbo.ADM_EvaluacionDetalle where estado_evd = 1

select * from dbo.ADM_Evaluacion_Alumno where estado_elu = 1

--alter table dbo.ADM_EvaluacionDetalle add  codigo_ind int
--alter table dbo.ADM_EvaluacionDetalle add  codigo_ncp int

select * from dbo.ADM_Indicador 
select * from dbo.ADM_NivelComplejidadPregunta

select * from dbo.ADM_TipoEvaluacion_Indicador where estado_tei = 1

select dbo.RetornaToken()

INSERT TablaArchivo (NombreTabla,PkTabla,Estado,RootPath,tokenTabla)
VALUES ('ADM_Evaluacion','codigo_evl','A','C:/Documentos/EvaluacionAdmision/Preguntas/','JX5LZ76SFA')

select * from dbo.TablaArchivo 

--select * from dbo.ArchivoCompartido where IdTabla = 36

exec ADM_EvaluacionDetalle_Listar '1', -1, 13, -1, 3

select * from vstalumno where codigo_cco = 7385 and estadoActual_Alu = 1


select * from dbo.CompetenciaAprendizaje where admision_com = 1

select * from dbo.CompetenciaAprendizaje where codigo_cat = 1 and codigo_tcom = 1

--update dbo.CompetenciaAprendizaje set estado_com = 0
--where codigo_tcom = 1 and codigo_cat = 1 and estado_com = 1 and isnull(admision_com,0) = 0 
--and codigo_com not in (335, 336, 337, 101)

--update dbo.CompetenciaAprendizaje set estado_com = 0 where codigo_com in (1,2,3,354)

--update dbo.CompetenciaAprendizaje set admision_com = 1 where codigo_com in (335, 336, 337, 101)

--335
--336
--337
--101

select * from dbo.PerfilIngreso(NOLOCK)

select cp.codigo_Cpf, cp.nombre_Cpf, pc.codigo_pcur, pc.nombre_pcur, pin.codigo_pIng, pin.codigo_pcur, pin.descripcion_pIng, 
ca.codigo_com, ca.admision_com, ca.nombre_com, ca.descripcion_com, ca.estado_com 
from dbo.PerfilIngreso(NOLOCK) pin
inner join dbo.PlanCurricular(nolock) pc on pin.codigo_pcur = pc.codigo_pcur
inner join dbo.CarreraProfesional(NOLOCK) cp on pc.codigo_cpf = cp.codigo_Cpf
inner join dbo.CompetenciaAprendizaje(nolock) ca on pin.codigo_com = ca.codigo_com
where estado_pIng = 1 and codigo_cat = 1
order by nombre_Cpf

select * from personal where apellidopat_per = 'MARIN'

select * from vstCursoProgramado where codigo_Cac = 75 and nombre_Cur like '%Implementación del Sistema%'

select * from vstCargaAcademica where codigo_Per = 6095