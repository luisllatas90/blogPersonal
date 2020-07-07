<!--#include file="../../../funciones.asp"-->
<% 
dim param

param=request.querystring("param")
resultado=request.querystring("resultado")

if (resultado="S" and trim(param)<>"") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
		Set rsAlumno= obj.Consultar("ConsultarEgresado","FO",22,param,0,0)
	set obj=Nothing	
  
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("ID","Código","Apellidos y Nombres","Escuela Profesional","Condición")
	ArrCampos=Array("codigo_alu","codigouniver_alu","Alumno","nombre_cpf","condicionEgresado")
	ArrCeldas=Array("8%","15%","40%","40%","3%")
	ArrCamposEnvio=Array("codigo_alu","codigouniver_alu")
	pagina="misdatos.asp?tipo=" & tipoResp

	if Not(rsAlumno.BOF and rsAlumno.EOF) then
		alto="height=""98%"""
		resultado="S"
	else
		resultado="N"
	end if
end if
%>
<html>
<head>
<title>Buscar Responsable de Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaralumno.js"></script>
<script type="text/javascript" language="javascript">
	function BuscarEgresado()
	{
	if (txtParam.value==""){
		alert("Especifique el parámetro de búsqueda")
		txtParam.focus()
		return(false)
	}
	location.href="lstegresado.asp?resultado=S&param=" + txtParam.value
	}
</script>
</head>

<body onload="document.all.txtParam.focus()">
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="3" valign="top" class="usatTitulo" width="100%">
		Búsqueda de estudiantes Egresados</td>
    </tr>
    <tr> 
      <td  width="15%" height="5%">Apellidos / Nombres</td>
      <td valign="top" width="28%" height="5%">
      <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2" onkeyup="if(event.keyCode==13){BuscarEgresado()}"></td>
      <td valign="top" width="30%" height="5%">
      <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" onClick="BuscarEgresado()"> </td>
    </tr>
    <%if resultado="S" then%>
    <tr id="trLista">
  		<td width="100%" height="35%" colspan="3">
  		<%
  		call CrearRpteTabla(ArrEncabezados,rsAlumno,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"ResaltarPestana2('0','','')")   
  		%>
  		</td>
	</tr>
	<tr id="trDetalle" >
  		<td width="100%" height="65%" colspan="3">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('0','','');AbrirFicha('misdatos.asp',this)">
                    Datos Personales</td>
					<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('1','','');AbrirFicha('historial.asp',this)">
                    Historial Académico</td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
                    <td width="10%" height="10%" class="bordeinf" align="right">
                    <%if session("codigo_tfu")=53 then%>
		   			<img border="0" src="../../../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()">
					<img border="0" src="../../../images/editar.gif" style="cursor:hand" ALT="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('frmactualizardatos.asp',this)">
                    <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')">
                    <%end if%>
                    </td>
				</tr>
	  			<tr height="92%">
		    	<td width="100%" valign="top" colspan="6" class="pestanarevez">
					<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el estudiante que desea verificar sus datos</span>
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>
  		</td>
  	</tr>
	<%else%>
	<tr><td colspan="3" class="usatsugerencia">    &nbsp;&nbsp;&nbsp;&nbsp;
		<strong>No se ha encontrado el estudiante EGRESADO</strong></td></tr>
	<%end if%>
  </table>
</body>
</html>