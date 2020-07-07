<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Domingo, 29 de Octubre de 2005 09:53:06 p.m.

Class clscursovirtual
  Dim idcursovirtual
  Dim idcodigo_apl
  Dim idcodigo_tfu
  Dim raiz

  Private Sub Class_Initialize()
	  Raiz=Server.MapPath("../../../archivoscv/")
  End Sub
  
  Private Sub Class_Terminate()
  
  End Sub
   
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET codigo_curso(strX)
  	idcursovirtual=strX
  End Property

  Public Property LET codigo_apl(strX)
  	idcodigo_apl=strX
  End Property
  
  Public Property LET codigo_tfu(strX)
  	idcodigo_tfu=strX
  End Property
    
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente el curso virtual. Por favor intente denuevo"");history.back(-1)</script>"
  		case "M"
  			response.write "<script>top.window.opener.location.reload();top.window.close()</script>"
  		case "E"
  			response.write "<script>location.href='listamatriculadoscurso.asp?idcursovirtual=" & idcursovirtual & "&codigo_apl=" & idcodigo_apl & "&codigo_tfu=" & idcodigo_tfu & "'</script>"
  		case "P"
  			response.redirect "../usuario/frmbuscarusuario.asp?modo=P&nombretabla=cursovirtual&idtabla=" & idcursovirtual
  		case "W"
  			response.redirect "frmweb.asp?modo=P&idcursovirtual=" & idcursovirtual
  		case "X"
  			response.redirect "enviaracceso.asp?idcursovirtual=" & idcursovirtual
  		case "C"
			response.write "<script>alert(""El curso virtual a registrar ya existe en la base de datos"");top.window.close()</script>"
	End select
  End Property
  
  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function
  
  Public Function ConsultarDesempeno( byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
  		ConsultarDesempeno=Obj.ConsultarDesempeno(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function
  
  Public Function ConsultarCategoria(byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCategoria")
  		ConsultarCategoria=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function

  Public Function Agregar(byval fechainicio,byval fechafin,byval titulocursovirtual,byval descripcion,byval modalidad,byval idusuario,byval codigo_apl,byval idestadorecurso,byval codigo_cup,byval creartemas,byval temapublico,byval integrartematarea,byval integrarrptatarea)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
  		Agregar=Obj.Agregar(fechainicio,fechafin,titulocursovirtual,descripcion,modalidad,idusuario,codigo_apl,idestadorecurso,codigo_cup,creartemas,temapublico,integrartematarea,integrarrptatarea)
  	Set Obj=Nothing
  End Function
  
  Public function CrearCarpetaCurso(ByVal idcurso)
	dim carpeta,fso,fol
	
	Set fso = CreateObject("Scripting.FileSystemObject")
  	carpeta=raiz & "/" & idcurso
  	'Verificar si existe la carpeta con el curso virtual
	If (Not fso.FolderExists(carpeta)) then
		Set fol = fso.CreateFolder(carpeta)
		Set fol = fso.CreateFolder(carpeta & "\documentos")
		Set fol = fso.CreateFolder(carpeta & "\tareas")
		Set fol = fso.CreateFolder(carpeta & "\images")
	End if
	Set fso=nothing
  	Set fol=nothing
  End function
  
  Public function EliminarCarpetaCurso(ByVal idcurso)
	dim carpeta,fso,fol
	
	Set fso = CreateObject("Scripting.FileSystemObject")
  	carpeta=raiz & "/" & idcurso
    
    If fso.FileExists(carpeta) then
    	fso.DeleteFolder(carpeta)
    end if
    Set fso = Nothing
  End function


  Public Function Modificar(byval idcursovirtual,byval fechainicio,byval fechafin,byval titulocursovirtual,byval descripcion,byval modalidad,byval creartemas,byval temapublico,byval integrartematarea,byval integrarrptatarea)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
  		Call Obj.Modificar(idcursovirtual,fechainicio,fechafin,titulocursovirtual,descripcion,modalidad,creartemas,temapublico,integrartematarea,integrarrptatarea)
  	Set Obj=Nothing
  End Function
  
  Public Function ModificarWeb(byval idcursovirtual,byval web)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
  		Call Obj.ModificarWeb(idcursovirtual,web)
  	Set Obj=Nothing
  End Function

  Public Function Eliminar(byVal Idcursovirtual)
	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
  		Eliminar=Obj.Eliminar(Idcursovirtual)
  	Set Obj=Nothing
  End Function

  Public Function Eliminaraccesocursovirtual(byval arrPermisos)
  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")		
		For I=LBound(arrPermisos) to UBound(arrPermisos)
			idpermiso=Trim(arrPermisos(I))
			call Obj.eliminaraccesocursovirtual(idpermiso)
		Next
		Set Obj=nothing
  End Function

  Public Function Administar(ByVal tf,ByVal capl)
  	dim pag
  	select case tf
  		case 1	'Administrador
			if trim(capl)=1 then pag="frmcurso.asp?accion=agregarcurso&codigo_apl=" & capl & "&codigo_tfu=" & tf
			if trim(capl)=4 then pag="frmcargaacademica.asp"

		case 2 'Organizador
			if trim(capl)=1 then pag="frmcurso.asp?accion=agregarcurso&codigo_apl=" & capl & "&codigo_tfu=" & tf
	end select
	if pag<>"" then
		Administar="|<span style=""cursor:hand"" onclick=""AbrirPopUp('" & pag & "','450','700','yes')"">&nbsp;<img border=""0"" src=""../../../images/disenar.gif""> Diseñar nuevo curso</span>"
	end if
  End Function
  
  Public Sub AlertaRecurso(ByVal publicado,Byval visitado)
  	response.write "<td width=""3%"">" & publicado & "</td>"
  	if clng(visitado)>=clng(publicado) then
  		if (visitado=0 and publicado=0) then
	  		response.write "<td width=""3%"">" & visitado & "</td>"
		else
  			response.write "<td width=""3%"" class=fondoverde>" & visitado & "</td>"
  		end if
	else
  		
	end if	
  end Sub
  
  Public Sub AlertaRecursoPtje(Byval publicado, Byval visitado)
	Dim ValMen, ValInterm, ValSup,PjeObt
	
	response.write "<td width=""3%"">" & publicado & "</td>"
	
	if (publicado=0 and visitado=0) then
	  	response.write "<td width=""3%"">" & visitado & "</td>"
	else
		ValMen = (100 / 2) - 1
		ValInterm = (100) - 1
    	ValSup = 100
    	'if publicado="" then publicado=0
		'if visita="" then visitado=0
		if (visita<>0 or publicado<>0) then
		    PjeObt=(visitado/publicado)*100
		end if
	    PjeObt=clng(PjeObt)
    
		If PjeObt < ValMen Then
    	    response.write "<td width=""3%"">" & visitado & "</td>"
		ElseIf PjeObt <= ValInterm Then
			response.write "<td width=""3%"" class=fondoamarillo>" & visitado & "</td>"
        ElseIf PjeObt >= ValSup Then
        	response.write "<td width=""3%"" class=fondoverde>" & visitado & "</td>"
		End If
	end if
  End Sub
  
  Public function Eliminararchivosdir(byVal ArrTemp)
  		if IsEmpty(ArrTemp)=false then
  			for i=lbound(ArrTemp,2) to Ubound(ArrTemp,2)
	  			call BorrarArchivoReg(ArrTemp(0,i))
	  		next
  		end if
  End function

  Public function Matricular(Byval arrCodUsu,Byval ArrNomUsu,Byval ArrEmail)
  	'Recorrer los alumnos matriculados
  	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		For I=LBound(ArrCodUsu) to UBound(ArrCodUsu)
			CU=Trim(ArrCodUsu(I))
			NU=Trim(ArrNomUsu(I))
			EU=Trim(ArrEmail(I))
			Call Obj.MatricularCursoVirtual(idcursovirtual,CU,NU,EU)
		Next
	Set Obj=nothing
	response.redirect "frmweb.asp?modo=X&idcursovirtual=" & idcursovirtual
  end function

  Public function PermisoModerador(Byval idtabla,byval idusuario,byval tfuncion)
	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		Call Obj.AgregaPermiso("cursovirtual",itabla,idusuario,idtabla,tfuncion)
	Set Obj=nothing
  end function

  Public function ConsultarRecursosvisitados(byval titulo,byval arr,byval pagina,byval encabezado)
  if IsEmpty(Arr)=false then%>
	<p class="e1"><%=titulo%></p>
	<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	  <thead>	
	  <tr class="etabla">
    	<td width="5%">&nbsp;</td>
	    <td width="15%">Fecha de visita</td>
    	<td width="70%">Descripción del recurso</td>
	    <td width="10%"><%=encabezado%>&nbsp;</td>
	  </tr>
	  </thead>
  	<%for i=lbound(Arr,2) to ubound(Arr,2)%>
	  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" <%if pagina<>"" then%>onClick="AbrirPopUp('<%=pagina & Arr(3,i)%>&codigo_acceso=<%=Arr(4,i)%>','500','650','yes','yes','yes')"<%end if%>>
    	<td width="5%"><%=i+1%>&nbsp;</td>
	    <td width="15%"><%=Arr(0,i)%>&nbsp;</td>
    	<td width="70%"><%=Arr(1,i)%>&nbsp;</td>
	    <td width="10%" align="center"><%=Arr(2,i)%>&nbsp;</td>
	  </tr>
	<%next%>
	</table>
  <%else%>
  		<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron <%=titulo%></p>
  <%end if
  End function
    
End Class
%>