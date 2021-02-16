/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          06/10/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ReporteCuadroMaximoMinimoEventoAdmision
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
            cpf.nombre_cpf
          , elu.codigo_alu
          , elu.notaFinal_elu
          , elu.condicion_ingreso_elu
          , ISNULL(vae.cantidad_vae, 0) AS vacantes
          , ISNULL(cnm.nota_min_cnm, 0) AS nota_minima
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON evl.codigo_evl = elu.codigo_evl AND elu.estado_elu = 1
             JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
             JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             OUTER APPLY (SELECT TOP 1 _cnm.nota_min_cnm
                          FROM ADM_Configuracion_NotaMinima _cnm WITH (NOLOCK)
                          WHERE _cnm.codigo_cpf = cpf.codigo_Cpf
                            AND _cnm.codigo_cco = @codigo_cco
                            AND _cnm.estado_cnm = 1) cnm
             OUTER APPLY (SELECT _vae.cantidad_vae
                          FROM Vacantes _vac WITH (NOLOCK)
                               JOIN ADM_VacantesEvento _vae WITH (NOLOCK)
                                    ON _vac.codigo_Vac = _vae.codigo_vac AND _vae.codigo_cco = @codigo_cco
                                        AND _vae.estado_vae = 1
                          WHERE _vac.codigo_cac = @codigo_cac
                            AND _vac.codigo_cpf = cpf.codigo_Cpf
                            AND _vac.codigo_min = alu.codigo_Min
                            AND _vac.estado_vac = 1) vae
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

GRANT EXECUTE ON [dbo].[ADM_ReporteCuadroMaximoMinimoEventoAdmision] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteCuadroMaximoMinimoEventoAdmision] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteCuadroMaximoMinimoEventoAdmision] TO iusrvirtualsistema
GO