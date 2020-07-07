<!--#include file="../../../../NoCache.asp"-->
<%

if request.querystring("codigo_per")="" then
	response.write "Error de Inicio de Sesión!"
else
codigo_per = request.querystring("codigo_per")
codigo_cac= request.form("codigo_cac")
cantidad = request.form("cantidad")
partes = request.form("cboPartes")

'-----------------------------------------------------------
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion


if request.form("codigo_sco")=10 then
    precio_sco= 10
	codigo_sco= 630   
	codigo_cco= 2237	
	cantidad =1	 
end if

if request.form("codigo_sco")=20 then
    precio_sco= 20
	codigo_sco= 630   
	codigo_cco= 2237	
	cantidad =1	 
end if

if request.form("codigo_sco")=50 then
    precio_sco= 50
	codigo_sco= 630   
	codigo_cco= 2237	
	cantidad =1	 
end if


set rs = obj.consultar("ConsultarServicioConcepto", "FO", "CO",codigo_sco)
moneda_sco= rs("moneda_sco")
generamora_sco= rs("generamora_sco")
fechaVencimiento_sco = "30/12/2010"

fechaInicio = "01/12/2010" 
codigo_alu=null
codigo_ocl=null

fecha = date()   'fecha de cargo

rs.close
set rs=nothing
'---------------------------------------------------------------


set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda_v2", "FO", "P",codigo_per, codigo_sco,37, codigo_cco)
obj.CerrarConexion


if rs.recordcount >0 then
'response.redirect "inscripcionEventoGeneral_v2.asp?msg='Usted ya ha solicitado este servicio.'"

%>
       <script type="text/javascript" language="JavaScript">
            alert ("ERROR: Este servicio ya lo ha solicitado.\nPor este medio, sólo podrá solicitar cada servicio una vez por ciclo.");
			window.close();
        </script> 
		
<% 		
else

    set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
    obj.AbrirConexionTrans
		'Grabar Deuda
		  precio_sco = cdbl(precio_sco) 
		        
		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "P", codigo_alu,  codigo_per, codigo_ocl, codigo_sco, codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=P" + "|| codigo_cli=" + cstr(codigo_per) + "|| codigo_sco=" + cstr(codigo_sco) + "|| total=" + moneda_sco + " "         + cstr(precio_sco)
      
   	     obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "P", codigo_per, "", StrBitacora, "desde web"

    obj.CerrarConexionTrans
    set obj = nothing
    %>
        <script type="text/javascript" language="JavaScript">
            alert ("Operacion registrada correctamente.");
			window.close();       
        </script>
    <%

	 'response.redirect "inscripcionEventoGeneral.asp"

end if
	  
end if


%>