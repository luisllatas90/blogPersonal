
-- =============================================
-- Author:		ENevado
-- Create date: 2020-09-14
-- Description:	
-- ==============================================================================================
-- HISTORIAL DE CAMBIOS
-- ==============================================================================================
-- CÓDIGO	FECHA		DESARROLLADOR	DESCRIPCIÓN
-- 001		2020-12-15	ENevado			Listado por tipo de usuario
-- ==============================================================================================
ALTER PROCEDURE ADM_TipoGrupoEvaluacion_Listar 
	@tipoOpe VARCHAR(2) = '',
	@codigo_tge INT = -1
AS
BEGIN
	IF @tipoOpe = ''
	BEGIN
		SELECT tge.codigo_tge, tge.nombre_tge, tge.estado_tge
		FROM dbo.ADM_TipoGrupoEvaluacion(NOLOCK) tge
		WHERE tge.estado_tge = 1
	END
	ELSE
	IF @tipoOpe = 'TU' -- 001
	BEGIN
		IF (@codigo_tge = 26 or @codigo_tge = 168)
		BEGIN
			SELECT tge.codigo_tge, tge.nombre_tge, tge.estado_tge
			FROM dbo.ADM_TipoGrupoEvaluacion(NOLOCK) tge
			WHERE tge.estado_tge = 1 AND tge.codigo_tge = 3
		END
		ELSE
		BEGIN
			SELECT tge.codigo_tge, tge.nombre_tge, tge.estado_tge
			FROM dbo.ADM_TipoGrupoEvaluacion(NOLOCK) tge
			WHERE tge.estado_tge = 1 AND tge.codigo_tge <> 3
		END
	END
END

