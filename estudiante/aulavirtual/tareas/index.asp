<!--#include file="clstarea.asp"-->
<%
Dim IdDocMarcado,scriptResalte

idOpt=request.querystring("idOpt")
IdDocMarcado=request.querystring("IdDocMarcado")
numfila=request.querystring("numfila")
if idOpt="" then idOpt=1

	Dim tarea
	Dim Arrtarea
	
	Set tarea=new clstarea
		with tarea
			.restringir=session("idcursovirtual")
			Arrtarea=.Consultar("1",session("idcursovirtual"),session("codigo_usu"),idOpt)
			
			if numfila<>"" and IsEmpty(Arrtarea)=false then
				scriptResalte="onLoad=""ResaltarTarea('" & numfila & "','" & IdDocMarcado & "')"""
			end if
		end with
	Set tarea=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de tareas</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
</head>
<body <%=scriptResalte%>>
<input type="hidden" id="txtelegido">
<input type="hidden" id="txttitulotarea">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="90%" height="22" class="e1">Lista de Tareas
    <select  name="idmostrar" onChange="actualizarlista('index.asp?idOpt='+ this.value)">
    <option value="1" <%If IdOpt=1 then response.write "SELECTED"%>>Activos</option>
    <option value="3" <%If IdOpt=3 then response.write "SELECTED"%>>No Activos</option>
    </select></td>
    <td width="10%" align="right" height="22">
    <%if session("tipofuncion")<>3 then%>
    <input name="cmdAgregar" type="button" onClick="AbrirTarea('A')" class="nuevo" onclick="" value=" Nueva tarea">
    <%end if%>
    </td>
  </tr>
</table>
<br>
<%If IsEmpty(ArrTarea)=true then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado 
    encuestas por realizar</p>
<%else%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#666666" width="100%" height="87%">
  <tr class="fondonulo">
    <td width="60%" colspan="2" height="3%"><b>&nbsp;Título de la tarea</b></td>
    <td width="40%" height="3%"><b>&nbsp;Disponibilidad</b></td>
  </tr>
  <tr>
  <td width="100%" align="center" colspan="4" valign="top" height="45%">
  <DIV id="listadiv" style="height:100%">
  <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbllistatareas">	  
  <%for I=lbound(ArrTarea,2) to Ubound(ArrTarea,2)%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="ResaltarTarea('<%=I%>','<%=ArrTarea(0,i)%>')">
    <td width="3%" height="10px"><img border='0' src="../../../images/vineta.gif"/>&nbsp;</td>
    <td width="57%" height="10px"><%=ArrTarea(1,I)%>&nbsp;</td>
    <td width="40%" height="10px"><%=ArrTarea(2,I) & " - " & ArrTarea(3,I)%>&nbsp;</td>
  </tr>
  <%next%>
  </table>
  </DIV>  
  </td></tr>
  <tr>
  	<td width="100%" colspan="3" height="3%">
  	<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  	<tr>
    <td class="paginaDoc" width="65%"><img border="0" src="../../../images/menos.gif" onclick="MostrarTabla(detalletarea,'../../../images/',this)"> 
    Detalles de la tarea </td>
    <td width="25%" class="azul" background="../../../images/fondopestana2.gif" align="right" valign="top"><b><%=ubound(ArrTarea,2)+1%> 
    tarea-s</b></td>
  	</tr>
  	</table> 	
  	</td>
  </tr>
  <tr id="detalletarea">
  	<td width="100%" colspan="3" height="40%"><span class="sugerencia" id="mensajedetalletarea">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente no hay ningúna tarea seleccionada de la Lista.</span>
  	<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
  	</td>
  </tr>
</table>
<%end if%>
</body>
</html>