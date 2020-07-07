<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudesProgramadas.aspx.vb" Inherits="Equipo_SolicitudesProgramadas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="Stylesheet" type ="text/css"  />
    <link href="../private/estiloweb.css" rel="Stylesheet" type ="text/css"  />
    <script language="javascript" type="text/javascript" src="../private/funcion.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="3" style="height: 107px; text-align: center; border-bottom: #3399cc thin solid;" class="titulocel">
                    <table id="TblBuscar" class="fondoblanco" width="95%" style="height: 31px">
                        <tr>
                            <td align="left" colspan="1" style="width: 73px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                Buscar por</td>
                            <td align="left" colspan="1" style="width: 1px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                &nbsp;:</td>
                            <td align="left" colspan="3" style="border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none; width: 472px;">
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" Width="87px">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">M&#243;dulo</asp:ListItem>
                                    <asp:ListItem Value="2">Prioridad</asp:ListItem>
                                    <asp:ListItem Value="3">&#193;rea</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                            </td>
                            <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar" Height="26px" Text="Buscar" /></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="1" style="width: 73px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 6px; text-align: left; border-bottom-style: none">
                            </td>
                            <td align="left" colspan="1" style="width: 1px; border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none; height: 6px;">
                            </td>
                            <td align="left" colspan="3" style="border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none; height: 6px; width: 472px;">
                                <asp:DropDownList ID="CboValor" runat="server" Visible="False" Width="490px">
                                </asp:DropDownList></td>
                            <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none; height: 6px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 24px; text-align: center;">
                    <table width="95%">
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 15px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 150px; text-align: center">
                <iframe src="DatosSolicitud.aspx" id ="ifDatos" name="ifDatos" frameborder ="0" width="95%"></iframe>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
