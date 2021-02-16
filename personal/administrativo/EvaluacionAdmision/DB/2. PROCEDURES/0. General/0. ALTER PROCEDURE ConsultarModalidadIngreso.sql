/* CAMBIO FECHA  HECHO POR OBSERVACION
 * 001  19/10/2017 CSenmache Solo modalidad activas - Solicitado x Esaavedra
 * 002  07/08/2018 HCano	Se agrega tipo 'NV', se muestran todas las modalidades y se concatena (NO VIGENTE) a las no vigentes
 * 003  03/02/2020 andy.diaz Se agrega tipo 'CT', se muestran todas las modalidades por tipo de estudio (codigo_test)
 * 004  12/10/2020 andy.diaz Se agrega tipo 'CO' para buscar una modalidad por codigo_min
 */
ALTER PROCEDURE [dbo].[ConsultarModalidadIngreso] (
 @tipo CHAR(2)
 ,@param VARCHAR(50)
 )
AS
IF @tipo = 'TO'
BEGIN
 SELECT *
 FROM ModalidadIngreso
 WHERE eliminado_Min = 0 /* 001 */
 ORDER BY nombre_Min
END

IF @tipo = 'EP' -- Escuela Pre Universitaria
BEGIN
 SELECT *
 FROM ModalidadIngreso
 WHERE eliminado_Min = 0 /* 001 */
  AND (
   codigo_Min = 4
   OR codigo_Min = 9
   OR codigo_Min = 15
   OR codigo_Min = 8
   OR codigo_Min = 1
   OR codigo_Min = 6
   OR codigo_Min = 20
   OR codigo_Min = 21
   OR codigo_Min = 14
   OR codigo_Min = 3
   OR codigo_Min = 23
   OR codigo_Min = 33
  )
 ORDER BY nombre_min
END

IF @tipo = 'NM'
BEGIN
 SELECT *
 FROM ModalidadIngreso
 WHERE nombre_Min = @param AND eliminado_Min = 0 /* 001 */
 ORDER BY nombre_Min
END

IF @tipo = 'NV'
BEGIN
 SELECT codigo_Min,nombre_Min + CASE WHEN eliminado_Min=1 THEN ' (NO VIGENTE)' ELSE '' END AS nombre_Min
 FROM ModalidadIngreso
 ORDER BY nombre_Min
END

--003: Por tipo de estudio
IF @tipo = 'CT'
 BEGIN
  SELECT
     isnull(min.codigo_Min, 0)       AS codigo_Min
   , isnull(min.nombre_Min, '')      AS nombre_Min
   , isnull(min.abreviatura_Min, '') AS abreviatura_Min
  FROM ModalidadIngreso min WITH (NOLOCK)
   JOIN ModalidadIngresoTipoEstudio mte WITH (NOLOCK) ON min.codigo_Min = mte.codigo_Min
  WHERE cast(mte.codigo_Test AS INTEGER) = @param
   AND min.eliminado_Min = 0
  ORDER BY min.nombre_Min
 END

--004: Por codigo_min
IF @tipo = 'CO'
BEGIN
 SELECT min.codigo_Min, min.nombre_Min
 FROM ModalidadIngreso min with (NOLOCK )
 WHERE min.codigo_Min = cast(@param AS INT)
END
GO