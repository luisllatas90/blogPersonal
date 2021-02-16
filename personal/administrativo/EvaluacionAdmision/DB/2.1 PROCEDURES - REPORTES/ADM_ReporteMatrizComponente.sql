
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-31
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_ReporteMatrizComponente 
	@codigo_cmp INT
AS
BEGIN
	SELECT cca.codigo_cmp, ca.codigo_com, ca.nombre_com, ca.nombre_corto_com, sc.codigo_scom, sc.nombre_scom, 
	ind.codigo_ind, ind.nombre_ind, ind.descripcion_ind 
	FROM dbo.CompetenciaAprendizaje(NOLOCK) ca
	INNER JOIN dbo.ADM_Componente_CompetenciaAprendizaje(NOLOCK) cca ON ca.codigo_com = cca.codigo_com
	INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) sc ON ca.codigo_com = sc.codigo_com
	INNER JOIN dbo.ADM_Indicador(NOLOCK) ind ON sc.codigo_scom = ind.codigo_scom 
	WHERE ca.admision_com = 1 and ca.estado_com = 1 AND sc.estado_scom = 1 AND ind.estado_ind = 1 AND cca.estado_cca = 1
	AND cca.codigo_cmp = @codigo_cmp
END
GO

GRANT EXECUTE ON ADM_ReporteMatrizComponente TO usuariogeneral
GRANT EXECUTE ON ADM_ReporteMatrizComponente TO IusrReporting
GO

