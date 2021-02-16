/*  Usuario Crea:   andy.diaz
    Fecha:          27/08/2020
    Descripción:    Listar de tabla ADM_AlternativaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_AlternativaEvaluacion_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_ale VARCHAR(100) = ''
    , @codigo_prv INT = 0
    , @orden_ale INT = 0
    , @texto_ale NVARCHAR(MAX) = ''
    , @correcta_ale BIT = NULL
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    isnull(ale.codigo_ale, 0)                         AS codigo_ale
                  , isnull(ale.codigo_prv, 0)                         AS codigo_prv
                  , isnull(ale.orden_ale, 0)                          AS orden_ale
                  , isnull(ale.texto_ale, '')                         AS texto_ale
                  , isnull(ale.correcta_ale, 0)                       AS correcta_ale
                  , isnull(ale.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN ale.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, ale.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, ale.fecha_reg, 108) END AS fecha_reg
                  , isnull(ale.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN ale.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, ale.fecha_act, 103) + ' ' +
                             convert(VARCHAR, ale.fecha_act, 108) END AS fecha_act
                  , isnull(ale.estado_ale, 0)                         AS estado_ale
                FROM ADM_AlternativaEvaluacion ale WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_ale = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ale, ',')
                                                  WHERE item = ale.codigo_ale))
                  AND (@codigo_prv = 0 OR ale.codigo_prv = @codigo_prv)
                  AND (@orden_ale = 0 OR ale.orden_ale = @orden_ale)
                  AND (@texto_ale = '' OR ale.texto_ale LIKE @texto_ale)
                  AND (@correcta_ale IS NULL OR ale.correcta_ale = @correcta_ale)
                  AND (@codigo_per_reg = 0 OR ale.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(ale.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ale.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(ale.fecha_act AS DATE) = @fecha_act)
                  AND ale.estado_ale = 1
            END
        ELSE
        IF @tipoConsulta = 'IND'
            BEGIN
                SELECT
                    isnull(ale.codigo_ale, 0)                         AS codigo_ale
                  , isnull(ale.codigo_prv, 0)                         AS codigo_prv
                  , isnull(ale.orden_ale, 0)                          AS orden_ale
                  , isnull(ale.texto_ale, '')                         AS texto_ale
                  , isnull(ale.correcta_ale, 0)                       AS correcta_ale
                  , isnull(ale.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN ale.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, ale.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, ale.fecha_reg, 108) END AS fecha_reg
                  , isnull(ale.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN ale.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, ale.fecha_act, 103) + ' ' +
                             convert(VARCHAR, ale.fecha_act, 108) END AS fecha_act
                  , isnull(ale.estado_ale, 0)                         AS estado_ale
                FROM ADM_AlternativaEvaluacion ale WITH (NOLOCK)
                INNER JOIN ADM_PreguntaEvaluacion prv WITH(NOLOCK) ON ale.codigo_prv = prv.codigo_prv
                WHERE 1 = 1
                  AND prv.codigo_ind = @codigo_prv
                  AND ale.estado_ale = 1 AND prv.estado_prv = 1
            END
         ELSE
        IF @tipoConsulta = 'EVL'
            BEGIN
                SELECT DISTINCT
                    isnull(ale.codigo_ale, 0)                         AS codigo_ale
                  , isnull(ale.codigo_prv, 0)                         AS codigo_prv
                  , isnull(ale.orden_ale, 0)                          AS orden_ale
                  , isnull(ale.texto_ale, '')                         AS texto_ale
                  , isnull(ale.correcta_ale, 0)                       AS correcta_ale
                  , isnull(ale.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN ale.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, ale.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, ale.fecha_reg, 108) END AS fecha_reg
                  , isnull(ale.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN ale.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, ale.fecha_act, 103) + ' ' +
                             convert(VARCHAR, ale.fecha_act, 108) END AS fecha_act
                  , isnull(ale.estado_ale, 0)                         AS estado_ale
                FROM ADM_AlternativaEvaluacion ale WITH (NOLOCK)
                INNER JOIN ADM_PreguntaEvaluacion prv WITH(NOLOCK) ON ale.codigo_prv = prv.codigo_prv
                INNER JOIN ADM_EvaluacionDetalle evd WITH (NOLOCK) ON prv.codigo_prv = evd.codigo_prv
                WHERE 1 = 1
                  AND evd.codigo_evl = @codigo_prv
                  AND ale.estado_ale = 1 AND prv.estado_prv = 1 AND evd.estado_evd = 1
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

GRANT EXECUTE ON [dbo].[ADM_AlternativaEvaluacion_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_AlternativaEvaluacion_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_AlternativaEvaluacion_Listar] TO iusrvirtualsistema
GO
