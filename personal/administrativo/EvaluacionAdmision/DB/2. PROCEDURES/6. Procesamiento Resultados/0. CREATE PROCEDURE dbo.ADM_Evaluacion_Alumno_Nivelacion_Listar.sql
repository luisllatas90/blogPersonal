/*  Usuario Crea:   andy.diaz
    Fecha:          14/10/2020
    Descripción:    Listar de tabla ADM_Evaluacion_Alumno_Nivelacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Evaluacion_Alumno_Nivelacion_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_ean VARCHAR(100) = ''
    , @codigo_elu INT = 0
    , @codigo_com INT = 0
    , @puntaje_ean NUMERIC(9, 3) = 0
    , @nota_ean NUMERIC(9, 3) = 0
    , @notaFinal_ean NUMERIC(9, 3) = 0
    , @estado_calificacion_ean CHAR(1) = ''
    , @necesita_nivelacion_ean BIT = NULL
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
                    ISNULL(ean.codigo_ean, 0)                         AS codigo_ean
                  , ISNULL(ean.codigo_elu, 0)                         AS codigo_elu
                  , ISNULL(ean.codigo_com, 0)                         AS codigo_com
                  , ISNULL(ean.puntaje_ean, 0)                        AS puntaje_ean
                  , ISNULL(ean.nota_ean, 0)                           AS nota_ean
                  , ISNULL(ean.notaFinal_ean, 0)                      AS notaFinal_ean
                  , ISNULL(ean.estado_calificacion_ean, '')           AS estado_calificacion_ean
                  , ISNULL(ean.necesita_nivelacion_ean, 0)            AS necesita_nivelacion_ean
                  , ISNULL(ean.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN ean.fecha_reg IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, ean.fecha_reg, 103) + ' ' +
                             CONVERT(VARCHAR, ean.fecha_reg, 108) END AS fecha_reg
                  , ISNULL(ean.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN ean.fecha_act IS NULL THEN ''
                        ELSE CONVERT(VARCHAR, ean.fecha_act, 103) + ' ' +
                             CONVERT(VARCHAR, ean.fecha_act, 108) END AS fecha_act
                  , ISNULL(ean.estado_ean, 0)                         AS estado_ean
                FROM ADM_Evaluacion_Alumno_Nivelacion ean WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_ean = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ean, ',')
                                                  WHERE item = ean.codigo_ean))
                  AND (@codigo_elu = 0 OR ean.codigo_elu = @codigo_elu)
                  AND (@codigo_com = 0 OR ean.codigo_com = @codigo_com)
                  AND (@puntaje_ean = 0 OR ean.puntaje_ean = @puntaje_ean)
                  AND (@nota_ean = 0 OR ean.nota_ean = @nota_ean)
                  AND (@notaFinal_ean = 0 OR ean.notaFinal_ean = @notaFinal_ean)
                  AND (@estado_calificacion_ean = '' OR ean.estado_calificacion_ean LIKE @estado_calificacion_ean)
                  AND (@necesita_nivelacion_ean IS NULL OR ean.necesita_nivelacion_ean = @necesita_nivelacion_ean)
                  AND (@codigo_per_reg = 0 OR ean.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR CAST(ean.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ean.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR CAST(ean.fecha_act AS DATE) = @fecha_act)
                  AND ean.estado_ean = 1
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

GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Nivelacion_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Nivelacion_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Nivelacion_Listar] TO iusrvirtualsistema
GO
