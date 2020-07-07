<!--#include file="../../../../funciones.asp"-->
<%
'Enviarfin session("codigo_usu"),"../../../../"
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if
on error resume next
codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
apto=request.querystring("apto")
'response.Write(session("codigo_tfu"))
 
'response.write(session("Ultimamatricula") & "<>" & session("descripcion_cac"))

Set objCicloActual=Server.CreateObject("PryUSAT.clsAccesoDatos")	
objCicloActual.AbrirConexion
Set rsCicloActual=objCicloActual.Consultar("ACAD_RetornaCicloVigenteTipoEstudio","FO",2)	
objCicloActual.CerrarConexion

if Not(rsCicloActual.BOF and rsCicloActual.EOF) then	
    session("codigo_cac") = rsCicloActual("codigo_Cac")
    session("descripcion_cac") = rsCicloActual("descripcion_Cac")
    session("tipo_cac") = rsCicloActual("tipo_Cac")
end if

paginaorigen="matricula/administrar/frmadminmatricula2015.asp"
'response.write ("codigo_alu : " & codigo_alu & "mod : " & request.querystring("mod"))

if codigo_alu<>"" and modo="resultado" then
	'determinar si el alumno tiene alguna categorizacion
	'if (cdbl(session("codigo_cpf"))=4 or cdbl(session("codigo_cpf"))=11 or cdbl(session("codigo_cpf"))=3) then 
	'	response.redirect "../mensajes.asp?proceso=B"
	'end if

	response.redirect("../verificaraccesomatricula.asp?rutaActual=../academico/matricula/administrar")
	apto="S"
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Administrar matr&iacute;cula del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula_v12015.js"></script>
</head>
<script type="text/javascript" language="JavaScript" src="../private/analytics-personal.js"></script>
<body onload="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tabla_buscar">
  <tr>
    <td width="60%" class="usattitulo">Asesorar <%  if Session("tipo_cac") = "E" then 
		            response.write("inscripci&oacute;n")
		        else
		            response.write("matr&iacute;cula")
		        end if
		    %> del estudiante (<%=session("descripcion_cac")%>)</td>
	<%if codigo_alu="" then	%>
    <td width="40%"><%       
            'Llama a funciones.asp y esa pagina redirige a ../../clsbuscaralumno.asp
            call buscaralumno(paginaorigen,"../../", request.querystring("mod"))%>
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="100%" colspan="2">&nbsp;</td>
  </tr>
</table>
<br/>
<%if codigo_alu <> ""  then%>
<!--#include file="../../fradatos.asp"-->
  <%' Bloquear a estudiantes con separación
  dim CalcularSituacion
  Dim tieneSeparacion
  tieneSeparacion= 0
 
    dim rsSeparacion  
    dim rsCarta   
	Set objSeparacion=Server.CreateObject("PryUSAT.clsAccesoDatos")	
	objSeparacion.AbrirConexion
	Set rsSeparacion=objSeparacion.Consultar("ACAD_ConsultarSeparacionVigente","FO",codigo_alu)	
	objSeparacion.CerrarConexion
	
	response.write("ACAD_ConsultarSituacionAlumno " & codigo_alu & "," & session("codigo_cac"))
	
	objSeparacion.AbrirConexion
	'Set rsCarta=objSeparacion.Consultar("ACAD_ConsultarSituacionAlumno","FO", codigo_alu, session("codigo_cac"))
	Set rsCarta=objSeparacion.Consultar("ConsultarSituacionAlumno","FO", "ALU", codigo_alu, session("codigo_cac"))
	objSeparacion.CerrarConexion
	
	'#EPENA 07/01/2020 {
	    objSeparacion.AbrirConexion	
	    CalcularSituacion=objSeparacion.Ejecutar("ACAD_TipoMatriculaAlumno", True,  codigo_alu, session("codigo_cac")) 
	    objSeparacion.CerrarConexion	
	'}#EPENA 07/01/2020
	
	'Set objSeparacion=nothing
    
	if Not(rsSeparacion.BOF and rsSeparacion.EOF) then	    
	    if rsSeparacion("codigo_tse") = 2 then
	        tieneSeparacion= 1	        
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " desde " & rsSeparacion("fechaIni_sep") & " hasta " & rsSeparacion("fechafin_sep") & " por motivo: " & "<b>" & rsSeparacion("motivo_sep") & "</b>"
	        if session("codigo_tfu")=11 then 'SI ES COMPLEMENTARIO QUE NO BLOQUEE POR SEPARACION TEMPORAL
              tieneSeparacion= 0
            end if 	    
	    end if 
	else
	    
	end if 
	
    if Not(rsCarta.BOF and rsCarta.EOF) then	        
        if (rsCarta("CodTipo") = "2" OR (rsCarta("CodTipo") = "1" and session("codigo_tfu") <> "9")) then
            tieneSeparacion= 1                    
            if rsCarta("motivo") ="" then
                motivoSeparacion = "Bloqueo por veces desaprobadas."
            else
                motivoSeparacion = "<b>" & rsCarta("motivo") & "</b>"         
            end if            
        end if        
      else
          tieneSeparacion= 0       
    end if	 	    	       	
  if tieneSeparacion = 1 then  %>
    <br/>
	<table align="center" bgcolor="#EEEEEE" style="width: 80%;height:10%" cellpadding="3" class="contornotabla_azul">
		<tr>
			<td valign="middle" align="center">
			<img alt="Mensaje" src="../../../../Images/menus/noconforme_1.gif" alt="" 
                    style="height: 46px; width: 47px"/></td>
			<td>
				El estudiante tiene <%=motivoSeparacion%> <br/><br/>
				Por lo cual no podr&aacute; matricularse para el semestre. Cualquier duda consulte con el 
                Director de Escuela / Dirección Académica</td>
		</tr>
		</table>
  <% else %>
    <%  if session("Ultimamatricula")<>session("descripcion_cac") and apto="S" then %>
	<table align="center" bgcolor="#EEEEEE" style="width: 80%;height:30%" cellpadding="3" class="contornotabla_azul">
		<tr>
			<td rowspan="2" valign="top">
			<img alt="Mensaje" src="../../../../images/alerta.gif" alt="" /></td>
			<td class="usatTitulousat">
				No se han encontrado asignaturas para el ciclo acad&eacute;mico <%=session("descripcion_cac")%>
			</td>
		</tr>
		<tr class="usatTitulo">
		    <%  if Session("tipo_cac") = "E" then 
		            response.write("<td>¿Desea realizar una nueva inscripci&oacute;n?</td>")
		        else
		            response.write("<td>¿Desea realizar una nueva matr&iacute;cula?</td>")
		        end if
		    %>
			
		</tr>		
		<tr>
			<td align="center">&nbsp;</td>
			<td align="center">
			<input type="button" value="       Aceptar" name="cmdNueva" class="conforme1" onClick="modificarmatricula('N','<%=session("codigo_pes")%>')" />
			</td>
		</tr>
	</table>
	<%else%>
<table cellspacing="0" cellpadding="0" style="border-collapse: collapse; border-color: #111111;" width="100%" height="73%">
<tr height="7%">
	<td class="pestanaresaltada" id="tab" align="center" width="22%" onclick="ResaltarPestana2('0','','detallematricula2015.asp')">
        Asignaturas Matriculadas</td>
	<td width="1%" class="bordeinf">&nbsp;</td>
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('1','','vsthorario.asp')">
        Horarios</td>
	<td width="1%" class="bordeinf">&nbsp;</td>
	<!--<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('2','','estadocuenta.asp')">-->
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('2','','../../../../librerianet/academico/admincuentaper.aspx?id=<%=session("codigo_alu")%>&VerDatos=0')">
        Estado de cuenta</td>   
    <td width="1%" class="bordeinf">&nbsp;</td>
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('3','','../../../../librerianet/academico/historial_personal.aspx?id=<%=session("codigo_alu")%>&VerDatos=0')">
        Historial</td>
    <td width="32%" class="bordeinf" align="right">&nbsp;</td>
</tr>
<tr height="93%">
<td width="100%" valign="top" colspan="8" class="pestanarevez">
	<iframe id="fracontenedor" height="100%" width="100%" border="0" frameborder="0" src="detallematricula2015.asp">
	</iframe>
</td>
</tr>
</table>
<%  end if
  end if 
end if 

if Err.number <> 0 then    
    response.Write (Err.Description)
end if
%>
</body>
</html>