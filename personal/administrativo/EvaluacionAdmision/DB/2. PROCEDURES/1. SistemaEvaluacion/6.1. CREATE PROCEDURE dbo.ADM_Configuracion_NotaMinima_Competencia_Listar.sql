/*  Usuario Crea:   andy.diaz
    Fecha:          28/09/2020
    Descripción:    Listar de tabla ADM_Configuracion_NotaMinima_Competencia

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Configuracion_NotaMinima_Competencia_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_cnc VARCHAR(100) = ''
    , @codigo_cnm INT = 0
    , @codigo_com INT = 0
    , @nota_min_cnc INT = 0
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Adicionales
    , @codigo_cpf INT = 0
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    ISNULL(cnc.codigo_cnc, 0)                         AS codigo_cnc
                  , ISNULL(cnc.codigo_cnm, 0)                         AS codigo_cnm
                  , ISNULL(cnc.codigo_com, 0)                         AS codigo_com
                  , ISNULL(cnc.nota_min_cnc, 0)                       AS nota_min_cnc
                  , ISNULL(cnc.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN cnc.fecha_reg IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, cnc.fecha_reg, 103) + ' ' +
                             CONVERT(VARCHAR, cnc.fecha_reg, 108) END AS fecha_reg
                  , ISNULL(cnc.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN cnc.fecha_act IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, cnc.fecha_act, 103) + ' ' +
                             CONVERT(VARCHAR, cnc.fecha_act, 108) END AS fecha_act
                  , ISNULL(cnc.estado_cnc, 0)                         AS estado_cnc
                FROM ADM_Configuracion_NotaMinima_Competencia cnc WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_cnc = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_cnc, ',')
                                                  WHERE item = cnc.codigo_cnc))
                  AND (@codigo_cnm = 0 OR cnc.codigo_cnm = @codigo_cnm)
                  AND (@codigo_com = 0 OR cnc.codigo_com = @codigo_com)
                  AND (@nota_min_cnc = 0 OR cnc.nota_min_cnc = @nota_min_cnc)
                  AND (@codigo_per_reg = 0 OR cnc.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR CAST(cnc.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR cnc.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR CAST(cnc.fecha_act AS DATE) = @fecha_act)
                  AND cnc.estado_cnc = 1
            END

        IF @tipoConsulta = 'LC' -- Listar por competencias
            BEGIN
                SELECT DISTINCT
                    com.codigo_com
                  , com.nombre_com
                  , ISNULL(com.nombre_corto_com, '')                 AS nombre_corto_com
                  , ISNULL(dat.codigo_cnc, 0)                        AS codigo_cnc
                  , ISNULL(CAST(dat.nota_min_cnc AS VARCHAR(5)), '') AS nota_min_cnc
                  , ISNULL(dat.codigo_cnm, 0)                        AS codigo_cnm
                  , ISNULL(CAST(dat.nota_min_cnm AS VARCHAR(5)), '') AS nota_min_cnm
                FROM dbo.CompetenciaAprendizaje (NOLOCK) com
                     JOIN PerfilIngreso ping WITH (NOLOCK)
                          ON com.codigo_com = ping.codigo_com AND ping.estado_pIng = 1
                     JOIN PlanCurricular pcur WITH (NOLOCK)
                          ON ping.codigo_pcur = pcur.codigo_pcur AND pcur.vigente_pcur = 1
                              AND pcur.codigo_cpf = @codigo_cpf
                     OUTER APPLY (SELECT TOP 1 _cnm.codigo_cnm, _cnm.nota_min_cnm, _cnc.codigo_cnc, _cnc.nota_min_cnc
                                  FROM ADM_Configuracion_NotaMinima _cnm WITH (NOLOCK)
                                       JOIN ADM_Configuracion_NotaMinima_Competencia _cnc WITH (NOLOCK)
                                            ON _cnm.codigo_cnm = _cnc.codigo_cnm AND _cnc.estado_cnc = 1
                                  WHERE 1 = 1
                                    AND (@codigo_cnm = 0 OR _cnm.codigo_cnm = @codigo_cnm)
                                    AND _cnc.codigo_com = com.codigo_com) dat
                WHERE com.codigo_tcom = 1
                  AND com.codigo_cat = 1
                  AND com.estado_com = 1
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

GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Competencia_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Competencia_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Competencia_Listar] TO iusrvirtualsistema
GO
