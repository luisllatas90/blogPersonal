CREATE TABLE dbo.ADM_Configuracion_NotaMinima (
    codigo_cnm INT IDENTITY
        CONSTRAINT PK_ADM_Configuracion_NotaMinima
            PRIMARY KEY,
    codigo_cpf INT
        CONSTRAINT FK_ADM_Configuracion_NotaMinima_CarreraProfesional
            REFERENCES CarreraProfesional,
    codigo_cco INT
        CONSTRAINT FK_ADM_Configuracion_NotaMinima_CentroCostos
            REFERENCES CentroCostos,
    nota_min_cnm NUMERIC(9, 2),
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_cnm BIT
)
GO
