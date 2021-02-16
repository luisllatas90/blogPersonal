/*  Usuario Crea:   andy.diaz
    Fecha:          01/10/2020
    Descripción:

    Historial de Cambios
    CODIGO  FECHA    DESARROLLADOR   DESCRIPCIÓN
*/
CREATE PROCEDURE dbo.ADM_CalcularNotasEvaluacion
    @codigo_evl INT = 0
    , @codigo_cco INT = 0
    , @codigo_min INT = 0
    , @codigo_cpf INT = 0
    , @cod_usuario INT = 0
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

        IF @codigo_evl = 0 AND @codigo_cco = 0
            BEGIN
                SET @rpta = 0;
                SET @msg = 'Debe especificar el codigo_evl o el codigo_cco';
                RAISERROR (@msg, 16, 1);
            END

        IF @codigo_cco = 0
            BEGIN
                SELECT @codigo_cco = evl.codigo_cco
                FROM ADM_Evaluacion evl WITH (NOLOCK)
                WHERE evl.codigo_evl = @codigo_evl
            END

        -- Variables para calcular puntaje y nota del postulante
        DECLARE @puntaje_total NUMERIC(8, 3) = 0
            , @puntaje_alumno NUMERIC(8, 3)
            , @correcta_ear BIT, @puntaje_ear NUMERIC(8, 3)
            , @suma_pesos_competencia NUMERIC(8, 3) = 0
            , @nota_proporcional NUMERIC(8, 3) = 20

        -- Variables para calcular puntaje y nota del postulante por competencia
        DECLARE @puntaje_total_competencia NUMERIC(8, 3) = 0
            , @puntaje_alumno_competencia NUMERIC(8, 3)
            , @nota_alumno_competencia NUMERIC(8, 3)
            , @nota_min_competencia NUMERIC(8, 3)
            , @necesita_nivelacion_ean BIT = 0

        -- Variables para determinar el puntaje de cada respuesta
        DECLARE @codigo_com INT
            , @peso_pcom NUMERIC(8, 2)
            , @abreviatura_ncp CHAR(1)
            , @peso_basica_tev NUMERIC(8, 2)
            , @peso_intermedia_tev NUMERIC(8, 2)
            , @peso_avanzada_tev NUMERIC(8, 2)
            , @codigo_cee INT
            , @cantidad_cee INT

        -- Variables para calcular puntajes y notas finales
        DECLARE @nota_elu DECIMAL(8, 3) = 0
            , @puntaje_elu DECIMAL(8, 3) = 0
            , @peso_ceep DECIMAL(8, 2) = 0
            , @notaFinal_elu DECIMAL(8, 3) = 0
            , @puntajeFinal_elu DECIMAL(8, 3) = 0

        -- Otras variables
        DECLARE @codigo_alu INT
            , @codigo_cac INT = 0
            , @codigo_elu INT
            , @codigo_ear INT
            , @descripcion_cac VARCHAR(10)
            , @nombre_com VARCHAR(250)
            , @nombre_cpf VARCHAR(250)
            , @nombre_evl VARCHAR(100) = ''

        -- Tablas temporales
        IF OBJECT_ID('tempdb..#notas_minimas_nivelacion') IS NOT NULL
            DROP TABLE #notas_minimas_nivelacion

        CREATE TABLE #notas_minimas_nivelacion (
            codigo_cpf INT,
            codigo_com INT,
            nota_com NUMERIC(8, 3)
        )

        IF OBJECT_ID('tempdb..#puntaje_total_competencia') IS NOT NULL
            DROP TABLE #puntaje_total_competencia

        CREATE TABLE #puntaje_total_competencia (
            codigo_evl INT,
            codigo_com INT,
            puntaje_total NUMERIC(8, 3),
        )

        IF OBJECT_ID('tempdb..#puntaje_alumno_competencia') IS NOT NULL
            DROP TABLE #puntaje_alumno_competencia

        CREATE TABLE #puntaje_alumno_competencia (
            codigo_elu INT,
            codigo_com INT,
            puntaje_alumno NUMERIC(8, 3),
        )

        -- Tablas temporales
        IF OBJECT_ID('tempdb..#notas_finales_nivelacion') IS NOT NULL
            DROP TABLE #notas_finales_nivelacion

        CREATE TABLE #notas_finales_nivelacion (
            codigo_alu INT,
            codigo_com INT,
            notaFinal NUMERIC(8, 3)
        )

        INSERT INTO #notas_minimas_nivelacion
        SELECT cnm.codigo_cpf, cnc.codigo_com, cnc.nota_min_cnc
        FROM ADM_Configuracion_NotaMinima cnm WITH (NOLOCK)
             JOIN ADM_Configuracion_NotaMinima_Competencia cnc WITH (NOLOCK)
                  ON cnm.codigo_cnm = cnc.codigo_cnm AND cnc.estado_cnc = 1
        WHERE cnm.codigo_cco = @codigo_cco
          AND cnm.estado_cnm = 1

        SELECT @codigo_cac = dea.codigo_cac
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN CentroCostos cco WITH (NOLOCK) ON evl.codigo_cco = cco.codigo_Cco
             JOIN ADM_DatosEventoAdmision dea WITH (NOLOCK) ON cco.codigo_Cco = dea.codigo_cco AND dea.estado_dea = 1
        WHERE 1 = 1
          AND (@codigo_evl = 0 OR evl.codigo_evl = @codigo_evl)
          AND (@codigo_Cco = 0 OR cco.codigo_Cco = @codigo_Cco)

        --------------------------------------------------------------------------
        -- 1. Obtengo los puntajes máximos por evaluación y competencia
        --------------------------------------------------------------------------
        INSERT INTO #puntaje_total_competencia
        SELECT
            evl.codigo_evl
          , scom.codigo_com
          , SUM(CASE ncp.abreviatura_ncp
                    WHEN 'B' THEN tev.peso_basica_tev
                    WHEN 'I' THEN tev.peso_intermedia_tev
                    WHEN 'A' THEN tev.peso_avanzada_tev END) AS puntaje_maximo
        FROM ADM_Evaluacion evl WITH (NOLOCK)
             JOIN ADM_TipoEvaluacion tev WITH (NOLOCK) ON evl.codigo_tev = tev.codigo_tev
             JOIN ADM_EvaluacionDetalle evd WITH (NOLOCK) ON evl.codigo_evl = evd.codigo_evl AND evd.estado_evd = 1
             JOIN ADM_Indicador ind WITH (NOLOCK) ON evd.codigo_ind = ind.codigo_ind
             JOIN ADM_SubCompetencia scom WITH (NOLOCK) ON ind.codigo_scom = scom.codigo_scom
             JOIN ADM_NivelComplejidadPregunta ncp WITH (NOLOCK) ON evd.codigo_ncp = ncp.codigo_ncp
        WHERE 1 = 1
          AND (@codigo_evl = 0 OR evl.codigo_evl = @codigo_evl)
        GROUP BY evl.codigo_evl, scom.codigo_com

        ------------------------------------------------------------------------------
        -- 2. Recorro la lista de evaluaciones por alumno para obtener los resultados
        ------------------------------------------------------------------------------
        DECLARE cur_eval_alumno CURSOR FOR
            SELECT
                alu.codigo_Alu
              , alu.tempcodigo_cpf
              , evl.codigo_evl
              , elu.codigo_elu
              , tev.peso_basica_tev
              , tev.peso_intermedia_tev
              , tev.peso_avanzada_tev
              , cee.codigo_cee
              , cee.cantidad_cee
            FROM Alumno alu WITH (NOLOCK)
                 JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK) ON alu.codigo_Alu = elu.codigo_alu AND elu.estado_elu = 1
                 JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl AND evl.estado_evl = 1
                 JOIN ADM_TipoEvaluacion tev WITH (NOLOCK) ON evl.codigo_tev = tev.codigo_tev
                 OUTER APPLY (SELECT TOP 1
                                  ISNULL(_cee.codigo_cee, 0)   AS codigo_cee
                                , ISNULL(_cee.cantidad_cee, 0) AS cantidad_cee
                              FROM ADM_ConfiguracionEvaluacionEvento _cee WITH (NOLOCK)
                              WHERE _cee.codigo_cco = evl.codigo_cco
                                AND _cee.codigo_cpf = alu.tempcodigo_cpf
                                AND _cee.codigo_tev = evl.codigo_tev
                                AND _cee.estado_cee = 1) cee
            WHERE 1 = 1
              AND (@codigo_evl = 0 OR evl.codigo_evl = @codigo_evl)
              AND (@codigo_cco = 0 OR evl.codigo_cco = @codigo_cco)
              AND (@codigo_min = 0 OR alu.codigo_Min = @codigo_min)
              AND (@codigo_cpf = 0 OR alu.tempcodigo_cpf = @codigo_cpf)

        OPEN cur_eval_alumno
        FETCH NEXT FROM cur_eval_alumno INTO @codigo_alu, @codigo_cpf, @codigo_evl, @codigo_elu, @peso_basica_tev, @peso_intermedia_tev, @peso_avanzada_tev, @codigo_cee, @cantidad_cee
        WHILE @@FETCH_STATUS = 0
            BEGIN
                SET @puntaje_alumno = 0

                SELECT @puntaje_total = SUM(pt.puntaje_total)
                FROM #puntaje_total_competencia pt
                WHERE 1 = 1
                  AND pt.codigo_evl = @codigo_evl

                IF @puntaje_total = 0
                    BEGIN
                        SELECT @nombre_evl = evl.nombre_evl
                        FROM ADM_Evaluacion evl WITH (NOLOCK)
                        WHERE evl.codigo_evl = @codigo_evl

                        SELECT @nombre_cpf = cpf.nombre_Cpf
                        FROM CarreraProfesional cpf WITH (NOLOCK)
                        WHERE cpf.codigo_Cpf = @codigo_cpf

                        SET @rpta = 0;
                        SET @msg = N'El puntaje máximo para la evaluación: ' + @nombre_evl +
                                   ', carrera: ' + @nombre_cpf +
                                   N' es: 0, verifique los pesos para el tipo de evaluación';
                        RAISERROR (@msg, 16, 1);
                    END

                -- 2.1. Recorro las respuestas por cada alumno y calculo los puntajes por competencia
                ---------------------------------------------------------------------------------------
                DECLARE cur_respuestas CURSOR FOR
                    SELECT ear.codigo_ear, scom.codigo_com, ncp.abreviatura_ncp
                    FROM ADM_Evaluacion_Alumno_Respuesta ear WITH (NOLOCK)
                         JOIN ADM_EvaluacionDetalle evd WITH (NOLOCK) ON ear.codigo_evd = evd.codigo_evd
                         JOIN ADM_Indicador ind WITH (NOLOCK) ON evd.codigo_ind = ind.codigo_ind
                         JOIN ADM_SubCompetencia scom WITH (NOLOCK) ON ind.codigo_scom = scom.codigo_scom
                         JOIN ADM_NivelComplejidadPregunta ncp WITH (NOLOCK) ON evd.codigo_ncp = ncp.codigo_ncp
                    WHERE ear.codigo_elu = @codigo_elu
                      AND ear.estado_ear = 1

                OPEN cur_respuestas
                FETCH NEXT FROM cur_respuestas INTO @codigo_ear, @codigo_com, @abreviatura_ncp
                WHILE @@FETCH_STATUS = 0
                    BEGIN
                        SELECT
                                @puntaje_ear = CASE
                                                   WHEN ear.correcta_ear = 1
                                                       THEN CASE @abreviatura_ncp
                                                                WHEN 'B' THEN @peso_basica_tev
                                                                WHEN 'I'
                                                                    THEN @peso_intermedia_tev
                                                                WHEN 'A'
                                                                    THEN @peso_avanzada_tev END
                                                   ELSE 0 END
                          ,     @correcta_ear = ear.correcta_ear
                        FROM ADM_Evaluacion_Alumno_Respuesta ear WITH (NOLOCK)
                        WHERE ear.codigo_ear = @codigo_ear

                        UPDATE ADM_Evaluacion_Alumno_Respuesta
                        SET puntaje_ear    = @puntaje_ear
                          , correcta_ear   = @correcta_ear
                          , codigo_per_act = @cod_usuario
                        WHERE codigo_ear = @codigo_ear

                        SET @puntaje_alumno = @puntaje_alumno + @puntaje_ear

                        IF NOT EXISTS(SELECT codigo_elu
                                      FROM #puntaje_alumno_competencia
                                      WHERE codigo_elu = @codigo_elu
                                        AND codigo_com = @codigo_com)
                            BEGIN
                                INSERT INTO #puntaje_alumno_competencia
                                    ( codigo_elu
                                    , codigo_com
                                    , puntaje_alumno)
                                VALUES
                                    ( @codigo_elu
                                    , @codigo_com
                                    , @puntaje_ear);
                            END
                        ELSE
                            BEGIN
                                UPDATE #puntaje_alumno_competencia
                                SET puntaje_alumno = puntaje_alumno + @puntaje_ear
                                WHERE codigo_elu = @codigo_elu
                                  AND codigo_com = @codigo_com
                            END

                        FETCH NEXT FROM cur_respuestas INTO @codigo_ear, @codigo_com, @abreviatura_ncp
                    END
                CLOSE cur_respuestas
                DEALLOCATE cur_respuestas

                -- 2.1. Calculo los notas por competencia y la nota del alumno en su evaluación
                -----------------------------------------------------------------------------------
                DECLARE cur_puntajes_competencia CURSOR FOR
                    SELECT pac.codigo_com, pac.puntaje_alumno, ptc.puntaje_total, pcom.peso_pcom
                    FROM #puntaje_alumno_competencia pac
                         OUTER APPLY (SELECT TOP 1 _ptc.puntaje_total
                                      FROM #puntaje_total_competencia _ptc
                                      WHERE 1 = 1
                                        AND _ptc.codigo_com = pac.codigo_com
                                        AND _ptc.codigo_evl = @codigo_evl) ptc
                         OUTER APPLY (SELECT TOP 1 _pcom.codigo_pcom, _pcom.peso_pcom
                                      FROM ADM_PesoCompetencia _pcom WITH (NOLOCK)
                                      WHERE 1 = 1
                                        AND _pcom.codigo_com = pac.codigo_com
                                        AND _pcom.codigo_cpf = @codigo_cpf
                                        AND _pcom.codigo_cac = @codigo_cac
                                        AND _pcom.estado_pcom = 1) pcom
                    WHERE pac.codigo_elu = @codigo_elu

                SET @nota_elu = 0
                SET @suma_pesos_competencia = 0

                OPEN cur_puntajes_competencia
                FETCH NEXT FROM cur_puntajes_competencia INTO @codigo_com, @puntaje_alumno_competencia, @puntaje_total_competencia, @peso_pcom
                WHILE @@FETCH_STATUS = 0
                    BEGIN
                        IF @peso_pcom IS NULL
                            BEGIN
                                SELECT @descripcion_cac = cac.descripcion_Cac
                                FROM CicloAcademico cac WITH (NOLOCK)
                                WHERE cac.codigo_Cac = @codigo_cac

                                SELECT @nombre_cpf = cpf.nombre_Cpf
                                FROM CarreraProfesional cpf WITH (NOLOCK)
                                WHERE codigo_Cpf = @codigo_cpf

                                SELECT @nombre_com = com.nombre_com
                                FROM CompetenciaAprendizaje com WITH (NOLOCK)
                                WHERE com.codigo_com = @codigo_com

                                SET @rpta = 0;
                                SET @msg =
                                            N'No se ha encontrado una configuración de pesos por competencia para la carrera: ' +
                                            @nombre_cpf + ', competencia: ' + @nombre_com + ', semestre: ' +
                                            @descripcion_cac +
                                            '. Verifique que el valor ingresado sea diferente de cero';
                                RAISERROR (@msg, 16, 1);
                            END

                        SET @nota_alumno_competencia = (@puntaje_alumno_competencia / @puntaje_total_competencia) *
                                                       @nota_proporcional

                        SET @nota_elu = @nota_elu + (@peso_pcom * @nota_alumno_competencia)
                        SET @suma_pesos_competencia = @suma_pesos_competencia + @peso_pcom

                        SET @nota_min_competencia = NULL

                        SELECT @nota_min_competencia = nota_com
                        FROM #notas_minimas_nivelacion
                        WHERE codigo_cpf = @codigo_cpf
                          AND codigo_com = @codigo_com

                        IF @nota_min_competencia IS NULL
                            BEGIN
                                SELECT @nombre_com = com.nombre_com
                                FROM CompetenciaAprendizaje com WITH (NOLOCK)
                                WHERE com.codigo_com = @codigo_com

                                SELECT @nombre_cpf = cpf.nombre_Cpf
                                FROM CarreraProfesional cpf WITH (NOLOCK)
                                WHERE codigo_Cpf = @codigo_cpf

                                SET @rpta = 0;
                                SET @msg = N'No se ha encontrado una configuración de nota mínima para la carrera: ' +
                                           ISNULL(@nombre_cpf, '') + ', competencia: ' + ISNULL(@nombre_com, '');

                                RAISERROR (@msg, 16, 1);
                            END

                        IF @nota_alumno_competencia >= @nota_min_competencia
                            SET @necesita_nivelacion_ean = 0
                        ELSE
                            SET @necesita_nivelacion_ean = 1

                        EXEC ADM_Evaluacion_Alumno_Nivelacion_IUD @operacion = 'I'
                            , @codigo_elu = @codigo_elu
                            , @codigo_com = @codigo_com
                            , @puntaje_ean = @puntaje_alumno_competencia
                            , @nota_ean = @nota_alumno_competencia
                            , @notaFinal_ean = @nota_alumno_competencia
                            , @estado_calificacion_ean = 'P'
                            , @necesita_nivelacion_ean = @necesita_nivelacion_ean
                            , @cod_usuario = @cod_usuario
                            , @rpta = @rpta OUTPUT
                            , @msg = @msg OUTPUT

                        FETCH NEXT FROM cur_puntajes_competencia INTO @codigo_com, @puntaje_alumno_competencia, @puntaje_total_competencia, @peso_pcom

                        IF @rpta <> 1
                            RAISERROR (@msg, 16, 1);
                    END
                CLOSE cur_puntajes_competencia
                DEALLOCATE cur_puntajes_competencia

                IF @suma_pesos_competencia = 0
                    BEGIN
                        SET @rpta = 0;
                        SET @msg = 'La suma de pesos por competencia es igual a cero';
                        RAISERROR (@msg, 16, 1);
                    END

                SET @nota_elu = @nota_elu / @suma_pesos_competencia

                UPDATE ADM_Evaluacion_Alumno
                SET puntaje_elu    = @puntaje_alumno
                  , nota_elu       = @nota_elu
                  , codigo_per_act = @cod_usuario
                WHERE codigo_elu = @codigo_elu

                -- 2.1. Notas finales (en caso de varias evaluaciones por evento)
                -----------------------------------------------------------------------------------
                IF @cantidad_cee <= 1
                    BEGIN
                        -- Hay una (o ninguna) evaluación para este tipo, la nota final será igual a la nota de la evaluación
                        UPDATE ADM_Evaluacion_Alumno
                        SET notaFinal_elu    = nota_elu
                          , puntajeFinal_elu = puntaje_elu
                          , codigo_per_act   = @cod_usuario
                        WHERE codigo_elu = @codigo_elu
                    END
                ELSE
                    BEGIN
                        SELECT @notaFinal_elu = 0, @puntajeFinal_elu = 0

                        DECLARE cur_notas_finales CURSOR FOR
                            SELECT elu.nota_elu, elu.puntaje_elu, ISNULL(ceep.peso_ceep, 0)
                            FROM (SELECT
                                      ROW_NUMBER() OVER (ORDER BY _elu.fecha_reg) AS orden
                                    , _elu.nota_elu
                                    , _elu.puntaje_elu
                                  FROM ADM_Evaluacion_Alumno _elu WITH (NOLOCK)
                                       JOIN ADM_Evaluacion _evl WITH (NOLOCK) ON _elu.codigo_evl = _evl.codigo_evl
                                  WHERE _evl.codigo_cco = codigo_cco
                                    AND _elu.codigo_alu = @codigo_alu
                                    AND _elu.estado_elu = 1) elu
                                 CROSS APPLY (SELECT TOP 1 _ceep.peso_ceep
                                              FROM ADM_ConfiguracionEvaluacionEvento_Peso _ceep WITH (NOLOCK)
                                                   JOIN ADM_ConfiguracionEvaluacionEvento _cee WITH (NOLOCK)
                                                        ON _ceep.codigo_cee = _cee.codigo_cee
                                              WHERE _ceep.estado_ceep = 1
                                                AND _cee.codigo_cee = @codigo_cee
                                                AND _ceep.nro_orden_ceep = elu.orden) ceep
                            ORDER BY elu.orden

                        OPEN cur_notas_finales
                        FETCH NEXT FROM cur_notas_finales INTO @nota_elu, @puntaje_elu, @peso_ceep

                        WHILE @@FETCH_STATUS = 0
                            BEGIN
                                SET @notaFinal_elu = @notaFinal_elu + (@nota_elu * @peso_ceep)
                                SET @puntajeFinal_elu = @puntajeFinal_elu + (@puntaje_elu * @peso_ceep)
                                FETCH NEXT FROM cur_notas_finales INTO @nota_elu, @puntaje_elu, @peso_ceep
                            END

                        CLOSE cur_notas_finales
                        DEALLOCATE cur_notas_finales

                        UPDATE elu
                        SET notaFinal_elu    = @notaFinal_elu
                          , puntajeFinal_elu = @puntajeFinal_elu
                        FROM ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                             JOIN ADM_Evaluacion evl WITH (NOLOCK) ON elu.codigo_evl = evl.codigo_evl
                        WHERE elu.codigo_alu = @codigo_alu
                          AND evl.codigo_cco = @codigo_cco
                          AND elu.estado_elu = 1

                        -- Actualizo las notas finales por nivelación
                        INSERT INTO #notas_finales_nivelacion
                        SELECT
                            elu.codigo_alu
                          , ean.codigo_com
                          , SUM(ean.nota_ean * ISNULL(ceep.peso_ceep, 0))
                        FROM (SELECT
                                  ROW_NUMBER() OVER (ORDER BY _elu.fecha_reg) AS orden
                                , _elu.codigo_alu
                                , _elu.codigo_elu
                              FROM ADM_Evaluacion_Alumno _elu WITH (NOLOCK)
                                   JOIN ADM_Evaluacion __evl WITH (NOLOCK)
                                        ON _elu.codigo_evl = __evl.codigo_evl
                              WHERE __evl.codigo_cco = @codigo_cco
                                AND _elu.codigo_alu = @codigo_alu
                                AND _elu.estado_elu = 1) elu
                             JOIN ADM_Evaluacion_Alumno_Nivelacion ean WITH (NOLOCK)
                                  ON elu.codigo_elu = ean.codigo_elu AND ean.estado_ean = 1
                             CROSS APPLY (SELECT TOP 1 _ceep.peso_ceep
                                          FROM ADM_ConfiguracionEvaluacionEvento_Peso _ceep WITH (NOLOCK)
                                               JOIN ADM_ConfiguracionEvaluacionEvento _cee WITH (NOLOCK)
                                                    ON _ceep.codigo_cee = _cee.codigo_cee
                                          WHERE _ceep.estado_ceep = 1
                                            AND _cee.codigo_cee = @codigo_cee
                                            AND _ceep.nro_orden_ceep = elu.orden) ceep
                        WHERE NOT EXISTS(SELECT nfn.codigo_alu
                                         FROM #notas_finales_nivelacion nfn
                                         WHERE nfn.codigo_alu = elu.codigo_alu
                                           AND nfn.codigo_com = ean.codigo_com)
                        GROUP BY elu.codigo_alu, ean.codigo_com

                        UPDATE ean
                        SET ean.notaFinal_ean = notas.notaFinal
                        FROM ADM_Evaluacion_Alumno_Nivelacion ean WITH (NOLOCK)
                             JOIN ADM_Evaluacion_Alumno elu WITH (NOLOCK)
                                  ON ean.codigo_elu = elu.codigo_elu AND elu.estado_elu = 1
                             JOIN ADM_Evaluacion evl WITH (NOLOCK)
                                  ON elu.codigo_evl = evl.codigo_evl AND evl.estado_evl = 1
                             CROSS APPLY (SELECT SUM(nfn.notaFinal) AS notaFinal
                                          FROM #notas_finales_nivelacion nfn
                                          WHERE nfn.codigo_alu = elu.codigo_alu
                                            AND nfn.codigo_com = ean.codigo_com) notas
                        WHERE 1 = 1
                          AND evl.codigo_cco = @codigo_cco
                          AND elu.codigo_alu = @codigo_alu
                          AND ean.estado_ean = 1
                    END

                FETCH NEXT FROM cur_eval_alumno INTO @codigo_alu, @codigo_cpf, @codigo_evl, @codigo_elu, @peso_basica_tev, @peso_intermedia_tev, @peso_avanzada_tev, @codigo_cee, @cantidad_cee
            END

        CLOSE cur_eval_alumno
        DEALLOCATE cur_eval_alumno

        IF @trancount = 1
            COMMIT

        SET @rpta = 1
        SET @msg = N'Se realizó la operación correctamente'
    END TRY
    BEGIN CATCH
        IF @trancount = 1
            ROLLBACK

        IF CURSOR_STATUS('global', 'cur_eval_alumno') >= -1
            BEGIN
                DEALLOCATE cur_eval_alumno
            END

        IF CURSOR_STATUS('global', 'cur_respuestas') >= -1
            BEGIN
                DEALLOCATE cur_respuestas
            END

        SET @rpta = -1;
        SET @msg = N'Ocurrió un error en la operación'

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH
END
GO

GRANT EXECUTE ON [dbo].[ADM_CalcularNotasEvaluacion] TO usuariogeneral
-- GRANT EXECUTE ON [dbo].[ADM_CalcularNotasEvaluacion] TO IusrReporting
-- GRANT EXECUTE ON [dbo].[ADM_CalcularNotasEvaluacion] TO iusrvirtualsistema
GO