/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          27/10/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ReporteConsolidadoNotasPorEscuela
    @codigo_test INT
    , @pago_matricula INT = 0
    , @codigo_cac_desde INT = 0
    , @codigo_cac_hasta INT = 0
AS
BEGIN
    BEGIN TRY

        DECLARE @fecha_desde SMALLDATETIME, @fecha_hasta SMALLDATETIME

        SELECT @fecha_desde = fechaIni_Cac
        FROM CicloAcademico
        WHERE codigo_Cac = @codigo_cac_desde;

        SELECT @fecha_hasta = fechaFin_Cac
        FROM CicloAcademico
        WHERE codigo_Cac = @codigo_cac_hasta;

        SELECT
            cpf.codigo_Cpf
          , cpf.nombre_Cpf
          , cac.codigo_Cac
          , cac.descripcion_Cac
          , elu.puntaje_elu
          , elu.nota_elu
          , CASE WHEN pag_mat.codigo_Deu IS NULL THEN 0 ELSE 1 END AS pago_matricula
        FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
             JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
             JOIN ADM_DatosEventoAdmision dea WITH (NOLOCK) ON evl.codigo_cco = dea.codigo_cco AND dea.estado_dea = 1
             JOIN CicloAcademico cac WITH (NOLOCK) ON dea.codigo_cac = cac.codigo_Cac
             JOIN Alumno alu ON elu.codigo_alu = alu.codigo_Alu
             JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             OUTER APPLY (SELECT TOP 1 _vst.codigo_Deu, _vst.fecha_Cin
                          FROM vstCajaIngresoMatricula_v2 _vst
                          WHERE _vst.codigo_alu = alu.codigo_alu
                            AND _vst.codigo_cac = cac.codigo_cac
                          ORDER BY _vst.fecha_Cin DESC) pag_mat
        WHERE 1 = 1
          AND elu.estado_elu = 1
          AND alu.codigo_test = @codigo_test
          AND (@pago_matricula = 0 OR (@pago_matricula = 1 AND pag_mat.codigo_Deu IS NOT NULL)
            OR (@pago_matricula = -1 AND pag_mat.codigo_Deu IS NULL))
          AND (@fecha_desde IS NULL OR cac.fechaIni_Cac >= @fecha_desde)
          AND (@fecha_hasta IS NULL OR cac.fechaFin_Cac <= @fecha_hasta)
          AND elu.condicion_ingreso_elu = 'I'
        ORDER BY cac.fechaIni_Cac DESC
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

GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoNotasPorEscuela] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoNotasPorEscuela] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoNotasPorEscuela] TO iusrvirtualsistema
GO