<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmArbolCentroCostos.aspx.vb" Inherits="indicadores_POA_FrmArbolCentroCostos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        <style type="text/css">
         .menuseleccionado
            {
                background-color: #FFCC66;
                border: 1px solid #808080;  
            }   
            
            .menuporelegir
            {
                border: 1px solid #808080;
                background-color: #FFCC66;
            }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TreeView ID="treePrueba" runat="server" MaxDataBindDepth="4" ExpandDepth="0" Font-Size="XX-Small">
    <Nodes>
    </Nodes>
    <HoverNodeStyle CssClass="menuporelegir" />
    </asp:TreeView>
    </div>
    </form>
</body>
</html>
