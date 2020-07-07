<!--#include file="../../../../funciones.asp"-->
<%
on error resume next
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
estado_cup=request.querystring("estado_cup")
usuario=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"


function AbrirCursoProg(codigo_cup,cant,estado,curso)
	if cint(cant)=0 then
		AbrirCursoProg=cant
	else	
		AbrirCursoProg="<a href='lstestudiantesgrupo.asp?nombre_cur=" & curso & "&estado_dma=" & estado & "&codigo_cup=" & codigo_cup & "'><font color='red'><u>" & cant & "</u></font></a>"
	end if
end function

Set objEscuela=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objEscuela.AbrirConexion
		if session("codigo_tfu")=1 or session("codigo_tfu")=7 or session("codigo_tfu")=16 or session("codigo_tfu")=18 then
		    tipo="S"		
		    Set rsEscuela= objEscuela.Consultar("ConsultarCarreraProfesional","FO","MA",0)
		else
			Set rsEscuela= objEscuela.Consultar("consultaracceso","FO","ESC",2,usuario)
		end if

		if codigo_cac<>"-2" and codigo_cpf<>"-2" then
			Set rsCursoProg= objEscuela.Consultar("ConsultarAlumnosMatriculados","FO",14,codigo_cpf,codigo_cac,estado_cup)
			
			if Not(rsCursoProg.BOF and rsCursoProg.EOF) then
				activo=true
				alto="height=""99%"""
			end if
		end if
	objEscuela.CerrarConexion
Set objEscuela=nothing
%>
<html>
<head>
<title>Matriculados por Asignatura y Ciclo Acad�mico</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}</style>
<script type="text/javascript" language="javascript">
function BuscarCursos()
{
	pagina="../academico/matricula/consultapublica/vstgruposcerrados.asp?codigo_cac="+cbocodigo_cac.value+"&codigo_cpf="+cbocodigo_cpf.value + "&estado_cup=" + cboestado_cup.value
	window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}
</script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tbody class="style1">
  <tr class="usattitulo">
    <td width="100%" colspan="4" height="5%">Grupos Horario Cerrados</td>
  </tr>
  <tr>
    <td width="27%" height="3%">Ciclo Acad�mico</td>
    <td width="73%" height="3%" colspan="3"><%call ciclosAcademicos("",codigo_cac,"","")%></td>
 </tr>
  <tr>
    <td width="27%" height="3%">Escuela Profesional</td>
    <td width="60%" height="3%"><%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional",tipo,"")%></td>
    <td width="13%" height="3%" align="right">
    <select name="cboestado_cup">
    	<option value="0">GRUPOS CERRADOS</option>
    	<option value="1">GRUPOS ABIERTOS</option>    	
    </select>
    </td>
    <td width="13%" height="3%" align="right">
    <img alt="Buscar cursos" class="imagen" src="../../../../images/menus/buscar_s.gif" width="16" height="17" onclick="BuscarCursos()"></td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td width="100%" colspan="4" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Ciclo</td>
        <td width="10%" height="3%" rowspan="2" style="height: 6%">C�digo</td>
        <td width="35%" height="3%" rowspan="2" style="height: 6%">Descripci�n</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Crd.</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">GH</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Pre</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Mat</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Total</td>
        <td width="5%" height="3%" colspan="2">Vacantes</td>
        <td width="5%" height="3%" rowspan="2" style="height: 6%">Horario</td>        
      </tr>
      <tr class="etabla">
        <td width="5%" height="3%" align="center" tooltip="Total de vacantes programadas">T</td>
        <td width="5%" height="3%"  align="center" tooltip="Vacantes faltantes">F</td>
      </tr>
      <tr>
        <td width="100%" colspan="11">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%	
			i=0:n=0:p=0
			Ciclo=1
			Do while not rsCursoProg.eof
				i=i+1
				subtotal=0
				subtotal=rsCursoProg("pre") + rsCursoProg("mat")
				faltantes=rsCursoProg("vacantes_cup")-subtotal
				
				bordeciclo=Agrupar(rsCursoProg("ciclo_cur"),Ciclo)
							
		%>
			<tr height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
			<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoProg("ciclo_Cur"))%>&nbsp;</td>
			<td <%=bordeciclo%> width="10%"><%=rsCursoProg("identificador_Cur")%>&nbsp;</td>			
			<td <%=bordeciclo%> width="38%"><%=rsCursoProg("nombre_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoProg("creditos_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="6%"><%=rsCursoProg("grupohor_cup")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoProg("pre")%></td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoProg("mat")%></td>
			<td <%=bordeciclo%> align="center" width="5%" class="azul"><%=subtotal%></td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoProg("vacantes_cup")%></td>
			<td <%=bordeciclo%> align="center" width="5%"><%=faltantes%></td>			
			<td <%=bordeciclo%> align="right" width="5%">
			<IMG  src="../../../../images/menu3.gif" width="16" height="16" border="0" align="middle" onClick="AbrirPopUp('vsthorariocup.asp?codigo_cup=<%=rsCursoProg("codigo_cup")%>&nombre_cur=<%=rsCursoProg("nombre_cur")%>','300','600','horario')" style="cursor:hand" alt="Ver horario del Curso seleccionado">
			</td>			
			</tr>
				<%rsCursoProg.movenext
			loop

		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr bgcolor="#FFFFCC">
      	<td height="5%" colspan="11">
      	TOTAL<b>: <%=i%> </b>
      	G<b>rupos horario cerrados</b>
      	</td>
      </tr>
      </table>
  </td>
  </tr>
  <%end if%>
</table>
<span id="mensaje" class="rojo"></span>
</body>
</html>
<% 
    if err.number<>0 then
        response.Write(err.Description)
    end if
    
    
 %>