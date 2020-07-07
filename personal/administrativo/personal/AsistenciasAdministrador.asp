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
-->
</style>
<link rel="stylesheet" href="style.css" />
<script type="text/javascript" src="script.js"></script>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../private/calendario.js"></script>



<script>
function enviarDatos(){
	var fechaInicio=frmpropuesta.txtFechaInicio.value
	var fechaFin=frmpropuesta.txtFechaFin.value
	var ceCo = frmpropuesta.cboCentroCostos.value
	//alert (fechaInicio)
	//if (fechaInicio >  fechaFin){
	//	alert ('La fecha fin debe ser mayor que la fecha inicio')
	//}else{
		location.href="AsistenciasAdministrador.asp?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin + "&cboCeCo=" + ceCo//+ "&codigo_per=" + codigo_per //+ "&descripcion=" + descripcion + "&Duracion=" + Duracion + "&Periodo=" + Periodo + "&FechaInicio=" + FechaInicio 			+ "&Renovacion="+ Renovacion + "&NumCopias=" + NumCopias + "&accion=guardar" + "&Observacion=" + Observacion + "&Responsable=" + Responsable + "&Referencia=" + Referencia + "&remLen=" + remLen + "&resolucion=" + resolucion 
	//}
}
</script>
</head>
<%


codigo_per=session("codigo_usu")
fechaInicio=Request.QueryString("fechaInicio")
fechaFin=Request.QueryString("fechaFin")
//response.write(Request.queryString("cboCeCo"))
				  
''consultar datos del personal
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
	 set personal=objProp.Consultar("spPla_ConsultarPersonal","FO","ES",session("codigo_usu"))
	 objProp.CerrarConexion
	 set objProP=nothing

if fechaInicio="" and fechaFin="" then
	fechaInicio=Date-1
	fechaFin=Date-1
end if
%>
		    <body topmargin="0" rightmargin="0" leftmargin="0">
		    <form method="post"  name="frmpropuesta" id="frmpropuesta" action="AsistenciasIndividual.asp">
		      <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                  <td align="center">&nbsp;			  
				  </td>
                </tr>
                <tr>
                  <td align="center"><span class="Estilo4">Reporte de Asistencias </span></td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
                <tr>
                  <td>
		  
<table width="95%" border="0" align="center" cellpadding="3" cellspacing="0" bgcolor="#FFFFFF">
                    <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td colspan="2">&nbsp;</td>
                    </tr>
                    
                    <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td width="25%" rowspan="3" align="center" valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                      <td width="3%">&nbsp;</td>
                      <td width="20%"><span class="Estilo14">Fecha de Inicio </span></td>
                      <td width="52%"><span class="Estilo8">
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
                      </span></td>
                    </tr>
                    <tr>
                      <td>&nbsp;</td>
                      <td class="Estilo14">Centro de costos </td>
                      <td>
					  <%
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RSCeCo=objProp.Consultar("PRESU_ConsultarCentroCostos","FO","CP",0)
	 objProp.CerrarConexion
	 set objProP=nothing
	 
call llenarlista("cboCentroCostos",Request.queryString("cboCeCo"),RSCeCo,"codigo_cco","descripcion_cco",cboCentroCostos,"TODOS","","")					  
					  %>
					                     </td>
                      <td align="center" valign="top">&nbsp;</td>
                    </tr>
                    <tr>
                      <td colspan="2">&nbsp;</td>
                      <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                      <td colspan="4" align="center" bgcolor="#F0F0F0" class="contornotabla">
					  <input name="Submit" type="button" class="buscar1" value="    Consultar Asistencias" onClick="enviarDatos()">					  </td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <td>
  <%
	 if Request.queryString("cboCeCo") ="" then
		codigo_cco=-2
	else
		codigo_cco = Request.queryString("cboCeCo")
	 end if

     codigo_per=Request.QueryString("codigo_per")
	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 'response.Write "FI:" + fechaInicio + " FF:" + fechaFin + " CCO:" + codigo_cco
	 
	 objProp.AbrirConexion
	 
	 
	 set asistencia=objProp.Consultar("spPla_ConsultarAsistencias","ST","TO",fechaInicio,fechaFin,codigo_cco)
	 
	 'objProp.CerrarConexion
	 set objProP=nothing
	 
	%>				  </td>
                </tr>

                <tr>
                  <td>

				  <div id="wrapper">		
<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class="sortable" id="sorter" >

                    <tr>
                      <th width="6%" align="center" bgcolor="#E1F1FB" class="nosort">Num.</th>
                      <th width="36%" align="center" bgcolor="#E1F1FB">Trabajador</th>
                      <th width="15%" align="center" bgcolor="#E1F1FB">Minutos Tarde  de Ingreso </th>
                      <th width="16%" align="center" bgcolor="#E1F1FB">Minutos Anticipados de Salida</th>
                      <th width="10%" align="center" bgcolor="#E1F1FB">N&uacute;mero de Faltas  </th>
                      <!--<th width="17%" align="center" bgcolor="#E1F1FB">Descuento</th>-->

                    </tr>
				<%do while not asistencia.eof%>                    
					<tr>
					<%i=i+1%>
                      <td align="center" bgcolor="#FFFFFF" ><%=i%></td>

                      <td align="left" bgcolor="#FFFFFF" >
					  <a href="AsistenciasIndividual.asp?codigo_per=<%=asistencia(0)%>&fechaInicio=<%=fechaInicio%>&fechaFin=<%=fechaFin%>"> <%=asistencia(1)%>
					  </a>
					  </td>
                      <td align="center" bgcolor="#FFFFFF" ><%=asistencia(2)%></td>
                      <td align="center" bgcolor="#FFFFFF" >
					  <%
					  if isnull(asistencia(3)) then
						Response.Write("-")
					  else
					  	Response.Write(asistencia(3))
					  end if
					  	%>
					</td>
                      <td align="center" bgcolor="#FFFFFF" ><%
					  if isnull(asistencia(4)) then
						Response.Write("-")
					  else
					  	Response.Write(asistencia(4))
					  end if
					  	%>
					</td>
                      <!--<td align="center" bgcolor="#FFFFFF" >
					  <%
					 if isnull(asistencia(5)) then
						Response.Write("-")
					  else
					  	Response.Write(round(asistencia(5),2))
					  end if
					  	%>					  
					</td>-->
                    </tr>


					<%
					asistencia.MoveNext
					loop%>
                  </table>
</div>		
<script type="text/javascript">
var sorter=new table.sorter("sorter");
sorter.init("sorter",1);
</script>		  
				  </td>
                </tr>
                <tr>
                  <td>&nbsp;</td>
                </tr>
              </table>
		    </form>

	     	</body>
</html>