<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vstdocumentosexportados.aspx.vb" Inherits="vstdocumentosexportados" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Documentos publicados</title>
    <style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline;}</style>
</head>
<body>
    <form id="form1" runat="server">
           <asp:TreeView ID="trw" runat="server" 
                    ShowExpandCollapse="False" EnableViewState="False" 
               Font-Strikeout="False" CssClass="arbol">
                <NodeStyle NodeSpacing="2px" Font-Names="Verdana" Font-Size="8pt" />
            </asp:TreeView>
    
     </form>
</body>
</html>
