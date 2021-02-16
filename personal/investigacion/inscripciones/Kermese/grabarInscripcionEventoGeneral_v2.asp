<!--#include file="../../../NoCache.asp"-->
<%
if session("codigo_usu")="" then
	response.write "Error de Inicio de Sesión!"
else



codigo_per= request.form("codigo_per")
codigo_sco= request.form("codigo_sco")
codigo_cac= request.form("codigo_cac")
precio_sco= request.form("precio_sco")
moneda_sco= request.form("moneda_sco")
cantidad = request.form("cantidad")

generamora_sco= request.form("generamora_sco")
fechaVencimiento_sco = request.form("fechaVencimiento_sco")
codigo_cco= request.form("codigo_cco")
partes =request.form("cbopartes")

fechaInicio = date() 'empezar a cobrar desde esta fecha
codigo_alu=null
codigo_ocl=null


fecha = date()   'fecha de cargo



set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("ContarCantidadEntradas", "FO", codigo_sco,codigo_cac)
obj.CerrarConexion

if rs("limite")="S" then


'Buscar si ya tiene cargo registrado
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexionTrans
		'Grabar Deuda
		  precio_sco = (precio_sco* cantidad)  
		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "P", codigo_alu,  codigo_per, codigo_ocl, codigo_sco,          codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=P" + "||codigo_cli=" + codigo_per + "||codigo_sco=" + codigo_sco + "||total=" + moneda_sco + " "         + cstr(precio_sco)
      
   	    obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "P", codigo_per, "", StrBitacora, "desde web"
   
 obj.CerrarConexionTrans
 set obj = nothing
 response.redirect "inscripcionEventoGeneral_v2.asp"


else
	  		response.write "Ya no hay entradas disponibles."
  		%>
			<input OnClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
			
<%end if
	  
	  
end if


%>