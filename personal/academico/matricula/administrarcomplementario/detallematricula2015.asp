<!--#include file="../../../../funciones.asp"-->
<%
'***********************************************
'Datos del alumno encontrado
'***********************************************
codigo_cac=session("codigo_cac")
codigo_alu=session("codigo_alu")
codigo_pes=session("codigo_pes")

codigo_tfu=session("codigo_tfu")
HayReg=false

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
'response.Write "TFU" & codigo_tfu
	Obj.AbrirConexion
		Set rsMatricula=Obj.Consultar("[ConsultarCursosMatriculadosPorAsesorComp_2015]","FO",codigo_alu,codigo_pes,codigo_cac,codigo_tfu)
	Obj.CerrarConexion
	
	If Not(rsMatricula.BOF and rsMatricula.EOF) then
		HayReg=true
		
		'***********************************************
		'Validar los mensajes
		'***********************************************
		mensaje=rsMatricula("mensaje")
		if IsNull(mensaje)=false and instr(mensaje,"|")>0 then
			mensaje=split(mensaje,"|")
			codigoAcceso=mensaje(0)
			mensajecliente=mensaje(1)
		end if
	End if
	
	esMatCondicional=0
    Obj.AbrirConexion
    set rsVerificarMatCond = Obj.Consultar("verificarMatriculaCondicional_2015", "FO", codigo_alu,codigo_cac)
    Obj.CerrarConexion
    if Not(rsVerificarMatCond.BOF and rsVerificarMatCond.EOF) then
         esMatCondicional = rsVerificarMatCond("Condicional")
    end if
  
Set obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Cursos Matriculados</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula_v1.js?x=1"></script>
</head>
<body bgcolor="#EEEEEE">
<%if HayReg=true then%>
<table width="100%">
	<tr>
		<td width="75%" class="usatTablaInfo">
		<%=mensajecliente%>		
		</td>
		<td width="15%" align="right">
		<%if codigoAcceso>1 then%>
		<input type="button" value="Agregar asignatura" name="cmdAgregar" class="attach_prp" onClick="modificarmatricula('A','<%=session("codigo_pes")%>')">
		<% end if %>		
		</td>		
		<% if esMatCondicional = 1  then	%>
			<td width='18%' style='background-color:Yellow; color:red; border: 1px solid black; padding:5px; font-weight:bold;'>Matricula Condicional</td>
		<%end if%>
		</td>
	</tr>
	<tr>
	    <td style="width:10%">Motivo de Retiro: 
	    <%  Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	        Obj.AbrirConexion
	        set rsMotivo = Obj.Consultar("ACAD_BuscaMotivoAyR","FO",0,"R")
	        Obj.CerrarConexion	        
	        Set Obj=nothing
	    %>
	    </td>
	    <td><% call llenarlista("cbocodigo_mar","",rsMotivo,"codigo_mar","descripcion_mar",codigo_mar,"Seleccione el motivo","","") %></td>
	    <td colspan="2"></td>	    
	</tr>
</table>
<br/>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" width="100%" bgcolor="white">
  <tr class="etabla">
    <td width="5%">Tipo</td>
    <td width="10%">GH</td>
    <td width="15%">C�digo</td>
    <td width="50%">Descripci�n</td>
    <td width="5%">Crd</td>
    <td width="5%">Ciclo</td>
    <td width="5%">Veces</td>
    <td width="10%">Estado</td>
	<%if codigoAcceso>1 then%>
	<td width="5%" height="14">Retirar</td>
	<%end if%>
  </tr>
  <%Do while not rsMatricula.EOF
  	i=i+1
  	
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
    <i class="cursoS"><%=rsMatricula("nombre_cpf")%></i><br>
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
	<%if codigoAcceso>1 then%>
	<td width="5%" align="center" class="rojo">
		<%if rsMatricula("PermitirRetiro")="S" then
		    'if rsMatricula("vecescurso_dma") < 1 then  
		    if  rsMatricula("estado_dma")<>"R" then %>
		    
		        <img alt="Retirar asignatura" src="../../../../images/eliminar.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>')" />
		        
		<%   end if 
		    'end if
		else
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
    <td colspan="6">
    <b>Cursos Inscritos: <%=totalmat%> | Cr�ditos Inscritos:<%=totalcrd%> | Cursos Retirados: <%=totalret%>&nbsp; </b>
    </td>
  </tr>
  </table>
<%end if%>  
</body>
</html>
<%%>