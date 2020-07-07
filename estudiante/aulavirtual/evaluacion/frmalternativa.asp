<!--#include file="clsevaluacion.asp"-->
<%
Dim NumFilas,idpregunta,idtipopregunta
Dim Modalidad,idalternativa,tituloalternativa,rptacorrecta,orden,mensaje

Modalidad=Request("modalidad")

idtipopregunta=Request.querystring("idtipopregunta")
idpregunta=Request.querystring("idpregunta")
idpregunta=iif(idpregunta="",0,idpregunta)

idalternativa=request("idalternativa")
tituloalternativa=Request("tituloalternativa")
rptacorrecta=Request("rptacorrecta")
rptacorrecta=iif(rptacorrecta="",0,1)
orden=Request("orden")
mensaje=Request("mensaje")

response.Buffer=true

set evaluacion=new clsevaluacion
	with evaluacion
		.restringir=session("idcursovirtual")
		
		If Len(Request.form("cmdGuardar"))>0 then
			Select case Modalidad
				case "AgregarNuevo"
					call .AgregarAlternativa(idpregunta,tituloalternativa,rptacorrecta,orden,mensaje)
					Modalidad=""
				case "Modificar"
					Call .ModificarAlternativa(idalternativa,idtipopregunta,idpregunta,tituloalternativa,rptacorrecta,orden,mensaje)
			end Select
		else
			If Modalidad="Eliminar" then Call .EliminarAlternativa(idalternativa)
		End if
	
		Arrdatos=.Consultar("11",idpregunta,idtipopregunta,"")
		If IsEmpty(Arrdatos)=false then orden=Ubound(Arrdatos,2)+2
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar enlaces web del documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarevaluacion.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmListaAlternativas" method="post" onSubmit="return validarAlternativa(this)" ACTION="frmalternativa.asp?idtipopregunta=<%=idtipopregunta%>&Modalidad=<%=Modalidad%>&idpregunta=<%=idpregunta%>">
<table width="100%" border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#EBEBEB">
  <tr class="variable"> 
      <td width="100%" height="25" colspan="5"><b>Alternativas</b></td>
  </tr>
  <tr> 
      <td width="5%" height="25" bgcolor="#E9F3FC"><b>Correcta</b></td>
      <td width="10%" height="25" bgcolor="#E9F3FC"><b>Orden</b></td>
      <td width="35%" height="25" bgcolor="#E9F3FC"><b>Texto de Alternativa</b></td>
      <td width="35%" height="25" bgcolor="#E9F3FC"><b>Mensaje de respuesta incorrecta</b></td>
      <td width="15%" bgcolor="#E9F3FC">
      <input name="Button" type="button" class="agregar" onclick="MM_goToURL('self','frmalternativa.asp?idtipopregunta=<%=idtipopregunta%>&idpregunta=<%=idpregunta%>&Modalidad=AgregarNuevo');return document.MM_returnValue" value="   Añadir"></td>
  </tr>
  <%if IsEmpty(ArrDatos)=false then
		num=0								  
		for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
			num=num+1%>			  
    <tr>
    <td width="5%"><%if trim(modalidad)="Modificar" and num=cint(request("IdReg")) then
		response.write .ImprimirControl(idtipopregunta,Arrdatos(2,I))%>
		<input type="hidden" name="idalternativa" id="idalternativa" value="<%=Arrdatos(0,I)%>">
	<%else
        response.write iif(Arrdatos(2,I)=0,"No","Sí")
    end if%>&nbsp;</td>
    <td width="10%"><%if trim(modalidad)="Modificar" and num=cint(request("IdReg")) then%>
	<input maxLength="10" onkeypress="validarnumero()" name="orden" id="orden" class="Cajas" size="1" value="<%=Arrdatos(3,I)%>">
	<%else
		response.write Arrdatos(3,I)
    end if%></td>
    <td width="35%"><%if trim(modalidad)="Modificar" and num=cint(request("IdReg")) then%>
		<input maxLength="100" name="tituloalternativa" id="tituloalternativa" class="Cajas" size="20" value="<%=Arrdatos(1,I)%>">
	<%else
		response.write Arrdatos(1,I)
    end if%></td>
	<td width="35%"><%if trim(modalidad)="Modificar" and num=cint(request("IdReg")) then%>
		    <input  maxLength="100" name="mensaje" class="Cajas" id="mensaje" size="20" value="<%=Arrdatos(4,I)%>">
  		<%else
  			response.write Arrdatos(4,I)
		end if%>
	</td>
	<td align="right" width="15%">
		<%if trim(modalidad)="Modificar" and num=cint(request("IdReg")) then%>
  			<input type="submit" value="     " name="cmdGuardar" class="imgGuardar">
  			<img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','frmalternativa.asp?idtipopregunta=<%=idtipopregunta%>&idpregunta=<%=idpregunta%>&idalternativa=<%=Arrdatos(0,I)%>');return document.MM_returnValue" class="imagen">
		<%else%>
  			<img border="0" src="../../../images/editar.gif" onclick="MM_goToURL('self','frmalternativa.asp?idtipopregunta=<%=idtipopregunta%>&idpregunta=<%=idpregunta%>&idalternativa=<%=Arrdatos(0,I)%>&modalidad=Modificar&IdReg=<%=num%>');return document.MM_returnValue" class="imagen">
  			<img border="0" src="../../../images/eliminar.gif" onclick="EliminarAlternativa('<%=Arrdatos(0,I)%>','<%=idtipopregunta%>','<%=idpregunta%>');" class="imagen">
		<%end if%>
	</td>
	</tr>
	<%Next
 end if
 if trim(modalidad)="AgregarNuevo" then%>
  	<tr>
       <td width="5%"><%=.ImprimirControl(idtipopregunta,0)%>&nbsp;</td>
       <td width="10%"><input maxLength="10" onkeypress="validarnumero()" name="orden" id="orden" class="Cajas" size="1" value="<%=orden%>"></td>
       <td width="35%"><input maxLength="100" name="tituloalternativa" id="tituloalternativa" class="Cajas" size="20"></td>
       <td width="35%"><input  maxLength="100" name="mensaje" class="Cajas" id="mensaje" size="20"></td>
		<td align="right" width="15%">
		<input type="submit" value="     " name="cmdGuardar" class="imgGuardar">
        <img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','frmalternativa.asp?idtipopregunta=<%=idtipopregunta%>&idpregunta=<%=idpregunta%>');return document.MM_returnValue" class="imagen"></td>
      </tr>
  <%end if%>
  </td>
  </tr>
	</td>
	</tr>
	</td></tr>
	</td></tr>
  </table>
</form>
</body>
</html>
<%
	end with
Set evaluacion=nothing
%>