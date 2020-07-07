<!--#include file="../../../../funciones.asp"-->
<%
'response.write (session("Usuario_bit"))

'on error resume next

dim pagina
accion=request.querystring("accion")
codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
botonAgregado = false
HayReg=false


if codigo_alu<>"" and trim(modo)="resultado" then
'determinar si el alumno tiene alguna categorizacion
	'if (cdbl(session("codigo_cpf"))=4 or cdbl(session("codigo_cpf"))=11 or cdbl(session("codigo_cpf"))=3) then 
	'	response.redirect "../mensajes.asp?proceso=B"
	'end if

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion		
		Set rsMatricula=Obj.Consultar("ConsultarAccesoMatriculaPorAsesor","FO",codigo_alu,session("codigo_cac"),0,"administrar/mantenimiento",session("codigo_tfu"))
		Set rsCronograma=Obj.Consultar("ConsultarCronograma","FO", "MY", session("codigo_cac"))
		Set rsSeparacion=Obj.Consultar("ACAD_ConsultarSeparacionVigente","FO",codigo_alu)		        
        Set rsMensajes=obj.Consultar("VerificarAccesoMatriculaEstudiante_V2","FO","PRO",session("codigo_alu"),session("codigo_cac"))                     
        'Set rsMensajes=obj.Consultar("VerificarAccesoMatriculaEstudiante_PP","FO","PRO",56984,56,1)                             
        set rsAlumno=Obj.Consultar("ConsultarAlumno", "FO", "AUN", codigo_alu)
		Obj.CerrarConexion		
	Set obj=nothing		
	
	Dim rsMensajes 
	Set ObjM= Server.CreateObject("PryUSAT.clsAccesoDatos")
		ObjM.AbrirConexion		
	    Set rsMensajes=ObjM.Consultar("VerificarAccesoMatriculaEstudiante_PP","FO","PRO",56984,56,144) 
		ObjM.CerrarConexion		
	Set ObjM=nothing		
	
	if Not(rsMatricula.BOF AND rsMatricula.EOF) then
	    
	    botonAgregado = true
	    
		pagina=rsMatricula("pagina")
		HayReg=true
		alto="99%"	
	'elseif int(session("preciocreditoact_alu"))=0 and (int(session("codigo_cpf"))<>19 and int(session("codigo_cpf"))=25)  then
	'	response.redirect("../mensajes.asp?proceso=CAT")
	end if	
	'response.write ("xxx" & pagina)
	
	tieneSeparacion= 0
	motivoSeparacion =""
	if Not(rsSeparacion.BOF and rsSeparacion.EOF) then
	    tieneSeparacion= 1
	    if rsSeparacion("codigo_tse") =1 then
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " desde " & rsSeparacion("fechaIni_sep") & " hasta " & rsSeparacion("fechafin_sep") & " por motivo: " & "<b>" & rsSeparacion("motivo_sep") & "</b>"
	    else
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " por motivo: <b>" & rsSeparacion("motivo_sep") & "</b>"
	    end if 
	end if 
    
end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<META HTTP-EQUIV="Last-modified:" CONTENT="11-April-07">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Mantenimiento de matrículas del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarfichamatricula.js?x=1"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/lbox/thickbox.js"></script>
<link rel="stylesheet" href="../../../../private/jq/lbox/thickbox.css" type="text/css" media="screen" /> 
<script language="javascript">
var aplicacionBD = new Array()
function ObjAplicacion(ciclo,cm,fecha,tipo,obs,crd,emat)
	{
		this.ciclo=ciclo
		this.cm=cm
	  	this.fecha = fecha
	  	this.tipo = tipo
	  	this.obs = obs
	  	this.crd = crd
	  	this.emat=emat
	}
	
	function AgregarItemObjeto()
	{	
		<%
		if HayReg=true then
		Do while Not rsMatricula.EOF
			response.write "aplicacionBD[aplicacionBD.length] = new ObjAplicacion('" & rsmatricula("codigo_cac") & "','" & rsmatricula("codigo_mat") & "','" & rsmatricula("fecha_mat") & "','" & rsmatricula("tipo_mat") & "','" & rsmatricula("observacion_mat") & "','" & rsmatricula("preciocredito_mat") & "','" & rsmatricula("estado_mat") & "')" & vbNewLine & vbtab & vbtab
			rsMatricula.movenext
		Loop
		end if
		%>
	}

	function AbrirMatricula(i,pagina)
	{
		AgregarItemObjeto()
		var ciclo=aplicacionBD[i].ciclo
		var cm=aplicacionBD[i].cm
		var fecha=aplicacionBD[i].fecha
		var tipo=aplicacionBD[i].tipo
		var obs=aplicacionBD[i].obs
		var crd=aplicacionBD[i].crd
		var emat=aplicacionBD[i].emat
	
		txtestado_mat.value=emat
		txtciclo_mat.value=ciclo
		
	    fradetalle.location.href=pagina + "?codigo_mat=" + cbocodigo_mat.value + "&codigo_cac=" + ciclo + "&descripcion_cac=" + cbocodigo_mat.options[cbocodigo_mat.selectedIndex].text
	}

</script>
</head>
<body onload="document.all.txtcodigouniver_alu.focus();<%if HayReg=true and tieneSeparacion = 0 then%>AbrirMatricula('0','detallematricula.asp')<%end if%>">
<input type="hidden" id="txtestado_mat" value="P">
<input type="hidden" id="txtciclo_mat">
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="<%=alto%>">
	<%if (codigo_alu="") then%>
    <tr>
      <td width="50%" class="usattitulo" style="height: 4%">Mantenimiento de 
		Matrículas</td>
      <td width="50%" style="height: 4%">
     	<%call buscaralumno("matricula/administrarPP/frmadminmatricula.asp","../../",request.querystring("mod"))%>
      </td>
    </tr>
    <%else   %>
    <tr>
      <td width="100%" class="etiqueta" valign="top" height="15%">
      <!--#include file="../../fradatos.asp"-->
      </td>
    </tr>
    <%   if tieneSeparacion = 1 then 
    %>
    <tr>
      <td width="100%" class="etiqueta" valign="top" height="85%">
	    <table align="center" bgcolor="#EEEEEE" style="width: 80%;height:10%" cellpadding="3" class="contornotabla_azul">
		<tr>
			<td valign="middle" align="center">
			<img alt="Mensaje" src="../../../../Images/menus/noconforme_1.gif" 
                    style="height: 46px; width: 47px"></td>
			<td>
				El estudiante tiene <%=motivoSeparacion%> <br/><br/>
				Por lo cual no podrá matricularse para el semestre. Cualquier duda consulte con el 
                Director de Escuela
            </td>
		</tr>
		</table>
	   </td>
    </tr>
	<% else %>	   
    <tr>
      <td   valign="top"  height="3%">
	  <%   
            if (rsMensajes.BOF = false and rsMensajes.EOF = false)  then
                response.Write("<font color='#000000' size='1'>  El estudiante " & rsAlumno("apellidoPat_Alu") & " " & rsAlumno("apellidoMat_Alu") & " " & rsAlumno("nombres_Alu")  & " tiene<font color='#FF0000' size='2'> BLOQUEOS</font> activos. <a href='BloqueosMatricula.aspx?cac=" & session("codigo_cac") & "&alu=" & codigo_alu & "&KeepThis=true&TB_iframe=true&height=495&width=500&modal=true' title='Ver Bloqueos' class='thickbox'><font color='#FF0000' style='text-decoration:blink'>(Clic aquí para ver los Bloqueos)</font></a></font>")
            end if    
      %>
	  </td>
    </tr>
    <tr>
      <td width="100%" valign="top" height="3%" colspan="2" class="contornotabla">
			<table border="0" width="100%" style="border-collapse: collapse;">
				<%if HayReg=true then%>
				<tr bgcolor="#FFFFCC">
					<td width="20%" class="etiqueta">Ciclo Académico</td>
					<td width="15%">
					<%
					rsMatricula.movefirst
					Call llenarlista("cbocodigo_mat","ResaltarPestana2('0','','');AbrirMatricula(this.selectedIndex,'detallematricula.asp')",rsMatricula,"codigo_mat","descripcion_cac",0,"","","")
					%>	
					</td>
					<td width="65%" align="right" class="rojo">
					
					<% Dim ciclo 
					   Dim cod_cac
					   cod_cac = 0
					   ciclo = session("descripcion_cac")
					   if pagina="" then					        
					        if rsMensajes.recordCount= 0 then						            
    					        if Not(rsCronograma.BOF AND rsCronograma.EOF) then
    					            ciclo = rsCronograma("descripcion_cac")
	                                cod_cac = rsCronograma("codigo_cac")	                                
	                                if session("Ultimamatricula")<>session("descripcion_cac") then%>
						                <input onClick="modificarmatricula('N','<%=session("codigo_pes")%>', '<%=cod_cac%>')" type="button" value="     Nueva Matrícula <%=ciclo%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px">
					        <%	    end if
                                else
                            %>
                                    Ha finalizado la fecha de matricula					    
    					            <input onClick="modificarmatricula('N','<%=session("codigo_pes")%>', '<%=cod_cac%>')" type="button" value="     Nueva Matrícula <%=ciclo%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px" >					                    					        
    					    <%  end if
					        end if					        	                    
					    else
						    response.redirect(pagina)
					    end if
					 %>
					</td>
				</tr>
				<%end if%>
			</table>
	  </td>
    </tr>
    <%if HayReg=true then%>
    <tr>
      <td valign="top" height="70%" colspan="2">
		<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="22%" onclick="ResaltarPestana2('0','','');AbrirMatricula(cbocodigo_mat.selectedIndex,'detallematricula.asp')">
                    Asignaturas Matriculadas</td>
					<td width="1%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('1','','');AbrirMatricula(cbocodigo_mat.selectedIndex,'vsthorario.asp')">
                    Horarios</td>
                    <td width="1%" class="bordeinf">&nbsp;</td>
                    <td class="pestanabloqueada" id="tab" align="center" width="15%" onclick="ResaltarPestana2('2','','');fradetalle.location.href='../../../../librerianet/academico/admincuentaper.aspx?id=<%=session("codigo_alu")%>&VerDatos=0'">
                        Estado de Cuenta</td>
                    <td width="46%" class="bordeinf" align="right">
					&nbsp;
                    </td>
				</tr>
	  			<tr height="95%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>      
      </td>
    </tr>
	<%else%>
	<tr>
		<td>
			<input onClick="modificarmatricula('N','<%=session("codigo_pes")%>')" type="button" value="     Nueva Matrícula <%=session("descripcion_cac")%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px">
		</td>
	</tr>
	<%end if%>
	<%Set rsMatricula=nothing
	end if 
 end if
 set obj= nothing 
 
 'if Err.number <> 0 then
    'response.Write "ERROR: " & Err.Description
 'end if
 
 %>
</table>
</body>
</html>