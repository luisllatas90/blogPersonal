<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle del Curso</title>
<link rel="stylesheet" type ="text/css" href="../../../private/estilo.css">  
<%
	
	dim Obj , rs , tipomatricula , motivoagregado
	codigo_dma= request.querystring("codigo_dma")
	codigouniver_alu= request.querystring("codigouniver_alu")
	
	
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.abrirconexion
	set rs=obj.consultar("dbo.ConsultarMatricula","FO","CDM",codigo_dma,"","","")
	obj.cerrarconexion
	dim idtipomatricula
	idtipomatricula=rs.fields("tipomatricula_dma").value 

	select 	case idtipomatricula	
		case "A" :
			tipomatricula="Agregado"

		case "N" :
			tipomatricula="Normal"			

		case "U" :
			tipomatricula="Examen de ubicación"			
		case "C" :
			tipomatricula="Convalidación"			

		case "S" :
			tipomatricula="Examen de suficiencia"			
			
	end select
	motivoAgregado=rs("agregado")
	motivoRetiro=rs("retiro")
	fechaAgregado=rs("fechareg_dma")
	fechaRetiro=rs("fechamod_dma")
	operadorRetiro =rs("operadormod_dma")
	operadorAgregado =rs("operadorreg_dma")
	obsAgregado=rs("obsagregado_dma")
	obsRetiro=rs("obsretiro_dma")
	
	if rs("Estado_dma")="R" then
		EstadoActual="Retirado"
	else
		EstadoActual="Matriculado"
	end if 

	'tipomatricula="No definido"
%>
</head>

<body>

<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr class= "usattitulo" height="5">
    <td width="898" height="26" colspan="6"><b>Detalles del Curso [ <%= rs("nombre_cur") & "      .::. Grupo horario : "  & rs("grupohor_cup") %> ]</b></td>
  </tr>
  <tr>
    <td width="134" height="1" class="usatceldatitulo"><b>&nbsp;Estudiante&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</td>
    <td height="29" width="265">&nbsp;<%=rs("NombresApellidos")%></td>
    <td class="usatceldatitulo" height="1" width="128">Código Universitario :</td>
    <td height="29" colspan="3" width="261">&nbsp;<%=rs("codigouniver_Alu")%></td>
  </tr>
  <tr>
    <td width="134" height="25" class="usatceldatitulo">&nbsp;Carrera 
    Profesional :</td>
    <td height="25" colspan="5" width="656"> &nbsp;<%=rs("nombre_Cpf")%></td>
  </tr>
  <tr>
    <td width="134" height="24" class="usatceldatitulo"><b>&nbsp;Fecha de Registro&nbsp;&nbsp;&nbsp; 
    :</b></td>
    <td width="265" height="24">&nbsp;<%=rs.fields("fechareg_dma")%></td>
    <td width="128" height="24" class="usatceldatitulo"><b>&nbsp;Registrado por :</b></td>
    <td width="261" height="24" colspan="3">&nbsp;<%=operadorAgregado%></td>
  </tr>
  <tr>
    <td width="134" height="24" class="usatceldatitulo"><b>&nbsp;Tipo de Matrícula&nbsp;&nbsp;&nbsp;&nbsp; 
    :</b></td>
    <td width="656" height="24" colspan="5">&nbsp;<%=tipomatricula%></b><b>&nbsp;</b></td>
  </tr>
  <tr>
    <td width="134" height="24" class="usatceldatitulo"><b>&nbsp;Estado Actual&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    :</b></td>
    <td width="656" height="24" colspan="5">&nbsp;<b><%=EstadoActual%></b></td>
  </tr>
  <%  if idtipomatricula="A" then
  
  %>
  <tr>
    <td width="134" height="38" class="usatceldatitulo"><b>&nbsp;Motivo de Agregado&nbsp; 
    :</b></td>
    <td width="265" height="38">&nbsp;<%=motivoAgregado%></td>
    <td width="128" height="38" class="usatceldatitulo"><b>&nbsp;Operador de Agregado :</b></td>
    <td width="92" height="38">&nbsp;[ <%=operadorAgregado%> ] </td>
    <td width="74" height="38" class="usatceldatitulo"> Fecha de Agregado&nbsp; :</td>
    <td width="93" height="38">&nbsp;<%=fechaAgregado%></td>
  </tr>
  
  <tr>
    <td width="134" height="52" class="usatceldatitulo"><b>&nbsp;Obs. Agregado&nbsp; 
    :</b></td>
    <td width="656" height="52" colspan="5"><b> <%=obsAgregado%></b></td>
  </tr>
  <%
  end if 
  if rs("Estado_dma")="R" then
  %>
  <tr>
    <td width="134" height="35" class="usatceldatitulo"><b>&nbsp;Motivo de Retiro&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    :</b></td>
    <td width="265" height="35">&nbsp;<%=motivoRetiro%></td>
    <td width="128" height="35" class="usatceldatitulo"><b>&nbsp;Operador de&nbsp; Retiro :</b></td>
    <td width="92" height="35">&nbsp;<%=operadorRetiro%></td>
    <td width="74" height="35" class="usatceldatitulo"><b> Fecha de Retiro&nbsp;&nbsp;&nbsp; 
    :</b></td>
    <td width="93" height="35">&nbsp;<%=fechaRetiro%></td>
  </tr>
  <tr>
    <td width="134" height="83" class="usatceldatitulo"><b>&nbsp;Obs. Retiro :</b></td>
    <td width="656" height="83" colspan="5">&nbsp;<%=obsRetiro%></td>
  </tr>
    <%
  end if
  %>

</table>

</body>

</html>