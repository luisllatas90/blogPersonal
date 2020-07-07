<!--#include file="clsrecepcion.asp"-->
<%
Dim Tipo
Dim Razon
Dim email
Dim	direccion
Dim	telefono

tabla=request.querystring("tabla")
modo=request.QueryString("modo")
idprocedencia=request.QueryString("idprocedencia")

If modo="modificarprocedencia" then
	Dim archivo
	set archivo=new clsrecepcion
		ArrDatos=archivo.ConsultarProcedencia("2",idprocedencia)
	set archivo=nothing
		If IsEmpty(ArrDatos)=false then
			Tipo=ArrDatos(1,0)
			razon=ArrDatos(2,0)
			direccion=ArrDatos(3,0)
			telefono=ArrDatos(4,0)
			email=ArrDatos(5,0)
		End If
End If
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar Procedencia</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validararchivo.js"></script>
</head>

<body>
<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <u>
<span style="background-repeat: repeat; background-position: 0 0"><b>
Recomendaciones</b></span><b> para el registro según el tipo de procedencia:</b></u><br>
1. Personas naturales:&nbsp; Ej. Pérez López, Jorge. <br>
2. Institución: [<i>Nombre de institución</i>] de [<i>Lugar</i>]-[<i>Sub Área</i>] Ej. Gobierno Regional de 
Lambayeque-Secretaría Gral.</p>
<form name="frmprocedencia" Method="POST" onSubmit="return validarprocedencia()" ACTION="procesaratributos.asp?modo=<%=modo%>&tabla=<%=tabla%>&idprocedencia=<%=idprocedencia%>">
<fieldset style="padding: 2; width:100%">
<legend class="e1">Registrar procedencia del documento</legend>
<table border="0" class="Listas" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr>
    <td width="25%" height="20">Tipo</td>
    <td width="75%" height="20">
    <input type="radio" onClick="cambiarRazon('0')" value="0" <%if tipo=0 then response.write "checked" end if%> name="tipoprocedencia" checked>Personal
    <input type="radio" onClick="cambiarRazon('1')" value="1" <%if tipo=1 then response.write "checked" end if%> name="tipoprocedencia">Institucional</td>
  </tr>
  <tr>
    <td width="25%" height="19" id="razonsocial"><b>Apellidos y Nombres</b></td>
    <td width="75%" height="19">
    <input type="text" name="razon" size="61" class="cajas" value="<%=Razon%>"></td>
  </tr>
  <tr>
    <td width="25%" height="19">email</td>
    <td width="75%" height="19">
    <input type="text" name="email" size="61" class="cajas" value="<%=email%>"></td>
  </tr>
  <tr>
    <td width="25%" height="19">Dirección</td>
    <td width="75%" height="19">
    <input type="text" name="direccion" size="61" class="cajas" value="<%=Direccion%>"></td>
  </tr>
  <tr>
    <td width="25%" height="19">Teléfono</td>
    <td width="75%" height="19">
    <input type="text" name="telefono" size="32" class="cajas" value="<%=Telefono%>"></td>
  </tr>
  <tr>
    <td width="25%" height="34">&nbsp;</td>
    <td width="75%" height="34">
    <input type="submit" class="guardar" value="Guardar">
    <input type="button" class="salir" onclick="history.back()" value="Cancelar" name="salir"></td>
  </tr>
</table>
</fieldset>
</form>
</body>
</html>