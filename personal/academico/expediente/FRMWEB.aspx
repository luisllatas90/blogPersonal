<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FRMWEB.aspx.vb" Inherits="FRMWEB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <asp:Button ID="cmdcargar" runat="server" BorderStyle="None" Text="Cargar" /><br />
        <br />
        <br />
        &nbsp;<table>
            <tr>
                <td style="width: 645px; height: 1149px">
                    <asp:TreeView ID="trvcarpetas" runat="server" Height="32216px" ShowLines="True" Width="624px">
                    </asp:TreeView>
                </td>
                <td bgcolor="#66ff66" style="width: 388px; height: 1149px">
                    <asp:ListBox ID="lstarchivos" runat="server" Height="736px" Width="520px"></asp:ListBox></td>
                <td style="width: 100px; height: 1149px">
                </td>
            </tr>
            <tr>
                <td style="width: 645px">
                </td>
                <td style="width: 388px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 645px">
                </td>
                <td style="width: 388px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        &nbsp; &nbsp;
    
    </div>
    </form>
</body>
</html>
