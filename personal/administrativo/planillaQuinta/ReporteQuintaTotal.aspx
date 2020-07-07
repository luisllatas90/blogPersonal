<%@ Page  Language="VB" AutoEventWireup="false" CodeFile="ReporteQuintaTotal.aspx.vb" Inherits="librerianet_planillaQuinta_ReporteQuintaTotal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"> </script>
    <style type="text/css">
        .style1
        {
            color: blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"  >
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="cmdExportar" />
        </Triggers>
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td>
                        Planilla:
                        <asp:DropDownList ID="cboPlanilla" runat="server">
                        </asp:DropDownList>
                        &nbsp;<asp:Button ID="cmdConsultar" runat="server" Text="Consultar" />
                        &nbsp;<asp:Button ID="cmdExportar" runat="server" Text="Exportar" />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                            AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <span class="style1">Espere un momento... la consulta se 
                    esta procesando<img alt="" src="../../../images/loading.gif" /> </span>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvQuintas" runat="server" 
                DataSourceID="objQuintas">
                            <HeaderStyle BackColor="#FF9900" ForeColor="White" />
                        </asp:GridView>
                        <asp:ObjectDataSource ID="objQuintas" runat="server" 
                SelectMethod="ConsultarQuintaTotalPorAcceso" TypeName="clsPlanilla">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="cboPlanilla" 
                        Name="codigo_plla" PropertyName="SelectedValue" Type="Int64" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
