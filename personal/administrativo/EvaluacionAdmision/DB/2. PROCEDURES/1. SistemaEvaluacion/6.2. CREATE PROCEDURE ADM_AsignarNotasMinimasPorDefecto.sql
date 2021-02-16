/*  Usuario Crea:   andy.diaz
    Fecha:          21/09/2020
    Descripción:

    Historial de Cambios
    CODIGO  FECHA    DESARROLLADOR   DESCRIPCIÓN
    001
*/
CREATE PROCEDURE ADM_AsignarNotasMinimasPorDefecto
    @codigo_test INT
    , @codigo_cco INT
    , @codigo_cpf INT = 0
    , @nota_min NUMERIC(9, 2) = 0.00
    , @nota_min_competencia NUMERIC(9, 2) = 0.00
    , @cod_usuario INT
    , @rpta INT = 0 OUTPUT
    , @msg VARCHAR(200) = '' OUTPUT
AS
BEGIN
    DECLARE @trancount BIT = 0

    BEGIN TRY
        IF @@TRANCOUNT = 0
            BEGIN
                BEGIN TRANSACTION
                    SET @trancount = 1
            END

        DECLARE @codigo_cnm INT, @codigo_com INT

        DECLARE cur_notas CURSOR FOR
            SELECT
                cpf.codigo_Cpf
              , ISNULL(cnm.codigo_cnm, 0) AS codigo_cnm
            FROM CarreraProfesional cpf WITH (NOLOCK)
                 LEFT JOIN ADM_Configuracion_NotaMinima cnm WITH (NOLOCK)
                           ON cpf.codigo_Cpf = cnm.codigo_cpf
                               AND (@codigo_cco = 0 OR cnm.codigo_cco = @codigo_cco) AND cnm.estado_cnm = 1
            WHERE 1 = 1
              AND (@codigo_test = 0 OR cpf.codigo_test = @codigo_test)
              AND (@codigo_cpf = 0 OR cpf.codigo_Cpf = @codigo_cpf)
              AND cpf.vigencia_Cpf = 1
              AND cpf.eliminado_cpf = 0
              AND cpf.codigo_Cpf NOT IN (407, 35)

        OPEN cur_notas
        FETCH NEXT FROM cur_notas INTO @codigo_cpf, @codigo_cnm
        WHILE @@FETCH_STATUS = 0
            BEGIN
                EXEC ADM_Configuracion_NotaMinima_IUD @operacion = 'I'
                    , @codigo_cnm = @codigo_cnm
                    , @codigo_cpf = @codigo_cpf
                    , @codigo_cco = @codigo_cco
                    , @nota_min_cnm = @nota_min
                    , @cod_usuario = @cod_usuario
                    , @rpta = @rpta OUTPUT
                    , @msg = @msg OUTPUT
                    , @cod = @codigo_cnm OUTPUT

                -- Limpio los valores antiguos
                DELETE
                FROM ADM_Configuracion_NotaMinima_Competencia
                WHERE codigo_cnm = @codigo_cnm

                DECLARE cur_notas_competencia CURSOR FOR
                    SELECT DISTINCT com.codigo_com
                    FROM CompetenciaAprendizaje com WITH (NOLOCK)
                         JOIN PerfilIngreso ping WITH (NOLOCK)
                              ON com.codigo_com = ping.codigo_com AND ping.estado_pIng = 1
                         JOIN PlanCurricular pcur WITH (NOLOCK)
                              ON ping.codigo_pcur = pcur.codigo_pcur AND pcur.vigente_pcur = 1 AND
                                 pcur.codigo_cpf = @codigo_cpf
                    WHERE com.codigo_tcom = 1
                      AND com.codigo_cat = 1
                      AND com.estado_com = 1
                    ORDER BY com.codigo_com

                OPEN cur_notas_competencia
                FETCH NEXT FROM cur_notas_competencia INTO @codigo_com
                WHILE @@FETCH_STATUS = 0
                    BEGIN
                        EXEC ADM_Configuracion_NotaMinima_Competencia_IUD @operacion = 'I'
                            , @codigo_cnc = 0
                            , @codigo_cnm = @codigo_cnm
                            , @codigo_com = @codigo_com
                            , @nota_min_cnc = @nota_min_competencia
                            , @cod_usuario = @cod_usuario
                            , @rpta = @rpta OUTPUT
                            , @msg = @msg OUTPUT

                        FETCH NEXT FROM cur_notas_competencia INTO @codigo_com

                        IF @rpta <> 1
                            BEGIN
                                RAISERROR (@msg, 16, 1);
                            END
                    END
                CLOSE cur_notas_competencia
                DEALLOCATE cur_notas_competencia

                FETCH NEXT FROM cur_notas INTO @codigo_cpf, @codigo_cnm

                IF @rpta <> 1
                    BEGIN
                        RAISERROR (@msg, 16, 1);
                    END
            END
        CLOSE cur_notas
        DEALLOCATE cur_notas

        IF @trancount = 1
            COMMIT

        SET @rpta = 1
        SET @msg = N'Se realizó la operación correctamente'
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

GRANT EXECUTE ON [dbo].[ADM_AsignarNotasMinimasPorDefecto] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_AsignarNotasMinimasPorDefecto] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_AsignarNotasMinimasPorDefecto] TO iusrvirtualsistema
GO