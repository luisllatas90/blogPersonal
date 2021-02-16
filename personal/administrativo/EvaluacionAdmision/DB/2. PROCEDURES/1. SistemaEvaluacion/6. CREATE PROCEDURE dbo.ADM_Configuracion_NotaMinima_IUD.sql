/*  Usuario Crea:   andy.diaz
    Fecha:          28/09/2020
    Descripción:    Mantenimiento de tabla ADM_Configuracion_NotaMinima

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_Configuracion_NotaMinima_IUD
    @operacion VARCHAR(3) = 'I'
    , @codigo_cnm INT = 0
    , @codigo_cpf INT = 0
    , @codigo_cco INT = 0
    , @nota_min_cnm NUMERIC(9, 2) = 0.00
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

        IF @operacion IN ('I', 'UNM')
            BEGIN
                IF @codigo_cnm = 0
                    BEGIN
                        SELECT @codigo_cnm = isnull(cnm.codigo_cnm, @codigo_cnm)
                        FROM ADM_Configuracion_NotaMinima cnm WITH (NOLOCK)
                        WHERE 1 = 1
                          AND cnm.codigo_cpf = @codigo_cpf
                          AND cnm.codigo_cco = @codigo_cco
                          AND cnm.estado_cnm = 1
                    END

                IF @operacion = 'I' AND (exists(SELECT 1
                                                FROM ADM_Configuracion_NotaMinima WITH (NOLOCK)
                                                WHERE codigo_cnm = @codigo_cnm))
                    SET @operacion = 'U'
            END

        --INSERT
        IF @operacion = 'I' OR (@operacion IN ('UNM') AND @codigo_cnm = 0)
            BEGIN
                INSERT INTO ADM_Configuracion_NotaMinima
                    ( codigo_cpf
                    , codigo_cco
                    , nota_min_cnm
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_cnm)
                VALUES
                    ( @codigo_cpf
                    , @codigo_cco
                    , @nota_min_cnm
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
                UPDATE ADM_Configuracion_NotaMinima
                SET codigo_cpf     = @codigo_cpf
                  , codigo_cco     = @codigo_cco
                  , nota_min_cnm   = @nota_min_cnm
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cnm = @codigo_cnm

                SET @cod = @codigo_cnm
            END

        --UPDATE NOTA MÍNIMA
        IF @operacion = 'UNM'
            BEGIN
                UPDATE ADM_Configuracion_NotaMinima
                SET nota_min_cnm   = @nota_min_cnm
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cnm = @codigo_cnm

                SET @cod = @codigo_cnm
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_Configuracion_NotaMinima
                SET estado_cnm     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_cnm = @codigo_cnm

                SET @cod = @codigo_cnm
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

GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_Configuracion_NotaMinima_IUD] TO iusrvirtualsistema
GO
