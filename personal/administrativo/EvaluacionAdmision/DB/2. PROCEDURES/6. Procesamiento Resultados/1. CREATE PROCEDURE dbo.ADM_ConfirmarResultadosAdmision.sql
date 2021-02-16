IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.ROUTINES
          WHERE ROUTINE_NAME = 'ADM_ConfirmarResultadosAdmision'
            AND SPECIFIC_SCHEMA = 'dbo')
    DROP PROCEDURE ADM_ConfirmarResultadosAdmision;
GO

/*  Usuario Crea:   andy.diaz
    Fecha:          27/10/2020
    Descripción:

    Historial de Cambios
    CODIGO  FECHA    DESARROLLADOR   DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ConfirmarResultadosAdmision
    @codigo_cac INT = 0
    , @codigo_min TINYINT = 0
    , @codigo_cpf INT = 0
    , @codigo_cco INT = 0
    , @cod_usuario INT
    , @rpta INT = 0 OUTPUT
    , @msg VARCHAR(200) = '' OUTPUT
    , @cod INT = 0 OUTPUT
AS
BEGIN
    DECLARE @trancount BIT = 0

    BEGIN TRY
        IF @@TRANCOUNT = 0
            BEGIN
                BEGIN TRANSACTION
                    SET @trancount = 1
            END

        -- Variables para cursor
        DECLARE @codigo_elu INT
            , @codigo_alu INT
            , @condicion_ingreso_elu CHAR(1)
            , @puntaje_elu NUMERIC(9, 2)

        DECLARE @estadoPostulacion CHAR(1), @alcanzoVacante BIT = 0, @cicloIng_Alu VARCHAR(10)

        DECLARE cur_alumnos CURSOR FOR
            SELECT
                elu.codigo_elu
              , elu.codigo_alu
              , elu.condicion_ingreso_elu
              , elu.puntaje_elu
              , alu.cicloIng_Alu
            FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                 JOIN Alumno alu WITH (NOLOCK) ON elu.codigo_alu = alu.codigo_Alu
            WHERE 1 = 1
              AND alu.codigo_cco = @codigo_cco
              AND alu.tempcodigo_cpf = @codigo_cpf
              AND alu.codigo_Min = @codigo_min
              AND elu.estado_elu = 1;

        OPEN cur_alumnos
        FETCH NEXT FROM cur_alumnos INTO @codigo_elu, @codigo_alu, @condicion_ingreso_elu, @puntaje_elu, @cicloIng_Alu

        WHILE @@fetch_status = 0
            BEGIN
                -- Actualizo el estado de las notas del postulante
                UPDATE ADM_Evaluacion_Alumno
                SET estadonota_elu = 'C'
                WHERE codigo_elu = @codigo_elu

                UPDATE ADM_Evaluacion_Alumno_Nivelacion
                SET estado_calificacion_ean = 'C'
                WHERE codigo_elu = @codigo_elu
                  AND estado_ean = 1

                -- Inactivo al postulante si no ingresó
                UPDATE dbo.Alumno
                SET estadoActual_Alu = CASE WHEN @condicion_ingreso_elu = 'P' THEN 0 ELSE 1 END
                WHERE codigo_Alu = @codigo_alu

                IF @condicion_ingreso_elu = 'I'
                    SET @alcanzoVacante = 1
                ELSE
                    SET @alcanzoVacante = 0

                IF @condicion_ingreso_elu = 'A'
                    SET @estadoPostulacion = 'I'
                ELSE
                    SET @estadoPostulacion = @condicion_ingreso_elu

                -- Activo el estado de postulación
                EXEC EPU_ActivarEstadoPostulacion_V2 @codigo_Alu = @codigo_alu,
                     @estadopostulacion_Dal = @estadoPostulacion,
                     @alcanzo_vacante = @alcanzoVacante,
                     @observacion = N'DESDE SISTEMA DE EVALUACIÓN',
                     @nota_ajustada = @puntaje_elu,
                     @nota_real = @puntaje_elu,
                     @usuario = @cod_usuario,
                     @log_ipusureg = '',
                     @ciclo_ing = @cicloIng_Alu

                -- Agrego a alumno en AlumnoPerfilIngreso en caso necesite nivelación
                EXEC ADM_AgregarAlumnoPerfilIngreso @codigo_alu = @codigo_alu
                    , @rpta = @rpta OUTPUT
                    , @msg = @msg OUTPUT

                IF @rpta <> 1
                    RAISERROR (@msg, 16, 1);

                FETCH NEXT FROM cur_alumnos INTO @codigo_elu, @codigo_alu, @condicion_ingreso_elu, @puntaje_elu, @cicloIng_Alu
            END

        CLOSE cur_alumnos
        DEALLOCATE cur_alumnos

        EXEC ADM_CierreResultadosAdmision_IUD @operacion = 'I'
            , @codigo_cac = @codigo_cac
            , @codigo_min = @codigo_min
            , @codigo_cpf = @codigo_cpf
            , @codigo_cco = @codigo_cco
            , @cod_usuario = @cod_usuario
            , @rpta = @rpta OUTPUT
            , @msg = @msg OUTPUT

        IF @rpta <> 1
            RAISERROR (@msg, 16, 1);

        IF @trancount = 1
            COMMIT

        SET @rpta = 1
        SET @msg = N'Se realizó la operación correctamente'
        SET @cod = 0
    END TRY
    BEGIN CATCH
        IF @trancount = 1
            ROLLBACK

        SET @rpta = -1;

        PRINT ERROR_MESSAGE()

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
GO

GRANT EXECUTE ON [dbo].[ADM_ConfirmarResultadosAdmision] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_ConfirmarResultadosAdmision] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_ConfirmarResultadosAdmision] TO iusrvirtualsistema
GO