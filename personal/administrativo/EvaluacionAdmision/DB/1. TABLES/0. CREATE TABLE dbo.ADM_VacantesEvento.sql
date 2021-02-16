CREATE TABLE dbo.ADM_VacantesEvento (
    codigo_vae INT IDENTITY
        CONSTRAINT PK_ADM_VacantesEvento
            PRIMARY KEY,
    codigo_cco INT
        CONSTRAINT FK_ADM_VacantesEvento_CentroCostos
            REFERENCES CentroCostos,
    codigo_vac INT
        CONSTRAINT FK_ADM_VacantesEvento_Vacantes
            REFERENCES Vacantes,
    cantidad_vae INT,
    cantidad_accesitarios_vae INT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_vae BIT
)
GO

EXEC sp_addextendedproperty 'MS_Description', 'Vacantes por evento de admisión', 'SCHEMA', 'dbo',
     'TABLE', 'ADM_VacantesEvento'
GO
