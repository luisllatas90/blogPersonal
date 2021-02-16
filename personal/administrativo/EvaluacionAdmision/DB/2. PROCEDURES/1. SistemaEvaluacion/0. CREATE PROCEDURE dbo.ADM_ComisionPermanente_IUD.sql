/*  Usuario Crea:   andy.diaz
    Fecha:          23/08/2020
    Descripción:    Mantenimiento de tabla ADM_ComisionPermanente

    Historial de Cambios
    CODIGO		FECHA       DESARROLLADOR       DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_ComisionPermanente_IUD
    @operacion CHAR(1) = 'I'
    , @codigo_cop INT = 0
    , @codigo_per INT = 0
    , @codigo_ccm INT = 0
    , @nro_resolucion_cop VARCHAR(50) = ''
    , @vigente_cop BIT = 1
    , @codigos_com AS VARCHAR(50) = NULL
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
                IF EXISTS(SELECT 1
                          FROM ADM_ComisionPermanente WITH (NOLOCK)
                          WHERE codigo_cop = @codigo_cop)
                    BEGIN
                        SET @operacion = 'U'
                    END
            END

        -- Verifico si el personal ya está registrado como miembro de la comisión
        IF @codigo_cop = 0
            BEGIN
                SELECT
                    @codigo_cop = ISNULL(codigo_cop, @codigo_cop)
                  , @codigo_per = ISNULL(codigo_per, @codigo_per)
                  , @vigente_cop = vigente_cop
                FROM ADM_ComisionPermanente WITH (NOLOCK)
                WHERE codigo_per = @codigo_per
                  AND nro_resolucion_cop = @nro_resolucion_cop
                  AND estado_cop = 1

                IF @codigo_cop <> 0
                    BEGIN
                        IF @vigente_cop = 0
                            BEGIN
                                SET @operacion = 'U'
                                SET @vigente_cop = 1
                            END
                        ELSE
                            BEGIN
                                SET @rpta = 0;
                                SET @msg = N'El personal ya está registrado como miembro vigente de la comisión';
                                IF @trancount = 1
                                    ROLLBACK
                                RETURN;
                            END
                    END
            END

        -- Verifico si ya existe un miembro presidente de esa comisión
        IF @codigo_ccm = 1 --PRESIDENTE
            BEGIN
                IF EXISTS(SELECT cop.codigo_cop
                          FROM ADM_ComisionPermanente cop WITH (NOLOCK)
                          WHERE cop.nro_resolucion_cop = @nro_resolucion_cop
                            AND cop.codigo_ccm = 1 --PRESIDENTE
                            AND cop.codigo_cop <> @codigo_cop
                            AND estado_cop = 1)
                    BEGIN
                        SET @rpta = 0;
                        SET @msg = N'Ya existe un presidente para esta comisión';
                        IF @trancount = 1
                            ROLLBACK
                        RETURN;
                    END
            END

        --INSERT
        IF @operacion = 'I'
            BEGIN
                INSERT INTO ADM_ComisionPermanente
                    ( codigo_per
                    , codigo_ccm
                    , vigente_cop
                    , nro_resolucion_cop
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_cop)
                VALUES
                    ( @codigo_per
                    , @codigo_ccm
                    , @vigente_cop
                    , @nro_resolucion_cop
                    , @cod_usuario
                    , GETDATE()
                    , @cod_usuario
                    , GETDATE()
                    , 1);
                SET @rpta = 1
                SET @msg = N'Se realizó la operación correctamente'
                SET @cod = @@IDENTITY
                SET @codigo_cop = @cod
            END

        --UPDATE
        IF @operacion = 'U'
            BEGIN
                UPDATE ADM_ComisionPermanente
                SET codigo_per         = @codigo_per
                  , codigo_ccm         = @codigo_ccm
                  , vigente_cop        = @vigente_cop
                  , nro_resolucion_cop = @nro_resolucion_cop
                  , codigo_per_act     = @cod_usuario
                  , fecha_act          = GETDATE()
                WHERE codigo_cop = @codigo_cop

                SET @cod = @codigo_cop
            END

        --DELETE
        IF @operacion = 'D'
            BEGIN
                UPDATE ADM_ComisionPermanente
                SET estado_cop     = 0
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = GETDATE()
                WHERE codigo_cop = @codigo_cop

                SET @cod = @codigo_cop
            END

        --VIGENCIA
        IF @operacion = 'V'
            BEGIN
                UPDATE ADM_ComisionPermanente
                SET vigente_cop    = @vigente_cop
                  , codigo_per_act = @cod_usuario
                  , fecha_act      = GETDATE()
                WHERE codigo_cop = @codigo_cop
            END

        -- Relación con Competencias
        IF @codigos_com IS NOT NULL
            BEGIN
                UPDATE ADM_ComisionPermanente_Competencia
                SET estado_cpc     = CASE
                                         WHEN codigo_com IN (SELECT item
                                                             FROM fnSplit2(@codigos_com, ',')) THEN 1
                                         ELSE 0 END
                  , codigo_per_act = @cod_usuario
                WHERE codigo_cop = @codigo_cop

                INSERT INTO ADM_ComisionPermanente_Competencia
                    ( codigo_cop
                    , codigo_com
                    , codigo_per_reg
                    , fecha_reg
                    , codigo_per_act
                    , fecha_act
                    , estado_cpc)
                SELECT @codigo_cop, item, @cod_usuario, GETDATE(), @cod_usuario, GETDATE(), 1
                FROM fnSplit2(@codigos_com, ',')
                WHERE item NOT IN (
                    SELECT codigo_com
                    FROM ADM_ComisionPermanente_Competencia
                    WHERE codigo_cop = @codigo_cop
                )
            END
        --/ Relación con Competencias

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

GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_IUD] TO usuariogeneral
--GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_IUD] TO IusrReporting
--GRANT EXECUTE ON [dbo].[ADM_ComisionPermanente_IUD] TO iusrvirtualsistema
GO
