/*  Usuario Crea:   andy.diaz
    Fecha:          26/08/2020
    Descripción:    Listar de tabla ADM_PreguntaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_PreguntaEvaluacion_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_prv VARCHAR(100) = ''
    , @codigo_ind INT = 0
    , @codigo_ncp INT = 0
    , @tipo_prv CHAR(1) = ''
    , @texto_prv NVARCHAR(MAX) = ''
    , @codigo_raiz_prv INT = 0
    , @cantidad_prv INT = 0
    , @identificador_prv VARCHAR(20) = ''
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Filtros adicionales
    , @codigo_com INT = 0
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    isnull(prv.codigo_prv, 0)                         AS codigo_prv
                  , isnull(prv.codigo_ind, 0)                         AS codigo_ind
                  , isnull(prv.codigo_ncp, 0)                         AS codigo_ncp
                  , isnull(prv.tipo_prv, '')                          AS tipo_prv
                  , isnull(prv.texto_prv, '')                         AS texto_prv
                  , isnull(prv.codigo_raiz_prv, 0)                    AS codigo_raiz_prv
                  , isnull(prv.cantidad_prv, 0)                       AS cantidad_prv
                  , isnull(prv.identificador_prv, '')                 AS identificador_prv
                  , isnull(prv.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN prv.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, prv.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, prv.fecha_reg, 108) END AS fecha_reg
                  , isnull(prv.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN prv.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, prv.fecha_act, 103) + ' ' +
                             convert(VARCHAR, prv.fecha_act, 108) END AS fecha_act
                  , isnull(prv.estado_prv, 0)                         AS estado_prv
                  , ISNULL(ncp.nombre_ncp, '')						  AS nombre_ncp
                FROM ADM_PreguntaEvaluacion prv WITH (NOLOCK)
                LEFT JOIN ADM_NivelComplejidadPregunta ncp WITH (NOLOCK) ON prv.codigo_ncp = ncp.codigo_ncp
                WHERE 1 = 1
                  AND (@codigo_prv = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_prv, ',')
                                                  WHERE item = prv.codigo_prv))
                  AND (@codigo_ind = 0 OR prv.codigo_ind = @codigo_ind)
                  AND (@codigo_ncp = 0 OR prv.codigo_ncp = @codigo_ncp)
                  AND (@tipo_prv = '' OR prv.tipo_prv LIKE @tipo_prv)
                  AND (@texto_prv = '' OR prv.texto_prv LIKE @texto_prv)
                  AND (@codigo_raiz_prv = 0 OR prv.codigo_raiz_prv = @codigo_raiz_prv)
                  AND (@cantidad_prv = 0 OR prv.cantidad_prv = @cantidad_prv)
                  AND (@codigo_per_reg = 0 OR prv.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(prv.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR prv.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(prv.fecha_act AS DATE) = @fecha_act)
                  AND (@identificador_prv = '' OR prv.identificador_prv LIKE @identificador_prv)
                  AND prv.estado_prv = 1
            END

        IF @tipoConsulta = 'RES' -- Vista resumen
            BEGIN
                SELECT
                    com.codigo_com
                  , com.nombre_com
                  , scom.codigo_scom
                  , scom.nombre_scom
                  , ind.codigo_ind
                  , ind.nombre_ind
                  , sum(CASE WHEN ncp.abreviatura_ncp = 'B' THEN 1 ELSE 0 END) AS cantidad_basica
                  , sum(CASE WHEN ncp.abreviatura_ncp = 'I' THEN 1 ELSE 0 END) AS cantidad_intermedia
                  , sum(CASE WHEN ncp.abreviatura_ncp = 'A' THEN 1 ELSE 0 END) AS cantidad_avanzada
                FROM CompetenciaAprendizaje com WITH (NOLOCK)
                     JOIN ADM_SubCompetencia scom WITH (NOLOCK) ON com.codigo_com = scom.codigo_com
                     JOIN ADM_Indicador ind WITH (NOLOCK) ON scom.codigo_scom = ind.codigo_scom
                     JOIN ADM_PreguntaEvaluacion prv WITH (NOLOCK)
                          ON ind.codigo_ind = prv.codigo_ind AND prv.estado_prv = 1
                     JOIN ADM_NivelComplejidadPregunta ncp WITH (NOLOCK) ON prv.codigo_ncp = ncp.codigo_ncp
                WHERE com.admision_com = 1
                  AND (@codigo_com = 0 OR com.codigo_com = @codigo_com)
                GROUP BY com.codigo_com, com.nombre_com, scom.codigo_scom, scom.nombre_scom, ind.codigo_ind
                       , ind.nombre_ind
            END
            
		IF @tipoConsulta = 'EVL' -- Listar Preguntas por Evaluacion
            BEGIN
                SELECT
                    isnull(prv.codigo_prv, 0)                         AS codigo_prv
                  , isnull(prv.codigo_ind, 0)                         AS codigo_ind
                  , isnull(prv.codigo_ncp, 0)                         AS codigo_ncp
                  , isnull(prv.tipo_prv, '')                          AS tipo_prv
                  , isnull(prv.texto_prv, '')                         AS texto_prv
                  , isnull(prv.codigo_raiz_prv, 0)                    AS codigo_raiz_prv
                  , isnull(prv.cantidad_prv, 0)                       AS cantidad_prv
                  , isnull(prv.identificador_prv, '')                 AS identificador_prv
                  , isnull(prv.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN prv.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, prv.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, prv.fecha_reg, 108) END AS fecha_reg
                  , isnull(prv.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN prv.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, prv.fecha_act, 103) + ' ' +
                             convert(VARCHAR, prv.fecha_act, 108) END AS fecha_act
                  , isnull(prv.estado_prv, 0)                         AS estado_prv
                  , evd.codigo_evd									  AS codigo_evd 
                  , evd.orden_evd									  AS nro_item
                FROM ADM_PreguntaEvaluacion prv WITH (NOLOCK)
                INNER JOIN ADM_EvaluacionDetalle evd WITH (NOLOCK) ON prv.codigo_prv = evd.codigo_prv
                WHERE 1 = 1
                  AND evd.codigo_evl = @codigo_ind
                  AND prv.estado_prv = 1 AND evd.estado_evd = 1
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

GRANT EXECUTE ON [dbo].[ADM_PreguntaEvaluacion_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_PreguntaEvaluacion_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_PreguntaEvaluacion_Listar] TO iusrvirtualsistema
GO