/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          21/09/2018
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
    001
*/
CREATE PROCEDURE dbo.ADM_ReporteConsolidadoIngresantes
    @codigo_cac INT
AS
BEGIN
    BEGIN TRY
        DECLARE @codigo_test INT = 2
            , @codigo_sco_insc INT = 15 --ESCUELA PRE UNIVERSITARIA

        SELECT
            cpf.nombre_Cpf
          , min.nombre_Min
          , alu.codigoUniver_Alu
          , alu.apellidoPat_Alu
          , alu.apellidoMat_Alu
          , alu.nombres_Alu
          , alu.nroDocIdent_Alu
          , isnull(alu.eMail_Alu, '')                                         AS eMail_Alu
          , isnull(alu.email2_Alu, '')                                        AS email2_Alu
          , isnull(dal.direccion_Dal, '')                                     AS direccion_Dal
          , isnull(fam.apellidoPaterno_fam, '')                               AS apellidoPaterno_fam
          , isnull(fam.apellidoMaterno_fam, '')                               AS apellidoMaterno_fam
          , isnull(fam.nombres_fam, '')                                       AS nombres_fam
          , isnull(fam.telefonoFijo_fam, '')                                  AS telefonoFijo_fam
          , isnull(fam.telefonoCelular_fam, '')                               AS telefonoCelular_fam
          , isnull(dal.telefono_Dal, '')                                      AS telefono_Dal
          , isnull(dal.telefonoMovil_Dal, '')                                 AS telefonoMovil_Dal
          , CASE WHEN pag_insc.codigo_Deu IS NULL THEN 'NO' ELSE 'SI' END     AS pago_inscripcion
          , CASE WHEN pag_mat.codigo_Deu IS NULL THEN 'NO' ELSE 'SI' END      AS pago_matricula
          , isnull(dal.notaIngreso_Dal, 0)                                    AS notaIngreso_Dal
          , CASE alu.sexo_Alu
                WHEN 'M' THEN 'MASCULINO'
                WHEN 'F' THEN 'FEMENINO'
                ELSE '' END                                                   AS sexo_Alu
          , cco.descripcion_Cco
          , isnull(convert(VARCHAR, pag_mat.fecha_Cin, 103), '')              AS fecha_pago_mat
          , CASE
                WHEN mat.fecha_Mat IS NULL THEN ''
                ELSE convert(VARCHAR, mat.fecha_Mat, 103) END                 AS fecha_mat
          , isnull(ied.Nombre_ied, '')                                        AS Nombre_ied
          , isnull(dis.nombre_Dis, '')                                        AS nombre_Dis
          , isnull(dal.añoEgresoSec_Dal, '')                                  AS añoEgresoSec_Dal
          , CASE WHEN isnull(alu.foto_Alu, '0') = '1' THEN 'SI' ELSE 'NO' END AS foto_Alu
        FROM Alumno alu WITH (NOLOCK)
             JOIN CicloAcademico cac WITH (NOLOCK) ON alu.cicloIng_Alu = cac.descripcion_Cac
             JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             JOIN ModalidadIngreso min WITH (NOLOCK) ON alu.codigo_Min = min.codigo_Min
             JOIN CentroCostos cco WITH (NOLOCK) ON alu.codigo_cco = cco.codigo_Cco
             LEFT JOIN DatosAlumno dal WITH (NOLOCK) ON alu.codigo_Alu = dal.codigo_Alu
             LEFT JOIN InstitucionEducativa ied WITH (NOLOCK) ON dal.codigo_ied = ied.codigo_ied
             LEFT JOIN Distrito dis WITH (NOLOCK) ON ied.Ubigeo_dis = dis.ubigeo_Dis
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
                          ORDER BY isnull(fam.indRespPago_fam, 0), fam.codigo_par) fam
             OUTER APPLY (SELECT TOP 1 _vst.codigo_Deu
                          FROM vstDetalleCajaIngreso _vst (NOLOCK)
                               JOIN Deuda deu ON _vst.codigo_Deu = deu.codigo_Deu
                          WHERE 1 = 1
                            AND deu.estado_Deu = 'C'
                            AND deu.codigo_Sco = @codigo_sco_insc
                            AND _vst.codigo_Cac = cac.codigo_Cac) pag_insc
             OUTER APPLY (SELECT TOP 1 _vst.codigo_Deu, _vst.fecha_Cin
                          FROM vstCajaIngresoMatricula_v2 _vst
                          WHERE _vst.codigo_alu = alu.codigo_alu
                            AND _vst.codigo_cac = cac.codigo_cac
                          ORDER BY _vst.fecha_Cin DESC) pag_mat
             OUTER APPLY (SELECT TOP 1 _mat.codigo_Mat, _mat.fecha_Mat
                          FROM Matricula _mat WITH (NOLOCK)
                               JOIN DetalleMatricula _dma WITH (NOLOCK)
                                    ON _dma.codigo_Mat = _mat.codigo_mat AND _dma.estado_Dma <> 'R' --Retirado
                               JOIN CursoProgramado _cup WITH (NOLOCK) ON _dma.codigo_Cup = _cup.codigo_Cup
                          WHERE _mat.codigo_Alu = alu.codigo_Alu
                            AND _mat.codigo_Cac = cac.codigo_Cac
                            AND _mat.estado_Mat <> 'A'
                          ORDER BY _mat.fecha_Mat DESC) mat
        WHERE 1 = 1
          AND cac.codigo_Cac = @codigo_cac
          AND alu.codigo_test = @codigo_test
          AND isnull(alu.eliminado_Alu, 0) = 0
          AND alu.condicion_Alu = 'I'
          AND alu.alcanzo_vacante = 1
        ORDER BY cpf.nombre_Cpf, alu.apellidoPat_Alu, alu.apellidoMat_Alu, alu.nombres_Alu

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

GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoIngresantes] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoIngresantes] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteConsolidadoIngresantes] TO iusrvirtualsistema
GO