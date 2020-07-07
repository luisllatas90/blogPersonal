<!--#include file="clsforo.asp"-->
<%
accion=Request.querystring("accion")
idforo=request.querystring("idforo")
dim foro

 if Accion="modificarforo" then
	Set foro=new clsforo
		foro.restringir=session("idcursovirtual")
		ArrForo=foro.Consultar("4",idforo,"","")
	Set foro=nothing
		tituloforo=ArrForo(2,0)
		descripcion=ArrForo(3,0)
		fechainicio=ArrForo(4,0)
		fechafin=ArrForo(5,0)
		Procesarfechas Fechainicio,FechaFin
		permitircalificar=ArrForo(6,0)
		tipocalificacion=ArrForo(7,0)
		numcalificacion=ArrForo(8,0)
		if permitircalificar=1 then
			activarcalificacion="onLoad=""frmforo.tipocalificacion.disabled='';frmforo.numcalificacion.disabled=''"""
		end if
  else
		procesarfechas now,session("fincursovirtual")
  end if

%>
<HTML>
<HEAD>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<title>Registrar foro</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarforo.js"></script>
</HEAD>
<BODY topmargin="0" leftmargin="0" <%=activarcalificacion%>>
<form name="frmforo" method="post" onSubmit="return validarforo(this)" action="procesar.asp?accion=<%=accion%>&idforo=<%=idforo%>">
<%BotonesAccion%>
<center>
<fieldset style="padding: 2; width:98%">
<legend class="e1">Registro de Temas de discusión</legend>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tblforo">
<tr>
<td width="20%" class="etiqueta">Título del tema</td>
<td width="80%">
<input name="tituloforo" type="text" class="cajas" value="<%=tituloforo%>" size="20" maxlength="250"></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Descripción</td>
<td width="80%"><textarea class="cajas" rows="2" name="descripcion" cols="80"><%=descripcion%></textarea></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Permitir calificar</td>
<td width="80%">
<input type="checkbox" name="permitircalificar" value="1" <%=seleccionaritem("chk",permitircalificar,1)%> onClick="activarcalificacion(this)"><select disabled=true size="1" name="tipocalificacion"><option value="0" <%=seleccionaritem("cbo",tipocalificacion,0)%>>Sólo moderador puede calificar mensajes</option>
<option value="1" <%=seleccionaritem("cbo",tipocalificacion,1)%>>Todos los participantes pueden calificar mensajes</option></select></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Calificación máxima</td>
<td width="80%"><select disabled=true size="1" name="numcalificacion">
	<%for i=1 to 20%>
	<option value="<%=i%>" <%=seleccionaritem("cbo",numcalificacion,i)%>><%=i%></option>
	<%next%>
</select></td>
</tr>
<tr>
<td width="100%" class="etiqueta" colspan="2"><%BarraProgramacionFechas%>&nbsp;</td>
</tr>
</table>
</fieldset>
</center>
</form>
</BODY>
</HTML>