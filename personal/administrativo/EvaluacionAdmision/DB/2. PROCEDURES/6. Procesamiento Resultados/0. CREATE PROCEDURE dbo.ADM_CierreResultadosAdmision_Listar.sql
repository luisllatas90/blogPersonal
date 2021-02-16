IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.ROUTINES
          WHERE ROUTINE_NAME = 'ADM_CierreResultadosAdmision_Listar'
            AND SPECIFIC_SCHEMA = 'dbo')
    DROP PROCEDURE ADM_CierreResultadosAdmision_Listar;
GO
/*  Usuario Crea:   andy.diaz
    Fecha:          26/10/2020
    Descripción:    Listar de tabla ADM_CierreResultadosAdmision

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_CierreResultadosAdmision_Listar
    @tipoConsulta VARCHAR(5) = 'GEN'
    , @codigo_cra VARCHAR(100) = ''
    , @codigo_cac INT = 0
    , @codigo_min TINYINT = 0
    , @codigo_cpf INT = 0
    , @codigo_cco INT = 0
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
                    isnull(cra.codigo_cra, 0)                         AS codigo_cra
                  , isnull(cra.codigo_cac, 0)                         AS codigo_cac
                  , isnull(cra.codigo_min, 0)                         AS codigo_min
                  , isnull(cra.codigo_cpf, 0)                         AS codigo_cpf
                  , isnull(cra.codigo_cco, 0)                         AS codigo_cco
                  , isnull(cra.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN cra.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, cra.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, cra.fecha_reg, 108) END AS fecha_reg
                  , isnull(cra.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN cra.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, cra.fecha_act, 103) + ' ' +
                             convert(VARCHAR, cra.fecha_act, 108) END AS fecha_act
                  , isnull(cra.estado_cra, 0)                         AS estado_cra
                FROM ADM_CierreResultadosAdmision cra WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_cra = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_cra, ',')
                                                  WHERE item = cra.codigo_cra))
                  AND (@codigo_cac = 0 OR cra.codigo_cac = @codigo_cac)
                  AND (@codigo_min = 0 OR cra.codigo_min = @codigo_min)
                  AND (@codigo_cpf = 0 OR cra.codigo_cpf = @codigo_cpf)
                  AND (@codigo_cco = 0 OR cra.codigo_cco = @codigo_cco)
                  AND (@codigo_per_reg = 0 OR cra.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(cra.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR cra.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(cra.fecha_act AS DATE) = @fecha_act)
                  AND cra.estado_cra = 1
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

GRANT EXECUTE ON [dbo].[ADM_CierreResultadosAdmision_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_CierreResultadosAdmision_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_CierreResultadosAdmision_Listar] TO iusrvirtualsistema
GO
