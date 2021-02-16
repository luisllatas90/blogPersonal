/*  Usuario Crea:   andy.diaz
    Fecha:          25/08/2020
    Descripción:    Mantenimiento de tabla ADM_VacantesEvento

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_VacantesEvento_IUD
    @operacion VARCHAR(5) = 'I'
    , @codigo_vae INT = 0
    , @codigo_cco INT = 0
    , @codigo_vac INT = 0
    , @cantidad_vae INT = 0
    , @cantidad_accesitarios_vae INT = 0
    , @cod_usuario INT
    , @rpta INT = 0 OUTPUT
    , @msg VARCHAR(200) = '' OUTPUT
    , @cod INT = 0 OUTPUT
AS
BEGIN
    DECLARE @trancount BIT = 0
    DECLARE @total_vacantes INT = 0
    DECLARE @vacantes_ocupadas INT = 0

    BEGIN TRY
        IF @@TRANCOUNT = 0
            BEGIN
                BEGIN TRANSACTION
                    SET @trancount = 1
            END

        IF @operacion IN ('I', 'UV', 'UA')
            BEGIN
                IF @codigo_vae = 0
                    BEGIN
                        SELECT @codigo_vae = isnull(vae.codigo_vae, @codigo_vae)
                        FROM ADM_VacantesEvento vae WITH (NOLOCK)
                        WHERE vae.codigo_vac = @codigo_vac
                          AND vae.codigo_cco = @codigo_cco
                          AND vae.estado_vae = 1
                    END

                IF @operacion = 'I' AND EXISTS(SELECT 1
                                               FROM ADM_VacantesEvento WITH (NOLOCK)
                                               WHERE codigo_vae = @codigo_vae)
                    SET @operacion = 'U'
            END

        -- Verifico que las vacantes no excedan
        IF @operacion IN ('I', 'UV', 'U')
            BEGIN
                SELECT @vacantes_ocupadas = isnull(sum(vae.cantidad_vae), 0)
                FROM ADM_VacantesEvento vae WITH (NOLOCK)
                WHERE 1 = 1
                  AND vae.codigo_vac = @codigo_vac
                  AND vae.codigo_vae <> @codigo_vae
                  AND vae.estado_vae = 1

                SELECT @total_vacantes = vac.numero_vac
                FROM Vacantes vac WITH (NOLOCK)
                WHERE vac.codigo_Vac = @codigo_vac

                IF @total_vacantes < @vacantes_ocupadas + @cantidad_vae
                    BEGIN
                        SET @rpta = 0;
                        SET @msg = N'Se ha sobrepasado el límite de vacantes. Restantes: ' +
                                   cast(@total_vacantes - @vacantes_ocupadas AS VARCHAR(10));
                        IF @trancount = 1
                            ROLLBACK
                        RETURN;

                    END
            END

        --INSERT
        IF @operacion = 'I' OR (@operacion IN ('UV', 'UA') AND @codigo_vae = 0)
            BEGIN
                INSERT INTO ADM_VacantesEvento
                    ( codigo_cco
                    , codigo_vac
                    , cantidad_vae
                    , cantidad_accesitarios_vae
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_vae)
                VALUES
                    ( @codigo_cco
                    , @codigo_vac
                    , @cantidad_vae
                    , @cantidad_accesitarios_vae
                    , @cod_usuario
                    , getdate()
                    , @cod_usuario
                    , getdate()
                    , 1);
                SET @rpta = 1
                SET @msg = N'Se realizó la operación correctamente'
                SET @cod = @@identity
                SET @codigo_vae = @cod
            END

        --UPDATE
        IF @operacion = 'U'
            BEGIN
                UPDATE ADM_VacantesEvento
                SET codigo_cco                = @codigo_cco
                  , codigo_vac                = @codigo_vac
                  , cantidad_vae              = @cantidad_vae
                  , cantidad_accesitarios_vae = @cantidad_accesitarios_vae
                  , codigo_per_act            = @cod_usuario
                  , fecha_act                 = getdate()
                WHERE codigo_vae = @codigo_vae

                SET @cod = @codigo_vae
            END

        --UPDATE CANTIDAD DE VACANTES
        IF @operacion = 'UV'
            BEGIN
                UPDATE ADM_VacantesEvento
                SET cantidad_vae   = @cantidad_vae
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_vae = @codigo_vae

                SET @cod = @codigo_vae
            END

        --UPDATE CANTIDAD DE ACCESITARIOS
        IF @operacion = 'UA'
            BEGIN
                UPDATE ADM_VacantesEvento
                SET cantidad_accesitarios_vae = @cantidad_accesitarios_vae
                  , codigo_per_act            = @cod_usuario
                  , fecha_act                 = getdate()
                WHERE codigo_vae = @codigo_vae

                SET @cod = @codigo_vae
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_VacantesEvento
                SET estado_vae     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_vae = @codigo_vae

                SET @cod = @codigo_vae
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

GRANT EXECUTE ON [dbo].[ADM_VacantesEvento_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_VacantesEvento_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_VacantesEvento_IUD] TO iusrvirtualsistema
GO