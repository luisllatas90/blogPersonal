
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_PesoCompetencia_Listar 
	@tipoOpe varchar(2),
	@codigo_cac int,
	@codigo_cpf int,
	@codigo_com int,
	@codigo_pcom int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT pcom.codigo_pcom, pcom.codigo_cac, pcom.codigo_cpf, pcom.codigo_com, pcom.peso_pcom, pcom.estado_pcom 
		FROM dbo.ADM_PesoCompetencia(NOLOCK) pcom
		WHERE pcom.estado_pcom = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		SELECT cpf.codigo_Fac, fac.nombre_Fac, cpf.codigo_Cpf, cpf.nombre_Cpf
		FROM dbo.CarreraProfesional(NOLOCK) cpf
		INNER JOIN dbo.Facultad(NOLOCK) fac ON cpf.codigo_Fac = fac.codigo_Fac
		WHERE cpf.codigo_test = 2 AND cpf.vigencia_Cpf = 1 AND cpf.eliminado_cpf = 0 AND cpf.codigo_Fac <> 0
		ORDER BY 2, 4
	END
	ELSE
	IF @tipoOpe = '2'
	BEGIN
		SELECT pcom.codigo_pcom, pcom.codigo_cac, pcom.codigo_cpf, pcom.codigo_com, pcom.peso_pcom, pcom.estado_pcom
		FROM dbo.ADM_PesoCompetencia(NOLOCK) pcom
		WHERE pcom.estado_pcom = 1 AND pcom.codigo_cac = @codigo_cac
	END
	ELSE
	IF @tipoOpe = '3'
	BEGIN
		SELECT ISNULL(pcom.codigo_pcom, -1) codigo_pcom, pcom.codigo_cac, ISNULL(pcom.codigo_cpf, @codigo_cpf) codigo_cpf, 
		ISNULL(pcom.codigo_com,com.codigo_com) codigo_com, ISNULL(pcom.peso_pcom, 0) peso_pcom, pcom.estado_pcom, com.nombre_com
		FROM dbo.CompetenciaAprendizaje(NOLOCK) com
		LEFT JOIN dbo.ADM_PesoCompetencia(NOLOCK) pcom ON com.codigo_com = pcom.codigo_com AND pcom.codigo_cac = @codigo_cac AND pcom.codigo_cpf = @codigo_cpf AND pcom.estado_pcom = 1
		WHERE com.admision_com = 1 AND com.estado_com = 1
	END
END
GO

GRANT EXECUTE ON ADM_PesoCompetencia_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_PesoCompetencia_Listar TO usuariogeneral
GO