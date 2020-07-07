<!--#include file="clsusuario.asp"-->
<%
modo=request.querystring("modo")
idusuario=session("codigo_usu")
idtabla=request.querystring("idtabla")
nombretabla=request.querystring("nombretabla")
tipodoc=request.querystring("tipodoc")

set usuario=new clsusuario
	usuario.Restringir=session("idcursovirtual")
	ArrDatos=usuario.consultar("5",nombretabla,idtabla,session("idcursovirtual"))
Set usuario=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Asignar los usuarios que compatirán el recurso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body topmargin="0" leftmargin="0" class="contornotabla">
<form name="frmListaCorreos" onsubmit="return validarlistaElegida(this)" method="post" ACTION="procesar.asp?accion=agregarpermiso&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>&modo=<%=modo%>&tipodoc=<%=tipodoc%>">
<%BotonesAccion%>
<table border="0" cellpadding="3"  cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="90%">
  <tr class="etabla">
    <td height="10%" style="text-align: left" colspan="2">Usuarios del Curso o Actividad Académica</td>
    <td height="10%">Usuarios seleccionados</td>
  </tr>
  <tr>
    <td width="45%" height="90%">
		  <select multiple name="ListaDe" size="10" style="width: 100%; height:100%">
		  <%If IsEmpty(ArrDatos)=False then
				FOR I=Lbound(ArrDatos,2) to Ubound(ArrDatos,2)
					If session("codigo_usu")<>ArrDatos(0,i) then%>
					<option value="<%=ArrDatos(0,I)%>"><%=ArrDatos(1,I)%></option>
					<%end if
				NEXT
			end if%>
		  </select></td>
    <td width="10%" valign="top" height="90%">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onclick="AgregarItem(this.form.ListaDe)" class="cajas"><p>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onclick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
    <td width="45%" height="90%">
				<select multiple name="ListaPara" size="10" style="width: 100%; height:100%">
		</select></td>
  </tr>
</table>
</form>
</body>
</html>