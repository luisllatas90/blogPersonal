<!--#include file="../../../funciones.asp"-->


<html>
<head>
<title>Registro de Propuestas</title>

<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo4 {
	color: #395ACC;
	font-weight: bold;
	font-size: 12pt;
}
.Estilo8 {color: #000000}
.Estilo12 {color: #395ACC; font-weight: bold; }
.Estilo14 {color: #000000; font-weight: bold; }
.Estilo15 {color: #395ACC}
-->
</style>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script>
function enviarDatos(codigo_per){
	var fechaInicio=frmpropuesta.txtFechaInicio.value
	var fechaFin=frmpropuesta.txtFechaFin.value
	//alert (fechaInicio)
	//if (fechaInicio >  fechaFin){
	//	alert ('La fecha fin debe ser mayor que la fecha inicio')
	//}else{
		location.href="AsistenciasIndividual_v2.asp?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin + "&codigo_per=" + codigo_per //+ "&descripcion=" + descripcion + "&Duracion=" + Duracion + "&Periodo=" + Periodo + "&FechaInicio=" + FechaInicio 			+ "&Renovacion="+ Renovacion + "&NumCopias=" + NumCopias + "&accion=guardar" + "&Observacion=" + Observacion + "&Responsable=" + Responsable + "&Referencia=" + Referencia + "&remLen=" + remLen + "&resolucion=" + resolucion 
	//}
}
</script>
</head>
<%
if Request.QueryString("codigo_per")<>"" then
	codigo_per=Request.QueryString("codigo_per")
else
	codigo_per=session("codigo_usu")
end if
fechaInicio=Request.QueryString("fechaInicio")
fechaFin=Request.QueryString("fechaFin")

''consultar datos del personal
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
		 set personal=objProp.Consultar("spPla_ConsultarPersonal","FO","ES",codigo_per)
	 objProp.CerrarConexion
	 set objProP=nothing


''response.write fechaInicio & " - "  & fechaFin
if fechaInicio="" and fechaFin="" then
	fechaInicio=Date-1
	fechaFin=Date-1
end if
%>
		    <body topmargin="0" rightmargin="0" leftmargin="0">
		    <form method="post"  name="frmpropuesta" id="frmpropuesta" action="AsistenciasIndividual_v2.asp"s>
		      <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center">&nbsp;</td>
                </tr>
                <tr>
                  <td align="center"><span class="Estilo4">Reporte de Asistencias </span></td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td><table width="95%" border="0" align="center" cellpadding="3" cellspacing="0" bgcolor="#FFFFFF" class="contornotabla">
                    <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo14">Trabajador</span></td>
                      <td><%
					  if IsNull(personal("descripcion_tpe")) then
					  	tipo= ""
					  else
					  	tipo =" - " & personal("descripcion_tpe")
					  end if
					  
					  if IsNull(personal("descripcion_ded")) then
					  	dedicacion= ""
					  else
					 	dedicacion = " - " & personal("descripcion_ded")
					  end if					  
					  Response.Write( personal("PERSONAL")  & tipo  & dedicacion)
					
					  
					  %></td>
                      <td width="38%" rowspan="4" align="center" valign="middle">
					  <%IF ISNULL(personal("foto")) THEN%>
					  <img src="../../../images/FotoVacia.gif" border="0" usemap="#Map">
					  <%ELSE%>
					  <img src="../../imgpersonal/<%=personal("codigo_per") &  ".jpg"%>" Width="100px" Height="130px" border="0" usemap="#Map" class="contornotabla_azul">
					  <%END IF%>
					  </td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo14">Centro de Costos </span></td>
                      <td><%=personal("descripcion_Cco")%></td>
                    </tr>
                    <tr>
                      <td width="3%">&nbsp;</td>
                      <td width="20%"><span class="Estilo14">Fecha de Inicio </span></td>
                      <td width="39%"><span class="Estilo8">
                        <input disabled="disabled" name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=fechainicio%>">
                        <input name="Submit2" type="button" class="cunia" onClick="MostrarCalendario('txtFechaInicio')" >
                      </span></td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td><span class="Estilo14">Fecha de Fin </span></td>
                      <td><span class="Estilo8">
                        <input disabled="disabled" name="txtFechaFin" type="text" class="Cajas" id="txtFechaFin" value="<%=fechaFin%>">
                        <input name="Submit3" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaFin')" >
                        <br>
                      * seleccionar como m&aacute;ximo el d&iacute;a de ayer </span></td>
                    </tr>
                    <tr>
                      <td colspan="2">&nbsp;</td>
                      <td colspan="2" align="right"><strong><a href="javascript:history.back()"><img src="../../../images/menus/contraer.gif" width="8" height="7" border="0"><span class="Estilo15">Volver</span></a> </td>
                    </tr>
                    <tr>
                      <td colspan="4" align="center" bgcolor="#F0F0F0" class="contornotabla">
					  <input name="Submit" type="button" class="buscar1" value="    Consultar Asistencias" onClick="enviarDatos(<%=codigo_per%>)">					  </td>
                    </tr>
                    
                    
                  </table></td>
                </tr>
                <tr>
                  <td>
  <%
'' if Request.QueryString("codigo_per")<>"" then
	''codigo_per=Request.QueryString("codigo_per")
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
	 if fechaFin>=date() then
		fechaFin=date()-1
	 end if
	 set asistencia=objProp.Consultar("spPla_ConsultarAsistencias","FO","DE",fechaInicio,fechaFin,codigo_per)

	 objProp.CerrarConexion
	 set objProP=nothing
'' end if	 
	%>				  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Información obtenida en base a 
                      la marcaciones procesadas por Dirección de Personal hasta el momento.</td>
                </tr>
                <tr>
                  <td><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
                    <tr>
                      <td width="4%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Num.</span></td>
                      <td width="8%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Día</span></td>
                      <td width="8%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Fecha</span></td>
                      <td width="11%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Marcaci&oacute;n<br>
                      Entrada </span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Min.Tarde</span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Min. Antic. Salida </span></td>
                      <td width="7%" align="center" bgcolor="#E1F1FB" class="bordeizqinf"><span class="Estilo12">Observaci&oacute;n</span></td>
                    </tr>
					<%do while not asistencia.eof%>                    
					<tr>
					<%i=i+1%>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=i%></td>

                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=asistencia("Dia")%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=asistencia("Fecha")%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=asistencia("iniciomarca")%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=asistencia("minutostardeingresoNoPerm_cpe")%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=asistencia("minutosanticipadosalida_cpe")%></td>
                      <td align="center" bgcolor="#FFFFFF" class="bordeizqinf Estilo8"><%=asistencia("Observacion")%></td>
                    </tr>
					<%
					asistencia.MoveNext
					loop%>
                  </table></td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
              </table>
		    </form>

	     	
<map name="Map"><area shape="rect" coords="-75,-14,-23,17" href="#"></map></body>
</html>
