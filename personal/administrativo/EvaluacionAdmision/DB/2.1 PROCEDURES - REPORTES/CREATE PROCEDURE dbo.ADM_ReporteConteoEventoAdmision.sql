/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          19/10/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ReporteConteoEventoAdmision
    @codigo_cco INT
    , @codigo_evl INT = 0
AS
BEGIN
    BEGIN TRY

        DECLARE @codigo_cac INT = 0

        SELECT @codigo_cac = dae.codigo_cac
        FROM ADM_DatosEventoAdmision dae WITH (NOLOCK)
        WHERE dae.codigo_cco = @codigo_cco
          AND dae.estado_dea = 1

        SELECT DISTINCT
            cpf.codigo_Cpf
          , cpf.nombre_cpf
          , elu.codigo_alu
          , elu.notaFinal_elu
          , elu.condicion_ingreso_elu
          , ISNULL(cnm.codigo_cnm, 0)              AS codigo_cnm
          , ISNULL(cnm.nota_min_cnm, 0)            AS nota_minima
          , ISNULL(com.codigo_com, 0)              AS codigo_com
          , ISNULL(com.nombre_com, '')             AS nombre_com_niv
          , ISNULL(ean.necesita_nivelacion_ean, 0) AS necesita_nivelacion
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON evl.codigo_evl = elu.codigo_evl AND elu.estado_elu = 1
             JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
             JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             LEFT JOIN ADM_Evaluacion_Alumno_Nivelacion ean WITH (NOLOCK)
                       ON elu.codigo_elu = ean.codigo_elu AND ean.estado_ean = 1
             LEFT JOIN CompetenciaAprendizaje com WITH (NOLOCK) ON ean.codigo_com = com.codigo_com
             OUTER APPLY (SELECT TOP 1 _cnm.codigo_cnm, _cnm.nota_min_cnm
                          FROM ADM_Configuracion_NotaMinima _cnm WITH (NOLOCK)
                          WHERE _cnm.codigo_cpf = cpf.codigo_Cpf
                            AND _cnm.codigo_cco = @codigo_cco
                            AND _cnm.estado_cnm = 1) cnm
        WHERE 1 = 1
          AND evl.codigo_cco = @codigo_cco
          AND (@codigo_evl = 0 OR evl.codigo_evl = @codigo_evl)
          AND evl.estado_evl = 1
        ORDER BY cpf.nombre_Cpf

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

GRANT EXECUTE ON [dbo].[ADM_ReporteConteoEventoAdmision] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteConteoEventoAdmision] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteConteoEventoAdmision] TO iusrvirtualsistema
GO