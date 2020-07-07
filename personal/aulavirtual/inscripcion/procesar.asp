<!--#include file="../../NoCache.asp"-->
<%
'On error Resume next
if session("codigo_usu")="" then response.redirect "../../tiempofinalizado.asp"

	accion=request.querystring("accion")
	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente su Inscripción. Por favor intente denuevo"");history.back(-1)</script>"
	End function
	
	Private Function mensajeregistro()
		mensajeregistro="<script>alert(""Se ha registrado correctamente su inscripción en el Curso. Gracias por su tiempo."");top.window.opener=self;top.window.close()</script>"	
	end function
		
	codigo_per=session("codigo_usu")				
	codigo_cpf=request.form("cbxescuela")
	codigo_dac=request.form("cbxdepartamento")
	nombre_cur=request.form("cbxasignatura")
	eje=request.form("cbxeje")
	tiempo=request.form("txttiempo")
	tipotiempo=request.form("cbxtipotiempo")
	tipomotivo=request.form("opttipomotivo")
	obs=request.form("txtobs")
	modalidad=request.form("chkmodalidad")
	if modalidad="" then modalidad="P"
	if obs="" then obs=" "
				
	Set Obj= Server.CreateObject("PryUSAT.clsFuncionesADO")
  		call Obj.procedimientoAccion("Agregarinscripcioncursovirtual",false,codigo_per,codigo_cpf,codigo_dac,nombre_cur,eje,tiempo,tipotiempo,tipomotivo,modalidad,"",obs)
	Set Obj=Nothing
		if Err.Number<>0 then
			response.write cerrarventanaerror
		else
			response.write mensajeregistro
		end if
		'response.write codigo_per & "," & codigo_cpf & "," &  codigo_dac & "," &  nombre_cur & "," &  eje & "," &  tiempo & "," & tipotiempo & "," &  tipomotivo & "," &  foto & "," &  obs
%>