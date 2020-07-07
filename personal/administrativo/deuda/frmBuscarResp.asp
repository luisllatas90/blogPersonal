<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>

<body>
<% 

Dim objCon
Dim rsCon
Dim tipoResp
Dim tipoBus
Dim param
tipoResp=request.form("cboTipoResp")
tipoBus=request.form("cboTipoBus")
param=request.form("txtParam")

if (tipoResp <> "N"  AND tipoBus <> "N") then

	if tipoResp="E" then
		Set objCon= Server.CreateObject("PryUSAT.clsDatAlumno")
		Set rsCon= Server.CreateObject("ADODB.RecordSet")
		
		if tipoBus="C" then
			Set rsCon= objCon.ConsultarAlumno("RS","CU",param)
		end if

		if tipoBus="P" then
			Set rsCon= objCon.ConsultarAlumno("RS","AP",param)
		end if

		if tipoBus="M" then
			Set rsCon= objCon.ConsultarAlumno("RS","AM",param)
		end if

		if tipoBus="D" then
			Set rsCon= objCon.ConsultarAlumno("RS","DNI",param)
		end if

		if rsCon.recordcount >0 then
			responsable=rsCon("Alumno")
			carrera =rsCon("nombre_Cpf")
		else
			responsable=""
			carrera =""
	
		end if

		
		
	end if
	
	if tipoResp="P" then

	end if
	
	if tipoResp="V" then

	end if


end if

%>
<form action="frmBuscarResp.asp" method="post" name="frmBuscarResp" id="frmBuscarResp" >
  <table width="75%" border="0" align="center" height="138">
    <!--DWLayoutTable-->
    <tr> 
      <td height="23" colspan="3" valign="top"><div align="center">
        <p class="usatTitulo">Buscar Deudas</div></td>
    </tr>
    <tr> 
      <td width="211" height="17"></td>
      <td width="174" height="17"></td>
      <td width="346" height="17"></td>
    </tr>
    <tr> 
      <td height="28" class="usatCabeceraCelda"><div align="right">Tipo de Responsable:</div>
        </td>
      <td colspan="2" valign="top" height="28"><select name="cboTipoResp" id="cboTipoResp">
          <option value="N" selected>--- Seleccione Tipo de Responsable ---</option>
          <option value="E">Estudiante</option>
          <option value="T">Trabajador</option>
          <option value="O">Otros</option>
        </select></td>
    </tr>
    <tr> 
      <td height="22" class="usatCabeceraCelda"><div align="right">Buscar Segun:</div></td>
      <td colspan="2" valign="top" height="22"><select name="cboTipoBus" id="cboTipoBus">
          <option value="N" selected>--- Seleccione Tipo de Busqueda ---</option>
          <option value="C">Codigo del Responsable</option>
          <option value="P">Apellido Paterno</option>
          <option value="M">Apellido Materno</option>
          <option value="D">DNI</option>
        </select></td>
    </tr>
    <tr> 
      <td height="28" class="usatCabeceraCelda"> <div align="right">
          Parametro de Busqueda:</div></td>
      <td height="28"> <div align="left"> 
          <input name="txtParam" type="text" id="txtParam" size="20">
        </div></td>
      <td valign="top" height="28"><input name="cmdBuscar" type="submit" id="cmdBuscar" value="Buscar" class="usatCmdboton"></td>
      </tr>
  </table>
 
  <table width="75%" border="1" align="center">
    <% if tiporesp="E"  AND tipoBus <> "N" then %>
    <tr> 
      <th width="16%" class="usatCabeceraCelda">Código</th>
      <th width="38%" class="usatCabeceraCelda">Apellidos y Nombres</th>
      <th width="27%" class="usatCabeceraCelda">Escuela Profesional</th>
      <th> </th>
    </tr>
    <%
		  
		do while not rsCon.eof %>
    <tr> 
      <td class="usatFormatoCelda"><%=rsCon("codigoUniver_Alu")%> &nbsp;</td>
      <td class="usatFormatoCelda"><%=rsCon("Alumno")%> &nbsp;</td>
      <td class="usatFormatoCelda"><%=rsCon("nombre_Cpf")%> &nbsp;</td>
      <td width="19%" class="usatLink"><a href="frmRegistrarDeuda.asp?id= <%=rsCon("codigo_Alu")%>&tr=<%=tipoResp%>">Registrar 
        Deuda</a> </td>
      <% rsCon.movenext %>
    </tr>
    <% loop%>
    <%
	 rsCon.close
	 set rsCon= Nothing
	 set objCon= Nothing
%>
    <% end if%>
  </table>
</form>
</body>
</html>