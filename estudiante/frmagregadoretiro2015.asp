<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
on error resume next
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	codigoAcceso=0
'### consultar mensajes bloqueos ###
	Obj.AbrirConexion
    '	Set rsMensajes=Obj.Consultar("VerificarAccesoMatriculaEstudiante_V2","FO","ALU",session("codigo_alu"),session("codigo_cac"))
		Set rsBloqueoAYR=Obj.Consultar("ACAD_ConsultarBloqueosAgregadosRetiros","FO",session("codigo_alu"),session("codigo_cac"),session("codigo_pes"))
	Obj.CerrarConexion	

	'if (rsMensajes.BOF = false and rsMensajes.EOF = false) or (rsBloqueoAYR.BOF = false and rsBloqueoAYR.EOF = false)  then
	if (rsBloqueoAYR.BOF = false and rsBloqueoAYR.EOF = false)  then
		codigoAcceso=0
		response.write("<h3>PROCESO DE AGREGADOS Y RETIROS (" & session("descripcion_cac") & ")</h3>")
		response.write("<font style='font-family: Arial;font-size: 11pt'>Lo sentimos, usted tiene <font color='red'><B>BLOQUEADA<B></font> esta opcin por los siguientes motivos:<br><br>")
		response.write("<table border='1' cellpadding='3' cellspacing='0' style='border-collapse: collapse; font-family: Arial;font-size: 11pt' bordercolor='#111111' width='100%'>")
		response.write("<tr  style='background-color: #395ACC; color:#FFFFFF;'><td width='60%' height='30' align='center' > <b>Motivo</b>")
		response.write("</td>")
		response.write("<td class='usattitulo' align='center' > <b>Acudir A</b>")
		response.write("</td>")
		response.write("<td class='usattitulo' align='center' > <b>Fecha Fin</b>")
		response.write("</td></tr>")
				
		Do while not rsBloqueoAYR.EOF
			response.write("<tr>")
			response.write("<td width='60%' height='30' align='left'>  " & rsBloqueoAYR("mensaje_blo") & "</td>")
			response.write("<td>" & rsBloqueoAYR("acudirA_blo") & "</td>")
			response.write("<td>" & rsBloqueoAYR("fechaVence_blo") & "</td></tr>")
			rsBloqueoAYR.movenext
		loop
		response.write("</table></font>")
	
	else

		Obj.AbrirConexion
			Set rsMatricula=Obj.Consultar("ConsultarCursosEnAgregadoRetiro","FO",session("codigo_alu"),session("codigo_pes"),session("codigo_cac"),session("beneficio_alu"),session("tipo_cac"))
		Obj.CerrarConexion	
	
		If (rsMatricula.BOF AND rsMatricula.EOF) then
			rsMatricula.close
			Set rsMatricula=nothing
			response.redirect ("mensajes2015.asp?proceso=FM")
		Else
			codigoAcceso=2
			esingresante = rsMatricula("esingresante")
	
%>
<HTML>
	<HEAD>
		<meta http-equiv="Content-Language" content="es">
		<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
		<meta name="ProgId" content="FrontPage.Editor.Document">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<link rel="stylesheet" type="text/css" href="../private/estilo.css">
		<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print">
		<script type="text/javascript" src="private/jquery.js"></script> 
			<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
			<script type="text/javascript" language="JavaScript" src="private/validarmatricula2015.js"></script>
			<script type="text/javascript" language="JavaScript" src="scriptMatricula2015/validarciclocurso.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>
			<style type="text/css" fprolloverstyle>A:hover {color: red; font-weight: bold}
        </style>
	    <title>Agregados/Retiros</title>
		<style type="text/css">
.mensaje {
	background-color: #FBF9C8;
	border-style: dotted;
	border-width: 1px;
	font-size: small;
	color: #0000FF;
	font-family: Arial, Helvetica, sans-serif;
	
}
        </style>
		
	</HEAD>
	<body oncontextmenu="return event.ctrlKey">
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
			<tr>
				<td width="60%" height="30" class="usattitulo">PROCESO DE AGREGADOS / RETIROS (<%=session("descripcion_cac")%>)</td>
				<td width="40%" height="30" align="right" class="NoImprimir">
					<%if codigoAcceso=2 then%>
					<input type="button" value="    Agregar curso" name="cmdAgregar" id="cmdAgregar" class="agregar2" onClick="modificarmatricula('A','<%=rsMatricula("codigo_mat")%>')" style="width: 100">
					<%end if%>
				</td>
			</tr>	
			<tr>
				<td width="60%">&nbsp;</td>
				<td width="40%">&nbsp;</td>
			</tr>
			<tr>
				<td width="60%" colspan="2" class="mensaje">
				<%=mensajecliente%>
				</td>
			</tr>
	</table>
	<br>
	<table id="tblmatriculados" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse;" bordercolor="gray" width="100%">
			<tr class="etabla">
				<td width="5%" height="14">N󻻺</td>
				<td width="5%" height="14" colspan="2">Tipo</td>
				<td width="15%" height="14" colspan="2">Cdigo</td>
				<td width="40%" height="14" colspan="2">Descripción</td>
				<td width="5%" height="14" colspan="2">Ciclo</td>
				<td width="5%" height="14">Crd.</td>
				<td width="10%" height="14">G.H.</td>
				<td width="5%" height="14">Veces Desaprob.</td>
				<td width="5%" height="14">Estado</td>
				<%if codigoAcceso=2 then%>
				<td width="5%" height="14">Retirar</td>
				<%end if%>
			</tr>
			<%
			Dim codigo_cpf
			TotalCreditos=0
			tc_vd =0.0
			preciocredito_mat=(cdbl(rsMatricula("precioCredito_Mat")) * 5.0)
			codigo_cpf = rsMatricula("codigo_cpf")
			i=0
			Dim VecesCurso 
		
			Do while not rsMatricula.eof
		  		i=i+1
		  		if rsMatricula("estado_dma")<>"R" then
					if rsMatricula("vecesCurso_Dma")="" then
						VecesCurso = 0
					else
						VecesCurso = cint(rsMatricula("vecesCurso_Dma"))
					end if
					TotalCreditos=cdbl(TotalCreditos) + cdbl(rsMatricula("creditocur_dma"))
					if  vecesCurso = 0 then
						tc_vd =tc_vd + (cdbl(rsMatricula("creditocur_dma")) )
					elseif  vecesCurso = 1 then
						tc_vd =tc_vd + (cdbl(rsMatricula("creditocur_dma")) * 1.3)
					else
						tc_vd =tc_vd + (cdbl(rsMatricula("creditocur_dma")) * 1.5)
					end if
				end if
				
		
			%>
			  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
				<td width="5%"><%=i%>
				<%if rsMatricula("PermitirRetiro")="S" and esingresante = 0 then%>
				<input name="hdcursos" id="hd<%=rsMatricula("codigo_dma")%>" type="hidden" value="<%=rsMatricula("creditoCur_Dma")%>" electivo="<%=iif(rsMatricula("electivo_cur")=true,1,0)%>" tc="<%=rsMatricula("tipoCurso_Dma")%>" ciclo="<%=rsMatricula("ciclo_cur")%>">
				<%end if%> &nbsp;<td width="5%" colspan="2"><%=rsMatricula("tipoCurso_Dma")%>&nbsp;</td>
				<td width="15%" colspan="2"><%=rsMatricula("identificador_Cur")%>&nbsp;</td>
				<td width="40%" colspan="2">
				<%=rsMatricula("nombre_cur")%>
				<br><br><em>Fecha de Registro: <%=rsMatricula("fechaReg_dma")%></em>
				<%If (IsNull(rsMatricula("fechamod_dma"))=false) then%>
					<br><em>Fecha de Retiro: <%=rsMatricula("fechamod_dma")%></em>
				<%end if%>
				</td>
				<td width="5%" align="center" colspan="2"><%=ConvRomano(rsMatricula("ciclo_cur"))%>&nbsp;</td>
				<td width="5%" align="center"><%=rsMatricula("creditocur_dma")%>&nbsp;</td>
				<td width="10%" align="center"><%=rsMatricula("grupohor_cup")%>&nbsp;</td>
				<td width="5%" align="center"><%=rsMatricula("vecesCurso_Dma")%>&nbsp;</td>
				<td width="5%" class="curso<%=rsMatricula("estado_dma")%>"><%=rsMatricula("estadoCurso")%>&nbsp;</td>
				<%if codigoAcceso=2 then%>
				<td width="5%" align="center" class="rojo">
				<%
				    
				    if rsMatricula("PermitirRetiro")="S" and session("tipo_Cac") = "N"  and esingresante = 0  then
				        if rsMatricula("vecesCurso_Dma") = 0 then %>				            
                            <img alt="Retirar asignatura" src="../images/eliminar.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>')" />
				    <%  end if
                    else
                        'response.write(rsMatricula("PermitirRetiro"))
                    %>					    
					<!-- <img alt="Retirar asignatura" src="../images/eliminar.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>')" /> -->
				 <% end if
				%>
				</td>
				<%end if%>
			</tr>
				<%								
				rsMatricula.movenext
			Loop
			%>
			  <tr class="usatencabezadotabla">
				<td colspan="9">Total de créditos matriculados: <%=TotalCreditos%> crédito(s)</td>
				<td colspan="5">Costo del crédito por ciclo académico:&nbsp;S/.<%=formatnumber(preciocredito_mat,2)%></td>
			</tr>
			<tr class="usatencabezadotabla">
			<td colspan="9" style="font-size:8"> <%if session("tipo_cac") = "E" then %>(No incluye costo de matricula o inscripci�n) <%end if %></td>
			<td colspan="5" style="background:yellow; font-size: small" > Costo total del ciclo : S/. 
			<%
			select case (cint(codigo_cpf))
				case 24 :
					if tc <= 13 then
						preciocredito_mat = formatnumber(preciocredito_mat,2)*tc_vd
					else
						preciocredito_mat = formatnumber(1300,2)*5
					end if 
				case 31 :
					preciocredito_mat = formatnumber(700,2)*5 
				case else :
					preciocredito_mat = formatnumber(preciocredito_mat,2)*tc_vd 
			end select 
			response.write(preciocredito_mat) 
			'=(formatnumber(preciocredito_mat,2) * tc_vd)
			%></td>
			</tr>
		</table>
		
		<br>
		<!--<h4 align="center" class="azul"><a href="../librerianet/academico/adminestadocuenta.aspx?id=<%=session("codigo_alu")%>">Haga click aqu�, para visualizar su monto a pagar.</a></h4>-->
		<script type="text/javascript" language="JavaScript" src="private/analyticsEstudiante.js?x=1"></script>
	</body>
</HTML>
		<%
		end if 
	end if 
Set rsMatricula=nothing
Set Matricula=nothing

'If Err.Number<>0 then
'    session("pagerror")="estudiante/frmagregadoretiro.asp"
'    session("nroerror")=err.number
'    session("descripcionerror")=err.description    
'	response.write("<script>top.location.href='../error.asp'</script>")
'End If
%>