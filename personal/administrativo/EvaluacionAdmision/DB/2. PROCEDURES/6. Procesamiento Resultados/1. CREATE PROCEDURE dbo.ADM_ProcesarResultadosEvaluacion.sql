/*  Usuario Crea:   andy.diaz
    Fecha:          02/10/2020
    Descripción:

    Historial de Cambios
    CODIGO  FECHA    DESARROLLADOR   DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ProcesarResultadosEvaluacion
    @codigo_evl INT
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

        DECLARE @codigo_cco INT = 0
            , @codigo_cac INT = 0
            , @codigo_cpf INT = 0
            , @codigo_min INT = 0
            , @nota_min NUMERIC(9, 2) = 0.00
            , @cantidad_accesitarios INT = 0
            , @nombre_cpf VARCHAR(250)

        -- Obtengo el centro de costo y el ciclo académico de la evaluación
        SELECT @codigo_cco = evl.codigo_cco
        FROM ADM_Evaluacion evl WITH (NOLOCK)
        WHERE evl.codigo_evl = @codigo_evl

        SELECT @codigo_cac = dea.codigo_cac
        FROM ADM_DatosEventoAdmision dea WITH (NOLOCK)
        WHERE dea.codigo_cco = @codigo_cco

        -- Calculo las notas de la evaluación
        EXEC ADM_CalcularNotasEvaluacion @codigo_evl = @codigo_evl
            , @cod_usuario = @cod_usuario
            , @rpta = @rpta OUTPUT,
             @msg = @msg OUTPUT

        IF @rpta <> 1
            RAISERROR (@msg, 16, 1);

        -- Obtengo la lista carreras, modalidades y notas mínimas para calcular las condiciones de ingreso
        DECLARE cur_bloques CURSOR FOR
            SELECT DISTINCT alu.tempcodigo_cpf, alu.codigo_Min, ISNULL(cnm.nota_min_cnm, 0)
            FROM ADM_Evaluacion evl WITH (NOLOCK)
                 JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON evl.codigo_evl = elu.codigo_evl AND elu.estado_elu = 1
                 JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
                 OUTER APPLY (SELECT TOP 1 _cnm.nota_min_cnm
                              FROM ADM_Configuracion_NotaMinima _cnm WITH (NOLOCK)
                              WHERE _cnm.codigo_cpf = alu.tempcodigo_cpf
                                AND _cnm.estado_cnm = 1) cnm

        -- Ejecuto el procedimiento que actualiza las condiciones de ingreso
        OPEN cur_bloques
        FETCH NEXT FROM cur_bloques INTO @codigo_cpf, @codigo_min, @nota_min
        WHILE @@FETCH_STATUS = 0
            BEGIN
                IF @nota_min = 0
                    BEGIN
                        SELECT @nombre_cpf = cpf.nombre_Cpf
                        FROM CarreraProfesional cpf WITH (NOLOCK)
                        WHERE codigo_Cpf = @codigo_cpf

                        SET @rpta = 0;
                        SET @msg = N'No se ha encontrado una configuración de nota mínima para la carrera: ' +
                                   ISNULL(@nombre_cpf, '');

                        RAISERROR (@msg, 16, 1);
                    END

                SET @cantidad_accesitarios = 0

                SELECT @cantidad_accesitarios = ISNULL(vae.cantidad_accesitarios_vae, 0)
                FROM ADM_VacantesEvento vae WITH (NOLOCK)
                     JOIN Vacantes vac WITH (NOLOCK) ON vae.codigo_vac = vac.codigo_Vac AND vac.estado_vac = 1
                WHERE vae.codigo_cco = @codigo_cco
                  AND vac.codigo_cpf = @codigo_cpf
                  AND vac.codigo_min = @codigo_min
                  AND vae.estado_vae = 1

                EXEC ADM_ProcesarCondicionesIngreso @codigo_cco = @codigo_cco
                    , @codigo_cac = @codigo_cac
                    , @codigo_cpf = @codigo_cpf
                    , @codigo_min = @codigo_min
                    , @nota_min = @nota_min
                    , @cantidad_accesitarios = @cantidad_accesitarios
                    , @cod_usuario = @cod_usuario
                    , @rpta = @rpta OUTPUT
                    , @msg = @msg OUTPUT

                FETCH NEXT FROM cur_bloques INTO @codigo_cpf, @codigo_min, @nota_min

                IF @rpta <> 1
                    RAISERROR (@msg, 16, 1);
            END
        CLOSE cur_bloques
        DEALLOCATE cur_bloques

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

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
GO

GRANT EXECUTE ON [dbo].[ADM_ProcesarResultadosEvaluacion] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_ProcesarResultadosEvaluacion] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ProcesarResultadosEvaluacion] TO iusrvirtualsistema
GO