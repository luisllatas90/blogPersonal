/*  Usuario Crea:   andy.diaz
    Fecha:          28/09/2020
    Descripción:    Mantenimiento de tabla ADM_Configuracion_NotaMinima_Competencia

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Configuracion_NotaMinima_Competencia_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_cnc INT = 0
    , @codigo_cnm INT = 0
    , @codigo_com INT = 0
    , @nota_min_cnc INT = 0
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
                          FROM ADM_Configuracion_NotaMinima_Competencia WITH (NOLOCK)
                          WHERE codigo_cnc = @codigo_cnc)
                    BEGIN
                        SET @operacion = 'U'
                    END

                IF @codigo_cnc = 0
                    BEGIN
                        SELECT @codigo_cnc = cnc.codigo_cnc
                        FROM ADM_Configuracion_NotaMinima_Competencia cnc WITH (NOLOCK)
                        WHERE 1 = 1
                          AND cnc.codigo_com = @codigo_com
                          AND cnc.codigo_cnm = @codigo_cnm
                          AND cnc.estado_cnc = 1

                        IF @codigo_cnc <> 0
                            SET @operacion = 'U'
                    END
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_Configuracion_NotaMinima_Competencia
                    ( codigo_cnm
                    , codigo_com
                    , nota_min_cnc
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_cnc)
                VALUES
                    ( @codigo_cnm
                    , @codigo_com
                    , @nota_min_cnc
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
                UPDATE ADM_Configuracion_NotaMinima_Competencia
                SET codigo_cnm     = @codigo_cnm
                  , codigo_com     = @codigo_com
                  , nota_min_cnc   = @nota_min_cnc
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cnc = @codigo_cnc

                SET @cod = @codigo_cnc
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_Configuracion_NotaMinima_Competencia
                SET estado_cnc     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cnc = @codigo_cnc

                SET @cod = @codigo_cnc
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

GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Competencia_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Competencia_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_Competencia_IUD] TO iusrvirtualsistema
GO
