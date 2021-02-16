/*  Usuario Crea:   andy.diaz
    Fecha:          24/09/2020
    Descripción:    Listar de tabla ADM_Evaluacion_Alumno

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Evaluacion_Alumno_Listar
    @tipoConsulta VARCHAR(5) = 'GEN'
    , @codigo_elu VARCHAR(100) = ''
    , @codigo_evl INT = 0
    , @codigo_alu INT = 0
    , @nota_elu NUMERIC(9, 3) = 0
    , @estadonota_elu CHAR(1) = ''
    , @condicion_ingreso_elu CHAR(1) = ''
    , @estadoverificacion_elu CHAR(1) = ''
    , @observacion_elu VARCHAR(500) = ''
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    , @respuesta_elu VARCHAR(MAX) = ''
    , @puntaje_elu NUMERIC(9, 3) = 0
    , @notaFinal_elu NUMERIC(9, 3) = 0
    , @puntajeFinal_elu NUMERIC(9, 3) = 0
    -- Filtros adicionales
    , @codigo_cco INT = 0
    , @codigo_cpf INT = 0
    , @codigo_min INT = 0
    , @nroDocIdent_Alu VARCHAR(15) = ''
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    ISNULL(elu.codigo_elu, 0)                         AS codigo_elu
                  , ISNULL(elu.codigo_evl, 0)                         AS codigo_evl
                  , ISNULL(elu.codigo_alu, 0)                         AS codigo_alu
                  , ISNULL(elu.nota_elu, 0)                        AS nota_elu
                  , ISNULL(elu.estadonota_elu, 0)                  AS estadonota_elu
                  , ISNULL(elu.condicion_ingreso_elu, '')             AS condicion_ingreso_elu
                  , ISNULL(elu.estadoverificacion_elu, '')            AS estadoverificacion_elu
                  , ISNULL(elu.observacion_elu, '')                   AS observacion_elu
                  , ISNULL(elu.codigo_per_reg, 0)                     AS codigo_per_reg
                  , ISNULL(elu.respuesta_elu, '')                     AS respuesta_elu
                  , ISNULL(elu.puntaje_elu, 0)                     AS puntaje_elu
                  , ISNULL(elu.notaFinal_elu, 0)                   AS notaFinal_elu
                  , ISNULL(elu.puntajeFinal_elu, 0)                AS puntajeFinal_elu
                  , CASE
                        WHEN elu.fecha_reg IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, elu.fecha_reg, 103) + ' ' +
                             CONVERT(VARCHAR, elu.fecha_reg, 108) END AS fecha_reg
                  , ISNULL(elu.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN elu.fecha_act IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, elu.fecha_act, 103) + ' ' +
                             CONVERT(VARCHAR, elu.fecha_act, 108) END AS fecha_act
                  , ISNULL(elu.estado_elu, 0)                         AS estado_elu
                  , ISNULL(elu.respuesta_elu, '')                     AS respuesta_elu
                FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_elu = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_elu, ',')
                                                  WHERE item = elu.codigo_elu))
                  AND (@codigo_evl = 0 OR elu.codigo_evl = @codigo_evl)
                  AND (@codigo_alu = 0 OR elu.codigo_alu = @codigo_alu)
                  AND (@nota_elu = 0 OR elu.nota_elu = @nota_elu)
                  AND (@estadonota_elu = 0 OR elu.estadonota_elu = @estadonota_elu)
                  AND (@condicion_ingreso_elu = '' OR elu.condicion_ingreso_elu LIKE @condicion_ingreso_elu)
                  AND (@estadoverificacion_elu = '' OR elu.estadoverificacion_elu LIKE @estadoverificacion_elu)
                  AND (@observacion_elu = '' OR elu.observacion_elu LIKE @observacion_elu)
                  AND (@codigo_per_reg = 0 OR elu.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR CAST(elu.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR elu.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR CAST(elu.fecha_act AS DATE) = @fecha_act)
                  AND (@respuesta_elu = '' OR elu.respuesta_elu LIKE @respuesta_elu)
                  AND (@puntaje_elu = 0 OR elu.puntaje_elu = @puntaje_elu)
                  AND (@notaFinal_elu = 0 OR elu.notaFinal_elu = @notaFinal_elu)
                  AND (@puntajeFinal_elu = 0 OR elu.puntajeFinal_elu = @puntajeFinal_elu)
                  AND elu.estado_elu = 1
            END

        IF @tipoConsulta = 'RC' -- Para recalcular ingresantes
            BEGIN
                SELECT
                    ROW_NUMBER()
                            OVER (PARTITION BY elu.codigo_evl ORDER BY cpf.nombre_Cpf, elu.notaFinal_elu DESC,
                                    ISNULL(alu.apellidoPat_Alu, '') + ' ' +
                                    ISNULL(alu.apellidoMat_Alu, '') + ' ' +
                                    ISNULL(alu.nombres_Alu, ''))                                        puesto
                  , tev.codigo_tev
                  , tev.nombre_tev
                  , alu.codigo_Alu
                  , alu.codigoUniver_Alu
                  , alu.nroDocIdent_Alu
                  , alu.apellidoPat_Alu
                  , alu.apellidoMat_Alu
                  , alu.nombres_Alu
                  , ISNULL(alu.apellidoPat_Alu, '') + ' ' + ISNULL(alu.apellidoMat_Alu, '') + ' ' +
                    ISNULL(alu.nombres_Alu, '')                                                         nombreCompleto
                  , evl.codigo_evl
                  , evl.nombre_evl
                  , cpf.codigo_Cpf
                  , cpf.nombre_Cpf
                  , ISNULL(elu.nota_elu, 0)                                                          nota_elu
                  , elu.estadonota_elu
                  , CASE elu.estadonota_elu WHEN 'P' THEN 'PENDIENTE' WHEN 'C' THEN 'CONFIRMADO' END AS estadoNota
                  , elu.condicion_ingreso_elu
                  , CASE elu.condicion_ingreso_elu
                        WHEN 'P' THEN 'POSTULANTE'
                        WHEN 'A' THEN 'ACCESITARIO'
                        WHEN 'I' THEN 'INGRESANTE' END                                                  condicionIngreso
                  , ISNULL(elu.respuesta_elu, '')                                                       respuesta_elu
                  , ISNULL(elu.puntaje_elu, 0)                                                          puntaje_elu
                  , ISNULL(elu.notaFinal_elu, 0)                                                        notaFinal_elu
                  , ISNULL(elu.puntajeFinal_elu, 0)                                                     puntajeFinal_elu
                  , elu.codigo_elu                                                                      codigo_elu
                  , elu.codigo_alu                                                                      codigo_alu
                FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                     JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
                     JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
                     JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
                     JOIN ADM_TipoEvaluacion tev WITH (NOLOCK) ON evl.codigo_tev = tev.codigo_tev
                WHERE 1 = 1
                  AND alu.codigo_cco = @codigo_cco
                  AND (@codigo_cpf = 0 OR alu.tempcodigo_cpf = @codigo_cpf)
                  AND (@codigo_min = 0 OR alu.codigo_Min = @codigo_min)
                  AND elu.estado_elu = 1
                ORDER BY cpf.nombre_Cpf, elu.notaFinal_elu DESC, nombreCompleto
            END

        IF @tipoConsulta = 'EV' -- Para confirmar las evaluaciones
            BEGIN
                SELECT
                    ROW_NUMBER() OVER (ORDER BY elu.nota_elu DESC)           puesto
                  , tev.codigo_tev
                  , tev.nombre_tev
                  , alu.codigoUniver_Alu
                  , alu.nroDocIdent_Alu
                  , alu.apellidoPat_Alu
                  , alu.apellidoMat_Alu
                  , alu.nombres_Alu
                  , ISNULL(alu.apellidoPat_Alu, '') + ' ' + ISNULL(alu.apellidoMat_Alu, '') + ' ' +
                    ISNULL(alu.nombres_Alu, '')                              nombreCompleto
                  , cpf.codigo_Cpf
                  , cpf.nombre_Cpf
                  , ISNULL(elu.nota_elu, 0)                               nota_elu
                  , elu.estadonota_elu
                  , CASE elu.estadonota_elu WHEN 'P' THEN 'PENDIENTE' END AS estadoNota
                  , elu.condicion_ingreso_elu
                  , CASE elu.condicion_ingreso_elu
                        WHEN 'P' THEN 'POSTULANTE'
                        WHEN 'A' THEN 'ACCESITARIO'
                        WHEN 'I' THEN 'INGRESANTE' END                       condicionIngreso
                  , ISNULL(elu.respuesta_elu, '')                            respuesta_elu
                  , elu.codigo_elu                                           codigo_elu
                  , elu.codigo_alu                                           codigo_alu
                FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                     JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
                     JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
                     JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
                     JOIN ADM_TipoEvaluacion tev WITH (NOLOCK) ON evl.codigo_tev = tev.codigo_tev
                WHERE 1 = 1
                  AND elu.codigo_evl = @codigo_evl
                  AND (@codigo_cpf = 0 OR alu.tempcodigo_cpf = @codigo_cpf)
                  AND (@codigo_min = 0 OR alu.codigo_Min = @codigo_min)
                  AND elu.estado_elu = 1
                ORDER BY elu.nota_elu DESC
            END

        IF @tipoConsulta = 'PA' -- Para accesitarios
            BEGIN
                SELECT
                    ROW_NUMBER() OVER (ORDER BY elu.nota_elu DESC)           puesto
                    --, tev.codigo_tev
                    --, tev.nombre_tev
                  , alu.codigoUniver_Alu
                  , alu.nroDocIdent_Alu
                  , alu.apellidoPat_Alu
                  , alu.apellidoMat_Alu
                  , alu.nombres_Alu
                  , ISNULL(alu.apellidoPat_Alu, '') + ' ' + ISNULL(alu.apellidoMat_Alu, '') + ' ' +
                    ISNULL(alu.nombres_Alu, '')                              nombreCompleto
                  , cpf.codigo_Cpf
                  , cpf.nombre_Cpf
                  , ISNULL(elu.nota_elu, 0)                               nota_elu
                  , elu.estadonota_elu
                  , CASE elu.estadonota_elu WHEN 'P' THEN 'PENDIENTE' END AS estadoNota
                  , elu.condicion_ingreso_elu
                  , CASE elu.condicion_ingreso_elu
                        WHEN 'P' THEN 'POSTULANTE'
                        WHEN 'A' THEN 'ACCESITARIO'
                        WHEN 'I' THEN 'INGRESANTE' END                       condicionIngreso
                  , ISNULL(elu.respuesta_elu, '')                            respuesta_elu
                  , elu.codigo_elu                                           codigo_elu
                  , elu.codigo_alu                                           codigo_alu
                  , alu.codigo_Min                                           codigo_Min
                  , mi.nombre_Min                                            nombre_Min
                  , ISNULL(tb1.nro, 0)                                       cant_noti
                  , 'NO'                                                     ind_cargo
                FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                     JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
                     JOIN ModalidadIngreso mi WITH (NOLOCK) ON alu.codigo_Min = mi.codigo_Min
                     JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
                     LEFT JOIN (SELECT ed.codigoDestinatario_end, COUNT(ed.codigo_end) nro
                                FROM dbo.EnvioCorreosMasivo (NOLOCK) ecm
                                     INNER JOIN dbo.CINS_EnvioNotificacionDetalle (NOLOCK) ed
                                                ON ecm.codigo_ecm = ed.codigoMedio_end
                                WHERE codigo_apl = 32
                                  AND ed.tipoCodigoMedio_end = 'codigo_ecm'
                                  AND ecm.estado = 1
                                  AND ed.estado = 1
                                  AND ed.codigo_not = 46
                                GROUP BY ed.codigoDestinatario_end) AS tb1
                               ON tb1.codigoDestinatario_end = elu.codigo_alu
                     --JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
                     --JOIN ADM_TipoEvaluacion tev WITH (NOLOCK) ON evl.codigo_tev = tev.codigo_tev
                WHERE 1 = 1
                  AND alu.codigo_cco = @codigo_cco
                  AND (@codigo_cpf = 0 OR alu.tempcodigo_cpf = @codigo_cpf)
                  --AND (@codigo_min = 0 OR alu.codigo_Min = @codigo_min)
                  AND elu.condicion_ingreso_elu = 'A'
                  AND elu.estado_elu = 1
                ORDER BY elu.nota_elu DESC
            END

        IF @tipoConsulta = 'RF' -- Resultados Finales
            BEGIN
                SELECT
                    alu.codigoUniver_Alu
                  , alu.nroDocIdent_Alu
                  , alu.apellidoPat_Alu
                  , alu.apellidoMat_Alu
                  , alu.nombres_Alu
                  , ISNULL(alu.apellidoPat_Alu, '') + ' ' + ISNULL(alu.apellidoMat_Alu, '') + ' ' +
                    ISNULL(alu.nombres_Alu, '')                                                         nombreCompleto
                  , cpf.codigo_Cpf
                  , cpf.nombre_Cpf
                  , ISNULL(elu.nota_elu, 0)                                                          nota_elu
                  , elu.estadonota_elu
                  , CASE elu.estadonota_elu WHEN 'P' THEN 'PENDIENTE' WHEN 'C' THEN 'CONFIRMADO' END AS estadoNota
                  , CASE
                        WHEN alu.condicion_Alu = 'P' THEN 'P'
                        WHEN alu.condicion_Alu = 'I' AND ISNULL(alu.alcanzo_vacante, 0) = 0 THEN 'A'
                        WHEN alu.condicion_Alu = 'I' AND ISNULL(alu.alcanzo_vacante, 0) = 1
                            THEN 'I' END                                                             AS condicionIngreso
                  , CASE
                        WHEN alu.condicion_Alu = 'P' THEN 'NO INGRESANTE'
                        WHEN alu.condicion_Alu = 'I' AND ISNULL(alu.alcanzo_vacante, 0) = 0 THEN 'ACCESITARIO'
                        WHEN alu.condicion_Alu = 'I' AND ISNULL(alu.alcanzo_vacante, 0) = 1
                            THEN 'INGRESANTE' END                                                    AS descripcionCondicionIngreso
                  , ISNULL(elu.puntaje_elu, 0)                                                          puntaje_elu
                  , ISNULL(elu.notaFinal_elu, 0)                                                        notaFinal_elu
                  , ISNULL(elu.puntajeFinal_elu, 0)                                                     puntajeFinal_elu
                FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                     JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
                     JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
                WHERE 1 = 1
                  AND alu.codigo_cco = @codigo_cco
                  AND (@codigo_cpf = 0 OR alu.tempcodigo_cpf = @codigo_cpf)
                  AND (@codigo_min = 0 OR alu.codigo_Min = @codigo_min)
                  AND (@nroDocIdent_Alu = '' OR alu.nroDocIdent_Alu = @nroDocIdent_Alu)
                  AND elu.estado_elu = 1
                ORDER BY cpf.nombre_Cpf, elu.nota_elu DESC, nombreCompleto
            END

    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE()

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
GO

GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Listar] TO iusrvirtualsistema
GO
