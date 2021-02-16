<%
on error resume next
codigo_usu=session("codigo_usu")
tipo_usu=session("tipo_usu")

If codigo_usu = "" Then codigo_usu=request.querystring("id")

session("usu_biblioteca") = codigo_usu 'agregado el 05/06/2012
session("Tusu_biblioteca")= tipo_usu

'response.Write codigo_usu & " - "
'response.Write tipo_usu

If codigo_usu <> "" Then
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Libros Electrónicos</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <script>
        function click() {
            if (event.button == 2) {
                alert('Esta opción se encuentra deshabilitada');
            }
        } 
    </script>

    <style type="text/css">
        .Estilo1
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-weight: bold;
            color: #990000;
            font-size: 12px;
        }
        .Estilo2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
        .Estilo3
        {
            font-size: 12px;
        }
        .Estilo4
        {
            width: 472px;
        }
        .Estilo5
        {
            width: 71%;
            height: 109px;
        }
        .style3
        {
            width: 247px;
        }
    </style>
</head>
<body>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr class="azul">
            <td align="center">
                <h3>Acceso a Libros Electrónicos</h3>
            </td>
        </tr>
    </table>
    <table width="99%" border="1" align="center" cellpadding="5" cellspacing="0">
        <!-- CIBERINDEX 23,24,25,26 -->
        <br />
        <tr class="rojo">
            <td colspan="2">                
                <h3><br/>Multidisciplinaria</h3>                
            </td>
        </tr>
        <!-- BIDI -->
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=75" target="_blank" class="Estilo2">
                    <img src="../images/BIDI_logo.png" alt="BIDI" width="120" height="180" border="1" />
                </a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3">
                        <strong class="azul">Bidi</strong> es una biblioteca digital de eBooks de reconocidos sellos editoriales. Actualmente, la USAT cuenta con eBooks para las siguientes carreras:                        
                        <ol>
                            <li>Administración de Empresas</li>
                            <li>Economía</li>
                            <li>Derecho</li>
                            <li>Filosofía y Teología</li>
                            <li>Arquitectura</li>
                            <li>Comunicación</li>
                            <li>Ing. Industrial</li>
                            <li>Psicología</li>
                        </ol>
                        <strong>A tu correo USAT se te ha enviado el enlace para crear tu cuenta.</strong>
                    </span>
                </p>
                <br/>
                <a class="usatenlace" href="https://www.youtube.com/watch?v=uA2Cht66SXA&feature=youtu.be" target="_blank">
                    <b class="rojo">&nbsp;&gt;&gt; Ver video tutorial de registro</b>
                </a>         
                <br/><br/>
            </td>
        </tr>
    </table>
</body>
</html>
<%
End If 

If Err.number <> 0 Then
    response.Write Err.Description
End If
%>