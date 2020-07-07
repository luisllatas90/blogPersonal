<!--#include file="../../../../funciones.asp"-->
<%
desactivar=false
codigo_cac=request.querystring("codigo_cac")
descripcion_cac=request.querystring("descripcion_cac")
agregado=0

'Datos del alumno encontrado
codigo_alu=session("codigo_alu")
codigo_cpf=session("codigo_cpf")
codigo_pes=session("codigo_pes")
beneficio_alu=session("beneficio_alu")
codigo_tfu=session("codigo_tfu")
HayReg=false

if codigo_cac<>"" then
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsMatricula=Obj.Consultar("ConsultarCursosMatriculadosPorAsesor","FO",codigo_alu,codigo_pes,codigo_cac,codigo_tfu)
		'response.Write(codigo_alu & "," & codigo_pes& "," & codigo_cac& "," & codigo_tfu)
	Obj.CerrarConexion
	
	If Not(rsMatricula.BOF and rsMatricula.EOF) then
		HayReg=true
		
		'***********************************************
		'Validar los mensajes
		'***********************************************
		mensaje=rsMatricula("mensaje")
		'response.Write(mensaje)
		if IsNull(mensaje)=false and instr(mensaje,"|")>0 then
			mensaje=split(mensaje,"|")
			codigoAcceso=mensaje(0)
			mensajecliente=mensaje(1)
			
		end if
	End if
	If HayReg=false then
		agregado=1
	end if
	
Set obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Cursos Matriculados</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarfichamatricula.js"></script>
</head>
<body bgcolor="#EEEEEE">
<table width="100%">
	<tr>
		<td width="75%" class="usatTablaInfo">
		<%=mensajecliente%>		
		</td>
		<td width="15%" align="right">
		<%if codigoAcceso>1 or agregado=1 then%>
		<input type="button" value="Agregar asignatura" name="cmdAgregar" class="attach_prp" onClick="modificarmatricula('A','<%=codigo_cac%>','<%=descripcion_cac%>','<%=codigo_pes%>','agregarcursomatricula')">
		<%end if%>
		</td>
	</tr>
</table>
<br>
<%if HayReg=true then%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" width="100%" bgcolor="white">
  <tr class="etabla">
    <td width="110%" colspan="11"  class="usatTablaInfo">
    <p style="text-align: left">Leyenda :
    <span style="color: lime; background-color: #00FF00">&#9632;</span> Se le cobra por el movimiento (Disponible desde 2007-II)</td>
  </tr>
  <tr class="etabla">
    <td width="5%">Tipo</td>
    <td width="10%">GH</td>
    <td width="10%">Código</td>
    <td width="35%">Descripción</td>
    <td width="5%">Crd</td>
    <td width="5%">Ciclo</td>
    <td width="5%">Veces</td>
    <td width="10%">Estado</td>
    <td width="10%">ObsAgregado</td>
    <td width="10%">ObsRetiro</td>


	<%if codigoAcceso>1 then%>
	<td width="5%" height="14">Retirar</td>
	<%end if%>
  </tr>
  <%Do while not rsMatricula.EOF
  	i=i+1
  	PrecioCredito=rsMatricula("PrecioCredito_mat")
  	if rsMatricula("estado_dma")<>"R" then
  	  	totalmat=cdbl(totalmat)+1
  		totalcrd=cdbl(totalcrd)+cdbl(cdbl(rsMatricula("creditoCur_dma")))
  	else
		totalret=cdbl(totalret)+1
	end if
  %>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="5%" align="center"><%=rsMatricula("tipocurso_dma")%>&nbsp;</td>
    <td width="10%" align="center"><%=rsMatricula("grupohor_cup")%>&nbsp;</td>
    <td width="15%"><%=rsMatricula("identificador_cur")%>&nbsp;</td>
    <td width="50%">
    <%=rsMatricula("nombre_cur")%><br>
    <i class="cursoS"><%=rsMatricula("nombre_cpf")%></i><br/>
    <span class="cursoC">        
    Fecha de Registro: <%=rsMatricula("fechareg_dma")%> por [<%=rsMatricula("operadorReg_dma")%>]<br>
    <%if IsNull(rsMatricula("fechamod_dma"))=false then%>
	    Fecha de Retiro: <%=rsMatricula("fechamod_dma")%> por [<%=rsMatricula("operadormod_dma")%>]
	<%end if%>
	</span>
    </td>
    <td width="5%" align="center"><%=rsMatricula("creditoCur_Dma")%>&nbsp;</td>
    <td width="5%" align="center"><%=ConvRomano(rsMatricula("ciclo_cur"))%>&nbsp;</td>
    <td width="5%" align="center"><%=rsMatricula("vecescurso_dma")%>&nbsp;</td>
    <td class="curso<%=rsMatricula("estado_dma")%>" width="10%"><%=rsMatricula("estadoCurso")%>&nbsp;</td>
    <!--- jmanay : agregado  para mostrar los motivos de agregado y retiro -->
    <%  'averiguar si paga  agregado o retiro 
    	pagaagregado	= "white"
    	pagaretiro		= "white"		
    	'response.write ("retiro  " & rsmatricula.fields("pagaretiro"))
    	if rsmatricula.fields("pagaagregado")=true then
    		pagaagregado="#60FD83"
    	end if 	
    	 if rsmatricula.fields("pagaretiro")=true then
    		pagaretiro="#60FD83"
    	end if 	
    	if isnull(rsmatricula.fields("codigoagregado_mar"))=false then	%>
    <td width="10%" align="center" bgcolor="<%=pagaagregado%>" ><p>Agregado</p><img alt="<%=rsmatricula.fields("Agregado")%>" src="../../../../images/rpta.gif" onClick="modificarmotivo(<%=rsMatricula("codigo_dma")%>,'A','<%=rsMatricula.fields("agregado")%>','<%=rsMatricula.fields("obsagregado_dma")%>')"></td>
    <%else%>
    <td width="10%" align="center"></td>
    <%end if%>
    
    <% if isnull(rsmatricula.fields("codigoretiro_mar"))=false or (InStr(rsmatricula.fields("grupohor_cup"), "CNV") = 0) then	 %>
    <td width="10%" align="center" bgcolor="<%=pagaretiro%>"><p>Retiro</p><img alt="<%=rsmatricula.fields("Retiro")%>" src="../../../../images/rpta.gif" onClick="modificarmotivo(<%=rsMatricula("codigo_dma")%>,'R','<%=rsMatricula.fields("RETIRO")%>','<%=rsMatricula.fields("obsretiro_dma")%>')"></td>
    <%else%>
    <td width="10%" align="center"></td>
    <%end if %>
    
	<%if codigoAcceso>1 then%>
	<td width="5%" align="center" class="rojo">

		<%if rsMatricula("PermitirRetiro")="S" then%>
		<!---- mostrar solo la opcion de retiro para los cursos que no estan en estado retirado  -->
		<%if (rsmatricula.fields("estado_dma")<>"R") AND (InStr(rsmatricula.fields("grupohor_cup"), "CNV") = 0) then %>
		<img alt="Retirar asignatura" src="../../../../images/rpta.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>','<%=codigo_cac%>')">
		<%end if%>
		<%if session("codigo_tfu")=1 then%><br>
		<!-- <img alt="Eliminar asignatura" src="../../../../images/eliminar.gif" onclick="modificarmatricula('E','<%=rsMatricula("codigo_dma")%>','<%=codigo_cac%>')"> -->
		<%end if%>
		<%else
			response.write(rsMatricula("PermitirRetiro"))
		end if
	end if%>
	</td>
    
  </tr>
  	<%rsMatricula.movenext
  	loop
  rsMatricula.close
  Set rsMatricula=nothing  
  %>
  <tr class="usatTablaInfo">
    <td colspan="3" align="right">
    TOTAL:
    </td>
    <td colspan="8">
    <b>Cursos Inscritos: <%=totalmat%> | Créditos Inscritos:<%=totalcrd%> | Cursos Retirados: <%=totalret%>&nbsp; </b>
    </td>
  </tr>
  <tr class="usatTablaInfo">
    <td colspan="3" align="right">
    &nbsp;</td>
    <td colspan="8"><b>Precio por crédito: <%=PrecioCredito%></b></td>
  </tr>
  </table>
<%else%>
<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron asignaturas registradas para el ciclo académico seleccionado</h5>
<%end if%>  
</body>
</html>
<%end if%>