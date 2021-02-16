
-- =============================================
-- Author:		ENevado
-- Create date: 2020-05-12
-- Description:
-- =============================================
--    CODIGO  FECHA       DESARROLLADOR   DESCRIPCIÓN
--    001     2020-05-21 ENevado       postulantes activos
--    002     2020-05-21 ENevado       mostrar sin deuda
--    003     2020-09-04 andy.diaz     Mostrar alumnos aunque tengan deuda dependiendo del parámetro @mostrar_con_deuda
--    004     2020-09-04 andy.diaz     Filtro para que no se muestren ingresantes ni accesitarios, en caso @mostrar_con_deuda sea = 1
--    005     2020-09-14 ENevado		Filtro para que se muestren los postulante segun tipo de grupo
--    006     2020-09-17 ENevado		Filtro para que no se muestren postulantes del mismo tipo asignado
--    007     2020-09-24 ENevado		Adicionar Apellidos y Nombres de Participantes
--    008     2020-10-12 ENevado		Filtrar postulantes de grupos anteriores solo que esten activos
--    009     2020-11-10 ENevado		Filtrar solo centro de costos relacionados al grupo
--    010     2020-11-10 ENevado		Agreagr Columnas de estado y deuda pendiente
-- =============================================

ALTER PROCEDURE dbo.ADM_GrupoAdmisionVirtual_Alumno_Listar 
	@tipoOperacion VARCHAR(2) = '',
	@codigo_gru INT = -1,
	@codigo_alu INT = -1,
	@mostrar_con_deuda BIT = 0 --003
AS
BEGIN
	Declare @codigo_tge INT, @codigo_test INT -- >   005 
	IF @tipoOperacion = ''
	BEGIN
		--> Obtiene codigo de tipo de grupo
		SELECT @codigo_tge = codigo_tge FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) where codigo_gru = @codigo_gru -- >   005
		 
		--> Obtiene codigo de tipo de estudio del grupo
		SELECT @codigo_test = MAX(de.codigo_test)
		FROM dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gac 
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON gac.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.DatosEvento(NOLOCK) de ON cco.codigo_Cco = de.codigo_cco
		WHERE gac.codigo_gru = @codigo_gru
		
		--> Lista postulantes registrados en el grupo
		SELECT ga.codigo_gva, ga.codigo_gru, ga.codigo_alu, ga.estado, 
		(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ' ' + a.nombres_Alu) Alumno, a.codigoUniver_Alu, gav.nombre nombre_gru, 
		ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) CentroCosto, cp.abreviatura_Cpf escuela, a.nroDocIdent_Alu, 
		cp.codigo_Cpf, a.password_Alu, cc.codigo_Cco, 
		(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu) apellidos_alu, a.nombres_Alu, -->    007
		(CASE a.condicion_Alu
			WHEN 'P' THEN
				CASE
					WHEN (SELECT COUNT(d.codigo_Deu)
							FROM dbo.Deuda d (NOLOCK)
							INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON d.codigo_Cco = gcc.codigo_cco --
							WHERE d.codigo_Alu = a.codigo_Alu 
							AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 --
							AND estado_Deu = 'P') > 0 THEN 'INTERESADO'
					ELSE 'POSTULANTE'
				END
			WHEN 'I' THEN
				CASE
		WHEN a.alcanzo_vacante = 1 THEN 'INGRESANTE'
					ELSE 'ACCESITARIO'
				END
		END) AS estadoAdmision, -->    010 
		(CASE ISNULL(d.SaldoTotal, 0.00) WHEN 0 THEN 'NO' ELSE 'SI' END) Deuda -->    010
		FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
		INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
		INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
		INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf
		OUTER APPLY (
				SELECT
					d2.codigo_Pso,
					SUM(d2.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS CargoTotal, --001
					SUM(d2.montoTotal_Deu - d2.saldo_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS AbonoTotal,
					SUM(d2.Saldo_Deu) AS SaldoTotal
				FROM dbo.Deuda d2 (NOLOCK)
				LEFT JOIN DetalleCajaIngreso cin (NOLOCK) ON cin.codigo_Deu = d2.codigo_deu AND cin.codigo_mno = 4
				WHERE d2.codigo_Pso = a.codigo_Pso
					AND d2.codigo_cco = a.codigo_cco
					AND d2.estado_Deu <> 'A'
				GROUP BY d2.codigo_Pso) d 
		WHERE ga.codigo_gru = @codigo_gru --(CASE @codigo_gru WHEN -1 THEN ga.codigo_gru ELSE @codigo_gru END)
		--AND ga.codigo_alu = (CASE @codigo_alu WHEN -1 THEN ga.codigo_alu ELSE @codigo_alu END)
		AND ga.estado <> 0
		
		UNION ALL
		
		SELECT DISTINCT -1 codigo_gva, -1 codigo_gru, ga.codigo_alu, 0 estado, 
		(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ' ' + a.nombres_Alu) Alumno, a.codigoUniver_Alu, '' nombre_gru, 
		ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) CentroCosto, cp.abreviatura_Cpf escuela, a.nroDocIdent_Alu, 
		cp.codigo_Cpf, a.password_Alu, cc.codigo_Cco, 
		(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu) apellidos_alu, a.nombres_Alu, -->    007
		(CASE a.condicion_Alu
			WHEN 'P' THEN
				CASE
					WHEN (SELECT COUNT(d.codigo_Deu)
							FROM dbo.Deuda d (NOLOCK)
							INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON d.codigo_Cco = gcc.codigo_cco --
							WHERE d.codigo_Alu = a.codigo_Alu 
							AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 --
							AND estado_Deu = 'P') > 0 THEN 'INTERESADO'
					ELSE 'POSTULANTE'
				END
			WHEN 'I' THEN
				CASE
		WHEN a.alcanzo_vacante = 1 THEN 'INGRESANTE'
					ELSE 'ACCESITARIO'
				END
		END) AS estadoAdmision, -->    010
		(CASE ISNULL(d.SaldoTotal, 0.00) WHEN 0 THEN 'NO' ELSE 'SI' END) Deuda -->    010
		FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
		INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
		INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
		INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
		INNER JOIN dbo.DatosEvento(NOLOCK) de ON cc.codigo_Cco = de.codigo_cco
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
		LEFT JOIN (SELECT ga2.codigo_gva, ga2.codigo_gru, ga2.codigo_alu 
					FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga2 
					WHERE ga2.estado <> 0 AND ga2.codigo_gru = @codigo_gru) AS tb1
					ON ga.codigo_alu = tb1.codigo_alu -- >   005  
		LEFT JOIN (SELECT ga3.codigo_gva, ga3.codigo_gru, ga3.codigo_alu
					FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga3 
					INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) g2 ON ga3.codigo_gru = g2.codigo_gru
					WHERE ga3.estado = 1 AND g2.estado = 1 
					AND g2.codigo_tge = @codigo_tge AND ga3.codigo_gru <> @codigo_gru) AS tb2
					ON ga.codigo_alu = tb2.codigo_alu --  >  006
		OUTER APPLY (
				SELECT
					d2.codigo_Pso,
					SUM(d2.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS CargoTotal, --001
					SUM(d2.montoTotal_Deu - d2.saldo_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS AbonoTotal,
					SUM(d2.Saldo_Deu) AS SaldoTotal
				FROM dbo.Deuda d2 (NOLOCK)
				LEFT JOIN DetalleCajaIngreso cin (NOLOCK) ON cin.codigo_Deu = d2.codigo_deu AND cin.codigo_mno = 4
				WHERE d2.codigo_Pso = a.codigo_Pso
					AND d2.codigo_cco = a.codigo_cco
					AND d2.estado_Deu <> 'A'
				GROUP BY d2.codigo_Pso) d 
		WHERE a.estadoActual_Alu = 1  -->    008 
		AND ga.codigo_gru <> @codigo_gru 
		AND de.codigo_test = @codigo_test AND gav.codigo_tge < @codigo_tge
		AND ga.estado <> 0
		AND tb1.codigo_gva IS NULL -- >   005 
		AND tb2.codigo_gva IS NULL --  >  006 
		
		UNION ALL
		
		SELECT -1  codigo_gva, -1 codigo_gru, tp.cli codigo_alu, 0 estado,
		tp.Participante Alumno, tp.codigoUniver_Alu, '' nombre_gru, tp.descripcion_Cco CentroCosto, tp.escuela, tp.nroDocIdent_Alu, 
		tp.codigo_Cpf, tp.password_Alu, tp.codigo_Cco, 
		tp.apellidos_alu, tp.nombres_Alu, -->    007
		tp.estadoAdmision, -->    010
		(CASE ISNULL(tp.SaldoTotal,0) WHEN 0 THEN 'NO' ELSE 'SI' END) Deuda -->    010
		FROM(SELECT p.codigo_Pso
				,a.codigo_Alu AS cli
				,a.tipoDocIdent_Alu AS TipoDoc
				,a.nroDocIdent_Alu AS Nrodoc
				,a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ', ' + a.nombres_Alu AS Participante
				,a.codigoUniver_Alu
				,a.cicloIng_Alu
				,mi.nombre_Min
				,(CASE a.condicion_Alu
					WHEN 'P' THEN
						CASE
							WHEN (SELECT COUNT(d.codigo_Deu)
									FROM dbo.Deuda d (NOLOCK)
									INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON d.codigo_Cco = gcc.codigo_cco --
									WHERE d.codigo_Alu = a.codigo_Alu 
									AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 --
									AND estado_Deu = 'P') > 0 THEN 'INTERESADO'
							ELSE 'POSTULANTE'
						END
					WHEN 'I' THEN
						CASE
			  WHEN a.alcanzo_vacante = 1 THEN 'INGRESANTE'
							ELSE 'ACCESITARIO'
						END
				END) AS estadoAdmision -- 004
				,ISNULL(d.CargoTotal, 0.00) AS CargoTotal
				,ISNULL(d.AbonoTotal, 0.00) AS AbonoTotal
				,ISNULL(d.SaldoTotal, 0.00) AS SaldoTotal
				,a.fechaInscripcion_Alu
				,ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) descripcion_Cco
				, cp.abreviatura_Cpf  escuela
				, cp.codigo_Cpf
				, a.nroDocIdent_Alu
				, a.password_Alu
				, cc.codigo_Cco
				,(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu) apellidos_alu -->    007
				, a.nombres_Alu  -->    007
			FROM dbo.Alumno a (NOLOCK)
			INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
			INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf
			INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON a.codigo_cco = gcc.codigo_cco --
			LEFT JOIN dbo.PERSONA p (NOLOCK) ON a.nroDocIdent_Alu = p.numeroDocIdent_Pso
			LEFT JOIN dbo.DatosAlumno da (NOLOCK) ON	da.codigo_Alu = a.codigo_Alu
			LEFT JOIN dbo.ModalidadIngreso mi (NOLOCK) ON mi.codigo_Min = a.codigo_Min
			OUTER APPLY (
				SELECT
					d2.codigo_Pso,
					SUM(d2.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS CargoTotal, --001
					SUM(d2.montoTotal_Deu - d2.saldo_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS AbonoTotal,
					SUM(d2.Saldo_Deu) AS SaldoTotal
				FROM dbo.Deuda d2 (NOLOCK)
				LEFT JOIN DetalleCajaIngreso cin (NOLOCK) ON cin.codigo_Deu = d2.codigo_deu AND cin.codigo_mno = 4
				WHERE d2.codigo_Pso = p.codigo_Pso
					AND d2.codigo_cco = a.codigo_cco
					AND d2.estado_Deu <> 'A'
				GROUP BY d2.codigo_Pso) d
			WHERE 1 = 1
				--AND a.codigo_cco = @codigo_cco
				AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 ---
				AND isnull(a.eliminado_Alu, 0) = 0 
				AND a.estadoActual_Alu = 1 --    001
				AND (@mostrar_con_deuda = 1 OR d.codigo_Pso IS NOT NULL)) AS tp --    002 --003
		LEFT JOIN dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga ON ga.codigo_alu = tp.cli AND ga.estado <> 0
		WHERE (@mostrar_con_deuda = 1 OR tp.estadoAdmision = 'POSTULANTE') --003
        AND tp.estadoAdmision NOT IN ('INGRESANTE', 'ACCESITARIO') --004
		AND ga.codigo_gva IS NULL
		
		ORDER BY Alumno
		
	END
	ELSE
	IF @tipoOperacion = 'LT'
	BEGIN
		SELECT ga.codigo_gva, ga.codigo_gru, ga.codigo_alu, ga.estado, 
		(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ' ' + a.nombres_Alu) Alumno, a.codigoUniver_Alu, gav.nombre nombre_gru, 
		ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) CentroCosto, cp.abreviatura_Cpf escuela, a.nroDocIdent_Alu, cp.codigo_Cpf, a.password_Alu 
		FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
		INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
		INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
		INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
		WHERE ga.codigo_gru = @codigo_gru AND ga.estado <> 0
		ORDER BY a.apellidoPat_Alu, a.apellidoMat_Alu, a.nombres_Alu
	END
	ELSE
	IF @tipoOperacion = 'CB'
	BEGIN
		SELECT @codigo_tge = codigo_tge FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) where codigo_gru = @codigo_gru -- >   005
		
		SELECT @codigo_test = MAX(de.codigo_test)
		FROM dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gac 
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON gac.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.DatosEvento(NOLOCK) de ON cco.codigo_Cco = de.codigo_cco
		WHERE gac.codigo_gru = @codigo_gru
		
		SELECT DISTINCT Tab.codigo_Cpf, Tab.nombre_Cpf
		FROM(
			--SELECT cp.codigo_Cpf, cp.nombre_Cpf
			--FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
			--INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
			--INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
			--INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
			--INNER JOIN dbo.DatosEvento(NOLOCK) de ON cc.codigo_Cco = de.codigo_cco
			--INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
			--WHERE a.estadoActual_Alu = 1  -->    008 
			--AND ga.codigo_gru <> @codigo_gru 
			--AND de.codigo_test = @codigo_test AND gav.codigo_tge < @codigo_tge
			--AND ga.estado <> 0
			-->    009 -----------------------------------------------------------------------------
			SELECT cp.codigo_Cpf, cp.nombre_Cpf
			FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
			INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
			INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
			INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
			INNER JOIN dbo.DatosEvento(NOLOCK) de ON cc.codigo_Cco = de.codigo_cco
			INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
			LEFT JOIN (SELECT ga2.codigo_gva, ga2.codigo_gru, ga2.codigo_alu 
						FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga2 
						WHERE ga2.estado <> 0 AND ga2.codigo_gru = @codigo_gru) AS tb1
						ON ga.codigo_alu = tb1.codigo_alu -- >   005  
			LEFT JOIN (SELECT ga3.codigo_gva, ga3.codigo_gru, ga3.codigo_alu
						FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga3 
						INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) g2 ON ga3.codigo_gru = g2.codigo_gru
						WHERE ga3.estado = 1 AND g2.estado = 1 
						AND g2.codigo_tge = @codigo_tge AND ga3.codigo_gru <> @codigo_gru) AS tb2
						ON ga.codigo_alu = tb2.codigo_alu --  >  006 
			WHERE a.estadoActual_Alu = 1  -->    008 
			AND ga.codigo_gru <> @codigo_gru 
			AND de.codigo_test = @codigo_test AND gav.codigo_tge < @codigo_tge
			AND ga.estado <> 0
			AND tb1.codigo_gva IS NULL -- >   005 
			AND tb2.codigo_gva IS NULL --  >  006
			
			UNION ALL 
			
			SELECT tp.codigo_Cpf, tp.nombre_Cpf
			FROM(SELECT p.codigo_Pso
					,a.codigo_Alu AS cli
					,a.tipoDocIdent_Alu AS TipoDoc
					,a.nroDocIdent_Alu AS Nrodoc
					,a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ', ' + a.nombres_Alu AS Participante
					,a.codigoUniver_Alu
					,a.cicloIng_Alu
					,mi.nombre_Min
					,(CASE a.condicion_Alu
						WHEN 'P' THEN
							CASE
								WHEN (SELECT COUNT(d.codigo_Deu)
										FROM dbo.Deuda d (NOLOCK)
										INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON d.codigo_Cco = gcc.codigo_cco --
										WHERE d.codigo_Alu = a.codigo_Alu 
										AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 --
										AND estado_Deu = 'P') > 0 THEN 'INTERESADO'
								ELSE 'POSTULANTE'
							END
						WHEN 'I' THEN
							CASE
				  WHEN a.alcanzo_vacante = 1 THEN 'INGRESANTE'
								ELSE 'ACCESITARIO'
							END
					END) AS estadoAdmision -- 004
					,ISNULL(d.CargoTotal, 0.00) AS CargoTotal
					,ISNULL(d.AbonoTotal, 0.00) AS AbonoTotal
					,ISNULL(d.SaldoTotal, 0.00) AS SaldoTotal
					,a.fechaInscripcion_Alu
					,ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) descripcion_Cco
					, cp.nombre_Cpf
					, cp.codigo_Cpf
					, a.nroDocIdent_Alu
					, a.password_Alu
				FROM dbo.Alumno a (NOLOCK)
				INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
				INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf
				INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON a.codigo_cco = gcc.codigo_cco --
				LEFT JOIN dbo.PERSONA p (NOLOCK) ON a.nroDocIdent_Alu = p.numeroDocIdent_Pso
				LEFT JOIN dbo.DatosAlumno da (NOLOCK) ON	da.codigo_Alu = a.codigo_Alu
				LEFT JOIN dbo.ModalidadIngreso mi (NOLOCK) ON mi.codigo_Min = a.codigo_Min
				OUTER APPLY (
					SELECT
						d2.codigo_Pso,
						SUM(d2.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS CargoTotal, --001
						SUM(d2.montoTotal_Deu - d2.saldo_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS AbonoTotal,
						SUM(d2.Saldo_Deu) AS SaldoTotal
					FROM dbo.Deuda d2 (NOLOCK)
					LEFT JOIN DetalleCajaIngreso cin (NOLOCK) ON cin.codigo_Deu = d2.codigo_deu AND cin.codigo_mno = 4
					WHERE d2.codigo_Pso = p.codigo_Pso
						AND d2.codigo_cco = a.codigo_cco
						AND d2.estado_Deu <> 'A'
					GROUP BY d2.codigo_Pso) d
				WHERE 1 = 1
					--AND a.codigo_cco = @codigo_cco
					AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 ---
					AND isnull(a.eliminado_Alu, 0) = 0 
					AND a.estadoActual_Alu = 1 --    001
					AND (@mostrar_con_deuda = 1 OR d.codigo_Pso IS NOT NULL)) AS tp --    002 --003
			LEFT JOIN dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga ON ga.codigo_alu = tp.cli AND ga.estado <> 0
			LEFT JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) g ON ga.codigo_gru = g.codigo_gru AND g.codigo_tge <> @codigo_tge -- >   005 
			WHERE (@mostrar_con_deuda = 1 OR tp.estadoAdmision = 'POSTULANTE') --003
			AND tp.estadoAdmision NOT IN ('INGRESANTE', 'ACCESITARIO') --004 v
			AND ga.codigo_gva IS NULL) as Tab
		
		ORDER BY 2
	END
	ELSE
	IF @tipoOperacion = 'EB'
	BEGIN
		SELECT @codigo_tge = codigo_tge FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) where codigo_gru = @codigo_gru -- >   005
		
		SELECT @codigo_test = MAX(de.codigo_test)
		FROM dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gac 
		INNER JOIN dbo.CentroCostos(NOLOCK) cco ON gac.codigo_cco = cco.codigo_Cco
		INNER JOIN dbo.DatosEvento(NOLOCK) de ON cco.codigo_Cco = de.codigo_cco
		WHERE gac.codigo_gru = @codigo_gru
		
		SELECT DISTINCT Tab.codigo_Cco, Tab.descripcion_Cco
		FROM(
			--SELECT cc.codigo_Cco, cc.descripcion_Cco
			--FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
			--INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
			--INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
			--INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
			--INNER JOIN dbo.DatosEvento(NOLOCK) de ON cc.codigo_Cco = de.codigo_cco
			--INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
			--WHERE a.estadoActual_Alu = 1  -->    008 
			--AND ga.codigo_gru <> @codigo_gru 
			--AND de.codigo_test = @codigo_test AND gav.codigo_tge < @codigo_tge
			--AND ga.estado <> 0
			-->    009 -----------------------------------------------------------------------------
			SELECT cc.codigo_Cco, cc.descripcion_Cco
			FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
			INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
			INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
			INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
			INNER JOIN dbo.DatosEvento(NOLOCK) de ON cc.codigo_Cco = de.codigo_cco
			INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
			LEFT JOIN (SELECT ga2.codigo_gva, ga2.codigo_gru, ga2.codigo_alu 
						FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga2 
						WHERE ga2.estado <> 0 AND ga2.codigo_gru = @codigo_gru) AS tb1
						ON ga.codigo_alu = tb1.codigo_alu -- >   005  
			LEFT JOIN (SELECT ga3.codigo_gva, ga3.codigo_gru, ga3.codigo_alu
						FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga3 
						INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) g2 ON ga3.codigo_gru = g2.codigo_gru
						WHERE ga3.estado = 1 AND g2.estado = 1 
						AND g2.codigo_tge = @codigo_tge AND ga3.codigo_gru <> @codigo_gru) AS tb2
						ON ga.codigo_alu = tb2.codigo_alu --  >  006 
			WHERE a.estadoActual_Alu = 1  -->    008 
			AND ga.codigo_gru <> @codigo_gru 
			AND de.codigo_test = @codigo_test AND gav.codigo_tge < @codigo_tge
			AND ga.estado <> 0
			AND tb1.codigo_gva IS NULL -- >   005 
			AND tb2.codigo_gva IS NULL --  >  006
			
		
			UNION ALL 
			
			SELECT tp.codigo_cco, tp.descripcion_Cco
			FROM(SELECT p.codigo_Pso
					,a.codigo_Alu AS cli
					,a.tipoDocIdent_Alu AS TipoDoc
					,a.nroDocIdent_Alu AS Nrodoc
					,a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ', ' + a.nombres_Alu AS Participante
					,a.codigoUniver_Alu
					,a.cicloIng_Alu
					,mi.nombre_Min
					,(CASE a.condicion_Alu
						WHEN 'P' THEN
							CASE
								WHEN (SELECT COUNT(d.codigo_Deu)
										FROM dbo.Deuda d (NOLOCK)
										INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON d.codigo_Cco = gcc.codigo_cco --
										WHERE d.codigo_Alu = a.codigo_Alu 
										AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 --
										AND estado_Deu = 'P') > 0 THEN 'INTERESADO'
								ELSE 'POSTULANTE'
							END
						WHEN 'I' THEN
							CASE
				  WHEN a.alcanzo_vacante = 1 THEN 'INGRESANTE'
								ELSE 'ACCESITARIO'
							END
					END) AS estadoAdmision -- 004
					,ISNULL(d.CargoTotal, 0.00) AS CargoTotal
					,ISNULL(d.AbonoTotal, 0.00) AS AbonoTotal
					,ISNULL(d.SaldoTotal, 0.00) AS SaldoTotal
					,a.fechaInscripcion_Alu
					,ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) descripcion_Cco
					, cp.nombre_Cpf
					, cp.codigo_Cpf
					, a.nroDocIdent_Alu
					, a.password_Alu
					, cc.codigo_Cco
				FROM dbo.Alumno a (NOLOCK)
				INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
				INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf
				INNER JOIN dbo.ADM_GrupoAdmision_CentroCosto(NOLOCK) gcc ON a.codigo_cco = gcc.codigo_cco --
				LEFT JOIN dbo.PERSONA p (NOLOCK) ON a.nroDocIdent_Alu = p.numeroDocIdent_Pso
				LEFT JOIN dbo.DatosAlumno da (NOLOCK) ON	da.codigo_Alu = a.codigo_Alu
				LEFT JOIN dbo.ModalidadIngreso mi (NOLOCK) ON mi.codigo_Min = a.codigo_Min
				OUTER APPLY (
					SELECT
						d2.codigo_Pso,
						SUM(d2.montoTotal_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS CargoTotal, --001
						SUM(d2.montoTotal_Deu - d2.saldo_Deu) - SUM(ISNULL(cin.TotalMonNac_Dci, 0)) AS AbonoTotal,
						SUM(d2.Saldo_Deu) AS SaldoTotal
					FROM dbo.Deuda d2 (NOLOCK)
					LEFT JOIN DetalleCajaIngreso cin (NOLOCK) ON cin.codigo_Deu = d2.codigo_deu AND cin.codigo_mno = 4
					WHERE d2.codigo_Pso = p.codigo_Pso
						AND d2.codigo_cco = a.codigo_cco
						AND d2.estado_Deu <> 'A'
					GROUP BY d2.codigo_Pso) d
				WHERE 1 = 1
					--AND a.codigo_cco = @codigo_cco
					AND gcc.codigo_gru = @codigo_gru AND gcc.estado_gcc <> 0 ---
					AND isnull(a.eliminado_Alu, 0) = 0 
					AND a.estadoActual_Alu = 1 --    001
					AND (@mostrar_con_deuda = 1 OR d.codigo_Pso IS NOT NULL)) AS tp --    002 --003
			LEFT JOIN dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga ON ga.codigo_alu = tp.cli AND ga.estado <> 0
			LEFT JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) g ON ga.codigo_gru = g.codigo_gru AND g.codigo_tge <> @codigo_tge -- >   005 
			WHERE (@mostrar_con_deuda = 1 OR tp.estadoAdmision = 'POSTULANTE') --003
			AND tp.estadoAdmision NOT IN ('INGRESANTE', 'ACCESITARIO') --004 v
			AND ga.codigo_gva IS NULL) as Tab
		
		ORDER BY 2
	END
END






