/*  Usuario Crea:   andy.diaz
    Fecha:          26/08/2020
    Descripción:    Mantenimiento de tabla ADM_PreguntaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_PreguntaEvaluacion_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_prv INT = 0
    , @codigo_ind INT = NULL
    , @codigo_ncp INT = NULL
    , @tipo_prv CHAR(1) = ''
    , @textoTable_prv ADM_CHUNKVARCHARTYPE READONLY
    , @codigo_raiz_prv INT = 0
    , @cantidad_prv INT = 1
    , @identificador_prv VARCHAR(20) = ''
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

        DECLARE @texto_prv VARCHAR(MAX) = ''
            ,@nombre_ind VARCHAR(150) = ''
            , @abreviatura_ncp CHAR(1) = ''

        IF @texto_prv IS NOT NULL
            BEGIN
                SELECT @texto_prv = COALESCE(@texto_prv + chunk, chunk)
                FROM @textoTable_prv;
            END

        IF @operacion = 'I'
            BEGIN
                IF exists(SELECT 1
                          FROM ADM_PreguntaEvaluacion WITH (NOLOCK)
                          WHERE codigo_prv = @codigo_prv)
                    BEGIN
                        SET @operacion = 'U'
                    END
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_PreguntaEvaluacion
                    ( codigo_ind
                    , codigo_ncp
                    , tipo_prv
                    , texto_prv
                    , codigo_raiz_prv
                    , cantidad_prv
                    , identificador_prv
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_prv)
                VALUES
                    ( @codigo_ind
                    , @codigo_ncp
                    , @tipo_prv
                    , @texto_prv
                    , @codigo_raiz_prv
                    , @cantidad_prv
                    , @identificador_prv
                    , @cod_usuario
                    , getdate()
                    , @cod_usuario
                    , getdate()
                    , 1);

                -- Genero el identificador de la pregunta
                SELECT @nombre_ind = ind.nombre_ind
                FROM ADM_Indicador ind WITH (NOLOCK)
                WHERE ind.codigo_ind = @codigo_ind

                SELECT @abreviatura_ncp = ncp.abreviatura_ncp
                FROM ADM_NivelComplejidadPregunta ncp WITH (NOLOCK)
                WHERE ncp.codigo_ncp = @codigo_ncp

                SET @identificador_prv = @nombre_ind + @abreviatura_ncp + CAST(@@identity AS VARCHAR(10))
                UPDATE ADM_PreguntaEvaluacion
                SET identificador_prv = @identificador_prv
                WHERE codigo_prv = @@identity

                SET @rpta = 1
                SET @msg = N'Se realizó la operación correctamente'
                SET @cod = @@identity
            END

        --UPDATE
        IF @operacion = 'U'
            BEGIN
                UPDATE ADM_PreguntaEvaluacion
                SET codigo_ind        = @codigo_ind
                  , codigo_ncp        = @codigo_ncp
                  , tipo_prv          = @tipo_prv
                  , texto_prv         = @texto_prv
                  , codigo_raiz_prv   = @codigo_raiz_prv
                  , cantidad_prv      = @cantidad_prv
                  , identificador_prv = @identificador_prv
                  , codigo_per_act    = @cod_usuario
                  , fecha_act         = getdate()
                WHERE codigo_prv = @codigo_prv

                SET @cod = @codigo_prv
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_PreguntaEvaluacion
                SET estado_prv     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_prv = @codigo_prv

                SET @cod = @codigo_prv
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

GRANT EXECUTE ON [dbo].[ADM_PreguntaEvaluacion_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_PreguntaEvaluacion_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_PreguntaEvaluacion_IUD] TO iusrvirtualsistema
GO