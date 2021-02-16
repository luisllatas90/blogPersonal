CREATE TABLE dbo.ADM_Evaluacion_Alumno_Respuesta (
    codigo_ear INT IDENTITY
        CONSTRAINT PK_ADM_Evaluacion_Alumno_Respuesta
            PRIMARY KEY,
    codigo_elu INT
        CONSTRAINT FK_ADM_Evaluacion_Alumno_Respuesta_ADM_Evaluacion_Alumno
            REFERENCES ADM_Evaluacion_Alumno,
    codigo_evd INT
        CONSTRAINT FK_ADM_Evaluacion_Alumno_Respuesta_ADM_EvaluacionDetalle
            REFERENCES ADM_EvaluacionDetalle,
    codigo_ale INT,
    orden_evd INT,
    orden_ale INT,
    respuesta_ear CHAR,
    correcta_ear BIT,
    puntaje_ear NUMERIC(9,3),
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_ear BIT
)
GO