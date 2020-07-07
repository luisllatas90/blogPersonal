<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleevento.aspx.vb" Inherits="evento" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Datos informativos del Evento</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
    <tr>
    <td bgcolor="#D1DDEF" colspan="2" height="30px">
                    <b>Datos informativos</b></td>
    </tr>
    <tr>
    <td width="15%" >
                    Nombre corto</td>
                <td width="85%">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
    <td width="15%" >
                    Nro. Resolución</td>
                <td width="85%">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
    </tr>
        <tr>
    <td width="15%" >
                                        Coordinador General</td>
                <td width="85%">
                    <asp:Label ID="Label4" runat="server" Font-Bold="True"></asp:Label>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                                        Coordinador Apoyo</td>
                <td width="85%">
                    <asp:Label ID="Label5" runat="server" Font-Bold="True"></asp:Label>
                </td>
        </tr>
        <tr>
    <td width="15%" >
                                        Fecha inicio propuesta</td>
                <td width="85%">
                    <asp:Label ID="Label17" runat="server" Font-Bold="True"></asp:Label>
&nbsp; / Fecha fin propuesta:
                    <asp:Label ID="Label18" runat="server" Font-Bold="True"></asp:Label>
                </td>
        </tr>
        <tr>
    <td bgcolor="#D1DDEF" colspan="2" height="30" width="100%">
                    <b>Precios / Descuentos por participante</td>
        </tr>
        <tr>
    <td width="15%" >
                    Meta de participantes</td>
                <td width="85%">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
    <td width="15%" >
                    Precios</td>
                <td width="85%">
                                        Contado: S/.
                                        &nbsp;<asp:Label ID="Label7" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
        </tr>
        <tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                        Financiado: S/.
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
        </tr>
        <tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                        Cuota Inicial: S/.
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
        </tr>
        <tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                        Nro de Cuotas:
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
        </tr>
        <tr>
    <td width="15%" >
                    % Descuentos</td>
                <td width="85%">
                                        Personal USAT:                                         <asp:Label ID="Label11" runat="server" Font-Bold="True"></asp:Label>
&nbsp;<b>%</b></td>
        </tr>
        <tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                        Alumno USAT:                                         <asp:Label ID="Label12" runat="server" Font-Bold="True"></asp:Label>
&nbsp;<b>%</b></td>
        </tr>
        <tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                        Corporativo:                                         <asp:Label ID="Label13" runat="server" Font-Bold="True"></asp:Label>
&nbsp;<b>%</b></td>
        </tr>
        <tr>
    <td width="15%" >
                    &nbsp;</td>
                <td width="85%">
                                        Egresado USAT:                                         <asp:Label ID="Label20" 
                                            runat="server" Font-Bold="True"></asp:Label>
&nbsp;%</td>
        </tr>
        <tr>
    <td bgcolor="#D1DDEF" colspan="2" height="30" width="100%">
                    <b>Otros datos</td>
        </tr>
        <tr>
    <td width="15%" >
                    Gestiona Notas</td>
                <td width="85%">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="True"></asp:Label>
            &nbsp;/
                                                <asp:Label ID="Label19" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
    <td width="15%"> Horarios</td>
                <td width="85%">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
    <td width="15%" >
                    Observaciones</td>
                <td width="85%">
                                        <asp:Label ID="Label16" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        </table>
</form>
</body>
</html>

