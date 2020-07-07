<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrarAreasLineasTematicas.aspx.vb" Inherits="DirectorInvestigacion_administrarAreasLineasTematicas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigación :: Unidades de Investigación</title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="center" colspan="2" style="font-weight: bold; font-size: 10.5pt; color: midnightblue;
                    height: 42px">
                    ÁREAS - LÍNEAS - TEMÁTICAS</td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2">
                    <asp:TreeView ID="TreeUnidades" runat="server" ExpandDepth="0" ImageSet="Faq" LineImagesFolder="~/TreeLineImages"
                        ShowLines="True">
                        <ParentNodeStyle Font-Bold="False" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
                        <SelectedNodeStyle BackColor="LightBlue" Font-Bold="True" Font-Underline="True" HorizontalPadding="0px"
                            VerticalPadding="0px" />
                        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                            NodeSpacing="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
