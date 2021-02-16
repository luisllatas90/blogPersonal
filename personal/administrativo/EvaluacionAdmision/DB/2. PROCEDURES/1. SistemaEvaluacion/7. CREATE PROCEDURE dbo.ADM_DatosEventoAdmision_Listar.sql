/*  Usuario Crea:   andy.diaz
    Fecha:          07/10/2020
    Descripción:    Listar de tabla ADM_DatosEventoAdmision

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_DatosEventoAdmision_Listar
    @tipoConsulta VARCHAR(5) = 'GEN'
    , @codigo_dea VARCHAR(100) = ''
    , @codigo_cco INT = 0
    , @codigo_cac INT = 0
    , @fechaEvento_dea DATE = NULL
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Filtros adicionales
    , @codigos_cco VARCHAR(MAX) = ''
    , @codigos_cac VARCHAR(MAX) = ''
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    isnull(dea.codigo_dea, 0)                         AS codigo_dea
                  , isnull(dea.codigo_cco, 0)                         AS codigo_cco
                  , isnull(dea.codigo_cac, 0)                         AS codigo_cac
                  , isnull(dea.fechaEvento_dea, '')                   AS fechaEvento_dea
                  , isnull(dea.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN dea.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, dea.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, dea.fecha_reg, 108) END AS fecha_reg
                  , isnull(dea.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN dea.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, dea.fecha_act, 103) + ' ' +
                             convert(VARCHAR, dea.fecha_act, 108) END AS fecha_act
                  , isnull(dea.estado_dea, 0)                         AS estado_dea
                FROM ADM_DatosEventoAdmision dea WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_dea = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_dea, ',')
                                                  WHERE item = dea.codigo_dea))
                  AND (@codigo_cco = 0 OR dea.codigo_cco = @codigo_cco)
                  AND (@codigo_cac = 0 OR dea.codigo_cac = @codigo_cac)
                  AND (@fechaEvento_dea IS NULL OR cast(dea.fechaEvento_dea AS DATE) = @fechaEvento_dea)
                  AND (@codigo_per_reg = 0 OR dea.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(dea.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR dea.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(dea.fecha_act AS DATE) = @fecha_act)
                  AND dea.estado_dea = 1
            END

        IF @tipoConsulta = 'CECO'
            BEGIN
                SELECT cco.codigo_Cco, cco.descripcion_Cco
                FROM ADM_DatosEventoAdmision dea WITH (NOLOCK)
                     JOIN CentroCostos cco WITH (NOLOCK) ON dea.codigo_cco = cco.codigo_Cco
                     JOIN CicloAcademico cac WITH (NOLOCK) ON dea.codigo_cac = cac.codigo_Cac
                WHERE 1 = 1
                  AND (@codigos_cco = '' OR cco.codigo_Cco IN (SELECT item
                                                               FROM fnSplit2(@codigos_cco, ',')))
                  AND (@codigos_cac = '' OR dea.codigo_cac IN (SELECT item
                                                               FROM fnSplit2(@codigos_cac, ',')))
                  AND dea.estado_dea = 1
                ORDER BY dea.fechaEvento_dea DESC
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

GRANT EXECUTE ON [dbo].[ADM_DatosEventoAdmision_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_DatosEventoAdmision_Listar] TO IusrReporting
GRANT EXECUTE ON [dbo].[ADM_DatosEventoAdmision_Listar] TO iusrvirtualsistema
GO
