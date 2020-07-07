<!--#include file="../../../NoCache.asp"-->
<!--#include file="../../../clsmensajes.asp"-->
<%
accion=request.querystring("accion")
codigo_cup=request.QueryString("codigo_cup")
codigo_pes=request.querystring("codigo_pes")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
codigo_cpf=request.querystring("codigo_cpf")
codigo_cur=request.querystring("codigo_cur")
modulo=request.querystring("modulo")

descripcion_cac=request.querystring("descripcion_cac")
usuario=session("Usuario_bit")

	if accion="eliminarsilabos" then
		archivo=server.MapPath("../../../silabos/") & "\" & descripcion_cac & "\"
		archivo=archivo & codigo_cup & ".zip"

		set obFile=server.createObject("Scripting.FileSystemObject")
			If obFile.FileExists(archivo) Then
				eliminado=true
				obFile.DeleteFile(archivo)
			End If
		set obFile=nothing
	'response.Write(codigo_cup)
		if eliminado=true then
			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				Call Obj.Ejecutar("AgregarSilabo",false,"E",codigo_cup,session("codigo_usu"))
			Obj.CerrarConexion
			Set Obj=Nothing
	  	
			response.redirect "frmadminsilabosAdministrador.asp?codigo_cac=" & codigo_cac & "&codigo_cpf=" & codigo_cpf & "&descripcion_cac=" & descripcion_cac & "&mod=" & modulo
			'response.write "<script>window.opener.location.reload();window.close()</script>"
		else
			response.write("<h2>No se ha eliminado el archivo " & archivo & "</h2>")
		
		end if
	end if
	
	if accion="eliminarsilabos2" then
		archivo=server.MapPath("../../../silabos/") & "\" & descripcion_cac & "\"
		archivo=archivo & codigo_cup & ".zip"

		set obFile=server.createObject("Scripting.FileSystemObject")
			If obFile.FileExists(archivo) Then
				eliminado=true
				obFile.DeleteFile(archivo)
			End If
		set obFile=nothing
	
		if eliminado=true then
			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				Call Obj.Ejecutar("AgregarSilabo",false,"E",codigo_cup,session("codigo_usu"))
			Obj.CerrarConexion
			Set Obj=Nothing
	  	
			response.redirect "frmadminsilabospp.asp?codigo_cac=" & codigo_cac & "&codigo_cpf=" & codigo_cpf & "&descripcion_cac=" & descripcion_cac
			'response.write "<script>window.opener.location.reload();window.close()</script>"
		else
			response.write("<h2>No se ha eliminado el archivo " & archivo & "</h2>")
		
		end if
	end if
	
	if accion="eliminarsilabos3" then
		
			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				Call Obj.Ejecutar("AgregarSilabo",false,"E",codigo_cup,session("codigo_usu"))
			Obj.CerrarConexion
			Set Obj=Nothing
	  	
			response.redirect "frmadminsilabos.asp?codigo_cac=" & codigo_cac & "&codigo_cpf=" & codigo_cpf & "&descripcion_cac=" & descripcion_cac & "&mod=2" 
			'response.write "<script>window.opener.location.reload();window.close()</script>"
		
	end if
%>