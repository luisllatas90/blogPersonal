<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistarRequerimientos.aspx.vb" Inherits="SolicitudRequerimientos" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href ="../private/estilo.css" rel="stylesheet" type="text/css" />
    <title>Página sin título</title>
</head>
<body>
    <form id="frmRegRequerimientos" runat="server">
    <div>
        <table width="90%">
            <tr>
                <td colspan="3" rowspan="3" style="text-align: center">
                    <table width="90%">
                        <tr>
                            <td colspan="3" style="height: 21px" class="usatCeldaTitulo">
                                Registrar Requerimientos de una solicitud</td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                            </td>
                            <td style="height: 15px">
                            </td>
                            <td style="height: 15px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px">
                            </td>
                            <td style="height: 15px">
                            </td>
                            <td style="height: 15px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GridView1" runat="server">
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
