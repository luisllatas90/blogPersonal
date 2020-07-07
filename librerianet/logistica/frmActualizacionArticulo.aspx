<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizacionArticulo.aspx.vb" Inherits="logistica_frmActualizacionArticulo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="smActualizarArticulo" runat="server">
        </asp:ScriptManager>

                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upBusqueda" runat="server">
                            <ContentTemplate>
                            <asp:Panel ID="pnlBusqueda" runat="server">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                                                <tr>
                                                    <td colspan="5" height="20px" 
                                                        style="background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                                        &nbsp;&nbsp; Búsqueda de Artículo</td>
                                                </tr>
                                                <tr>
                                                    <td style="width:5%">
                                                    </td>
                                                    <td style="width:10%">
                                                    </td>
                                                    <td style="width:65%">
                                                    </td>
                                                    <td style="width:10%">
                                                        &nbsp;</td>
                                                    <td style="width:10%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:5%">
                                                        &nbsp;</td>
                                                    <td style="width:10%">
                                                        Artículo:</td>
                                                    <td style="width:65%">
                                                        <asp:TextBox ID="txtDescripcionArt" runat="server" Width="70%"></asp:TextBox>
                                                    </td>
                                                    <td style="width:10%">
                                                        <asp:ImageButton ID="ImgBuscar" runat="server" ImageUrl="~/images/busca.gif" 
                                                            ToolTip="Buscar" Width="19px" />
                                                    </td>
                                                    <td style="width:10%">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width:5%">
                                                    </td>
                                                    <td style="width:10%">
                                                    </td>
                                                    <td style="width:65%">
                                                    </td>
                                                    <td style="width:10%">
                                                        &nbsp;</td>
                                                    <td style="width:10%">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvArticulo" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                                                PageSize="15" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" />
                                                    <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" />
                                                    <asp:BoundField DataField="descripcioncls" HeaderText="Clase" />
                                                    <asp:BoundField DataField="descripcionRub" HeaderText="Rubro" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hfidArt" runat="server" 
                                                                value='<%# Bind("idArt") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="gvArticulo" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upActualizacion" runat="server">
                            <ContentTemplate>
                            <asp:Panel ID="pnlActualizacion" runat="server" Visible="False">
                                <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                                <tr>
                                    <td colspan="8" height="20px" 
                                        style="background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                        &nbsp;&nbsp; Actualizar Artículo</td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                    </td>
                                    <td style="width:15%">
                                    </td>
                                    <td style="width:30%">
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:15%">
                                    </td>
                                    <td style="width:30%">
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:5%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                    </td>
                                    <td style="width:15%">
                                    </td>
                                    <td style="width:30%">
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:15%">
                                    </td>
                                    <td style="width:30%">
                                        <asp:HiddenField ID="hfIdArticulo" runat="server" />
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:5%">
                                        <asp:ImageButton ID="ibtnRegresar" runat="server" ImageUrl="~/images/back.gif" 
                                            ToolTip="Regresar" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                    </td>
                                    <td style="width:15%">
                                        Descpripción:</td>
                                    <td style="width:30%">
                                        <asp:TextBox ID="txtDescripcion" runat="server" Width="98%"></asp:TextBox>
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:15%">
                                        Des. Resumida:</td>
                                    <td style="width:30%">
                                        <asp:TextBox ID="txtDescripcionRes" runat="server" Width="98%"></asp:TextBox>
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:5%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:1%">
                                    </td>
                                    <td style="width:15%">
                                    </td>
                                    <td style="width:30%">
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:15%">
                                    </td>
                                    <td style="width:30%">
                                    </td>
                                    <td style="width:2%">
                                    </td>
                                    <td style="width:5%">
                                        &nbsp;</td>
                                </tr>
                                    <tr>
                                        <td style="width:1%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Unidad:</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlUnidad" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Clase:</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlClase" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:1%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:1%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Rubro:</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlRubro" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Categoría:</td>
                                        <td style="width:30%">
                                            <asp:DropDownList ID="ddlCategoria" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:1%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:2%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                    </tr>
                                <tr>
                                    <td align="center" colspan="8">
                                        <asp:ImageButton ID="ibtnGuardar" runat="server" 
                                            ImageUrl="~/images/dikette.gif" ToolTip="Guardar" />
                                    </td>
                                </tr>
                            </table>
                        
                            </asp:Panel>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="ibtnRegresar" />
                                    <asp:PostBackTrigger ControlID="ibtnGuardar" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>

    
    </div>
    </form>
</body>
</html>
