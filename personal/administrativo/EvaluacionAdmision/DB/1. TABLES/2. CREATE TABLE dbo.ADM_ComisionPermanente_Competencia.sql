CREATE TABLE dbo.ADM_ComisionPermanente_Competencia (
    codigo_cpc INT IDENTITY
        CONSTRAINT PK_ADM_ComisionPermision_Competencia
            PRIMARY KEY,
    codigo_cop INT
        CONSTRAINT FK_ADM_ComisionPermision_Competencia_ADM_ComisionPermanente
            REFERENCES ADM_ComisionPermanente,
    codigo_com INT
        CONSTRAINT FK_ADM_ComisionPermision_Competencia_CompetenciaAprendizaje
            REFERENCES CompetenciaAprendizaje,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_cpc BIT
)
GO

EXEC sp_addextendedproperty 'MS_Description', 'Relación de comisión permanente y competencias', 'SCHEMA', 'dbo',
     'TABLE', 'ADM_ComisionPermanente_Competencia'
GO