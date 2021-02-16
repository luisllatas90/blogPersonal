<!--#include file="../../../../funciones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if
'response.write (session("codigo_cac"))

dim pagina
accion=request.querystring("accion")
codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
HayReg=false

'session("codigo_cac")=61
'session("descripcion_cac")="2017-II"

Set objCicloActual=Server.CreateObject("PryUSAT.clsAccesoDatos")	
objCicloActual.AbrirConexion
Set rsCicloActual=objCicloActual.Consultar("ACAD_RetornaCicloVigenteTipoEstudio","FO",2)	
objCicloActual.CerrarConexion

if Not(rsCicloActual.BOF and rsCicloActual.EOF) then	
    session("codigo_cac") = rsCicloActual("codigo_Cac")
    session("descripcion_cac") = rsCicloActual("descripcion_Cac")
    session("tipo_cac") = rsCicloActual("tipo_Cac")
end if

ON ERROR resume next
if codigo_alu<>"" and trim(modo)="resultado" then
'determinar si el alumno tiene alguna categorizacion
	'if (cdbl(session("codigo_cpf"))=4 or cdbl(session("codigo_cpf"))=11 or cdbl(session("codigo_cpf"))=3) then 
	'	response.redirect "../mensajes.asp?proceso=B"
	'end if
	'response.redirect("../verificaraccesomatricula.asp?rutaActual=../academico/matricula/mantenimiento")
	
	'Z:\personal\academico\matricula\mantenimiento\frmadminmatricula.asp
	'apto="S"       
	
	Set ObjBloqueo=Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjBloqueo.AbrirConexion
	Set rsBloqueo=ObjBloqueo.Consultar("ConsultarAccesoMatriculaPorAsesorBloqueoAlu_2015","FO",codigo_alu,session("codigo_cac"),"administrar/mantenimiento",session("codigo_tfu"))
	
	if(rsBloqueo(0) <> "") then
	    pagina2=rsBloqueo(0)
	end if
	
	ObjBloqueo.CerrarConexion
	set ObjBloqueo = nothing
	response.Write(pagina)
	
	if (pagina2<>"") then			
		response.redirect("../"&pagina2)
		'response.write(pagina2)
	end if

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	Set rsMatricula=Obj.Consultar("ConsultarAccesoMatriculaPorAsesor_2015","FO",codigo_alu,session("codigo_cac"),0,"administrar/mantenimiento",session("codigo_tfu"))
	Set rsCronograma=Obj.Consultar("ConsultarCronograma","FO", "ME", session("codigo_cac"))
	Set rsSeparacion=Obj.Consultar("ACAD_ConsultarSeparacionVigente","FO",codigo_alu)
	Obj.CerrarConexion
	Set obj=nothing
	
	if Not(rsMatricula.BOF AND rsMatricula.EOF) then
	    if(rsMatricula(0) <> "") then
	        pagina=rsMatricula("pagina")
	    end if		
		HayReg=true
		alto="99%"	
	'elseif int(session("preciocreditoact_alu"))=0 and (int(session("codigo_cpf"))<>19 and int(session("codigo_cpf"))=25)  then
	'	response.redirect("../mensajes.asp?proceso=CAT")
	    if(pagina <> "") then
	        response.redirect ("../" & pagina)
	    end if
	end if	
	
	
	tieneSeparacion= 0
	motivoSeparacion =""
	if Not(rsSeparacion.BOF and rsSeparacion.EOF) then
	    'tieneSeparacion= 1 'comentado por yperez 10.01.2018
	    if rsSeparacion("codigo_tse") =1 then
	        tieneSeparacion=1 'Bloquear si es temporal sin revocacion ID:38642  solicitado por marilin
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " desde " & rsSeparacion("fechaIni_sep") & " hasta " & rsSeparacion("fechafin_sep") & " por motivo: " & "<b>" & rsSeparacion("motivo_sep") & "</b>"
	    else
	        tieneSeparacion= 1 'solo bloquear si es definitiva- desde 2018-0 yperez 10.01.2018
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " por motivo: <b>" & rsSeparacion("motivo_sep") & "</b>"
	    end if 
	end if 
 
end if
%>
<%


''	if (pagina2<>"") then	
''		response.redirect(pagina2)
''	end if
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Mantenimiento de matrículas del estudiante</title>
<script type="text/javascript" language="JavaScript" src="../../matricula/administrar/private/jquery.js"/></script>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula2015.js?v=<% response.Write DateDiff("s", "12/31/1969 00:00:00", Now) %>"></script>
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
		AgregarItemObjeto();
		var ciclo=aplicacionBD[i].ciclo;
		var cm=aplicacionBD[i].cm;
		var fecha=aplicacionBD[i].fecha;
		var tipo=aplicacionBD[i].tipo;
		var obs=aplicacionBD[i].obs;
		var crd=aplicacionBD[i].crd;
		var emat=aplicacionBD[i].emat;
	
		txtestado_mat.value=emat;
		txtciclo_mat.value=ciclo;
		
	   // fradetalle.location.href=pagina + "?codigo_mat=" + cbocodigo_mat.value + "&codigo_cac=" + ciclo + "&descripcion_cac=" + cbocodigo_mat.options[cbocodigo_mat.selectedIndex].text;
	   //fradetalle.location.href="HTTPS//WWW.GOOGLE.COM.PE";
	   $("#fradetalle").attr("src", pagina + "?codigo_mat=" + cbocodigo_mat.value + "&codigo_cac=" + ciclo + "&descripcion_cac=" + cbocodigo_mat.options[cbocodigo_mat.selectedIndex].text);
	}

</script>
<script type="text/javascript" language="JavaScript" src="../private/analytics-personal.js"></script>
</head>
<body onload="document.all.txtcodigouniver_alu.focus();<%if HayReg=true and tieneSeparacion = 0 then%>AbrirMatricula('0','detallematricula2015.asp')<%end if%>">
<input type="hidden" id="txtestado_mat" value="P">
<input type="hidden" id="txtciclo_mat">
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="<%=alto%>">
	<%if (codigo_alu="") then%>
    <tr>
      <td width="50%" class="usattitulo" style="height: 4%">Mantenimiento de 
		Matrículas</td>
      <td width="50%" style="height: 4%">
     	<%call buscaralumno("matricula/mantenimiento/frmadminmatricula2015.asp","../../",-1)%>
      </td>
    </tr>
    <%else   %>
    <tr>
      <td width="100%" class="etiqueta" valign="top" height="15%">
      <!--#include file="../../fradatos.asp"-->
      </td>
    </tr>

    <%   
  
    if tieneSeparacion = 1 then 
    
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
      <td width="100%" valign="top" height="3%" colspan="2" class="contornotabla">
			<table border="0" width="100%" style="border-collapse: collapse;">
				<%if HayReg=true then%>
				<tr bgcolor="#FFFFCC">
					<td width="20%" class="etiqueta">Semestre Académico</td>
					<td width="15%">
					<%
					rsMatricula.movefirst
					Call llenarlista("cbocodigo_mat","ResaltarPestana2('0','','');AbrirMatricula(this.selectedIndex,'detallematricula2015.asp')",rsMatricula,"codigo_mat","descripcion_cac",0,"","","")
					%>	
					</td>
					<td width="65%" align="right" class="rojo">
					
					<%if pagina="" then
	                    if Not(rsCronograma.BOF AND rsCronograma.EOF) then
						    if session("Ultimamatricula")<>session("descripcion_cac") then%>
						        <input onclick="modificarmatricula('N','<%=session("codigo_pes")%>')" type="button" value="     Nueva Matrícula <%=session("descripcion_cac")%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px" />
					<%	    end if
					    else %>
					            Ha finalizado la fecha de matricula					    
					            <input onclick="modificarmatricula('N','<%=session("codigo_pes")%>')" type="button" value="     Nueva Matrícula <%=session("descripcion_cac")%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px" />
					<%  end if 
					else
						response.redirect(pagina)
					end if%>
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
					<td class="pestanaresaltada" id="tab" align="center" width="22%" onclick="ResaltarPestana2('0','','');AbrirMatricula(cbocodigo_mat.selectedIndex,'detallematricula2015.asp')">
                    Asignaturas Matriculadas</td>
					<td width="1%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('1','','');AbrirMatricula(cbocodigo_mat.selectedIndex,'vsthorario.asp')">
                    Horarios</td>
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
			<input onclick="modificarmatricula('N','<%=session("codigo_pes")%>')" type="button" value="     Nueva Matrícula <%=session("descripcion_cac")%>" name="cmdAgregarMatricula" class="agregar2" style="width: 140px" />
		</td>
	</tr>
	<%end if%>
	<%Set rsMatricula=nothing
	end if 
 end if%>
</table>
</body>
</html>
<% 
If Err.Number <> 0 Then
   response.Write Err.Description   
End If
 %>