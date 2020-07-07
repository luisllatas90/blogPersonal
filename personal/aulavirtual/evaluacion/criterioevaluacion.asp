<!--#include file="../funcionesaulavirtual.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

Dim Modalidad
Dim idcursovirtual
Dim idcriterio
Dim titulocriterio

Modalidad=Request("modalidad")
idcursovirtual=session("idcursovirtual")
idcriterio=Request("idcriterio")
titulocriterio=Request("titulocriterio")

response.Buffer=true

	If Len(Request.form("cmdGuardar"))>0 then
		Select case Modalidad
			case "AgregarNuevo": Call Agregarcriterioevaluacion:Modalidad=""
			case "Modificar": Call Modificarcriterioevaluacion:Modalidad=""
		end Select
	Else
		If Modalidad="Eliminar" then Call Eliminarcriterioevaluacion
	End if
	
	Sub Agregarcriterioevaluacion()
		set Obj=server.CreateObject ("AulaVirtual.clscursovirtual")
			
				Obj.Agregacriterioevaluacion idcursovirtual,titulocriterio
			
		set Obj=nothing
	End Sub
	
	Sub Modificarcriterioevaluacion()
		set Obj=server.CreateObject ("AulaVirtual.clscursovirtual")
			
				Obj.Modificacriterioevaluacion idcriterio,titulocriterio
			
		set Obj=nothing
	End Sub
	
	Sub Eliminarcriterioevaluacion()
		set Obj=server.CreateObject ("AulaVirtual.clscursovirtual")
			Obj.eliminacriterioevaluacion idcriterio
		set Obj=nothing
	end Sub
	
	Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
		ArrDatos=Obj.ListaCriteriosEvaluacion(idcursovirtual)
	Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de fechas presenciales de la Actividad Académica</title>
<script language="JavaScript" src="../../../private/validaciones.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" type="text/JavaScript">
	function ColocarEnfoque()
	{
		<%if Modalidad="AgregarNuevo" then%>
		frmListacriterioevaluaciones.titulocriterio.focus()
		<%End if%>
	}
</script>
</head>
<body onload="ColocarEnfoque()">
<form name="frmListacriterioevaluaciones" method="post" onSubmit="return validarcriterioevaluacion(this)" ACTION="criterioevaluacion.asp?Modalidad=<%=Modalidad%>&idcursovirtual=<%=idcursovirtual%>">
<table width="70%" border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
  <tr class="etabla"> 
      <td width="3%">&nbsp;</td>
      <td style="text-align: left" width="40%">Criterios para evaluar el 
      desempeño</td>
      <td width="12%">
<input name="Button" type="button" class="agregar" onclick="MM_goToURL('self','criterioevaluacion.asp?Modalidad=AgregarNuevo');return document.MM_returnValue" value="   Agregar"><input name="cmdRegresar" type="button" class="salir" onclick="location.href='listadesempeno.asp'" value="Regresar"></td>
	</tr>
	<%if IsEmpty(ArrDatos)=false then
		num=0								  
	for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
		num=num+1%>			  
    <tr>
    <td width="3%"><%=num%>.</td>
    <td width="40%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
		<input type="text" name="titulocriterio" size="20" value="<%=Arrdatos(1,I)%>" class="Cajas" style="width: 100%">
		<input name="idcriterio" type="hidden" class="cajas" id="idcriterio" value="<%=Arrdatos(0,I)%>">
	</td>	
	<%else%>
        <%=Arrdatos(1,I)%>
    <%end if%>
	<td align="right" width="12%">
	<%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
  		<input type="submit" value="----" name="cmdGuardar" class="imgGuardar">
  		<img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','criterioevaluacion.asp?idcriterio=<%=Arrdatos(0,I)%>');return document.MM_returnValue" class="imagen">
	<%else%>
  		<img border="0" src="../../../images/editar.gif" onclick="MM_goToURL('self','criterioevaluacion.asp?idcriterio=<%=Arrdatos(0,I)%>&modalidad=Modificar&recordid=<%=num%>');return document.MM_returnValue" class="imagen">
  		<img border="0" src="../../../images/eliminar.gif" onclick="ConfirmarEliminar('¿Está seguro que desea eliminar el criterio de evaluación \n <%=Arrdatos(1,I)%> \n y todos los participantes registrados en este criterio?','criterioevaluacion.asp','idcriterio','<%=Arrdatos(0,I)%>');" class="imagen">
	<%end if%>
	</td>
	</tr>
	<%Next
 end if
 if trim(modalidad)="AgregarNuevo" then%>
  	<tr>
       <td width="3%"><%=num+1%>.</td>
       <td width="40%">
       <input type="text" name="titulocriterio" size="20" class="Cajas" style="width: 100%"></td>
	<td align="right" width="12%">
	<input type="submit" value="----" name="cmdGuardar" class="imgGuardar">
    <img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','criterioevaluacion.asp?idcursovirtual=<%=idcursovirtual%>');return document.MM_returnValue" class="imagen"></td>
    </tr>
  <%end if%>
  </td>
  </tr>
	</td>
	</tr>
</td>
  </tr>
</td>
  </tr>
  </table>
</form>
</body>
</html>