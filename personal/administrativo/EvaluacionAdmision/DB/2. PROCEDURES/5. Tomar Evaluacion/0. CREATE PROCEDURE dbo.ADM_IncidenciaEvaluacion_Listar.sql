/*  Usuario Crea:   andy.diaz
    Fecha:          15/09/2020
    Descripción:    Listar de tabla ADM_IncidenciaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_IncidenciaEvaluacion_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_ine VARCHAR(100) = ''
    , @codigo_gru INT = 0
    , @descripcion_ine VARCHAR(250) = ''
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Filtros adicionales
    , @codigo_cco INT = 0
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    isnull(ine.codigo_ine, 0)                         AS codigo_ine
                  , isnull(ine.codigo_gru, 0)                         AS codigo_gru
                  , isnull(ine.descripcion_ine, '')                   AS descripcion_ine
                  , isnull(ine.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN ine.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, ine.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, ine.fecha_reg, 108) END AS fecha_reg
                  , isnull(ine.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN ine.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, ine.fecha_act, 103) + ' ' +
                             convert(VARCHAR, ine.fecha_act, 108) END AS fecha_act
                  , isnull(ine.estado_ine, 0)                         AS estado_ine
                FROM ADM_IncidenciaEvaluacion ine WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_ine = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ine, ',')
                                                  WHERE item = ine.codigo_ine))
                  AND (@codigo_gru = 0 OR ine.codigo_gru = @codigo_gru)
                  AND (@descripcion_ine = '' OR ine.descripcion_ine LIKE @descripcion_ine)
                  AND (@codigo_per_reg = 0 OR ine.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(ine.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ine.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(ine.fecha_act AS DATE) = @fecha_act)
                  AND ine.estado_ine = 1
            END

        IF @tipoConsulta = 'LT'
            BEGIN
                SELECT
                    isnull(ine.codigo_ine, 0)       AS codigo_ine
                  , isnull(ine.codigo_gru, 0)       AS codigo_gru
                  , isnull(ine.descripcion_ine, '') AS descripcion_ine
                  , isnull(gru.nombre, '')          AS nombre_gru
                  , isnull(cco.codigo_Cco, 0)       AS codigo_cco
                  , isnull(cco.descripcion_Cco, '') AS descripcion_cco
                FROM ADM_IncidenciaEvaluacion ine WITH (NOLOCK)
                     JOIN ADM_GrupoAdmisionVirtual gru WITH (NOLOCK) ON ine.codigo_gru = gru.codigo_gru
                     JOIN ADM_GrupoAdmision_CentroCosto gcc WITH (NOLOCK)
                          ON gru.codigo_gru = gcc.codigo_gru
                              AND (@codigo_cco = 0 OR gcc.codigo_cco = @codigo_cco) AND gcc.estado_gcc = 1
                     JOIN CentroCostos cco WITH (NOLOCK) ON gcc.codigo_cco = cco.codigo_Cco
                WHERE 1 = 1
                  AND (@codigo_ine = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ine, ',')
                                                  WHERE item = ine.codigo_ine))
                  AND (@codigo_gru = 0 OR ine.codigo_gru = @codigo_gru)
                  AND (@descripcion_ine = '' OR ine.descripcion_ine LIKE @descripcion_ine)
                  AND (@codigo_per_reg = 0 OR ine.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(ine.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ine.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(ine.fecha_act AS DATE) = @fecha_act)
                  AND ine.estado_ine = 1
            END

        IF @tipoConsulta = 'EDIT'
            BEGIN
                SELECT
                    isnull(ine.codigo_ine, 0)       AS codigo_ine
                  , isnull(ine.codigo_gru, 0)       AS codigo_gru
                  , isnull(ine.descripcion_ine, '') AS descripcion_ine
                  , isnull(cco.codigo_Cco, 0)       AS codigo_cco
                  , isnull(dea.codigo_cac, 0)       AS codigo_cac
                FROM ADM_IncidenciaEvaluacion ine WITH (NOLOCK)
                     JOIN ADM_GrupoAdmisionVirtual gru WITH (NOLOCK) ON ine.codigo_gru = gru.codigo_gru
                     CROSS APPLY (SELECT TOP 1 _gcc.codigo_gcc, _gcc.codigo_cco
                                  FROM ADM_GrupoAdmision_CentroCosto _gcc WITH (NOLOCK)
                                  WHERE _gcc.codigo_gru = gru.codigo_gru
                                    AND (@codigo_cco = 0 OR _gcc.codigo_cco = @codigo_cco)
                                    AND _gcc.estado_gcc = 1) gcc
                     JOIN CentroCostos cco WITH (NOLOCK) ON gcc.codigo_cco = cco.codigo_Cco
                     JOIN ADM_DatosEventoAdmision dea WITH (NOLOCK) ON cco.codigo_Cco = dea.codigo_cco
                WHERE 1 = 1
                  AND (EXISTS(SELECT item
                              FROM fnSplit2(@codigo_ine, ',')
                              WHERE item = ine.codigo_ine))
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

GRANT EXECUTE ON [dbo].[ADM_IncidenciaEvaluacion_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_IncidenciaEvaluacion_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_IncidenciaEvaluacion_Listar] TO iusrvirtualsistema
GO