<%@ Page Language="VB" AutoEventWireup="false" CodeFile="asistenciaremota.aspx.vb" Inherits="servicios_asistenciaremota" %>

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
    
    <table style="width:100%" cellpadding="0" cellspacing="0">
        <tr valign="top">
        <td style="text-align:center" colspan="3"><h1 class="titulo">Asistencia Remota<hr /></h1></td>
        </tr>   
        <tr valign="top">
        <td style="width:55%">        
       <p class="contenido">
        La asistencia remota es una herramienta de la <b>mesa de servicios TI </b>empleada en la atención de incidentes reportados en los equipos de cómputo y sistemas de la universidad en <b>oficinas del campus USAT.</b>
        </p>

    <p class="contenido">
    Esta asistencia es brindada solo por personal de la DTI permitiéndole realizar las tareas necesarias para el diagnóstico y, de ser posible, la resolución del incidente reportado.
    </p>
    <p class="contenido">
    Esta modalidad de atención requiere de la autorización del usuario para acceder de manera remota al equipo de cómputo. El usuario podrá ver en pantalla todas las acciones realizadas por el especialista de TI.
    </p>
    <p>
    <a href="../../librerianet/reglamentos/GuiaAsistenciaRemota.pdf" target="_blank" class="enlace">Conoce más sobre la asistencia remota aquí</a><br />
    </p>
        </td>
        <td style="width:15px">&nbsp;&nbsp;&nbsp;
        </td>
        <td style="width:40%">
         <p>   
         <%--<iframe width="560" height="315" src="https://www.youtube.com/embed/6EryUdZeak4" frameborder="0" allowfullscreen></iframe>        --%>
         <a href="https://www.youtube.com/embed/rWpoMIUPgrI" target="_blank"><img alt="" src="../images/menus/videomesadeservicios.png"  style="width:100%"/></a>
        </p>
        </td>
        </tr>     
    </table>    

    </form>
</body>
</html>
