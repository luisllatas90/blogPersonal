/*  Usuario Crea:   andy.diaz
    Fecha:          11/09/2020
    Descripción:    Mantenimiento de tabla ADM_AsistenciaEvaluacion

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_AsistenciaEvaluacion_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_ase INT = 0
    , @codigo_gru INT = 0
    , @codigo_alu INT = 0
    , @estadoAsistencia_ase CHAR(1) = ''
    , @fechaCierre_ase DATETIME = NULL
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
                          FROM ADM_AsistenciaEvaluacion WITH (NOLOCK)
                          WHERE codigo_ase = @codigo_ase)
                    BEGIN
                        SET @operacion = 'U'
                    END

                SELECT @codigo_ase = isnull(ase.codigo_ase, @codigo_ase)
                FROM ADM_AsistenciaEvaluacion ase WITH (NOLOCK)
                WHERE ase.codigo_alu = @codigo_alu
                  AND ase.codigo_gru = @codigo_gru
                  AND ase.estado_ase = 1

                IF @codigo_ase <> 0
                    SET @operacion = 'U'
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_AsistenciaEvaluacion
                    ( codigo_gru
                    , codigo_alu
                    , estadoAsistencia_ase
                    , fechaCierre_ase
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_ase)
                VALUES
                    ( @codigo_gru
                    , @codigo_alu
                    , @estadoAsistencia_ase
                    , @fechaCierre_ase
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
                UPDATE ADM_AsistenciaEvaluacion
                SET codigo_gru           = @codigo_gru
                  , codigo_alu           = @codigo_alu
                  , estadoAsistencia_ase = @estadoAsistencia_ase
                  , fechaCierre_ase      = @fechaCierre_ase
                  , codigo_per_act       = @cod_usuario
                  , fecha_act            = getdate()
                WHERE codigo_ase = @codigo_ase

                SET @cod = @codigo_ase
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_AsistenciaEvaluacion
                SET estado_ase     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = getdate()
                WHERE codigo_ase = @codigo_ase

                SET @cod = @codigo_ase
            END

        --CERRAR ASISTENCIAS
        IF @operacion = 'C'
            BEGIN
                UPDATE ADM_AsistenciaEvaluacion
                SET fechaCierre_ase = @fechaCierre_ase
                WHERE codigo_gru = @codigo_gru
                  AND estado_ase = 1
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

GRANT EXECUTE ON [dbo].[ADM_AsistenciaEvaluacion_IUD] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_AsistenciaEvaluacion_IUD] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_AsistenciaEvaluacion_IUD] TO iusrvirtualsistema
GO