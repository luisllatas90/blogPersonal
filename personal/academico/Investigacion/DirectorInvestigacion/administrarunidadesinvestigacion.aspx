<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrarunidadesinvestigacion.aspx.vb" Inherits="DirectorInvestigacion_administrarunidadesinvestigacion" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones :: Administración de Unidades de Investigación</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div> <center>
        <table style="width: 707px">
            <tr>
                <td colspan="3" style="font-weight: bold; font-size: 10.5pt; color: midnightblue; font-family: verdana; height: 42px; text-align: center">
                    Mantenimiento de Unidades de Investigación</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="CmdAgregar" runat="server" Text="      Agregar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="attach_prp" Height="25px" Width="78px" /></td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:GridView ID="GridView1"  runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        DataSourceID="Unidades" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="2" GridLines="Horizontal" DataKeyNames="codigo_cco">
                        <Columns>
                            <asp:BoundField HeaderText="N&#176;" >
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_cco" SortExpression="codigo_cco" Visible="False" />
                            <asp:BoundField DataField="descripcion_cco" HeaderText="Unidad de Investigaci&#243;n"
                                SortExpression="descripcion_cco" />
                            <asp:BoundField DataField="NAreas" HeaderText="N&#176; Areas" SortExpression="NAreas" >
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:CommandField  ShowDeleteButton="True"  ButtonType="Image" DeleteImageUrl="../../../../images/menus/Eliminar.gif" DeleteText="Eliminar una Unidad de Investigaci&#243;n" >
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:CommandField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <RowStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Verdana" Font-Size="9pt"
                            ForeColor="White" HorizontalAlign="Center" />
                    </asp:GridView>
                </td>
            </tr>
        </table> </center>
    
    </div>
        <asp:ObjectDataSource ID="Unidades" runat="server" SelectMethod="ConsultarUnidadesInvestigacion"
            TypeName="Investigacion" DeleteMethod="ModificarUnidadInvestigacion">
            <SelectParameters>
                <asp:Parameter DefaultValue="11" Name="tipo" Type="String" />
                <asp:Parameter DefaultValue="&quot;&quot;" Name="param1" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="tipo" Type="Int32" />
                <asp:Parameter Name="codigo_cco" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
