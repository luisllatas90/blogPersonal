/*  Usuario Crea:   andy.diaz
    Fecha:          28/09/2020
    Descripción:    Listar de tabla ADM_Configuracion_NotaMinima

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Configuracion_NotaMinima_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_cnm VARCHAR(100) = ''
    , @codigo_cpf INT = 0
    , @codigo_cco INT = 0
    , @nota_min_cnm NUMERIC(9, 2) = 0.00
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Filtros adicionales
    , @codigo_test INT = 0
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    ISNULL(cnm.codigo_cnm, 0)                         AS codigo_cnm
                  , ISNULL(cnm.codigo_cpf, 0)                         AS codigo_cpf
                  , ISNULL(cnm.codigo_cco, 0)                         AS codigo_cco
                  , ISNULL(cnm.nota_min_cnm, 0.00)                    AS nota_min_cnm
                  , ISNULL(cnm.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN cnm.fecha_reg IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, cnm.fecha_reg, 103) + ' ' +
                             CONVERT(VARCHAR, cnm.fecha_reg, 108) END AS fecha_reg
                  , ISNULL(cnm.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN cnm.fecha_act IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, cnm.fecha_act, 103) + ' ' +
                             CONVERT(VARCHAR, cnm.fecha_act, 108) END AS fecha_act
                  , ISNULL(cnm.estado_cnm, 0)                         AS estado_cnm
                FROM ADM_Configuracion_NotaMinima cnm WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_cnm = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_cnm, ',')
                                                  WHERE item = cnm.codigo_cnm))
                  AND (@codigo_cpf = 0 OR cnm.codigo_cpf = @codigo_cpf)
                  AND (@codigo_cco = 0 OR cnm.codigo_cco = @codigo_cco)
                  AND (@nota_min_cnm = 0.00 OR cnm.nota_min_cnm = @nota_min_cnm)
                  AND (@codigo_per_reg = 0 OR cnm.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR CAST(cnm.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR cnm.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR CAST(cnm.fecha_act AS DATE) = @fecha_act)
                  AND cnm.estado_cnm = 1
            END

        IF @tipoConsulta = 'LC' -- Lista de Carreras para configuración
            BEGIN
                SELECT
                    cpf.codigo_Cpf
                  , cpf.nombre_Cpf
                  , fac.codigo_Fac
                  , fac.nombre_Fac
                  , ISNULL(cco.codigo_Cco, 0)                        AS codigo_cco
                  , ISNULL(cco.descripcion_Cco, '')                  AS descripcion_cco
                  , ISNULL(cnm.codigo_cnm, 0)                        AS codigo_cnm
                  , ISNULL(CAST(cnm.nota_min_cnm AS VARCHAR(5)), '') AS nota_min_cnm
                  , ISNULL(dat.notasPorCompetencia, '')              AS notasPorCompetencia
                FROM CarreraProfesional cpf WITH (NOLOCK)
                     JOIN Facultad fac WITH (NOLOCK) ON cpf.codigo_Fac = fac.codigo_Fac
                     LEFT JOIN ADM_Configuracion_NotaMinima cnm WITH (NOLOCK)
                               ON cpf.codigo_Cpf = cnm.codigo_cpf
                                   AND (@codigo_cco = 0 OR cnm.codigo_cco = @codigo_cco) AND cnm.estado_cnm = 1
                     LEFT JOIN CentroCostos cco WITH (NOLOCK) ON cnm.codigo_cco = cco.codigo_Cco
                     OUTER APPLY (SELECT
                                      STUFF((SELECT
                                                     '|' + com.nombre_corto_com + '=' +
                                                     CAST(_cnc.nota_min_cnc AS VARCHAR(5))
                                             FROM ADM_Configuracion_NotaMinima_Competencia _cnc WITH (NOLOCK)
                                                  JOIN CompetenciaAprendizaje com WITH (NOLOCK)
                                                       ON _cnc.codigo_com = com.codigo_com AND _cnc.estado_cnc = 1
                                             WHERE _cnc.codigo_cnm = cnm.codigo_cnm
                                             FOR XML PATH('')), 1, 1, '') AS notasPorCompetencia
                ) dat
                WHERE 1 = 1
                  AND (@codigo_test = 0 OR cpf.codigo_test = @codigo_test)
                  AND (@codigo_cpf = 0 OR cpf.codigo_Cpf = @codigo_cpf)
                  AND cpf.vigencia_Cpf = 1
                  AND cpf.eliminado_cpf = 0
                  AND cpf.codigo_Cpf NOT IN (407, 35, 30, 38)
                ORDER BY fac.nombre_Fac, cpf.nombre_Cpf
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

GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Listar] TO iusrvirtualsistema
GO