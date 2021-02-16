CREATE TABLE dbo.ADM_ComisionPermanente (
    codigo_cop INT IDENTITY
        CONSTRAINT PK_ADM_ComisionPermanente
            PRIMARY KEY,
    codigo_per INT
        CONSTRAINT FK_ADM_ComisionPermanente_Personal
            REFERENCES Personal,
    codigo_ccm INT
        CONSTRAINT FK_ADM_ComisionPermanente_ADM_CargoComision
            REFERENCES ADM_CargoComision,
    nro_resolucion_cop VARCHAR(50),
    vigente_cop BIT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_cop BIT
)
GO

EXEC sp_addextendedproperty 'MS_Description', 'Comisión permanente de admisión', 'SCHEMA', 'dbo',
     'TABLE', 'ADM_ComisionPermanente'
GO