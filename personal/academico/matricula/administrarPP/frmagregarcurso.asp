<!--#include file="../../../../funciones.asp"-->
<%
on error resume next
accion=request.querystring("accion")
codigo_pes=request.querystring("codigo_pes")
codigo_cac=request.querystring("codigo_cac")
'codigo_cac=session("codigo_cac")
descripcion_cac=request.querystring("descripcion_cac")
tipo_cac=request.querystring("tipo_cac")
' para nueva matricula

esnuevamatricula=request.querystring("esnuevamatricula")

codigo_alu=session("codigo_alu")
codigo_cpf=session("codigo_cpf")
nombre_cpf=session("nombre_cpf")
codigo_tfu=session("codigo_tfu")
alto=""
'-Comentado por hreyes-
'if codigo_cac="" then codigo_cac=session("codigo_cac")
'if codigo_cac="undefined" then codigo_cac=session("codigo_cac")
'if descripcion_cac="" then descripcion_cac=session("descripcion_cac")
'--------------------------------------------------------------------
'if tipo_cac="" then tipo_cac=session("tipo_cac")
if accion="" then accion="matriculasegura"

if trim(codigo_pes)<>"-2" then
	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	' jmanay
	' rescatar los motivos de agregado y retiro	
	tipo_mar="A" ' solo los motivos de agregado
	obj.AbrirConexion
	if esnuevamatricula="S" then
		Set rsMotivo=Obj.Consultar("ConsultarMotivosAgregadoRetiro","FO",1,tipo_mar,0,0)
	else
		Set rsMotivo=Obj.Consultar("ConsultarMotivosAgregadoRetiro","FO",0,tipo_mar,0,0)
	end if 
    dim rsRequisitos	
	
	Set rsPlanesProgramados=Obj.Consultar("ConsultarCursoProgramadoPorAsesor","FO",0,codigo_cpf,codigo_tfu,codigo_cac,codigo_alu)
	'Set rsCursosProgramados= Obj.Consultar("ConsultarCursoProgramadoPorAsesor","FO",1,codigo_alu,codigo_pes,codigo_cac,codigo_alu)
	Set rsCursosProgramados= Obj.Consultar("MAT_ConsultarCursoProgramadoPorAsesorPP","FO",1,codigo_alu,codigo_pes,codigo_cac,session("codigo_tfu"))
	set rsCuotas=Obj.Consultar("MAT_ConsultarCronogramaPensiones","FO",codigo_cac)
	set rsAlumno=Obj.Consultar("ConsultarAlumno", "FO", "AUN", codigo_alu)
	set rsRequisitos=obj.consultar("ConsultarRequisitosMatricula_v2","FO",codigo_alu, codigo_pes, codigo_cac)
	'response.Write "No. Cursos: " & codigo_alu & "," & codigo_pes & "," & codigo_cac & "," & session("codigo_tfu")
	if Not(rsCursosProgramados.BOF and rsCursosProgramados.EOF) then
		activo=true
		'alto="height=""100%"" "
		'alto="height='100%'"
		alto=""
	end if
	obj.CerrarConexion
	Set obj=nothing	
    '#######################################################
    'Consultar requisitos 
    '#######################################################
	if Not(rsRequisitos.BOF and rsRequisitos.EOF) then
		ponderado = rsRequisitos("Ponderado")
		maxCreditos = rsRequisitos("creditomaximomatricula")
		session("credMat")=rsRequisitos.fields("credMatriculados")
	    session("cantCursos")=rsRequisitos.fields("cantCursos")
	    precioCredito=cdbl(rsRequisitos.fields("precioCredito"))
	    TieneMatricula = iif(rsRequisitos.fields("codigo_mat")>0, 1, 0)
	    nroCuotas = rsRequisitos.fields("nroCuotas")
	end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta name="GENERATOR" content="Microsoft FrontPage 5.0" />
<meta name="ProgId" content="FrontPage.Editor.Document" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Elija los cursos que desea agregar a la matrícula <%=descripcion_cac%></title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/lbox/thickbox.js"></script>
<link rel="stylesheet" href="../../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" /> 
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarfichamatricula.js?x=1"></script>

<script>
	function validarseleccion()
		{
			alert (document.frmficha.cbocodigo_mar.selectedIndex);
		}
</script>

<style type="text/css">
.bloque {
	border-style: solid none solid none;
	border-width: 1px;
	border-color: #808080;
}
    </style>
</head>
<body bgcolor="#EEEEEE">


<table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	<tr>
	<td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	Procesando<br/>
	Por favor espere un momento...<br />
	<img border="0" src="../../../../images/cargando.gif" width="209" height="20" alt="" />
	</td>
	</tr>
</table>
<form name="frmFicha" method="post" action="procesarmatricula.asp?accion=<%=accion%>&codigo_cac=<%=codigo_cac%>&descripcion_cac=<%=descripcion_cac%>">
<input name="CursosProgramados" type="hidden" value="0" />
<input name="txtesnuevamatricula" type="hidden" value="<%=esnuevamatricula%>" />
<input name="VecesDesprobados" type="hidden" value="0" />
<input name="txtcodigo_cpf" type="hidden" value="<%=codigo_cpf%>" />
<input name="txttipo_cac" type="hidden" value="<%=tipo_cac%>" />
<div id="tblFicha">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%" <%=alto%>>
	<tr height="5px">
		<td colspan="3" class="usatTitulo" width="100%">
		Buscar asignaturas para matrícula 
		<%'=descripcion_cac%> 
		</td>
	</tr>
		<tr height="5px">
	<td><b>Ciclo Académico</b></td>
		<td colspan="2" width="80%"  >		    
		<%'### consultar mensajes bloqueos ###
            Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")	
            obj.AbrirConexion
            Set rsMensajes=obj.Consultar("VerificarAccesoMatriculaEstudiante_V2","FO","ASE",codigo_alu, codigo_cac) ' ASESOR DE MATRICULA
            Set rsCiclo=obj.Consultar("ConsultarCicloAcademico", "FO", "UCI", 0) ' ciclo académico
            'call llenarlista("cbociclo","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")
            obj.CerrarConexion
            'if codigo_cac="" then codigo_cac=rsCiclo("codigo_cac")
            'if descripcion_cac="" then descripcion_cac=session("descripcion_cac")
            response.Write(descripcion_cac)
            if (rsMensajes.BOF = false and rsMensajes.EOF = false)  then
                response.Write("<font color='#000000' size='1'>  El estudiante " & rsAlumno("apellidoPat_Alu") & " " & rsAlumno("apellidoMat_Alu") & " " & rsAlumno("nombres_Alu")  & " tiene<font color='#FF0000' size='2'> BLOQUEOS</font> activos. <a href='BloqueosMatricula.aspx?cac=" & codigo_cac & "&alu=" & codigo_alu & "&KeepThis=true&TB_iframe=true&height=495&width=500&modal=true' title='Ver Bloqueos' class='thickbox'><font color='#FF0000' style='text-decoration:blink'>(Clic aqui para ver los Bloqueos)</font></a></font>")
            end if 

            set obj= nothing 
        %></td>        
	</tr>
	<% if TieneMatricula = 1 then %>
	<tr height="5px">
	<td bgcolor="#E1F1FB" bordercolor="#000000"><b><u>Información adicional</u></b></td>
		<td colspan="2" width="80%" bgcolor="#E1F1FB" bordercolor="#000000">&nbsp;</td>        
	</tr>
	<tr height="5px">
	<td  bgcolor="#FFFFFF" bordercolor="#000000">Motivo : </td>
		<td colspan="2" width="80%" bgcolor="#FFFFFF" bordercolor="#000000">&nbsp;
		        <%call llenarlista("cbocodigo_mar","",rsMotivo,"codigo_mar","descripcion_mar",codigo_mar,"Seleccione el motivo","","")%> &nbsp;</td>        
	</tr>
	<tr height="5px">
		<td bgcolor="#FFFFFF" bordercolor="#000000">Observación : </td>
		<td colspan="2"  bgcolor="#FFFFFF" bordercolor="#000000">
         <textarea class="cajas2" rows="4" name="txtobs" cols="20"><%=obs%></textarea>
         </td>	
	</tr>
	<tr height="5px">
		<td colspan="3" width="100%" align="right">
		<input name="chkpermitircruce" type="checkbox" value="1">	Permitir matrícula con cruce de horario	
		</td>
	</tr>
	<% end if  %>
  <tr height="5px" class="azul">
    <td width="30%" class="etiqueta">Plan del estudiante</td>
    <td width="65%"><%=session("nombre_cpf") & "(" & session("descripcion_pes") & ")"%></td>
    <td width="5%">
    &nbsp;</td>
  </tr>
  <tr height="5px">
    <td width="30%" class="etiqueta">Planes de la Escuela</td>
    <td width="65%">
    <select id="cbocodigo_pes" name="cbocodigo_pes" class="cajas2">
  	<%
  	If Not(rsPlanesProgramados.BOF and rsPlanesProgramados.EOF) then
  		tempcodigo_cpf=0
  		Do while not rsPlanesProgramados.EOF
			i=i+1
  			if cdbl(tempcodigo_cpf)<>cdbl(rsPlanesProgramados("codigo_cpf")) then
				tempcodigo_cpf=rsPlanesProgramados("codigo_cpf")
				if i>1 then
					cadena="</optgroup>" & vbcrlf
				end if
				response.write cadena & "<optgroup label=""" & rsPlanesProgramados("nombre_cpf") & """>"
  			end if
		%>
  			<option label value="<%=rsPlanesProgramados("codigo_pes")%>" <%=SeleccionarItem("cbo",codigo_pes,rsPlanesProgramados("codigo_pes"))%>>-<%=rsPlanesProgramados("descripcion_pes")%></option>
  			<%
  			rsPlanesProgramados.movenext
		loop
	End if	
	rsPlanesProgramados.close
	Set rsPlanesProgramados=nothing
	%>
	</select>
  </td>
    <td width="5%">
    <img class="imagen" alt="Buscar cursos" src="../../../../images/buscar.gif" width="58" height="17" onclick="BuscarCursosProgramados(frmFicha.cbociclo.value,'<%=descripcion_cac%>')"></td>
  </tr>

  <tr >
    <td width="100%" colspan="3" valign="top">
      <%if (activo=true) then%>
	    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%" bgcolor="white">
			<tr class="etabla">
				<td width="3%">&nbsp;</td>
				<td width="3%">Tipo</td>	    
				<td width="5%" height="3%">Ciclo</td>
				<td width="8%" height="3%">Código</td>
				<td width="45%" height="3%">Descripción [Veces desaprobadas]</td>
				<td width="5%" height="3%">Créd.</td>
				<td width="5%" height="3%">TH</td>    
			</tr>
		    <tr>
		        <td width="100%" colspan="7" >
		        <div id="listadiv"  class="NoImprimir">		
				<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" id="tblcursoprogramado">
				<%	i=0
					codigo_cur=0
					codigo_cup=0
		
					Do while not rsCursosProgramados.eof
						i=i+1
						j=j+1
						HayHorario=false
						
					if cdbl(rsCursosProgramados("codigo_cur"))<>cdbl(codigo_cur) then
							'*******************************************************************************
							'Agregar el texto (electivo) al nombre del curso
							'*******************************************************************************	
							if rsCursosProgramados("electivo_cur")=true then
								electivo=1
								nombre_cur=nombre_cur & "<font color='#0000FF'>(Electivo)</font>"
							end if

					
			  				codigo_cur=rsCursosProgramados("codigo_cur")
			  				k=k+1	
				%>
					<tr valign="top" height="15px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="AbrirCurso('<%=codigo_cur%>')" id="curso_padre<%=codigo_cur%>" clase="<%=Clase%>">
					<td class="bloque" width="3%" align="center" class="NoImprimir">
					<img alt="Ver horarios" src="../../../../images/mas.gif" id="img<%=codigo_cur%>">
					</td>
					<td class="bloque" align="center" width="5%"><%=rsCursosProgramados("tipo_Cur")%></td>			
					<td class="bloque" align="center" width="5%"><%=ConvRomano(rsCursosProgramados("ciclo_Cur"))%>&nbsp;</td>
					<td class="bloque" width="10%"><%=rsCursosProgramados("identificador_Cur")%></td>
                    <% if rsCursosProgramados("NroVecesDes")= 1 then %>
                    <td class="bloque" width="50%"><%response.Write (rsCursosProgramados("nombre_Cur") & " <font color='#FF0000'>[" & rsCursosProgramados("NroVecesDes") & " vez]</font>") %></td>
                    <% elseif rsCursosProgramados("NroVecesDes")>= 2 then %>
                    <td class="bloque" width="50%"><%response.Write (rsCursosProgramados("nombre_Cur") & " <font color='#FF0000'>[" & rsCursosProgramados("NroVecesDes") & " veces]</font>") %></td>
                    <% else %>
                    <td class="bloque" width="50%"><%response.Write (rsCursosProgramados("nombre_Cur")) %></td>					
					<% end if  %>
					<td class="bloque" align="center" width="5%"><%=rsCursosProgramados("creditos_Cur")%></td>			
					<td class="bloque" align="center" width="5%"><%=rsCursosProgramados("totalhoras_Cur")%></td>
					<td class="bloque" align="center" width="3%">
					<a href="../vstrequisitos.asp?codigo_alu=<%=codigo_alu%>&codigo_pes=<%=codigo_pes%>&codigo_cur=<%=codigo_cur%>&KeepThis=true&TB_iframe=true&height=495&width=500&modal=true" title='Ver Requisitos' class='thickbox'><img src="../../../../images/libroabierto.gif" class="imagen" style="border-style:none" /></a>
					</td>
					</tr>
					<%end if%>	
					<tr valign="top" style="display:none" id="codigo_cur<%=rsCursosProgramados("codigo_cur")%>">
						<td colspan="7" width="100%" align="right">
						<table style="border-collapse:collapse" width="100%">
						<%	
							clase=""
			  				inicio=Extraercaracter(1,2,rsCursosProgramados("nombre_hor"))
			  				fin=Extraercaracter(1,2,rsCursosProgramados("horafin_Lho"))
							if IsNull(rsCursosProgramados("docente"))=false then
								docente=ConvertirTitulo(rsCursosProgramados("docente"))
							end if
							
							grupo=""
							VacantesDisponibles=""
							clasehorario=""
							activar=false
		
							if cdbl(codigo_cup)<>cdbl(rsCursosProgramados("codigo_cup")) then
								grupo="GRUPO " & rsCursosProgramados("grupohor_cup")
								codigo_cup=rsCursosProgramados("codigo_cup")
										
								'*******************************************************************************
								'Verificar vacantes disponibles para el GH
								'*******************************************************************************
								VacantesDisponibles=0
								VacantesDisponibles=cdbl(rsCursosProgramados("vacantes_cup"))-cdbl(rsCursosProgramados("nroMatriculados"))
								if rsCursosProgramados("estado_cup")=0 then
									VacantesDisponibles=0
								end if
								
								'*******************************************************************************
								'Mostrar mensaje si se ha matriculado en el CURSO
								'*******************************************************************************
								if rsCursosProgramados("EsCursomatriculado")=0 then
									if VacantesDisponibles<=0 then
										VacantesDisponibles="[GRUPO CERRADO]"
									else
										VacantesDisponibles=VacantesDisponibles & " vacantes disponibles"
										activar=true
									end if
								end if
								
								if j>1 then clasehorario="class='lineahorario'"
								j=0
							end if
		
		  					obs="Inicio: " & rsCursosProgramados("fechainicio_cup") & " Fin " & rsCursosProgramados("fechafin_cup")
			  				response.write "<tr>"
			  				response.write "<td width='3%' " & clasehorario & ">&nbsp;</td>" & vbcrlf			  				
			  				response.write "<td width='10%' " & clasehorario & ">" & grupo & "</td>" & vbcrlf
			  				response.write "<td width='30%' " & clasehorario & ">" & vbcrlf
			  				if rsCursosProgramados("dia_lho")<>"" OR IsNull(rsCursosProgramados("dia_lho"))=false then
			  					response.write("- " & ConvDia(rsCursosProgramados("dia_Lho")) & " " & rsCursosProgramados("nombre_hor") & "-" & rsCursosProgramados("horafin_Lho") & "<br>")
								response.write("&nbsp;&nbsp;" & ConvertirTitulo(rsCursosProgramados("ambiente")) & "(Hrs. " & rsCursosProgramados("tipohoracur_lho") & ")") & vbcrlf
								response.write "</td><td width='40%' " & clasehorario & ">" & vbcrlf
								response.write(docente & "<br>" & obs) & vbcrlf
							else
								response.write "<span class=rojo>[No hay horario registrado]</span>"
							end if
							response.write "</td><td width='15%' " & clasehorario & ">" & vbcrlf
							response.write "<span class='rojo'>" & VacantesDisponibles & "</span></td>"
							response.write "<td align='right' width='5%' " & clasehorario & ">"
							
							if activar=true then
							%>
							<input type="checkbox" onclick="Actualizar(this)" cp='<%=rsCursosProgramados("codigo_cup")%>' tc="<%=rsCursosProgramados("tipo_cur")%>" cc='<%=rsCursosProgramados("codigo_cur")%>' ciclo="<%=rsCursosProgramados("ciclo_cur")%>" electivo="<%=electivo%>" value="<%=rsCursosProgramados("creditos_cur")%>" name="chkcursoshabiles" id='chk<%=rsCursosProgramados("codigo_cup")%>' vd='<%=rsCursosProgramados("NroVecesDes")%>'>
							<%
							end if
							response.write "</td>" & vbcrlf
							response.write "</tr>"
						%>
						</table>
						</td>
					</tr>
						<%
					
						rsCursosProgramados.movenext
					loop
					set rsCursosProgramados=nothing
				%>
				</table>
				</div>
			    </td>
		    </tr>
			<tr bgcolor="#F0F0F0" align="right">
		      	<td class="usattablainfo"  width="100%" colspan="7">
		      	Créditos matriculados: <b><span id="creditosMat"><%=session("credMat")%></span></b> |
		      	Total de créditos seleccionados: <b><span id="totalcrd">&nbsp;</span></b>&nbsp;&nbsp;&nbsp;&nbsp; |
				Nº Cursos seleccionados :<b><span id="totalcur">&nbsp;</span></b>
				Total de Cursos Programados :<b><%=k%></b>
		    	</td>
		    </tr>
		    <% if (TieneMatricula = 0) then %>
		    <% end if %>
		</table>
  <%else %>&nbsp;<%end if%>		
    </td>
  </tr>

  <tr>
    <td width="100%" colspan="3"   valign="top" align="right">
    <input type="button" value="       Cancelar" onclick="location.href='frmadminmatricula.asp?modo=resultado&codigo_alu=<%=session("codigo_alu")%>'" name="cmdCancelar" class="noconforme1"/>
    <%'if session("codigo_usu")<>551 then %>
    <input type="button" value="     Guardar matrícula" disabled="true" onclick="EnviarFichaMatricula()" name="cmdAgregar" class="conforme1" />
    <% 'else
        'response.write("No autorizado")
    'end if%>
    </td>
  </tr>
  </table>
</div> 
<input type="hidden" name="credMat" id="credMat" value="<%=session("credMat")%>">	
<input type="hidden" name="cantCursos" id="cantCursos" value="<%=session("cantCursos")%>">	
<input type="hidden" name="precioCredito" id="precioCredito" value="<%=precioCredito %>">	
<input type="hidden" name="NroCuotas" id="NroCuotas" value="<%=nroCuotas%>">	
<input type="hidden" name="TieneMatricula" id="TieneMatricula" value="<%=tieneMatricula%>">	 
</form>
</body>
</html>
<%
 if Err.number <> 0 then
    response.Write "ERROR: " & Err.Description
 end if
 %>