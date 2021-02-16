/*  Usuario Crea:   andy.diaz
    Fecha:          25/08/2020
    Descripción:    Listar de tabla ADM_VacantesEvento

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_VacantesEvento_Listar
    @tipoConsulta CHAR(5) = 'GEN'
    , @codigo_vae VARCHAR(100) = ''
    , @codigo_cco INT = 0
    , @codigo_vac INT = 0
    , @cantidad_vae INT = 0
    , @cantidad_accesitarios_vae INT = 0
    , @codigo_per_reg INT = 0
    , @fecha_reg DATETIME = NULL
    , @codigo_per_act INT = 0
    , @fecha_act DATETIME = NULL
    -- Filtros adicionales
    , @codigo_cac INT = 0
    , @codigo_cpf INT = 0
    , @codigo_min INT = 0
AS
BEGIN
    BEGIN TRY
        DECLARE @descripcion_cac AS VARCHAR(10)

        -- Tabla temporal que almacena los códigos únicos de los interesados de acuerdo a los filtros enviados
        IF OBJECT_ID('tempdb..#cco') IS NOT NULL
            DROP TABLE #cco

        CREATE TABLE #cco (
            codigo_cco INT,
            descripcion_cco VARCHAR(500),
            fechaInicio DATE
        )

        IF @codigo_cac = 0
            SET @descripcion_cac = ''
        ELSE
            BEGIN
                SELECT @descripcion_cac = descripcion_Cac
                FROM CicloAcademico cac WITH (NOLOCK)
                WHERE cac.codigo_Cac = @codigo_cac
            END

        IF @tipoConsulta = 'GEN'
            BEGIN
                SELECT
                    isnull(vae.codigo_vae, 0)                         AS codigo_vae
                  , isnull(vae.codigo_cco, 0)                         AS codigo_cco
                  , isnull(vae.codigo_vac, 0)                         AS codigo_vac
                  , isnull(vae.cantidad_vae, 0)                       AS cantidad_vae
                  , isnull(vae.cantidad_accesitarios_vae, 0)          AS cantidad_accesitarios_vae
                  , isnull(vae.codigo_per_reg, 0)                     AS codigo_per_reg
                  , CASE
                        WHEN vae.fecha_reg IS NULL THEN ''
                        ELSE convert(VARCHAR, vae.fecha_reg, 103) + ' ' +
                             convert(VARCHAR, vae.fecha_reg, 108) END AS fecha_reg
                  , isnull(vae.codigo_per_act, 0)                     AS codigo_per_act
                  , CASE
                        WHEN vae.fecha_act IS NULL THEN ''
                        ELSE convert(VARCHAR, vae.fecha_act, 103) + ' ' +
                             convert(VARCHAR, vae.fecha_act, 108) END AS fecha_act
                  , isnull(vae.estado_vae, 0)                         AS estado_vae
                FROM ADM_VacantesEvento vae WITH (NOLOCK)
                WHERE 1 = 1
                  AND (@codigo_vae = '' OR EXISTS(SELECT item
                                                  FROM fnSplit2(@codigo_vae, ',')
                                                  WHERE item = vae.codigo_vae))
                  AND (@codigo_cco = 0 OR vae.codigo_cco = @codigo_cco)
                  AND (@codigo_vac = 0 OR vae.codigo_vac = @codigo_vac)
                  AND (@cantidad_vae = 0 OR vae.cantidad_vae = @cantidad_vae)
                  AND (@cantidad_accesitarios_vae = 0 OR vae.cantidad_accesitarios_vae = @cantidad_accesitarios_vae)
                  AND (@codigo_per_reg = 0 OR vae.codigo_per_reg = @codigo_per_reg)
                  AND (@fecha_reg IS NULL OR cast(vae.fecha_reg AS DATE) = @fecha_reg)
                  AND (@codigo_per_act = 0 OR vae.codigo_per_act = @codigo_per_act)
                  AND (@fecha_act IS NULL OR cast(vae.fecha_act AS DATE) = @fecha_act)
                  AND vae.estado_vae = 1
            END

        IF @tipoConsulta = 'VAC' -- Lista vacantes aunque no exista registro en tabla ADM_VacanteEvento
            BEGIN
                INSERT INTO #cco
                    ( codigo_cco
                    , descripcion_cco
                    , fechaInicio)
                    EXEC EVE_ConsultarCentroCostosXPermisosXVisibilidad @tipo=1, @codigo_per=684, @centrocostos='',
                         @codigo_test=1, @visibilidad=1

                SELECT
                    cac.codigo_cac
                  , cac.descripcion_Cac
                  , cpf.nombre_Cpf
                  , min.nombre_Min
                  , vac.codigo_Vac
                  , vac.numero_vac
                  , cco.codigo_cco
                  , cco.descripcion_Cco
                  , isnull(vae.codigo_vae, 0)                AS codigo_vae
                  , isnull(vae.cantidad_vae, 0)              AS cantidad_vae
                  , isnull(vae.cantidad_accesitarios_vae, 0) AS cantidad_accesitarios_vae
                FROM Vacantes vac WITH (NOLOCK)
                     JOIN CicloAcademico cac WITH (NOLOCK) ON vac.codigo_cac = cac.codigo_Cac
                     JOIN CarreraProfesional cpf WITH (NOLOCK) ON vac.codigo_cpf = cpf.codigo_Cpf
                     JOIN ModalidadIngreso min WITH (NOLOCK) ON vac.codigo_min = min.codigo_Min
                     CROSS APPLY (SELECT cco.codigo_cco, cco.descripcion_cco
                                  FROM #cco cco WITH (NOLOCK)
                                  WHERE cco.descripcion_Cco LIKE '% ' + cac.descripcion_Cac + ' %') cco
                     LEFT JOIN ADM_VacantesEvento vae WITH (NOLOCK)
                               ON vac.codigo_Vac = vae.codigo_vac AND vae.codigo_cco = cco.codigo_cco
                WHERE 1 = 1
                  AND (@codigo_cac = 0 OR cac.codigo_Cac = @codigo_cac)
                  AND (@codigo_cpf = 0 OR cpf.codigo_Cpf = @codigo_cpf)
                  AND (@codigo_min = 0 OR min.codigo_min = @codigo_min)
                  AND (@codigo_cco = 0 OR cco.codigo_Cco = @codigo_cco)
                ORDER BY cpf.nombre_Cpf, cco.codigo_cco DESC
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

GRANT EXECUTE ON [dbo].[ADM_VacantesEvento_Listar] TO usuariogeneral
GRANT EXECUTE ON [dbo].[ADM_VacantesEvento_Listar] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_VacantesEvento_Listar] TO iusrvirtualsistema
GO