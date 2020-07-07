<!--#include file="clsagenda.asp"-->
<%
  Accion=Request.querystring("Accion")
  IdAgenda=Request.querystring("IdAgenda")

  if Accion="modificaragenda" then
		dim agenda
		Set agenda=new clsagenda
			ArrDatos=agenda.Consultar("3",IdAgenda,"","","","")
		Set agenda=nothing
		tituloagenda=Arrdatos(1,0)
		Fechainicio=Arrdatos(2,0)
		Fechafin=Arrdatos(3,0)
		Procesarfechas Fechainicio,FechaFin
		lugar=Arrdatos(4,0)
		descripcion=Arrdatos(5,0)
		contactos=Arrdatos(6,0)
		idCategoria=Arrdatos(7,0)
		prioridad=Arrdatos(10,0)
  else
		ProcesarFechas now,now
  end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de cursovirtuals</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="private/validaragenda.js"></script>
</head>
<body onLoad="document.all.tituloagenda.focus()" topmargin="0">
<form name="frmagenda" method="POST" onSubmit="return validaragenda(this);" action="procesar.asp?Accion=<%=Accion%>&IdAgenda=<%=IdAgenda%>">
<fieldset style="padding: 2; width:100%">
  <legend class="e1">Datos del Evento&nbsp;</legend>
<table width="100%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
  <tr>
    <td width="72">&nbsp;</td>
    <td  width="40" class="etiqueta">Tipo</td>
    <td  width="249"><%call escribirlista("idcategoria","","",idCategoria,"clscategoria","1","Agenda","","")%></td>
    <td  width="123" class="etiqueta">
    Prioridad</td>
    <td  width="161">
    <select  name="prioridad">
    <option value="Normal">Normal</option>
    <option value="Alta">Alta</option>
    <option value="Baja">Baja</option>
    </select></td>
  </tr>
  <tr>
    <td width="72" class="etiqueta">Título</td>
    <td  width="591" colspan="4">
    <input  maxLength="100" size="82" name="tituloagenda" class="cajas" value="<%=tituloagenda%>"></td>
  </tr>
  <tr>
    <td width="669" class="etiqueta" colspan="5"><%BarraProgramacionFechas%></td>
  </tr>
  <tr>
    <td width="72" class="etiqueta">Lugar</td>
    <td  width="591" colspan="4">
    <input  maxLength="100" size="83" name="lugar" class="cajas" value="<%=lugar%>"></td>
  </tr>
  <tr>
    <td width="72" valign="top" class="etiqueta">Descripción</td>
    <td  width="591" colspan="4">
    <textarea  name="descripcion" rows="3" cols="81" class="cajas"><%=descripcion%></textarea></td>
  </tr>
  <%call escribirtipopublicacion(Accion)%>
  <tr>
    <td width="72" valign="top">&nbsp;</td>
    <td  width="591" colspan="4"><input type="submit" value="Guardar" name="cmdGuardar" class="guardar"> 
    <input onClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="salir"></td>
  </tr>
</table>
</fieldset>
</form>
</body>
</html>