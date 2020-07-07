<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
estadonota=request.querystring("estadonota")
examen=request.querystring("examen")

codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	
	if codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16 or codigo_tfu=85 or codigo_tfu=181  then
	    if request("mod")="10" then '
	        Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","GO",0) '
	    else '
	        Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","MA",request.QueryString("mod"))
	    end if '
	    
	else
		Set rsEscuela= obj.Consultar("consultaracceso","FO","ESC",request.QueryString("mod"),codigo_usu)
	end if
	
	if  request.querystring("ct") <> "" then
		Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","TE",request.querystring("ct"))
	end if

	if codigo_cac<>"-2" and codigo_cpf<>"-2" then
		Set rsProfesor= obj.Consultar("ConsultarNotas","FO","EN",codigo_cac,codigo_cpf,estadonota,request.QueryString("mod"))
		if Not(rsProfesor.BOF and rsProfesor.EOF) then
			Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
			
			ArrEncabezados=Array("ID","Profesor","Asignatura (*)","Grupo horario","N° Matriculados","Escuela Profesional","Departamento Académico")
			ArrCampos=Array("codigo_cup","docente","nombre_cur","grupohor_cup","nro_mat","nombre_cpf","nombre_Dac")
			ArrCeldas=Array("0%","25%","30%","5%","5%","15%","20%")
			ArrCamposEnvio=Array("codigo_cup")
			pagina="detalleregistro.asp?tipo=" & codigo_cac
			alto="height=""98%"""
			activo=true
			'HCano inicio
			titulorpte = "Listado de Docentes"
			call ValoresExportacion(titulorpte,ArrEncabezados,rsProfesor,Arrcampos,ArrCeldas)
			'HCano Fin
		end if
	end if

	obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de llenado de notas</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="4" height="5%">Reporte de Llenado de Notas</td>
  </tr>
  <tr>
    <td width="20%" height="3%">Ciclo Académico</td>
    <td width="15%" height="3%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>    
    </td>
    <td width="20%" height="3%" align="right">Escuela Profesional</td>
    <td width="50%" height="3%">
	<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"","S","")%>
	</td>
 </tr>
 <tr>
    <td width="20%" height="3%">Estado de Registro</td>
    <td width="15%" height="3%">
    <select size="1" name="cboestadonota" id="cboestadonota">
    <option value="R" <%=SeleccionarItem("cbo",trim(estadonota),"R")%>>Llenaron 
	registros</option>
    <option value="P" <%=SeleccionarItem("cbo",trim(estadonota),"P")%>>No 
	llenaron registros</option>
    <option value="A" <%=SeleccionarItem("cbo",trim(estadonota),"A")%>>Tienen 
	autorización</option>
    </select></td>
    <td width="20%" height="3%" align="right">
    <% response.write("<input type='hidden' id='ct' name='ct' value='" + request("ct") + "'  />") %>
    <input type="button" value="     Consultar..." name="cmdBuscar" class="buscar2" onClick="mensaje.innerHTML='<b>Espere un momento...</b>';actualizarlista('llenadonotas.asp?codigo_cac=' + cbocodigo_cac.value + '&codigo_cpf=' + cbocodigo_cpf.value + '&estadonota=' + cboestadonota.value + '&mod=<% response.write Request.querystring("mod") %>&ct=' + ct.value)">
    
    <%
    'HCano inicio
    call botonExportar("../../../","xls","ListaDocentes","S","B")
    'HCano Fin
    %>
    </td>
    <td width="50%" height="3%" align="right" class="rojo" id="mensaje">
    (*) Se incluyen&nbsp; los cursos por Suficiencia</td>
  </tr>
  <%if activo=true then%>  
  <tr>
    <td width="100%" colspan="4" height="50%">
    <%call CrearRpteTabla(ArrEncabezados,rsProfesor,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"")
    
    'HCano inicio
    response.write "<script>document.all.exportar.style.display=''</script>"
    response.write "<script>document.all.exportar.style.height='20'</script>"
    'HCano Fin
    %>
  	</td>
  </tr>
<tr height="3%">
	<td width="100%" valign="top" colspan="4" height="3%" class="pestanaresaltada">
		Estudiantes matriculados
	</td>
</tr>  
  <tr height="92%">
	<td width="100%" valign="top" colspan="4" height="30%" class="pestanarevez">
		<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el registro del profesor para ver su detalle</span>
		<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
		</iframe>
	</td>
	</tr>
  <%end if%>
</table>
</body>
</html>