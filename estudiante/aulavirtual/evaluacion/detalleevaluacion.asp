<!--#include file="clsevaluacion.asp"-->
<%
idevaluacion=request.querystring("idevaluacion")
numfila=request.querystring("numfila")

Set evaluacion=new clsevaluacion
	with evaluacion
		.restringir=session("idcursovirtual")
		Arrevaluacion=.Consultar("2",Idevaluacion,session("codigo_usu"),"")
		
		If IsEmpty(Arrevaluacion)=false then
			tituloevaluacion=Arrevaluacion(2,0)
			fechainicio=Arrevaluacion(3,0)
			fechafin=Arrevaluacion(4,0)
			descripcion=Arrevaluacion(5,0)
			instrucciones=Arrevaluacion(6,0)
			idcreador=Arrevaluacion(8,0)
			mostrarresultados=Arrevaluacion(9,0)
			incluirimagenes=Arrevaluacion(10,0)
			modificarrespuesta=Arrevaluacion(11,0)
			preguntaporpregunta=Arrevaluacion(12,0)
			retrocederpaginas=Arrevaluacion(13,0)
			respuestacorrecta=Arrevaluacion(14,0)
			minutos=Arrevaluacion(17,0)
			limiteaccesos=Arrevaluacion(16,0)
			idtipopublic=Arrevaluacion(20,0)
			idestadorecurso=Arrevaluacion(21,0)
			nombrecategoria=Arrevaluacion(22,0)
			vecesrealizadas=Arrevaluacion(23,0)
			totalpreguntas=Arrevaluacion(24,0)
			minutos=iif(minutos=0,"Ilimitado",minutos)
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de evaluacion</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarevaluacion.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body topmargin="0" leftmargin="0">
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td valign="top"  rowspan="6" width="5%">
    <%if idcreador=session("codigo_usu") and session("idestadocursovirtual")=1 then%>
    <img style="cursor:hand" onClick="AbrirEvaluacion('P','<%=idevaluacion%>')" src="../../../images/<%=iif(idtipopublic=3,"p1","todos")%>.gif" ALT="Haga click aquí para modificar los permisos al recurso"/><br>
    <img style="cursor:hand" onClick="AbrirEvaluacion('M','<%=idevaluacion%>','<%=numfila%>')" border="0" src="../../../images/editar.gif" ALT="Haga clic aquí para modificar el evaluacion"/><br><br>
    <img style="cursor:hand" onClick="AbrirEvaluacion('E','<%=idevaluacion%>')" border="0" src="../../../images/eliminar.gif" ALT="Haga clic aquí para Eliminar el evaluacion"/><br><br>
    <%call enviaremail("evaluacion",idevaluacion,idtipopublic)
	else%><br>
    <img src="../../../images/<%=iif(idtipopublic=3,"p1","menu0")%>.gif">
    <%end if%>
   </td>
    <td width="20%" valign="top" class="etiqueta">Duración</td>
    <td valign="top" width="65%">:&nbsp;<%=Fechainicio & " hasta " & Fechafin%>&nbsp;</td>
    <td valign="top" rowspan="6" align="center" width="10%">

    <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="100%" class="encabezadopregunta" id="txtmensajeinicio" align="center">
        <%call .mostrarestado(idestadorecurso,limiteaccesos,vecesrealizadas,idevaluacion)%>&nbsp;</td>
      </tr>
    </table>

    </td>
  </tr>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Título</td>
    <td valign="top" width="65%" id="txttituloevaluacion" class="azul">:&nbsp;<%=tituloevaluacion%>&nbsp;</td>
  </tr>
  <%if descripcion<>"" then%>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Descripción</td>
    <td valign="top" width="65%">:&nbsp;<%=PreparaMemo(descripcion)%>&nbsp;</td>
  </tr>
  <%end if
  if tipoeval="L" then%>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Instrucciones</td>
    <td valign="top" width="65%"><%=PreparaMemo(instrucciones)%>&nbsp;</td>
  </tr>
  <%end if%>
  <tr>
    <td width="20%" valign="top" class="etiqueta">Tipo</td>
    <td valign="top" width="65%">:&nbsp;<%=nombrecategoria%></td>
  </tr>
  <tr>
    <td width="20%" valign="top" class="etiqueta">&nbsp;</td>
    <td valign="top" width="65%">
    <ul>
      <li>Número de accesos:&nbsp;<b><%=limiteaccesos%></b>&nbsp;</li>
      <li>Tiempo de Duración:&nbsp;<b><%=minutos%></b></li>
      <li>Número de preguntas:&nbsp;<b><%=totalpreguntas%> preguntas</b></li>
    </ul>
    </td>
  </tr>
</table>
</body>
</html>
	<%end if
end with
Set evaluacion=nothing
%>