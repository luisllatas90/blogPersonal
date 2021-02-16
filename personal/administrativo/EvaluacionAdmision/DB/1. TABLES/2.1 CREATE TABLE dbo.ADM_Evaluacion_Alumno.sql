CREATE TABLE dbo.ADM_Evaluacion_Alumno (
    codigo_elu INT IDENTITY
        CONSTRAINT PK_ADM_Evaluacion_Alumno
            PRIMARY KEY,
    codigo_evl INT
        CONSTRAINT FK_ADM_Evaluacion_Alumno_ADM_Evaluacion
            REFERENCES ADM_Evaluacion,
    codigo_alu INT
        CONSTRAINT FK_ADM_Evaluacion_Alumno_Alumno
            REFERENCES Alumno,
    nota_elu NUMERIC(9, 3),
    estadonota_elu CHAR(1),
    condicion_ingreso_elu CHAR,
    estadoverificacion_elu CHAR,
    observacion_elu VARCHAR(500),
    codigo_per_reg INT,
    fecha_reg DATETIME,
    codigo_per_act INT,
    fecha_act DATETIME,
    estado_elu BIT,
    respuesta_elu VARCHAR(MAX),
    puntaje_elu NUMERIC(9, 3),
    notaFinal_elu NUMERIC(9, 3),
    puntajeFinal_elu NUMERIC(9, 3)
)
GO