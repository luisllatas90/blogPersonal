<!--#include file="../../../../funciones.asp"-->
<%
	codigo_cac=request.querystring("codigo_cac")
	'if codigo_cac="" then codigo_cac=session("codigo_cac")

	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			'Set rsSemestre=Obj.Consultar("ConsultarCicloAcademico","FO","CIN",0)
			Set rsCiclo=Obj.Consultar("ConsultarCicloAcademico","FO","CVP",0)
		Obj.CerrarConexion
	Set Obj=nothing
	
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Reporte de Tercio Estudiantil</title>
<script language="javascript">
	function BuscarCuadroMeritos(modo)
	{
		if (cbocodigo_cpf.value=='-2'){
			alert('Por favor seleccione la Escuela Profesional')
		}
		else{
			mensaje.innerHTML="<b>Espere un momento por favor...</b>"
			var incluir=0
			var cicloIngreso=cboinicio_cac.options[cboinicio_cac.selectedIndex].text
			
			//if (chkincluirreglamento.checked==true){incluir=1}
			if (modo=="B"){
			    //fracuadromeritos.location.href="lstcuadromeritos.asp?incluir=" + incluir + "&codigo_cpf=" + cbocodigo_cpf.value + "&cicloingreso=" + cicloIngreso + "&codigo_cacini=" + cboinicio_cac.value + "&codigo_cacfin=" + cbofin_cac.value + "&tipo=" + cbotipo.value
			    fracuadromeritos.location.href = "lstcuadromeritos.asp?incluir=" + incluir + "&codigo_cpf=" + cbocodigo_cpf.value + "&cicloingreso=" + cicloIngreso + "&codigo_cacini=" + cboinicio_cac.value + "&codigo_cacfin=" + "0" + "&tipo=" + cbotipo.value
			}
			else{
				document.all.cmdExportar.disabled=true
				var ncpf=cbocodigo_cpf.options[cbocodigo_cpf.selectedIndex].text
				var dcacfin=cbocodigofin_cac.options[cbocodigofin_cac.selectedIndex].text
			
				location.href="xlscuadromeritos.asp?incluir=" + incluir + "&nombre_cpf=" + ncpf + "&descripcion_cacfin=" + dcacfin + "&codigo_cpf=" + cbocodigo_cpf.value + "&cicloingreso=" + cicloIngreso + "&codigo_cacini=" + cboinicio_cac.value + "&codigo_cacfin=" + cbofin_cac.value + "&tipo=" + cbotipo.value
				mensaje.innerHTML=""
			}
				
		}
	}
</script>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>
<body>
<p class="usatTitulo">Reporte de Cuadro de Méritos de Estudiantes por Escuela Profesional</p>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="95%" align="right">
  <!--
  <tr>
    <td width="30%" align="right" height="5%">
    <input type="checkbox" name="chkincluirreglamento" value="1" checked></td>
    <td width="100%" colspan="3" height="5%">Incluir Reglamento de Cuadro de Méritos</td>
  </tr>
  -->
  <tr height="5%">
    <td width="25%" align="right">Escuela Profesional:</td>
    <td width="50%" align="right" colspan="2">
    <%call escuelaprofesional("",0,">> Seleccione la Escuela Profesional<<")%>
    </td>
    <td width="25%">&nbsp;</td>
  </tr>
  <tr height="5%">
    <td width="25%" colspan="2" style="width: 50%" align="right">Ciclo de 
	<strong>Ingreso</strong> 
	del estudiante:</td>
    <td width="25%">
    <%call llenarlista("cboinicio_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"Ciclo de Ingreso","","")%>
    </td>
    <td width="25%">
    &nbsp;
    </td>
  </tr>
  <!--<tr height="5%">
    <td width="25%" colspan="2" style=width 50%" align="right">&nbsp;</td>
    <td width="25%">
    <%'rsCiclo.movefirst
   ' call llenarlista("cbofin_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"Ciclo de Egreso","","")%>
    </td>
    <td width="25%">
    &nbsp;</td>
  </tr>
  -->
  <tr height="5%">
    <td width="25%">&nbsp;</td>
    <td width="25%" align="right">Tipo de Reporte:</td>
    <td width="25%">
    <select name="cbotipo" class="cajas2">
	<option value="T" selected="">Tercio Estudiantil</option>
	<option value="Q">Quinto estudiantil</option>
	</select></td>
    <td width="25%">
    &nbsp;
    <input type="button" value="Buscar..." name="cmdBuscar" class="buscar2" onClick="BuscarCuadroMeritos('B')"></td>
  </tr>
  <tr height="85%">
    <td width="100%" colspan="4" >
    <iframe name="fracuadromeritos" height="100%" width="100%" class="contornotabla" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
  <tr  height="3%">
    <td width="200%" colspan="4" id="mensaje" class="rojo">&nbsp;</td>
  </tr>
</table>
</body>
</html>
<%
Set rsCiclo=nothing
Set rsCicloIngreso=nothing
%>