<!--#include file="funcionesaulavirtual.asp"-->
<%
IdTabla=request.querystring("IdTabla")

Function RecuperaTabla(ByVal Tabla,ByVal ID)
	cadSQL= "SELECT titulo" & Tabla & " FROM " & Tabla
	cadSQL= cadSQL & " WHERE Id" & Tabla & "=" & ID
	Set Obj= Server.CreateObject("AulaVirtual.clsDocumento")
		ArrTabla=Obj.CargarTablas(cadSQL)
	Set Obj=nothing
		If IsEmpty(ArrTabla)=false then
			RecuperaTabla=ArrTabla(0,0)
		else
			RecuperaTabla="--"
		end if
End Function
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Visitas a la Actividad Académica</title>
<script language="JavaScript">
function Mostrar(control)
{
  if(control.style.display=="none")
    {
		control.style.display=""
	 	event.srcElement.style.listStyleImage="url(images/menos.gif)"
    }
    else
    {
		control.style.display="none"
		event.srcElement.style.listStyleImage="url(images/mas.gif)"
    }
}
</script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<style>
<!--
li           {list-style-image: url('../../../images/mas.gif')}
-->
</style>
</head>
<body>
<%
	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		ArrDatos=Obj.MostrarVisitasRecurso("visitantes",0,idTabla)
	Set Obj=nothing
%>
<p class="e1"><u>Ingresos al Sistema</u>&nbsp;</p>
<ul>
<%If IsEmpty(Arrdatos) then
	response.write "<h3>No se han registrado accesos al Sistema</h3>"
else
for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
	<li onclick="Mostrar(document.all.datosvisita<%=i%>)" style="cursor:hand">&nbsp;<%=Arrdatos(1,I)%></li>
	<%	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
			ArrVisitas=Obj.MostrarVisitasRecurso("acceso",Arrdatos(0,I),idTabla)
		Set Obj=nothing
	%>
	<table border="1" cellpadding="3" cellspacing="0" style="display=none; border-collapse: collapse" bordercolor="#E4E4E4" width="80%" id="datosvisita<%=i%>">
	<%for v=Lbound(Arrvisitas,2) to Ubound(Arrvisitas,2)%>
  	<tr>
    	<td width="50%" colspan="2" bgcolor="#FEFFE1"><b>Entrada:</b> <%=Arrvisitas(1,v)%></td>
	    <td width="50%" bgcolor="#FEFFE1"><b>Salida:</b> <%=Arrvisitas(2,v)%></td>
  	</tr>
  		<%Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
			ArrRecursos=Obj.MostrarVisitasRecurso("visitasrecurso",Arrdatos(0,I),Arrvisitas(0,v))
			Set Obj=nothing
		If IsEmpty(ArrRecursos) then%>
		  	<tr><td width="100%" colspan="3"><font color="#FF0000">No se han registrado visitas a los recursos</font></td></tr>
	  	<%else%>
	  		<tr><td width="100%" colspan="3"><i><b>&nbsp;&nbsp; </b>
              <font color="#0000FF">&nbsp;Visitó los siguientes recursos</font></i></td></tr>
			<%for r=Lbound(ArrRecursos,2) to Ubound(ArrRecursos,2)%>
		  	<tr>
    			<td width="4%" style="border-right-style: none; border-right-width: medium" align="right">
                -</td>
		    	<td width="45%" style="border-left-style: none; border-left-width: medium"><%=ArrRecursos(1,r)%>:</td>
			    <td width="73%"><%=RecuperaTabla(ArrRecursos(1,r),ArrRecursos(0,r))%>&nbsp;</td>
			</tr>
			<%next
		end if
	next%>
	</table>
	</br>
<%next%>
</ul>
<%End if%>
</body>
</html>