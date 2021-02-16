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
-- =============================================

ALTER PROCEDURE dbo.ADM_GrupoAdmisionVirtual_Alumno_Listar 
	@tipoOperacion VARCHAR(2) = '',
	@codigo_gru INT = -1,
	@codigo_alu INT = -1,
	@mostrar_con_deuda BIT = 0 --003
AS
BEGIN
	IF @tipoOperacion = ''
	BEGIN
	
		--Declare @codigo_cco INT
		
		---SELECT @codigo_cco = codigo_cco FROM dbo.ADM_GrupoAdmisionVirtual(NOLOCK) where codigo_gru = @codigo_gru
		
		SELECT ga.codigo_gva, ga.codigo_gru, ga.codigo_alu, ga.estado, 
		(a.apellidoPat_Alu + ' ' + a.apellidoMat_Alu + ' ' + a.nombres_Alu) Alumno, a.codigoUniver_Alu, gav.nombre nombre_gru, 
		ISNULL(cc.abreviatura_cco, cc.descripcion_Cco) CentroCosto, cp.abreviatura_Cpf escuela, a.nroDocIdent_Alu, 
		cp.codigo_Cpf, a.password_Alu
		FROM dbo.ADM_GrupoAdmisionVirtual_Alumno(NOLOCK) ga
		INNER JOIN dbo.ADM_GrupoAdmisionVirtual(NOLOCK) gav ON ga.codigo_gru = gav.codigo_gru
		INNER JOIN dbo.Alumno(NOLOCK) a ON ga.codigo_alu = a.codigo_Alu
		INNER JOIN dbo.CentroCostos(NOLOCK) cc ON a.codigo_cco = cc.codigo_Cco
		INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON a.tempcodigo_cpf = cp.codigo_Cpf 
		WHERE ga.codigo_gru = @codigo_gru --(CASE @codigo_gru WHEN -1 THEN ga.codigo_gru ELSE @codigo_gru END)
		--AND ga.codigo_alu = (CASE @codigo_alu WHEN -1 THEN ga.codigo_alu ELSE @codigo_alu END)
		AND ga.estado <> 0
		
		UNION ALL
		
		SELECT -1  codigo_gva, -1 codigo_gru, tp.cli codigo_alu, 0 estado,
		tp.Participante Alumno, tp.codigoUniver_Alu, '' nombre_gru, tp.descripcion_Cco CentroCosto, tp.escuela, tp.nroDocIdent_Alu, 
		tp.codigo_Cpf, tp.password_Alu
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
		SELECT DISTINCT tp.codigo_Cpf, tp.nombre_Cpf
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
		WHERE (@mostrar_con_deuda = 1 OR tp.estadoAdmision = 'POSTULANTE') --003
		AND tp.estadoAdmision NOT IN ('INGRESANTE', 'ACCESITARIO') --004
		AND ga.codigo_gva IS NULL
		
		ORDER BY 2
	END
END
GO