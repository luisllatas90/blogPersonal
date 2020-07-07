<!--#include file="../../../funciones.asp"-->
<% 
dim param

tipobus=request.querystring("tipobus")
param=request.querystring("param")
resultado=request.querystring("resultado")
modulo=request.querystring("mod")

if (resultado="S" and trim(param)<>"") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsAlumno= obj.Consultar("EVE_ConsultarAlumnosPorModulo","FO",tipoBus,modulo,param)
	obj.CerrarConexion
	set obj= Nothing	
  
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("ID","Código","Apellidos y Nombres","Escuela Profesional","Estado Actual","Tiene Deuda")
	ArrCampos=Array("codigo_alu","codigouniver_alu","Alumno","nombre_cpf","estadoactual","estadodeuda")
	ArrCeldas=Array("5%","15%","35%","20%","10%","10%")
	ArrCamposEnvio=Array("codigo_alu","codigouniver_alu")
	pagina="misdatosprof.asp?tipo=" & tipoResp

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
<script language="JavaScript">
function BuscarAlumnoProf(modulo)
{
	if (txtParam.value==""){
		alert("Especifique el parámetro de búsqueda")
		txtParam.focus()
		return(false)
	}
	
	location.href="lstalumnosprof.asp?resultado=S&param=" + txtParam.value + "&tipobus=" + cboTipoBus.value + "&mod=" + modulo
}
</script>
</head>

<body onload="document.all.txtParam.focus()">
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="4" valign="top" class="usatTitulo" width="100%">
      <%   	
		response.write "Búsqueda de estudiantes de Programas de Profesionalización - Actualizar P.P"
      %>
      
      </td>
    </tr>
    <tr> 
      <td  width="15%" height="5%">Buscar por:</td>
      <td valign="top" width="20%" height="5%"><select name="cboTipoBus" id="cboTipoBus" class="cajas2">
          <option value="1" <%=SeleccionarItem("cbo","1",tipoBus)%>>Apellidos y Nombres</option>
          <option value="0" <%=SeleccionarItem("cbo","0",tipoBus)%>>Codigo</option>
        </select></td>
      <td valign="top" width="28%" height="5%">
      <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2" onkeyup="if(event.keyCode==13){BuscarAlumnoProf('<%=modulo%>')}"></td>
      <td valign="top" width="30%" height="5%">
        <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" onClick="BuscarAlumnoProf('<%=modulo%>')">
      </td>
    </tr>
    <%if resultado="S" then%>
    <tr id="trLista">
  		<td width="100%" height="35%" colspan="4">
  		<%
  		call CrearRpteTabla(ArrEncabezados,rsAlumno,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"ResaltarPestana2('0','','')")   
  		%>
  		</td>
	</tr>
	<tr id="trDetalle" >
  		<td width="100%" height="65%" colspan="4">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('0','','');AbrirFicha('misdatosprof.asp',this)">
                    Datos del Estudiante</td>
				</tr>
	  			<tr height="92%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">
					<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el estudiante que desea verificar sus datos</span>
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>
  		</td>
  	</tr>
	<%end if%>
  </table>
</body>
</html>