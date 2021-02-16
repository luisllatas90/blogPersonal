CREATE TABLE dbo.ADM_AsistenciaEvaluacion (
    codigo_ase INT IDENTITY
        CONSTRAINT PK_ADM_AsistenciaEvaluacion
            PRIMARY KEY,
    codigo_gru INT
        CONSTRAINT FK_ADM_AsistenciaEvaluacion_ADM_GrupoAdmisionVirtual
            REFERENCES ADM_GrupoAdmisionVirtual,
    codigo_alu INT
        CONSTRAINT FK_ADM_AsistenciaEvaluacion_Alumno
            REFERENCES Alumno,
    estadoAsistencia_ase CHAR,
    fechaCierre_ase DATETIME,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_ase BIT
)
GO