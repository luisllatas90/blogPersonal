
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_SubCompetencia_Listar 
	@tipoOpe varchar(2),
	@codigo_com int,
	@codigo_scom int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT scom.codigo_scom, scom.nombre_scom, scom.estado_scom
		FROM dbo.ADM_SubCompetencia(NOLOCK) scom
		WHERE scom.estado_scom = 1
	END

    IF @tipoOpe = '1'
    BEGIN
        SELECT scom.codigo_scom, scom.nombre_scom
        FROM ADM_SubCompetencia scom WITH (NOLOCK)
        WHERE scom.estado_scom = 1 AND (@codigo_com = 0 OR scom.codigo_com = @codigo_com)
    END
END
GO

GRANT EXECUTE ON ADM_SubCompetencia_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_SubCompetencia_Listar TO usuariogeneral
GO