<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Miércoles, 19 de Octubre de 2005 10:39:06 p.m.

Class clslibrodigital
  Public idlibrodigital
  Public idcontenido
  Public titulolibro
  
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_cont(strX)
  	idcontenido=strX
  End Property

  Public Property LET codigo_libro(strX)
  	idlibrodigital=strX
  End Property
  
  Public Property LET titulo_libro(strX)
  	titulolibro=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "PL" 'Enviar al frmpermisos para especificar los usuarios del recurso
  			response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & idlibrodigital & "&nombretabla=librodigital"
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente el contenido. Por favor intente denuevo"");history.back(-1)</script>"
  		case "M" 'Modificar versión del contenido
  			response.write "<script>window.opener.location.reload();window.close()</script>"
 		case "E" 'Eliminar libro digital
	 		response.write "<script>location.href='index.asp'</script>"
 		case "EI" 'Eliminar índice
 			'response.write "<script>window.location.reload()</script>"
 			response.write "<script>location.href='listaindice.asp?modo=administrar&idlibrodigital=" & idlibrodigital & "&titulolibro=" & titulolibro & "'</script>"			
 		case "G"
 			response.write "<script>window.parent.location.reload()</script>"
  	End select
  End Property
  
  Public Function GenerarLetras(idlibro,modo)
    Dim I,Letra

    For I = Asc("A") To Asc("Z")
    	Letra=Letra & " " & enlaceGlosario(idlibro,Chr(I),modo)
        If Chr(I) = "N" Then
            Letra=Letra & " " & enlaceGlosario(idlibro,"Ñ",modo)
        End If
    Next
	GenerarLetras=Letra
  End Function
  
  Private function enlaceGlosario(Byval idlibro,byval texto,modo)
  	Dim ArrTemp,boton
  	boton="<input type=""button"" style=""width:25"" value=""" & texto & """"
  	ArrTemp=Consultar("6",idlibro,texto,"")
  	if IsEmpty(ArrTemp)=false then
  		enlaceGlosario=boton & " class=""botonresaltado"" onClick=""AbrirGlosario('L','" & idlibro & "','" & texto & "','" & modo & "')"">"
  	else
  		enlaceGlosario=boton & " class=""botonbloqueado"" disabled=true>"
  	end if
  End function
  
  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function
  
  Public sub Cargarcontenido(ByVal idlibrodigital,ByVal refidcontenido)
	Dim Arrcontenido,script
	
	script=" onClick=""Resaltarcontenido(this)"""
	
	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
		Arrcontenido=Obj.Consultar("3",idlibrodigital,refidcontenido,"")
	Set Obj=nothing
	'blockquote
	if IsEmpty(Arrcontenido)=false then
		for j=lbound(Arrcontenido,2) to Ubound(Arrcontenido,2)
			if Arrcontenido(12,j)>0 then
				response.write "<input type=""checkbox"" value=""" & Arrcontenido(0,j) & """  name=""chkidcontenido"" id=chk" & Arrcontenido(0,j) & script & "><span id=doc" & Arrcontenido(0,j) & " " & ResaltarIndice(Arrcontenido(12,j),Arrcontenido(5,j),Arrcontenido(3,j),Arrcontenido(4,j)) & "</span><br>" & vbNewLine
				response.write "<blockquote class='linea'>" & vbNewLine
				call Cargarcontenido(idlibrodigital,Arrcontenido(0,j))
				response.write "</blockquote>" & vbNewLine
			else
				response.write "<input type=""checkbox"" value=""" & Arrcontenido(0,j) & """ name=""chkidcontenido"" id=chk" & Arrcontenido(0,j) & script & "><span id=doc" & Arrcontenido(0,j) & " " & ResaltarIndice(Arrcontenido(12,j),Arrcontenido(5,j),Arrcontenido(3,j),Arrcontenido(4,j)) & "</span><br>" & vbNewLine
			end if
		next
	end if
   end Sub

	Private Sub ImprimeTexto(byval arrTemp)
		if IsEmpty(arrTemp)=false then
			'response.write "<tr><td class=""tcontenido"">" & arrTemp(3,0) & ". " & arrTemp(4,0) & "</td></tr>"
			'response.write "<tr><td>" & arrTemp(5,0) & "</td></tr>"
			response.write "<h3>" & arrTemp(3,0) & ". " & arrTemp(4,0) & "</h3>" & ArrTemp(5,0) & "<HR>"
		end if
	End sub
	
	Public Sub ConstruirDocumento(byVal arrColeccion)
		Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
			'response.write "<table border=0>"
			For I=LBound(Coleccion) to UBound(Coleccion)
				idcontenido=Trim(Coleccion(I))
				ArrDatos=Obj.Consultar("5",idcontenido,"","")
				call ImprimeTexto(ArrDatos)
			Next
			'response.write "</table>"
		Set Obj=nothing
	End Sub
	
	Private Function ResaltarIndice(byval numNodos,ByVal HayContenido,byval orden,Byval titulo)
		if numNodos>0 then
			'Si hay nodos
			if IsNull(HayContenido)=true then 'No hay contenido=>es indice
				ResaltarIndice=" class=""etiqueta"">" & orden & ".&nbsp;" & titulo
			else
				'Es hoja de contenidos
				ResaltarIndice="><i>" & orden & ".&nbsp;" & titulo & "</i>"
			end if
		else
			'Si no hay nodos
			if IsNull(HayContenido)=true then 'No hay contenido=>es indice
				ResaltarIndice=" class=""etiqueta"">" & orden & ".&nbsp;" & titulo
			else
				'Es hoja de contenidos
				ResaltarIndice="><i>" & orden & ".&nbsp;" & titulo & "</i>"
			end if			
		end if
	End Function
	
	Private function ImagenNodo(Byval numNodos,Byval HayContenido)
		if numNodos>0 then
			'Si hay nodos
			if IsNull(HayContenido)=true then 'No hay contenido=>es indice
				ImagenNodo=" tipocont=""I"" src=""../../../images/librocerrado.gif"""
			else
				'Es hoja de contenidos
				ImagenNodo=" tipocont=""C"" src=""../../../images/librohoja.gif"""
			end if
		else
			'Si no hay nodos
			if IsNull(HayContenido)=true then 'No hay contenido=>es indice
				ImagenNodo=" tipocont=""I"" src=""../../../images/librocerrado.gif"""
			else
				'Es hoja de contenidos
				ImagenNodo=" tipocont=""C"" src=""../../../images/librohoja.gif"""
			end if
		end if
	End Function
	
	Private Function ScriptMostrarContenido(xtipo,xbloquear,xidcont,xtit,modo,xidlibro)
		if (modo="administrar") then
			ScriptMostrarContenido="OnClick=""MostrarContenido('" & xtipo & "','" & xbloquear & "','" & xidcont & "','" & xtit & "')"""
		else
			ScriptMostrarContenido="OnClick=""txtidcontenido.value='" & xidcont & "';AbrirContenido('V','" & xidlibro & "')"""
		end if
	End Function

	Private Function  ScriptMenuDerecho(tbl,modo)
		if modo="administrar" then
			ScriptMenuDerecho="OnContextmenu=""CrearMenuPopUp(this,'" & modo & "')"""
		end if
	End Function
	
	Public Function CargarIndice(RefIdcontenido,prefix,idlibrodigital,modo,titulolibro)
		Dim Arrcontenido, NodoIzquiero, preadd, Resultado, NodoAbierto

		Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
			Arrcontenido=Obj.Consultar("3",idlibrodigital,RefIdcontenido,"")
		Set Obj=nothing
	
		If IsEmpty(Arrcontenido) then
			Resultado="<tr><td class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han definido los contenidos temáticos de la Actividad Académica. Haga clic en ""Tabla de contenidos"", para definirlo.</td></tr>"
		Else
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(Arrcontenido,2) to Ubound(Arrcontenido,2)
				NodoAbierto = 0
				NodoIzquierdo=Arrcontenido(12,C) 'Número de Nodos
				NodoIzquiero = NodoIzquiero - 1
				Resultado=Resultado & "<tr id=""tbl" & Arrcontenido(0,C) & """" & ScriptMenuDerecho("this",modo) & ">"
				Resultado=Resultado & "<td>" & prefix

				if NodoIzquiero then
					'agregar la line del mas.gif
					Resultado = Resultado & "<img src='../../../images/blanco.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../../images/blanco.gif' align=absbottom>"
				end if
		
				if Arrcontenido(12,C) > 0 then
					NodoAbierto = instr(NumNodoAbierto,"[" & Arrcontenido(0,C) & "]")
					if NodoAbierto > 0 then
					Resultado = Resultado & "<a href='listaindice.asp?idlibrodigital=" & idlibrodigital & "&modo=" & modo & "&titulolibro=" & titulolibro & "&NumNodoAbierto=" & _
					replace(NumNodoAbierto, "[" & Arrcontenido(0,C) & "]", "") & "'>" & _
					"<img src='../../../images/menos.gif' align=top></a>"
				else
					Resultado=Resultado & "<a href='listaindice.asp?idlibrodigital=" & idlibrodigital & "&modo=" & modo & "&titulolibro=" & titulolibro & "&NumNodoAbierto=" & _
					NumNodoAbierto & "[" & Arrcontenido(0,C) & "]'>" & _
					"<img src='../../../images/mas.gif' align=top></a>"
				end if
				else
					Resultado=Resultado & "<img src='../../../images/blanco.gif' align=absbottom>"
			end if
				Resultado = Resultado & "&nbsp;<img border=""0"" name=""arrImgcontenidos"" id=""imgcontenido" & Arrcontenido(0,C) & """" & ImagenNodo(ArrContenido(12,C),ArrContenido(5,C)) & " align=""absbottom"">&nbsp;" & _
								"<span id=""spcontenido" & Arrcontenido(0,C) & """ " & ScriptMostrarContenido(iif(ArrContenido(12,C)>0,"I","C"),iif(IsNull(ArrContenido(5,C))=true,"Agregar","Bloquear"),Arrcontenido(0,C),Arrcontenido(4,C),modo,idlibrodigital) & _
								ResaltarIndice(ArrContenido(12,C),ArrContenido(5,C),Arrcontenido(3,C),Arrcontenido(4,C)) & "</span></td></tr>" & chr(13) & chr(10)
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/blanco.gif' width=30 height=5 align=absbottom>"
					else
						preadd = "<img src='../../../images/blanco.gif' width=50 height=5 align=absbottom>"
					end if
				Resultado = Resultado & CargarIndice(Arrcontenido(0,C), prefix & preadd,idlibrodigital,modo,tituloindice)
				end if
			NEXT
		end if

		CargarIndice= Resultado
	end function
  
  Public Function Agregar(ByVal idcursovirtual, ByVal titulolibrodigital, ByVal descripcion, ByVal fechainicio, ByVal fechafin, ByVal idusuario, ByVal idtipopublicacion, ByVal idestadorecurso)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		Agregar=Obj.Agregar(idcursovirtual,titulolibrodigital,descripcion,fechainicio,fechafin,idusuario,idtipopublicacion,idestadorecurso)
  	Set Obj=Nothing
  End Function
  
  Public Function Modificar(ByVal idlibrodigital, ByVal titulolibrodigital, ByVal descripcion, ByVal fechainicio, ByVal fechafin)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		call Obj.Modificar(idlibrodigital,titulolibrodigital,descripcion,fechainicio,fechafin)
  	Set Obj=Nothing
  End Function
  
  Public Function Eliminar(ByVal idlibrodigital)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		Eliminar=Obj.Eliminar(idlibrodigital)
  	Set Obj=Nothing
  End Function
  
  Public Function AgregarContenido(ByVal tipocontenido, ByVal ordencontenido, ByVal titulocontenido, ByVal descripcioncontenido, ByVal idlibrodigital, ByVal fechainicio, ByVal fechafin, ByVal refidcontenido,ByVal idestadorecurso)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		call Obj.AgregarContenido(tipocontenido,ordencontenido,titulocontenido,descripcioncontenido,idlibrodigital,fechainicio,fechafin,refidcontenido,idestadorecurso)
  	Set Obj=Nothing
  End Function

  Public Function ModificarContenido(ByVal idcontenido, ByVal ordencontenido, ByVal titulocontenido, ByVal descripcioncontenido, ByVal fechainicio, ByVal fechafin, ByVal refidcontenido, ByVal idestadorecurso)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		Call Obj.ModificarContenido(idcontenido,ordencontenido,titulocontenido,fechainicio,fechafin,refidcontenido,idestadorecurso)
  	Set Obj=Nothing
  End Function
  
   Public Function ModificarwebContenido(ByVal idcontenido, ByVal descripcioncontenido)
   	Dim mensaje
   	on error resume next
  		Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  			mensaje=Obj.ModificarwebContenido(idcontenido,descripcioncontenido)
	  	Set Obj=Nothing
  		if (err.number>0 OR mensaje<>"") then
			response.write "<script>alert('" & mensaje & "');window.close()</script>"
		else
			Cerrar="M"
		end if
  	End Function

  Public Function EliminarContenido(byVal idcontenido)
	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		EliminarContenido=Obj.EliminarContenido(idcontenido)
  	Set Obj=Nothing
  End Function
  
  Public function Eliminararchivosdir(byVal ArrTemp)
  		if IsEmpty(ArrTemp)=false then
  			for i=lbound(ArrTemp,2) to Ubound(ArrTemp,2)
	  			call BorrarArchivoReg(ArrTemp(0,i))
	  		next
  		end if
  End function

  Public Function Agregarglosario(ByVal idlibrodigital, ByVal tituloglosario,ByVal descripcion,idusuario)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		call Obj.Agregarglosario(idlibrodigital,tituloglosario,descripcion,idusuario)
  	Set Obj=Nothing
  End Function

  Public Function Modificarglosario(ByVal idglosario, ByVal tituloglosario,ByVal descripcion)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		call Obj.Modificarglosario(idglosario,tituloglosario,descripcion)
  	Set Obj=Nothing
  End Function
  
  Public Function Eliminarglosario(ByVal idglosario)
  	Set Obj= Server.CreateObject("AulaVirtual.clslibrodigital")
  		call Obj.Eliminarglosario(idglosario)
  	Set Obj=Nothing
  End Function
  
End Class
%>