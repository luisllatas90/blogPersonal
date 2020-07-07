<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
recurso=request.querystring("recurso")
if recurso="" then recurso="evaluacion"

select case recurso
	case "documento"
		response.redirect "../documentos/exploradordocumentos.asp?mostrardescargas=s"
	case "evaluacion"
		Set Obj= Server.CreateObject("AulaVirtual.clsEvaluacion")
  			arrDatos=Obj.Consultar("13",session("idcursovirtual"),"","")
	  	Set Obj= Nothing
		pagina="../evaluacion/verresultadosencuesta.asp?idevaluacion="
end select

%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione el tipo de recurso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script>
	function AbrirEstadistica(codigo_recurso)
	{
		AbrirMensaje()
		location.href='<%=pagina%>' + codigo_recurso
	}
</script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="28%" class="etiqueta">Seleccione el tipo de recurso</td>
    <td width="72%"><select size="1" name="cbxrecurso">
    <option value="Documento">Documentos</option>
    <option value="evaluacion" selected <%=SeleccionarItem("cbo",recurso,"evaluacion")%>>Encuestas</option>
    <option value="Tarea" <%=SeleccionarItem("cbo",recurso,"tarea")%>>Tareas</option>
    </select> 
    <img style="cursor:hand" border="0" src="../../../images/buscar.gif" onclick="location.href='estadisticasrecurso.asp?recurso=' + cbxrecurso.value"></td>
  </tr>
  <%If IsEmpty(Arrdatos)=false then%>
  <tr>
    <td width="100%" colspan="2">
    <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr class="etabla">
        <td width="5%">#</td>
        <td width="95%">Descripción del recurso</td>
      </tr>
      <%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
      <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
        <td width="5%"><%=i+1%>&nbsp;</td>
        <td width="95%" onclick="AbrirEstadistica('<%=Arrdatos(0,i)%>')"><%=Arrdatos(1,i)%>&nbsp;</td>
      </tr>
      <%next%>
    </table>
    </td>
  </tr>
  <%end if%>  
</table>

</body>

</html>