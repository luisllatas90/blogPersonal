<%
codigo_cur=request.querystring("codigo_cur")
codigo_pes=request.querystring("codigo_pes")
condicion=request.querystring("condicion")

on error resume next

if condicion="" then condicion=0

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsAlumnos= obj.Consultar("ConsultarEstimadoProgramacion","FO",2,codigo_pes,codigo_cur,condicion)
		
		if Not(rsAlumnos.BOF and rsAlumnos.EOF) then
			activo=true
		end if
    obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Estimado de estudiantes</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function VerEstudiantes(fila)
	{
		if (fila.length==undefined){
			if (fila.style.display=="none"){
				fila.style.display=""
			}
			else{
				fila.style.display="none"
			}
		}
		else{
			for (var i=0;i<fila.length;i++){
				var item=fila[i]
				
				if (item.style.display=="none"){
					item.style.display=""
				}
				else{
					item.style.display="none"
				}
			}
		}
	}
	
	function AbrirHistorial(codigo)
	{
	
		AbrirPopUp("../../clsbuscaralumno.asp?codigouniver_alu=" + codigo + "&mod=-1&pagina=estudiante/historial2.asp","450","750","no","no","yes")
		//AbrirPopUp('../../estudiante/historial2.asp?modo=resultado&codigo_alu=' + codigo_alu,'500','700','yes','yes','yes')
	}
	
	function TotalizarGrupos(grupos)
	{
		for (var i=1;i<=grupos;i++){
			var fila=document.all.item("fila" + i)
			var total=document.all.item("total" + i)
				
			if (fila.length==undefined){
				total.innerHTML=1
			}
			else{
				total.innerHTML=fila.length
			}
		}
		
		parent.document.all.cmdImprimir.disabled=false
	}
	
</script>
<style type="text/css">
.cicloingreso {
	border-width: 1px;
	border-color: #808080;
	border-style: solid none solid none;
	cursor: hand;
	background-color: #FFFFCC;
}
</style>
</head>
<body>
<%if activo=true then%>
<table cellpadding="3" id="tblestimado" style="border-collapse: collapse"  width="100%" border="0" bordercolor="gray" class="contornotabla">
<%
i=0
ciclo=""
cc=0
n=0
Do while not rsalumnos.EOF
	i=i+1
	n=n+1

	if trim(rsAlumnos("cicloIng_alu"))<>trim(ciclo) then
		ciclo=rsAlumnos("cicloIng_alu")
		cc=cc+1
		response.write "<tr onclick='VerEstudiantes(fila" & cc & ")'>"
		response.write "<td class='cicloingreso' colspan='3'>" & ciclo & "</td>"
		response.write "<td id='total" & cc & "'  align='right' class='cicloingreso'></td>"		
		response.write "</tr>"
		n=1
	end if
%>
	<tr class="piepagina" id="fila<%=cc%>" style="display:none">
	<td align="right" width="5%"><%=n%>&nbsp;</td>	
	<td align="center" width="15%"><%=rsAlumnos("codigouniver_alu")%></td>	
	<td width="75%"><%=rsAlumnos("alumno")%></td>
	<td align="right" width="5%">
	<img class="imagen" alt="Ver historial" src="../../../../images/menus/buscar_small12.gif" onclick="AbrirHistorial('<%=rsAlumnos("codigouniver_alu")%>')"></td>	
	</tr>
<%	
	rsalumnos.movenext
Loop		
%>
	<tr class="etiqueta">
	<td class="bordesup" colspan="3">TOTAL</td>	
	<td class="bordesup" align="right">[<%=i%>]</td>	
	</tr>
</table>
<script type="text/javascript" language="javascript">
	TotalizarGrupos('<%=cc%>')	
</script>
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp; No se han encontrado estudiantes que pueden llevar la asignatura</h5>
<%end if

Set rsalumnos=nothing
%>
</body>

</html>
<% 
    if Err.number <> 0 then
        response.Write Err.Description
    end if
%>