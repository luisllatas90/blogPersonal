<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmagregarunidadinvestigacion.aspx.vb" Inherits="DirectorInvestigacion_frmagregarunidadinvestigacion" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigacion :: Agregar Unidad de Investigación</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
    
    <script language="javascript" type="text/javascript">
        function validarlista(source, arguments)
            {
                if (document.form1.LstUnidades.value == "")
                    arguments.IsValid = false;
                else
                    arguments.IsValid = true;
            }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 10.5pt; color: midnightblue; font-family: verdana; height: 29px; text-align: center">
                    Agregar Unidad de Investigación</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    Buscar unidad de invesigación
                    <asp:TextBox ID="TextBox1" runat="server" Width="351px"></asp:TextBox>
                    <asp:Button ID="CmdBuscar" runat="server" CssClass="buscar1" Text="       Buscar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="69px" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Debe seleccionar un Item" ClientValidationFunction="validarlista" Display="Dynamic" ValidationGroup="agregar">*</asp:CustomValidator></td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:ListBox ID="LstUnidades" runat="server" Width="100%" Height="196px" ValidationGroup="agregar"></asp:ListBox></td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td align="right">
                    &nbsp;<asp:Button ID="CmdAgregar" runat="server" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" CssClass="attach_prp" Height="25px" Text="      Agregar" Width="78px" ValidationGroup="agregar" /></td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="agregar" ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
