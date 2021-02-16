/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          21/09/2018
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
    001
*/
CREATE PROCEDURE dbo.ADM_ReporteDiagnosticoRespuestas
    @codigo_evl INT = 0
    , @codigo_cco INT = 0
AS
BEGIN
    BEGIN TRY
        IF OBJECT_ID('tempdb..#pregunta_competencia') IS NOT NULL
            DROP TABLE #pregunta_competencia

        CREATE TABLE #pregunta_competencia (
            codigo_evd INT,
            codigo_com INT
        )

        IF OBJECT_ID('tempdb..#postulaciones') IS NOT NULL
            DROP TABLE #postulaciones

        CREATE TABLE #postulaciones (
            codigo_alu INT,
            postulaciones INT
        )

        INSERT INTO #pregunta_competencia
        SELECT evd.codigo_evd, com.codigo_com
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN ADM_EvaluacionDetalle evd WITH (NOLOCK) ON evl.codigo_evl = evd.codigo_evl AND evd.estado_evd = 1
             JOIN ADM_PreguntaEvaluacion prv WITH (NOLOCK) ON evd.codigo_prv = prv.codigo_prv
             JOIN ADM_Indicador ind WITH (NOLOCK) ON prv.codigo_ind = ind.codigo_ind
             JOIN ADM_SubCompetencia scom WITH (NOLOCK) ON ind.codigo_scom = scom.codigo_scom
             JOIN CompetenciaAprendizaje com WITH (NOLOCK) ON scom.codigo_com = com.codigo_com
        WHERE 1 = 1
          AND (@codigo_evl = 0 OR evl.codigo_evl = @codigo_evl)
          AND (@codigo_cco = 0 OR evl.codigo_cco = @codigo_cco)
        ORDER BY evd.orden_evd

        INSERT INTO #postulaciones
        SELECT alu.codigo_Alu, COUNT(DISTINCT post.codigo_Alu)
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON evl.codigo_evl = elu.codigo_evl
             JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
             JOIN Alumno post WITH (NOLOCK)
                  ON alu.nroDocIdent_Alu = post.nroDocIdent_Alu AND ISNULL(post.eliminado_Alu, 0) = 0
        WHERE 1 = 1
          AND (@codigo_evl = 0 OR evl.codigo_evl = @codigo_evl)
          AND (@codigo_cco = 0 OR evl.codigo_cco = @codigo_cco)
          AND evl.estado_evl = 1
        GROUP BY alu.codigo_Alu

        SELECT
            elu.codigo_evl
          , alu.codigo_Alu
          , alu.nroDocIdent_Alu
          , alu.codigoUniver_Alu
          , alu.apellidoPat_Alu
          , alu.apellidoMat_Alu
          , alu.nombres_Alu
          , cpf.codigo_Cpf
          , cpf.nombre_Cpf
          , com.codigo_com
          , com.nombre_com
          , ear.codigo_ear
          , ear.orden_evd
          , ear.correcta_ear
          , ean.puntaje_ean
          , ean.notaFinal_ean
          , elu.puntaje_elu
          , elu.notaFinal_elu
          , CASE elu.condicion_ingreso_elu
                WHEN 'P' THEN 'NO INGRESANTE'
                WHEN 'A' THEN N'NO ALCANZÓ VACANTE'
                WHEN 'I' THEN 'INGRESANTE' END AS estado
          , min.nombre_Min
          , post.postulaciones
        FROM #pregunta_competencia pc WITH (NOLOCK)
             JOIN CompetenciaAprendizaje com WITH (NOLOCK) ON com.codigo_com = pc.codigo_com
             LEFT JOIN ADM_Evaluacion_Alumno_Respuesta ear WITH (NOLOCK)
                       ON ear.codigo_evd = pc.codigo_evd AND ear.estado_ear = 1
             LEFT JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON ear.codigo_elu = elu.codigo_elu
             LEFT JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
             LEFT JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             LEFT JOIN ADM_Evaluacion_Alumno_Nivelacion ean WITH (NOLOCK)
                       ON com.codigo_com = ean.codigo_com AND elu.codigo_elu = ean.codigo_elu AND ean.estado_ean = 1
             LEFT JOIN ModalidadIngreso min WITH (NOLOCK) ON alu.codigo_Min = min.codigo_Min
             LEFT JOIN #postulaciones post WITH (NOLOCK) ON alu.codigo_Alu = post.codigo_alu
        ORDER BY cpf.nombre_Cpf, alu.apellidoPat_Alu, alu.apellidoMat_Alu, alu.nombres_Alu, ear.orden_evd
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

GRANT EXECUTE ON [dbo].[ADM_ReporteDiagnosticoRespuestas] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteDiagnosticoRespuestas] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteDiagnosticoRespuestas] TO iusrvirtualsistema
GO