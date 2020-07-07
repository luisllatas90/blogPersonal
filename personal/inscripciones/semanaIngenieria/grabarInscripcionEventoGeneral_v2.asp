<!--#include file="../../../NoCache.asp"-->
<%

if session("codigo_Usu")="" then
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
precio_sco= rs("precio_sco")
moneda_sco= rs("moneda_sco")
generamora_sco= rs("generamora_sco")
fechaVencimiento_sco = rs("fechaVencimiento_sco")
codigo_cco= rs("codigo_cco")


fechaInicio = "01/06/2009" 'empezar a cobrar desde esta fecha
codigo_alu=null
codigo_ocl=null

fecha = date()   'fecha de cargo

rs.close
set rs=nothing
'---------------------------------------------------------------


set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda", "FO", "P",codigo_per, codigo_sco,codigo_cac)
obj.CerrarConexion

if rs.recordcount >0 then
'response.redirect "inscripcionEventoGeneral_v2.asp?msg='Usted ya ha solicitado este servicio.'"

%>
       <script type="text/javascript" language="JavaScript">
            alert ("ERROR: Este servicio ya lo ha solicitado.\nSólo podrá solicitar cada servicio una vez.");
            location.href ="inscripcionEventoGeneral_v2.asp";
        </script> 
		
<% 		
else

    set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
    obj.AbrirConexionTrans
		'Grabar Deuda
		  precio_sco = cdbl(precio_sco) * cdbl(cantidad)
		        
		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "P", codigo_alu,  codigo_per, codigo_ocl, codigo_sco, codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
          
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=P" + "||codigo_cli=" + codigo_per + "||codigo_sco=" + codigo_sco + "||total=" + moneda_sco + " "         + cstr(precio_sco)
      
   	     obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "P", codigo_per, "", StrBitacora, "desde web"



    obj.CerrarConexionTrans
    set obj = nothing
    %>
        <script type="text/javascript" language="JavaScript">
            alert ("Operacion registrada correctamente.");
            location.href ="inscripcionEventoGeneral_v2.asp";
        </script>
    <%
    
    'response.redirect "inscripcionEventoGeneral_v2.asp"

end if
	  
end if


%>