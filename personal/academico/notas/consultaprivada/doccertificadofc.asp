<!--#include file="../../../../funciones.asp"-->
<%
	tipo=request.querystring("tipo")
	codigo_alu=request.querystring("codigo_alu")
	codigo_pes=request.querystring("codigo_pes")
	tipoOrden=request.querystring("tipoOrden")
    %>
    <!--<script type="text/javascript" language="javascript">alert(<% response.write (codigo_pes) %>)</script>-->
    <%
    
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsHistorial=Obj.Consultar("GenerarCertificadoEstudios","FO",tipo,codigo_alu,codigo_pes)
	Obj.CerrarConexion
	Set Obj=nothing
	
	If (rsHistorial.BOF and rsHistorial.EOF) then%>
			<script language="Javascript">
				alert("No se han registrado Historial Académico para el estudiante seleccionado")
				history.back(-1)
			</script>
	<%else
		
		alumno=rsHistorial("alumno")
		nombre_esp=rsHistorial("especialidad")
		nombre_cpf=rsHistorial("escuela")
		nombre_fac=rsHistorial("facultad")
		codigouniver_alu=rsHistorial("codigouniver_alu")
		
		if rsHistorial("sexo_Alu")="M" then
		    genero="el señor"
		else
		    genero="la señorita"
		end if
		
		if rsHistorial("egresado_alu")=0 then
		    estadoestudiante="estudiante"
		else
		    if rsHistorial("sexo_Alu")="M" then
		        estadoestudiante="egresado"
		    else
		        estadoestudiante="egresada"
		    end if 
		end if
		
		Response.ContentType = "application/msword"
		Response.AddHeader "Content-Disposition","attachment;filename=" & codigouniver_alu & ".doc"
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<style>
<!--
.etiqueta    { font-weight: bold }
.cabezera    { font-weight: bold; text-align: center; background-color: #C0C0C0; border-left-width:1; border-right-width:1; border-top-style:solid; border-top-width:1; border-bottom-style:solid; border-bottom-width:1}
body         { font-family: Belwe Lt BT; font-size: 9pt }
td           { font-size: 9pt }
-->
</style>
</head>
<body style="margin-top:3cm">
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <THEAD>
  <tr><td colspan="6">
  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="130">
  <tr>
    <th style="font-size: 16pt" width="100%" colspan="2" height="50" align="center" valign="top">
        El Director Académico de la Universidad Católica Santo Toribio de Mogrovejo
    </th>
  </tr>
  <tr>
    <th style="font-size: 16pt" width="100%" colspan="2" height="50" align="center" valign="top">
 &nbsp;
    </th>
  </tr>
  <tr>
    <th style="font-size: 16pt" width="100%" colspan="2" height="50" align="center" valign="top">
    CERTIFICA
    </th>
  </tr>
   <tr>
    <th style="font-size: 16pt" width="100%" colspan="2" height="50" align="center" valign="top">
 &nbsp;
    </th>
  </tr>
    <tr>
    <th style="font-size: 16pt" width="100%" colspan="2" height="50" align="center" valign="top">
      <p style="text-align:justify">Que <%=genero%>&nbsp; <%=UCase(alumno)%>, con código de matrícula Nº <%=codigouniver_alu%>, <%=estadoestudiante%> de la Escuela de <%=nombre_cpf%>, Facultad de <%=nombre_fac%> de nuestra Universidad, ha acreditado las Actividades de Formación Complementaria, según la siguiente descripción: </p>
    </th>
  </tr> 
  </table>
  </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr>
    <td class="cabezera" height="14" width="20%">Semestre Académico</td>
    <td class="cabezera" height="14" width="60%">Actividad</td>
    <td class="cabezera" height="14" width="20%">Créditos</td>
    
  </tr>
  </THEAD>
  <%
  Semestre=rsHistorial(tipoOrden) 'Capturar el 1er Semestre
  CicloA=rsHistorial("ciclo_cur") 'Capturar el 1er Ciclo

  totalcrd=0:notacrd=0:totalcursos=0:NumConvalidaciones=0
  
  Do while not rsHistorial.eof
	  	  	
		totalcursos=totalcursos+1
		if tipo =15 or tipo=16 then
		    if rsHistorial("condicion_Dma") ="A" then
		        totalcrd=totalcrd + cdbl(rsHistorial("creditocur_dma")) 'Sumatoria de Créditos matriculados aprobados
		        notacrd=notacrd + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Crédito(Calculado)
		    elseif rsHistorial("condicion_Dma") ="D" then
		            totalcrdDes=totalcrdDes+ cdbl(rsHistorial("creditocur_dma")) 'Sumatoria de Créditos matriculados desaprobados
		    else
		            notacrd=notacrd + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Crédito(Calculado)
		    end if 
		else		
		    totalcrd=totalcrd + cdbl(rsHistorial("creditocur_dma")) 'Sumatoria de Créditos matriculados
		    notacrd=notacrd + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Crédito(Calculado)
		end if 

	if tipo <> 22 then
		
		If Semestre<>rsHistorial(tipoOrden) then
	  		Semestre=rsHistorial(tipoOrden)
	  		response.write "<tr><td colspan=""6"">&nbsp;</td></tr>"
  		End if
  	else
		If CicloA<>rsHistorial("ciclo_cur") then
	  		CicloA=rsHistorial("ciclo_cur")
	  		response.write "<tr><td colspan=""6"">&nbsp;</td></tr>"
  		End if
	end if	

  		if rsHistorial("tipoMatricula_dma")="C" then
  			NumConvalidaciones=NumConvalidaciones+1
  		end if
  %>
  <tr>
    <td height="14" width="20%"><%=rsHistorial("descripcion_cac")%></td>
    <td height="14" width="60%"><%=rsHistorial("nombre_cur")%></td>
    <td align="center" height="14" width="20%"><%=rsHistorial("creditoCur_Dma")%></td> 
  </tr>
  	<%rsHistorial.movenext
  loop%>
  </table><p>
  <%if NumConvalidaciones>0 then%>
    	<b>(*)Asignaturas convalidadas</b>
    	   <%end if%>	
  <b> <br />


 </BR>
  <%if tipo = 15 or tipo = 16 then%>
        <font color='red'>        
        (**)Asignaturas desaprobadas en 
        la Universidad Católica Santo Toribio de Mogrovejo, la nota mínima aprobatoria 
        es 14. </font>
   <%end if %>     
  </b></p>
  <br/>

<p align="right"><b>Chiclayo, <%=QuitarDia(formatdatetime(date,1))%></b></p>
<p>&nbsp;</p>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="30%" align="right" class="etiqueta">
  <tr>
    <td width="100%" align="center">Ing. Willy Augusto Oliva Tong
</td>
  </tr>
  <tr>
    <td width="100%" align="center">Director Académico</td>
  </tr>
</table>
</body>
</html>
	<%end if
Set rsHistorial=nothing
%>