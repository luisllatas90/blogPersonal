
<!--#include file="../../../funciones.asp"-->
<%
codigo_amb=request.querystring("codigo_amb")
codigo_cac=request.querystring("codigo_cac")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_amb="" then codigo_amb="-2"

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		Set rsAmbiente=Obj.Consultar("ConsultarHorarios","FO",18,codigo_cac,0,0)
		if Not(rsAmbiente.BOF and rsAmbiente.EOF) AND (codigo_amb<>"" and codigo_cac<>"") then
			HayReg=true
		end if
	obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>HORARIOS POR AMBIENTE</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
function AbrirFicha(pagina,num)
{
	ResaltarPestana2(num,'','')
	fradetalle.location.href=pagina + "?codigo_amb=" + cbocodigo_amb.value + "&codigo_cac=" + cbocodigo_cac.value
}


function ImprimirFicha()
{
	var ambiente=cbocodigo_amb.options[cbocodigo_amb.selectedIndex].text
	fradetalle.document.title="HORARIOS DEL AMBIENTE: " + ambiente
	fradetalle.focus();
	fradetalle.print();
}
</script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="100%" colspan="2" class="usatencabezadopagina" height="5%">
    <p class="usattitulo">Consultar Horarios por Ambiente</td>
  </tr>
  <tr>
    <td width="27%" height="5%" class="etiqueta">Ciclo Académico</td>
    <td width="73%" height="5%">
    <%call llenarlista("cbocodigo_cac","actualizarlista('vsthorarioambiente.asp?codigo_cac=' + this.value + '&codigo_amb=' + cbocodigo_amb.value)",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
    </td>
  </tr>
  <tr>
    <td width="105" class="etiqueta">Ambiente</td>
    <td width="83%">
    <%call llenarlista("cbocodigo_amb","AbrirFicha('tblhorarioambiente.asp',0)",rsAmbiente,"codigo_amb","ambiente",codigo_amb,"Seleccione el Ambiente","","")%>
    </td>
  </tr>
</table>
<br>
<%if (HayReg=true) then%>
<table width="100%" height="80%" border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
	<tr id="trDetalle" >
  		<td width="100%" height="100%">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="5%">
					<td class="pestanaresaltada" id="tab" align="center" width="18%" onclick="AbrirFicha('tblhorarioambiente.asp',0)">
                    Vista Gráfica</td>
					<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="AbrirFicha('tblcursosambiente.asp',1)">
                    Vista detalle</td>
                    <td width="67%" height="10%" class="bordeinf" align="right">
					<input class="imprimir2" name="cmdImprimir" type="button" value="Imprimir" onclick="ImprimirFicha()" />
                    </td>
				</tr>
	  			<tr height="95%">
		    	<td width="100%" valign="top" colspan="4" class="pestanarevez">
					<iframe id="fradetalle" name="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  	</tr>
			</table>
  		</td>
  	</tr>
  </table>
<%if HayReg=true then%>
	<%elseif codigo_amb<>"-2" then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado Registros</h5>
<%end if

Set rsAmbiente=nothing
Set rsCiclo=nothing

end if
%>
</body>
</html>