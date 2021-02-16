/*    
* Fecha Crea: 16/08/2018 - 11:23:00am     
* Modificado por: HCano    
* Descripción: Obtener el ultimo ID generado segun tabla, id de transaccion y nro de operacion
 /* Historial de Cambios */    
 -- Codigo Fecha  Usuario  Detalle    
*/  
CREATE PROCEDURE [dbo].[ObtenerUltimoIDArchvoCompartido]
@idtabla int,
@idtransaccion int,
@nrooperacion int
AS
BEGIN

SELECT TOP 1 Idarchivoscompartidos AS idarchivo,NombreArchivo,Extencion AS Extension,RutaArchivo AS ruta 
FROM ArchivoCompartido 
WHERE IdTabla=@idtabla AND IdTransaccion=@idtransaccion AND NroOperacion=@nrooperacion
ORDER BY idarchivo DESC

END

GO

GRANT EXECUTE ON [dbo].[ObtenerUltimoIDArchvoCompartido] to usuariogeneral
GRANT EXECUTE ON [dbo].[ObtenerUltimoIDArchvoCompartido] to IusrReporting
GRANT EXECUTE ON [dbo].[ObtenerUltimoIDArchvoCompartido] to IUsrVirtualSistema
