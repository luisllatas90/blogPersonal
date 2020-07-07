<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CtrlFotoAlumno.ascx.vb" Inherits="CtrlFotoAlumno" %>
<asp:Panel ID="PanFoto" runat="server" Height="192px" Width="132px" 
    HorizontalAlign="Center">
    <asp:Panel ID="Panel3" runat="server" Height="25px" 
    Width="118px" HorizontalAlign="Center">   
        <asp:Label ID="LblCodigo" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="8pt" ForeColor="Black"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Height="126px" HorizontalAlign="Center" 
        Width="119px">
        <asp:Image ID="ImgFoto" runat="server" Height="119px" ImageAlign="Middle" 
            Width="104px" />
    </asp:Panel>
    <asp:Panel ID="Panel4" runat="server" Height="40px" HorizontalAlign="Center" 
        Width="118px">
        <asp:Label ID="LblNombres" runat="server" Font-Bold="True" Font-Names="Verdana" 
            Font-Size="8pt" ForeColor="Black"></asp:Label>
    </asp:Panel>
</asp:Panel>
