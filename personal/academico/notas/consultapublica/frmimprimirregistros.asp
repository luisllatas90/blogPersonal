<!--#include file="../administrarconsultar/clsNotas.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
nombre_dac=request.querystring("nombre_dac")
estadonota_cup=request.querystring("estadonota_cup")
descripcion_cac=request.querystring("descripcion_cac")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_dac="" then codigo_dac="-2"

Dim notas

Set notas=new clsnotas
	with notas
		Set rsCac=.ConsultarCicloAcademico("TO","")
		
		if codigo_cac<>"-2" then
			Set rsDpto=.ConsultarDptoAcademico("TO","")
		end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Llenado de Registro de Notas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarnotas.js"></script>
</head>
<body>
<table width="100%" <%if codigo_dac<>"-2" and codigo_cac<>"" then%>height="100%"<%end if%> border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse">
  <tr>
    <td height="5%" class="usattitulo">Registro de Notas</span></td>
    <td height="5%">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" height="10%" valign="top">
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="18%">Ciclo</td>
        <td width="82%"><%call llenarlista("cboCiclo","ActualizarListaRegistros()",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
		</td>
      </tr>
      <tr>
        <td width="18%">Departamento</td>
        <td width="82%"><%call llenarlista("cboDpto","",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico","S","")%></td>
      </tr>
      <tr>
        <td width="18%">Estados</td>
        <td width="82%">
          <select size="1" id="cboEstado" name="cboestado">
            <option value="R" <%=SeleccionarItem("cbo",estadonota_cup,"R")%>>Registros Llenados</option>
            <option value="I" <%=SeleccionarItem("cbo",estadonota_cup,"I")%>>Registros Impresos</option>
            <option value="C" <%=SeleccionarItem("cbo",estadonota_cup,"C")%>>Registros Confirmados</option>
            <option value="P" <%=SeleccionarItem("cbo",estadonota_cup,"P")%>>Registros Pendientes</option>
          </select>
          <img onClick="ActualizarListaRegistros()" border="0" src="../../../../images/buscar.gif"></td>
      </tr>
    </table></td>
  </tr>
  <%if codigo_dac<>"-2" and codigo_cac<>"" then
	set rsDocente=.ConsultarNotas("ER",codigo_cac,codigo_dac,estadonota_cup)
	total=rsDocente.recordcount

	if total>0 then%>
  <tr>
    <td colspan="2" width="100%" height="40%" valign="top">
	<%
		Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
		ArrEncabezados=Array("Docente")
		ArrCampos=Array("docente")
		ArrCeldas=Array("90%")
		ArrCamposEnvio=Array("codigo_per","docente")
		pagina="detallecarga.asp?descripcion_cac=" & descripcion_cac & "&codigo_cac=" & codigo_cac

		call CrearRpteTabla(ArrEncabezados,rsDocente,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,null)
	%>
	</td>
  </tr>
    <tr>
  	<td width="100%" colspan="3" height="3%" class="contornotabla" bgcolor="#C7E0CE">
  	<table cellSpacing="0" cellPadding="0" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  	<tr class="usatEtiqOblig"><td width="65%">Carga Académica </td>
    <td width="25%" align="right" valign="top"><b><%=total%> profesor-es</b></td>
  	</tr>
  	</table> 	
  	</td>
  </tr>
  <tr>
    <td colspan="3" width="100%" height="30%" valign="top" class="contornotabla">
	<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el docente para ver la Carga Académica asignada</span>
	<iframe id="fradetalle" src="detallecarga.asp" height="100%" width="100%" border="0" frameborder="0">
	</iframe>
	</td>
  </tr>
	<%else%>
		<tr><td valign="top"><span class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp; No se han encontrado profesores que han llenado registro de notas</span></td></tr>
	<%end if
  end if%>
</table>
</body>
</html>
<%
end with

set rsDpto=nothing
set rsCac=nothing	
set notas=nothing
%>