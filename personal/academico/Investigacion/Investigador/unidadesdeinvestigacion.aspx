<%@ Page Language="VB" AutoEventWireup="false" CodeFile="unidadesdeinvestigacion.aspx.vb" Inherits="Investigador_unidadesdeinvestigacion" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Consultar Unidades de Investigacion</title>
  </head>
<body style="margin-left:0; margin-right:0">
    <form id="form1" runat="server">
    <div >
        <table style="width: 100%; height: 100%;">
            <tr>
                <td colspan="2" style="border-top-width: 1px; border-left-width: 1px; border-bottom-width: 1px;
                    height: 24px; border-right-width: 1px" valign="top">
                    <table style="width: 100%; border-top: maroon 1px solid; font-weight: bold; color: maroon; border-bottom: maroon 1px solid; font-family: verdana; background-color: #f0f0f0; text-align: center; font-size: 10.5pt;">
                        <tr>
                            <td style="width: 50%; height: 21px;">
                    Areas, Lineas y Temáticas</td>
                            <td style="height: 21px">
                    Descripción</td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
            <tr>
                <td valign="top" style="border-top-width: 1px; border-left-width: 1px; border-bottom-width: 1px; width: 50%; border-right-width: 1px">
        <asp:TreeView ID="TreeUnidades" runat="server" ImageSet="Faq">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px"
                VerticalPadding="0px" BackColor="PowderBlue" Font-Bold="True" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
                </td>
                <td valign="top" align="center" style="width: 50%;">
                    <table style="width: 90%">
                        <tr>
                            <td colspan="3" rowspan="3" style="font-weight: bold; font-size: 10pt; color: navy; font-family: verdana; height: 35px; text-transform: uppercase;" align="center">
                                <asp:Label ID="LblTitulo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td colspan="3" rowspan="1" style="font-size: 10pt; font-family: verdana; text-align: justify; border-right: black 1px solid; padding-right: 10px; border-top: black 1px solid; padding-left: 10px; padding-bottom: 10px; border-left: black 1px solid; padding-top: 10px; border-bottom: black 1px solid; background-color: lemonchiffon;">
                                <asp:Label ID="LblProposito" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
           
    </div>
    </form>
</body>
</html>
