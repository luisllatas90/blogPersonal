/*  Usuario Crea:   andy.diaz
    Fecha:          24/09/2020
    Descripción:    Mantenimiento de tabla ADM_Evaluacion_Alumno

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Evaluacion_Alumno_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_elu INT = 0
    , @codigo_evl INT = 0
    , @codigo_alu INT = 0
    , @nota_elu NUMERIC(9, 3) = 0
    , @estadonota_elu CHAR(1) = ''
    , @condicion_ingreso_elu CHAR(1) = ''
    , @estadoverificacion_elu CHAR(1) = ''
    , @observacion_elu VARCHAR(500) = ''
    , @respuesta_elu VARCHAR(MAX) = ''
    , @puntaje_elu NUMERIC(9, 3) = 0
    , @notaFinal_elu NUMERIC(9, 3) = 0
    , @puntajeFinal_elu NUMERIC(9, 3) = 0
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

        IF @operacion = 'I'
            BEGIN
                IF EXISTS(SELECT 1
                          FROM ADM_Evaluacion_Alumno WITH (NOLOCK)
                          WHERE codigo_elu = @codigo_elu)
                    BEGIN
                        SET @operacion = 'U'
                    END
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_Evaluacion_Alumno
                    ( codigo_evl
                    , codigo_alu
                    , nota_elu
                    , estadonota_elu
                    , condicion_ingreso_elu
                    , estadoverificacion_elu
                    , observacion_elu
                    , respuesta_elu
                    , puntaje_elu
                    , notaFinal_elu
                    , puntajeFinal_elu
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_elu)
                VALUES
                    ( @codigo_evl
                    , @codigo_alu
                    , @nota_elu
                    , @estadonota_elu
                    , @condicion_ingreso_elu
                    , @estadoverificacion_elu
                    , @observacion_elu
                    , @respuesta_elu
                    , @puntaje_elu
                    , @notaFinal_elu
                    , @puntajeFinal_elu
                    , @cod_usuario
                    , GETDATE()
                    , @cod_usuario
                    , GETDATE()
                    , 1);
                SET @rpta = 1
                SET @msg = N'Se realizó la operación correctamente'
                SET @cod = @@IDENTITY
            END

        --UPDATE
        IF @operacion = 'U'
            BEGIN
                UPDATE ADM_Evaluacion_Alumno
                SET codigo_evl             = @codigo_evl
                  , codigo_alu             = @codigo_alu
                  , nota_elu               = @nota_elu
                  , estadonota_elu         = @estadonota_elu
                  , condicion_ingreso_elu  = @condicion_ingreso_elu
                  , estadoverificacion_elu = @estadoverificacion_elu
                  , observacion_elu        = @observacion_elu
                  , respuesta_elu          = @respuesta_elu
                  , puntaje_elu            = @puntaje_elu
                  , notaFinal_elu          = @notaFinal_elu
                  , puntajeFinal_elu       = @puntajeFinal_elu
                  , codigo_per_act         = @cod_usuario
                  , fecha_act              = GETDATE()
                WHERE codigo_elu = @codigo_elu

                SET @cod = @codigo_elu
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_Evaluacion_Alumno
                SET estado_elu     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = GETDATE()
                WHERE codigo_elu = @codigo_elu

                SET @cod = @codigo_elu
            END

        SET @rpta = 1
        SET @msg = N'Se realizó la operación correctamente'

        IF @trancount = 1
            COMMIT

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

GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_IUD] TO iusrvirtualsistema
GO