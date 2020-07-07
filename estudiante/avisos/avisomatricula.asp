<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Matr&iacute;cula 2008</title>
</head>
<%

cp=Request.QueryString("cp")

select case cp

case 24:
'medicina
%>
<img src="imagenes/Medicina.jpg"/>
<%
case 31:
'odontología
%>
<img src="imagenes/odontologia.jpg"/>
<%
case 5,6,7,8,12,14,15,16,17,26:
'educación
%>
<img src="imagenes/Educacion.jpg"/>
<%
case else:
'demás carreras
%>
<img src="imagenes/Escuelas.jpg"/>
<%
end select
%>
<body>
</body>
</html>