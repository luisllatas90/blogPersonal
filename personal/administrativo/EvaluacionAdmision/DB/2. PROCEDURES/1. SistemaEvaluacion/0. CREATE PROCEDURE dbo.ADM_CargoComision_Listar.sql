/*  Usuario Crea:   andy.diaz
    Fecha:          01/09/2020
    Descripción:    Listar de tabla ADM_CargoComision

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_CargoComision_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_ccm VARCHAR(100) = ''
    , @nombre_ccm NVARCHAR(50) = ''
    , @descripcion_ccm NVARCHAR(250) = ''
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
                    isnull(ccm.codigo_ccm, 0)                         AS codigo_ccm
                  , isnull(ccm.nombre_ccm, '')                        AS nombre_ccm
                  , isnull(ccm.descripcion_ccm, '')                   AS descripcion_ccm
                  , isnull(ccm.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN ccm.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, ccm.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, ccm.fecha_reg, 108) END AS fecha_reg
                  , isnull(ccm.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN ccm.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, ccm.fecha_act, 103) + ' ' +
                             convert(VARCHAR, ccm.fecha_act, 108) END AS fecha_act
                  , isnull(ccm.estado_ccm, 0)                         AS estado_ccm
                FROM ADM_CargoComision ccm WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_ccm = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ccm, ',')
                                                  WHERE item = ccm.codigo_ccm))
                  AND (@nombre_ccm = '' OR ccm.nombre_ccm LIKE @nombre_ccm)
                  AND (@descripcion_ccm = '' OR ccm.descripcion_ccm LIKE @descripcion_ccm)
                  AND (@codigo_per_reg = 0 OR ccm.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(ccm.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ccm.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(ccm.fecha_act AS DATE) = @fecha_act)
                  AND ccm.estado_ccm = 1
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

GRANT EXECUTE ON [dbo].[ADM_CargoComision_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_CargoComision_Listar] TO IusrReporting
--GRANT EXECUTE ON [dbo].[ADM_CargoComision_Listar] TO iusrvirtualsistema
GO
