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
codigo_mat=0

'Permite retiros
Dim PerfilAutorizado
PerfilAutorizado = "N"

if codigo_tfu = 81 or codigo_tfu = 185 or codigo_tfu = 1 or codigo_tfu = 7 then
    PerfilAutorizado = "S"
end if

if codigo_cac<>"" then
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		'Set rsMatricula=Obj.Consultar("ConsultarCursosMatriculadosPorAsesor","FO",codigo_alu,codigo_pes,codigo_cac,codigo_tfu,7)
		Set rsMatricula=Obj.Consultar("ACAD_ConsultarCursosMatriculados","FO",codigo_alu,codigo_pes,codigo_cac,codigo_tfu,7)		
		Set rsCronograma=Obj.Consultar("ConsultarCronograma","FO", "ME", session("codigo_cac"))
		'response.Write(codigo_alu & "," & codigo_pes& "," & codigo_cac& "," & codigo_tfu)
	Obj.CerrarConexion
	
	If Not(rsMatricula.BOF and rsMatricula.EOF) then
		HayReg=true
	    
	    codigo_mat= rsMatricula("codigo_mat")
	    
		'***********************************************
		'Validar los mensajes
		'***********************************************
		mensaje=rsMatricula("mensaje")
		'response.Write(mensaje)
		if IsNull(mensaje)=false and instr(mensaje,"|")>0 then
			mensaje=split(mensaje,"|")
			codigoAcceso=mensaje(0)
			mensajecliente=mensaje(1)
			
      'RETIRAR = rsMatricula("PermitirRetiro")
      'RETIRAR = "[Bloqueado]"

		end if
	End if
	If HayReg=false then
		agregado=1
	end if
	
  
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
<meta http-equiv="Content-Language" content="es"/>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
<title>Cursos Matriculados</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css"/>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarciclocurso.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>
<script language="JavaScript" src="private/validarfichamatricula2015.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>
</head>
<body bgcolor="#EEEEEE">
<table width="100%">
	<tr>
		<td width="75%" class="usatTablaInfo">
		<%=mensajecliente%>		
		</td>
		<td width="15%" align="right">			
		<%	
		if rsCronograma is nothing then
			if codigoAcceso>1 or agregado=1 then%>
		    <input type="button" value="Agregar asignatura" name="cmdAgregar" class="attach_prp" onClick="modificarmatricula('A','<%=codigo_cac%>','<%=descripcion_cac%>','<%=session("codigo_pes")%>','agregarcursomatricula')">
		<%	end if
    response.Write("</td>")
			if esMatCondicional = 1  then %>
			<td width='18%' style='background-color:Yellow; color:red; border: 1px solid black; padding:5px; font-weight:bold;'>Matricula Condicional</td>
		<%end if
		end if
		%>
	
		</td>
	</tr>
	<tr>
	    <td style="width:15%" colspan="2">
	    Motivo de Retiro: 
	    <%  Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	        Obj.AbrirConexion
	        set rsMotivo = Obj.Consultar("ACAD_BuscaMotivoAyR","FO",0,"R")
	        Obj.CerrarConexion	        
	        Set Obj=nothing
	        
	        call llenarlista("cbocodigo_mar","",rsMotivo,"codigo_mar","descripcion_mar",codigo_mar,"Seleccione el motivo","","") 
	        %>
	        <input type="text" id="txtMotAyR" value="" style="width:100%" />
	    </td>
	    <td> </td>
	    
	</tr>
</table>
<br>
<%if HayReg=true then%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" width="100%" bgcolor="white">
  <tr class="etabla">
    <td width="110%" colspan="11"  class="usatTablaInfo">
    <!--<<p style="text-align: left">Leyenda :
    span style="color: lime; background-color: #00FF00">&#9632;</span> Se le cobra por el movimiento (Disponible desde 2007-II)</td>-->
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
    <!--<td width="10%">ObsAgregado</td>
    <td width="10%">ObsRetiro</td>-->


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
    <td width="5%" align="center"><%=rsMatricula("tipocurso_dma")%>&nbsp;
     <% if rsMatricula("estado_dma")="M" then %>
        <input name="hdcursos" id="hd<%=rsMatricula("codigo_dma")%>" type="hidden" value="<%=rsMatricula("creditoCur_Dma")%>" electivo="<%=iif(rsMatricula("electivo_cur")=true,1,0)%>" tc="<%=rsMatricula("tipoCurso_Dma")%>" ciclo="<%=rsMatricula("ciclo_cur")%>">
  <% end if %>
    </td>
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
    	end if 	%>
<!--
    	<%if isnull(rsmatricula.fields("codigoagregado_mar"))=false then	%>
    
    <td width="10%" align="center" bgcolor="<%=pagaagregado%>" ><p>Agregado</p><img alt="<%=rsmatricula.fields("Agregado")%>" src="../../../../images/rpta.gif" onClick="modificarmotivo(<%=rsMatricula("codigo_dma")%>,'A','<%=rsMatricula.fields("agregado")%>','<%=rsMatricula.fields("obsagregado_dma")%>')"></td>
    <%else%>
    <td width="10%" align="center"></td>
    <%end if%>
    
    <% if isnull(rsmatricula.fields("codigoretiro_mar"))=false or (InStr(rsmatricula.fields("grupohor_cup"), "CNV") = 0) then	 %>
    <td width="10%" align="center" bgcolor="<%=pagaretiro%>"><p>Retiro</p><img alt="<%=rsmatricula.fields("Retiro")%>" src="../../../../images/rpta.gif" onClick="modificarmotivo(<%=rsMatricula("codigo_dma")%>,'R','<%=rsMatricula.fields("RETIRO")%>','<%=rsMatricula.fields("obsretiro_dma")%>')"></td>
    <%else%>
    <td width="10%" align="center"></td>
    <%end if %>
    -->
	<%if codigoAcceso>1 then%>
	<td width="5%" align="center" class="rojo">
		<% RETIRAR = rsMatricula("PermitirRetiro")
		
        if  RETIRAR="S" then  'rsMatricula("PermitirRetiro")="S" %>
		<!---- mostrar solo la opcion de retiro para los cursos que no estan en estado retirado  -->
		<% if (rsmatricula.fields("estado_dma")<>"R") then
		        
		        if (InStr(rsmatricula.fields("grupohor_cup"), "CNV") = 0)  and rsMatricula("estado_dma")="M" then %>
		            <img alt="Retirar asignatura" src="../../../../images/rpta.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>','<%=codigo_cac%>')">
		        <%
		        else
		            if PerfilAutorizado = "S" then %>
                        <img alt="Retirar asignatura" src="../../../../images/rpta.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>','<%=codigo_cac%>')"> 
		        <%  
		            else
		                if rsmatricula.fields("estado_dma")<>"R" AND rsmatricula.fields("tipomatricula_dma") = "C" AND rsmatricula.fields("condicion_dma") = "A"  THEN %>
		                    <img alt="Retirar asignatura" src="../../../../images/rpta.gif" onclick="modificarmatricula('R','<%=rsMatricula("codigo_dma")%>','<%=codigo_cac%>')">
		                <%
		                end if
		            end if
		        end if
		    
		    end if %>
		
		
		    <%if session("codigo_tfu")=1 then%><br>
		    <!-- <img alt="Eliminar asignatura" src="../../../../images/eliminar.gif" onclick="modificarmatricula('E','<%=rsMatricula("codigo_dma")%>','<%=codigo_cac%>')"> -->
		    <%end if%>
		<%else
			'response.write(rsMatricula("PermitirRetiro"))
          response.write(RETIRAR)
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
  <br />
  <% if Cint(codigo_tfu) = 1 or Cint(codigo_tfu) = 7  or Cint(codigo_tfu) = 85  or Cint(codigo_tfu) = 181   then %>    
  <input type="button" value="Restablecer Matrícula <%=descripcion_cac%>" name="cmdRestablecer" onclick="restablecermatricula('<%=codigo_mat%>')" /> 
  <% end if %>
  
<%else%>
<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron asignaturas registradas para el ciclo académico seleccionado</h5>
<%end if%>  
<script type="text/javascript" language="JavaScript" src="../private/analytics-personal.js"></script>
</body>
</html>
<%end if%>