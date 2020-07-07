<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
estadonota=request.querystring("estadonota")
examen=request.querystring("examen")

usuario=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

if codigo_cac<>"-2" and codigo_cpf<>"-2" then
	activo=true
	alto="height=""99%"""
end if

  	Set objEscuela=Server.CreateObject("PryUSAT.clsAccesoDatos")
  		objEscuela.AbrirConexion
  			if session("codigo_tfu")=1 or session("codigo_tfu")=7 or session("codigo_tfu")=16 then
			    Set rsEscuela= objEscuela.Consultar("ConsultarCarreraProfesional","FO","MA",0)
			else
				Set rsEscuela= objEscuela.Consultar("consultaracceso","FO","ESC","Silabo",usuario)
			end if

			if activo=true then
				Set rsProfesor= objEscuela.Consultar("ConsultarNotas","FO","EN",codigo_cac,codigo_cpf,estadonota)
			end if
		    
	    objEscuela.CerrarConexion
	Set objEscuela=nothing
%>
<html>
<head>
<title>Registro de llenado de notas</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="4" height="5%">Reporte de Llenado de Notas</td>
  </tr>
  <tr>
    <td width="18%" height="3%">Ciclo Académico</td>
    <td width="73%" height="3%" colspan="3"><%call ciclosAcademicos("",codigo_cac,"","")%></td>
 </tr>
  <tr>
    <td width="18%" height="3%">Escuela Profesional</td>
    <td width="73%" height="3%" colspan="3"><%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"","","")%></td>
  </tr>

  <tr>
    <td width="18%" height="3%">Estado de Registro</td>
    <td width="10%" height="3%"><select size="1" name="cboestadonota">
    <option value="1" <%=SeleccionarItem("cbo",trim(estadonota),1)%>>Llenaron registros</option>
    <option value="0" <%=SeleccionarItem("cbo",trim(estadonota),0)%>>No llenaron registros</option>
    </select></td>
    <td width="5%" height="3%">
    <input type="button" value="     Consultar..." name="cmdBuscar" class="buscar2" onClick="mensaje.innerHTML='<b>Espere un momento...</b>';actualizarlista('llenadonotas.asp?codigo_cac=' + cbocodigo_cac.value + '&codigo_cpf=' + cbocodigo_cpf.value + '&estadonota=' + cboestadonota.value)"></td>
    <td width="26%" height="3%" align="right" class="rojo" id="mensaje">
    (*) Se incluyen&nbsp; los cursos por Suficiencia</td>
  </tr>
  <%if activo=true then%>  
  <tr>
    <td width="100%" colspan="4" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%">Nº</td>
        <td width="25%" height="3%">Profesor</td>
        <td width="28%" height="3%">Nombre del Curso (*)</td>
        <td width="5%" height="3%">Grupo Horario</td>
        <!--<td width="5%" height="3%">Estado</td>-->
      </tr>
      <tr>
        <td width="100%" colspan="4">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%
		if estadonota=0 then
			rsProfesor.filter="llenados=0"
			mensaje="POR LLENAR NOTAS"
		else
			rsProfesor.filter="llenados<>0"
			mensaje="CON REGISTRO DE NOTAS COMPLETO"
		end if
		
		codigo_per=0
		
		Do while not rsProfesor.eof
				i=i+1
				
				if rsProfesor("codigo_per")<>codigo_per then
					p=p+1
					codigo_per=rsProfesor("codigo_per")
				end if
		%>
			<tr height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
			<td align="center" width="5%"><%=i%>&nbsp;</td>
			<td width="40%"><%=rsProfesor("docente")%>&nbsp;</td>
			<td width="38%"><%=rsProfesor("nombre_Cur")%>&nbsp;</td>
			<td align="center" width="10%"><%=rsProfesor("grupohor_cup")%>&nbsp;</td>
			<!--<td align="center" width="10%"><%=iif(estadonota=0,"Registro por llenar","Registro llenado")%>&nbsp;</td>-->
			</tr>
				<%rsProfesor.movenext
			loop
			set rsProfesor=nothing
		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr>
    	<td width="100%" colspan="4" height="5%" bgcolor="#E6E6FA" align="right"><b> <%=I%> ASIGNATURAS <%=mensaje%>  | <%=P%> PROFESORES <%=mensaje%></b></td>
	  </tr>
      </table>
  </td>
  </tr>
  <%end if%>   
</table>
</body>
</html>