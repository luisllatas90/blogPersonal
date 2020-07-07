<%
codigo_cpf=request.querystring("codigo_cpf")
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
th=request.QueryString("th")

codigo_per=1
EncuentraReg=false
Agregar=false
Modificar=false
Eliminar=false
ExcedeCarga=false
msjExcedeCarga=false
if request.querystring("excedecarga")="1" then msjExcedeCarga=true end if


Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	    Set rsAviso=obj.Consultar("ACAD_RetornaDocentesPermitidos","FO",codigo_cup)		    
	    if Not(rsAviso.BOF and rsAviso.EOF) then
	        response.Write "Docentes asignados: " & rsAviso.fields("docentes") & " de " & rsAviso.fields("nrodocente_cup")
	    end if
	    if msjExcedeCarga then
	    response.Write("</br><font color=""red"">NO se registró carga porque SUPERÓ el límite de docentes permitidos: " & rsAviso.fields("nrodocente_cup"))
	    end if
		set rsPermisos=obj.Consultar("ValidarPermisoAccionesEnProcesoMatriculaCarga","FO","0",codigo_cac,session("codigo_usu"),"cargaacademica")
		Set rs=obj.Consultar("ConsultarCargaAcademica","FO","EXC",codigo_cup,0)
				
		if Not(rs.BOF and rs.EOF) then
			EncuentraReg=true		
		end if

		if not(rsPermisos.BOF and rsPermisos.EOF) then
			Agregar=cbool(rsPermisos("agregar_acr"))
			Modificar=cbool(rsPermisos("modificar_acr"))
			Eliminar=cbool(rsPermisos("eliminar_acr"))
		end if				
	obj.CerrarConexion
Set obj=nothing

'Asignar permisos a Evaluación y Registros, Administrador,Programas especiales y si el ciclo >ciclo actual
if (session("codigo_tfu")=16 OR session("codigo_tfu")=1 OR codigo_cpf=25) or int(codigo_cac)>int(session("codigo_cac")) then
	Agregar=true
	Eliminar=true
	Modificar = true
	'response.write("Sin Permisos")
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de Carga Académica</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarcargaacademica.js"></script>

<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/lbox/thickbox.js"></script>
<script type="text/javascript">
    function CargarPagina() {
        location.reload();
        //window.reload();
    }
</script>
<link rel="stylesheet" href="../../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
	
	
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
	
	
</head>
<body bgcolor="#EEEEEE">
<p class="rojo">
<%if Agregar=true then%>
<input onClick="AbrirCarga('A','<%=codigo_cup%>','<%=codigo_cac%>',0, '<%=th%>')" type="button" value="     Agregar" name="cmdAgregar" class="agregar2">
<input onClick="CargarPagina();" type="button" value="Refrescar" 
        name="cmdRefrescar" class="horario2">
<!-- Modificado por mvillavicencio 04/11/2011. Envia tambien el total de horas del Plan (th) cuand es modo 'A'-->
<%else%>
[Bloqueado para asignar profesor]
<%end if%>
</p>
<%if EncuentraReg=true then%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="90%" bgcolor="white">
  <tr class="etabla">
    <td width="5%" class="style1">
	Eliminar
	</td>
	 <td width="5%" class="style1">
	Modificar
	</td>
	<td width="8%" class="style1">ID</td>
    <td width="50%" class="style1">Profesor</td>
    <td width="20%" class="style1">Tipo Docente</td>
    <td width="15%" class="style1">Total Hrs</td>
     <td width="15%" class="style1">Exceso</td>
  </tr>
  <%Do while not rs.EOF
    Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
        Set rsExcedeCarga=obj.Consultar("PER_ValidarCargaAcademicaDocente","FO","CA",rs("codigo_per"), codigo_cac,0) '03/11/2011 verifica si se excede en su carga				    
    obj.CerrarConexion
    Set obj=nothing
  %>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="5%" align="center">
	<%if Modificar=true or Eliminar=true then%>
    <img border="0" src="../../../../images/eliminar.gif" onClick="AbrirCarga('E','<%=codigo_cup%>','<%=codigo_cac%>','<%=rs("codigo_per")%>','<%=codigo_dac%>','<%=th%>')">  
    <%else%>
    [Bloqueado]
    <%end if%>
    </td>
  
      <td width="5%" align="center">
	<%if Modificar=true or Eliminar=true then%>
	<a href='frmModificarHorasDocenteCarga.aspx?codigo_per=<%=rs("codigo_per")%>&docente=<%=rs("personal")%>&codigo_cup=<% =codigo_cup%>&th=<% =th%>&totalhoras=<%=rs("totalhoras_car")%>&KeepThis=true&TB_iframe=true&height=120&width=355&modal=true' title='Modificar horas' class='thickbox'">
    <img border="0" src="../../../../images/editar.gif">
    </a>
    <%else%>
    [Bloqueado]
    <%end if%>
    </td>
    
    <td width="8%"><%=rs("codigo_per")%>&nbsp;</td>
    <td width="50%"><%=rs("personal")%>&nbsp;</td>
    <td width="20%"><%=rs("descripcion_fun")%>&nbsp;</td>
    <td width="15%"><%=rs("totalhoras_car")%>&nbsp;</td>
    
    <td width="5%" align="center">
	<%
	ExcedeCarga=cbool(rsExcedeCarga("Mensaje"))
	if ExcedeCarga=true then%>
	
	<a href='frmCargaAcademicaDocente.aspx?codigo_per=<%=rs("codigo_per")%>&codigo_cac=<% =codigo_cac%>&dedicacion=<% =rsExcedeCarga("Dedicacion") %>&parametrohoras=<% =rsExcedeCarga("ParametroHoras") %>&KeepThis=true&TB_iframe=true&height=300&width=600&modal=true' title='Ver Detalle' class='thickbox'">
    <img border="0" src="../../../../images/error.gif">
    </a>
    <%else%>        
        
    <%end if%>
    </td>
  </tr>
  	<%rs.movenext
  loop
  Set rs=nothing
  Set rsExcedeCarga=nothing  
  %>
  </table>
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado Profesores para la asignatura seleccionada.</h5>
<%end if%>
</body>
</html>