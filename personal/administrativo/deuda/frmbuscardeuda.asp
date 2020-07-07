<!--#include file="../../../funciones.asp"-->
<% 
dim resultado,tiporesp,tipobus,param

tipoResp=request.querystring("tiporesp")
tipoBus=request.querystring("tipobus")
param=request.querystring("param")

resultado=false

if (trim(tipoResp) <> "N" and trim(param)<>"") then
	resultado=true
	alto="height=""98%"""
end if
'response.write tipoResp & "/" & tipoBus & "/" & param & "/" & resultado
%>
<html>
<head>
<title>Buscar Responsable de Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validardeuda.js"></script>
</head>

<body>
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="4" valign="top" class="usatTitulo" width="100%">Buscar Deudas</td>
    </tr>
    <tr> 
      <td  width="15%" height="5%">Tipo:</td>
      <td valign="top" width="20%" height="5%">
      <select name="cboTipoResp" id="cboTipoResp" class="cajas2">
      	  <!--
          <option value="N" selected>--- Seleccione Tipo de Responsable ---</option>
          -->
          <option value="E" <%=SeleccionarItem("cbo","E",tipoResp)%>>Estudiante</option>
          <!--
          <option value="T">Trabajador</option>
          <option value="O">Otros</option>
          -->
        </select></td>
      <td valign="top" width="28%" height="5%">&nbsp;</td>
      <td valign="top" width="30%" height="5%">&nbsp;</td>
    </tr>
    <tr> 
      <td  width="15%" height="5%">Buscar por:</td>
      <td valign="top" width="20%" height="5%"><select name="cboTipoBus" id="cboTipoBus" class="cajas2">
          <option value="CU" <%=SeleccionarItem("cbo","CU",tipoBus)%>>Codigo</option>
          <option value="AL" <%=SeleccionarItem("cbo","AL",tipoBus)%>>Apellidos y Nombres </option>
          <option value="DNI" <%=SeleccionarItem("cbo","DNI",tipoBus)%>>DNI</option>
        </select></td>
      <td valign="top" width="28%" height="5%">
      <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2" onkeyup="if(event.keyCode==13){BuscarCliente()}"></td>
      <td valign="top" width="30%" height="5%">
      <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" onClick="BuscarCliente()"></td>
    </tr>
    <%if resultado=true then%>
    <tr> 
      <td  width="100%" colspan="4" height="50%" valign="top">
      <%
	  
	  	if tiporesp="E" then
			Set objCon= Server.CreateObject("PryUSAT.clsDatAlumno")
				Set rsCliente= objCon.ConsultarAlumno("RS",tipoBus,param)
			set objCon= Nothing	
		end if
  
  		Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
		ArrEncabezados=Array("ID","Código Universitario","Apellidos y Nombres","Escuela Profesional","Estado")
		ArrCampos=Array("codigo_alu","codigouniver_alu","Alumno","nombre_cpf","estadodeuda_alu")
		ArrCeldas=Array("8%","15%","40%","40%","3%")
		ArrCamposEnvio=Array("codigo_alu","codigouniver_alu","Alumno")
		pagina="detalledeuda.asp?tipo=" & tipoResp

		call CrearRpteTabla(ArrEncabezados,rsCliente,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"")
	  %>  
      </td>
      </tr>
      <tr class="usatEtiqOblig">
        <td colspan="4" class="contornotabla" bgcolor="#C7E0CE" width="100%" height="5%">Relación de deudas pendientes</td></tr>
      <tr>
    	<td colspan="4" width="100%" height="40%" valign="top" class="contornotabla">
		<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija la usuario para ver sus deudas</span>
		<iframe id="fradetalle" src="detalledeuda.asp" height="100%" width="100%" border="0" frameborder="0">
		</iframe>
		</td>
	  </tr>
    <%end if%>
  </table>
</body>
</html>