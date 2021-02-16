/*  Usuario Crea:   ANDY.DIAZ
    Fecha:          26/08/2020
    Descripción:

    Historial de Cambios
    CODIGO      FECHA       DESARROLLADOR       DESCRIPCIÓN
    001
*/
CREATE PROCEDURE dbo.ADM_ConsultarEventoAdmision
    @tipo_consulta VARCHAR(5) = 'GEN'
    , @codigo_cac INT = 0
    , @descripcion_cco VARCHAR(500) = ''
    , @codigo_test INT = 1
    , @visibilidad INT = 1
    , @cod_usuario INT
AS
BEGIN
    BEGIN TRY
        --SELECT -1 codigo_cco, '' descripcion_cco
        DECLARE @descripcion_cac AS VARCHAR(10) = ''

        IF OBJECT_ID('tempdb..#cco') IS NOT NULL
            DROP TABLE #cco
        CREATE TABLE #cco (
            codigo_cco INT,
            descripcion_cco VARCHAR(500),
            fechaInicio DATE
        )

        INSERT INTO #cco
            ( codigo_cco
            , descripcion_cco
            , fechaInicio)
            EXEC EVE_ConsultarCentroCostosXPermisosXVisibilidad @tipo=1, @codigo_per=@cod_usuario,
                 @centrocostos=@descripcion_cco, @codigo_test=@codigo_test, @visibilidad=@visibilidad

        IF @tipo_consulta = 'GEN'
            BEGIN
                SELECT DISTINCT cco.codigo_cco, cco.descripcion_cco, cco.fechaInicio
                FROM #cco cco WITH (NOLOCK)
                     LEFT JOIN ADM_DatosEventoAdmision dea WITH (NOLOCK)
                               ON cco.codigo_cco = dea.codigo_cco AND dea.estado_dea = 1
                WHERE (@codigo_cac = 0 OR dea.codigo_cac = @codigo_cac)
                ORDER BY cco.fechaInicio DESC, cco.descripcion_cco DESC
            END

        IF @tipo_consulta = 'DEA' -- Para Datos Evento Admisión
            BEGIN
                SELECT DISTINCT
                    cco.codigo_cco
                  , cco.descripcion_cco
                  , cco.fechaInicio
                  , isnull(dea.codigo_dea, 0)                                   AS codigo_dea
                  , isnull(cac.codigo_Cac, 0)                                   AS codigo_Cac
                  , isnull(cac.descripcion_Cac, '')                             AS descripcion_Cac
                  , isnull(convert(NVARCHAR(30), dea.fechaEvento_dea, 103), '') AS fechaEvento_dea
                FROM #cco cco WITH (NOLOCK)
                     LEFT JOIN ADM_DatosEventoAdmision dea WITH (NOLOCK)
                               ON cco.codigo_cco = dea.codigo_cco AND dea.estado_dea = 1
                     LEFT JOIN CicloAcademico cac WITH (NOLOCK) ON dea.codigo_cac = cac.codigo_Cac
                WHERE 1 = 1
                  AND (@codigo_cac = 0 OR dea.codigo_cac = @codigo_cac)
                ORDER BY fechaInicio DESC, cco.descripcion_cco DESC
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

GRANT EXECUTE ON [dbo].[ADM_ConsultarEventoAdmision] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_ConsultarEventoAdmision] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ConsultarEventoAdmision] TO iusrvirtualsistema
GO