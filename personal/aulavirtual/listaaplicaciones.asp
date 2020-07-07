<!--#include file="../../funcionesaulavirtual.asp"-->
<%
idestadorecurso=request.querystring("idestadorecurso")
idusuario=session("codigo_usu")
if idestadorecurso="" then idestadorecurso=1

if session("codigo_usu")="" then response.redirect "../../tiempofinalizado.asp"

Set Obj=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsCursos=Obj.Consultar("ConsultarCursoVirtual","FO",7,idusuario,idestadorecurso,0)
	Obj.CerrarConexion
Set Obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Aula Virtual</title>
<link rel="stylesheet" type="text/css" href="../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../private/funcionesaulavirtual.js"></script>
<style type="text/css">
<!--
A:hover
{
	color: red;
	font-weight: bold;
}

li{list-style-image: url('../../images/librocerrado.gif');}
a:Link {
	color: #000000;
	text-decoration: underline;
}
.Menu {
	border-style: solid;
	border-width: 0px;
	border-color: #96965E;
	font-weight: bold;
	font-size: 12pt;
	font-family: Arial Narrow;
	background-color: #F3F3F3;
}
.menuElegido {
	border-style: solid;
	border-width: 2px;
	border-color: #96965E;
	background-color: #EBE1BF;
	cursor:hand;
	font-size: 12pt;
	font-weight: bold;
}
.menuNoElegido {
	border-style: solid;
	border-width: 0px;
	border-color: #96965E;
	background-color: #F3F3F3;
	font-weight: bold;
	font-size: 12pt;
}
.tituloCursos {
	font-size: small;
	font-weight: bold;
	background-color: #ECE9D8;
	color:navy
}
-->
</style>
<script type="text/javascript" language="javascript">
function ResaltarMenuElegido(op,fila)
{
	if(op==1)
		{fila.className="menuElegido"}
	else
		{fila.className="menuNoElegido"}
}
</script>
</head>
<body>
<h3 style="color: #800000">Cursos habilitados para uso del Aula Virtual</h3>
<%if session("codigo_tfu")=1 then%>
<h5 onclick="AbrirPopUp('moodleusat/frmcambiarplataforma.asp','500','650')">[Cambiar 
    cursos a nuevo entorno de Aula Virtual]</h5>
<%end if%>
<table bgcolor="white" align="center" class="contornotabla" cellpadding="3" cellspacing="0" style="border-collapse: collapse; width:100%">
	<tr class="tituloCursos"> 
		<td width="100%" height="3%" valign="top">Mis cursos
		<select name="cboidestadorecurso" onchange="actualizarlista('listaaplicaciones.asp?idestadorecurso=' + this.value)">
		<optgroup label="Según Disponibilidad">
		<option value="1" <%=Seleccionar(idestadorecurso,"1")%>>En proceso</option>
		<option value="3" <%=Seleccionar(idestadorecurso,"3")%>>Finalizados</option>
		</optgroup>
		</select></td>
	</tr>
	<%If (rsCursos.BOF and rsCursos.EOF) then%>
	<tr><td width="100%" colspan="2" align="center">
	<h2 class="rojo"><b>:::Nueva Aula Virtual:::</b><br /></h2>
	<p style="color: Blue; font-size:x-small">
        &nbsp; A partir del Ciclo Academico 2011-II Modalidad Regular:<br />
        Para acceder al aula virtual, deberá ingresar haciendo clic en <b>&quot;Nueva Aula 
        Virtual&quot;</b> y posteriormente hacer clic en el botón:<b>&quot;Clic para Ingresar&quot; . </b>
        Automáticamente accederá a la&nbsp; plataforma virtual para gestionar sus cursos 
        habilitados.</p>
	<h3><br>
	    Haga clic en el menú &quot;activar cursos&quot;, según la carga académica asignada al 
        ciclo académico ACTUAL <br>
	    E-mail :desarrollosistemas@usat.edu.pe <br>
	    Teléfono: 606200 Anexo 113 </h3>
	</td>
	</tr>
	<%else%>
	<tr>
		<td width="100%" valign="top" colspan="2">
		<table width="100%" cellpadding="3" cellspacing="0">
		<%
		tipo=0
		i=0
		response.write "<ul>"
		Do while Not rsCursos.EOF
			i=i+1
			if int(tipo)<>int(rsCursos("Tipo")) then
				tipo=rsCursos("Tipo")
				if i>1 then
					response.write "<tr><td>&nbsp;</td><tr>"
				end if
		%>
		<tr>
			<td id="tipo<%=tipo%>" width="78%" height="3%" valign="top" class="colorbarra">
			<b><%=rsCursos("nTipo")%></b> </td>
		</tr>
		<%end if%>
		<tr>
			<td id="tipo<%=tipo%>" width="78%" height="3%" valign="top">
			<li><a target="_self" href='abrircursovirtual.asp?idcursovirtual=<%=rscursos("idcursovirtual")%>'>
                &nbsp;<%=Server.HTMLEncode(rsCursos("titulocursovirtual"))%></a></li>
			</td>
		</tr>
		<%rscursos.movenext
	  	Loop
	  	response.write "</ul>"
  		%>
		</table>
		</td>
	</tr>
	<%end if%>
	</table>
    <p style="color: #FF0000">
        Para activar los cursos según su carga académica actual, haga clic en el menú 
        &quot;Activar cursos&quot;</p>
    
    
</body>
</html>