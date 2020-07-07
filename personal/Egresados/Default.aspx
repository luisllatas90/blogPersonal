<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Egresados_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdateProgress runat="server" id="PageUpdateProgress">
            <ProgressTemplate>
                Loading...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
                <asp:Button runat="server" id="UpdateButton" onclick="UpdateButton_Click" text="Update" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
