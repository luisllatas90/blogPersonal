<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
on error resume next
codigo_cac=session("codigo_cac")
codigo_pes=session("codigo_pes")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_pes="" then codigo_pes="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsProgramacion= Obj.Consultar("ConsultarCursoProgramado","FO",20,codigo_pes,codigo_cac,"","")

	if Not(rsProgramacion.BOF and rsProgramacion.EOF) then
		activo=true
		alto="height=""98%"""
	end if
    obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
<title>Programación de Cursos&nbsp;<%=descripcion_cac%></title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link href="../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarmatricula.js"></script>

</head>
<body oncontextmenu="return false">

<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" class="usattitulo" width="95%">Programación Académica <%=session("descripcion_cac")%></br>
		<span style="font-size:10px;">La siguiente programación académica puede estar sujeta a cambios.</span>
	</td>
	<td height="3%" class="usattitulo" valign="top" width="5%">
	<input class="usatimprimir" type="button" name="cmdImprimir" value="Imprimir" onclick="imprimir('N')">
	</td>
</tr>
<%if activo=true then%>
  <tr>
	<td height="3%" class="etiqueta" width="95%"><i><%=session("descripcion_pes")%></i></td>
	<td height="3%" valign="top" width="5%">
	&nbsp;</td>
</tr>
  <tr height="95%" valign="top">
    <td width="100%" colspan="2" id="trCursos">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
      <tr class="etabla">
	    <td width="3%">&nbsp;</td>
	    <td width="5%">Nº</td>    
	    <td width="5%">Tipo</td>	    
        <td width="5%" height="3%">Ciclo</td>
        <td width="10%" height="3%">Código</td>
        <td width="50%" height="3%">Descripcion</td>
        <td width="5%" height="3%">Créd.</td>
        <td width="5%" height="3%">TH</td>    
      </tr>
      <tr>
        <td width="100%" colspan="8" height="92%">
        <div id="listadiv" style="height:100%" class="NoImprimir">
				
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc" id="tblcursoprogramado">
		<%	i=0
			codigo_cur=0
			codigo_cup=0
			
			'Par=false

			Do while not rsProgramacion.eof
				i=i+1
				j=j+1
				HayHorario=false
				
				'if Par=True then
				'	Clase="Par"
				'else
				'	Clase="Impar"
				'end if
				'Par=Not Par

			Clase="Impar"
			if cdbl(rsProgramacion("codigo_cur"))<>cdbl(codigo_cur) then
	  				codigo_cur=rsProgramacion("codigo_cur")
	  				k=k+1
				if k mod 2 = 0 then
					Clase="Par"
				end if

		%>
			<tr class="<%=clase%>" height="15px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td class="bordesup" width="3%" align="center" class="NoImprimir" onclick="AbrirCurso('<%=codigo_cur%>')">
			<img alt="Ver horarios" src="../images/mas.gif" id="img<%=codigo_cur%>">
			</td>
			<td class="bordesup" align="center" width="5%"><%=k%></td>
			<td class="bordesup" align="center" width="5%"><%=rsProgramacion("tipo_Cur")%></td>			
			<td class="bordesup" align="center" width="5%"><%=ConvRomano(rsProgramacion("ciclo_Cur"))%>&nbsp;</td>
			<td class="bordesup" width="10%"><%=rsProgramacion("identificador_Cur")%></td>
			<td class="bordesup" width="50%"><%=rsProgramacion("nombre_Cur")%></td>
			<td class="bordesup" align="center" width="5%"><%=rsProgramacion("creditos_Cur")%></td>			
			<td class="bordesup" align="center" width="5%"><%=rsProgramacion("totalhoras_Cur")%></td>
			</tr>
			<%end if%>	
			<tr valign="top" style="display:none" id="codigo_cur<%=rsProgramacion("codigo_cur")%>">
				<td colspan="8" width="100%" align="right">
				<table style="border-collapse:collapse" width="95%">
				<%	
					clase=""
	  				inicio=Extraercaracter(1,2,rsProgramacion("nombre_hor"))
	  				fin=Extraercaracter(1,2,rsProgramacion("horafin_Lho"))
					if IsNull(rsProgramacion("docente"))=false then
						docente=ConvertirTitulo(rsProgramacion("docente"))
					end if
					
					grupo=""
					VacantesDisponibles=""
					clasehorario=""

					if cdbl(codigo_cup)<>cdbl(rsProgramacion("codigo_cup")) then
						grupo="GRUPO " & rsProgramacion("grupohor_cup")
						codigo_cup=rsProgramacion("codigo_cup")

						'*******************************************************************************
						'Verificar vacantes disponibles para el GH
						'*******************************************************************************
						VacantesDisponibles=0
						VacantesDisponibles=cdbl(rsProgramacion("vacantes_cup"))-cdbl(rsProgramacion("nroMatriculados"))
						if rsProgramacion("estado_cup")=0 then
							VacantesDisponibles=0
						end if
						
						if VacantesDisponibles<=0 then
							VacantesDisponibles="[GRUPO CERRADO]"
						else
							VacantesDisponibles=VacantesDisponibles & " vacantes disponibles"
						end if
						
						if j>1 then clasehorario="class='lineahorario'"
						j=0
					end if

  					obs="Inicio: " & rsProgramacion("fechainicio_cup") & " Fin " & rsProgramacion("fechafin_cup")
	  				response.write "<tr>"
	  				response.write "<td width='10%' " & clasehorario & ">" & grupo & "</td>" & vbcrlf
	  				response.write "<td width='30%' " & clasehorario & ">" & vbcrlf
	  				if rsProgramacion("dia_lho")<>"" OR IsNull(rsProgramacion("dia_lho"))=false then
	  					response.write("- " & ConvDia(rsProgramacion("dia_Lho")) & " " & rsProgramacion("nombre_hor") & "-" & rsProgramacion("horafin_Lho") & "<br>")
						response.write("&nbsp;&nbsp;" & ConvertirTitulo(rsProgramacion("ambiente")) & "(Hrs. " & rsProgramacion("tipohoracur_lho") & ")") & vbcrlf
						response.write "</td><td width='40%' " & clasehorario & ">" & vbcrlf
						response.write(docente & "<br>" & obs) & vbcrlf
					else
						response.write "<span class=rojo>[No hay horario registrado]</span>"
					end if
					response.write "</td><td width='20%' " & clasehorario & ">" & vbcrlf
					response.write "<span class='rojo'>" & VacantesDisponibles & "</span>"
					response.write "</td></tr>"
				%>
				</table>
				</td>
			</tr>
				<%
			
				rsProgramacion.movenext
			loop
			set rsProgramacion=nothing
		%>
		</table>
		</div>
	    </td>
      	</tr>
		<tr bgcolor="#F0F0F0" align="right">
      	<td class="azul" height="5%" width="100%" colspan="8">Total de cursos programados: <%=k%>
      	</td>
      </tr>      	
      </table>
      </td>
</tr>
<%end if

If Err.Number<>0 then
    session("pagerror")="estudiante/vstconsultarprogramacion.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>	
</table>
<script type="text/javascript" language="JavaScript" src="private/analyticsEstudiante.js?x=1"></script>
</body>
</html>