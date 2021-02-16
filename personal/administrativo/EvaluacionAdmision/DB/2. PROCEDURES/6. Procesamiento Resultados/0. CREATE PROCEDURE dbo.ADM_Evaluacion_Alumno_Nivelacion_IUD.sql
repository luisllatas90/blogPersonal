/*  Usuario Crea:   andy.diaz
    Fecha:          14/10/2020
    Descripción:    Mantenimiento de tabla ADM_Evaluacion_Alumno_Nivelacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Evaluacion_Alumno_Nivelacion_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_ean INT = 0
    , @codigo_elu INT = 0
    , @codigo_com INT = 0
    , @puntaje_ean NUMERIC(9, 2) = 0.00
    , @nota_ean NUMERIC(9, 2) = 0.00
    , @notaFinal_ean NUMERIC(9, 2) = 0.00
    , @estado_calificacion_ean CHAR(1) = ''
    , @necesita_nivelacion_ean BIT = 1
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
                IF @codigo_ean = 0
                    BEGIN
                        SELECT @codigo_ean = ISNULL(codigo_ean, @codigo_ean)
                        FROM ADM_Evaluacion_Alumno_Nivelacion WITH (NOLOCK)
                        WHERE codigo_elu = @codigo_elu
                          AND codigo_com = @codigo_com
                          AND estado_ean = 1
                    END

                IF EXISTS(SELECT 1
                          FROM ADM_Evaluacion_Alumno_Nivelacion WITH (NOLOCK)
                          WHERE codigo_ean = @codigo_ean)
                    SET @operacion = 'U'
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_Evaluacion_Alumno_Nivelacion
                    ( codigo_elu
                    , codigo_com
                    , puntaje_ean
                    , nota_ean
                    , notaFinal_ean
                    , estado_calificacion_ean
                    , necesita_nivelacion_ean
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_ean)
                VALUES
                    ( @codigo_elu
                    , @codigo_com
                    , @puntaje_ean
                    , @nota_ean
                    , @notaFinal_ean
                    , @estado_calificacion_ean
                    , @necesita_nivelacion_ean
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
                UPDATE ADM_Evaluacion_Alumno_Nivelacion
                SET codigo_elu              = @codigo_elu
                  , codigo_com              = @codigo_com
                  , puntaje_ean             = @puntaje_ean
                  , nota_ean                = @nota_ean
                  , notaFinal_ean           = @notaFinal_ean
                  , estado_calificacion_ean = @estado_calificacion_ean
                  , necesita_nivelacion_ean = @necesita_nivelacion_ean
                  , codigo_per_act          = @cod_usuario
                  , fecha_act               = GETDATE()
                WHERE codigo_ean = @codigo_ean

                SET @cod = @codigo_ean
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_Evaluacion_Alumno_Nivelacion
                SET estado_ean     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = GETDATE()
                WHERE codigo_ean = @codigo_ean

                SET @cod = @codigo_ean
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

GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Nivelacion_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Nivelacion_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Evaluacion_Alumno_Nivelacion_IUD] TO iusrvirtualsistema
GO
