<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Martes, 25 de Octubre de 2005 4:34:06 p.m.

Class clstarea
  Public idtarea
  Public filamodificada

  
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_tarea(strX)
  	idtarea=strX
  End Property
  
  Public Property LET codigo_fila(strX)
  	filamodificada=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "PL" 'Enviar al frmpermisos para especificar los usuarios del recurso
  			response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & idtarea & "&nombretabla=tarea"
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente el tarea. Por favor intente denuevo"");history.back(-1)</script>"
  		case "AT" 'Agregar tarea y actualizar lista
  			response.write "<script>top.window.opener.location.reload();top.window.close()</script>"
  		case "MT" 'Modificar tarea y actualizar lista y remarcarla
  			response.write "<script>top.window.opener.parent.location='index.asp?IdDocMarcado=" & idtarea & "&numfila=" & filamodificada & "';top.window.close()</script>"
 		case "ET" 'Eliminar tarea y actualizara lista
 			response.write "<script>parent.location.reload()</script>"
  		case "M" 'Modificar versión del tarea
  			response.write "<script>window.opener.location.reload();window.close()</script>"
  		case "ER" 'Eliminar recursos integrados a la tarea
  			response.redirect "listarecursos.asp?idtipopublicacion=0&idtarea=" & idtarea
  	End select
  End Property
  
  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function
  
	Public Function ObtenerTipoIcono(byval narchivo)
		dim icono
		icono=right(narchivo,3) & ".gif"
		ObtenerTipoIcono="<img border=0 src=""../../../images/ext/" & icono & """>"
	End Function
 
  Public Function BuscarRuta(ByVal IdDoc,ByVal Tipo,ByVal narchivo,byVal textoLink,ByVal idestado)
  	Dim ruta
		'if idestado=3 then
		'	BuscarRuta="<span class='rojo'>No puede descargar el recurso porque la fecha final de publicación ha finalizado. Consulta con el Administrador del recurso</span>"
		'else
			select case tipo
				case "documento"
'**
					'ruta="http://www.usat.edu.pe/campusvirtual/archivoscv/" & narchivo
					ruta=narchivo
					BuscarRuta="<a TARGET=""_blank"" href=""../documentos/descargar.asp?tblrecurso=tareausuario&Doc=" & IdDoc & "&Ruta=" & ruta & """>" & textolink & "</a>"
				case "evaluacion"
					BuscarRuta="<a TARGET=""_self"" href=""../evaluacion/detalleevaluacion.asp?idevaluacion=" & IdDoc & """>" & textolink & "</a>"
			end select
		'End if
  End Function
  
  Public Function Agregar(ByVal idcursovirtual, ByVal titulotarea, ByVal descripcion, ByVal fechainicio, ByVal fechafin, ByVal idusuario, ByVal idtipopublicacion, ByVal idestadorecurso, ByVal permitirreenvio, ByVal calificacion,byval idtipotarea)
  	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  		Agregar=Obj.Agregar(idcursovirtual,titulotarea,descripcion,fechainicio,fechafin,idusuario,idtipopublicacion,idestadorecurso,permitirreenvio,calificacion,idtipotarea)
  	Set Obj=Nothing
  End Function

  Public Function Modificar(ByVal idtarea, ByVal titulotarea, ByVal descripcion, ByVal fechainicio, ByVal fechafin,ByVal permitirreenvio, ByVal calificacion)
  	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  		Call Obj.Modificar(idtarea,titulotarea,descripcion,fechainicio,fechafin,permitirreenvio,calificacion)
  	Set Obj=Nothing
  End Function

  Public Function Eliminar(byVal idtarea)
	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  		Eliminar=Obj.Eliminar(idtarea)
  	Set Obj=Nothing
  End Function
  
  Public Function AgregarTareaRecurso(Byval idtarea,byval nombretabla,byVal arrRecursos)
  	dim idtabla
  	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  	For I=LBound(arrRecursos) to UBound(arrRecursos)
		idtabla=Trim(arrRecursos(I))
		call Obj.AgregarTareaRecurso(idtarea,nombretabla,idtabla)
	Next
  	Set Obj=Nothing
  End Function
  
  Public Function EliminarTareaRecurso(Byval idtarearecurso)
	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  		call Obj.EliminarTareaRecurso(idtarearecurso)
  	Set Obj=Nothing  
  End Function
  
  Public function Eliminararchivosdir(byVal ArrTemp)
  		if IsEmpty(ArrTemp)=false then
  			for i=lbound(ArrTemp,2) to Ubound(ArrTemp,2)
	  			call BorrarArchivoReg(ArrTemp(0,i))
	  		next
  		end if
  End function
  
  Public function Modificartareausuario(byval idtareausuario,byval obs,byval bloqueada)
	Set Obj= Server.CreateObject("AulaVirtual.clstarea")
  		call Obj.Modificartareausuario(idtareausuario,obs,bloqueada)
  	Set Obj=Nothing 
  end function
  
  Public function AbrirTarea(byval idfuncion,byval idusuario,byval idestadorecurso,byval idtarea,byval titulotarea,byval idtipotarea,byval descripciontipotarea,byval permitirreenvio)
  	dim pagina
  	'if (idestadorecurso=1 OR idfuncion=1) then
  		select case idtipotarea
  			case "3": pagina="frmrevisardocs.asp"
        	case "4":
				cadena="vez=1&idestadorecurso=" & idestadorecurso & "&idusuario=" & replace(idusuario,"\","***") & "&idtarea=" & idtarea & "&idcursovirtual=" & session("idcursovirtual") & "&idvisita=" & session("idvisita_sistema") & "&codigo_tfu=" & idfuncion & "&titulotarea=" & titulotarea 

'	cadena="../../libreriaNET/aulavirtual/admintareas.aspx?" & cadena
cadena="../aulavirtualProfesores/admintareas.aspx?" & cadena
        end select%>
		<a target="_parent" href="../cargando.asp?rutapagina=<%=cadena%>">
        <img border="0" src="../../../images/prop.gif"><br>Abrir tarea </a>
     <%'else
        '<img border="0" src="../../../images/bloquear.gif"><br>Tarea bloqueada por finalizar la fecha de publicación</a>
     'end if
  end function

  Public function AbrirTareaRealizada(byval permitirreenvio,byval idtarea,byval titulotarea,byval idusuario,byval archivo,byval texto)
  	dim rutaarchivo
  	if permitirreenvio="0" then
		rutaarchivo="../../../archivoscv/" & session("idcursovirtual") & "/" & archivo
		icono=ObtenerTipoIcono(archivo)
		texto=icono & "&nbsp;" & texto
		AbrirTareaRealizada="<a TARGET=""_blank"" href=""" & rutaarchivo & " "">" & texto & "</a>"
  	else
	  	AbrirTareaRealizada="<a href=""listaversiones.asp?idtarea=" & idtarea & "&titulotarea=" & titulotarea & "&idusuario=" & idusuario & """>" & texto & "</a>"
	end if
  end function
  
  Public Function cargardocumentos(byval idtarea,byval titulotarea,byval refIdversion,byval prefix,byval icodigousu,byval modo)
		Dim ArrCarpeta, NodoIzquiero, preadd, Resultado, NodoAbierto

		ArrCarpeta=Consultar("8",idtarea,refIdversion,icodigousu)
	
		If IsEmpty(ArrCarpeta) then%>
			<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado versiones del documento</p>
		<%Else
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)
				NodoAbierto = 0
				NodoIzquierdo=ArrCarpeta(6,C)
				NodoIzquiero = NodoIzquiero - 1
				Resultado=Resultado & "<tr id=""tbl" & ArrCarpeta(0,C) & """>"
				Resultado=Resultado & "<td>" & prefix

				if NodoIzquiero then
					Resultado = Resultado & "<img src='../../../images/beforenode.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../../images/beforelastnode.gif' align=absbottom>"
				end if
		
				if ArrCarpeta(6,C) > 0 then
					NodoAbierto = instr(NumNodoAbierto,"[" & ArrCarpeta(0,C) & "]")
					if NodoAbierto > 0 then
					Resultado = Resultado & "<a href='listaversiones.asp?modo=" & modo & "&idusuario=" & icodigousu & "&idtarea=" & idtarea & "&titulotarea=" & titulotarea & "&NumNodoAbierto=" & _
					replace(NumNodoAbierto, "[" & ArrCarpeta(0,C) & "]", "") & "'>" & _
					"<img src='../../../images/NodoAbierto.gif' align=absbottom></a>"
				else
					Resultado=Resultado & "<a href='listaversiones.asp?modo=" & modo & "&idusuario=" & icodigousu & "&idtarea=" & idtarea & "&titulotarea=" & titulotarea & "&NumNodoAbierto=" & _
					NumNodoAbierto & "[" & ArrCarpeta(0,C) & "]'>" & _
					"<img src='../../../images/NodoCerrado.gif' align=absbottom></a>"
				end if
				else
					Resultado=Resultado & "<img src='../../../images/nodeleaf.gif' align=absbottom>"
				end if
				Resultado = Resultado & "&nbsp;<img border=""0"" name=""arrImgCarpetas"" id=""imgCarpeta" & ArrCarpeta(0,C) & """ src=""../../../images/ext/" & right(ArrCarpeta(1,C),3) & ".gif"" align=absbottom>&nbsp;" & _
								"<span id=""spCarpeta" & ArrCarpeta(0,C) & """ " & scriptMostrarVersiones(idtarea,ArrCarpeta(0,C),ArrCarpeta(5,C),ArrCarpeta(3,C)) & ">" & _
								mostrarestadoversion(ArrCarpeta(2,C),ArrCarpeta(7,C)) & "</span></td></tr>" & chr(13) & chr(10)
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/beforechild.gif' align=absbottom>"
					else
						preadd = "<img src='../../../images/beforelastchild.gif' align=absbottom>"
					end if
				Resultado = Resultado & cargardocumentos(idtarea,titulotarea,ArrCarpeta(0,C), prefix & preadd,icodigousu,modo)
				end if
			NEXT
		end if
		cargardocumentos= Resultado
	end function
	
	Private Function ScriptMostrarVersiones(xiddoc,xidver,xescrit,xcreador)
		ScriptMostrarVersiones="OnClick=""MostrarVersionesDoc('" & xiddoc & "','" & xidver & "','" & xescrit & "','" & replace(xcreador,"\","/") & "')"""
	End Function

	Private function Mostrarestadoversion(titulover,estado)
		estado=iif(estado=0,"V0","V1")
		Mostrarestadoversion="<img src=""../../../images/" & estado & ".gif"">&nbsp;" & titulover
	end function
End Class
%>