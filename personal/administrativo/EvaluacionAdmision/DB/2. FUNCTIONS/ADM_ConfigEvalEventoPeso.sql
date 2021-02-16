-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE FUNCTION dbo.ADM_ConfigEvalEventoPeso(
	@codigo_cee int
) RETURNS NVARCHAR(MAX)
AS BEGIN
	DECLARE @val NVARCHAR(MAX) = ''
	
	SELECT @val = @val + (CASE WHEN @val = '' THEN 'E'+CONVERT(VARCHAR,ceep.nro_orden_ceep)+': '+CONVERT(VARCHAR,(ceep.peso_ceep*100))+' %' 
							ELSE '<br />' + 'E'+CONVERT(VARCHAR,ceep.nro_orden_ceep)+': '+CONVERT(VARCHAR,(ceep.peso_ceep*100))+' %' END) 
	FROM dbo.ADM_ConfiguracionEvaluacionEvento_Peso(nolock) ceep
	WHERE ceep.estado_ceep = 1 AND ceep.codigo_cee = @codigo_cee
	order by ceep.nro_orden_ceep
		
	RETURN @val 
END