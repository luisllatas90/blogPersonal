/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          28/10/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ReporteConsolidadoNivelacion
    @codigo_cco INT = 0
    , @codigo_evl INT = 0
AS
BEGIN
    BEGIN TRY

        SELECT
            evl.codigo_evl
          , alu.codigo_Alu
          , alu.codigoUniver_Alu
          , alu.nroDocIdent_Alu
          , alu.apellidoPat_Alu
          , alu.apellidoMat_Alu
          , alu.nombres_Alu
          , min.codigo_Min
          , min.nombre_Min
          , cpf.codigo_Cpf
          , cpf.nombre_Cpf
          , elu.nota_elu
          , elu.notaFinal_elu
          , elu.puntaje_elu
          , elu.puntajeFinal_elu
          , elu.condicion_ingreso_elu
          , CASE elu.condicion_ingreso_elu
                WHEN 'I' THEN 'INGRESANTE'
                WHEN 'A' THEN N'NO ALCANZÓ VACANTE'
                WHEN 'P' THEN 'NO INGRESANTE' END                           AS estadoIngreso
          , CASE WHEN pag_mat.codigo_Deu IS NULL THEN 'NO' ELSE 'SI' END    AS pago_matricula
          , ean.codigo_ean
          , CASE ean.necesita_nivelacion_ean WHEN 1 THEN 'SI' ELSE 'NO' END AS necesita_nivelacion
          , ean.nota_ean
          , com.codigo_com
          , com.nombre_com
          , ISNULL(com.nombre_corto_com, '')                                AS nombre_corto_com
          , dal.telefono_Dal
          , dal.telefonoMovil_Dal
          , alu.eMail_Alu
          , fam.telefonoFijo_fam
          , fam.telefonoCelular_fam
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN ADM_DatosEventoAdmision dea WITH (NOLOCK) ON evl.codigo_cco = dea.codigo_cco AND dea.estado_dea = 1
             JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON evl.codigo_evl = elu.codigo_evl AND elu.estado_elu = 1
             JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
             JOIN ModalidadIngreso MIN WITH (NOLOCK) ON alu.codigo_Min = MIN.codigo_Min
             JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             LEFT JOIN DatosAlumno dal WITH (NOLOCK) ON alu.codigo_Alu = dal.codigo_Alu
             LEFT JOIN ADM_Evaluacion_Alumno_Nivelacion ean WITH (NOLOCK)
                       ON elu.codigo_elu = ean.codigo_elu AND ean.estado_ean = 1
             LEFT JOIN CompetenciaAprendizaje com WITH (NOLOCK) ON ean.codigo_com = com.codigo_com
             OUTER APPLY (SELECT TOP 1
                              fam.apellidoPaterno_fam
                            , fam.apellidoMaterno_fam
                            , fam.nombres_fam
                            , fam.telefonoFijo_fam
                            , fam.telefonoCelular_fam
                          FROM FamiliarAlumno_ADM fal WITH (NOLOCK)
                               JOIN Familiar_ADM fam WITH (NOLOCK) ON fal.codigo_fam = fam.codigo_fam
                          WHERE fal.codigo_alu = alu.codigo_Alu
                            AND fal.estado_fal = 1
                          ORDER BY ISNULL(fam.indRespPago_fam, 0), fam.codigo_par) fam
             OUTER APPLY (SELECT TOP 1 _vst.codigo_Deu, _vst.fecha_Cin
                          FROM vstCajaIngresoMatricula_v2 _vst
                          WHERE _vst.codigo_alu = alu.codigo_alu
                            AND _vst.codigo_cac = dea.codigo_cac
                          ORDER BY _vst.fecha_Cin DESC) pag_mat
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

GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoNivelacion] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoNivelacion] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoNivelacion] TO iusrvirtualsistema
GO