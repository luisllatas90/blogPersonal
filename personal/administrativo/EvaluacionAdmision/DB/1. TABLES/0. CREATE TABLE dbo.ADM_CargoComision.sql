CREATE TABLE dbo.ADM_CargoComision (
    codigo_ccm INT IDENTITY
        CONSTRAINT PK_ADM_CargoComision
            PRIMARY KEY,
    nombre_ccm NVARCHAR(50) NOT NULL,
    descripcion_ccm NVARCHAR(250),
    codigo_per_reg INT,
    fecha_reg DATETIME NOT NULL,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_ccm BIT
)
GO

