CREATE TABLE dbo.ADM_EvaluacionDetalle (
    codigo_evd INT IDENTITY
        CONSTRAINT PK_ADM_EvaluacionDetalle
            PRIMARY KEY,
    codigo_evl INT
        CONSTRAINT FK_ADM_EvaluacionDetalle_ADM_Evaluacion
            REFERENCES ADM_Evaluacion,
    codigo_prv INT
        CONSTRAINT FK_ADM_EvaluacionDetalle_ADM_PreguntaEvaluacion
            REFERENCES ADM_PreguntaEvaluacion,
    orden_evd INT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_evd BIT,
    estadovalidacion_evd CHAR,
    codigo_ind INT,
    codigo_ncp INT
)
GO

