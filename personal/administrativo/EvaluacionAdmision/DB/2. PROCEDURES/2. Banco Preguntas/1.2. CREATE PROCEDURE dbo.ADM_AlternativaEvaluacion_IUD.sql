/*  Usuario Crea:   andy.diaz
    Fecha:          27/08/2020
    Descripción:    Mantenimiento de tabla ADM_AlternativaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_AlternativaEvaluacion_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_ale INT = 0
    , @codigo_prv INT = 0
    , @orden_ale INT = 0
    , @textoTable_ale ADM_CHUNKVARCHARTYPE READONLY
    , @correcta_ale BIT = 1
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

        DECLARE @texto_ale AS VARCHAR(MAX) = ''
        IF @texto_ale IS NOT NULL
            BEGIN
                SELECT @texto_ale = COALESCE(@texto_ale + chunk, chunk)
                FROM @textoTable_ale;
            END

        IF @operacion = 'I'
            BEGIN
                IF exists(SELECT 1
                          FROM ADM_AlternativaEvaluacion WITH (NOLOCK)
                          WHERE codigo_ale = @codigo_ale)
                    BEGIN
                        SET @operacion = 'U'
                    END
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_AlternativaEvaluacion
                    ( codigo_prv
                    , orden_ale
                    , texto_ale
                    , correcta_ale
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_ale)
                VALUES
                    ( @codigo_prv
                    , @orden_ale
                    , @texto_ale
                    , @correcta_ale
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
                UPDATE ADM_AlternativaEvaluacion
                SET codigo_prv     = @codigo_prv
                  , orden_ale      = @orden_ale
                  , texto_ale      = @texto_ale
                  , correcta_ale   = @correcta_ale
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_ale = @codigo_ale

                SET @cod = @codigo_ale
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_AlternativaEvaluacion
                SET estado_ale     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_ale = @codigo_ale

                SET @cod = @codigo_ale
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

GRANT EXECUTE ON [dbo].[ADM_AlternativaEvaluacion_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_AlternativaEvaluacion_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_AlternativaEvaluacion_IUD] TO iusrvirtualsistema
GO
