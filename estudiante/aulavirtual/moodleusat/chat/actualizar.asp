<!--#include file ="emoticons.asp"-->
<%
if request.querystring("M_send")<>"" then
	'Enviar mensajes digitados por el usuario
	Set Obj=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	Obj.AbrirConexion
		call Obj.Ejecutar("DI_Agregarmensajechat",false,replace(session("codigo_usu"),"-","\"),replace(Request.querystring("M_send"),"'","&#39;"),"",session("idsesion"),session("idcursovirtual"))
	Obj.CerrarConexion
	Set ObjChat=nothing
	
	on error resume next
	if err<>0 then
	  	Response.Write("No se han actualizado los permisos necesarios para iniciar el chat en línea!")
	else
	  Response.Write("<h3>" & recaffected & " Registros agregados</h3>")
	end if

end if
	'Actualizar últimos mensajes enviados por la sesión
	if request.querystring("codigo_usu")<>"" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			call Obj.Ejecutar("DI_Actualizarsesionchat",false,now,replace(session("codigo_usu"),"-","\"),session("idsesion"))
		obj.CerrarConexion
		Set Obj=nothing
  	end if

	'Recuperar los usuarios que iniciaron sesión en el curso virtual
	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
		set rsUsuarios=Obj.Consultar("ConsultarChat","FO",3,session("idchat"),"","")
		Set rsMensajes=Obj.Consultar("ConsultarChat","FO",4,replace(session("codigo_usu"),"-","\"),session("idcursovirtual"),"")
		
		obj.CerrarConexion
	Set Obj=nothing
	isuser=false
	firstname=true
	i=0
	If not(rsUsuarios.BOF and rsUsuarios.EOF) then
		Do while not rsUsuarios.EOF
			if DateDiff("s",rsUsuarios(1),Now())<5 then
				if NOT firstname then
					response.write "^#^"
				end if
				response.write rsUsuarios("nombreusuario")
				isuser=true
				firstname=false
			end if
			rsUsuarios.movenext
		Loop
	else
		response.write "No se hay usuarios en el CHAT!"
	end if
	set Usuarios=nothing
	response.write "!#!"
	
	temp = ""
	j=0
	firstmessage=true
	If not(rsMensajes.BOF and rsMensajes.EOF) then
		Do while Not rsMensajes.EOF
			if NOT firstmessage then
				temp="^#^" & temp
			end if
			temp = "<b>" & rsMensajes("nombreusuario") & "</b> dice: " & BBCode(replace(rsMensajes("mensaje"),"&#39;","'")) & "&nbsp;(<span class='variable'>" & rsMensajes("fechareg") & "</span>)" & temp
			firstmessage=false
			rsMensajes.movenext
		Loop
	end if
	set rsMensajes=nothing
	response.write temp
	'if j=0 then response.write "No hay mensajes enviados!"
%>