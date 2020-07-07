<!--#include file="clsrecepcion.asp"-->
<%
if session("codigo_usu")="" then response.Redirect "../../../tiempofinalizado.asp"
idarchivo=request.querystring("idarchivo")
accion=request.querystring("accion")

if accion="" then accion="agregararchivo"

Dim archivo

set archivo=new clsrecepcion

if accion="agregararchivo" then
			NumeroExpediente=archivo.ConsultarUltimoNumeroArchivo(session("idanio"))
			tipoarchivo=6
			NumeroTipo="S/N"
			diai=day(now):mesi=month(now)
			horai=ConvertirAFormato12(cint(hour(now))):mini=cint(minute(now))
			turnoi=IIf(hour(now)>=12,"pm","am")
			
			titulopagina="Registrar nuevo documento"
			idprocedencia=0'964
			iddestinatario=0'45
			nombreprocedencia="-No definido-"
			nombredestinatario="-No definido-"
else
		ArrDatos=archivo.ConsultarArchivosRegistrados("4",idarchivo,0,0)
		if IsEmpty(ArrDatos)=false then
			NumeroExpediente=Arrdatos(0,0)
			tipoarchivo=Arrdatos(1,0)
			NumeroTipo=Arrdatos(2,0)
			Prioridad=Arrdatos(3,0)
			Fecha=Arrdatos(4,0)
			Hora=Arrdatos(5,0)
			idProcedencia=Arrdatos(6,0)
			idDestinatario=Arrdatos(7,0)
			Asunto=Arrdatos(8,0)
			Obs=Arrdatos(9,0)
			Estado=Arrdatos(10,0)
			nombreprocedencia=Arrdatos(11,0)
			nombredestinatario=Arrdatos(12,0)
			
			ProcesarFechas fecha & " " & hora,now
			titulopagina="Modificar documento"
		end if
end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title><%=titulopagina%></title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validararchivo.js"></script>
</head>
<body leftmargin="0" topmargin="0">
<form AUTOCOMPLETE="OFF" name="frmarchivo" Method="POST" onSubmit="return validararchivo()" ACTION="procesar.asp?Accion=<%=accion%>&idarchivo=<%=idarchivo%>">
<table style="border-collapse: collapse;" bordercolor="#111111" cellpadding="4" cellspacing="0" width="100%">
  <tr>
   	<td height="19" width="692" colspan="6" class="barraherramientas">
    <input type="submit" class="guardar3" value="Guardar" NAME="cmdGrabar">
    <input type="button" class="cerrar3" onclick="window.close()" value="  Cancelar" NAME="cmdCancelar"></td>
  </tr>
  <tr>
   	<td height="19" style="width: 87px">Nº Exp.</td>
   	<td height="19" width="80" >
    <input type="text" name="numeroexpediente" size="9" value="<%=numeroexpediente%>" class="e1" maxlength="9"></td>
   	<td height="19" width="33">Tipo:</td>
   	<td height="19" width="332"><%call archivo.mostrarpropiedades("T",tipoarchivo)%></td>
   	<td height="19" width="5">Nº</td>
   	<td height="19" width="86" >
    <input type="text" name="numerotipo" size="9" class="cajas" value="<%=numerotipo%>"></td>
  </tr>
  <tr>
   	<td height="19" style="width: 87px">Prioridad:</td>
   	<td height="19" width="80">
   	<select size="1" name="prioridad">
	<option value="0" <%if prioridad=0 then response.write "selected" end If%>>
	Normal</option>
    <option value="1" <%if prioridad=1 then response.write "selected" end If%>>
	Media</option>
    <option value="2" <%if prioridad=2 then response.write "selected" end If%>>
	Alta</option>
	</select></td>
   	<td height="19" width="33">Fecha: </td>
   	<td height="19" width="470" colspan="3">
    <p>
    <select  name="dia">
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
    <option value="AM" <%=seleccionar(replace(lcase(turnoi),".",""),"am")%>>a.m.</option>
    <option value="PM" <%=seleccionar(replace(lcase(turnoi),".",""),"pm")%>>p.m.</option>
    </select></p>
</td>
  </tr>
  <tr>
   	<td height="19" style="width: 87px">
   	&nbsp;Procedencia:
   	</td>
   	<td height="19" align="left" colspan="4">
   	<input type="hidden" name="idprocedencia" id="idprocedencia" value="<%=idprocedencia%>">
   	<input class="Cajas" type="text" name="txtprocedencia" id="txtprocedencia" value="<%=nombreprocedencia%>" disabled="true">
    </td>
   	<td height="19" align="left">
	<img style="cursor:hand" border="0" src="../../../images/resultados.gif" onClick="AbrirPopUp('frmbuscadorprop.asp?tabla=procedencia&criterio='+frmarchivo.txtprocedencia.value,'300','450','no','no','no')">
	</td>
  </tr>
  <tr>
   	<td height="19" style="width: 87px" valign="top">
   	Asunto</td>
   	<td height="19" align="left" colspan="4">
   	<textarea name="asunto" cols="20" rows="4" class="Cajas" onkeyup="BloquearEnter()"><%=Asunto%></textarea>
    </td>
   	<td height="19" align="left">
	<img style="cursor:hand" border="0" src="../../../images/resultados.gif" onClick="AbrirPopUp('frmbuscadorprop.asp?tabla=asunto&criterio='+frmarchivo.asunto.value,'300','450','no','no','no')"></td>
  </tr>
  <tr>
   	<td height="19" style="width: 87px">&nbsp;Dirigido a:
   	</td>
   	<td height="15" colspan="4" align="left">
    <input type="hidden" name="iddestinatario" id="iddestinatario" value="<%=iddestinatario%>">
    <input class="Cajas" type="text" name="txtdestinatario" id="txtdestinatario" value="<%=nombredestinatario%>" disabled="true">
	</td>
   	<td height="19" align="left">
    	<img style="cursor:hand" border="0" src="../../../images/resultados.gif" onClick="AbrirPopUp('frmbuscadorprop.asp?tabla=destinatario&criterio='+frmarchivo.txtdestinatario.value,'300','450','no','no','no')">
	</td>
  </tr>
  <tr>
   	<td height="19" valign="top" style="width: 87px">Observaciones:</td>
   	<td height="19"  colspan="5">
   	<textarea name="obs" cols="20" rows="2" class="Cajas"><%=Obs%></textarea>
   	</td>
  </tr>
  <tr class="colorbarra">
      <td height="19" style="width: 87px">Estado:</td>
      <td height="19"  colspan="5"><b><%=ucase(Estado)%></b></td>
    </tr>
</table>
</form>
</body>
</html>
<%set archivo=nothing%>