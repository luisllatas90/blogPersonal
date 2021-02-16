<!--#include file="../../../NoCache.asp"-->
<%

if session("codigo_usu")="" then
	response.write "Error de Inicio de Sesión!"
else

codigo_per= request.form("codigo_per")
codigo_sco= request.form("codigo_sco")
codigo_cac= request.form("codigo_cac")
cantidad = request.form("cantidad")
partes =  request.form("cboPartes")

'-----------------------------------------------------------
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = obj.consultar("ConsultarServicioConcepto", "FO", "CO",codigo_sco)
precio_sco= 25
moneda_sco= rs("moneda_sco")
generamora_sco= rs("generamora_sco")
fechaVencimiento_sco = rs("fechaVencimiento_sco")
codigo_cco= 1751 'rs("codigo_cco") '1558 Entendiendo las adicciones

fechaInicio = "01/07/2010" 'empezar a cobrar desde esta fecha
codigo_alu=null
codigo_ocl=null

fecha = date()   'fecha de cargo

rs.close
set rs=nothing
'---------------------------------------------------------------


set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda_v2", "FO", "P",codigo_per, codigo_sco,36, codigo_cco)
obj.CerrarConexion

if rs.recordcount >0 then
'response.redirect "inscripcionEventoGeneral_v2.asp?msg='Usted ya ha solicitado este servicio.'"

%>
       <script type="text/javascript" language="JavaScript">
            alert ("ERROR: Este servicio ya lo ha solicitado.\nPor este medio, sólo podrá solicitar cada servicio una vez.");
            location.href ="inscripcionEventoGeneral.asp";
        </script> 
		
<% 		
else

    set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
    obj.AbrirConexionTrans
		'Grabar Deuda
		  precio_sco = cdbl(precio_sco) * cdbl(cantidad)
		        
		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "P", codigo_alu,  codigo_per, codigo_ocl, codigo_sco, codigo_cac, "Inscripcion Web " , precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=P" + "||codigo_cli=" + codigo_per + "||codigo_sco=" + codigo_sco + "||total=" + moneda_sco + " "         + cstr(precio_sco)
      
   	     obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "P", codigo_per, "", StrBitacora, "desde web"

    obj.CerrarConexionTrans
    set obj = nothing
    %>
        <script type="text/javascript" language="JavaScript">
            alert ("Operacion registrada correctamente.");
            location.href ="inscripcionEventoGeneral.asp";
        </script>
    <%
    
    'response.redirect "inscripcionEventoGeneral.asp"

end if
	  
end if


%>