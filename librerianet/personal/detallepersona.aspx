<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detallepersona.aspx.vb" Inherits="personal_detallepersona" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Datos informativos del Evento</title>    
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script src="script/jquery-3.1.0.min.js" type="text/javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
    <tr>
        <td align="center" rowspan="15" style="font-size: 10pt; text-transform: none; color: darkred;
                            font-family: Arial; height: 21px">
                            &nbsp;<asp:Image ID="imgfoto" runat="server" BorderColor="Black" BorderWidth="1px" Height="135px" />                            
                        </td>
        <td bgcolor="#D1DDEF" colspan="2" height="30px"><b>Datos personales</b></td>
    </tr>
    <tr>
        <td width="15%" >Nombre </td>
        <td width="85%"><asp:Label ID="lblNombre" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Estado Civil</td>
        <td width="85%"><asp:Label ID="lblEstadoCivil" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="15%" >Nacionalidad</td>
        <td width="85%"><asp:Label ID="lblNacionalidad" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="15%"> Fecha de Nacimiento</td>
        <td width="85%"><asp:Label ID="lblFNacimiento" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Sexo</td>
        <td width="85%"><asp:Label ID="lblSexo" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Doc. Identidad</td>
        <td width="85%"><asp:Label ID="lblDNI" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>    
    <tr>
        <td bgcolor="#D1DDEF" colspan="2" height="30" width="100%"><b>Datos laborales</td>
    </tr>
    <tr>
        <td width="15%" >Centro Costos</td>
        <td width="85%"><asp:Label ID="lblCco" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Tipo Persona</td>
        <td width="85%"><asp:Label ID="lblTipo" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Dedicación</td>
        <td width="85%"><asp:Label ID="lblDedicacion" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Horas semanales</td>
        <td width="85%"><asp:Label ID="lblHorasSemanales" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Estado</td>
        <td width="85%"><asp:Label ID="lblEstado" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td width="15%" >Correo</td>
        <td width="85%"><asp:Label ID="lblCorreo" runat="server" Font-Bold="True"></asp:Label></td>
    </tr>    

 </table>
</form>
</body>
</html>

