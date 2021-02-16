/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          08/09/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ReporteAforoPorAula
    @codigo_test INT = 0
    , @codigo_cac INT = 0
    , @codigo_cco VARCHAR(MAX) = ''
    , @codigo_amb VARCHAR(MAX) = ''
AS
BEGIN
    BEGIN TRY

        IF @codigo_test = 1
            SET @codigo_test = 2

        SELECT DISTINCT
            tam.descripcion_Tam + ' - ' + amb.descripcionReal_Amb AS aula
          , gru.capacidad
          , count(dat.codigo_Alu)                                 AS asignado
          , gru.capacidad - count(dat.codigo_Alu)                 AS disponoble
          , cast(gru.fechaHoraInicio_gru AS DATE)                 AS fecha
          , CASE
                WHEN gru.fechaHoraInicio_gru IS NULL THEN ''
                WHEN cast(gru.fechaHoraInicio_gru AS TIME) >= '19:00:00' THEN 'NOCHE'
                WHEN cast(gru.fechaHoraInicio_gru AS TIME) >= '12:00:00' THEN 'TARDE'
                ELSE N'MAÑANA' END                                AS turno
        FROM ADM_GrupoAdmisionVirtual gru WITH (NOLOCK)
             JOIN ADM_GrupoAdmision_CentroCosto gcc WITH (NOLOCK)
                  ON gru.codigo_gru = gcc.codigo_gru AND gcc.estado_gcc = 1
             JOIN Ambiente amb WITH (NOLOCK) ON gru.codigo_amb = amb.codigo_Amb
             JOIN TipoAmbiente tam WITH (NOLOCK) ON amb.codigo_Tam = tam.codigo_Tam
             LEFT JOIN ADM_GrupoAdmisionVirtual_Alumno gva WITH (NOLOCK)
                       ON gru.codigo_gru = gva.codigo_gru AND gva.estado = 1
             OUTER APPLY (SELECT DISTINCT alu.codigo_Alu
                          FROM Alumno alu WITH (NOLOCK)
                               JOIN CicloAcademico cac WITH (NOLOCK) ON alu.cicloIng_Alu = cac.descripcion_Cac
                          WHERE 1 = 1
                            AND alu.codigo_Alu = gva.codigo_alu
--                             AND alu.codigo_cco = gcc.codigo_cco
                            AND (@codigo_test = 0 OR alu.codigo_test = @codigo_test)
                            AND (@codigo_cac = 0 OR cac.codigo_Cac = @codigo_cac)
        ) dat
        WHERE 1 = 1
          AND gru.estado = 1
          AND (@codigo_cco = '' OR gcc.codigo_cco IN (SELECT item
                                                      FROM dbo.fnSplit2(@codigo_cco, ',')))
          AND (@codigo_amb = '' OR amb.codigo_Amb IN (SELECT item
                                                      FROM dbo.fnSplit2(@codigo_amb, ',')))
        GROUP BY tam.descripcion_Tam + ' - ' + amb.descripcionReal_Amb, gru.capacidad
               , CASE
                     WHEN gru.fechaHoraInicio_gru IS NULL
                         THEN ''
                     WHEN cast(gru.fechaHoraInicio_gru AS TIME) >= '19:00:00'
                         THEN 'NOCHE'
                     WHEN cast(gru.fechaHoraInicio_gru AS TIME) >= '12:00:00'
                         THEN 'TARDE'
                     ELSE N'MAÑANA' END
               , gru.fechaHoraInicio_gru, gru.fechaHoraFin_gru, gcc.codigo_cco
        ORDER BY aula

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

GRANT EXECUTE ON [dbo].[ADM_ReporteAforoPorAula] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ReporteAforoPorAula] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ReporteAforoPorAula] TO iusrvirtualsistema
GO