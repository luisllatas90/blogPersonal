<!--#include file="../../../NoCache.asp"-->
<%
if session("codigo_Usu")="" then

	response.write "Error de Inicio de Sesión!"
	
else
	codigo_per= request.form("codigo_per")
	codigo_sco= request.form("codigo_sco")
	precio_sco= request.form("precio_sco")
	moneda_sco= request.form("moneda_sco")
	generamora_sco= request.form("generamora_sco")
	fechaVencimiento_sco = request.form("fechaVencimiento_sco")
	codigo_cco= request.form("codigo_cco")
	apellidos_Afi= request.form("txtApellidos")
	nombres_Afi = request.form("txtNombres")
	nroamigos= request.form("nroamigos") 'nro maximo de acompañantes (2)
	
	
	partes = 1

	fechaInicio = date() 'empezar a cobrar desde esta fecha
	codigo_alu=null
	codigo_ocl=null
	codigo_cac=30 '2008-I

	fecha = date()   'fecha de cargo

	'Buscar si ya tiene cargo registrado
	set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexionTrans
	set rs = Server.CreateObject("ADODB.RecordSet")
	
	'Consultar si existe la deuda para la fiesta de gala
	set rs = obj.consultar("consultarExistenciaDeuda", "FO", "P",codigo_per, codigo_sco,0)
	
	if rs.recordcount >0 then
		
		
		if  cdbl(rs("montoTotal_deu")) < nroamigos*precio_sco then 'para controlar el maximo de acompañantes
		
			lngCodigo_Deu = rs("codigo_deu")	
			'Modificar EL MONTO de la deuda
			obj.Ejecutar "ModificarDeuda", False,lngCodigo_Deu, "", (cdbl(rs("montoTotal_deu")) + precio_sco),generamora_sco,fecha
	
	
			'-----REGISTRAR BITACORA
		     StrBitacora = "TipoCliente=P" + "||codigo_cli=" + codigo_per + "||codigo_sco=" + codigo_sco + "||total=" + moneda_sco + " "         + precio_sco
      
   			 obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "MODIFICAR", "P", codigo_per, "", StrBitacora, "desde web" 
   			 
   			 'Agregar Acompañante
			 obj.Ejecutar "AgregarAcompañanteFiesta", False,lngCodigo_Deu, apellidos_Afi, nombres_Afi,precio_sco,"C" 
	
   		else 'ya tiene el maximo de acompañantes
   		
   			rs.close
			set rs=nothing

   			obj.CancelarConexionTrans
   			set obj = nothing
			response.redirect "fiestaGala.asp"
   		end if
   		
   		
   	    
	else
		'Grabar Deuda
   		  lngCodigo_Deu = obj.Ejecutar("AgregarDeuda_v2", True, fecha,  "", "P", codigo_alu,  codigo_per, codigo_ocl, codigo_sco,          codigo_cac, "Inscripcion Web (" + cstr(partes) + " Partes)", precio_sco, moneda_sco, precio_sco, "P",          generamora_sco,fechaVencimiento_sco , codigo_cco, 1 , Null, Null, 0, 0,partes, fechaInicio, Null)
            
         '-----REGISTRAR BITACORA
         StrBitacora = "TipoCliente=P" + "||codigo_cli=" + codigo_per + "||codigo_sco=" + codigo_sco + "||total=" + moneda_sco + " "         + precio_sco
      
   	    obj.Ejecutar "RegistrarBitacoraCaja", False, "DEUDA", lngCodigo_Deu, "AGREGAR", "P", codigo_per, "", StrBitacora, "desde web"
   	    
   	    
   	    'Agregar Acompañante
		obj.Ejecutar "AgregarAcompañanteFiesta", False,lngCodigo_Deu, apellidos_Afi, nombres_Afi,precio_sco,"C" 
	
   end if


	rs.close
	set rs=nothing

	obj.CerrarConexionTrans
	set obj = nothing
	response.redirect "fiestaGala.asp"
end if

%>