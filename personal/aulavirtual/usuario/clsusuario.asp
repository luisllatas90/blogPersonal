<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: martes, 06 de septiembre de 2005 08:36:49

Class clsusuario

  Public idusuario
  
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_usuario(strX)
  	idusuario=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "A"
  			response.redirect "index.asp"
  		case "R"
			response.write "<script>history.back(-1)</script>"
  		case "M"
  			response.write "<script>window.opener.location.reload();window.close()</script>"
  		case "E"
  			response.redirect "index.asp"
  		case "C"
  			response.write "<script>top.window.close()</script>"
  		case "T" 'Actualizar toda la página opener
  			response.write "<script>top.window.opener.location.reload();top.window.close()</script>" 			  		
  	End select
  End Property

  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3 )
	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function


  Public function agregarpermisocursovirtual(byval ntabla,byval itabla,byval arrpermisos,byval idcursovirtual,byval tipofuncion)
  	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		For I=LBound(arrpermisos) to UBound(arrpermisos)
			idusuarioElegido=Trim(arrpermisos(I))
			Obj.AgregaPermiso ntabla,itabla,idusuarioElegido,idcursovirtual,tipofuncion
		Next
	Set Obj=nothing
  end function
  
  Public Function agregarpermiso(byval imodo,byval ntabla,Byval itabla,byval arrpermisos,byval idcursovirtual,byval tipodoc)
  	Dim idusuarioElegido
  	'Recorrer la lista de usuarios para agregar los permisos
  	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		For I=LBound(arrpermisos) to UBound(arrpermisos)
			idusuarioElegido=Trim(arrpermisos(I))
			Obj.AgregaPermiso ntabla,itabla,idusuarioElegido,idcursovirtual,3
		Next
	Set Obj=nothing
	ActualizarTipoPublicacion ntabla,itabla,"2"
	'Modos de cerrar y actualizar ventanas
	Select case imodo
		case "1"
			cerrar="T"	
			'response.redirect "listapermisos.asp?modo=" & imodo & "&idtabla=" & itabla & "&nombretabla=" & ntabla
		case else
			cerrar="M"
	End Select
  End function
  
  Public Function EliminarPermiso(arrCheck,itabla,ntabla,icursovirtual,ititulo,idescripcion,imodo)
  	Dim Idpermiso
  	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")		
	For I=LBound(arrCheck) to UBound(arrCheck)
		Idpermiso=Trim(arrCheck(I))
		Obj.EliminaPermiso idpermiso,ntabla,icursovirtual
	Next
	Set Obj=nothing
	response.redirect "listapermisos.asp?modo=" & imodo & "&titulo=" & ititulo & "&idtabla=" & itabla & "&nombretabla=" & ntabla & "&descripcion=" & idescripcion
  End function
  
  Public Function ActualizarTipoPublicacion(nombretabla,idtabla,idtipopNuevo)
  	Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
		Obj.ActualizarTipoPublicacion nombretabla,idtabla,idtipopNuevo 
	Set Obj=nothing
  End function
  
  Private Sub Class_Initialize()

  End Sub
  
  Private Sub Class_Terminate()

  End Sub

End Class
%>