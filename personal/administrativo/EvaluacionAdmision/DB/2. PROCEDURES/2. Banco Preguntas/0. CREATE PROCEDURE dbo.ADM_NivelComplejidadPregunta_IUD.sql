/*  Usuario Crea:   andy.diaz
    Fecha:          02/09/2020
    Descripción:    Mantenimiento de tabla ADM_NivelComplejidadPregunta

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_NivelComplejidadPregunta_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_ncp INT = 0
    , @nombre_ncp NVARCHAR(50) = ''
    , @abreviatura_ncp CHAR(1) = ''
    , @descripcion_ncp NVARCHAR(250) = ''
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
                IF exists(SELECT 1
                          FROM ADM_NivelComplejidadPregunta WITH (NOLOCK)
                          WHERE codigo_ncp = @codigo_ncp)
                    BEGIN
                        SET @operacion = 'U'
                    END
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_NivelComplejidadPregunta
                    ( nombre_ncp
                    , abreviatura_ncp
                    , descripcion_ncp
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_ncp)
                VALUES
                    ( @nombre_ncp
                    , @abreviatura_ncp
                    , @descripcion_ncp
                    , @cod_usuario
                    , getdate()
                    , @cod_usuario
                    , getdate()
                    , 1);
                SET @rpta = 1
                SET @msg = N'Se realizó la operación correctamente'
                SET @cod = @@identity
            END

        --UPDATE
        IF @operacion = 'U'
            BEGIN
                UPDATE ADM_NivelComplejidadPregunta
                SET nombre_ncp      = @nombre_ncp
                  , abreviatura_ncp = @abreviatura_ncp
                  , descripcion_ncp = @descripcion_ncp
                  , codigo_per_act  = @cod_usuario
                  , fecha_act       = getdate()
                WHERE codigo_ncp = @codigo_ncp

                SET @cod = @codigo_ncp
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_NivelComplejidadPregunta
                SET estado_ncp     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_ncp = @codigo_ncp

                SET @cod = @codigo_ncp
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

GRANT EXECUTE ON [dbo].[ADM_NivelComplejidadPregunta_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_NivelComplejidadPregunta_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_NivelComplejidadPregunta_IUD] TO iusrvirtualsistema
GO