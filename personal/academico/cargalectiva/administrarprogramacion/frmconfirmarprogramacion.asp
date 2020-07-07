<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigoElegido_pes=request.querystring("codigoElegido_pes")
arrcodigo_cur=verificacomaAlfinal(request.form("arrCursosMarcados"))
arrcodigo_pes=verificacomaAlfinal(request.form("arrPlanesMarcados"))
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Confirmar Programación</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/calendario.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarprogramacion.js"></script>
<style type="text/css">
.lblcurso {
	background-color: #E7E6C9;
}
</style>
</head>
<body style="background-color: #F2F2F2">

<p class="usatTitulo">Asignaturas seleccionadas para programación</p>
<form name="frmlistacursos" method="POST" action="procesar.asp?accion=agregarcursoprogramado&codigo_cac=<%=codigo_cac%>&codigoElegido_pes=<%=codigoElegido_pes%>">
<input name="txtcodigo_pes" type="hidden" value="<%=arrcodigo_pes%>">
<input name="txtcodigo_cur" type="hidden" value="<%=arrcodigo_cur%>">

<table width="100%" height="93%" cellpadding="2">
	<tr height="5%">
		<td width="100%" colspan="2" class="rojo">
		<strong>IMPORTANTE:</strong> &nbsp;Si 
		Ud. desea puede marcar 
		las asignaturas equivalentes de planes de estudio anteriores, para programarlas.			
		</td>
	</tr>	
	<tr height="90%">
		<td width="100%" colspan="2" class="contornotabla_azul">
		<div id="listadiv" style="height:100%;padding:3">
		<%
		
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
		
			'********************************************************
			'Convertir a array y recorrerlo
			'********************************************************
			arrcodigo_cur=split(arrcodigo_cur,",")
			arrcodigo_pes=split(arrcodigo_pes,",")
						
			for i=0 to Ubound(arrcodigo_cur)
				mostrar="style='display:none'"
				ID=trim(arrcodigo_cur(i))
				codigo_pes=trim(arrcodigo_pes(i))
				
				Set rsCurso=obj.Consultar("ConsultarCursoProgramado","FO",11,codigo_pes,ID,codigo_cac,0)
				'Set rsDpto=obj.Consultar("ConsultarCargaAcademicaDpto","FO","5",0,0,0)
				
				'********************************************************
				'No mostrar equivalencias de cursos complementarios

				if codigoElegido_pes=1 then codigo_pes=0
				'********************************************************
								
				Set rsEquivalente=obj.Consultar("ConsultarCursoEquivalente","FO","PRG",codigo_pes,ID,codigo_cac)
				
				If Not(rsEquivalente.BOF and rsEquivalente.EOF) then
					HayReg=true
				End if
				
				fechaini_cac=Formatdatetime(rsCurso("fechaini_cac"),2)
				fechafin_cac=Formatdatetime(rsCurso("fechafin_cac"),2)
				fecharetiro_cac=Formatdatetime(rsCurso("fecharetiro_cac"),2)
				vacantes_cac=rsCurso("vacantes_cac")
				soloPrimerCiclo=rsCurso("soloPrimerCiclo_cup")		
				If trim(rsCurso("mododesarrollo_pcu"))="M" then
					mostrar=""			
				end if
			%>
			<table width="100%" class="contornotabla" cellpadding="3">
			<tr>
				<td style="width: 15%" class="lblcurso">Asignatura</td>
				<td style="width: 75%" colspan="3"><%=rsCurso("nombre_cur")%>(<%=rsCurso("identificador_cur")%>)</td>
			</tr>
			<tr>
				<td style="width: 15%; " class="lblcurso">Nº de Grupos</td>
				<td style="width: 10%; ">
				<input name="txtGrupos<%=i%>" type="text" value="1" class="Cajas" size="2" maxlength="2" onKeyPress="validarnumero()" >
				</td>
				<td align="right" style="width: 20%; " class="etiqueta">Vacantes por Grupo</td>
				<td style="width: 20%; ">
				<input name="txtVacantes<%=i%>" type="text" value="<%=vacantes_cac%>" class="Cajas" size="3" onKeyPress="validarnumero()" maxlength="3" >
				</td>
			</tr>
			<tr <%=mostrar%>>
				<td style="width: 15%" class="lblcurso">Fecha de Inicio</td>
				<td style="width: 10%">
				<input name="txtInicio<%=i%>" type="text" value="<%=fechaini_cac%>" class="Cajas" size="10" readonly="true" ><input name="cmdinicio" type="button" class="cunia" onClick="MostrarCalendario('txtInicio<%=i%>')" ></td>
				<td align="right" style="width: 20%" class="etiqueta">Fecha de Culminación</td>
				<td style="width: 20%">
				<input name="txtFin<%=i%>" type="text" value="<%=fechafin_cac%>" class="Cajas" size="10" ><input name="cmdinicio1" type="button" class="cunia" onClick="MostrarCalendario('txtFin<%=i%>');" ></td>
			</tr>
			<tr <%=mostrar%>>
				<td style="width: 15%" class="lblcurso">Fecha de Retiro</td>
				<td style="width: 10%">
				<input name="txtRetiro<%=i%>" type="text" value="<%=fecharetiro_cac%>" class="Cajas" size="10" ><input name="cmdinicio0" type="button" class="cunia" onClick="MostrarCalendario('txtRetiro<%=i%>')" ></td>
				<td style="width: 20%">&nbsp;</td>
				<td style="width: 20%">&nbsp;</td>
			</tr>
			<tr>
				<td style="width: 15%" class="lblcurso" valign="top">Observaciones</td>
				<td style="width: 70%" colspan="3">
				<textarea name="txtobs_cup<%=i%>" cols="" rows="3" class="Cajas2"></textarea><br>
				</td>
			</tr>
			<tr>
				<td style="width: 15%" class="lblcurso" valign="top">Dpto. Acad. al cual solicita Profesor</td>
				<td style="width: 70%" colspan="3">
				<%
				'call llenarlista("cbocodigodac_cup" & i,"",rsDpto,"codigo_dac","nombre_dac",rsCurso("codigo_dac"),"","","")
				response.Write(rsCurso("nombre_dac"))
				'Campo oculto temporal ya que la escuela no debe cambiar de Dpto.
				%>
				<input type="hidden" name="cbocodigodac_cup<%=i%>" value="<%=rsCurso("codigo_dac")%>"/>
				</td>
			</tr>					
			<%If HayReg=true then%>
			<tr id="trEquivalencias">
				<td style="width: 15%;" class="lblcurso" valign="top">Asignaturas equivalentes 
				de planes de estudio vigentes en la Escuela</td>
				<td style="width: 75%" colspan="3" class="bordesup">
					<table width="100%">
						<%Do While Not rsEquivalente.EOF%>
						<tr>
							<td width="5%">
                            <input name="chkEq<%=i%>" type="checkbox" value="<%=rsEquivalente("codigo_Ceq")%>" >&nbsp;</td>
							<td width="95%" class="piepagina"><%=rsEquivalente("nombre_curE")%>&nbsp;</td>
						</tr>
						<%
							rsEquivalente.movenext
						Loop
						%>
					</table>
				</td>
			</tr>
			<%end if%>
			<tr id="trEquivalencias">
				<td style="width: 15%;" class="lblcurso" valign="top">Solo para primer ciclo</td>
				<td style="width: 75%" colspan="3" class="bordesup">
					<input id="chkPrimerCiclo" type="checkbox" value="<%=soloPrimerCiclo %>"/></td>
			</tr>
			</table>
			<br>
			<%
			next
		    obj.CerrarConexion
		Set obj=nothing
		%>
		</div>
		</td>
	</tr>
	<tr height="5%">
		<td width="70%">
			<%if HayReg=true then%>
			<select name="cboMostrar" onChange="OcultarEquivalencias(this.value)">
			<option value="">Mostrar equivalencias</option>
			<option value="none">Ocultar equivalencias</option>
			</select>
			<%end if%>
			<span class="usatTituloPagina">Elegidos:<%=i%> </span>
		</td>
		<td width="30%" align="right">
		<input name="cmdRegresar" type="button" value="       Cancelar" class="noconforme1" onClick="history.back(-1)">
		<input name="cmdGuardar" type="button" value="        Guardar" class="conforme1" onClick="GuardarProgramacion()">
		</td>
	</tr>	
</table>
</form>
</body>
</html>