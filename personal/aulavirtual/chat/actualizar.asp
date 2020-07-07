<!--#include file ="emoticons.asp"-->
<%
if request.querystring("M_send")<>"" then
	'Enviar mensajes digitados por el usuario
	Set Obj= Server.CreateObject("AulaVirtual.clsChat")
		call Obj.AgregarMensaje(replace(session("codigo_usu"),"-","\"),replace(Request.querystring("M_send"),"'","&#39;"),"",session("idsesion"),session("idcursovirtual"))
	Set Obj=nothing
	
	on error resume next
	if err<>0 then
	  	Response.Write("No se han actualizado los permisos necesarios para iniciar el chat en línea!")
	else
	  Response.Write("<h3>" & recaffected & " Registros agregados</h3>")
	end if

end if
	'Actualizar últimos mensajes enviados por la sesión
	if request.querystring("codigo_usu")<>"" then
		Set Obj= Server.CreateObject("AulaVirtual.clsChat")
			call Obj.ActualizarSesion(now,replace(session("codigo_usu"),"-","\"),session("idsesion"))
		Set Obj=nothing
  	end if

	'Recuperar los usuarios que iniciaron sesión en el curso virtual
	Set Obj= Server.CreateObject("AulaVirtual.clschat")
		Arrsesion=Obj.Consultar("2",session("idcursovirtual"),"","")
	Set Obj=nothing
	isuser=false
	firstname=true
	i=0
	If IsEmpty(Arrsesion)=false then
		for i=0 to Ubound(Arrsesion,2)
			if DateDiff("s",Arrsesion(1,i),Now())<5 then
				if NOT firstname then
					response.write "^#^"
				end if
				response.write Arrsesion(5,i)
				isuser=true
				firstname=false
			end if
		next
	else
		response.write "No se hay usuarios en el CHAT!"
	end if
	response.write "!#!"
	
	Set Obj= Server.CreateObject("AulaVirtual.clschat")
		ArrMensaje=Obj.Consultar("1",replace(session("codigo_usu"),"-","\"),session("idcursovirtual"),"")
	Set Obj=nothing

	temp = ""
	j=0
	firstmessage=true
	if IsEmpty(Arrmensaje)=false then
		for j=0 to Ubound(ArrMensaje,2)
			if NOT firstmessage then
				temp="^#^" & temp
			end if
			temp = "<b>" & ArrMensaje(7,j) & "</b> dice: " & BBCode(replace(ArrMensaje(3,j),"&#39;","'")) & "&nbsp;(<span class='variable'>" & ArrMensaje(1,j) & "</span>)" & temp
			firstmessage=false
		next
	end if
	response.write temp
	if j=0 then response.write "No hay mensajes enviados!"
%>