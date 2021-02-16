
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-31
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ReportePerfilIngresoCarrera
	
AS
BEGIN
	SELECT f.codigo_Fac, f.nombre_Fac, cp.codigo_Cpf, cp.nombre_Cpf, ca.codigo_com, ca.nombre_com, ca.nombre_corto_com 
	FROM dbo.PerfilIngreso(NOLOCK) ping
	INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) ca ON ping.codigo_com = ca.codigo_com
	INNER JOIN dbo.PlanCurricular(NOLOCK) pc ON ping.codigo_pcur = pc.codigo_pcur
	INNER JOIN dbo.CarreraProfesional(NOLOCK) cp ON pc.codigo_cpf = cp.codigo_Cpf
	INNER JOIN dbo.Facultad(NOLOCK) f ON cp.codigo_Fac = f.codigo_Fac
	WHERE ping.estado_pIng = 1 AND pc.vigente_pcur = 1 AND cp.vigencia_Cpf = 1 AND cp.eliminado_cpf = 0
	AND ca.estado_com = 1 AND ca.admision_com = 1
END
GO

GRANT EXECUTE ON ADM_ReportePerfilIngresoCarrera TO usuariogeneral
GRANT EXECUTE ON ADM_ReportePerfilIngresoCarrera TO IusrReporting
GO

