<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTestAjax.aspx.vb" Inherits="investigacion_frmTestAjax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </td>
                
                 <asp:UpdatePanel ID="upContenido" runat="server">
                    <ContentTemplate>
                        <td>
                            <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
                        </td>
                    </ContentTemplate>
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                     </Triggers>
                </asp:UpdatePanel>
                
                
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
