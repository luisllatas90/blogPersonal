<!--#include file="../../../../funciones.asp"-->
<%

'Hector: 09/01/08 Esto lo uso para porgramas especiales.

session("codigo_cac") = 29 'Pra jalar la  programacion de cursos 2007-II
'session("codigo_tfu") = 1
'session("Usuario_bit") = "USAT\hzelada"


dim pagina
accion=request.querystring("accion")
codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
HayReg=false

if codigo_alu<>"" and trim(modo)="resultado" then
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		Set rsMatricula=Obj.Consultar("ConsultarAccesoMatriculaPorAsesor","FO",codigo_alu,session("codigo_cac"),0,"administrar/mantenimiento",session("codigo_tfu"))
		Obj.CerrarConexion
	Set obj=nothing
	
	if Not(rsMatricula.BOF AND rsMatricula.EOF) then
		pagina=rsMatricula("pagina")
		HayReg=true
		alto="99%"		
	end if	
end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<META HTTP-EQUIV="Last-modified:" CONTENT="11-April-07">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Mantenimiento de matrículas del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarfichamatricula.js"></script>
<script language="javascript">
var aplicacionBD = new Array()
function ObjAplicacion(ciclo,cm,fecha,tipo,obs,crd,emat)
	{
		this.ciclo=ciclo
		this.cm=cm
	  	this.fecha = fecha
	  	this.tipo = tipo
	  	this.obs = obs
	  	this.crd = crd
	  	this.emat=emat
	}
	
	function AgregarItemObjeto()
	{	
		<%
		if HayReg=true then
		Do while Not rsMatricula.EOF
			response.write "aplicacionBD[aplicacionBD.length] = new ObjAplicacion('" & rsmatricula("codigo_cac") & "','" & rsmatricula("codigo_mat") & "','" & rsmatricula("fecha_mat") & "','" & rsmatricula("tipo_mat") & "','" & rsmatricula("observacion_mat") & "','" & rsmatricula("preciocredito_mat") & "','" & rsmatricula("estado_mat") & "')" & vbNewLine & vbtab & vbtab
			rsMatricula.movenext
		Loop
		end if
		%>
	}

	function AbrirMatricula(i,pagina)
	{
		AgregarItemObjeto()
		var ciclo=aplicacionBD[i].ciclo
		var cm=aplicacionBD[i].cm
		var fecha=aplicacionBD[i].fecha
		var tipo=aplicacionBD[i].tipo
		var obs=aplicacionBD[i].obs
		var crd=aplicacionBD[i].crd
		var emat=aplicacionBD[i].emat
	
		txtestado_mat.value=emat
		txtciclo_mat.value=ciclo
		
		fradetalle.location.href=pagina + "?codigo_mat=" + cbocodigo_mat.value + "&codigo_cac=" + ciclo + "&descripcion_cac=" + cbocodigo_mat.options[cbocodigo_mat.selectedIndex].text
	}

</script>
</head>
<body onload="document.all.txtcodigouniver_alu.focus();<%if HayReg=true then%>AbrirMatricula('0','detallematricula.asp')<%end if%>">
<input type="hidden" id="txtestado_mat" value="P">
<input type="hidden" id="txtciclo_mat">
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="<%=alto%>">
	<%if (codigo_alu="") then%>
    <tr>
      <td width="50%" class="usattitulo" style="height: 4%">Mantenimiento de 
		Matrículas</td>
      <td width="50%" style="height: 4%">
     	<%call buscaralumno("matricula/mantenimiento/frmadminmatriculaPE.asp","../../")%>
      </td>
    </tr>
    <%else%>
    <tr>
      <td width="100%" class="etiqueta" valign="top" height="15%">
      <!--#include file="../../fradatos.asp"-->
      </td>
    </tr>
    <tr>
      <td width="100%" valign="top" height="3%" colspan="2" class="contornotabla">
			<table border="0" width="100%" style="border-collapse: collapse;">
				<%if HayReg=true then%>
				<tr bgcolor="#FFFFCC">
					<td width="20%" class="etiqueta">Ciclo Académico</td>
					<td width="15%">
					<%
					rsMatricula.movefirst
					Call llenarlista("cbocodigo_mat","ResaltarPestana2('0','','');AbrirMatricula(this.selectedIndex,'detallematricula.asp')",rsMatricula,"codigo_mat","descripcion_cac",0,"","","")
					%>	
					</td>
					<td width="65%" align="right" class="rojo">
					
					<%if pagina="" then
						if session("Ultimamatricula")<>session("descripcion_cac") then%>
						<input onClick="modificarmatricula('N','<%=session("codigo_pes")%>')" type="button" value="     Nueva Matrícula <%=session("descripcion_cac")%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px">
					<%	end if
					else
						response.redirect(pagina)
					end if%>
					</td>
				</tr>
				<%end if%>
			</table>
	  </td>
    </tr>
    <%if HayReg=true then%>
    <tr>
      <td valign="top" height="70%" colspan="2">
		<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="22%" onclick="ResaltarPestana2('0','','');AbrirMatricula(cbocodigo_mat.selectedIndex,'detallematricula.asp')">
                    Asignaturas Matriculadas</td>
					<td width="1%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('1','','');AbrirMatricula(cbocodigo_mat.selectedIndex,'vsthorario.asp')">
                    Horarios</td>
                    <td width="46%" class="bordeinf" align="right">
					&nbsp;
                    </td>
				</tr>
	  			<tr height="95%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>      
      </td>
    </tr>
	<%else%>
	<tr>
		<td>
			<input onClick="modificarmatricula('N','<%=session("codigo_pes")%>')" type="button" value="     Nueva Matrícula <%=session("descripcion_cac")%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px">
		</td>
	</tr>
	<%end if%>
	<%Set rsMatricula=nothing
end if%>
</table>
</body>
</html>