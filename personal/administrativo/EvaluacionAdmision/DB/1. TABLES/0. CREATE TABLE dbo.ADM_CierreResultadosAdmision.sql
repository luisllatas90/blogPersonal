CREATE TABLE dbo.ADM_CierreResultadosAdmision (
    codigo_cra INT IDENTITY
        CONSTRAINT ADM_CierreResultadosAdmision_pk
            PRIMARY KEY,
    codigo_cac INT
        CONSTRAINT FK_ADM_CierreResultadosAdmision_CicloAcademico
            REFERENCES CicloAcademico,
    codigo_min TINYINT
        CONSTRAINT FK_ADM_CierreResultadosAdmision_ModalidadIngreso
            REFERENCES ModalidadIngreso,
    codigo_cpf INT
        CONSTRAINT FK_ADM_CierreResultadosAdmision_CarreraProfesional
            REFERENCES CarreraProfesional,
    codigo_cco INT
        CONSTRAINT FK_ADM_CierreResultadosAdmision_CentroCostos
            REFERENCES CentroCostos,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_cra BIT
)
GO