<!--#include file="clscursovirtual.asp"-->
<%
  accion=Request.querystring("accion")
  idcursovirtual=Request.querystring("idcursovirtual")
  codigo_apl=request.querystring("codigo_apl")
  codigo_tfu=request.querystring("codigo_tfu")
  
  	if accion="modificarcurso" then
  		dim curso
		Set curso=new clscursovirtual
			with curso
				ArrDatos=.Consultar("3",idcursovirtual,codigo_apl,"")
			end with 
		Set curso=nothing
			Fechainicio=Arrdatos(1,0)
			Fechafin=Arrdatos(2,0)
			titulocursovirtual=Arrdatos(3,0)
			descripcion=Arrdatos(4,0)
			modalidad=Arrdatos(5,0)
			creartemas=Arrdatos(13,0)
			temapublico=Arrdatos(14,0)
			integrartematarea=Arrdatos(15,0)
			integrarrptatarea=Arrdatos(16,0)
	else
		fechainicio=date
		fechafin=date
  	end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de cursos virtuales</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="private/validarcurso.js"></script>
</head>
<body <%if codigo_apl<>4 then%>onLoad="document.all.titulocursovirtual.focus()"<%end if%> topmargin="0" leftmargin="0">
<form name="frmcursovirtual" method="POST" onSubmit="return validarcursovirtual(this,'<%=codigo_apl%>');" ACTION="procesar.asp?accion=<%=accion%>&codigo_apl=<%=codigo_apl%>&idcursovirtual=<%=idcursovirtual%>">
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblbotones" class="barraherramientas">
  	<tr><td>
	<input type="submit" value=" Guardar" class="guardar3" name="cmdGuardar">
	<input onClick="top.window.close()" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar3">
	<%if accion="modificarcurso" then%>
	<input onClick="EliminarCurso('<%=idcursovirtual%>')" type="button" value="   Eliminar Curso Virtual" name="cmdEliminar" class="eliminar3" style="width: 150">
	<%end if%>
	<span id="mensaje" style="color:#FF0000"></span>
	</td></tr>
</table>
<center>
<fieldset style="padding: 2; width:97%">
  <legend></legend>
<table width="100%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
  <tr>
    <td width="15%" class="etiqueta">Denominación</td>
    <td  width="85%" colspan="4">
    	<%if codigo_apl=4 then 
    		response.write titulocursovirtual
    		response.write "<input type=""hidden"" name=""titulocursovirtual"" value=""" & titulocursovirtual & """>"
    	else%><input  maxLength="100" size="82" name="titulocursovirtual" class="cajas" value="<%=titulocursovirtual%>">
    	<%end if%>
    </td>
  </tr>
  <tr>
    <td width="15%" valign="top" class="etiqueta">Descripción</td>
    <td  width="85%" colspan="4">
    <textarea name="descripcion" rows="3" cols="81" class="cajas"><%=descripcion%></textarea></td>
  </tr>  
  <tr>
    <td width="15%" class="etiqueta" valign="top">Duración</td>
    <td width="5%">
    Desde</td>
    <td width="10%">
    <input type="text" name="fechainicio" size="12" class="Cajas2" readonly value="<%=fechainicio%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechainicio')"></td>
    <td width="5%">
    Hasta</td>
    <td>
    <input type="text" name="fechafin" size="12" class="Cajas2" readonly value="<%=fechafin%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechafin')">
    </td>
  </tr>
  <tr>
    <td width="15%" valign="top" class="etiqueta">Modalidad</td>
    <td  width="85%" colspan="4">
    <select  name="modalidad">
    <option value="Presencial" <%=Seleccionar(modalidad,"Presencial")%>>Presencial</option>
    <option value="Semipresencial" <%=Seleccionar(modalidad,"Semipresencial")%>>Semipresencial</option>
    <option value="En linea" <%=Seleccionar(modalidad,"En linea")%>>En linea</option>
    </select></td>
  </tr>
  <tr>
    <td width="100%" valign="top" class="etiqueta" colspan="5" align="right">
    <table width="100%" style="border-collapse: collapse" bordercolor="#C0C0C0" cellpadding="3" cellspacing="0" border="1">
  <tr>
    <td width="100%" valign="top" class="etabla2" colspan="2" style="text-align: left">
    Opciones de Configuración</td>
  </tr>
  <tr>
    <td width="5%" valign="top" class="etiqueta" align="right">
    <input type="checkbox" name="creartemas" value="1" <%=Marcar(1,creartemas)%>></td>
    <td  width="85%" valign="top" class="seccion">
    Permitir a los participantes crear temas de discusión</td>
  </tr>
  <tr>
    <td width="5%" valign="top" class="etiqueta" align="right">
    <input type="checkbox" name="temapublico" value="1" <%=Marcar(1,temapublico)%>></td>
    <td  width="85%" valign="top" class="seccion">
    Permitir que los temas de discusión propuestos sean publicados previa 
    autorización del moderador.<br>
    Si no selecciona esta opción los temas serán publicados automáticamente</td>
  </tr>
  <tr>
    <td width="5%" valign="top" class="etiqueta" align="right">
    <input type="checkbox" name="integrartematarea" value="1" <%=Marcar(1,integrartematarea)%>></td>
    <td  width="85%" valign="top" class="seccion">
    Integrar la creación de temas al sistema de tareas<br>
    Permite registrar la creación de tareas de los alumnos como una tarea.</td>
  </tr>
  <tr>
    <td width="5%" valign="top" class="etiqueta" align="right">
    <input type="checkbox" name="integrarrptatarea" value="1" <%=Marcar(1,integrarrptatarea)%>></td>
    <td  width="85%" valign="top" class="seccion">
    &nbsp;Integrar las respuestas de los temas al sistema de tareas<br>
    Permite registrar las respuestas de los temas de los alumnos como una tarea.</td>
  </tr>
</table>&nbsp;</td>
  </tr>
</table>
</center>	
</form>
</fieldset>
</body>
</html>