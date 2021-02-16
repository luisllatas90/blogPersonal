CREATE TABLE dbo.ADM_PreguntaEvaluacion (
    codigo_prv INT IDENTITY
        CONSTRAINT PK_ADM_PreguntaEvaluacion
            PRIMARY KEY,
    codigo_ind INT
        CONSTRAINT FK_ADM_PreguntaEvaluacion_ADM_Indicador
            REFERENCES ADM_Indicador,
    codigo_ncp INT
        CONSTRAINT FK_ADM_PreguntaEvaluacion_ADM_NivelComplejidadPregunta
            REFERENCES ADM_NivelComplejidadPregunta,
    tipo_prv CHAR,
    texto_prv NVARCHAR(MAX),
    codigo_raiz_prv INT,
    cantidad_prv INT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_prv BIT,
    identificador_prv VARCHAR(20)
)
GO

