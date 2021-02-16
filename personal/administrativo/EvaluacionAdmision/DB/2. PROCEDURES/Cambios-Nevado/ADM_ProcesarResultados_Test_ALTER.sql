
-- =============================================
-- Author:		ENevado
-- Create date: 2020-09-21
-- Description:	
-- ==================================================================
-- HISTORIAL DE CAMBIOS
-- ==================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-11-25	ENevado			Cambiar formato excel para cargar notas
-- ==================================================================
ALTER PROCEDURE ADM_ProcesarResultados_Test
	@codigo_evl INT,
	@ruta AS VARCHAR(500),
	@idArchivo AS BIGINT,
	@codigo_per INT,
	@rpta AS INT OUTPUT,
    @msg AS VARCHAR(500) OUTPUT
AS
BEGIN
	DECLARE @trancount BIT = 0
	BEGIN TRY
		IF @@TRANCOUNT = 0
            BEGIN
                BEGIN TRANSACTION
                SET @trancount = 1
            END

        DECLARE @sql NVARCHAR(max)

        DECLARE	@codigo_Alu int,
				@CODIGO_REG int,
				@FECHA datetime,
				@CODIGO_UNIV varchar(50),
				@NRO_DOCUMENTO varchar(50),
				@APE_PATERNO varchar(100),
				@APE_MATERNO varchar(100),
				@NOMBRES varchar(200),
				@PREG01 CHAR(1),
				@PREG02 CHAR(1),
				@PREG03 CHAR(1),
				@PREG04 CHAR(1),
				@PREG05 CHAR(1),
				@PREG06 CHAR(1),
				@PREG07 CHAR(1),
				@PREG08 CHAR(1),
				@PREG09 CHAR(1),
				@PREG10 CHAR(1),
				@PREG11 CHAR(1),
				@PREG12 CHAR(1),
				@PREG13 CHAR(1),
				@PREG14 CHAR(1),
				@PREG15 CHAR(1),
				@PREG16 CHAR(1),
				@PREG17 CHAR(1),
				@PREG18 CHAR(1),
				@PREG19 CHAR(1),
				@PREG20 CHAR(1),
				@PREG21 CHAR(1),
				@PREG22 CHAR(1),
				@PREG23 CHAR(1),
				@PREG24 CHAR(1),
				@PREG25 CHAR(1),
				@PREG26 CHAR(1),
				@PREG27 CHAR(1),
				@PREG28 CHAR(1),
				@PREG29 CHAR(1),
				@PREG30 CHAR(1),
				@PREG31 CHAR(1),
				@PREG32 CHAR(1),
				@PREG33 CHAR(1),
				@PREG34 CHAR(1),
				@PREG35 CHAR(1),
				@PREG36 CHAR(1),
				@PREG37 CHAR(1),
				@PREG38 CHAR(1),
				@PREG39 CHAR(1),
				@PREG40 CHAR(1),
				@PREG41 CHAR(1),
				@PREG42 CHAR(1),
				@PREG43 CHAR(1),
				@PREG44 CHAR(1),
				@PREG45 CHAR(1),
				@PREG46 CHAR(1),
				@PREG47 CHAR(1),
				@PREG48 CHAR(1),
				@PREG49 CHAR(1),
				@PREG50 CHAR(1),
				@PROCESADO bit

        IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL
        BEGIN
            Truncate TABLE #TempTable
        END
        ELSE
        BEGIN
            CREATE TABLE #TempTable (
                    --CODIGO_REG int,
                    --FECHA datetime,
                    --CODIGO_UNIV varchar(50),
                    NRO_DOCUMENTO varchar(50),
                    --APE_PATERNO varchar(100),
                    --APE_MATERNO varchar(100),
                    --NOMBRES varchar(200),
                    PREG01 CHAR(1),
                    PREG02 CHAR(1),
                    PREG03 CHAR(1),
                    PREG04 CHAR(1),
                    PREG05 CHAR(1),
                    PREG06 CHAR(1),
                    PREG07 CHAR(1),
                    PREG08 CHAR(1),
                    PREG09 CHAR(1),
                    PREG10 CHAR(1),
                    PREG11 CHAR(1),
					PREG12 CHAR(1),
					PREG13 CHAR(1),
					PREG14 CHAR(1),
					PREG15 CHAR(1),
					PREG16 CHAR(1),
					PREG17 CHAR(1),
					PREG18 CHAR(1),
					PREG19 CHAR(1),
					PREG20 CHAR(1),
					PREG21 CHAR(1),
					PREG22 CHAR(1),
					PREG23 CHAR(1),
					PREG24 CHAR(1),
					PREG25 CHAR(1),
					PREG26 CHAR(1),
					PREG27 CHAR(1),
					PREG28 CHAR(1),
					PREG29 CHAR(1),
					PREG30 CHAR(1),
					PREG31 CHAR(1),
					PREG32 CHAR(1),
					PREG33 CHAR(1),
					PREG34 CHAR(1),
					PREG35 CHAR(1),
					PREG36 CHAR(1),
					PREG37 CHAR(1),
					PREG38 CHAR(1),
					PREG39 CHAR(1),
					PREG40 CHAR(1),
					PREG41 CHAR(1),
					PREG42 CHAR(1),
					PREG43 CHAR(1),
					PREG44 CHAR(1),
					PREG45 CHAR(1),
					PREG46 CHAR(1),
					PREG47 CHAR(1),
					PREG48 CHAR(1),
					PREG49 CHAR(1),
					PREG50 CHAR(1),
                    PROCESADO bit,
                );
        END

        SET @sql='BULK INSERT #TempTable
		FROM '''+ @ruta +
		''' WITH
		(
			FIRSTROW = 2,
			MAXERRORS = 0,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR = ''\n'',
			CODEPAGE = ''ACP''
		)' /* ROWS_PER_BATCH : NUMERO DE FILAS POR ARCHIVO QUE PERMITE INSERTAR. */

        EXEC(@sql)

        -- Verifico que todos los alumnos cargados pertenezcan al mismo ceco
        DECLARE @codigo_cco INT
        SELECT @codigo_cco = evl.codigo_cco
        FROM ADM_Evaluacion evl WITH (NOLOCK)
        WHERE evl.codigo_evl = @codigo_evl;

        IF not exists(SELECT alu.codigo_Alu
                  FROM #TempTable tt
                       JOIN Alumno alu WITH (NOLOCK) ON RTRIM(LTRIM(tt.NRO_DOCUMENTO)) = alu.nroDocIdent_Alu
                  WHERE alu.codigo_cco = @codigo_cco)
            BEGIN
                SET @rpta = 0;
                SET @msg =
                        N'El archivo de carga contiene alumnos inscritos a un centro de costo distinto, por favor verificar';
                IF @trancount = 1
                    ROLLBACK
                RETURN
            END
        ------

        UPDATE dbo.ADM_Evaluacion SET idArchivoCompartido = @idArchivo WHERE codigo_evl = @codigo_evl

        UPDATE
			ear
		SET
			ear.estado_ear = 0,
			ear.codigo_per_act = @codigo_per,
			fecha_act = GETDATE()
		FROM
			dbo.ADM_Evaluacion_Alumno_Respuesta ear
			INNER JOIN dbo.ADM_Evaluacion_Alumno elu
				ON ear.codigo_elu = elu.codigo_elu
		WHERE
			elu.codigo_evl = @codigo_evl AND elu.estado_elu = 1 AND ear.estado_ear = 1

        UPDATE dbo.ADM_Evaluacion_Alumno SET estado_elu = 0, codigo_per_act = @codigo_per, fecha_act = GETDATE() WHERE codigo_evl = @codigo_evl

        DECLARE notas_cursor CURSOR FOR
            SELECT --CODIGO_REG,
                   --FECHA,
                   --CODIGO_UNIV,
                   NRO_DOCUMENTO,
                   --APE_PATERNO,
                   --APE_MATERNO,
                   --NOMBRES,
                   ISNULL(PREG01,''),
                   ISNULL(PREG02,''),
                   ISNULL(PREG03,''),
                   ISNULL(PREG04,''),
                   ISNULL(PREG05,''),
                   ISNULL(PREG06,''),
                   ISNULL(PREG07,''),
                   ISNULL(PREG08,''),
                   ISNULL(PREG09,''),
                   ISNULL(PREG10,''),
                   ISNULL(PREG11,''),
				   ISNULL(PREG12,''),
				   ISNULL(PREG13,''),
				   ISNULL(PREG14,''),
				   ISNULL(PREG15,''),
				   ISNULL(PREG16,''),
				   ISNULL(PREG17,''),
				   ISNULL(PREG18,''),
				   ISNULL(PREG19,''),
				   ISNULL(PREG20,''),
				   ISNULL(PREG21,''),
				   ISNULL(PREG22,''),
				   ISNULL(PREG23,''),
				   ISNULL(PREG24,''),
				   ISNULL(PREG25,''),
				   ISNULL(PREG26,''),
					ISNULL(PREG27,''),
					ISNULL(PREG28,''),
					ISNULL(PREG29,''),
					ISNULL(PREG30,''),
					ISNULL(PREG31,''),
					ISNULL(PREG32,''),
					ISNULL(PREG33,''),
					ISNULL(PREG34,''),
					ISNULL(PREG35,''),
					ISNULL(PREG36,''),
					ISNULL(PREG37,''),
					ISNULL(PREG38,''),
					ISNULL(PREG39,''),
					ISNULL(PREG40,''),
					ISNULL(PREG41,''),
					ISNULL(PREG42,''),
					ISNULL(PREG43,''),
					ISNULL(PREG44,''),
					ISNULL(PREG45,''),
					ISNULL(PREG46,''),
					ISNULL(PREG47,''),
					ISNULL(PREG48,''),
					ISNULL(PREG49,''),
					ISNULL(PREG50,''),
                   PROCESADO
            FROM #TempTable
            WHERE PROCESADO = 0

        DECLARE @cont AS INT = 0

        OPEN notas_cursor
            FETCH NEXT FROM notas_cursor
                INTO --@CODIGO_REG,
					--@FECHA,
					--@CODIGO_UNIV,
					@NRO_DOCUMENTO,
					--@APE_PATERNO,
					--@APE_MATERNO,
					--@NOMBRES,
					@PREG01,
					@PREG02,
					@PREG03,
					@PREG04,
					@PREG05,
					@PREG06,
					@PREG07,
					@PREG08,
					@PREG09,
					@PREG10,
					@PREG11,
					@PREG12,
					@PREG13,
					@PREG14,
					@PREG15,
					@PREG16,
					@PREG17,
					@PREG18,
					@PREG19,
					@PREG20,
					@PREG21,
					@PREG22,
					@PREG23,
					@PREG24,
					@PREG25,
					@PREG26,
					@PREG27,
					@PREG28,
					@PREG29,
					@PREG30,
					@PREG31,
					@PREG32,
					@PREG33,
					@PREG34,
					@PREG35,
					@PREG36,
					@PREG37,
					@PREG38,
					@PREG39,
					@PREG40,
					@PREG41,
					@PREG42,
					@PREG43,
					@PREG44,
					@PREG45,
					@PREG46,
					@PREG47,
					@PREG48,
					@PREG49,
					@PREG50,
					@PROCESADO

			WHILE @@FETCH_STATUS = 0
				BEGIN

					SELECT @codigo_alu = codigo_alu
                    FROM dbo.Alumno WHERE nroDocIdent_Alu = @NRO_DOCUMENTO 
                    AND codigo_cco = @codigo_cco -- 001

                    IF @codigo_alu IS NOT NULL
                    BEGIN

						INSERT INTO dbo.ADM_Evaluacion_Alumno(codigo_evl,codigo_alu,nota_elu,estadonota_elu,condicion_ingreso_elu,
									estadoverificacion_elu,observacion_elu,codigo_per_reg,fecha_reg,estado_elu,respuesta_elu)
						VALUES(@codigo_evl,@codigo_Alu,0.0,'P','P','P','',@codigo_per,GETDATE(),1,
						@PREG01+'|'+@PREG02+'|'+@PREG03+'|'+@PREG04+'|'+@PREG05+'|'+@PREG06+'|'+@PREG07+'|'+@PREG08+'|'+@PREG09+'|'+@PREG10+'|'+
						@PREG11+'|'+@PREG12+'|'+@PREG13+'|'+@PREG14+'|'+@PREG15+'|'+@PREG16+'|'+@PREG17+'|'+@PREG18+'|'+@PREG19+'|'+@PREG20+'|'+
						@PREG21+'|'+@PREG22+'|'+@PREG23+'|'+@PREG24+'|'+@PREG25+'|'+@PREG26+'|'+@PREG27+'|'+@PREG28+'|'+@PREG29+'|'+@PREG30+'|'+
						@PREG31+'|'+@PREG32+'|'+@PREG33+'|'+@PREG34+'|'+@PREG35+'|'+@PREG36+'|'+@PREG37+'|'+@PREG38+'|'+@PREG39+'|'+@PREG40+'|'+
						@PREG41+'|'+@PREG42+'|'+@PREG43+'|'+@PREG44+'|'+@PREG45+'|'+@PREG46+'|'+@PREG47+'|'+@PREG48+'|'+@PREG49+'|'+@PREG50)

						SET @cont = @cont + 1

                    END

                    FETCH NEXT FROM notas_cursor
                        INTO --@CODIGO_REG,
							--@FECHA,
							--@CODIGO_UNIV,
							@NRO_DOCUMENTO,
							--@APE_PATERNO,
							--@APE_MATERNO,
							--@NOMBRES,
							@PREG01,
							@PREG02,
							@PREG03,
							@PREG04,
							@PREG05,
							@PREG06,
							@PREG07,
							@PREG08,
							@PREG09,
							@PREG10,
							@PREG11,
							@PREG12,
							@PREG13,
							@PREG14,
							@PREG15,
							@PREG16,
							@PREG17,
							@PREG18,
							@PREG19,
							@PREG20,
							@PREG21,
							@PREG22,
							@PREG23,
							@PREG24,
							@PREG25,
							@PREG26,
							@PREG27,
							@PREG28,
							@PREG29,
							@PREG30,
							@PREG31,
							@PREG32,
							@PREG33,
							@PREG34,
							@PREG35,
							@PREG36,
							@PREG37,
							@PREG38,
							@PREG39,
							@PREG40,
							@PREG41,
							@PREG42,
							@PREG43,
							@PREG44,
							@PREG45,
							@PREG46,
							@PREG47,
							@PREG48,
							@PREG49,
							@PREG50,
							@PROCESADO

				END
		CLOSE notas_cursor
        DEALLOCATE notas_cursor

        --SELECT * FROM #TempTable

        SET @rpta = 1
        SET @msg = 'Operación realizada correctamente, registros procesados: ' + CAST(@cont AS VARCHAR(50))

        IF @trancount = 1
            COMMIT

	END TRY
	BEGIN CATCH
        IF @trancount = 1
            ROLLBACK

        SET @rpta = -1;
        SET @msg = 'Ocurrió un error en la transacción'

        PRINT ERROR_MESSAGE()

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState)
    END CATCH

END

