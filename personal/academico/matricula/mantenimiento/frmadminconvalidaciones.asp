<!--#include file="../../../funciones.asp"-->
<%
codigoorigen_cac=request.querystring("codigoorigen_cac")
codigodestino_cac=request.querystring("codigodestino_cac")
cicloIng_Alu=request.querystring("cicloIng_Alu")
tipoConvalidacion=request.querystring("tipoConvalidacion")
modo=request.querystring("modo")

if modo="R" then
	activo=true
	alto="height=""99%"""
end if

Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	Set rsCicloIngreso=Obj.ConsultarCicloAcademico("RS","CIN",codigo_cpf)
	Set rsCac=Obj.ConsultarCicloAcademico("RS","TO","")
Set Obj=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Administrar convalidaciones por Bloque</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarmodalidadmatricula.js"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="4" height="5%">Actualizar convalidaciones de un 
    Ciclo a otro..</td>
  </tr>
  <tr>
    <td width="27%" height="3%">Semestre de Ingreso de Alumno</td>
    <td width="25%" height="3%">
    <%call llenarlista("cbocicloingreso","",rsCicloIngreso,"cicloIng_Alu","cicloIng_Alu",cicloIng_Alu,"","","")%>
	</td>
    <td width="26%" height="3%">Tipo de convalidación</td>
    <td width="22%" height="3%">
    <select name="cboModalidad" class="cajas2">
			<option value="CNVPR" <%=SeleccionarItem("cbo",tipoConvalidacion,"CNVPR")%>>Convalidación por Escuela Pre</option>
			<option value="CNVTR" <%=SeleccionarItem("cbo",tipoConvalidacion,"CNVTR")%>>Convalidación por Traslado</option>
			
			<!--
			<option value="EXSUF" <%=SeleccionarItem("cbo",modalidad,"EXSUF")%>>Exámen de Suficiencia</option>
			-->
			</select></td>
  </tr>
  <!--
  <tr>
    <td width="27%" height="3%">Escuela Profesional</td>
    <td width="73%" height="3%" colspan="3"><%call escuelaprofesional("",codigo_cpf,"--TODAS--")%>&nbsp;</td>
  </tr>
  -->
  <tr>
    <td width="27%" height="3%" class="azul">Ciclo Origen, en el que se registró la convalidación</td>
    <td width="25%" height="3%"><%call llenarlista("cboCicloOrigen","",rsCac,"codigo_cac","descripcion_cac",codigoorigen_cac,"","","")%></td>
    <td width="24%" height="3%" class="rojo">Ciclo Destino, al que se desea copiar las convalidaciones</td>
    <td width="24%" height="3%"><%rsCac.movefirst:call llenarlista("cboCicloDestino","",rsCac,"codigo_cac","descripcion_cac",codigodestino_cac,"","","")%></td>
  </tr>
  
  <tr>
    <td width="27%" height="3%">&nbsp;</td>
    <td width="73%" height="3%" colspan="3"><input onclick="ConsultarConvalidaciones()" type="button" value="    Consultar..." name="cmdConsultar" class="usatbuscar">
    <input onclick="EnviarCambioCicloConvalidacion()" type="button" value="Guadar..." name="cmdGuardar" class="usatguardar"></td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td width="100%" colspan="4" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="3%" height="3%"><input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck()" value="0"></td>
        <td width="3%" height="3%">ID</td>
        <td width="10%" height="3%">Código</td>
        <td width="30%" height="3%">Alumno</td>
        <td width="30%" height="3%">Escuela Profesional</td>
        <td width="25%" height="3%">Modalidad de Ingreso</td>
      </tr>
      <tr>
        <td width="100%" colspan="6">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%	Set Obj=Server.CreateObject("PryUSAT.clsDatMatricula")
				Set rsAlumnos= Obj.ConsultarMatricula("RS","18",cicloIng_Alu,tipoConvalidacion,codigoorigen_cac)
			Set obj=nothing
			i=0:n=0:p=0
			Do while not rsAlumnos.eof
				i=i+1
		%>
			<tr height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td width="4%" align="center"><input type="checkbox" name="chk" id="chk<%=i%>" onClick="pintarfilamarcada(this)" value="<%=rsAlumnos("codigo_alu")%>"></td>
			<td align="center" width="5%"><%=i%>&nbsp;</td>
			<td width="12%"><%=rsAlumnos("codigouniver_alu")%>&nbsp;</td>
			<td width="30%"><%=rsAlumnos("alumno")%>&nbsp;</td>
			<td width="30%"><%=rsAlumnos("nombre_cpf")%>&nbsp;</td>
			<td width="25%"><%=rsAlumnos("nombre_min")%>&nbsp;</td>
			</tr>
				<%rsAlumnos.movenext
			loop
			set rsAlumnos=nothing
		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr>
    	<td width="188%" height="5%" bgcolor="#E6E6FA" align="right" colspan="6"><span class="azul">&nbsp;&nbsp;&nbsp;&nbsp; TOTAL DE ALUMNOS: <%=i%></b></span></td>
	  </tr>
      </table>
  </td>
  </tr>
  <%end if%>
</table>
</body>
</html>