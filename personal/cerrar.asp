<!--
    '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
-->

<%
	session("tipo_usu")=""
	session("descripciontipo_usu")=""
	session("codigo_Usu")=""
	session("Ident_Usu")=""
	session("Nombre_Usu")=""
	session("codigo_Cco")= ""
	session("codigo_Dac")= ""
	session("Descripcion_Cco")= ""
	session("Descripcion_Dac")= ""
	session("Equipo_bit")=""
	session("Usuario_bit")=""

	'Almacenar datos del ciclo académico
	session("Codigo_Cac")=""
	session("descripcion_Cac")=""
	session("tipo_Cac")=""
	session("notaminima_cac")=""

	Session.Contents.RemoveAll() 'nuevo
	session.abandon
	
    response.Redirect("CerrarSesionNet.aspx")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<!--<meta http-equiv="refresh" content="2; url=http://www.usat.edu.pe/" />-->
<link rel="stylesheet" type="text/css" href="../private/estilo.css" />
<title>Cerrando sistema...</title>
</head>
<body>
    <h4 align="center">
        <img border="0" src="../images/close.loading.gif" width="26px" alt="Cierre de Sistema" /> Cerrando sistema...
    </h4>
</body>
</html>