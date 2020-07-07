<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
codigo_tfu=request.QueryString("ctf")
usuario=session("codigo_usu")
modulo=request.QueryString("mod")

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
		Set rsEscuela= objEscuela.Consultar("EVE_ConsultarCarreraProfesional","FO",modulo,codigo_tfu,usuario)
		
		if codigo_cac<>"-2" and codigo_cpf<>"-2" then
			Set rsCursoProg= objEscuela.Consultar("ConsultarMatriculaXGrupoHorario","FO",10,codigo_cpf,codigo_cac,0)
			
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
<title>Matriculados por Asignatura y Ciclo Académico</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}</style>
<script language="javascript">
    function MostrarReporte() {
        actualizarlista("totmatriculadoscurso.asp?codigo_cac="+cbocodigo_cac.value+"&codigo_cpf="+cbocodigo_cpf.value + "&ctf=<%=request.querystring("ctf")%>&mod=<%=request.querystring("mod")%>")
    }
</script>
<style type="text/css">
.style1 {
	text-align: center;
}
</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tbody class="style1">
  <tr class="usattitulo">
    <td width="100%" colspan="3" height="5%">Matriculados por Grupo Horario</td>
  </tr>
  <tr>
    <td width="27%" height="3%">Ciclo Académico</td>
    <td width="73%" height="3%" colspan="2"><%call ciclosAcademicos("actualizarlista('totmatriculadoscurso.asp?codigo_cac='+this.value);mensaje.innerHTML='Espere un momento...'",codigo_cac,"","")%></td>
 </tr>
  <tr>
    <td width="27%" height="3%">Escuela Profesional</td>
    <td width="60%" height="3%"><%call llenarlista("cbocodigo_cpf","MostrarReporte()",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional",tipo,"")%></td>
    <td width="13%" height="3%" align="right"><%if activo=true then%><input type="button" value="Exportar..." class="excel"><%end if%></td>
  </tr>
  <tr>
    <td colspan="3">Capac.Max.Ambiente: Indica la capacidad máxima del ambiente asignado al grupo horario. Recuerde que este dato lo debe actualizar el encargo de asignar ambientes (Dirección Académica)</td>
  </tr>

  <%if activo=true then%>
  <tr>
    <td width="100%" colspan="3" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td height="3%" rowspan="2">Ciclo</td>
        <td height="3%" rowspan="2">Asignatura</td>
        <td height="3%" rowspan="2">Crd.</td>
        <td height="3%" rowspan="2">GH</td>
        <td height="3%" tooltip="Haga click en el valor del curso seleccionado para ver detalle" rowspan="2" style="height: 6%">Pre</td>
        <td height="3%" tooltip="Haga click en el valor del curso seleccionado para ver detalle" rowspan="2" style="height: 6%">Mat</td>
	<td height="3%" tooltip="Haga click en el valor del curso seleccionado para ver detalle" rowspan="2" style="height: 6%">Susp</td>
	<td height="3%" tooltip="Haga click en el valor del curso seleccionado para ver detalle" rowspan="2" style="height: 6%">
        Ret</td>
        <td height="3%" rowspan="2">Total</td>
	<td height="3%" rowspan="2">Capac. Max<br>Ambiente</td>	
        <td height="3%" colspan="2">Vacantes</td>
        <td height="3%" rowspan="2">Acción</td>        
      </tr>
      <tr class="etabla">
        <td height="3%" align="center" tooltip="Total de vacantes programadas">T</td>
        <td height="3%"  align="center" tooltip="Vacantes faltantes">F</td>
      </tr>
      	<%	
			i=0:n=0:p=0
			Ciclo=1
			Do while not rsCursoProg.eof
				i=i+1
				subtotal=0
				subtotal=rsCursoProg("pre") + rsCursoProg("mat") + rsCursoProg("susp")
				faltantes=rsCursoProg("vacantes_cup")-subtotal
							
				IF cint(faltantes)<=5 THEN 
					imgalerta="CR"
				elseif (cint(faltantes)<=10) THEN 
					imgalerta="CA"
					else
						imgalerta="CV"
				end if
				
				bordeciclo=Agrupar(rsCursoProg("ciclo_cur"),Ciclo)
		%>
			<tr height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
			<td <%=bordeciclo%> align="center"><%=ConvRomano(rsCursoProg("ciclo_Cur"))%>&nbsp;</td>	
			<td <%=bordeciclo%> style=" font-size:10px"><%=rsCursoProg("nombre_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center"><%=rsCursoProg("creditos_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center"><%=rsCursoProg("grupohor_cup")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center">
			<%=AbrirCursoProg(rsCursoProg("codigo_cup"),rsCursoProg("pre"),"P",rsCursoProg("nombre_cur"))%>
			</td>
			<td <%=bordeciclo%> align="center">
			<%=AbrirCursoProg(rsCursoProg("codigo_cup"),rsCursoProg("mat"),"M",rsCursoProg("nombre_cur"))%>		
			</td>
			<td <%=bordeciclo%> align="center" class="azul">
			<%=AbrirCursoProg(rsCursoProg("codigo_cup"),rsCursoProg("susp"),"S",rsCursoProg("nombre_cur"))%>
			</td>
			<td <%=bordeciclo%> align="center" class="azul">
			<%=AbrirCursoProg(rsCursoProg("codigo_cup"),rsCursoProg("ret"),"R",rsCursoProg("nombre_cur"))%>
			</td>
			<td <%=bordeciclo%> align="center" class="azul"><%=subtotal%></td>
			<td <%=bordeciclo%> align="center" class="azul"><%=rsCursoProg("capacidad")%></td>

			<td <%=bordeciclo%> align="center"><%=rsCursoProg("vacantes_cup")%></td>
			<td <%=bordeciclo%> align="center"><%=faltantes%></td>			
			<td <%=bordeciclo%> align="right">
			<%if rsCursoProg("horario_cup")>0 then%>
			<IMG  src="../../../../images/menu3.gif" width="16" height="16" border="0" align="middle" onClick="AbrirPopUp('vsthorariocup.asp?codigo_cup=<%=rsCursoProg("codigo_cup")%>&nombre_cur=<%=rsCursoProg("nombre_cur")%>','300','600','horario')" style="cursor:hand" alt="Ver horario del Curso seleccionado">
			<%end if%>		
			<IMG  src="../../../../images/<%=imgalerta%>.gif">
			</td>			
			</tr>
				<%rsCursoProg.movenext
			loop

		%>
		
      <tr bgcolor="#FFFFCC">
      	<td height="5%" colspan="13">
      	<b>Leyenda:</b>
      	<IMG  src="../../../../images/CR.gif">&nbsp;Quedan menos de 5 vacantes |
      	<IMG  src="../../../../images/CA.gif">&nbsp;Quedan de 6 a 10 vacantes |
		<IMG  src="../../../../images/CV.gif">&nbsp;Quedan más de 10 vacantes
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