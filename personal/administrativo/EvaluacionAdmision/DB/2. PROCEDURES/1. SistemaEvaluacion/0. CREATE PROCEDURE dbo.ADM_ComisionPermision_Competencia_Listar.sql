/*  Usuario Crea:   andy.diaz
    Fecha:          24/08/2020
    Descripción:    Listar de tabla ADM_ComisionPermanente_Competencia

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ComisionPermanente_Competencia_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_cpc VARCHAR(100) = ''
    , @codigo_cop INT = 0
    , @codigo_com INT = 0
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
                    isnull(cpc.codigo_cpc, 0)                         AS codigo_cpc
                  , isnull(cpc.codigo_cop, 0)                         AS codigo_cop
                  , isnull(cpc.codigo_com, 0)                         AS codigo_com
                  , isnull(cpc.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN cpc.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, cpc.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, cpc.fecha_reg, 108) END AS fecha_reg
                  , isnull(cpc.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN cpc.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, cpc.fecha_act, 103) + ' ' +
                             convert(VARCHAR, cpc.fecha_act, 108) END AS fecha_act
                  , isnull(cpc.estado_cpc, 0)                         AS estado_cpc
                FROM ADM_ComisionPermanente_Competencia cpc WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_cpc = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_cpc, ',')
                                                  WHERE item = cpc.codigo_cpc))
                  AND (@codigo_cop = 0 OR cpc.codigo_cop = @codigo_cop)
                  AND (@codigo_com = 0 OR cpc.codigo_com = @codigo_com)
                  AND (@codigo_per_reg = 0 OR cpc.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(cpc.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR cpc.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(cpc.fecha_act AS DATE) = @fecha_act)
                  AND cpc.estado_cpc = 1
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

GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_Competencia_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_Competencia_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_Competencia_Listar] TO iusrvirtualsistema
GO