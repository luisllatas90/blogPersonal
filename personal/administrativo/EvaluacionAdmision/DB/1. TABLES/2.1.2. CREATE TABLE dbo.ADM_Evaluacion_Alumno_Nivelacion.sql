CREATE TABLE dbo.ADM_Evaluacion_Alumno_Nivelacion (
    codigo_ean INT IDENTITY
        CONSTRAINT PK_ADM_Evaluacion_Alumno_Nivelacion
            PRIMARY KEY,
    codigo_elu INT
        CONSTRAINT FK_ADM_Evaluacion_Alumno
            REFERENCES ADM_Evaluacion_Alumno,
    codigo_com INT
        CONSTRAINT FK_ADM_Evaluacion_Alumno_CompetenciaAprendizaje
            REFERENCES CompetenciaAprendizaje,
    puntaje_ean NUMERIC(9, 3),
    nota_ean NUMERIC(9, 3),
    notaFinal_ean NUMERIC(9, 3),
    estado_calificacion_ean CHAR(1),
    necesita_nivelacion_ean BIT,
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_ean BIT
)
GO