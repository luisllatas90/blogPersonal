/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          08/09/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ReportePostulantesPorAula
    @codigo_test INT = 0
    , @codigo_cac INT = 0
    , @codigo_cco VARCHAR(MAX) = ''
    , @codigo_amb VARCHAR(MAX) = ''
    , @codigo_min VARCHAR(MAX) = ''
AS
BEGIN
    BEGIN TRY

        IF @codigo_test = 1
            SET @codigo_test = 2 --ESCUELA PRE -> PREGRADO

        SELECT DISTINCT
            alu.nroDocIdent_Alu
          , alu.apellidoPat_Alu
          , alu.apellidoMat_Alu
          , alu.nombres_Alu
          , cpf.nombre_Cpf
          , isnull(deu.SaldoTotal, 0) AS deuda
          , dal.telefonoCasa_Dal
          , dal.telefonoMovil_Dal
          , amb.descripcion_Amb
          , min.nombre_Min
        FROM ADM_GrupoAdmisionVirtual gru WITH (NOLOCK)
             JOIN Ambiente amb WITH (NOLOCK) ON gru.codigo_amb = amb.codigo_Amb
             JOIN ADM_GrupoAdmisionVirtual_Alumno gva WITH (NOLOCK) ON gru.codigo_gru = gva.codigo_gru
             JOIN Alumno alu WITH (NOLOCK) ON gva.codigo_alu = alu.codigo_Alu
             JOIN CicloAcademico cac WITH (NOLOCK) ON cac.descripcion_Cac = alu.cicloIng_Alu
             JOIN ModalidadIngreso min WITH (NOLOCK) ON alu.codigo_Min = min.codigo_Min
             JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
             JOIN DatosAlumno dal WITH (NOLOCK) ON alu.codigo_Alu = dal.codigo_Alu
             OUTER APPLY (SELECT
                              d2.codigo_Pso
                            , SUM(d2.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0))                AS CargoTotal
                            , SUM(d2.montoTotal_Deu - d2.saldo_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS AbonoTotal
                            , SUM(d2.Saldo_Deu)                                                           AS SaldoTotal
                          FROM dbo.Deuda d2 WITH (NOLOCK)
                               LEFT JOIN DetalleCajaIngreso cin WITH (NOLOCK)
                                         ON cin.codigo_Deu = d2.codigo_deu AND cin.codigo_mno = 4
                          WHERE d2.codigo_Pso = alu.codigo_Pso
                            AND d2.codigo_cco = alu.codigo_cco
                            AND d2.estado_Deu <> 'A'
                          GROUP BY d2.codigo_Pso) deu
        WHERE 1 = 1
          AND (@codigo_test = 0 OR alu.codigo_test = @codigo_test)
          AND (@codigo_cac = 0 OR cac.codigo_Cac = @codigo_cac)
          AND (@codigo_cco = '' OR alu.codigo_cco IN (SELECT item
                                                      FROM dbo.fnSplit2(@codigo_cco, ',')))
          AND (@codigo_amb = '' OR amb.codigo_Amb IN (SELECT item
                                                      FROM dbo.fnSplit2(@codigo_amb, ',')))
          AND (@codigo_min = '' OR alu.codigo_Min IN (SELECT item
                                                      FROM dbo.fnSplit2(@codigo_min, ',')))
          AND isnull(alu.eliminado_Alu, 0) = 0
        ORDER BY amb.descripcion_Amb, alu.apellidoPat_Alu, alu.apellidoMat_Alu, alu.nombres_Alu

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