alter table dbo.ADM_TipoEvaluacion add virtual_tev bit
alter table dbo.ADM_Evaluacion add idArchivoPreguntas bigint
alter table dbo.ADM_Evaluacion add virtual_evl bit
alter table dbo.ADM_EvaluacionDetalle add  codigo_ind int
alter table dbo.ADM_EvaluacionDetalle add  codigo_ncp int
go

INSERT TablaArchivo (NombreTabla,PkTabla,Estado,RootPath,tokenTabla)
VALUES ('ADM_Evaluacion','codigo_evl','A','C:/Documentos/EvaluacionAdmision/Preguntas/','JX5LZ76SFA')
go