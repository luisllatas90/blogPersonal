<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

'****************************************************
'Obtener los datos del curso
'****************************************************
if session("codigo_cup")>0 then
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			Set rsCurso=obj.Consultar("ConsultarCursoProgramado","FO",21,session("codigo_cup"),0,0,0)
		obj.CerrarConexion
	Set Obj=nothing
	
	If not(rsCurso.BOF and rsCurso.EOF) then
		nombre_cur=rsCurso("nombre_cur")
		identificador_cur=rsCurso("identificador_cur")
		nombre_cpf=rsCurso("nombre_cpf")
		creditos_cur=rsCurso("creditos_cur")
		ciclo_cur=rsCurso("ciclo_cur")
		descripcion_cac=rsCurso("descripcion_cac")
		duracion=formatdatetime(rsCurso("fechainicio_cup"),1) & " al " & formatdatetime(rsCurso("fechafin_cup"),1)
		profesor=rsCurso("profesor")
		email=rsCurso("email")
	end if
else
	nombre_cur=session("nombrecursovirtual")
	duracion=formatdatetime(session("iniciocursovirtual"),1) & " al " & formatdatetime(session("fincursovirtual"),1)
end if

'****************************************************
'Verificar los permisos del usuario
'****************************************************
Dim resaltar
if session("codigo_tfu")=1 then
	EsAdministrador=true
	resaltar="onMouseOver=""Resaltar(1,this,'S')"" onMouseOut=""Resaltar(0,this,'S')"" "
end if

'****************************************************
'Obtener los contenidos del curso virtual
'****************************************************
Sub CrearTematica(ByVal idcursovirtual,ByVal codigo_tfu,ByVal codigo_usu,ByVal codigo_ccv,ByVal j)
		dim i,x
		dim ImagenMenu,TextoMenu,idPadre
		dim rsContenido
		x=0
		
		Set rsContenido=obj.Consultar("ConsultarPortadaCursoVirtual","FO",idcursovirtual,codigo_tfu,codigo_usu,codigo_ccv)
		
		for i=1 to rsContenido.recordcount
			cadena=""
			
			'Genera espacios para jerarquía
			for x=1 to j
				'cadena=cadena & "..." ' incluir imagen en blanco
				cadena=cadena & "<img border=""0"" src='../../../images/blanco.gif'>"
			next
					
			'====================================================================
			'Almacenar variables de campos
			'====================================================================
			ImagenMenu=rsContenido("iconoNormal_tre")
			idPadre=rsContenido("refcodigo_ccv")
			TextoMenu=rsContenido("tematica")
								
			'====================================================================
			'Imprimir Fila del menú
			'====================================================================
			response.write  "<tr " & resaltar & " id='Mnu" & idPadre & "'>"  & vbcrlf
			response.write "<td>"
			response.write  cadena & "<img hspace='0' vspace='0' border='0' name='arrImgCarpetas' src='../../../images/" & ImagenMenu & "' align=absbottom>&nbsp;" & TextoMenu & vbcrlf
			response.write "</td>"
			response.write  "</tr>"  & vbcrlf

			x=x+1
													
			CrearTematica idcursovirtual,codigo_tfu,codigo_usu,rsContenido("codigo_ccv"),x

			rsContenido.movenext 						
			
		next
end Sub

%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>contenido del curso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script type="text/javascript" language="Javascript">
	function MostrarBloque(id,img)
	{
		if (id.style.display=="none"){
			id.style.display=""
			img.src="../../../images/menos.gif"
		}
		else{
			id.style.display="none"
			img.src="../../../images/mas.gif"
		}
	}
	
	function OcultarBloque(id,img)
	{
		id.style.display="none"
		img.src="../../../images/mas.gif"
	}
	
	function AbrirEnlace(obj)
	{
		if (obj.value!=""){window.location.href=obj.value}
	}
	
	function AbrirVentana(obj)
	{
		if (obj.value!=""){
			if (obj.value.indexOf('eliminar')!=-1){
				if (confirm("¿Está completamente seguro que desea eliminar el ítem?")==true){
					window.location.href="cargando.asp?rutapagina=" + obj.value
				}
			}
			else{
				if (obj.value.indexOf('frmencuesta')!=-1){
					window.location.href=obj.value
				}
				else{
					var izq = (screen.width-600)/2
					var arriba= (screen.height-500)/2
			
					window.open(obj.value,"tematica","height=400,width=600,statusbar=yes,scrollbars=yes,top=" + arriba + ",left=" + izq + ",resizable=yes,toolbar=no,menubar=no");
					obj.value=""
				}
	   		}
	   	}
	}

</script>
<style type="text/css">
.bordeitem   { font-size: 12pt; font-weight: bold; background-color: #ECE9D8; text-align:center }
.contornobloque{ border: 1px solid #91A9DB;  }
.titulomenu  { border-style:solid; border-width:0px; font-size:14pt; font-weight:bold; color:#800000  }
</style>
<style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline; font-weight: bold}
</style>
</head>
<body>
<%
Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
		if EsAdministrador=true and trim(session("mododesarrollo"))<>"I" then
			Set rsMenu=obj.Consultar("ConsultarAplicacionUsuario","FO",12,session("codigo_apl"),session("codigo_tfu"),0)
		end if
		Set rsContenidoP=obj.Consultar("ConsultarPortadaCursoVirtual","FO",session("idcursovirtual"),session("codigo_tfu"),session("codigo_usu"),0)

%>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr>
    <td width="75%" class="titulomenu">Diseño de asignatura</td>
    <td width="25%" align="right">
    <%if EsAdministrador=true and trim(session("mododesarrollo"))<>"I" then
    	If Not(rsMenu.BOF and rsMenu.EOF) then
    %>
    <select name="cboAgregar" class="Cajas2" onchange="AbrirVentana(this)">
		<option value="">Administrar...</option>
		<%Do while Not rsMenu.EOF
			response.write"<option value=""" & rsMenu("enlace_men") & """>-&nbsp;" & rsMenu("descripcion_men") & "</option>"
			rsMenu.movenext
			Loop
		%>
	</select>
	<%	end if
	set rsMenu=nothing
	
	end if%>
	<!--<img alt="Ayuda" src="../../../images/ayuda.gif">-->
	</td>
  </tr>
  </table>
<br>
<table class="contornobloque" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="90%" colspan="3" class="e1">Datos Informativos</td>
    <td width="3%" class="bordeitem">
    <a href="Javascript:MostrarBloque(tblInformacion,imginf)"><img id="imginf" border="0" src="../../../images/menos.gif"></a></td>
  </tr>
  <tbody id="tblInformacion">
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Descripción</b></td>
    <td width="70%" colspan="2"><%=nombre_cur%>&nbsp;</td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
<%if identificador_cur<>"" then%>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Código</b></td>
    <td width="70%" colspan="2"><%=identificador_cur%>&nbsp;</td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Escuela Profesional</b></td>
    <td width="70%" colspan="2"><%=nombre_cpf%>&nbsp;</td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Créditos</b></td>
    <td width="35%"><%=creditos_cur%>&nbsp;</td>
    <td width="35%"><b>Ciclo de estudios:</b> <%=ciclo_cur%></td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Ciclo Académico</b></td>
    <td width="70%" colspan="2"><%=descripcion_cac%>&nbsp;</td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
<%end if%>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Duración</b></td>
    <td width="70%" colspan="2"><%=duracion%>&nbsp;</td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
<%if identificador_cur<>"" then%>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="20%"><b>Profesor(es)</b></td>
    <td width="70%" class="azul" colspan="2"><%=profesor%><br><%=email%>&nbsp;</td>
    <td width="3%" class="bordeitem">&nbsp;</td>
  </tr>
<%end if%>
  </tbody>
</table>
<%
If Not(rsContenidoP.BOF and rsContenidoP.EOF) then
	Do While Not rsContenidoP.EOF
		i=i+1
		imagen="menos.gif"
		oculto=""
		
		if rsContenidoP("idestadorecurso")>1 then
			imagen="mas.gif"
			oculto="style=""display:none"" "
		end if
%>
<br>
<table class="contornobloque" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr>
    <td width="3%" class="bordeitem"><%=i%></td>
    <td width="90%">
    <table border="0" width="100%">
    	<tr class="e1">
			<%response.write "<td>" & rsContenidoP("tematica")%>    	
    	</tr>
    </table>
    </td>
    <td width="3%" class="bordeitem">
    <a href="Javascript:MostrarBloque(tblbloque<%=i%>,imginf<%=i%>)"><img id="imginf<%=i%>" border="0" src="../../../images/<%=imagen%>"></a></td>
  </tr>
  <tbody id="tblbloque<%=i%>" <%=oculto%>>
  <tr>
    <td width="3%" class="bordeitem">&nbsp;</td>
    <td width="90%">
    <table border="0" width="100%">
    <%
    CrearTematica session("idcursovirtual"),session("codigo_tfu"),session("codigo_usu"),rsContenidoP("codigo_ccv"),0
    %>
    </table>
    </td>
    <td width="3%" class="bordeitem">
	&nbsp;
    </td>
  </tr>
</table>
	<%rsContenidoP.movenext
	Loop
	
	Set rsContenidoP=nothing
end if

	obj.CerrarConexion
Set Obj=nothing
%>
</body>
</html>