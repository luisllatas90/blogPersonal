<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
condicion=request.querystring("condicion")
veces=request.querystring("veces")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	
	if codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16 then
		Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
	else
		Set rsEscuela= obj.Consultar("consultaracceso","FO","ESC","Silabo",codigo_usu)
	end if

	if codigo_cac<>"-2" and codigo_cpf<>"-2" then
		SET rsAlumnos=obj.Consultar("ConsultarSeguimientoNotas","FO",2,codigo_cpf,codigo_cac,condicion,veces)

		if Not(rsAlumnos.BOF and rsAlumnos.EOF) then
			activo=true
			alto="height=""100%"""
			
			Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
			
			ArrEncabezados=Array("Código","Apellidos y Nombres","Nro de asignaturas")
			ArrCampos=Array("codigouniver_alu","alumno","total")
			ArrCeldas=Array("15%","40%","5%","20%","5%")
			'pagina="misdatos.asp?tipo=" & tipoResp			
		end if
	end if
    obj.CerrarConexion
Set obj=nothing

'oncontextmenu="return false"
%>
<html>
<head>
<title>Veces matriculadas</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function ConsultarDatos()
	{
		if (cbocodigo_cpf.value=="-2"){
			alert("Debe elegir la Escuela Profesional")
			cbocodigo_cpf.focus()
			return(false)
		}
		pagina="../academico/notas/consultapublica/lstcondicioncurso.asp?codigo_cpf=" + cbocodigo_cpf.value + "&codigo_cac=" + cbocodigo_cac.value + "&condicion=" + cbocondicion.value + "&veces=" + cboveces.value
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
</script>
<style type="text/css">
.style1 {
	cursor: hand;
	vertical-align: middle;
}
</style>
</head>
<body >
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="5" class="usattitulo">Estudiantes según nro. de asignaturas 
	aprobadas/desaprobadas</td>
</tr>
<tr>
	<td height="3%" style="width: 20%">Ciclo Académico:</td>
	<td height="3%" style="width: 15%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
	</td>
	<td height="3%" style="width: 20%" align="right">Escuela Profesional:</td>
	<td height="3%" style="width: 65%">
	<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","")%>
	</td>
	<td height="3%" style="width: 10%" align="right">
    &nbsp;</td>
</tr>
  <tr class="rojo">
	<td height="3%">&nbsp;</td>
	<td height="3%">&nbsp;</td>
	<td height="3%" style="width: 20%" align="right">Estudiantes que </td>
	<td height="3%" style="width: 65%">
    <select name="cbocondicion">
    <option value="D" <%=SeleccionarItem("cbo",condicion,"D")%>>Desaprobaron</option>
	<option value="A" <%=SeleccionarItem("cbo",condicion,"A")%>>Aprobaron</option>
	</select><select name="cboveces">
    <%for i=1 to 10%>
	<option value="<%=i%>" <%=SeleccionarItem("cbo",veces,i)%>><%=i%> asignatura(s)</option>
	<%next%>
	</select>
    <img alt="Buscar cursos programados" src="../../../../images/buscar.gif" class="style1" onclick="ConsultarDatos()" width="58" height="17"></td>
	<td height="3%" style="width: 10%" align="right">
    &nbsp;</td>
</tr>
<%if activo=true then%>
<tr valign="top">
    <td height="44%" width="100%" colspan="5">
	<%call CrearRpteTabla(ArrEncabezados,rsAlumnos,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"")%>
    </td>
    </tr>    
	<%end if%>		
	</table>
	<%if activo<>true and codigo_cac<>"-2" and codigo_cpf<>"-2" then%>
		<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp; <span><u>Importante</u></span>:<br>
		No se han encontrado registros según los criterios seleccionados</h5>
	<%end if%>
</body>
</html>
