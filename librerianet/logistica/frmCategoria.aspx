<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCategoria.aspx.vb" Inherits="logistica_frmCategoria" %>

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
    
        <asp:ScriptManager ID="smCategoria" runat="server">
        </asp:ScriptManager>
    
        <asp:UpdatePanel ID="upCategoria" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlBusqueda" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td>
                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="5" height="20px">
                                &nbsp;&nbsp; Búsqueda de Categoría</td>
                        </tr>
                        <tr>
                            <td style = "width:5%">
                                </td>
                            <td style = "width:10%">
                                </td>
                            <td style = "width:65%">
                            </td>
                            <td style = "width:10%">
                                &nbsp;</td>
                            <td style = "width:10%">
                                </td>
                        </tr>
                        <tr>
                            <td style = "width:5%">
                                &nbsp;</td>
                            <td style = "width:10%">
                                Categoría:</td>
                            <td style = "width:65%">
                                <asp:TextBox ID="txtDesCategoria" runat="server" Width ="70%"></asp:TextBox>
                            </td>
                            <td style = "width:10%">
                                <asp:ImageButton ID="ImgBuscar" runat="server" ImageUrl="~/images/busca.gif" 
                                    ToolTip="Buscar" Width="19px" />
                            </td>
                            <td style = "width:10%">
                                <asp:ImageButton ID="ibtnNuevo" runat="server" ImageUrl="~/images/agregar.gif" 
                                    ToolTip="Nueva Categoría" />
                            </td>
                        </tr>
                        <tr>
                            <td style = "width:5%">
                                </td>
                            <td style = "width:10%">
                                </td>
                            <td style = "width:65%">
                            </td>
                            <td style = "width:10%">
                                &nbsp;</td>
                            <td style = "width:10%">
                                        &nbsp;</td>
                        </tr>
                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvCategoria" runat="server" AutoGenerateColumns="False" 
                            BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                            ForeColor="#333333" GridLines="Horizontal" Width="60%" AllowPaging="True" 
                            PageSize="15">
                                <Columns>
                                    <asp:BoundField DataField="desCategoria" HeaderText="Categoría" />
                                    <asp:BoundField DataField="fecRegistro" HeaderText="Registro" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfcodCategoria" runat="server" value= '<%# Bind("codCategoria") %>'/>
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
                <asp:Panel ID="pnlCategoria" runat="server" Visible="False">
                
                    <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td colspan="5" height="20px" 
                                style="background-color: #e8eef7; color: #3366CC; font-weight: bold;">
                                &nbsp;&nbsp; Actualizar Categoría</td>
                        </tr>
                        <tr>
                            <td style="width:5%">
                            </td>
                            <td style="width:10%">
                                &nbsp;</td>
                            <td style="width:65%">
                                <asp:HiddenField ID="hfCategoria" runat="server" />
                            </td>
                            <td style="width:10%">
                                <asp:HiddenField ID="hfTransaccion" runat="server" />
                            </td>
                            <td style="width:10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:5%">
                                &nbsp;</td>
                            <td style="width:10%">
                                Categoría:</td>
                            <td style="width:65%">
                                <asp:TextBox ID="txtNDesCategoria" runat="server" Width="70%"></asp:TextBox>
                            </td>
                            <td style="width:10%">
                                &nbsp;</td>
                            <td style="width:10%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:5%">
                                &nbsp;</td>
                            <td style="width:10%">
                                Estado:</td>
                            <td style="width:65%">
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="70%">
                                    <asp:ListItem Value="A">Activo</asp:ListItem>
                                    <asp:ListItem Value="I">Inactivo</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width:10%">
                                <asp:ImageButton ID="ibtnGuardar" runat="server" 
                                    ImageUrl="~/images/dikette.gif" ToolTip="Guardar" Width="30px" />
                            </td>
                            <td style="width:10%">
                                <asp:ImageButton ID="ibtnRegresar" runat="server" ImageUrl="~/images/back.gif" 
                                    ToolTip="Regresar" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:5%">
                                &nbsp;</td>
                            <td style="width:10%">
                                &nbsp;</td>
                            <td style="width:65%">
                                &nbsp;</td>
                            <td style="width:10%">
                                &nbsp;</td>
                            <td style="width:10%">
                                &nbsp;</td>
                        </tr>
                    </table>
                
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="ibtnGuardar" />
            </Triggers>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
