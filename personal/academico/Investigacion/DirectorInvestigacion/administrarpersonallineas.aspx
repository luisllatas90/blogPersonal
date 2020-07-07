<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrarpersonallineas.aspx.vb" Inherits="DirectorInvestigacion_administrarpersonallineas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invetigación :: Personal de Invetsigación</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div><center>
        <table>
            <tr>
                <td align="center" colspan="3" style="font-weight: bold; font-size: 10.5pt; color: midnightblue;
                    height: 34px">
                    LÍNEAS DE INVESTIGACIÓN Y PERSONAL ASIGNADO</td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <table class="contornotabla" style="width: 100%">
                        <tr>
                            <td style="font-weight: bold; color: black; height: 36px">
                                &nbsp; &nbsp; Unidad de Investigacion</td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLUnidad" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: black; height: 36px">
                                &nbsp; &nbsp; Area de Investigación</td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLArea" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: black; height: 36px">
                                &nbsp; &nbsp; Línea de Investigación</td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLLinea" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; color: black; height: 36px">
                                <asp:Label ID="LblTematica" runat="server" Text="Temática de Investigación" Visible="False"></asp:Label></td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="DDLTematica" runat="server" AutoPostBack="True" Visible="False"
                                    Width="422px">
                                </asp:DropDownList></td>
                            <td style="height: 36px">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="border-top: black 1px solid">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center" style="font-weight: bold; color: maroon" colspan="2">
                                            Personal que pertenece a la Linea</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Horizontal" Width="100%">
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="N&#176;">
                                                        <ItemStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="datos_per" HeaderText="Personal" SortExpression="datos_per" />
                                                </Columns>
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        </center>
        </div>
        <asp:ObjectDataSource ID="Personal" runat="server" SelectMethod="ConsultarPersonaldeLineaInvestigacion"
            TypeName="Investigacion">
            <SelectParameters>
                <asp:ControlParameter ControlID="DDLUnidad" Name="codigo_cco" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="DDLLinea" Name="codigo_lin" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:Parameter DefaultValue="1" Name="tipo" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HddCodigoCco" runat="server" />
    </form>
</body>
</html>
