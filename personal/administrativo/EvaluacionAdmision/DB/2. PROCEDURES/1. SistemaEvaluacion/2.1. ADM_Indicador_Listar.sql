
-- =============================================
-- Author:		ENevado
-- Create date: 2020-08-21
-- Description:	
-- =============================================
CREATE PROCEDURE ADM_Indicador_Listar
	@tipoOpe varchar(2),
	@codigo_scom int,
	@codigo_ind int
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT ind.codigo_scom, ind.nombre_ind, ind.descripcion_ind, ind.estado_ind, ISNULL(ind.descripcion_ind, '') descripcion_ind
		FROM dbo.ADM_Indicador(NOLOCK) ind
		WHERE ind.estado_ind = 1
	END
	ELSE
	IF @tipoOpe = '1'
	BEGIN
		SELECT ind.codigo_ind, ind.nombre_ind, scom.codigo_scom, scom.nombre_scom, com.codigo_com, com.nombre_com, ind.estado_ind, ISNULL(ind.descripcion_ind, '') descripcion_ind
		FROM dbo.ADM_Indicador(NOLOCK) ind
		INNER JOIN dbo.ADM_SubCompetencia(NOLOCK) scom ON ind.codigo_scom = scom.codigo_scom
		INNER JOIN dbo.CompetenciaAprendizaje(NOLOCK) com ON scom.codigo_com = com.codigo_com
		WHERE ind.estado_ind = 1 AND scom.estado_scom = 1 AND com.estado_com = 1
		and scom.codigo_com = @codigo_scom
		ORDER BY com.codigo_com, scom.codigo_scom
	END

    IF @tipoOpe = '2'
    BEGIN
        SELECT ind.codigo_ind, ind.nombre_ind, ISNULL(ind.descripcion_ind, '') descripcion_ind
        FROM ADM_Indicador ind WITH (NOLOCK)
        WHERE 1 = 1
          AND (@codigo_scom = 0 OR ind.codigo_scom = @codigo_scom)
          AND ind.estado_ind = 1
    END
END
GO

GRANT EXECUTE ON ADM_Indicador_Listar TO iusrvirtualsistema
GRANT EXECUTE ON ADM_Indicador_Listar TO usuariogeneral
GO