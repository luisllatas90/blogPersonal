<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Adjuntar archivos...</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
 <script language="JavaScript" src="../../../../private/funciones.js"></script>
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo1 {
	color: #000000;
	font-weight: bold;
}
-->
</style>

</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script>
function Eliminar(codigo_dap,codigo_atp){

location.href="actividadesPropuesta.asp?eliminar=si&codigo_dap="+codigo_dap+"&codigo_atp="+codigo_atp
//frmActividades.submit()
}
</script>

<body topmargin="0" leftmargin="0" rightmargin="0">

<%


codigo_prp=Request.QueryString("codigo_prp")
codigo_dap=Request.QueryString("codigo_dap")
codigo_atp=Request.QueryString("codigo_atp")
eliminar=Request.QueryString("eliminar")

if eliminar="si" then
				Set objAct1=Server.CreateObject("PryUSAT.clsAccesoDatos")
				objAct1.AbrirConexion
				
				objAct1.Ejecutar "EliminarActividadesPropuesta",false,codigo_atp 
				objAct1.CerrarConexion
				set objAct1=nothing			
end if
%>
<%			if codigo_Dap ="" then
				codigo_Dap=0
			end if	
				Set objAct=Server.CreateObject("PryUSAT.clsAccesoDatos")
				objAct.AbrirConexion
				set RsActividades=objAct.Consultar("ConsultarActividadesPropuesta","FO","ES",codigo_dap,0)
				objAct.CerrarConexion
				set objAct=nothing					
				
			%>	

		<form id="frmActividades" name="frmActividades" >
		  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td width="100%" valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
                <tr>
                  <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo1">Descripci&oacute;n de la Actividad</span></td>
                  <td width="12%" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo1">Fecha Inicio </span></td>
                  <td width="12%" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo1">Fecha Fin </span></td>
                  <td width="12%" align="center" bgcolor="#E1F1FB" class="bordeinf Estilo1">Costo Aprox. </td>
                  <td width="21%" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo1">Observaci&oacute;n</span></td>
                  <td align="center" bgcolor="#E1F1FB" class="bordeinf">&nbsp;</td>
                </tr>
				<% do while not RsActividades.EOF%>
                <tr><%i=i+1%>
                  <td width="2%" bgcolor="#FFFFFF">&nbsp;<%=i%>.-</td>
                  <td width="37%" bgcolor="#FFFFFF">&nbsp;<%=RsActividades("Descripcion_Atp")%></td>
                  <td bgcolor="#FFFFFF">&nbsp;<%=RsActividades("fechaInicio_atp")%></td>
                  <td bgcolor="#FFFFFF">&nbsp;<%=RsActividades("fechaFin_atp")%></td>
                  <td bgcolor="#FFFFFF"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="79%" align="right"><%=FormatNumber(RsActividades("costo_atp"),2)%></td>
                      <td width="21%">&nbsp;</td>
                    </tr>
                  </table></td>
                  <td bgcolor="#FFFFFF"><%=RsActividades("observacion_atp")%></td>
                  <td width="4%" align="center" bgcolor="#FFFFFF"><img src="../../../../images/menus/edit_remove.gif" alt="Eliminar Actividad" width="15" height="15" border="0" style="cursor:hand" onClick="Eliminar('<%=codigo_dap%>','<%=RsActividades("codigo_atp")%>')"></td>
                </tr>
				<%
				RsActividades.MoveNext
				loop%>
              </table></td>
            </tr>
          </table>
</form>
</body>
</html>

