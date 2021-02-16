CREATE TABLE dbo.ADM_Configuracion_NotaMinima_Competencia (
    codigo_cnc INT IDENTITY
        CONSTRAINT PK_ADM_Configuracion_NotaMinima_Competencia
            PRIMARY KEY,
    codigo_cnm INT
        CONSTRAINT FK_ADM_Configuracion_NotaMinima_Competencia_ADM_Configuracion_NotaMinima
            REFERENCES ADM_Configuracion_NotaMinima,
    codigo_com INT
        CONSTRAINT FK_ADM_Configuracion_NotaMinima_Competencia_CompetenciaAprendizaje
            REFERENCES CompetenciaAprendizaje,
    nota_min_cnc INT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_cnc INT
)
GO
