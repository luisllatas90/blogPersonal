<!--#include file="../../../../funciones.asp"-->
<%
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsSemestre=Obj.Consultar("ConsultarCicloAcademico","FO","CIN",0)
			Set rsCiclo=Obj.Consultar("ConsultarCicloAcademico","FO","CVP",0)
		Obj.CerrarConexion
	Set Obj=nothing
	
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Reporte de Tercio Estudiantil</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">

<script>
	function BuscarCuadroMeritos(modo)
	{
		if (cbocodigo_cpf.value=='-2'){
			alert('Por favor seleccione la Escuela Profesional')
		}
		else{
			mensaje.innerHTML="<b>Espere un momento por favor...</b>"
			var incluir=0
			//if (chkincluirreglamento.checked==true){incluir=1}
			if (modo=="B"){
				//alert(cbocodigoinicio_cac.value)
				fracuadromeritos.location.href="lstcuadromeritos.asp?incluir=" + incluir + "&codigo_cpf=" + cbocodigo_cpf.value + "&cicloingreso=" + cbocicloingreso.value + "&codigo_cacini=" + cbocodigoinicio_cac.value + "&codigo_cacfin=" + cbocodigofin_cac.value
			}
			else{
				document.all.cmdExportar.disabled=true
				var ncpf=cbocodigo_cpf.options[cbocodigo_cpf.selectedIndex].text
				var dcacfin=cbocodigofin_cac.options[cbocodigofin_cac.selectedIndex].text
			
				location.href="xlscuadromeritos.asp?incluir=" + incluir + "&nombre_cpf=" + ncpf + "&descripcion_cacfin=" + dcacfin + "&codigo_cpf=" + cbocodigo_cpf.value + "&cicloingreso=" + cbocicloingreso.value + "&codigo_cacini=" + cbocodigoinicio_cac.value + "&codigo_cacfin=" + cbocodigofin_cac.value
				mensaje.innerHTML=""
			}
				
		}
	}
	/*
	function IgualarCbo(ctrl)
	{
		cbocodigoinicio_cac.options[cbocodigoinicio_cac.selectedIndex].text=ctrl.options[ctrl.selectedIndex].text
		cbocodigoinicio_cac.value=cbocodigoinicio_cac.options[cbocodigoinicio_cac.selectedIndex].value
	}
	*/
</script>
</head>
<body>
<p class="usatTitulo">Reporte de Cuadro de Méritos</p>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" height="95%">
  <tr>
    <td width="15%" height="5%">Escuela Profesional</td>
    <td width="35%" height="5%"><%call escuelaprofesional("",0,"--Seleccionar la Escuela Profesional--")%></td>
    <td width="15%" height="5%">Semestre de Ingreso</td>
    <td width="30%" height="5%"><%call llenarlista("cbocicloingreso","",rsSemestre,"cicloIng_Alu","cicloIng_Alu","1999-I","","","")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" height="5%" align="right" colspan="4">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
    <td width="10%" height="5%" align="right">Desde</td>
    <td width="15%" height="5%"><%call llenarlista("cbocodigoinicio_cac","",rsCiclo,"codigo_cac","descripcion_cac",0,"","","")%></td>
    <td width="10%" height="5%" align="right">Hasta</td>
    <td width="15%" height="5%">
    <%rsCiclo.movefirst
   call llenarlista("cbocodigofin_cac","",rsCiclo,"codigo_cac","descripcion_cac",0,"","","")%></td>
    <td width="40%" height="5%" align="right">
    <input type="button" value="Buscar..." name="cmdBuscar" class="usatbuscar" onClick="BuscarCuadroMeritos('B')">
    <input type="button" value="Exportar" name="cmdExportar" class="excel" onClick="BuscarCuadroMeritos('E')" disabled=true></td>
      </tr>
    </table>
    </td>
  </tr>
  <!--
  <tr>
    <td width="30%" align="right" height="5%">
    <input type="checkbox" name="chkincluirreglamento" value="1" checked></td>
    <td width="100%" colspan="3" height="5%">Incluir Reglamento de Cuadro de Méritos</td>
  </tr>
  -->
  <tr>
    <td width="100%" colspan="4" height="70%">
    <iframe name="fracuadromeritos" height="100%" width="100%" class="contornotabla" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
  <tr>
    <td width="200%" height="5%" colspan="4" id="mensaje" class="rojo">&nbsp;</td>
  </tr>
</table>
</body>
</html>
<%
Set rsCiclo=nothing
Set rsCicloIngreso=nothing
%>