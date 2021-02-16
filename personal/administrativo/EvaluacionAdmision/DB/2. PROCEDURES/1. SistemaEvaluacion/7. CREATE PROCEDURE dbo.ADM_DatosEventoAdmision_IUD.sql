/*  Usuario Crea:   andy.diaz
    Fecha:          02/09/2020
    Descripción:    Mantenimiento de tabla ADM_DatosEventoAdmision

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_DatosEventoAdmision_IUD
    @operacion VARCHAR(3) = 'I'
    , @codigo_dea INT = 0
    , @codigo_cco INT = 0
    , @codigo_cac INT = 0
    , @fechaEvento_dea DATE = NULL
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

        IF @operacion IN ('I', 'UCA', 'UFE')
            BEGIN
                IF @codigo_dea = 0
                    BEGIN
                        SELECT @codigo_dea = isnull(dea.codigo_dea, @codigo_dea)
                        FROM ADM_DatosEventoAdmision dea WITH (NOLOCK)
                        WHERE 1 = 1
                          AND dea.codigo_cco = @codigo_cco
                          AND dea.codigo_cac = @codigo_cac
                          AND dea.fechaEvento_dea = @fechaEvento_dea
                          AND dea.estado_dea = 1
                    END

                IF @operacion = 'I' AND EXISTS(SELECT 1
                                               FROM ADM_DatosEventoAdmision WITH (NOLOCK)
                                               WHERE codigo_dea = @codigo_dea)
                    SET @operacion = 'U'
            END

        --INSERT
        IF @operacion = 'I' OR (@operacion IN ('UCA', 'UFE') AND @codigo_dea = 0)
            BEGIN
                INSERT INTO ADM_DatosEventoAdmision
                    ( codigo_cco
                    , codigo_cac
                    , fechaEvento_dea
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_dea)
                VALUES
                    ( @codigo_cco
                    , @codigo_cac
                    , @fechaEvento_dea
                    , @cod_usuario
                    , getdate()
                    , @cod_usuario
                    , getdate()
                    , 1);
                SET @rpta = 1
                SET @msg = N'Se realizó la operación correctamente'
                SET @cod = @@identity
                set @codigo_dea = @cod
            END

        --UPDATE
        IF @operacion = 'U'
            BEGIN
                UPDATE ADM_DatosEventoAdmision
                SET codigo_cco      = @codigo_cco
                  , codigo_cac      = @codigo_cac
                  , fechaEvento_dea = @fechaEvento_dea
                  , codigo_per_act  = @cod_usuario
                  , fecha_act       = getdate()
                WHERE codigo_dea = @codigo_dea

                SET @cod = @codigo_dea
            END

        --UPDATE CICLO ACADÉMICO
        IF @operacion = 'UCA'
            BEGIN
                UPDATE ADM_DatosEventoAdmision
                SET codigo_cac     = @codigo_cac
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_dea = @codigo_dea

                SET @cod = @codigo_dea
            END

        --UPDATE FECHA DE EVENTO
        IF @operacion = 'UFE'
            BEGIN
                UPDATE ADM_DatosEventoAdmision
                SET fechaEvento_dea = @fechaEvento_dea
                  , codigo_per_act  = @cod_usuario
                  , fecha_act       = getdate()
                WHERE codigo_dea = @codigo_dea

                SET @cod = @codigo_dea
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_DatosEventoAdmision
                SET estado_dea     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_dea = @codigo_dea

                SET @cod = @codigo_dea
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

GRANT EXECUTE ON [dbo].[ADM_DatosEventoAdmision_IUD] TO usuariogeneral
--GRANT EXECUTE ON [dbo].[ADM_DatosEventoAdmision_IUD] TO IusrReporting
--GRANT EXECUTE ON [dbo].[ADM_DatosEventoAdmision_IUD] TO iusrvirtualsistema
GO
