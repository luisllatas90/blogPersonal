CREATE TABLE dbo.ADM_AlternativaEvaluacion (
    codigo_ale INT IDENTITY
        CONSTRAINT PK_ADM_AlternativaEvaluacion
            PRIMARY KEY,
    codigo_prv INT
        CONSTRAINT FK_ADM_AlternativaEvaluacion_ADM_PreguntaEvaluacion
            REFERENCES ADM_PreguntaEvaluacion,
    orden_ale INT,
    texto_ale NVARCHAR(MAX),
    correcta_ale BIT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_ale BIT
)
GO

