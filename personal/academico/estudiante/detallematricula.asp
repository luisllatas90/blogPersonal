<!--#include file="../../../funciones.asp"-->
<%
on error resume next
desactivar=false
codigouniver_alu=request.querystring("codigouniver_alu")
alumno=request.querystring("alumno")
nombre_cpf=request.querystring("nombre_cpf")
codigo_mat=request.querystring("codigo_mat")
codigo_alu=request.querystring("codigo_alu")

'agregado
reactivar=0
reactivar = request.querystring("reactivar")

'Response.Write (reactivar)

Public function ContarCursos(byVal estado_dma,ByVal creditos_cur,byVal notacredito,ByRef CD,ByRef NC,ByRef TR)
		dim cursos,crd,ncrd,ret
		cursos=0	:	crd=0	:	ncrd=0	:	ret=0
		if (estado_dma<>"C" AND estado_dma<>"R") then
			cursos=1
			crd=creditos_cur
			ncrd=notacredito
		end if
		if estado_dma="R" then
			ret=1
		end if
		ContarCursos=cursos :	CD=crd	:	NC=ncrd	:	TR=ret
end function

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")        
		Obj.AbrirConexion
			Set rsMatriculas=Obj.Consultar("ConsultarMatricula","FO","29",codigo_alu,0,0)
			if not(rsMatriculas.BOF and rsMatriculas.EOF) then
				if codigo_mat="" then codigo_mat=rsMatriculas("codigo_mat")
				
				HayReg=true
			end if
			
			if codigo_mat<>"" then
			    'Modificado.
			    if reactivar = 1 then
			        Set rsCursos=Obj.Consultar("ConsultarMatricula","FO","33",codigo_mat,0,0)	
			    else
			        Set rsCursos=Obj.Consultar("ConsultarMatricula","FO","3",codigo_mat,0,0)
			    end if
			end if
		Obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es"/>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
<title>Cursos Matriculados</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css"/>
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function actualizarmatricula(obj)
	{
		location.href="detallematricula.asp?codigo_mat=" + obj.value + "&<%=replace(request.querystring,"codigo_mat=","")%>"
	}
</script>
</head>
<body>

<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; border-color:#111111;" width="100%" class="contornotabla" bgcolor="#FFFFFF">
	          <tr>
	    <td width="18%">Código Universitario&nbsp;</td>
	    <td class="usatsubtitulousuario" width="71%">: <%=codigouniver_alu%></td>
	          </tr>
	          <tr>
	    <td width="18%">Apellidos y Nombres</td>
	    <td class="usatsubtitulousuario" width="71%">: <%=alumno%></td>
	          </tr>
	          <tr>
	    	<td width="18%">Escuela Profesional&nbsp;</td>
	    	<td class="usatsubtitulousuario" width="46%">: <%=nombre_cpf%>&nbsp;</td>
	    </tr>
	   <%if HayReg=true then%>
	    <tr class="usattablainfo">
		<td width="18%">Ciclo Académico&nbsp;</td>
	    <td class="usatsubtitulousuario" width="46%">
		<%call llenarlista("cbocodigo_mat","actualizarmatricula(this)",rsMatriculas,"codigo_mat","descripcion_cac",trim(codigo_mat),"","","")%>
		</td>
	    </tr>
	    <%end if%>
</table>
<%if codigo_mat<>"" and HayReg=true then%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#E9E9E9" width="100%" id="tbllista">
  <tr class="usatencabezadotabla" style="cursor:hand">
    <td width="3%">
    <% ' esaavedra 26.08.2013 muetsra el id del registro - sólo para el admin 
    if (session("codigo_tfu") =1) or (session("codigo_tfu") =16) then
        response.Write(rsCursos("codigo_mat"))
    else
        response.Write("..")
    end if
    %></td>
    <td width="10">Tipo</td>
    <td width="50">GH</td>
    <td width="70">Código</td>
    <td width="250">Descripción del Curso</td>
    <td width="27">Crd</td>
    <td width="27">Ciclo</td>
    <td width="46">Veces</td>
    <td width="38">Nota</td>
    <td width="10">Estado</td>
    <td width="10">Cond.</td>
    <td width="80">Fecha Reg.</td>
    <td width="80">Obs. Mat </td>
    <td width="80">Fecha Mod.</td>
    <td width="80">Operador Registro</td>    
    <td width="80">Operador Retiro</td>
    <td width="80">Cambio<br />
        Grupo</td>
    <td width="80">Obs<br />
        Retiro</td>
    <!--AGREGADO-->
    <% if reactivar = 1 then
        response.Write("<td width='80'>Vacantes<br />Disponibles</td>")
        response.Write("<td width='80'>Estado</td>")
       end if
    %>
    <!--FIN AGREGADO-->    
  </tr>
  <!--onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')"-->
  <%Do while not rsCursos.EOF
  	i=i+1
  	TC=ContarCursos(rsCursos("estado_dma"),rsCursos("creditos_cur"),rscursos("NotaCredito"),CD,NC,TR)  	  	
  	totalmat=cdbl(totalmat)+cdbl(TC)  	
  	  	
  	'if(rsCursos("tipoMatricula_Dma") <> "R") then  	    
  	if(rsCursos("tipoMatricula_Dma") <> "C" and rsCursos("inhabilitado_dma") = 0  and  rsCursos("codigo_pes") <> 593 ) then  	    
  	    if(rsCursos("tipoMatricula_Dma") <> "R" and rsCursos("inhabilitado_dma") = 0 and  rsCursos("codigo_pes") <> 593 ) then  	    
  	        if(rsCursos("tipoMatricula_Dma") <> "S" and rsCursos("inhabilitado_dma") = 0 and  rsCursos("codigo_pes") <> 593 ) then  	    
  	        if(rsCursos("tipoMatricula_Dma") <> "U" and rsCursos("inhabilitado_dma") = 0 and  rsCursos("codigo_pes") <> 593 ) then  	    
  	            totalcrd=cdbl(totalcrd)+cdbl(CD)  	 
  	            notacrd=cdbl(notacrd)+cdbl(NC)
  	            end if
  	        end if  	    
  	    end if  	  	
  	end if
  	if  rsCursos("inhabilitado_dma") <>0 then
        NumInh = NumInh + 1
        CredInh = CredInh + rsCursos("creditos_cur")
  	
   end if
  	
	totalret=cdbl(totalret)+cdbl(TR)
	
	if Isnull(rsCursos("fechamod_dma"))=false then
		fechamod=FormatDatetime(rsCursos("fechamod_dma"),2)
	else
		fechamod=""
	end if

	if (rsCursos("tipomatricula_dma")="N" or rsCursos("tipomatricula_dma")="A") then
		CurMat=CurMat+1
	end if

  %>
  <tr class="curso<%=rsCursos("estado_dma")%>" id="fila<%=i%>">
    <td width="3%" style="cursor:hand">
    <% ' esaavedra 26.08.2013 muetsra el id del registro - sólo para el admin 
    if session("codigo_tfu") =1 or session("codigo_tfu") =16 then
        response.Write(rsCursos("codigo_dma"))
    else
        response.Write("..")
    end if
    
    Dim estadoDMA
    Dim condicionDMA
    condicionDMA = rsCursos("condicion_dma")
    estadoDMA = rsCursos("estado_dma")
    
    if rsCursos("vecescurso_dma") = 0 AND rsCursos("inhabilitado_dma") = true then
        'condicionDMA = condicionDMA & "<span style='color:#FF0000';>(Inh)</span>"
        condicionDMA = "<span style='color:#3c3c3c';>INHABILITADO</span>" 
    elseif rsCursos("vecescurso_dma") > 0 AND rsCursos("inhabilitado_dma") = true AND rsCursos("estado_dma") <> "R"   then
        'condicionDMA = condicionDMA & "<span style='color:#FF0000';>(Inh)</span>"
        condicionDMA = "<span style='color:#3c3c3c';>INHABILITADO</span>"
    end if
    
    dim nota 
    nota =  rsCursos("notafinal_dma")
    If rsCursos("inhabilitado_dma") = true then
       nota = "-"
    end if
     %>
    </td>
    <td width="10"><%=rsCursos("tipocurso_dma")%>&nbsp;</td>
    <td width="50"><%=rsCursos("grupohor_cup")%>&nbsp;</td>
    <td width="70"><%=rsCursos("identificador_cur")%>&nbsp;</td>
    <td width="250"><img src="../../../images/atencion.gif" alt="<%=rsCursos("nombre_cpf")%>">&nbsp;<%=rsCursos("nombre_cur")%>&nbsp;</td>
    <td width="27"><%=rsCursos("creditos_cur")%>&nbsp;</td>
    <td width="27"><%=ConvRomano(rsCursos("ciclo_cur"))%>&nbsp;</td>
    <td width="46"><%=rsCursos("vecescurso_dma")%>&nbsp;</td>
    <td width="38"><%=nota%>&nbsp;</td>
    <td width="10"><%=estadoDMA%>&nbsp;</td>
    <td width="10"><%=condicionDMA%>&nbsp;</td>
    <td width="80"><%=FormatDatetime(rsCursos("fechareg_dma"),2)%>&nbsp;</td>
    <td width="80"><%=rsCursos("observacion_dma")%>&nbsp;</td>
    <td width="80"><%=fechamod%>&nbsp;</td>
    <td width="80"><%=rsCursos("operadorReg_dma")%>&nbsp;</td>
    <td width="80"><%=rsCursos("operadorMod_dma")%>&nbsp;</td>
    <td width="80"><%=rsCursos("obsCambioGH_dma")%>&nbsp;</td>
    <td width="80"><%=rsCursos("obsretiro_dma")%>&nbsp;</td>
    
    <!--Agregado-->
    <% 
    colspan = 14'13
    if reactivar = 1 then 
            response.Write("<td width='80'>" & rsCursos("VacantesDisponibles")& "&nbsp;</td><td>")
            if (rsCursos("VacantesDisponibles")<=0) then 
                response.Write("<font color='red'>CERRADO</font>") 
            else 
                response.Write("ABIERTO")
            end if
            response.Write("&nbsp;</td>")
            colspan = 15
       end if  
    %>
    <!--Fin Agregado-->   
  </tr>
  	<%rsCursos.movenext
  	loop
  rsCursos.close
  Set rsCursos=nothing
  
  if (notacrd>0 AND totalcrd>0) then
  	ponderado=formatnumber(notacrd/totalcrd,2)
  end if
  %>
  <tr class="usatTablaInfo">
    <td width="100%" colspan="<%=colspan+3%>" align="right"><b>Cursos Matriculados: <%=totalmat%> |  Cursos Retirados: <%=totalret%> | Créditos Matriculados:<%=totalcrd%> | Cursos Inhabilitados:<%=NumInh%> | Créditos Inhabilitados:<%=CredInh%> &nbsp;&nbsp; | Promedio 
    Ponderado:<%= ponderado %></b></td>
    <!-- SumCrd/TotCrd -->

  </tr>
  	</table>
	<input type="hidden" name="txtCurMat" id="txtCurMat" value="<%=CurMat%>" />
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado Matrículas registradas para el Estudiante</h5>
<%end if%>
</body>

</html>
<%
If Err.Number <> 0 Then
    Response.Write ("Error: " & Err.Description& "<br><br>")         
end if
%>
