CREATE TABLE dbo.ADM_Evaluacion (
    codigo_evl INT IDENTITY
        CONSTRAINT PK_ADM_Evaluacion
            PRIMARY KEY,
    codigo_cco INT
        CONSTRAINT FK_ADM_Evaluacion_CentroCostos
            REFERENCES CentroCostos,
    codigo_tev INT
        CONSTRAINT FK_ADM_Evaluacion_ADM_TipoEvaluacion
            REFERENCES ADM_TipoEvaluacion,
    nombre_evl VARCHAR(250),
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_evl BIT,
    estadovalidacion_evl CHAR,
    idArchivoCompartido BIGINT,
    virtual_evl BIT DEFAULT 0,
    idArchivoPreguntas BIGINT
)
GO

