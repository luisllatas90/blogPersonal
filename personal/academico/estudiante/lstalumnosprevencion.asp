<!--#include file="../../../funciones.asp"-->
<% 
dim param

'tipobus=request.querystring("tipobus")
tipobus=0
param=request.querystring("param")
resultado=request.querystring("resultado")
modulo=request.querystring("mod")

if (resultado="S" and trim(param)<>"") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsAlumno= obj.Consultar("ACAD_BuscarAlumno","FO",param)
	obj.CerrarConexion
	set obj= Nothing	
  
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("ID","Código","Apellidos y Nombres","Escuela Profesional","Estado Actual","Tiene Deuda")
	ArrCampos=Array("codigo_alu","codigouniver_alu","Alumno","nombre_cpf","estadoactual","estadodeuda")
	ArrCeldas=Array("5%","15%","35%","20%","10%","10%")
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
<title>Buscar Estudiantes</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaralumno.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>
</head>

<body onload="document.all.txtParam.focus()">
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="4" valign="top" class="usatTitulo" width="100%">
      <%
      select case modulo
      	case 1: response.write "Búsqueda de estudiantes de Escuela Pre-Universitaria"
      	case 2: response.write "Búsqueda de estudiantes de PreGrado"      	
		case 3: response.write "Búsqueda de estudiantes de Programas de Profesionalización"
		case 4: response.write "Búsqueda de estudiantes de Complementarios/Idiomas"
		case 5: response.write "Búsqueda de estudiantes de la Escuela de PostGrado"		
		case 6: response.write "Búsqueda de estudiantes de Educación Contínua"
		case 7: response.write "Búsqueda de estudiantes de Titulación"
	  end select		      
      %>
      
      </td>
    </tr>
    <tr> 
      <td  width="25%" height="5%">Apellidos y Nombres/DNI/Cod. Univ.</td>
      <td valign="top" width="40%" height="5%" colspan="2">
        <!--
        <select name="cboTipoBus" id="cboTipoBus" class="cajas2">
          <option value="1" <%=SeleccionarItem("cbo","1",tipoBus)%>>Apellidos y Nombres</option>
          <option value="0" <%=SeleccionarItem("cbo","0",tipoBus)%>>Codigo</option>
        </select>
        -->      
      <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2" onkeyup="if(event.keyCode==13){BuscarAlumnoPrevencion('<%=modulo%>')}"/>
      </td>
      <td valign="top" width="30%" height="5%">
      <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" onClick="BuscarAlumnoPrevencion('<%=modulo%>')">
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
					<td class="pestanaresaltada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('0','','');AbrirFicha('misdatos.asp',this)">
                    Datos del Estudiante</td>					
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			<td class="pestanabloqueada" id="tab" align="center" width="13%" onclick="ResaltarPestana2('4','','');AbrirFicha('horario.asp',this)">
                    Horarios</td>
                    <td width="10%" height="10%" class="bordeinf" align="right">
		   			<img border="0" src="../../../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()">
                    <img border="0" src="../../../images/editar.gif" style="cursor:hand" ALT="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('frmactualizardatospe.asp',this)">
		            <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')">
                    </td>
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