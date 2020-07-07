<!--#include file="../../../NoCache.asp"-->
<%

if session("codigo_alu")="" then
	response.write "Error de Inicio de Sesión!"
else

codigo_alu= request.form("codigo_alu")
codigo_sco= request.form("codigo_sco")
codigo_cac= request.form("codigo_cac")
cantidad = request.form("cantidad")
partes = request.form("cboPartes")

'-----------------------------------------------------------
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = obj.consultar("ConsultarServicioConcepto", "FO", "CO",codigo_sco)

if codigo_sco=1 then
	partes = 3
	fechaVencimiento_sco = "31/03/2011"
	codigo_cco= 2282  
	precio_sco=150
end if

if codigo_sco= 2 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2281
	precio_sco=220
end if

if codigo_sco= 3 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2277
	precio_sco=	180
end if

if codigo_sco=4 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2278
	precio_sco= 200	
end if

if codigo_sco=5 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2280
	precio_sco= 200		
end if

if codigo_sco=6 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2283
	precio_sco= 200	
end if

if codigo_sco=7 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2279
	precio_sco= 200
end if

if codigo_sco=8 then
	partes = 2
	fechaVencimiento_sco = "28/02/2011"
	codigo_cco= 2276
	precio_sco= 240
end if

codigo_sco= 630
cantidad = 1
moneda_sco= rs("moneda_sco")
generamora_sco= rs("generamora_sco")

fechaInicio = "01/01/2011" 'empezar a cobrar desde esta fecha (ESTA FECHA SE SUBIO)
codigo_per=null
codigo_ocl=null

fecha = date()   'fecha de cargo

rs.close
set rs=nothing
'---------------------------------------------------------------

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda_v2", "FO", "E",codigo_alu, codigo_sco,codigo_cac, codigo_cco)
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
		 ' precio_sco = cdbl(precio_sco) * cdbl(cantidad)
		        
		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "E", codigo_alu,  codigo_per, codigo_ocl, codigo_sco, codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
      StrBitacora = "TipoCliente=E" + "||codigo_cli=" + cstr(codigo_alu) + "||codigo_sco=" + cstr(codigo_sco) + "||total=" + moneda_sco + " "         + cstr(precio_sco)
     ' response.Write(StrBitacora)
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