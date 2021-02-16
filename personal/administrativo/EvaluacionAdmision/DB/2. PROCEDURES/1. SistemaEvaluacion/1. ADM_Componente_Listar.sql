
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Componente_Listar 
	@tipoOpe varchar(2),
	@codigo_cmp int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT cmp.codigo_cmp, cmp.nombre_cmp, cmp.estado_cmp
		FROM dbo.ADM_Componente(NOLOCK) cmp
		WHERE cmp.estado_cmp = 1
	END
END
GO

GRANT EXECUTE ON ADM_Componente_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Componente_Listar TO usuariogeneral
GO