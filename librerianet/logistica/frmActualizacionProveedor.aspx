<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizacionProveedor.aspx.vb" Inherits="logistica_frmActualizacionProveedor" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
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
        <asp:ScriptManager ID="smActualizacionProveedor" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%;">
            <tr>
                <td>
                     <asp:UpdatePanel ID="upBusqueda" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlBusqueda" runat="server" Visible ="true">                        
                    <table style="width: 100%;" border="0">
            <tr>
                <td>
                     <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="7" height="20px">
                                &nbsp;&nbsp; Búsqueda de Proveedor</td>
                        </tr>
                        <tr>
                            <td style ="width:10%">
                                </td>
                            <td  style ="width:20%">
                                &nbsp;</td>
                            <td style ="width:2%">
                                </td>
                            <td style ="width:15%">
                                </td>
                            <td style ="width:45%">
                                </td>
                            <td style ="width:3%">
                                </td>
                            <td style ="width:5%">
                                </td>
                        </tr>
                        <tr>
                            <td style ="width:10%">
                                R.U.C.:</td>
                            <td style ="width:20%">
                                <asp:TextBox ID="txtRuc" runat="server"></asp:TextBox>
                                </td>
                            <td style ="width:2%">
                                </td>
                            <td style ="width:15%">
                                Razón Social:</td>
                            <td style ="width:45%">
                                <asp:TextBox ID="txtRazSoc" runat="server" Width ="98%" ></asp:TextBox>
                                </td>
                            <td style ="width:3%">
                                </td>
                            <td style ="width:5%">
                                <asp:ImageButton ID="ImgBuscarSubasta" runat="server" 
                                    ImageUrl="~/images/busca.gif"/>
                            </td>
                        </tr>
                        <tr>
                            <td style ="width:10%">
                                </td>
                            <td style ="width:20%">
                                </td>
                            <td style ="width:2%">
                                </td>
                            <td style ="width:15%">
                                </td>
                            <td style ="width:45%">
                                </td>
                            <td style ="width:3%">
                                </td>
                            <td style ="width:5%">
                                &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:GridView ID="gvProveedor" runat="server" AutoGenerateColumns="False" 
                            BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                            ForeColor="#333333" GridLines="Horizontal" Width="100%" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="nombrePro" HeaderText="Razón social" />
                                <asp:BoundField DataField="rucPro" HeaderText="R.U.C." />
                                <asp:BoundField DataField="direccionPro" HeaderText="Dirección" />
                                <asp:TemplateField HeaderText="Participa">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkParticipa" runat="server" Enabled="False" Checked= '<%# Bind("participa") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfidPro" runat="server" value= '<%# Bind("idPro") %>'/>
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
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upActualizacion" runat="server">     
                    <ContentTemplate>
                        
                        <asp:Panel ID="pnlActualizacion" runat="server" Visible ="False"> 
                        <table style ="width:100%">
                        <tr>
                        <td>
                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="8" height="20px">
                                &nbsp;&nbsp; Actualizar Proveedor</td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                            </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                <asp:HiddenField ID="hfPro" runat="server" />
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                <asp:ImageButton ID="ibtnRegresar" runat="server" 
                                    ImageUrl="~/images/back.gif" ToolTip="Regresar" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                R.U.C.:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtARUC" runat="server" Enabled="False" Width="35%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                            </td>
                            <td style="width:10%">
                                Raz. Soc.:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtARazSoc" runat="server" Enabled="False" Width="98%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                </td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                            </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                        &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                Dirección:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtADireccion" runat="server" Width="90%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                Teléfono:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtATelefono" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                </td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                Fax:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtAFax" runat="server" Width="90%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                E-mail:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtAEmail" runat="server" Width="98%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                </td>
                            <td style="width:35%">
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                Usuario:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtAUsuario" runat="server" Enabled="False" Width="60%"></asp:TextBox>
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                Password:</td>
                            <td style="width:35%">
                                <asp:TextBox ID="txtAPassword" runat="server" Enabled="False" Width="60%"></asp:TextBox>
                                <asp:ImageButton ID="ibtnGenerarUsuario" runat="server" 
                                    ImageUrl="~/images/usuario.PNG" Width="19px" />
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                &nbsp;</td>
                        </tr>
                            <tr>
                                <td style="width:1%">
                                    </td>
                                <td style="width:10%">
                                    </td>
                                <td style="width:35%">
                                    </td>
                                <td style="width:2%">
                                    </td>
                                <td style="width:10%">
                                    </td>
                                <td style="width:35%">
                                    </td>
                                <td style="width:2%">
                                    </td>
                                <td style="width:5%">
                                    &nbsp;</td>
                            </tr>
                        <tr>
                            <td style="width:1%">
                                </td>
                            <td style="width:10%">
                                Ranking:</td>
                            <td style="width:35%">
                                <ajaxToolkit:Rating ID="rtgRanking" runat="server"
                                    CurrentRating="2"
                                    MaxRating="5"
                                    StarCssClass="ratingStar"
                                    WaitingStarCssClass="savedRatingStar"
                                    FilledStarCssClass="filledRatingStar"
                                    EmptyStarCssClass="emptyRatingStar" AutoPostBack="True"/> 
                                
                            </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:10%">
                                Participa:</td>
                            <td style="width:35%">
                                <asp:CheckBox ID="chkParticipa" runat="server" />
                                </td>
                            <td style="width:2%">
                                </td>
                            <td style="width:5%">
                                </td>
                        </tr>
                            <tr>
                                <td style="width:1%">
                                    </td>
                                <td style="width:10%">
                                    </td>
                                <td style="width:35%">
                                    </td>
                                <td style="width:2%">
                                    </td>
                                <td style="width:10%">
                                    </td>
                                <td style="width:35%">
                                    </td>
                                <td style="width:2%">
                                    </td>
                                <td style="width:5%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="8">
                                    <asp:ImageButton ID="ibtnGuardar" runat="server" 
                                        ImageUrl="~/images/dikette.gif" ToolTip="Guardar" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width:1%">
                                    &nbsp;</td>
                                <td style="width:10%">
                                    &nbsp;</td>
                                <td style="width:35%">
                                    &nbsp;</td>
                                <td style="width:2%">
                                    &nbsp;</td>
                                <td style="width:10%">
                                    &nbsp;</td>
                                <td style="width:35%">
                                    &nbsp;</td>
                                <td style="width:2%">
                                    &nbsp;</td>
                                <td style="width:5%">
                                    &nbsp;</td>
                            </tr>
                    </table>
                        </td>
                        </tr>
                        <tr>
                        <td>
                        <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;">
                        <tr>
                            <td style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" 
                                colspan="5" height="20px">
                                &nbsp;&nbsp; Categorías del Proveedor</td>
                        </tr>
                        <tr>
                            <td style="width:10%">
                                </td>
                            <td style="width:15%">
                                </td>
                            <td style="width:60%">
                                </td>
                            <td style="width:5%">
                                </td>
                            <td style="width:10%">
                                &nbsp;</td>
                        </tr>
                            <tr>
                                <td style="width:10%">
                                    </td>
                                <td style="width:15%">
                                    Categoría:</td>
                                <td style="width:60%">
                                    <asp:DropDownList ID="ddlCategoria" runat="server" Width="98%">
                                    </asp:DropDownList>
                                </td>
                                <td style="width:5%">
                                    <asp:ImageButton ID="ibtnAgregar" runat="server" 
                                        ImageUrl="~/images/agregar.gif" Width="16px" />
                                </td>
                                <td style="width:10%">
                                    </td>
                            </tr>
                            <tr>
                                <td style="width:10%">
                                    </td>
                                <td style="width:15%">
                                    </td>
                                <td style="width:60%">
                                    </td>
                                <td style="width:5%">
                                    </td>
                                <td style="width:10%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5">
                                    <asp:GridView ID="gvCategoriaPrv" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                        BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                                        Width="50%" PageSize="5">
                                        <Columns>
                                            <asp:BoundField DataField="desCategoria" HeaderText="Categoria" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hfcodCategoria" runat="server" value='<%# Bind("codCategoria") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ibtnEliminar" runat="server" 
                                                        ImageUrl="~/images/eliminar.gif" onclick="ibtnEliminar_Click" />
                                                </ItemTemplate>
                                                <ItemStyle Width="1%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                            BorderWidth="1px" ForeColor="#3366CC" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="5">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        </td>
                        </tr>
                        </table>
                    
                    
                        </asp:Panel>
                    </ContentTemplate>
                        <Triggers>
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
