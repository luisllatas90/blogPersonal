<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cuentausuario.aspx.vb" Inherits="servicios_mesaservicios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
     .titulo
        {
        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
        color: #E33439;
        font-size: 13pt;
        font-weight: bold;

        }
     .contenido
        {       
        font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
        color: #2F4F4F;
        font-size: 9pt;
        text-align:justify;
        }
        .enlace
        {
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            color: #E33439;
            font-size: 9pt;
            font-weight: bold;
            }
    </style>
</head>
<body>
     <form id="form1" runat="server">
    
     <h1 class="titulo">Cuenta de Usuario</h1>
     <hr />
     
    <p class="contenido">
    La DTI pone a disposición de cada colaborador USAT una cuenta de usuario que le permite el acceso al correo electrónico institucional, campus virtual, aula virtual, sistemas de la universidad y demás servicios que requieran autenticación. 
    </p>
     <p class="contenido">
         Las cuentas de usuario son de uso exclusivo e intransferible por lo que cada colaborador es responsable de mantener la confidencialidad de sus credenciales. El nombre de usuario y contraseña son comunicados al colaborador a través del Director de Departamento en el que labora. 
    </p>
     <p class="contenido">
         La contraseña es generada por primera vez automáticamente y por seguridad, debe ser cambiada después del primer ingreso a su cuenta. Adicionalmente, la contraseña tiene una vigencia de 120 días y es posible cambiarla en cualquier momento siguiendo los pasos indicados en la guía 
        <b><a href="GuiaCambioPassword.pdf" target="_blank">Cambio de contraseña</a></b>.
    </p>
    
   <p class="contenido">
        <b>Correo Electrónico</b>
    </p>
    
     <p class="contenido">
    La cuenta de correo electrónico sirve como medio de enlace y comunicación entre la USAT y sus colaboradores. 
    </p>
     <p class="contenido">
         &nbsp;La dirección de correo electrónico tiene el formato <b><a href="http://">usuario@usat.edu.pe</a></b>, el usuario está conformado por la inicial del primer nombre y el apellido paterno, sin embargo, puede ser distinto pues depende de la disponibilidad de nombres de usuario.
    </p>
    
    
    </form>
</body>
</html>
