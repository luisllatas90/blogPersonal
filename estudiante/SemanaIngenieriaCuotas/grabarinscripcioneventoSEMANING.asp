<!--#include file="../NoCache.asp"-->
<%
if session("codigo_alu")="" then
	response.write "Error de Inicio de Sesión!"
else
codigo_alu= request.form("codigo_alu")
codigo_sco= request.form("codigo_sco")
precio_sco= request.form("precio_sco")
moneda_sco= request.form("moneda_sco")
generamora_sco= request.form("generamora_sco")
fechaVencimiento_sco = request.form("fechaVencimiento_sco")
codigo_cco= request.form("codigo_cco")
partes =request.form("cbopartes")

fechaInicio = date() 'empezar a cobrar desde esta fecha
codigo_per=null
codigo_ocl=null
codigo_cac=30 '2008-I

fecha = date()   'fecha de cargo

'Buscar si ya tiene cargo registrado
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexionTrans
	set rs = Server.CreateObject("ADODB.RecordSet")
	set rs = obj.consultar("consultarExistenciaDeuda", "FO", "E",codigo_alu, codigo_sco,0)
	if rs.recordcount >0 then
		response.redirect "inscripcioneventoSEMANING.asp"
	else
		'Grabar Deuda
   		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "E", codigo_alu,  codigo_per, codigo_ocl, codigo_sco,          codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=E" + "||codigo_cli=" + codigo_alu + "||codigo_sco=" + codigo_sco + "||total=" + moneda_sco + " "         + precio_sco
      
   	    obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "E", codigo_alu, "", StrBitacora, "desde web"
   end if

	rs.close
	set rs=nothing

 obj.CerrarConexionTrans
 set obj = nothing
 response.redirect "inscripcioneventoSEMANING.asp"
end if

%>