<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Martes, 06 de Septiembre de 2005 4:34:06 p.m.

Class clsdocumento
  Public iddocumento
  Public idversion
  Public filamodificada
  Public xidcarpeta
  
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_doc(strX)
  	iddocumento=strX
  End Property
    
  Public Property LET codigo_dir(strX)
  	xidcarpeta=strX
  End Property
  
  Public Property LET codigo_fila(strX)
  	filamodificada=strX
  End Property

  Public Property LET codigo_ver(strX)
  	idversion=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente el documento. Por favor intente denuevo"");history.back(-1)</script>"
  		case "AD" 'Agregar documento y actualizar lista
  			response.write "<script>window.opener.document.frames[0].location.reload();window.close()</script>"
 		case "ED" 'Eliminar documento y actualizara lista
 			response.write "<script>parent.location.reload()</script>"
  		case "MV" 'Modificar versión del documento
  			'='listaversiones.asp?iddocumento=" & iddocumento & "&IdverMarcado=" & idversion & "';
  			response.write "<script>window.parent.location.reload();window.close()</script>"
  		case "DIR"
  			response.write "<script>window.opener.location.reload();window.close()</script>"
  	End select
  End Property
  
  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3,Byval param4)
  	Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3,param4)
  	Set Obj= Nothing
  End Function
  
  Public sub CargarCarpeta(ByVal codigousuario,ByVal codigodoc,ByVal codigocursovirtual,ByVal codigodir)
	Dim ArrTemp,script
	
	script=" onClick=""ResaltarCarpeta(this)"""
	
	ArrTemp=Consultar("7",codigousuario,codigodoc,codigocursovirtual,"")
	
	if IsEmpty(ArrTemp)=false then
		for j=lbound(ArrTemp,2) to Ubound(ArrTemp,2)
			if clng(codigodir)<>clng(ArrTemp(0,j)) then
				if ArrTemp(7,j)>0 then
					response.write "<li><span id=doc" & ArrTemp(0,j) & script & ">&nbsp;" & ArrTemp(1,j) & "</span></li>" & vbNewLine
					response.write "<ul>" & vbNewLine
					call CargarCarpeta(codigousuario,ArrTemp(0,j),codigocursovirtual,codigodir)
					response.write "</ul>" & vbNewLine
				else
					response.write "<li><span id=doc" & ArrTemp(0,j) & script & ">&nbsp;" & ArrTemp(1,j) & "</span></li>" & vbNewLine
				end if
			end if
		next
	end if
   end Sub

	Public sub DocumentosDelUsuario(ByVal codigousuario,ByVal codigodoc,ByVal codigocursovirtual,byVal idtarea)
	 Dim ArrTemp
	
	 ArrTemp=Consultar("10",codigousuario,codigodoc,codigocursovirtual,idtarea)
	
	if IsEmpty(ArrTemp)=false then
		for j=lbound(ArrTemp,2) to Ubound(ArrTemp,2)
			if ArrTemp(7,j)>0 then
				response.write "<li><span id=carpeta" & ArrTemp(0,j) & ">&nbsp;" & ArrTemp(1,j) & "</span></li>" & vbNewLine
				response.write "<ul>" & vbNewLine
				call DocumentosDelUsuario(codigousuario,ArrTemp(0,j),codigocursovirtual,idtarea)
				response.write "</ul>" & vbNewLine
			else
				response.write "<li style=""list-style-image: url('../../../images/" & ObtenerTipoIcono(ArrTemp(3,j),Arrtemp(4,j)) & "')""><span id=""doc" & ArrTemp(0,j) & """>" & VerificarDocumentoAsignado(ArrTemp(3,j),ArrTemp(8,j),ArrTemp(0,j)) & " &nbsp;" & ArrTemp(1,j) & "</span></li>" & vbNewLine
			end if
		next
	end if
   End Sub

	Private Function VerificarDocumentoAsignado(byval tipodoc,Byval existe,byval iddoc)
		script=" onClick=""ElegirDocumento(this)"""
		if (clng(existe)=0 and tipodoc<>"C") then
			VerificarDocumentoAsignado="<input type=checkbox name=ArrArchivos value=""" & iddoc & """ id=""chk" & iddoc & """" & script & ">"
		else
			VerificarDocumentoAsignado="<img src='../../../images/bloquear.gif' ALT='Documento asignado a la Tarea'>"
		end if
	End Function
	
	Public Function ColorEscritura(byval escrit)
		if escrit="1" then
			ColorEscritura="class=""azul"""
		end if
	End Function
	
   	Public Function predeterminarCarpeta(byval iveces,icodigousu,icursovirtual,tipofuncion)
   	dim ArrTemp
   		if iveces=1 then
			Set Obj= Server.CreateObject("AulaVirtual.clsDocumento")
				ArrTemp=Obj.Consultar("1",icodigousu,0,icursovirtual,"")
			Set Obj=nothing
			If IsEmpty(ArrTemp)=false then
				predeterminarCarpeta="OnLoad=""MostrarArchivos('" & ArrTemp(0,0) & "','" & ArrTemp(2,0) & "','" & ArrTemp(1,0) & "','" & ArrTemp(8,0) & "','" & replace(ArrTemp(6,0),"\","/") & "','" & tipofuncion & "','" & replace(icodigousu,"\","/") & "')"""
			end if
		end if
   	End function
	
	Private Function ScriptMostrarArchivos(modo,xiddoc,xescrit,xtit,xtipopublic,xncreador,xcreador,xtipofuncion,xusuarioactual)
		xcreador=replace(xcreador,"\","/")
		xusuarioactual=replace(xusuarioactual,"\","/")
		if modo="b" then
			ScriptMostrarArchivos="OnClick=""MostrarArchivos('" & xiddoc & "','" & xescrit & "','" & xtit & "','" & xtipopublic & "','" & xcreador & "','" & xtipofuncion & "','" & xusuarioactual & "')"""
		else
			ScriptMostrarArchivos="OnContextmenu=""CrearMenuPopUp(this,'" & xescrit & "','" & xtit & "','" & xtipopublic & "','" & xncreador & "','" & xcreador & "','" & xtipofuncion & "','" & xusuarioactual & "')"""
		end if
	End Function

	Public Function crearArbolArchivos(ByVal RefIdDocumento,ByVal prefix,ByVal icodigousu,ByVal icursovirtual,ByVal tipofuncion,ByVal usuarioactual)
		Dim ArrCarpeta, NodoIzquiero, preadd, Resultado, NodoAbierto

		ArrCarpeta=Consultar("1",icodigousu,RefIdDocumento,icursovirtual,"")
	
		If IsEmpty(ArrCarpeta) then%>
			<tr><td class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han definido Carpetas y/o documentos. Haga clic en Carpeta Principal para definirlas</td></tr>
		<%Else
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)
				NodoAbierto = 0
				NodoIzquierdo=ArrCarpeta(7,C) 'Número de Nodos
				NodoIzquiero = NodoIzquiero - 1
				Resultado=Resultado & "<tr id=""tbl" & ArrCarpeta(0,C) & """" & ScriptMostrarArchivos("d",ArrCarpeta(0,C),ArrCarpeta(2,C),ArrCarpeta(1,C),ArrCarpeta(8,C),ArrCarpeta(5,C),ArrCarpeta(6,C),tipofuncion,usuarioactual) & ">"
				Resultado=Resultado & "<td>" & prefix

				if NodoIzquiero then
					Resultado = Resultado & "<img src='../../../images/beforenode.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../../images/beforelastnode.gif' align=absbottom>"
				end if
		
				if ArrCarpeta(7,C) > 0 then
					NodoAbierto = instr(NumNodoAbierto,"[" & ArrCarpeta(0,C) & "]")
					if NodoAbierto > 0 then
					Resultado = Resultado & "<a href='carpetas.asp?NumNodoAbierto=" & _
					replace(NumNodoAbierto, "[" & ArrCarpeta(0,C) & "]", "") & "'>" & _
					"<img src='../../../images/NodoAbierto.gif' align=absbottom></a>"
				else
					Resultado=Resultado & "<a href='carpetas.asp?NumNodoAbierto=" & _
					NumNodoAbierto & "[" & ArrCarpeta(0,C) & "]'>" & _
					"<img src='../../../images/NodoCerrado.gif' align=absbottom></a>"
				end if
				else
					Resultado=Resultado & "<img src='../../../images/nodeleaf.gif' align=absbottom>"
			end if
				Resultado = Resultado & "&nbsp;<img border=""0"" name=""arrImgCarpetas"" id=""imgCarpeta" & ArrCarpeta(0,C) & """ src=""../../../images/cerrado.gif"" align=absbottom ALT=""" & ArrCarpeta(1,C) &  """>&nbsp;" & _
								"<span id=""spCarpeta" & ArrCarpeta(0,C) & """ " & ScriptMostrarArchivos("b",ArrCarpeta(0,C),ArrCarpeta(2,C),ArrCarpeta(1,C),ArrCarpeta(8,C),ArrCarpeta(5,C),ArrCarpeta(6,C),tipofuncion,usuarioactual) & ">" & _
								ArrCarpeta(1,C) & "</span></td></tr>" & chr(13) & chr(10)
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/beforechild.gif' align=absbottom>"
					else
						preadd = "<img src='../../../images/beforelastchild.gif' align=absbottom>"
					end if
				Resultado = Resultado & crearArbolArchivos(ArrCarpeta(0,C), prefix & preadd,icodigousu,icursovirtual,tipofuncion,usuarioactual)
				end if
			NEXT
		end if

		crearArbolArchivos= Resultado
	end function

	Public Function CargarTodo(ByVal RefIdDocumento,ByVal prefix,ByVal icodigousu,ByVal icursovirtual,ByVal tipofuncion,ByVal usuarioactual,byval mostrardescargas)
		Dim ArrCarpeta, NodoIzquiero, preadd, Resultado, NodoAbierto

		ArrCarpeta=Consultar("11",icodigousu,RefIdDocumento,icursovirtual,"")
	
		If IsEmpty(ArrCarpeta) then%>
			<tr><td class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han definido Carpetas y/o documentos. Haga clic en Carpeta Principal para definirlas</td></tr>
		<%Else
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)
				NodoAbierto = 0
				NodoIzquierdo=ArrCarpeta(7,C) 'Número de Nodos
				NodoIzquiero = NodoIzquiero - 1
				Resultado=Resultado & "<tr id=""tbl" & ArrCarpeta(0,C) & """>"
				Resultado=Resultado & "<td>" & prefix

				if NodoIzquiero then
					Resultado = Resultado & "<img src='../../../images/beforenode.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../../images/beforelastnode.gif' align=absbottom>"
				end if
		
				if ArrCarpeta(7,C) > 0 then
					NodoAbierto = instr(NumNodoAbierto,"[" & ArrCarpeta(0,C) & "]")
					if NodoAbierto > 0 then
					Resultado = Resultado & "<a href='exploradordocumentos.asp?mostrardescargas=" & mostrardescargas & "&NumNodoAbierto=" & _
					replace(NumNodoAbierto, "[" & ArrCarpeta(0,C) & "]", "") & "'>" & _
					"<img src='../../../images/NodoAbierto.gif' align=absbottom></a>"
				else
					Resultado=Resultado & "<a href='exploradordocumentos.asp?mostrardescargas=" & mostrardescargas & "&NumNodoAbierto=" & _
					NumNodoAbierto & "[" & ArrCarpeta(0,C) & "]'>" & _
					"<img src='../../../images/NodoCerrado.gif' align=absbottom></a>"
				end if
				else
					Resultado=Resultado & "<img src='../../../images/nodeleaf.gif' align=absbottom>"
			end if
				Resultado = Resultado & "<input type=checkbox name=ArrArchivos value=""" & ArrCarpeta(0,C) & """ id=""chk" & ArrCarpeta(0,C) & """ onClick=""ElegirDocumento(this)"">&nbsp;<img border=""0"" src=""../../../images/" & ObtenerTipoIcono(ArrCarpeta(3,C),ArrCarpeta(4,C)) & """ align=absbottom ALT=""" & ArrCarpeta(1,C) &  """>&nbsp;" & _
								"<span id=""doc" & ArrCarpeta(0,C) & """>" & mostrartitulodoc(mostrardescargas,ArrCarpeta(1,C),ArrCarpeta(8,C)) &  "</span></td></tr>" & chr(13) & chr(10)
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/beforechild.gif' align=absbottom>"
					else
						preadd = "<img src='../../../images/beforelastchild.gif' align=absbottom>"
					end if
				Resultado = Resultado & CargarTodo(ArrCarpeta(0,C), prefix & preadd,icodigousu,icursovirtual,tipofuncion,usuarioactual,mostrardescargas)
				end if
			NEXT
		end if

		CargarTodo= Resultado
	end function
	
	Public function mostrartitulodoc(byval md,tdoc,ddoc)
		mostrartitulodoc=tdoc
		if md="s" and ddoc<>0 then
			mostrartitulodoc=mostrartitulodoc & "(" & ddoc & " descargas)"
		end if
	end function

	Public Function ObtenerTipoIcono(byval tipo,byval narchivo)
		if tipo="C" then
			ObtenerTipoIcono="cerrado.gif"
		else
			ObtenerTipoIcono="ext/" & right(narchivo,3) & ".gif"
		end if
	End Function

	Private function Mostrarestadoversion(titulover,estado)
		estado=iif(estado=0,"V0","V1")
		Mostrarestadoversion="<img src=""../../../images/" & estado & ".gif"">&nbsp;" & titulover
	end function

	Private Function ScriptMostrarVersiones(xiddoc,xidver,xescrit,xcreador)
		ScriptMostrarVersiones="OnClick=""MostrarVersionesDoc('" & xiddoc & "','" & xidver & "','" & xescrit & "','" & replace(xcreador,"\","/") & "')"""
	End Function
	
	Public Function crearArbolVersiones(iddoc,titulodoc,refIdversion,prefix,icodigousu,icursovirtual,tipofuncion,usuarioactual)
		Dim ArrCarpeta, NodoIzquiero, preadd, Resultado, NodoAbierto

		ArrCarpeta=Consultar("5",iddoc,refIdversion,icodigousu,"")
	
		If IsEmpty(ArrCarpeta) then%>
			<h5 class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han registrado versiones del documento</h5>
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
					Resultado = Resultado & "<a href='listaversiones.asp?iddocumento=" & iddoc & "&titulodocumento=" & titulodoc & "&NumNodoAbierto=" & _
					replace(NumNodoAbierto, "[" & ArrCarpeta(0,C) & "]", "") & "'>" & _
					"<img src='../../../images/NodoAbierto.gif' align=absbottom></a>"
				else
					Resultado=Resultado & "<a href='listaversiones.asp?iddocumento=" & iddoc & "&titulodocumento=" & titulodoc & "&NumNodoAbierto=" & _
					NumNodoAbierto & "[" & ArrCarpeta(0,C) & "]'>" & _
					"<img src='../../../images/NodoCerrado.gif' align=absbottom></a>"
				end if
				else
					Resultado=Resultado & "<img src='../../../images/nodeleaf.gif' align=absbottom>"
				end if
				Resultado = Resultado & "&nbsp;<img border=""0"" name=""arrImgCarpetas"" id=""imgCarpeta" & ArrCarpeta(0,C) & """ src=""../../../images/" & ObtenerTipoIcono("A",ArrCarpeta(2,C)) & """ align=absbottom>&nbsp;" & _
								"<span id=""spCarpeta" & ArrCarpeta(0,C) & """ " & scriptMostrarVersiones(iddoc,ArrCarpeta(0,C),ArrCarpeta(4,C),ArrCarpeta(5,C)) & ">" & _
								mostrarestadoversion(ArrCarpeta(1,C),ArrCarpeta(3,C)) & "</span></td></tr>" & chr(13) & chr(10)
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/beforechild.gif' align=absbottom>"
					else
						preadd = "<img src='../../../images/beforelastchild.gif' align=absbottom>"
					end if
				Resultado = Resultado & crearArbolVersiones(iddoc,titulodoc,ArrCarpeta(0,C), prefix & preadd,icodigousu,icursovirtual,tipofuncion,usuarioactual)
				end if
			NEXT
		end if
		crearArbolVersiones= Resultado
	end function

  Function AbrirEnlace(ByVal Comentario,ByVal Link,ByVal Texto,ByVal IdDoc)
		AbrirEnlace="<a title='" & comentario & "' TARGET=""_blank"" href=descargar.asp?Doc=" & IdDoc & "&Ruta=" & Link & ">" & Texto & "</a>"
  End Function

  Public Function CargarEnlaces(ByVal iddoc)
	Dim ArrEnlace,x,textolink
		ArrEnlace=Consultar("4",iddoc,"","","")
	If IsEmpty(ArrEnlace)=false then
		response.write "<ul>"
		For x=Lbound(ArrEnlace,2) to Ubound(ArrEnlace,2)
			textolink="<LI>" & AbrirEnlace(ArrEnlace(1,x),ArrEnlace(2,x) & ArrEnlace(3,x),ArrEnlace(2,x) & ArrEnlace(3,x),iddoc) & "</LI>" & textolink
		next
		response.write "</ul>"
		CargarEnlaces=textolink
	end if
  End Function
  
  Public Function BuscarRuta(ByVal tbl,ByVal IdDoc,ByVal Tipo,Byval version,ByVal narchivo,byVal textoLink,ByVal idestado)
  	Dim ruta
  	if version="1" then
  		BuscarRuta="<span class=""rojo"" onClick=""VersionDoc('" & IdDoc & "')""><img border=""0"" src=""../../../images/generar.gif""/>Visualizar versiones del documento</span>"
	else
		If Tipo<>"L" then
			'if idestado=3 then
			'	BuscarRuta="<span class='rojo'>No puede descargar el archivo porque la fecha final de publicación ha finalizado. Consulta con el Administrador del archivo</span>"
			'else
				'ruta="../../../archivoscv/" & narchivo por jmanay por motivos de seguridad
				'ruta ="http://www.usat.edu.pe/campusvirtual/archivoscv/" & narchivo
				ruta=narchivo
			BuscarRuta="<a TARGET=""_blank"" href=descargar.asp?tblrecurso=" & tbl & "&Doc=" & IdDoc & "&Ruta=" & ruta & ">" & textolink & "</a>"
			'end if
		End if
	end if
  End Function
  
  Public Sub RetornarTipofrm(Byval modo,Byval tipodoc,ByRef tfrm,ByRef titfrm)
	  	if (tipodoc="A" AND modo="agregardocumento") then
			tfrm="enctype=""multipart/form-data"" "
		end if
		if (tipodoc="A") then
			titfrm=" del Documento"
		else
			if tipodoc="L" then
				titfrm=" del Enlace a página web/ Bibliografía"
			else
				titfrm=" de la Carpeta"
			End if
		end if
  End Sub
      
  Public Function Agregar(byVal tipodoc, byVal nombrearchivo, byVal titulodocumento, byVal idusuario, byVal fechainicio, byVal fechafin, byVal descripcion, byVal estado, byVal refIdDocumento, byVal idcursovirtual,byval versiondoc, byVal escrit,byval idtipopublic)
  	Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
  		Agregar=Obj.Agregar(tipodoc,nombrearchivo,titulodocumento,idusuario,fechainicio,fechafin,descripcion,estado,refIdDocumento,idcursovirtual,versiondoc,escrit,idtipopublic)
  	Set Obj=Nothing
  End Function

   Public Function Agregarlink( byVal iddoc, byVal tituloLink, byVal tipoLink, byVal URL )
  	Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
  		Call Obj.Agregarlink(iddoc,titulolink,tipolink,URL)
  	Set Obj=Nothing
  End Function
  
  'Agregar versión se ha procesado en guardarversion.asp
  	  
  Private Sub Class_Initialize()
    
  End Sub
  
  Private Sub Class_Terminate()
  
  End Sub

End Class
%>