IF EXISTS(SELECT 1
          FROM INFORMATION_SCHEMA.ROUTINES
          WHERE ROUTINE_NAME = 'ADM_CierreResultadosAdmision_IUD'
            AND SPECIFIC_SCHEMA = 'dbo')
    DROP PROCEDURE ADM_CierreResultadosAdmision_IUD;
GO
/*  Usuario Crea:   andy.diaz
    Fecha:          26/10/2020
    Descripción:    Mantenimiento de tabla ADM_CierreResultadosAdmision

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_CierreResultadosAdmision_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_cra INT = 0
    , @codigo_cac INT = 0
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

        IF @operacion = 'I'
            BEGIN
                IF @codigo_cra = 0
                    BEGIN
                        SELECT @codigo_cra = isnull(codigo_cra, 0)
                        FROM ADM_CierreResultadosAdmision WITH (NOLOCK)
                        WHERE codigo_cac = @codigo_cac
                          AND codigo_min = @codigo_min
                          AND codigo_cpf = @codigo_cpf
                          AND codigo_cco = @codigo_cco
                          AND estado_cra = 1
                    END

                IF @operacion = 'I' AND exists(SELECT 1
                                               FROM ADM_CierreResultadosAdmision WITH (NOLOCK)
                                               WHERE codigo_cra = @codigo_cra)
                    SET @operacion = 'U'
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_CierreResultadosAdmision
                    ( codigo_cac
                    , codigo_min
                    , codigo_cpf
                    , codigo_cco
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_cra)
                VALUES
                    ( @codigo_cac
                    , @codigo_min
                    , @codigo_cpf
                    , @codigo_cco
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
                UPDATE ADM_CierreResultadosAdmision
                SET codigo_cac     = @codigo_cac
                  , codigo_min     = @codigo_min
                  , codigo_cpf     = @codigo_cpf
                  , codigo_cco     = @codigo_cco
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cra = @codigo_cra

                SET @cod = @codigo_cra
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_CierreResultadosAdmision
                SET estado_cra     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cra = @codigo_cra

                SET @cod = @codigo_cra
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

GRANT EXECUTE ON [dbo].[ADM_CierreResultadosAdmision_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_CierreResultadosAdmision_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_CierreResultadosAdmision_IUD] TO iusrvirtualsistema
GO