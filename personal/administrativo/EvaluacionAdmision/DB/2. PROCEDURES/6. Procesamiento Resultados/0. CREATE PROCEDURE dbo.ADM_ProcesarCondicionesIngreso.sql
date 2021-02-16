/*  Usuario Crea:   andy.diaz
    Fecha:          01/10/2020
    Descripción:

    Historial de Cambios
    CODIGO  FECHA    DESARROLLADOR   DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ProcesarCondicionesIngreso
    @codigo_cco INT
    , @codigo_cac INT
    , @codigo_cpf INT
    , @codigo_min INT
    , @nota_min NUMERIC(9, 3)
    , @cantidad_accesitarios INT
    , @cod_usuario INT = 0
    , @rpta INT = 0 OUTPUT
    , @msg VARCHAR(200) = '' OUTPUT
AS
BEGIN
    DECLARE @trancount BIT = 0

    BEGIN TRY
        IF @@TRANCOUNT = 0
            BEGIN
                BEGIN TRANSACTION
                    SET @trancount = 1
            END

        DECLARE @cantidad_vae INT = 0

        IF OBJECT_ID('tempdb..#resultados') IS NOT NULL
            DROP TABLE #resultados

        CREATE TABLE #resultados (
            codigo_alu INT,
            notaFinal_elu NUMERIC(9, 3),
            apellidoPat_Alu VARCHAR(100),
            apellidoMat_Alu VARCHAR(100),
            nombres_Alu VARCHAR(200)
        )

        --Obtengo el número de vacantes
        SELECT @cantidad_vae = ISNULL(vae.cantidad_vae, 0)
        FROM ADM_VacantesEvento vae WITH (NOLOCK)
             JOIN Vacantes vac WITH (NOLOCK) ON vae.codigo_vac = vac.codigo_Vac
        WHERE vac.codigo_cac = @codigo_cac
          AND vac.codigo_cpf = @codigo_cpf
          AND vac.codigo_min = @codigo_min
          AND vae.codigo_cco = @codigo_cco
          AND vae.estado_vae = 1

        -- Guardo temporalmente los postulantes
        INSERT INTO #resultados
        SELECT DISTINCT elu.codigo_alu, elu.notaFinal_elu, alu.apellidoPat_Alu, alu.apellidoMat_Alu, alu.nombres_Alu
        FROM Alumno alu WITH (NOLOCK)
             JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON alu.codigo_Alu = elu.codigo_alu AND elu.estado_elu = 1
        WHERE alu.codigo_cco = @codigo_cco
          AND alu.tempcodigo_cpf = @codigo_cpf
          AND alu.codigo_Min = @codigo_min
        ORDER BY elu.notaFinal_elu DESC, alu.apellidoPat_Alu, alu.apellidoMat_Alu, alu.nombres_Alu

        -- Reseteo la condición de ingreso (POSTULANTE) para toda la lista
        UPDATE elu
        SET elu.condicion_ingreso_elu = 'P'
          , elu.codigo_per_act        = @cod_usuario
        FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
             JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
        WHERE evl.codigo_cco = @codigo_cco
          AND elu.codigo_alu IN (SELECT res.codigo_alu
                                 FROM #resultados res WITH (NOLOCK))

        -- Actualizo la condición de ingreso de los postulantes con nota mayor o igual a la mínima y que alcanzaron vacante (INGRESANTES)
        UPDATE elu
        SET condicion_ingreso_elu = 'I'
          , codigo_per_act        = @cod_usuario
        FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
             JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
        WHERE evl.codigo_cco = @codigo_cco
          AND codigo_alu IN (SELECT TOP (@cantidad_vae) res.codigo_alu
                             FROM #resultados res WITH (NOLOCK)
                             WHERE res.notaFinal_elu >= @nota_min
                             ORDER BY res.notaFinal_elu DESC, res.apellidoPat_Alu, res.apellidoMat_Alu, res.nombres_Alu)

        -- Actualizo la condición de ingreso a los postulantes con nota mayor o igual a la nota mínima pero que no alcanzaron vacante (ACCESITARIOS)
        UPDATE elu
        SET condicion_ingreso_elu = 'A'
          , codigo_per_act        = @cod_usuario
        FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
             JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
        WHERE evl.codigo_cco = @codigo_cco
          AND elu.codigo_alu IN (SELECT TOP (@cantidad_accesitarios) res.codigo_alu
                                 FROM #resultados res WITH (NOLOCK)
                                      JOIN ADM_Evaluacion_Alumno _elu WITH (NOLOCK)
                                           ON _elu.codigo_alu = res.codigo_alu
                                 WHERE res.notaFinal_elu >= @nota_min
                                   AND _elu.condicion_ingreso_elu <> 'I'
                                 GROUP BY res.codigo_alu
                                 ORDER BY MAX(res.notaFinal_elu) DESC, MAX(res.apellidoPat_Alu)
                                        , MAX(res.apellidoMat_Alu)
                                        , MAX(res.nombres_Alu))

        IF @trancount = 1
            COMMIT

        SET @rpta = 1
        SET @msg = N'Se realizó la operación correctamente'
    END TRY
    BEGIN CATCH
        IF @trancount = 1
            ROLLBACK

        SET @rpta = -1;
        SET @msg = N'Ocurrió un error en la operación'

        PRINT ERROR_MESSAGE()

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
GO

GRANT EXECUTE ON [dbo].[ADM_ProcesarCondicionesIngreso] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_ProcesarCondicionesIngreso] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ProcesarCondicionesIngreso] TO iusrvirtualsistema
GO