<!--#include file="../../../../NoCache.asp"-->
<%

if session("codigo_alu")="" then
	response.write "Error de Inicio de Sesión!"
else

codigo_alu= request.form("codigo_alu")
codigo_cac= request.form("codigo_cac")
cantidad = request.form("cantidad")
partes = request.form("cboPartes")

'-----------------------------------------------------------
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion


if request.form("codigo_sco")=1 then
    precio_sco= 45
	codigo_sco= 630   
	codigo_cco= 2218 	
	cantidad =1	 
end if



set rs = obj.consultar("ConsultarServicioConcepto", "FO", "CO",codigo_sco)
moneda_sco= rs("moneda_sco")
generamora_sco= rs("generamora_sco")
fechaVencimiento_sco = "30/12/2010"

fechaInicio = "01/12/2010" 
codigo_per=null
codigo_ocl=null

fecha = date()   'fecha de cargo

rs.close
set rs=nothing
'---------------------------------------------------------------


set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda_v2", "FO", "E",codigo_alu, codigo_sco,37, codigo_cco)
obj.CerrarConexion


if rs.recordcount >0 then
'response.redirect "inscripcionEventoGeneral_v2.asp?msg='Usted ya ha solicitado este servicio.'"

%>
       <script type="text/javascript" language="JavaScript">
            alert ("ERROR: Este servicio ya lo ha solicitado.\nPor este medio, sólo podrá solicitar cada servicio una vez por ciclo.");
            location.href ="inscripcionEventoGeneral.asp";
        </script> 
		
<% 		
else

    set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
    obj.AbrirConexionTrans
		'Grabar Deuda
		  precio_sco = cdbl(precio_sco) 
		        
		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "E", codigo_alu,  codigo_per, codigo_ocl, codigo_sco, codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=E" + "|| codigo_cli=" + cstr(codigo_alu) + "|| codigo_sco=" + cstr(codigo_sco) + "|| total=" + moneda_sco + " "         + cstr(precio_sco)
      
   	     obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "E", codigo_alu, "", StrBitacora, "desde web"

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