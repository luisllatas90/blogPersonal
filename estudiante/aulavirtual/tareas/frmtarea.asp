<!--#include file="clstarea.asp"-->
<%
Dim mostrartr
accion=request.querystring("accion")
idtarea=request.querystring("idtarea")
numfila=request.querystring("numfila")

  if Accion="modificartarea" then
		dim tarea
		Set tarea=new clstarea
			tarea.Restringir=session("idcursovirtual")
			ArrTarea=tarea.Consultar("2",idtarea,"","")
		Set tarea=nothing
		titulotarea=ArrTarea(3,0)
		fechainicio=ArrTarea(4,0)
		fechafin=ArrTarea(5,0)
		Procesarfechas Fechainicio,FechaFin
		descripcion=ArrTarea(6,0)
		permitirreenvio=ArrTarea(11,0)
		calificacion=ArrTarea(12,0)
		idtipotarea=ArrTarea(13,0)
		descripciontipotarea=ArrTarea(15,0)
		if trim(idtipotarea)="4" then
			mostrartr=""
		else
			mostrartr="style=""display:none"" "			
		end if
  else
		procesarfechas now,session("fincursovirtual")
		mostrartr="style=""display:none"" "
  end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de tareas</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
</head>
<body topmargin="0" onload="document.all.titulotarea.focus()" leftmargin="0">
<form name="frmtarea" method="POST" onSubmit="return validartarea(this)" action="procesar.asp?idtarea=<%=idtarea%>&accion=<%=accion%>&numfila=<%=numfila%>&idtipotarea=<%=idtipotarea%>">
<%BotonesAccion%>
<center>
<table width="98%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
  <tr>
    <td width="12%" class="etiqueta">Tipo</td>
    <td  width="25%" class="etiqueta">
    <%if accion="agregartarea" then
    	response.write "subir archivos"
    else
    	response.write descripciontipotarea
    end if%></td>
    <td  width="24%" class="etiqueta">
    Calificación Máx.&nbsp;
    <select size="1" name="calificacion">
    <option value="0">Ninguna</option>
    <%for i=1 to 20%>
    	<option value="<%=i%>" <%=seleccionar(i,calificacion)%>><%=i%></option>
    <%next%>
    </select></td>
  </tr>
  <tr>
    <td width="12%" class="etiqueta">Título</td>
    <td  width="88%" colspan="2">
    <input  maxLength="100" size="82" name="titulotarea" class="cajas" value="<%=titulotarea%>"></td>
  </tr>
  <tr>
    <td width="100%" colspan="3"><%BarraProgramacionFechas%></td>
  </tr>
  <tr>
    <td width="12%" valign="top" class="etiqueta">Descripción</td>
    <td  width="88%" colspan="2">
    <textarea  name="descripcion" rows="3" cols="81" class="cajas"><%=descripcion%></textarea></td>
  </tr>
  <tr class="etiqueta">
    <td width="12%" valign="top" class="etiqueta">&nbsp;</td>
    <td  width="68%" id="trintegrar" style="display:none" valign="top" colspan="2">
    Integrar tarea con
    <select size="1" name="cbxrecurso">
    <option value="">Ningún recurso publicado</option>
    <option value="frmasignarencuestas.asp">Encuestas publicadas</option>
    <option value="frmasignartemaforo.asp">Temas de foro publicados</option>
    </select></td>
  </tr>
  <!--
  <tr id="trpermitirreenvio" <%=mostrartr%>>
    <td width="12%" valign="top" class="etiqueta" align="right">
    <input type="checkbox" name="permitirreenvio" value="1" <%=marcar(1,permitirreenvio)%>></td>
    <td  width="88%" colspan="2">
	<font color="#FF0000"><b>Administración de versiones</b></font><br>
	Permitir a los participantes reenvió de archivos y al moderador realizar 
    correcciones, para registrar un control de versiones de la tarea asignada.</td>
  </tr>
  -->
  <%call escribirtipopublicacion(Accion)%>
  </table>
</center>
</form>
</body>
</html>