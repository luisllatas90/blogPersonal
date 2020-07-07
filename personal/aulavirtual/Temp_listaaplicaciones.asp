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
<body topmargin="0" leftmargin="0">
<table width="100%" height="50" border="0" cellpadding="0" cellspacing="0" bordercolor="#111111" style="border-collapse: collapse">
  <tr>
    <td width="100%" height="56px" 
          style="background-image: url('../Images/bannerav.png'); background-repeat: no-repeat; color: #FFFFFF;text-align:right;">
          
          <b>Usuario: <%=session("nombre_usu")%>&nbsp;&nbsp;</b>
    </td>
  </tr>
  
</table>
<br>
<table width="100%" height="75%">
<tr>
<td width="15%" height="100%" valign="top" class="contornotabla" bgcolor="#F0F0F0">
<table style="width: 100%">
	<tr>
		<td class="Menu" onMouseOver="ResaltarMenuElegido(1,this)" onMouseOut="ResaltarMenuElegido(0,this)" align="center" onclick="top.location.href='../acceder.asp?cbxtipo=P'">
		<img alt="Principal" src="../../images/menus/home4.gif" width="50" height="50"><br>
		Inicio</td>
	</tr>
	<tr>
		<td align="center">&nbsp;</td>
	</tr>
	<tr>
		<td class="Menu" onMouseOver="ResaltarMenuElegido(1,this)" onMouseOut="ResaltarMenuElegido(0,this)" align="center" onclick="AbrirPopUp('../../ayuda/aulavirtual.pdf','500','650')">
		<img alt="Ayuda" src="../../images/menus/Ayuda.gif" width="50" height="50"><br>
		Ayuda</td>
	</tr>
	<tr>
		<td align="center">&nbsp;</td>
	</tr>
	<%if instr(session("codigo_usu"),"gchunga")>0 then%>
	<tr>
		<td class="Menu" onMouseOver="ResaltarMenuElegido(1,this)" onMouseOut="ResaltarMenuElegido(0,this)" align="center" onclick="AbrirPopUp('moodleusat/frmcambiarplataforma.asp','500','650')">
		<img alt="Cerrar" src="../../images/menus/enviaryrecibir_1.gif"><br>
		Cambiar Plataforma
		</td>
	</tr>	
	<%end if%>
	
	
	<!-- Hector: 26/11/07 Mensaje de horario para programa de profesionalización sistema para profesores-->
	<%'if instr(session("codigo_usu"),"hzelada")>0  then %>
	    <script>
				  //AbrirPopUp('6toCiclo.htm','550','950','no','no','yes')
		 </script>
	<%'end if%>
	<!------------------------------------------------------------------------------------------------->
	
	<tr>
		<td class="Menu" onMouseOver="ResaltarMenuElegido(1,this)" onMouseOut="ResaltarMenuElegido(0,this)" align="center" onclick="top.window.close()">
		<img alt="Cerrar" src="../../images/menus/cerrar.gif" width="50" height="50"><br>
		Cerrar sesión</td>
	</tr>
	</table>
</td>
<td width="85%" height="100%" valign="top">
<table bgcolor="white" align="center" class="contornotabla" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%" height="100%">
	<tr class="tituloCursos"> 
		<td width="95%" height="3%" valign="top">
		Mis cursos</td>
		<td width="5%" height="3%" valign="top" align="right" style="width: 19%">
		<select name="cboidestadorecurso" onchange="actualizarlista('listaaplicaciones.asp?idestadorecurso=' + this.value)">
		<optgroup label="Según Disponibilidad">
		<option value="1" <%=Seleccionar(idestadorecurso,"1")%>>En proceso</option>
		<option value="3" <%=Seleccionar(idestadorecurso,"3")%>>Finalizados</option>
		</optgroup>
		</select></td>
	</tr>
	<%If (rsCursos.BOF and rsCursos.EOF) then%>
	<tr><td width="100%" colspan="2" align="center">
	<h2 class="rojo">No se encontraron cursos virtuales.</h2>
	<h3><br>
	Para cualquier consulta<br>puede comunicarse con la Oficina de Desarrollo de Sistemas de la Universidad <br>
	E-mail :desarrollosistemas@usat.edu.pe <br>
	Teléfono: 606200 Anexo 113 </h3>
	</td>
	</tr>
	<%else%>
	<tr>
		<td width="100%" height="97%" valign="top" colspan="2">
		<DIV id="listadiv" style="height:100%;">
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
			<li><a href='abrircursovirtual.asp?idcursovirtual=<%=rscursos("idcursovirtual")%>'>&nbsp;<%=Server.HTMLEncode(rsCursos("titulocursovirtual"))%></a></li>
			</td>
		</tr>
		<%rscursos.movenext
	  	Loop
	  	response.write "</ul>"
  		%>
		</table>
		</div>
		</td>
	</tr>
	<%end if%>
	</table>
</td>
</tr>
</table>
</body>
</html>