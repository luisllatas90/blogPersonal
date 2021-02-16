/*  Usuario Crea:   andy.diaz
    Fecha:          02/09/2020
    Descripción:    Listar de tabla ADM_NivelComplejidadPregunta

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_NivelComplejidadPregunta_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_ncp VARCHAR(100) = ''
    , @nombre_ncp NVARCHAR(50) = ''
    , @abreviatura_ncp CHAR(1) = ''
    , @descripcion_ncp NVARCHAR(250) = ''
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
                    isnull(ncp.codigo_ncp, 0) AS codigo_ncp
                  , isnull(ncp.nombre_ncp, '') AS nombre_ncp
                  , isnull(ncp.abreviatura_ncp, '') AS abreviatura_ncp
                  , isnull(ncp.descripcion_ncp, '') AS descripcion_ncp
                  , isnull(ncp.codigo_per_reg, 0) AS codigo_per_reg
                  , CASE
                        WHEN ncp.fecha_reg IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, ncp.fecha_reg, 103) + ' ' +
                             CONVERT(VARCHAR, ncp.fecha_reg, 108) END AS fecha_reg
                  , isnull(ncp.codigo_per_act, 0) AS codigo_per_act
                  , CASE
                        WHEN ncp.fecha_act IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, ncp.fecha_act, 103) + ' ' +
                             CONVERT(VARCHAR, ncp.fecha_act, 108) END AS fecha_act
                  , isnull(ncp.estado_ncp, 0) AS estado_ncp
                FROM ADM_NivelComplejidadPregunta ncp WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_ncp = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ncp, ',')
                                                  WHERE item = ncp.codigo_ncp))
                  AND (@nombre_ncp = '' OR ncp.nombre_ncp LIKE @nombre_ncp)
                  AND (@abreviatura_ncp = '' OR ncp.abreviatura_ncp LIKE @abreviatura_ncp)
                  AND (@descripcion_ncp = '' OR ncp.descripcion_ncp LIKE @descripcion_ncp)
                  AND (@codigo_per_reg = 0 OR ncp.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(ncp.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ncp.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(ncp.fecha_act AS DATE) = @fecha_act)
                  AND ncp.estado_ncp = 1
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

GRANT EXECUTE ON [dbo].[ADM_NivelComplejidadPregunta_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_NivelComplejidadPregunta_Listar] TO IusrReporting
--GRANT EXECUTE ON [dbo].[ADM_NivelComplejidadPregunta_Listar] TO iusrvirtualsistema
GO
