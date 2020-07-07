<!--#include file="../../../funciones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

variable = time() 'Yperez 10.01.18 variable tiempo para refrescar caché del servidor en URL de descarga del silabo

dim estado 
estado=false
Dim enFecha
codigo_cac=request.querystring("codigo_cac")  
codigo_cpf=request.querystring("codigo_cpf")
descripcion_cac=request.querystring("descripcion_cac")
modulo=request.querystring("mod")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	Set rsEscuela= obj.Consultar("EVE_ConsultarCarreraProfesional","FO",modulo,codigo_tfu,codigo_usu)
	'Set rsCronograma= obj.Consultar("ConsultarCronograma","FO","SI",codigo_cac)
	Set rsCronograma= obj.Consultar("ACAD_ConsultarCronograma","FO","SI",codigo_cac, request.QueryString("mod"))

    if (rsCronograma.BOF and rsCronograma.EOF) then
        enFecha=true	
        response.Write("No puede registrar silabos por encontrarse fuera de fecha, coordinar con Dirección académica")        
    end if
	    
	if codigo_cac<>"-2" and codigo_cpf<>"-2" then
		Set rsCursoPlan= Obj.Consultar("ConsultarCursoProgramado","FO",8,codigo_cpf,codigo_cac,0,0)
		Set rsCronograma = Obj.Consultar("ACAD_ConsultarCronogramaxTipo", "FO", "SI", codigo_cac, Request.QueryString("mod"))
		
		if Not (rsCursoPlan.BOF and rsCursoPlan.EOF) then
			activo=true
			alto="height=""99%"""
		end if
		
       
	end if

    obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Administrar sílabos</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
<!--<script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>-->
<script type="text/javascript" src="../../../private/funciones.js"></script>
<script src="private/validarsilabos.js?y=88" type="text/javascript"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="5" height="5%">Administrador de Sílabos de Cursos Programados</td>
  </tr>
<tr>
	<td height="3%" style="width: 20%">Semestre Académico:</td>
	<td height="3%" style="width: 15%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
	</td>
	<td height="3%" style="width: 20%" align="right">Carrera Profesional:</td>
	<td height="3%" style="width: 65%">
	<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Carrera Profesional","","")%>
	</td>
	<td height="3%" style="width: 10%" align="right">
    <img alt="Buscar cursos programados" src="../../../images/menus/buscar_small.gif" class="imagen" onclick="AccionSilabos('C','<%=modulo%>')" width="25" height="24">
	</td>
</tr>
  <tr>
	<td height="2%" colspan="5"> 
	<% 
	  if codigo_cac<>"-2" and codigo_cpf<>"-2" then
	    if  Not (rsCronograma.BOF and rsCronograma.EOF) then
	        response.Write ("<font color='blue'>Fechas definidas para subir sílabos: </font><font color='red'>" & rsCronograma("fechaini_cro") & " - " & rsCronograma("fechafin_cro") & " </font>")
	    else 
	        response.Write ("")
	    end if 
	  end if 
	%>
	</td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td width="100%" colspan="5" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="5%" height="3%">Tipo</td>
        <td width="10%" height="3%">Código</td>
        <td width="28%" height="3%">Nombre del Curso</td>
        <td width="5%" height="3%">Créditos</td>
        <td width="5%" height="3%">TH</td>
        <td width="5%" height="3%">Grupo Horario</td>
        <td width="20%" height="3%">Docente</td>
        <td width="10%" height="3%">Sílabos</td>
      </tr>
      <tr>
        <td width="100%" colspan="9">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%	i=0:n=0:p=0
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
				bordeciclo=Agrupar(rsCursoPlan("ciclo_cur"),Ciclo)				
		%>
			<tr class="<%=iif(estado=false,"rojo","azul")%>" height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%>&nbsp;</td>
			<td <%=bordeciclo%> width="5%"><%=rsCursoPlan("tipo_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> width="12%"><%=rsCursoPlan("identificador_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> width="28%"><%=rsCursoPlan("nombre_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("creditos_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("totalhoras_cur") %>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="8%"><%=rsCursoPlan("grupohor_cup") %>&nbsp;</td>
			<td <%=bordeciclo%> width="20%"><span style="font-size: 7pt"><%=ConvertirTitulo(rsCursoPlan("profesor_cup"))%>&nbsp;</span></td>
			<td <%=bordeciclo%> align="center" width="10%">
			<%if enFecha = true then%>
				<img src="../../../images/bloquear.gif" alt="Fuera de fecha para subir silabus" class="imagen" />				
			<%elseif estado=false and enFecha = false  then%>
				<img src="../../../images/agregar.gif" alt="Agregar Sílabos" class="imagen" onclick="AccionSilabos('A','<%=descripcion_cac%>','<%=codigo_cpf%>','<%=rsCursoPlan("codigo_cup")%>')"/>
            <%elseif enFecha = false then %>
				<a href="../../../silabos/<%=descripcion_cac%>/<%=rsCursoPlan("codigo_cup")%>.zip<%="?x=" & variable%>">
				    <img src="../../../images/previo.gif" alt="Ver Silabus" border="0" />				    
				</a>				
				<a href="#">
				    <img src="../../../images/eliminar.gif" alt="Borrar Silabus" class="imagen" border="0" onclick="AccionSilabos('E','<%=codigo_cac%>','<%=codigo_cpf%>','<%=rsCursoPlan("codigo_cup")%>','<%=descripcion_cac%>')"/>
				</a>
			<%end if%>
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
    	<td width="100%" colspan="9" height="5%" bgcolor="#E6E6FA" align="right"><span class="azul">
		&nbsp;&nbsp;&nbsp;&nbsp; Sílabos registrados: <%=p%></span> | <span class=rojo><b>Sílabos No 
		registrados: <%=n%></b></span></td>
	  </tr>
      </table>
  </td>
  </tr>
  <%end if%>   
</table>
</body>
</html>