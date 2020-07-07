<!--#include file="../../../../funciones.asp"-->
<%
codigo_ins=request.querystring("codigo_ins")
if codigo_ins="" then
codigo_ins=-1
end if
codigo_ped=request.querystring("codigo_ped")
codigo_eped=request.querystring("codigo_eped")
codigo_cco=request.querystring("codigo_cco")
tipobandeja=request.querystring("tipobandeja")

if codigo_ped<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsInstancia=Obj.Consultar("BI_ConsultarInstancia","FO","TO",codigo_ped)
			Set rsDetalle=Obj.Consultar("BI_ConsultarPedidos","FO","HI",codigo_ins,codigo_ped)
		Obj.CerrarConexion
	Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>DETALLE DE PEDIDO</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function MarcarItem(item)
	{
		var cant=document.getElementById("cant" + item)
		var mensaje=document.getElementById("mensaje" + item)
	
		VerificaCheckMarcados(frmDetalle.chkcodigo_dpe,frmDetalle.cmdAprobar)
		if (cant.disabled==true){
			mensaje.style.display=""
			cant.disabled=false
		}
		else{
			mensaje.style.display="none"		
			cant.disabled=true
		}
	}
	
	function AprobarPedido(modo)
	{
		if (confirm("Está completamente que desea " + modo)==true){
			frmDetalle.submit()
		}
	}
	
	function MostrarLibros()
	{
		location.href="detallepedido.asp?codigo_ins=<%=codigo_ins%>&codigo_ped=<%=codigo_ped%>&codigo_cco=<%=codigo_cco%>&tipobandeja=<%=tipobandeja%>&codigo_eped=" + frmDetalle.cboestado.value
	}
</script>
<style type="text/css">
<!--
.style1 {
	color: #000000;

}
.style2 {
	color: #000000;
	font-weight: bold;
}
-->
</style>
</head>
<body style="background-color: #EEEEEE">
<form name="frmDetalle" method="post" action="procesar.asp?accion=aprobarpedido&codigo_ped=<%=codigo_ped%>">
<table width="100%">

	<tr>
	  <td colspan="3"><table border="0" align="center" cellpadding="3" cellspacing="0">
        <tr>
		<%
		do while not rsInstancia.EOF
		if codigo_ins=-1 then
			instancia=rsInstancia("insActual")
		else
			instancia=codigo_ins
		end if
		%>
          <td width="100" align="center" <% if cstr(instancia) = cstr(rsInstancia("codigo_ins")) then %>  bgcolor="#FFFFCC" <%else%> bgcolor="#e1f1fb" <%end if%> class="contornotabla style1" >
		  <a href="historial.asp?codigo_ins=<%=rsInstancia("codigo_ins")%>&codigo_ped=<%=codigo_ped%>">
		  <%Response.Write(rsInstancia("descripcion_ins"))%>	  
		  </a>
		  <%rsInstancia.MoveNext%>
	    </td>
        <%loop%>
		</tr>
        <tr>
          <td align="center" >&nbsp;</td>
        </tr>
		
      </table>
	  </td>
    </tr>
</table>
<%
Do while not rsDetalle.EOF
	i=i+1
%>
<table style="width: 100%" bgcolor="white" class="contornotabla">
	<tr>
		<%if codigo_ins>0 and tipobandeja="E" then%>		
		<td style="width: 5%" rowspan="9" valign="top" bgcolor="#FFFFCC">
		<input name="chkcodigo_dpe" type="checkbox" value="<%=rsDetalle("codigo_dpe")%>" onClick="MarcarItem(<%=i%>)" />
		</td>
		<%end if%>
		<td style="width: 15%" class="usatLinkCeldaGris">Nº</td>
		<td style="width: 75%"><%=i%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Procedencia</td>
		<td style="width: 75%"><%=rsDetalle("tipo_dpe")%>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Título</td>
		<td style="width: 75%"><span class="style2"><%=rsDetalle("titulo")%></span></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Autor</td>
		<td style="width: 75%"><span class="style2"><%=rsDetalle("nombreautor")%></span></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Editorial</td>
		<td style="width: 75%"><%=rsDetalle("nombreeditorial")%>
		
		. edición: <%=rsDetalle("edicion")%>
		</td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Asignatura</td>
		<td style="width: 75%" class="bordesup"><%=rsDetalle("nombre_cur")%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Justificación</td>
		<td style="width: 75%"><%=rsDetalle("justificacion_jpe")%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Cantidad</td>
		<td style="width: 75%">	<span class="style2"><%=rsDetalle("cantidad_dpe")%></span>
		</td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Tema de Inv.</td>
		<td style="width: 75%"><%=rsDetalle("tema_dpe")%></td>
	</tr>
</table>
<br>
<%
	rsDetalle.movenext
Loop

set rsDetalle=nothing
%>
</form>
</body>

</html>
<%end if%>