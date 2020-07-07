<!--#include file="clsrecepcion.asp"-->
<%
accion=request.querystring("accion")
idmovimiento=request.querystring("idmovimiento")
idarchivo=request.querystring("idarchivo")
numeroexpediente=request.querystring("numeroexpediente")


Dim archivo

	set archivo=new clsrecepcion
	
	if accion="modificarmovimiento" then
		ArrDatos=archivo.ConsultarArchivosRegistrados("6",idmovimiento,0,0)
		if IsEmpty(ArrDatos)=false then
			fecha=Arrdatos(0,0)
			hora=Arrdatos(1,0)
			ProcesarFechas fecha & " " & hora,now
			idareaarchivo=Arrdatos(2,0)
			idareaarchivo2=Arrdatos(3,0)
			otrodestino=Arrdatos(4,0)
			numcargo=Arrdatos(5,0)
			motivo=Arrdatos(6,0)
			confirmacion=Arrdatos(7,0)
			nombreareaarchivo=Arrdatos(8,0)
			nombreareaarchivo2=Arrdatos(9,0)
		end if
	else
		if trim(session("Usuario_bit"))="USAT\jvillalobos" then
			idareaarchivo=98
			nombreareaarchivo="Mesa de partes"
			idareaarchivo2=74
			nombreareaarchivo2="-No definido-"
		end if
		ProcesarFechas now,now
	end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Registro de envio de documentos a su Destinatario(s)</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validararchivo.js"></script>
</head>
<body topmargin="0" leftmargin="0" bgcolor="#EEEEEE">
<FORM NAME="frmmovimiento" METHOD=POST ACTION="procesar.asp?accion=<%=accion%>&idarchivo=<%=idarchivo%>&numeroexpediente=<%=numeroexpediente%>&idmovimiento=<%=idmovimiento%>">
<table width="100%" cellspacing="0" cellpadding="3" border="0" style="border-collapse: collapse" bordercolor="#111111">
	  <tr>
	  	<td width="100%" colspan="3" bgcolor="#E9E9E9">
        <input type="submit" value="Enviar" class="guardar"> 
        <input type="button" value="Cancelar" name="Cancelar" class="salir" onclick="window.close()"></td>
	  </tr>
	  <tr>
	  	<td width="20%" >Número Expediente</td>
	  	<td width="80%"  class="e1" colspan="2"><%=numeroexpediente%></td>
	  </tr>
	  <tr>
	  	<td width="20%" >Fecha de envío</td>
	  	<td width="80%"  colspan="2">    <select  name="dia">
	<%for i=1 to 31%>
		<option value="<%=iif(len(i)=1,"0" & i,i)%>" <%=seleccionar(Diai,i)%>><%=iif(len(i)=1,"0" & i,i)%></option>
	<%next%>
    </select> <select name="mes">
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=left(MonthName(m, False),3)%></option>
  	<%next%>
    </select> <%=anioi%>&nbsp; a las
    <select name="hora" onChange="MedioDiaHoraInicio()">
    <%for i=1 to 12%>
		<option value="<%=i%>" <%=seleccionar(horai,i)%>><%=i%></option>
	<%next%>
    </select> <select name="min">
    <%for i=0 to 60%>
		<option value="<%=iif(len(i)=1,"0" & i,i)%>" <%=seleccionar(mini,i)%>><%=iif(len(i)=1,"0" & i,i)%></option>
	<%next%>
    </select><select name="turno">
    <option value="AM" <%=seleccionar(turnoi,"a.m.")%>>a.m.</option>
    <option value="PM" <%=seleccionar(turnoi,"p.m.")%>>p.m.</option>
    </select></td>
	  </tr>
  <tr>
   	<td width="20%" >Área Orígen:
   	</td>
   	<td width="75%">
   	<input type="hidden" name="idareaarchivo" id="idareaarchivo" value="<%=idareaarchivo%>">
   	<input class="Cajas" type="text" name="txtareaarchivo" id="txtareaarchivo" value="<%=nombreareaarchivo%>" disabled="true">
    </td>
   	<td width="5%">
	<img style="cursor:hand" border="0" src="../../../images/resultados.gif" onClick="AbrirPopUp('frmbuscadorprop.asp?tabla=origen&criterio='+frmmovimiento.txtareaarchivo.value,'300','450','no','no','no')"></td>
  </tr>
	<tr>
	<td width="20%" >Área Destino:
	</td>
	<td width="75%">
	<input type="hidden" name="idareaarchivo2" id="idareaarchivo2" value="<%=idareaarchivo2%>">
	<input class="Cajas" type="text" name="txtareaarchivo2" id="txtareaarchivo2" value="<%=nombreareaarchivo2%>" disabled="true">
	</td>
	<td width="5%">
	<img style="cursor:hand" border="0" src="../../../images/resultados.gif" onClick="AbrirPopUp('frmbuscadorprop.asp?tabla=destino&criterio='+frmmovimiento.txtareaarchivo2.value,'300','450','no','no','no')"></td>
	</tr>

	  <tr>
	  	<td width="20%" >Nº de Cargo</td>
	  	<td width="80%"  colspan="2">
    <input type="text" name="numcargo" size="35" class="cajas" value="<%=numcargo%>"></td>
	  </tr>
	  <tr>
	  	<td width="20%" >Otro destino:</td>
	  	<td width="80%"  colspan="2">
    <input type="text" name="otrodestino" size="35" value="<%=otrodestino%>" style="width: 90%" class="cajas"></td>
	  </tr>
	  <tr>
	  	<td width="20%"  valign="top">Motivo</td>
	  	<td width="80%"  colspan="2">
    <input type="text" name="motivo" size="35" style="height: 50; width:90%" value="<%=motivo%>" class="cajas"></td>
	  </tr>
	  <tr>
	  	<td width="20%" >&nbsp;</td>
	  	<td width="80%"  colspan="2">
    <input type="checkbox" name="confirmacion" value="1" <%if confirmacion=1 then%>checked<%end if%>>Solicitar 
	confirmación</td>
	  </tr>
	  </table>
</form>
</body>
</html>
<%Set archivo=nothing%>