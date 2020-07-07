<!--#include file="clsforo.asp"-->
<%
idforo=request.querystring("idforo")
numfila=request.querystring("numfila")

Set foro=new clsforo
	with foro
		.restringir=session("idcursovirtual")
		Arrforo=.Consultar("4",Idforo,session("codigo_usu"),"")
		
		If IsEmpty(Arrforo)=false then
			tituloforo=ArrForo(2,0)
			descripcion=ArrForo(3,0)
			fechainicio=ArrForo(4,0)
			fechafin=ArrForo(5,0)
			Procesarfechas Fechainicio,FechaFin
			permitircalificar=ArrForo(6,0)
			tipocalificacion=ArrForo(7,0)
			numcalificacion=ArrForo(8,0)
			idcreador=ArrForo(9,0)
			idestadorecurso=ArrForo(10,0)
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de foro</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarforo.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body topmargin="0" leftmargin="0">
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td valign="top"  rowspan="4" width="5%">
    <%if idcreador=session("codigo_usu") and session("idestadocursovirtual")=1 then%>
    <img style="cursor:hand" onClick="AbrirForo('M','<%=idforo%>','<%=numfila%>')" border="0" src="../../../images/editar.gif" ALT="Haga clic aquí para modificar el foro"/><br><br>
    <img style="cursor:hand" onClick="AbrirForo('E','<%=idforo%>')" border="0" src="../../../images/eliminar.gif" ALT="Haga clic aquí para Eliminar el foro"/><br><br>
    <%call enviaremail("cursovirtual",session("idcursovirtual"),2)
	end if%>
   </td>
    <td width="20%" valign="top" class="etiqueta">Duración</td>
    <td valign="top" width="65%">:&nbsp;<%=Fechainicio & " hasta " & Fechafin%>&nbsp;</td>
    <td valign="top" rowspan="4" align="center" width="10%">

    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="100%" class="encabezadopregunta" align="center">
        <a target="_parent" href="listamensajes.asp?idforo=<%=idforo%>&idestadorecurso=<%=idestadorecurso%>&tituloforo=<%=tituloforo%>">
        <img border="0" src="../../../images/leer.gif"><br>Visualizar Mensajes </a>
        </td>
      </tr>
    </table>

    </td>
  </tr>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Título</td>
    <td valign="top" width="65%" id="txttituloforo" class="azul">:&nbsp;<%=tituloforo%>&nbsp;</td>
  </tr>
  <%if descripcion<>"" then%>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Descripción</td>
    <td valign="top" width="65%">:&nbsp;<%=PreparaMemo(descripcion)%>&nbsp;</td>
  </tr>
  <%end if
  if permitircalificar=1 then%>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Observaciones</td>
    <td valign="top" width="65%">Ud. puede calificar a los mensajes registrados&nbsp;</td>
  </tr>
  <%end if%>
</table>
</body>
</html>
	<%end if
end with
Set foro=nothing
%>