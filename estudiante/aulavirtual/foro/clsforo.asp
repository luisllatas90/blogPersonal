<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Viernes, 09 de Diciembre de 2005 9:57:06 a.m.

Class clsforo
  Public idforo
  Public filamodificada

  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_foro(strX)
  	idforo=strX
  End Property
  
  Public Property LET codigo_fila(strX)
  	filamodificada=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "PL" 'Enviar al frmpermisos para especificar los usuarios del recurso
  			response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & idforo & "&nombretabla=foro"
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente el foro. Por favor intente denuevo"");history.back(-1)</script>"
  		case "MT" 'Modificar foro y actualizar lista y remarcarla
  			response.write "<script>top.window.opener.parent.location='index.asp?IdDocMarcado=" & idforo & "&numfila=" & filamodificada & "';top.window.close()</script>"
 		case "ET" 'Eliminar foro y actualizara lista
 			response.write "<script>parent.location.reload()</script>"
  		case "M" 'Modificar versión del foro
  			response.write "<script>window.opener.location.reload();window.close()</script>"
  		case "ER" 'Eliminar recursos integrados a la foro
  			response.redirect "listarecursos.asp?idtipopublicacion=0&idforo=" & idforo
  	End select
  End Property
  
  Public Function Consultar(byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function
  
  Public Function Agregar(byval idcursovirtual,byval tituloforo,byval descripcion,byval fechainicio,byval fechafin,byval permitircalificar,byval tipocalificacion,byval numcalificacion,byval idusuario)
  	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		Agregar=Obj.Agregar(idcursovirtual,tituloforo,descripcion,fechainicio,fechafin,permitircalificar,tipocalificacion,numcalificacion,idusuario)
  	Set Obj=Nothing
  End Function

  Public Function Modificar(byval idforo,byval tituloforo,byval descripcion,byval fechainicio,byval fechafin,byval permitircalificar,byval tipocalificacion,byval numcalificacion)
  	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		Call Obj.Modificar(idforo,tituloforo,descripcion,fechainicio,fechafin,permitircalificar,tipocalificacion,numcalificacion)
  	Set Obj=Nothing
  End Function

  Public Function Eliminar(byVal idforo)
	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		Eliminar=Obj.Eliminar(idforo)
  	Set Obj=Nothing
  End Function

  Public Function Agregarmensaje(byval idforo,titulomensaje,byval descripcionmensaje,byval idusuario,byval refidforomensaje)
   	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		call Obj.Agregarmensaje(idforo,titulomensaje,descripcionmensaje,idusuario,refidforomensaje)
  	Set Obj=Nothing
  End Function

  Public Function Modificarmensaje(byval idforomensaje,byval titulomensaje,byval descripcionmensaje)
  	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		Call Obj.Modificarmensaje(idforomensaje,titulomensaje,descripcionmensaje)
  	Set Obj=Nothing
  End Function

  Public Function Eliminarmensaje(byVal idforomensaje)
	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		Eliminarmensaje=Obj.Eliminarmensaje(idforomensaje)
  	Set Obj=Nothing
  End Function
  
  Public function calificarmensaje(Byval idforomensaje,byval usuario,byval calificacion)
  	Set Obj= Server.CreateObject("AulaVirtual.clsforo")
  		call Obj.Agregarforocalificacionmensajes(idforomensaje,idusuario,calificacion)
  	Set Obj=Nothing
  end function
  
  Public Sub CargarArbolRptas(byval idforo,byval tituloforo,byval idestadorecurso,byval idforomensaje,byval nivel)
	Dim ArrRptas
	Dim i
		ArrRptas=Consultar("3",idforo,idforomensaje,"")
	
	if IsEmpty(ArrRptas) and idforomensaje=0 then
		response.write "<p class=""sugerencia""> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado mensajes para el tema de discusión</p>"
		exit sub
	end if
	
	If IsEmpty(ArrRptas)=false then
		for i=lbound(ArrRptas,2) to Ubound(ArrRptas,2)
	%>
	<TR onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="AbrirMensaje('D','<%=ArrRptas(0,i)%>','<%=idforo%>','<%=tituloforo%>','<%=idestadorecurso%>')">
		<TD width="60%" height="5%" ALIGN="left"><IMG SRC="../../../images/espacio.gif" WIDTH="<%= nivel * 20 %>" HEIGHT="1"><IMG SRC="../../../images/mensaje.gif" WIDTH="24" HEIGHT="19" ALIGN="absmiddle" HSPACE="4"><%=ArrRptas(3,i)%></TD><TD width="15%" height="5%" ALIGN="left"><%=ArrRptas(5,i)%></TD><TD width="5%" height="5%" ALIGN="right" class="<%=iif(ArrRptas(7,i)=0,"rojo","azul")%>"><%=ArrRptas(7,i) & IconoRecursonuevo(ArrRptas(2,i)) %></TD><TD width="20%" height="5%" ALIGN="right"><%=ArrRptas(10,i)%></TD></TR><%
			If ArrRptas(7,i)>0 then
				call CargarArbolRptas(idforo,tituloforo,idestadorecurso,ArrRptas(0,i), nivel+1)
			end if
		next
	End If
  End Sub
  
  Public function IconoRecursonuevo(byval fecha)
  	if datepart("ww",fecha)=datepart("ww",now) then
  		IconoRecursonuevo="&nbsp;<img src=""../../../images/new.gif"">"
  	end if
  end function
    
  Public function MostrarCalificador(byval num,byval tipocalificacion,byval tipousuario)
  	dim x
  	if num>0 then
  		if tipocalificacion="0" and tipousuario<>"3" then%>
  		Puntaje:<select size="1" name="numcalificacion">
		<%for x=1 to num%>
			<option value="<%=x%>"><%=x%></option>
		<%next%>
		</select>
        <img border="0" src="../../../images/rpta.gif" alt="Enviar calificación" align="baseline"> Calificar
  		<%elseif tipocalificacion="1" and tipousuario="3" then%>
  		Puntaje:<select size="1" name="numcalificacion">
		<%for x=1 to num%>
			<option value="<%=x%>"><%=i%></option>
		<%next%>
		</select>
        <img border="0" src="../../../images/rpta.gif" alt="Enviar calificación" align="baseline"> Calificar
  		<%end if
  	end if
  end function
  
End Class
%>