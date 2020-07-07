<!--#include file="../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
descripcion_cac=request.querystring("descripcion_cac")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

if codigo_cac<>"-2" and codigo_cpf<>"-2" then
	activo=true
	alto="height=""99%"""
end if

' 20180913 ENevado ---------------------------------------------------------------
codigo_per=request.querystring("id")
if codigo_per="" then codigo_per=session("codigo_usu")
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","PG",codigo_per)
obj.CerrarConexion
Set Obj=nothing
' ---------------------------------------------------------------------------------
%>
<html>
<% on error resume next %>
<head>
<title>Administrar sílabos</title>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarsilabos.js"></script>
<style>
<!--
.subido      { color: #0000FF }
-->
</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="2" height="5%">Administrador de Sílabos de Cursos Programados</td>
  </tr>
  <tr>
    <td width="27%" height="3%">Ciclo Académico</td>
    <td width="73%" height="3%"><%call ciclosAcademicos("ActualizarListaSilabos('vstsilabos.asp')",codigo_cac,"","")%></td>
  </tr>
  <tr>
    <td width="27%" height="3%">Escuela Profesional</td>
    <td width="73%" height="3%">
        <%'call escuelaprofesional("ActualizarListaSilabos('vstsilabos.asp')",codigo_cpf,"Seleccione la Escuela Profesional")
            call llenarlista("cbocodigo_cpf","ActualizarListaSilabos('vstsilabos.asp')",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","") ' 20180913 ENevado
        %>
        &nbsp;
    </td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td colspan="2" height="50%">
    <table cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="5%" height="3%">Tipo</td>
        <td width="10%" height="3%">Código</td>
        <td width="25%" height="3%">Nombre del Curso</td>
        <td width="5%" height="3%">Créditos</td>
        <td width="5%" height="3%">TH</td>
        <td width="5%" height="3%">Grupo Horario</td>
        <td width="15%" height="3%">Profesor</td>
        <td width="15%" height="3%">Plan</td> 
        <td width="5%" height="3%">Sílabos</td>
        <td width="5%" height="3%">Sílabos Nuevo Sistema</td>
      </tr>
      <tr>
        <td width="100%" colspan="11">
        <div id="listadiv" style="height:100%" class="NoImprimir"> 
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc"> 
		<%	Set Obj=Server.CreateObject("PryUSAT.clsDatCurso")
			Set rsCursoPlan= Obj.ConsultarCursoProgramado("RS","25",codigo_cpf,codigo_cac,"","")
			Set obj=nothing
			i=0:n=0:p=0
			Ciclo=1
			Do while not rsCursoPlan.eof
				i=i+1
				if Isnull(rsCursoPlan("fechasilabo_cup"))=true then
					estado=false
					n=n+1
				else
					estado=true
					p=p+1
				end if
				if rsCursoPlan("estado_sil")<>"-" then
					estado = true
				end if

				bordeciclo=Agrupar(rsCursoPlan("ciclo_cur"),Ciclo)				
		%>
			<tr class="<%=iif(estado=false,"rojo","subido")%>" height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			    <td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%>&nbsp;</td>
			    <td <%=bordeciclo%> width="5%"><%=rsCursoPlan("tipo_Cur")%>&nbsp;</td>
			    <td <%=bordeciclo%> width="10%"><%=rsCursoPlan("identificador_Cur")%>&nbsp;</td>
			    <td <%=bordeciclo%> width="25%"><%=rsCursoPlan("nombre_Cur")%>&nbsp;</td>
			    <td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("creditos_cur")%>&nbsp;</td>
			    <td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("totalhoras_cur")%>&nbsp;</td>
			    <td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("grupohor_cup")%>&nbsp;</td>
			    <td <%=bordeciclo%> width="15%"><span style="font-size: 7pt"><%=ConvertirTitulo(rsCursoPlan("profesor_cup"))%>&nbsp</span></td>			
			    <td <%=bordeciclo%> width="15%"><span style="font-size: 7pt"><%=ConvertirTitulo(rsCursoPlan("descripcion_Pes"))%>&nbsp</span></td> 
			    <td <%=bordeciclo%> align="center" width="5%">
			    <%if estado=true then%>
				    <a href="../../../silabos/<%=descripcion_cac%>/<%=rsCursoPlan("codigo_cup")%>.zip"><img src="../../../images/zip.gif" ALT="Ver Silabus Registrado" border=0></a>
			    <%else%>
				    <img src="../../../images/bloquear.gif" border="0" alt="Sílabo no disponible">
			    <%end if%>
			    </td>
			    <td <%=bordeciclo%> align="center" width="5%" <span style="font-size: 7pt">
				<% 
					Response.write(rsCursoPlan("estado_sil"))
					if rsCursoPlan("estado_sil")<>"-" then
						ns=ns+1
					else
						nns=nns+1	
					end if
				%>

			    </td>
		
			</tr>
				<%rsCursoPlan.movenext
			loop
			set rsCursoPlan=nothing
		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr>
    	<td width="100%" colspan="11" height="5%" bgcolor="#E6E6FA" align="right"><span class="azul">&nbsp;&nbsp; Sílabos registrados: <%=p%></span> | <span class="rojo"><b>Sílabos No registrados: <%=n%></b></span> | <span class="azul"><b>Sílabos registrados Nuevo Sistema: <%=ns%></b></span>| <span class="rojo"><b>Sílabos NO registrados Nuevo Sistema: <%=nns%></b></span></td>
	  </tr>
      </table>
  </td>
  </tr>
  <%end if %>   
</table>
</body>
<% 
    if Err.number <> 0 then
        response.Write "Error al descargar."
    end if 
%>
</html>