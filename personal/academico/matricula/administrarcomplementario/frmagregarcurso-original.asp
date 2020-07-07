<!--#include file="../../../../funciones.asp"-->
<%
accion=request.querystring("accion")
codigo_pes=request.querystring("codigo_pes")

codigo_alu=session("codigo_alu")
codigo_cpf=session("codigo_cpf")
nombre_cpf=session("nombre_cpf")
codigo_tfu=session("codigo_tfu")
alto=""

'---------------------------------------------
'INDICAR MANUAL CODIGO_CAC Y DESCRIPCION_CAC
'---------------------------------------------
tipo_cac="E"
codigo_cac=35
descripcion_Cac="2010-0"
'---------------------------------------------
if trim(codigo_pes)<>"-2" then

	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")	
	obj.AbrirConexion	
	Set rsCursosProgramados= Obj.Consultar("ConsultarCursoProgramadoPorAsesor","FO",1,codigo_alu,codigo_pes,codigo_cac,session("codigo_tfu"))
	
	if Not(rsCursosProgramados.BOF and rsCursosProgramados.EOF) then
		activo=true
		alto="height=""100%"" "
	end if
	'--------------------------------------------------
	'consultar los creditos aprobados y el ciclo actual del alumno
	dim rstotales
	set rstotales=obj.consultar("sp_obtenerciclootros","FO",codigo_alu, codigo_pes)
	credaprobados=rstotales.fields("creditosaprobados")
	cicloalumno=rstotales.fields("ciclo")
	cicloIng_Alu =rstotales.fields("cicloIng_Alu")  'Hector: 05-08-09
	set rstotales=nothing
	'-------------------------------------------------
	obj.CerrarConexion
	Set obj=nothing
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Elija los cursos que desea agregar a la matrícula <%=descripcion_cac%></title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula.js"></script>
<style type="text/css">
.bloque {
	border-style: solid none solid none;
	border-width: 1px;
	border-color: #808080;
}
</style>
</head>
<body bgcolor="#EEEEEE">
<form name="frmFicha" method="post" action="procesarmatricula.asp?accion=<%=accion%>&codigo_mat=0">
<input name="CursosProgramados" type="hidden" value="0">
<input name="VecesDesprobados" type="hidden" value="0">
<input name="txtcodigo_cpf" type="hidden" value="<%=session("codigo_cpf")%>">
<input name="txttipo_cac" type="hidden" value="<%=session("tipo_cac")%>">
<div id="tblFicha">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%" <%=alto%>>
	<tr height="5%">
		<td colspan="3" class="usatTitulo" width="100%">
		Buscar asignaturas para matrícula <%=descripcion_cac%>
		</td>
	</tr>
	<tr height="5%">
		<td colspan="3" width="100%">&nbsp;</td>
	</tr>
  <tr height="5%" class="azul">
    <td width="30%" class="etiqueta">Plan del estudiante</td>
    <td width="65%"><%=session("nombre_cpf") & "(" & session("descripcion_pes") & ")"%></td>
    <td width="5%">
	<img class="imagen" alt="Buscar cursos" src="../../../../images/buscar.gif" width="58" height="17" onClick="BuscarCursosProgramados('<%=codigo_pes%>')">
    </td>
  </tr>
  <%if (activo=true) then%>
  <tr height="5%">
  <td width="100%" colspan="3" class="usattablainfo" align="right">
  	Cantidad de créditos aprobados : <b><%=credaprobados%></b> | Ciclo actual del Alumno : <b><%=cicloalumno%></b>|
	Créditos seleccionados: <b><span id="totalcrd">&nbsp;</span></b>&nbsp;&nbsp;&nbsp;&nbsp; |
	Cursos seleccionados :<b><span id="totalcur">&nbsp;</span></b>
	</td>
  </tr>
  <tr>
    <td width="100%" colspan="3" height="90%" valign="top">
	    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%" bgcolor="white">
			
			<tr class="etabla">
				<td width="3%">&nbsp;</td>
				<td width="3%">Tipo</td>	    
				<td width="5%" height="3%">Ciclo</td>
				<td width="8%" height="3%">Código</td>
				<td width="45%" height="3%">Descripción</td>
				<td width="5%" height="3%">Créd.</td>
				<td width="5%" height="3%">TH</td>    
			</tr>
		    <tr>
		        <td width="100%" colspan="7" height="92%">
		        <div id="listadiv" style="height:100%" class="NoImprimir">		
				<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" id="tblcursoprogramado">
				<%	i=0
					codigo_cur=0
					codigo_cup=0
		
					Do while not rsCursosProgramados.eof
						i=i+1
						j=j+1
						HayHorario=false
						imagenrequisito=""
						
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
					<tr height="15px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" id="curso_padre<%=codigo_cur%>" clase="<%=Clase%>">
					<td class="bloque" width="3%" align="center" class="NoImprimir" onClick="AbrirCurso('<%=codigo_cur%>')">
					<img alt="Ver horarios" src="../../../../images/mas.gif" id="img<%=codigo_cur%>">
					</td>
					<td onClick="AbrirCurso('<%=codigo_cur%>')" class="bloque" align="center" width="5%"><%=rsCursosProgramados("tipo_Cur")%></td>			
					<td onClick="AbrirCurso('<%=codigo_cur%>')" class="bloque" align="center" width="5%"><%=ConvRomano(rsCursosProgramados("ciclo_Cur"))%>&nbsp;</td>
					<td onClick="AbrirCurso('<%=codigo_cur%>')" class="bloque" width="10%"><%=rsCursosProgramados("identificador_Cur")%></td>
					<td onClick="AbrirCurso('<%=codigo_cur%>')" class="bloque" width="50%"><%=rsCursosProgramados("nombre_Cur")%></td>
					<td onClick="AbrirCurso('<%=codigo_cur%>')" class="bloque" align="center" width="5%"><%=rsCursosProgramados("creditos_Cur")%></td>			
					<td class="bloque" align="center" width="5%"><%=rsCursosProgramados("totalhoras_Cur")%>
					<img src="../../../../images/libroabierto.gif" class="imagen" onClick="AbrirPopUp('../vstrequisitos.asp?codigo_alu=<%=codigo_alu%>&codigo_pes=<%=codigo_pes%>&codigo_cur=<%=codigo_cur%>','400','600','yes','yes','yes')">
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
								
								    
								    'Hector Zelada:05/08/09 Complementarios solicita bloqueo para cachimbos
	  			                    if ((trim(rsCursosProgramados("tipo_cur"))="CO" or trim(rsCursosProgramados("tipo_cur"))="CC") AND int(rsCursosProgramados("creditos_cur"))=0 AND cicloIng_Alu="2009-II") then
                                       
                                         if rsCursosProgramados("codigo_cur")=2164 or rsCursosProgramados("codigo_cur")=1025 or rsCursosProgramados("codigo_cur")=1026 or rsCursosProgramados("codigo_cur")=2165  or rsCursosProgramados("codigo_cur")=1145 or rsCursosProgramados("codigo_cur")=1027 or rsCursosProgramados("codigo_cur")=1336 or rsCursosProgramados("codigo_cur")=1028 or rsCursosProgramados("codigo_cur")= 1333 or rsCursosProgramados("codigo_cur")= 2166 or rsCursosProgramados("codigo_cur")=1331 or  rsCursosProgramados("codigo_cur")=1029 or  rsCursosProgramados("codigo_cur")=1030 or  rsCursosProgramados("codigo_cur")=2167 or  rsCursosProgramados("codigo_cur")=1143 or  rsCursosProgramados("codigo_cur")=1334 or rsCursosProgramados("codigo_cur")=1337 or rsCursosProgramados("codigo_cur")=1031 or rsCursosProgramados("codigo_cur")=1032 then
                                            
					                        VacantesDisponibles="[GRUPO CERRADO*]"
					                        activar=false
					                     end if
					                      
                                    end if
	  			                    '----------------------------------------------
								
								
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
							<input type="checkbox" onClick="Actualizar(this)" cp='<%=rsCursosProgramados("codigo_cup")%>' tc="<%=rsCursosProgramados("tipo_cur")%>" cc='<%=rsCursosProgramados("codigo_cur")%>' ciclo="<%=rsCursosProgramados("ciclo_cur")%>" electivo="<%=electivo%>" value="<%=rsCursosProgramados("creditos_cur")%>" name="chkcursoshabiles" id='chk<%=rsCursosProgramados("codigo_cup")%>' vd="0">
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
		</table>
    </td>
  </tr>
  <%end if%>
  <tr>
    <td width="100%" colspan="3" height="3%" valign="top" align="right">
    <input type="button" value="       Cancelar" onClick="location.href='frmadminmatricula.asp?modo=resultado&codigo_alu=<%=session("codigo_alu")%>'" name="cmdCancelar" class="noconforme1">    
    <input type="button" value="     Guardar matrícula" disabled="true" onClick="EnviarFichaMatricula()" name="cmdAgregar" class="conforme1">
    </td>
  </tr>
  </table>
</div>  
</form>
<table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	<tr>
	<td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	Procesando<br>
	Por favor espere un momento...<br>
	<img border="0" src="../../../../images/cargando.gif" width="209" height="20">
	</td>
	</tr>
</table>
</body>
</html>