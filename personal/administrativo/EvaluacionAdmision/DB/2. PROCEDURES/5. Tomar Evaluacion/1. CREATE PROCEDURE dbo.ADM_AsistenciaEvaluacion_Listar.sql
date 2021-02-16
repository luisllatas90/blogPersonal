/*  Usuario Crea:   andy.diaz
    Fecha:          11/09/2020
    Descripción:    Listar de tabla ADM_AsistenciaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_AsistenciaEvaluacion_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_ase VARCHAR(100) = ''
    , @codigo_gru INT = 0
    , @codigo_alu INT = 0
    , @estadoAsistencia_ase CHAR(1) = ''
    , @fechaCierre_ase DATETIME = NULL
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Filtros adicionales
    , @codigo_cco INT = 0
    , @codigo_tge INT = 0
AS
BEGIN
    BEGIN TRY

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    isnull(ase.codigo_ase, 0)                               AS codigo_ase
                  , isnull(ase.codigo_gru, 0)                               AS codigo_gru
                  , isnull(ase.codigo_alu, 0)                               AS codigo_alu
                  , isnull(ase.estadoAsistencia_ase, '')                    AS estadoAsistencia_ase
                  , CASE
                        WHEN ase.fechaCierre_ase IS NULL THEN ''
                        ELSE convert(VARCHAR, ase.fechaCierre_ase, 103) + ' ' +
                             convert(VARCHAR, ase.fechaCierre_ase, 108) END AS fechaCierre_ase
                  , isnull(ase.codigo_per_reg, 0)                           AS codigo_per_reg
                  , CASE
                        WHEN ase.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, ase.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, ase.fecha_reg, 108) END       AS fecha_reg
                  , isnull(ase.codigo_per_act, 0)                           AS codigo_per_act
                  , CASE
                        WHEN ase.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, ase.fecha_act, 103) + ' ' +
                             convert(VARCHAR, ase.fecha_act, 108) END       AS fecha_act
                  , isnull(ase.estado_ase, 0)                               AS estado_ase
                FROM ADM_AsistenciaEvaluacion ase WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_ase = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_ase, ',')
                                                  WHERE item = ase.codigo_ase))
                  AND (@codigo_gru = 0 OR ase.codigo_gru = @codigo_gru)
                  AND (@codigo_alu = 0 OR ase.codigo_alu = @codigo_alu)
                  AND (@estadoAsistencia_ase = '' OR ase.estadoAsistencia_ase LIKE @estadoAsistencia_ase)
                  AND (@fechaCierre_ase IS NULL OR cast(ase.fechaCierre_ase AS DATE) = @fechaCierre_ase)
                  AND (@codigo_per_reg = 0 OR ase.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(ase.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR ase.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(ase.fecha_act AS DATE) = @fecha_act)
                  AND ase.estado_ase = 1
            END

        IF @tipoConsulta = 'LT'
            BEGIN
                SELECT
                    gru.codigo_gru
                  , gru.codigo_cco
                  , gru.codigo
                  , gru.nombre
                  , gru.aula_activa
                  , gru.estado
                  , gru.idcourse
                  , gru.idcourserole
                  , gru.idcoursecontext
                  , gru.codigo_per_reg
                  , gru.fecha_reg
                  , gru.codigo_per_act
                  , gru.fecha_act
                  , ''                                                                                           centrocosto
                  , ISNULL(te.total, 0)                                                                          cant_estudiante
                  , gru.codigo_amb
                  , amb.descripcionReal_Amb                                                                      ambiente
                  , ISNULL(amb.virtual_amb, 0)                                                                   virtual_amb
                  , ISNULL(gru.capacidad, 0)                                                                     capacidad
                  , ISNULL(te.total, 0)             AS                                                           asignado
                  , (CONVERT(VARCHAR, ISNULL(te.total, 0)) + ' / ' + CONVERT(VARCHAR, ISNULL(gru.capacidad, 0))) cap_dis
                  , ''                                                                                           fechaHoraInicio_gru
                  , ''                                                                                           fechaHoraFin_gru
                  , (tam.descripcion_Tam + ' - ' + amb.descripcionReal_Amb)                                      nombre_amb
                  , ISNULL(gru.codigo_tge, -1)                                                                   codigo_tge
                  , ISNULL(tge.nombre_tge, '')                                                                   nombre_tge
                  , isnull(cer.total_asistencia, 0) AS                                                           total_asistencia
                  , CASE WHEN isnull(cer.total_cerrado, 0) > 0 THEN 'CERRADO' ELSE 'NO CERRADO' END              estado_cierre
                  , CASE WHEN isnull(cer.total_cerrado, 0) > 0 THEN 1 ELSE 0 END                                 cerrado
                FROM dbo.ADM_GrupoAdmisionVirtual (NOLOCK) gru
                     JOIN (SELECT _gcc.codigo_gru
                           FROM dbo.ADM_GrupoAdmision_CentroCosto (NOLOCK) _gcc
                           WHERE 1 = 1
                             AND (@codigo_cco = 0 OR _gcc.codigo_cco = @codigo_cco)
                             AND _gcc.estado_gcc = 1
                           GROUP BY _gcc.codigo_gru) AS gcc ON gru.codigo_gru = gcc.codigo_gru
                     JOIN dbo.Ambiente (NOLOCK) amb ON gru.codigo_amb = amb.codigo_Amb
                     JOIN dbo.TipoAmbiente (NOLOCK) tam ON amb.codigo_Tam = tam.codigo_Tam
                     LEFT JOIN (SELECT ga.codigo_gru, COUNT(ga.codigo_alu) total
                                FROM dbo.ADM_GrupoAdmisionVirtual_Alumno (NOLOCK) ga
                                WHERE ga.estado <> 0
                                GROUP BY ga.codigo_gru) AS te ON gru.codigo_gru = te.codigo_gru
                     LEFT JOIN dbo.ADM_TipoGrupoEvaluacion (NOLOCK) tge ON gru.codigo_tge = tge.codigo_tge
                     OUTER APPLY (SELECT
                                      sum(CASE
                                              WHEN ltrim(rtrim(isnull(_ase.estadoAsistencia_ase, ''))) = '' THEN 0
                                              ELSE 1 END)                                           AS total_asistencia
                                    , sum(CASE WHEN _ase.fechaCierre_ase IS NULL THEN 0 ELSE 1 END) AS total_cerrado
                                  FROM ADM_AsistenciaEvaluacion _ase WITH (NOLOCK)
                                  WHERE 1 = 1
                                    AND _ase.codigo_gru = gru.codigo_gru
                                    AND _ase.estado_ase = 1
                                  GROUP BY _ase.codigo_gru) cer
                WHERE (gru.codigo_gru = @codigo_gru OR (gru.codigo_gru = gru.codigo_gru - @codigo_gru))
                  AND (gru.codigo_tge = @codigo_tge OR (gru.codigo_tge = gru.codigo_tge - @codigo_tge))
                  AND gru.estado <> 0
            END

        IF @tipoConsulta = 'REGAS' --Para formulario de registro de asistencia
            BEGIN
                SELECT
                    alu.codigo_Alu
                  , alu.codigoUniver_Alu
                  , alu.nroDocIdent_Alu
                  , alu.apellidoPat_Alu
                  , alu.apellidoMat_Alu
                  , alu.nombres_Alu
                  , cpf.codigo_Cpf
                  , cpf.nombre_Cpf
                  , ISNULL(deu.CargoTotal, 0.00)                            AS CargoTotal
                  , ISNULL(deu.AbonoTotal, 0.00)                            AS AbonoTotal
                  , ISNULL(deu.SaldoTotal, 0.00)                            AS SaldoTotal
                  , isnull(ase.estadoAsistencia_ase, '')                    AS estadoAsistencia_ase
                  , ase.fechaCierre_ase                                     AS fechaCierre_ase
                  , CASE WHEN ase.fechaCierre_ase IS NULL THEN 0 ELSE 1 END AS estado_cierre
                FROM ADM_GrupoAdmisionVirtual_Alumno gva
                     JOIN Alumno alu WITH (NOLOCK) ON gva.codigo_alu = alu.codigo_Alu
                     JOIN CarreraProfesional cpf WITH (NOLOCK) ON alu.tempcodigo_cpf = cpf.codigo_Cpf
                     LEFT JOIN ADM_AsistenciaEvaluacion ase WITH (NOLOCK)
                               ON gva.codigo_gru = ase.codigo_gru AND ase.codigo_alu = gva.codigo_alu AND
                                  ase.estado_ase = 1

                     OUTER APPLY (SELECT
                                      _deu.codigo_Pso
                                    , SUM(_deu.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS CargoTotal
                                    , SUM(_deu.montoTotal_Deu - _deu.saldo_Deu) -
                                      SUM(ISNULL(cin.TotalMonNac_Dci, 0))                            AS AbonoTotal
                                    , SUM(_deu.Saldo_Deu)                                            AS SaldoTotal
                                  FROM dbo.Deuda _deu (NOLOCK)
                                       LEFT JOIN DetalleCajaIngreso cin (NOLOCK)
                                                 ON cin.codigo_Deu = _deu.codigo_deu AND cin.codigo_mno = 4
                                  WHERE _deu.codigo_Alu = alu.codigo_alu
                                    AND _deu.codigo_cco = alu.codigo_cco
                                    AND _deu.codigo_Sco = 15 --ESCUELA PRE UNIVERSITARIA
                                    AND _deu.estado_Deu <> 'A'
                                  GROUP BY _deu.codigo_Pso) deu
                WHERE 1 = 1
                  AND gva.codigo_gru = @codigo_gru
                  AND gva.estado = 1
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

GRANT EXECUTE ON [dbo].[ADM_AsistenciaEvaluacion_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_AsistenciaEvaluacion_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_AsistenciaEvaluacion_Listar] TO iusrvirtualsistema
GO
