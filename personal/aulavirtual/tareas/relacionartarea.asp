<!--#include file="../funcionesaulavirtual.asp"-->
<%
Dim tituloframe,mensajeError

Accion=Request.querystring("accion")
TablaAMostrar=Request.querystring("TablaAMostrar")
IdTabla=Request.querystring("IdTabla")

If TablaAMostrar="Documento" then
	tituloframe="Seleccionar los documentos que se relacionan con la tarea"
	mensajeError="No hay documentos con que relacionar la tarea"
	
	cadSQL="SELECT IdDocumento,titulodocumento FROM listadocumentosYpermisos "
  	cadSQL= cadSQL & " WHERE acceso='" & session("codigo_usu") & "' AND tipodoc='A' and idcursovirtual=" & session("idcursovirtual")
Else
	tituloframe="Seleccionar las tareas con que se relacionará el cursovirtual de la agenda"
	mensajeError="No hay tareas registradas para esta actividad académica"

	cadSQL="SELECT IdTarea,titulotarea FROM listatareasYpermisos "
  	cadSQL= cadSQL & " WHERE acceso='" & session("codigo_usu") & "' AND and idcursovirtual=" & session("idcursovirtual")
End if
	'response.write cadSQL
	ArrIdOpt=CargarTablas(cadSQL)  	
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Información sobre la tarea</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>

<body>
<%if IsEmpty(ArrIdOpt) then%>
    <h3><%=mensajeError%></h3>
<%else%>
<form name="frmTarea" method="POST" action="../procesar.asp?Accion=<%=Accion%>&IdTabla=<%=IdTabla%>&Nombretabla=<%=TablaAMostrar%>">
<fieldset style="padding: 2; width:100%">
  <legend class="e1"><%=tituloframe%>&nbsp;&nbsp; </legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">   
    <tr>
      <td width="100%">
	<%for i=lbound(ArrIdOpt,2) to Ubound(ArrIdOpt,2)%>
		<input type="checkbox" name="chk" value="<%=ArrIdOpt(0,I)%>"><%=ArrIdOpt(1,I)%><br>
	<%next%></td>
    </tr>
    <tr>
      <td width="100%" align="center"><input type="submit" value="Guardar" name="cmdGuardar" class="guardar"> 
      <input Onclick="location.href='../listacorreos.asp?accion=agregarpermisos&idtabla=<%=IdTabla%>&nombretabla=tarea'" type="button" value="Cancelar" name="cmdCancelar" class="salir"></td>
    </tr>
  </table>
  </fieldset>
</form>
<%end if%>
</body>
</html>