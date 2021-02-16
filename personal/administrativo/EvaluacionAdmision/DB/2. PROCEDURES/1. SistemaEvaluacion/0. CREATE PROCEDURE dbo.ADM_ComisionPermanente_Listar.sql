/*  Usuario Crea:   andy.diaz
    Fecha:          23/08/2020
    Descripción:    Listar de tabla ADM_ComisionPermanente

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ComisionPermanente_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_cop VARCHAR(100) = ''
    , @codigo_per INT = 0
    , @codigo_ccm INT = 0
    , @nro_resolucion_cop VARCHAR(50) = ''
    , @vigente_cop BIT = NULL
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
                    isnull(cop.codigo_cop, 0)                         AS codigo_cop
                  , isnull(cop.codigo_per, 0)                         AS codigo_per
                  , isnull(cop.codigo_ccm, 0)                         AS codigo_ccm
                  , isnull(cop.nro_resolucion_cop, '')                AS nro_resolucion_cop
                  , isnull(cop.vigente_cop, 0)                        AS vigente_cop
                  , isnull(cop.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN cop.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, cop.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, cop.fecha_reg, 108) END AS fecha_reg
                  , isnull(cop.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN cop.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, cop.fecha_act, 103) + ' ' +
                             convert(VARCHAR, cop.fecha_act, 108) END AS fecha_act
                  , isnull(cop.estado_cop, 0)                         AS estado_cop
                FROM ADM_ComisionPermanente cop WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_cop = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_cop, ',')
                                                  WHERE item = cop.codigo_cop))
                  AND (@codigo_per = 0 OR cop.codigo_per = @codigo_per)
                  AND (@codigo_ccm = 0 OR cop.codigo_ccm = @codigo_ccm)
                  AND (@nro_resolucion_cop = '' OR isnull(cop.nro_resolucion_cop, '') LIKE @nro_resolucion_cop)
                  AND (@vigente_cop IS NULL OR cop.vigente_cop = @vigente_cop)
                  AND (@codigo_per_reg = 0 OR cop.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(cop.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR cop.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(cop.fecha_act AS DATE) = @fecha_act)
                  AND cop.estado_cop = 1
            END

        IF @tipoConsulta = 'PC' -- Datos personales y cargo
            BEGIN
                SELECT
                    isnull(cop.codigo_cop, 0)                         AS codigo_cop
                  , isnull(cop.codigo_per, 0)                         AS codigo_per
                  , isnull(cop.nro_resolucion_cop, '')                AS nro_resolucion_cop
                  , isnull(per.apellidoPat_Per, '')                   AS apellidoPat_Per
                  , isnull(per.apellidoMat_Per, '')                   AS apellidoMat_Per
                  , isnull(per.nombres_Per, '')                       AS nombres_Per
                  , isnull(per.apellidoPat_Per, '') + ' ' + isnull(per.apellidoMat_Per, '') + ' ' +
                    isnull(per.nombres_Per, '')                       AS nombreCompleto_Per
                  , isnull(cop.vigente_cop, 0)                        AS vigente_cop
                  , isnull(cop.codigo_per_reg, 0)                     AS codigo_per_reg
                  , isnull(ccm.codigo_ccm, 0)                         AS codigo_ccm
                  , isnull(ccm.nombre_ccm, '')                        AS nombre_ccm
                  , CASE
                        WHEN cop.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, cop.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, cop.fecha_reg, 108) END AS fecha_reg
                  , isnull(cop.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN cop.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, cop.fecha_act, 103) + ' ' +
                             convert(VARCHAR, cop.fecha_act, 108) END AS fecha_act
                  , isnull(cop.estado_cop, 0)                         AS estado_cop
                FROM ADM_ComisionPermanente cop WITH (NOLOCK)
                     JOIN Personal per WITH (NOLOCK) ON cop.codigo_per = per.codigo_Per
                     LEFT JOIN ADM_CargoComision ccm WITH (NOLOCK) ON cop.codigo_ccm = ccm.codigo_ccm
                WHERE 1 = 1
                  AND (@codigo_cop = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_cop, ',')
                                                  WHERE item = cop.codigo_cop))
                  AND (@codigo_per = 0 OR cop.codigo_per = @codigo_per)
                  AND (@codigo_ccm = 0 OR cop.codigo_ccm = @codigo_ccm)
                  AND (@nro_resolucion_cop = '' OR isnull(cop.nro_resolucion_cop, '') LIKE @nro_resolucion_cop)
                  AND (@vigente_cop IS NULL OR cop.vigente_cop = @vigente_cop)
                  AND (@codigo_per_reg = 0 OR cop.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(cop.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR cop.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(cop.fecha_act AS DATE) = @fecha_act)
                  AND cop.estado_cop = 1
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

GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_Listar] TO IusrReporting
--GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_Listar] TO iusrvirtualsistema
GO