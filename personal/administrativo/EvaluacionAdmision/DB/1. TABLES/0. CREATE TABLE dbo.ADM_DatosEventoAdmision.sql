CREATE TABLE dbo.ADM_DatosEventoAdmision (
    codigo_dea INT IDENTITY
        CONSTRAINT ADM_DatosEventoAdmision_pk
            PRIMARY KEY,
    codigo_cco INT
        CONSTRAINT FK_ADM_DatosEventoAdmision_CentroCostos
            REFERENCES CentroCostos,
    codigo_cac INT
        CONSTRAINT FK_ADM_DatosEventoAdmision_CicloAcademico
            REFERENCES CicloAcademico,
    fechaEvento_dea DATE,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_dea BIT
)
GO