<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Martes, 06 de Septiembre de 2005 4:34:06 p.m.

Class clsagenda
  Dim idgenda
  
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_agenda(strX)
  	idagenda=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "A"
  			response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & idagenda & "&nombretabla=agenda"
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente la Agenda. Por favor intente denuevo"");history.back(-1)</script>"
  		case "M"
  			response.write "<script>window.opener.location.reload();window.close()</script>"
  		case "E"
  			response.redirect "index.asp"
  	End select
  End Property
  
  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3, byVal param4, byVal param5)
  	Set Obj= Server.CreateObject("AulaVirtual.clsAgenda")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3,param4,param5)
  	Set Obj= Nothing
  End Function

  Public Function Agregar( byVal tituloagenda, byVal fechainicio, byVal fechafin, byVal lugar, byVal descripcion, byVal contactos, byVal idcategoria, byVal idcursovirtual, byVal idusuario, byVal prioridad,byval idtipopublicacion)
  	Set Obj= Server.CreateObject("AulaVirtual.clsAgenda")
  		Agregar=Obj.Agregar(tituloagenda,fechainicio,fechafin,lugar,descripcion,contactos,idcategoria,idcursovirtual,idusuario,prioridad,idtipopublicacion)
  	Set Obj=Nothing
  End Function

  Public Function Modificar( byVal IdAgenda, byVal tituloagenda, byVal fechainicio, byVal fechafin, byVal lugar, byVal descripcion, byVal contactos, byVal idcategoria, byVal prioridad)
  	Set Obj= Server.CreateObject("AulaVirtual.clsAgenda")
  		Call Obj.Modificar(IdAgenda,tituloagenda,fechainicio,fechafin,lugar,descripcion,contactos,idcategoria,prioridad)
  	Set Obj=Nothing
  End Function

  Public Function Eliminar(byVal IdAgenda)
	Set Obj= Server.CreateObject("AulaVirtual.clsAgenda")
  		Call Obj.Eliminar(IdAgenda)
  	Set Obj=Nothing
  End Function

  Public Function Pintar( byVal param1, byVal param2, byVal param3 )
	Set Obj= Server.CreateObject("AulaVirtual.clsAgenda")
  		Pintar=Obj.Pintar(param1,param2,param3)
  	Set Obj=Nothing
  End Function

  Private Sub Class_Initialize()
    
  End Sub
  
  Private Sub Class_Terminate()
  
  End Sub

End Class
%>