<% 
' --------------------------------------------------
RutaActual = "C:\"
' --------------------------------------------------
Function StripTrail(PathName)
	While Right(PathName, 1) = "/"
		PathName = Left(PathName, Len(PathName) - 1)
	Wend
	StripTrail = PathName
End Function

Function ParsePath(PathName)
	ParseStr = Replace(PathName & "/","\","/")

	While Right(ParseStr, 1) = "/"
		ParseStr = Left(ParseStr, Len(ParseStr) - 1)
	Wend
	
	ParseStr = ParseStr & "/"
	ParseSta = 1

	Response.Write "<tr>" & vbcrlf
	Response.Write "	<td  width=""5%"" >" & vbcrlf
	Response.Write "	<a href=""frmsubirfoto.asp""><img border=""0"" src=""../../../../images/mover.gif"" width=""16"" >Ir a carpeta principal</a></td>" & vbcrlf
	Response.Write "</tr>" & vbcrlf
	  
	SplitMax = UBound(Split(ParseStr,"/")) - 1

	For ParseLop = 0 To SplitMax
		ParseCnt = Split(ParseStr,"/")(ParseLop)
		  If ParseLop = SplitMax Then
				ParsePrev = ParsePrev & ParseCnt & "/"
				Response.Write "<tr>" & vbcrlf
				Response.Write "	<td  width=""60%"" ><img border=""0"" src=""../../../../images/Abierto.gif"">&nbsp;<b>" & ParsePrev & "</b></td>" & vbcrlf
				Response.Write "</tr>"		
		  Else
    	  If ParseCnt <> "" Then
				ParsePrev = ParsePrev & ParseCnt & "/"
				Response.Write "<tr>" & vbcrlf
				Response.Write "	<td  width=""60%"" ><img border=""0"" src=""../../../../images/cerrado.gif"">&nbsp;" & ParsePrev & "</td>" & vbcrlf
				Response.Write "</tr><tr>"	  
		 End If
		 End If
	Next	
End Function
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Copiar fotos de estudiantes</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
var total=0
	function HabilitarFoto(obj)
	{
		frmFotos.cmdGuardar.disabled=true
		frmFotos.chkSeleccionar.checked=false
		
		var foto=document.getElementById(obj.value)
		if (obj.checked==true){
			total=total+1
			foto.disabled=false
		}
		else{
			foto.disabled=true
			total=total-1
		}
		
		if (total>0){
			frmFotos.cmdGuardar.disabled=false
		}
		Marcas.innerHTML="| <b>Fotos elegidas: " + total + "</b>"
	}

	function GuardarFotos()
	{
		frmFotos.action="guardarfotos1.asp?num=" + total
		frmFotos.submit()
	}
	
	function MarcarTodo(opt,chk)
	{
		frmFotos.cmdGuardar.disabled=true
		total=0
		VerificaSeleccion(opt,chk)

		if (chk.length==undefined && opt.checked==true){
			total=1
		}
		else{	
			if (opt.checked==true){
				total=chk.length
			}
			else{
				total=0
			}
		}
		
		if (total>0){
			frmFotos.cmdGuardar.disabled=false
		}
		
		//Habilitar objetos File			
		for(var i = 0; i < frmFotos.elements.length; i ++) {
			if(frmFotos.elements[i].type=='file'){
				frmFotos.elements[i].disabled=true
				if (opt.checked==true){
					frmFotos.elements[i].disabled=false
				}
			}
		}
		
		Marcas.innerHTML="| <b>Fotos elegidas: " + total + "</b>"
	}
</script>
</head>
<body bgcolor="#f0f0f0">
<FORM name="frmFotos" method="post" encType="multipart/form-data">
<p class="usatTitulo">Enviar fotos de estudiantes</p>
<p>
<input name="cmdGuardar" type="button" value="    Guardar fotos en BD" class="guardar_prp" onclick="GuardarFotos()" disabled="true">
<span id="Marcador" style="display:none">
<input name="chkSeleccionar" type="checkbox" value="0" onclick="MarcarTodo(this,chkfoto)">Seleccionar todos
</span>
</p>
<table cellpadding="3" height="83%" width="100%" bgcolor="white" class="contornotabla_azul">
<%  Dim strPath 
    Dim objFSO 
    Dim objFolder 
    Dim objItem 
    Set objFSO = Server.CreateObject("Scripting.FileSystemObject")
    
    If Request.QueryString("dir") <> "" Then 
    	Set objFolder = objFSO.GetFolder(RutaActual & Replace(StripTrail(Request.QueryString("dir")),"\","/") & "/" )
    	RutaDirectorio = striptrail(Replace(Request.QueryString("dir"),"\","/"))
		ParsePath(RutaDirectorio)
    Else
    	Set objFolder = objFSO.GetFolder(RutaActual)
		RutaDirectorio = ""
	End If
%>
	<tr>
		<td width="100%" height="100%">
		<div id="listadiv" style="height:100%" class="NoImprimir">
		<table border="0" width="100%">
  <%For Each objItem In objFolder.SubFolders %>
  <tr>
    <td width="60%" >
    <img alt="Abrir" border="0" src="../../../../images/cerrado.gif">
    <a href="frmsubirfoto.asp?dir=<%= replace(RutaDirectorio," ","+") & "/" & Replace(objItem.Name," ","+")%>">
    &nbsp;<%= objItem.Name %>
    </a>
    </td>
  </tr>
  <%Next%>
  <tr>
	<td width="100%" colspan="3">&nbsp;</td>
  </tr>
  <tr>
	<td width="100%" colspan="2">
	<table width="100%" bordercolor="gray" border="1">
	<%
	if objFolder.Files.Count>0 then
	
	i=0
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion

	For Each objItem In objFolder.Files
		mensaje="[No existe en BD]"
		alumno=""
		archivo=objItem.Name
		extension=ucase(right(archivo,3))

		if extension="JPG" and instr(archivo," ")=0 then
			set rsAlumno=Obj.Consultar("ConsultarAlumno","FO","AC",left(archivo,len(archivo)-4))
		
			if Not(rsAlumno.BOF and rsAlumno.EOF) then
				mensaje="[Existe en BD]"
				alumno="Estudiante: " & rsAlumno("alumno") & chr(13)
			end if
		
			i=i+1
			tamanio=int(objItem.Size/1000)
			imgFoto=mid(RutaDirectorio,2,len(RutaDirectorio))
			imgFoto=replace(imgFoto,"/","\")
			imgFoto="C:\" & imgFoto & "\" & archivo

			if (i mod 7= 0) then
				response.write "</tr><tr>"
			elseif i=1 then
				response.write "<tr>"
			end if
	%>
	<td align="center">
		<%if Tamanio>100 then%>
		<span class="rojo">[Tamaño no válido]<br></span>
		<%elseif mensaje="[Existe en BD]" then%>
	    <input name="chkfoto" id="chk<%=i%>" type="checkbox" value="File<%=i%>" onclick="HabilitarFoto(this)" />
	    <%end if%>
		<span class="cursoM"><%=mensaje%></span>
	    <br>	
	    <input disabled="true" type="file" name="File<%=i%>" value="<%=imgFoto%>"><br>
	    <img src="<%=imgFoto%>" alt="<%=alumno%>Tamaño: <%=tamanio%> KB" width="50" height="50" >
	    <br>
	    <%=archivo%>
    </td>
		<%	
	end if
  Next
  Obj.CerrarConexion
  Set Obj=nothing

  end if
  Set objItem = Nothing
  Set objFolder = Nothing
  Set objFSO = Nothing
%>
	</table>
	</td>
	</tr>
	<%if i>0 then%>
	<tr>
		<td class="usatTablaInfo" align="right" width="100%" ><%=i%> fotos encontradas
		<span id="Marcas"></span>
		</td>
		<script>Marcador.style.display=""</script>
	</tr>
	<%end if%>
	</table>
	</div>
	</td>
	</tr>
</table>
</form>
</body>
</html>