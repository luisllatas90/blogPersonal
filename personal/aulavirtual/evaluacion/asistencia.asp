<!--#include file="../funcionesaulavirtual.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

Dim Modalidad
Dim idcursovirtual
Dim Idfechapresencial
Dim fechareg

Modalidad=Request("modalidad")
idcursovirtual=session("idcursovirtual")
Idfechapresencial=Request("Idfechapresencial")
fechareg=Request("dia") & "/" & Request("mes") & "/" & Request("anio")

response.Buffer=true

	If Len(Request.form("cmdGuardar"))>0 then
		Select case Modalidad
			case "AgregarNuevo": Call Agregarfechapresencial:Modalidad=""
			case "Modificar": Call Modificarfechapresencial:Modalidad=""
		end Select
	Else
		If Modalidad="Eliminar" then Call Eliminarfechapresencial
	End if
	
	Sub Agregarfechapresencial()
		set Obj=server.CreateObject ("AulaVirtual.clscursovirtual")
			
				Obj.Agregafecha idcursovirtual,fechareg
			
		set Obj=nothing
	End Sub
	
	Sub Modificarfechapresencial()
		set Obj=server.CreateObject ("AulaVirtual.clscursovirtual")
			
				Obj.Modificafecha Idfechapresencial,fechareg
			
		set Obj=nothing
	End Sub
	
	Sub Eliminarfechapresencial()
		set Obj=server.CreateObject ("AulaVirtual.clscursovirtual")
			Obj.eliminafecha Idfechapresencial
		set Obj=nothing
	end Sub
	
	Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
		ArrDatos=Obj.ListaFechasPresenciales(Idcursovirtual)
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
</head>
<body>
<form name="frmListafechapresenciales" method="post" onSubmit="return validarfechapresencial(this)" ACTION="asistencia.asp?Modalidad=<%=Modalidad%>&idcursovirtual=<%=idcursovirtual%>">
<table width="70%" border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
  <tr class="etabla"> 
      <td width="3%">&nbsp;</td>
      <td style="text-align: left" width="40%">Fecha presencial de la Actv. 
      Académica</td>
      <td width="12%"><input name="Button" type="button" class="agregar" onclick="MM_goToURL('self','asistencia.asp?idcursovirtual=<%=idcursovirtual%>&Modalidad=AgregarNuevo');return document.MM_returnValue" value="   Agregar"><input name="cmdRegresar" type="button" class="salir" onclick="location.href='listaasistencia.asp'" value="Regresar"></td>
	</tr>
	<%if IsEmpty(ArrDatos)=false then
		num=0								  
	for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
		num=num+1%>			  
    <tr>
    <td width="3%"><%=num%>.</td>
    <td width="40%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then
    ProcesarFechas Arrdatos(1,i),now%>
	<select  name="dia">
    <option>Día</option>
	<%for d=1 to 31%>
		<option value="<%=iif(len(d)=1,"0" & d,d)%>" <%=seleccionar(Diai,d)%>><%=iif(len(d)=1,"0" & d,d)%></option>
	<%next%>
    </select> <select  name="mes">
    <option value>Mes</option>
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=MonthName(m, False)%></option>
  	<%next%>
    </select> <select  name="anio">
    <option value="2004" <%=seleccionar(anioi,"2004")%>>2004</option>
    <option value <%=seleccionar(anioi,"2005")%>>2005</option>
    </select>&nbsp;&nbsp;
	<input name="idfechapresencial" type="hidden" class="cajas" id="Idfechapresencial" value="<%=Arrdatos(0,I)%>">
	<%else%>
        <%=Arrdatos(1,I)%>
    <%end if%></td>
	<td align="right" width="12%">
	<%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
  		<input type="submit" value="----" name="cmdGuardar" class="imgGuardar">
  		<img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','asistencia.asp?idcursovirtual=<%=idcursovirtual%>&Idfechapresencial=<%=Arrdatos(0,I)%>');return document.MM_returnValue" class="imagen">
	<%else%>
  		<img border="0" src="../../../images/editar.gif" onclick="MM_goToURL('self','asistencia.asp?idcursovirtual=<%=idcursovirtual%>&Idfechapresencial=<%=Arrdatos(0,I)%>&modalidad=Modificar&recordid=<%=num%>');return document.MM_returnValue" width="18" height="13" class="imagen">
  		<img border="0" src="../../../images/eliminar.gif" onclick="ConfirmarEliminar('¿Está seguro que desea eliminar la fecha presencial <%=Arrdatos(1,I)%> de la Activ. Académica y todos los asistentes en esta fecha?','asistencia.asp','Idfechapresencial','<%=Arrdatos(0,I)%>');" class="imagen">
	<%end if%>
	</td>
	</tr>
	<%Next
 end if
 if trim(modalidad)="AgregarNuevo" then
 	ProcesarFechas now,now%>
  	<tr>
       <td width="3%"><%=num+1%>.</td>
       <td width="40%">
       <select  name="dia">
    <option value>Día</option>
	<%for d=1 to 31%>
		<option value="<%=iif(len(d)=1,"0" & d,d)%>" <%=seleccionar(Diai,d)%>><%=iif(len(d)=1,"0" & d,d)%></option>
	<%next%>
    </select> <select  name="mes">
    <option value>Mes</option>
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=MonthName(m, False)%></option>
  	<%next%>
    </select> <select  name="anio">
    <option value="2004" <%=seleccionar(anioi,"2004")%>>2004</option>
    <option value <%=seleccionar(anioi,"2005")%>>2005</option>
    </select>&nbsp; </td>
		<td align="right" width="12%">
		<input type="submit" value="----" name="cmdGuardar" class="imgGuardar">
        <img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','asistencia.asp?idcursovirtual=<%=idcursovirtual%>');return document.MM_returnValue" class="imagen"></td>
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