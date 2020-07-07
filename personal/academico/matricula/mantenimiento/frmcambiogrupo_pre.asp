<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_pes=request.querystring("codigo_pes")
codigo_cup=request.querystring("codigo_cup")
codigodestino=request.querystring("codigodestino")
usuario=session("codigo_usu")
codigo_tfu=session("codigo_tfu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_pes="" then codigo_pes="-2"
if codigo_cup="" then codigo_cup="-2"
if codigodestino="" then codigodestino=codigo_cup

if codigo_cac<>"-2" and codigo_pes<>"-2" then
	activo=true
end if

Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	if codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16 or codigo_tfu=124 then
		'if codigo_tfu=16 then
		'	Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","CO",25)
		'else		
		'if  date <= cdate("09/11/2009") then
			Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","TE",1)
		'End if
		'end if
	else
	    'response.Write usuario
		Set rsEscuela= obj.Consultar("consultaracceso","FO","ESC",1,usuario)

	end if
	
	if activo=true then
		'Cargar cursos programados
		set rsCursos=Obj.Consultar("ConsultarCambioGrupoHorario","FO",1,codigo_cac,codigo_pes,0)

		if codigo_cup<>"-2" then
			set rsAlumnos=Obj.Consultar("ConsultarCambioGrupoHorario","FO",2,codigo_cup,codigo_cac,0)
			if Not(rsAlumnos.BOF and rsAlumnos.EOF) then
				alto="height=""99%"""
				activarlista=true
			else
				mensaje="<tr><td colspan=7><h5 class='usatsugerencia'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se encontraron estudiantes matriculados en el grupo horario seleccionado</h5></td></tr>"
			end if
		end if
	end if
	Obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<title>Cambio de Grupos Horarios</title>
<meta charset= "utf-8"/>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript">
	function EnviarCambioGrupo(frm)
	{
		VerificaCheckMarcados(frm.chk,frm.cmdGuardar)
		if (frm.cmdGuardar.disabled==true){
			frm.cmdGuardar.disabled=false
			alert("Debe seleccionar los alumnos que desea Cambiar de grupo horario")
			return(false)
		}
		
		if (frm.cbocursodestino.value=="-2"){
			alert("Seleccione el grupo horario destino, a donde se cambiarán los estudiantes.")
			return(false)
		}
		
		if (confirm("¿Está completamente seguro que desea Cambiar de Grupo a los estudiantes seleccionados?")==true){
			frm.submit()
		}
	}
</script>

</head>
<body bgcolor="#EEEEEE">
<form name="frmlistaalumnos" method="POST" action="procesar.asp?accion=cambiaralumnosgrupohorario&codigo_cac=<%=codigo_cac%>&codigo_pes=<%=codigo_pes%>&codigo_cup=<%=codigo_cup%>">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="5" height="19">Administrar Cambio de Grupos Horarios 
    por Escuela Profesional</td>
  </tr>
  <tr>
    <td width="15%" height="5%">Ciclo Académico</td>
    <td width="15%" height="5%"><%call ciclosAcademicos("actualizarlista('frmcambiogrupo_pre.asp?codigo_cac='+ this.value)",codigo_cac,"","")%></td>
    <td width="20%" height="5%" align="right">Escuela Profesional:</td>
    <td width="40%" height="5%" colspan="2">
    <%call planescuela2("actualizarlista('frmcambiogrupo_pre.asp?codigo_cac='+ document.all.cbocodigo_cac.value + '&codigo_pes='+ this.value)",codigo_pes,rsEscuela)%>
    </td>
 </tr>
  <%if activo=true then%>
  <tr>
    <td width="15%" height="5%" class="rojo">Asignatura origen</td>
    <td width="70%" height="5%" valign="top" colspan="3">
    <%call llenarlista("cbocursoorigen","",rsCursos,"codigo_cup","nombre_cur",codigo_cup,"","","")%>    
    </td>
    <td width="15%" height="5%" valign="top" align="right">
    <img class="imagen" border="0" src="../../../../images/menu3.gif" onclick="AbrirPopUp('vsthorariocup.asp?codigo_cup='+ document.all.cbocursoorigen.value,'300','600','no','no','no','horario')">
    <input type="button" value="Buscar" name="cmdBuscar" onClick="location.href='frmcambiogrupo_pre.asp?codigo_cac=<%=codigo_cac%>&codigo_pes=<%=codigo_pes%>&codigo_cup='+document.all.cbocursoorigen.value" class="buscar2" id="cmdBuscar"></td>
  </tr>
  <%=mensaje%>
  <%if activarlista=true then%>
  <tr>
    <td width="100%" colspan="5" height="80%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%"><input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck()" value="0"></td>
        <td width="8%" height="3%">Ingreso</td>
        <td width="10%" height="3%">Código</td>
        <td width="30%" height="3%">Apellidos y Nombres</td>
        <td width="25%" height="3%">Escuela Profesional</td>
        <td width="15%" height="3%">Fecha Reg.</td>
        <td width="5%" height="3%">Estado</td>
      </tr>
      <tr>
        <td width="100%" colspan="7" bgcolor="#FFFFFF">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%
		p=0:m=0:r=0
		Do while not rsAlumnos.eof
			i=i+1
			select case rsAlumnos("estado_dma")
				case "P": p=p+1
				case "M": m=m+1
				case "R": r=r+1
			end select
		%>
			<tr id="fila<%=i%>" class="curso<%=rsAlumnos("estado_dma")%>" height="20px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td align="center" width="5%"><input type="checkbox" name="chk" id="chk<%=i%>" onClick="pintarfilamarcada(this)" value="<%=rsAlumnos("codigo_dma")%>"></td>
			<td width="8%"><%=rsAlumnos("cicloIng_alu")%>&nbsp;</td>
			<td width="10%"><%=rsAlumnos("codigouniver_alu")%>&nbsp;</td>
			<td width="32%"><%=rsAlumnos("nombresapellidos")%>&nbsp;</td>
			<td width="25%"><%=rsAlumnos("nombre_cpf")%>&nbsp;</td>
			<td width="18%"><%=rsAlumnos("fechareg_dma")%>&nbsp;</td>
			<td width="8%"><%=rsAlumnos("estado_dma")%>&nbsp;</td>
			</tr>
				<%rsAlumnos.movenext
			loop
			set rsAlumnos=nothing
		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr>
    	<td width="200%" colspan="7" height="5%" bgcolor="#E6E6FA" align="right" class="azul">TOTAL: Matriculados <%=m%> | Pre-Matriculados <%=p%> | Retirados <%=r%></td>
	  </tr>
      </table>
  </td>
  </tr>
  <tr>
    <td width="10%" height="5%" class="rojo">Asignatura destino</td>
    <td width="70%" height="5%" valign="top" colspan="3"><%rsCursos.movefirst
    call llenarlista("cbocursodestino","",rsCursos,"codigo_cup","nombre_cur",codigodestino,"","","")%>
    </td>
    <td width="15%" height="5%" valign="top" align="right">
    <img class="imagen" border="0" src="../../../../images/menu3.gif" onclick="AbrirPopUp('vsthorariocup.asp?codigo_cup='+ document.all.cbocursodestino.value,'300','600','no','no','no','horario')">
    <input type="button" value="   Cambiar Grupo" name="cmdGuardar" class="guardar2" style="width: 100" onclick="EnviarCambioGrupo(frmlistaalumnos)"></td>
  </tr>
  	<%end if
  end if%>  
  	</table>
</form>
</body>
</html>