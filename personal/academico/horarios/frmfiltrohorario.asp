<!--#include file="../../../funciones.asp"-->
<%
codigo_cpf=request.querystring("codigo_cpf")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsPlan=obj.Consultar("ConsultarPlanEstudio","FO","AC",codigo_cpf,0)
	obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Opciones de consulta</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="javascript">
function RetornarParametro()
	{
		var modo=5
		var Argumentos = window.dialogArguments;
		
		if (cbocodigo_pes.selectedIndex>0){
			modo=6
		}
		Argumentos.modo=modo
		Argumentos.descripcion_pes=cbocodigo_pes.options[cbocodigo_pes.selectedIndex].text
	   	Argumentos.codigo_pes=cbocodigo_pes.value
	   	Argumentos.ciclo_cur=cbociclo_cur.value
	   	Argumentos.GenerarVista()
		window.close();
	}

</script>
</head>
<body bgcolor="#F0F0F0">

<table width="100%" cellpadding="4">
	<tr>
		<td colspan="2" class="etiqueta">Elija el Plan de Estudio de la Escuela</td>
	</tr>
	<tr>
		<td align="right" colspan="2">
		<select name="cbocodigo_pes" multiple class="cajas2" style="height:150px">
		  <option value="<%=codigo_cpf%>" selected>[TODOS LOS PLANES DE ESTUDIO AGRUPADOS]</option>
		  <%if not(rsPlan.BOF and rsPlan.EOF) then
			  Do while not rsPlan.eof%>
  				<option value="<%=rsPlan("codigo_pes")%>"><%=rsPlan("descripcion_pes")%></option>
	  		<%rsPlan.movenext
		  	loop
  		end if%>
  	</select>
	</td>
	</tr>
	<tr>
		<td colspan="2" class="etiqueta">Ciclo de asignatura:
		<SELECT name="cbociclo_cur">
	    	<option value="0">TODOS</option>
	    	<%for i=1 to 12%>
	    	<option value="<%=i%>"><%=ConvRomano(i)%></option>
	    	<%next%>
	    </SELECT>
	    </td>
	</tr>
	<tr>
		<td align="right">
		<input name="cmdBuscar" type="button" value="   Búsqueda" class="buscar1" onclick="RetornarParametro()">		
		</td>
	</tr>
	</table>
</body>
</html>
