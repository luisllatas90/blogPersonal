CREATE TABLE dbo.ADM_NivelComplejidadPregunta (
    codigo_ncp INT IDENTITY
        CONSTRAINT PK_ADM_NivelComplejidadPregunta
            PRIMARY KEY,
    nombre_ncp NVARCHAR(50) NOT NULL,
    abreviatura_ncp CHAR NOT NULL,
    descripcion_ncp NVARCHAR(250),
    codigo_per_reg INT,
    fecha_reg DATETIME NOT NULL,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_ncp BIT
)
GO

