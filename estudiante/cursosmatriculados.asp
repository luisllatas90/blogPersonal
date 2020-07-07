<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
on error resume next
    Dim montocomp    
	Set ObjMatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		ObjMatricula.AbrirConexion
			Set rsMatricula=ObjMatricula.Consultar("ConsultarMatricula","FO","0",session("codigo_alu"),session("Codigo_Cac"),session("Codigo_Pes"))

			'response.Write(session("codigo_alu") & " , ")
            'response.Write(session("Codigo_Cac") & " , ")
			'response.Write(session("Codigo_Pes")& " , ")
			montocomp = rsMatricula("montocomp")
			'response.Write(rsMatricula.recordCount)
			
			
		ObjMatricula.CerrarConexion
	Set ObjMatricula=nothing
	
	If (rsMatricula.BOF AND rsMatricula.EOF) then
		rsMatricula.close
		Set rsMatricula=nothing
		Set objMatricula=nothing
		response.redirect ("mensajes2015.asp?proceso=FM")
	else
%>
<HTML>
	<HEAD>
		<meta http-equiv="Content-Language" content="es">
		<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
		<meta name="ProgId" content="FrontPage.Editor.Document">
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<link rel="stylesheet" type="text/css" href="../private/estilo.css">
		<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print">
		<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
		<script type="text/javascript" language="JavaScript" src="private/validarmatricula.js"></script>
		<script type="text/javascript" language="JavaScript" src="private/validarciclocurso.js"></script>

		<style type="text/css" fprolloverstyle>A:hover {color: red; font-weight: bold}</style>
	    <title>Cursos Matriculados</title>
	
	</HEAD>
	<body >
	<form id="frmFicha">
					<input type="hidden" value="<%=rsMatricula("nombre_cur")%>" id="txtNombreCurso<%=rsMatricula("codigo_cup")%>">
					<input type="hidden" value="<%=rsMatricula("codigo_cup")%>" name="txtCodCursoProgramado">
		<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
			<tr>
				<td width="41%" height="30" class="usattitulo">Cursos matriculados <%=session("descripcion_cac")%></td>
				<td width="59%" height="30" align="right" class="NoImprimir">
				<%'if session("codigo_cpf")<>20 and session("codigo_cpf")<>22 and session("codigo_cpf")<>23 and session("codigo_cpf")<>28 and session("codigo_cpf")<>31 and session("codigo_cpf")<>30 and session("codigo_cpf")<>32 and session("codigo_cpf")<>33 then%>
					<input type="button" value="   Vista horario" name="cmdHorario" class="horario2" onClick="VistaHorario('M')">
				<%'end if%>
					<input type="button" value="   Imprimir" name="cmdImprimir" class="imprimir2" onClick="imprimir('P','2','')">
				</td>
			</tr>
		</table>
		<br>
		<table id="tblmatriculados" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; border: 1px solid #808080" bordercolor="#111111" width="100%">
			<tr class="usatceldatitulo">
				<td width="3%" class="NoImprimir"></td>
				<td width="5%" height="14">Nº</td>
				<td width="5%" height="14">Tipo</td>
				<td width="15%" height="14">Código</td>
				<td width="35%" height="14">Descripción</td>
				<td width="5%" height="14">Ciclo</td>
				<td width="5%" height="14">Crd.</td>
				<td width="5%" height="14">Hrs.</td>
				<td width="10%" height="14" align="center">G.H.</td>
				<td width="5%" height="14">Veces</td>
				<td width="5%" height="14">Estado</td>
				<td width="5%" height="14">Sílabo</td>
			</tr>
			<%
			
			codigo_cup=0
			estadoant=""
			tc=0
			tc_vd=0

			Dim VecesCurso
			Do while not rsMatricula.eof
		  		i=i+1
		  		j=j+1
		  		HayHorario=false
		  		
		  		clase=""
		  		
				if rsMatricula("dia_lho")<>"" OR IsNull(rsMatricula("dia_lho"))=false then
					HayHorario=true
				end if
				preciocredito_mat=(cdbl(rsMatricula("preciocredito_mat")) * 5.0)
				codigo_cpf = rsMatricula("codigo_cpf")
				if rsMatricula("estado_dma")<>"R" then
				if cdbl(rsMatricula("codigo_cup"))<>cdbl(codigo_cup) then
	  				codigo_cup=rsMatricula("codigo_cup")
	  				if j>1 then clase="class='lineahorario'"
	  				j=0
	  				
	  				if rsMatricula("estado_dma")<>"R" then
						if rsMatricula("vecesCurso_Dma")="" then
							VecesCurso = 0
						else
							VecesCurso = cint(rsMatricula("vecesCurso_Dma"))
						end if
					
						tc = cdbl(tc) + cdbl(rsMatricula("creditocur_dma"))
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
				<td class="bordesup" width="3%" align="center" class="NoImprimir" <%if HayHorario=true then%>onclick="AbrirCurso('<%=codigo_cup%>')"<%end if%>>
				<%if HayHorario=true then%>
				<img alt="Ver horarios" src="../images/mas.gif" id="img<%=codigo_cup%>">
				<%end if%>
				</td>
				<td class="bordesup" width="5%"><%=i%></td>
				<td class="bordesup" width="5%"><%=rsMatricula("tipoCurso_Dma")%></td>
				<td class="bordesup" width="15%"><%=rsMatricula("identificador_Cur")%></td>
				<td class="bordesup" width="35%"><span class="curso<%=rsMatricula("estado_dma")%>"><%=rsMatricula("nombre_cur")%></span></td>
				<td class="bordesup" width="5%" align="center"><%=ConvRomano(rsMatricula("ciclo_cur"))%></td>
				<td class="bordesup" width="5%" align="center"><%=rsMatricula("creditocur_dma")%></td>
				<td class="bordesup" width="5%" align="center"><%=rsMatricula("totalhoras_cur")%></td>
				<td class="bordesup" width="10%" align="center"><%=rsMatricula("grupohor_cup")%></td>
				<td class="bordesup" width="10%" align="center"><%=rsMatricula("vecesCurso_Dma")%>&nbsp;</td>
				<td class="bordesup" width="5%"><%=rsMatricula("estado_dma")%></td>
				<td class="bordesup" width="5%">
					<%if Isnull(rsMatricula("silabo_cup"))=true then%>
						<img src="../images/bloquear.gif" ALT="Sílabo no disponible">
					<%else%>
						<a href="../silabos/<%=rsMatricula("silabo_cup")%>"><img src="../images/zip.gif" ALT="Descargar Sílabos" border=0></a>
					<%end if%>
				</td>
			</tr>
				<%end if
					
				if HayHorario=true then
				%>	
			<tr valign="top" style="display:none" id="codigo_cur<%=rsMatricula("codigo_cup")%>">
				<td colspan="12" width="100%" align="right" class="bordesup">
				<table style="border-collapse:collapse" width="90%">
				<%

	  				inicio=Extraercaracter(1,2,rsMatricula("nombre_hor"))
	  				fin=Extraercaracter(1,2,rsMatricula("horafin_Lho"))
					if IsNull(rsMatricula("docente"))=false then
						docente=ConvertirTitulo(rsMatricula("docente"))
					end if
					
  					obs="Inicio: " & rsMatricula("fechainicio_cup") & " Fin " & rsMatricula("fechafin_cup")
	  				response.write "<tr>"
	  				response.write "<td width='30%' " & clase & ">" & vbcrlf
					response.write("<input type=""hidden"" name=""txthorario" & rsMatricula("codigo_cup") & """ value=""" & rsMatricula("dia_Lho") & inicio & fin & """>") & vbcrlf
					response.write("<input type=""hidden"" name=""txtambiente" & rsMatricula("codigo_cup") & """ value=""" & rsMatricula("ambiente") & """>") & vbcrlf
					response.write("<input type=""hidden"" name=""txtCodCursoProgramado"" value=""" & rsMatricula("codigo_cup") & """>") & vbcrlf
					response.write("<input type=""hidden"" name=""txtNombreCurso" & rsMatricula("codigo_cup") & """ value=""" & rsMatricula("nombre_cur") & """>") & vbcrlf
					
					response.write(ConvDia(rsMatricula("dia_Lho")) & " " & rsMatricula("nombre_hor") & "-" & rsMatricula("horafin_Lho") & "<br>")
					response.write(ConvertirTitulo(rsMatricula("ambiente")) & "(Hrs. " & rsMatricula("tipohoracur_lho") & ")") & vbcrlf
					response.write "</td><td width='70%' " & clase & ">" & vbcrlf
					response.write(docente & "<br>" & obs) & vbcrlf
					response.write "</td></tr>"
				%>
				</table>
				</td>
			</tr>
				<%end if
				
				end if
				
				rsMatricula.movenext
			Loop
			
			%>
			  <tr class="usatencabezadotabla">
				<td width="73%" class="bordesup" align="left" colspan="5">Total de créditos 
                    matriculados: <%=tc%> crédito(s)</td>
				<td width="80%"  class="bordesup" colspan="7" style="text-align: left">
				Costo del crédito por ciclo:&nbsp;S/.<%=formatnumber(preciocredito_mat,2)%></td>
			</tr>
			<tr class="usatencabezadotabla">
				<td width="73%" colspan="5" class="bordesup" style="font-size:8"> 
				<% 	if session("tipo_cac") = "E" then
						response.write("(No incluye costo de matricula o inscripción)") 
					end if 
				%>
	
				</td>

				<td width="80%" class="bordesup" colspan="7" style="text-align: left; background:yellow; font-size: x-small" >												
				<%  
				    if(codigo_cpf <> "24") then				        
				        if (codigo_cpf <> "31") then
				            response.Write "Costo total por pensiones: S/. "
					        select case (cint(codigo_cpf))
						        case 24 :
							        if tc <= 13 then
								        preciocredito_mat = formatnumber(preciocredito_mat,2)*tc_vd
							        else
								        preciocredito_mat = formatnumber(1500,2)*5
							        end if 
						        case 31 :
							        preciocredito_mat = formatnumber(700,2)*5 
						        case else :
							        preciocredito_mat = formatnumber(preciocredito_mat,2)*tc_vd 
					        end select 
					        response.write(preciocredito_mat)					        
				        end if				        
				    end if
				    response.Write("<br/> Costo total por complementarios: S/. " & montocomp)				  
				    %>
				</td>
			</tr>
		</table>
	</form>
	<br>
	<!--#include file="vstcrucescursos.asp"-->
	<script type="text/javascript" language="JavaScript" src="private/analyticsEstudiante.js?x=1"></script>
	</body>
</HTML>
<%end if
rsMatricula.close		
Set rsMatricula=nothing
'If Err.Number<>0 then
'    session("pagerror")="estudiante/cursosmatriculados.asp"
'    session("nroerror")=err.number
'    session("descripcionerror")=err.description    
'	response.write("<script>top.location.href='../error.asp'</script>")
'End If
%>