<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroCoordinadores.aspx.vb"
    Inherits="Egresados_frmRegistroCoordinadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>

    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>

    <script type="text/javascript" src="scripts/jquery-1.3.2.js"></script>

    <script type="text/javascript" src="scripts/jHtmlArea-0.7.5.min.js"></script>

    <link rel="Stylesheet" type="text/css" href="style/jHtmlArea.css" />

    <script type="text/javascript" src="scripts/jHtmlArea.ColorPickerMenu-0.7.0.js"></script>

    <link rel="Stylesheet" type="text/css" href="style/jHtmlArea.ColorPickerMenu.css" />
    <style type="text/css">
        .style1
        {
            width: 90%;
        }
        TBODY
        {
            display: table-row-group;
        }
        .style2
        {
        }
        select
        {
            font-family: Verdana;
            font-size: 8.5pt;
        }
        .buscar2
        {
            border: 1px solid #666666;
            background: #FEFFE1 url('../../images/previo.gif') no-repeat 0% center;
            width: 80;
            font-family: Tahoma;
            font-size: 8pt;
            height: 20;
            cursor: hand;
        }
        .style3
        {
            width: 84px;
        }
        .agregar2
        {
            border: 1px solid #666666;
            background: #FEFFE1 url('../../images/anadir.gif') no-repeat 0% center;
            width: 80;
            font-family: Tahoma;
            font-size: 8pt;
            height: 20;
            cursor: hand;
        }
        .style4
        {
        }
        .style5
        {
        }
        .style6
        {
        }
        .style9
        {
            width: 284px;
        }
        .style10
        {
            width: 214px;
        }
        .style11
        {
            height: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="style1">
            <tr>
                <td colspan="6">
                    <table width="100%">
                        <tr>
                            <td style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;"
                                height="40px" bgcolor="#E6E6FA">
                                <br />
                                <asp:Label ID="lblTitulo" runat="server" Text="Registro de Coordinadores" Font-Bold="True"
                                    Font-Size="11pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    Carrera Profesional:
                </td>
                <td>
                    <asp:DropDownList ID="ddlEscuela" runat="server" Height="20px" Width="265px">
                    </asp:DropDownList>
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    Personal:
                </td>
                <td>
                    <asp:DropDownList ID="ddlPersonal" runat="server" Height="20px" Width="265px">
                    </asp:DropDownList>
                </td>
                <td class="style9">
                    &nbsp;
                    <asp:Button ID="btnNuevo" runat="server" Text="Agregar" CssClass="agregar2" Width="110px"
                        Height="22px" />
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style11" colspan="6">
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="2">
                    <asp:Button ID="btnBusca" runat="server" Text="Consultar" CssClass="buscar2" Width="109px"
                        Height="21px" />
                </td>
                <td>
                    &nbsp;</td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="6">
                    <%--<asp:GridView ID="dgv_Personal" runat="server" Width="99%" AutoGenerateColumns="False"
                        PageSize="15" CellPadding="2" ForeColor="#333333" Font-Size="Smaller" DataKeyNames="codigo_per,codigo_cpf">
                        <Columns>
                            <asp:BoundField DataField="codigo_alucor" HeaderText="Id" />
                            <asp:BoundField DataField="persona" HeaderText="Personal" />
                            <asp:BoundField DataField="escuela" HeaderText="Escuela" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" EditText="Eliminar"
                                HeaderText="Eliminar" ShowDeleteButton="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" Visible="False" />
                            <asp:BoundField DataField="codigo_Cpf" HeaderText="codigo_Cpf" Visible="False" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Height="25px" Font-Bold="True" />
                        <RowStyle Height="22px" Wrap="False" BackColor="#F7F6F3" ForeColor="#333333" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>--%>
                    <asp:GridView ID="dgv_Personal" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_alucor,codigo_per,codigo_cpf"
                        Width="100%">
                        <Columns>
                            <%--<asp:BoundField DataField="codigo_alucor" HeaderText="Id" HtmlEncode="false">
                                <HeaderStyle Width="50px" />
                                <ItemStyle BorderColor="DarkGray" BorderStyle="Solid" Width="50px" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="persona" HeaderText="Personal" HtmlEncode="false">
                                <HeaderStyle Width="350px" />
                                <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="escuela" HeaderText="Carrera Profesional" HtmlEncode="false">
                                <HeaderStyle Width="350px" />
                                <ItemStyle BorderColor="DarkGray" BorderStyle="Solid" Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado" HtmlEncode="false" >
                                <HeaderStyle Width="100px" />
                                <ItemStyle BorderColor="DarkGray" BorderStyle="Solid" Width="100px" />
                            </asp:BoundField>
                            <%--<asp:CommandField ButtonType="Image" HeaderText="EDITAR" SelectImageUrl="../../../images/editar_poa.png"
                                ShowSelectButton="True">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:CommandField>--%>
                            <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="../../images/eliminar.gif" OnClick="ibtnElimina_Click" OnClientClick="return confirm('¿Desea Eliminar?.')"
                                        Text="Eliminar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="ELIMINAR OBJETIVO" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="EliminarObjetivo1" runat="server" CausesValidation="False" CommandName="Delete"
                                        ImageUrl="../../Images/menus/noconforme_small.gif" OnClick="ibtnEliminaObjetivo_Click"
                                        OnClientClick="return confirm('¿Desea Eliminar el Objetivo?.')" Text="Eliminar" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="codigo_alucor" HeaderText="codigo_alucor" Visible="False" />
                            <asp:BoundField DataField="codigo_Per" HeaderText="codigo_Per" Visible="False" />
                            <asp:BoundField DataField="codigo_Cpf" HeaderText="codigo_Cpf" Visible="False" />
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style4" colspan="6">
                    <asp:Label ID="lblMensajeFormulario" runat="server" Font-Bold="True" ForeColor="#3366CC"
                        Width="99%" Style="margin-top: 0px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td class="style9">
                    &nbsp;
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style5" colspan="6">
                    <asp:Label ID="lbl_Mensaje" runat="server" Font-Bold="True" ForeColor="#3366CC" Width="99%"
                        Style="margin-top: 0px"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
