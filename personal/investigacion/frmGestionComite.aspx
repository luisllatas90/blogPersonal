<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionComite.aspx.vb" Inherits="frmGestionComite" EnableEventValidation ="true" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
   
<link href="../css/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    <link href="../css/MyStyles.css" rel="stylesheet" type="text/css" />
    <script src="private/funciones.js" type="text/javascript"></script>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestión Comité</title>    
    <style type="text/css">
    .addmiembros {
	border: 0px solid #C0C0C0;
	background: #f0f0f0 url('../Images/asignar.gif') no-repeat 0% 50%;
	font-family: Tahoma;
	font-size: 8pt;
	font-weight: bold;
	height: 15;
	cursor: hand;
}
    </style>
</head>

<body>
    <form id="form1" runat="server">

    <div>
        <asp:ScriptManager ID="smGestionComite" runat="server">
        </asp:ScriptManager>

            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upGestionComite" runat="server">
                        <ContentTemplate>
                        <asp:Panel ID="pnlListado" runat="server">
                            <table style="width:100%;">
                                <tr>
                                    <td style="width:100%;background-color: #5D7B9D; font-weight: bold; color: #FFFFFF;" colspan="4">
                                        Listado de Comites</td>
                                </tr>
                                <tr>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                    <td style="width:79%;">
                                        &nbsp;</td>
                                    <td style="width:1%;">
                                        &nbsp;</td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:10%;">
                                        Centro</td>
                                    <td style="width:79%;">
                                        <asp:DropDownList ID="ddlListaCentroCosto" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:1%;">
                                        &nbsp;</td>
                                    <td style="width:10%;">
                                        
                                        <asp:Button ID="ibtnBuscar" runat="server" CssClass="buscar1" Height="35px" 
                                            Text="Buscar" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                    <td style="width:79%;">
                                        </td>
                                    <td style="width:1%;">
                                        </td>
                                    <td style="width:10%;">
                                        
                                            &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                    <td style="width:79%;">
                                        &nbsp;</td>
                                    <td style="width:1%;">
                                        &nbsp;</td>
                                    <td style="width:10%;">
                                        <asp:Button ID="ibtnNuevoComite" runat="server" CssClass="nuevo1" Height="35px" 
                                            Text="Nuevo" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                <asp:Panel ID="pnlResultado" runat="server">
                                <table style="width:100%">
                                                                        <tr>
                                    <td colspan="3" style="width:90%;">
                                        Lista de Comité</td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width:100%;">
                                        <asp:GridView ID="gvListaComite" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                            GridLines="None" PageSize="5" Width="100%">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="nombre" HeaderText="Comité" />
                                                <asp:BoundField DataField="descripcion_cco" HeaderText="Centro de Costos" />
                                                <asp:BoundField DataField="nombreinstancia" HeaderText="Instancia" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hfCodigo" runat="server" value='<%# Bind("id") %>' />
                                                        
                                                            <asp:LinkButton ID="lbModificar" runat="server"  Font-Underline="True"                                                      
                                                            onclick="ibtnModificar_Click">Modificar</asp:LinkButton>
                                                    
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                </asp:TemplateField>
                                                <asp:CommandField SelectText="" ShowSelectButton="True" />
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                    <td style="width:79%;">
                                        &nbsp;</td>
                                    <td style="width:1%;">
                                        &nbsp;</td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width:100%;">
                                        Lista de Miembros</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width:100%;">
                                        <asp:GridView ID="gvComiteMiembros" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                            GridLines="None" PageSize="5" Width="100%">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                                                <asp:TemplateField HeaderText="Coordinador">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" 
                                                            Checked='<%# Bind("coordinador") %>' Enabled="False" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                        </table>
                                        </asp:Panel>
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
                        <asp:UpdatePanel ID="upModificacion" runat="server">
                        <ContentTemplate>
                        <asp:Panel ID="pnlModificacion" runat="server" Visible="False">
                            <table style="width:100%; height: 100%;">
                                <tr>
                                    <td style="width:100%; background-color: #5D7B9D;" colspan="3">
                                        <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        &nbsp;</td>
                                    <td style="width:75%;">
                                        &nbsp;</td>
                                    <td align="center" style="width:10%;">
                                   
                                        <asp:Button ID="ibtnRegresar" runat="server" CssClass="cerrar_prp" 
                                            Height="35px" Text="  Regresar" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        &nbsp;</td>
                                    <td style="width:75%;">
                                        &nbsp;</td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        Centro</td>
                                    <td style="width:75%;">
                                        <asp:DropDownList ID="ddlACentroCosto" runat="server" Width="92%">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="cvCentro" runat="server" 
                                            ControlToValidate="ddlACentroCosto" 
                                            ErrorMessage="Debe de seleccionar un Centro de Costos" Operator="GreaterThan" 
                                            ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td style="width:10%;" align="center" rowspan="3" valign="top">
                                        
                                        <asp:Button ID="ibtnGrabar" runat="server" CssClass="guardar_prp" Height="35px" 
                                            Text="      Guardar" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        Instancia</td>
                                    <td style="width:75%;">
                                        <asp:DropDownList ID="ddlAInstancia" runat="server" Width="92%">
                                        </asp:DropDownList>
                                        <asp:CompareValidator ID="cvInstancia" runat="server" 
                                            ControlToValidate="ddlAInstancia" Operator="GreaterThan" 
                                            ErrorMessage="Debe de seleccionar una Instancia" ValidationGroup="Guardar" 
                                            ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        Comité</td>
                                    <td style="width:75%;">
                                        <asp:TextBox ID="txtAComite" runat="server" Width="92%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComite" runat="server" 
                                            ControlToValidate="txtAComite"
                                            ErrorMessage="Debe de ingresar el nombre del Comité" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        Estado</td>
                                    <td style="width:75%;">
                                        <asp:CheckBox ID="chkEstado" runat="server" Checked="True" Enabled="False" />
                                    </td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        <asp:HiddenField ID="hfSel" runat="server" />
                                    </td>
                                    <td style="width:75%;">
                                        <asp:HiddenField ID="hfid" runat="server" />
                                    </td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:100%; background-color: #5D7B9D; font-weight: bold; color: #FFFFFF;" 
                                        colspan="3">
                                        Asignar Miembros</td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        &nbsp;</td>
                                    <td style="width:75%;">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="#FF3300"></asp:Label>
                                    </td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        Personal</td>
                                    <td style="width:75%;">
                                        <asp:DropDownList ID="ddlAPersonal" runat="server" Width="92%">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:10%;" align="center">
                                            <asp:Button ID="ibtnAgregar" runat="server" CssClass="addmiembros" 
                                            Height="35px" Text="  Agregar" Width="100px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                        &nbsp;</td>
                                    <td style="width:75%;">
                                        &nbsp;</td>
                                    <td style="width:10%;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3"  style="width:100%;">
                                        <asp:Panel ID="pnlMiembros" runat="server" Height="310px" ScrollBars="Vertical" 
                                            Width="100%">
                                        <asp:GridView ID="gvMiembros" runat="server" CellPadding="4" ForeColor="#333333" 
                                            GridLines="None" Width="100%" AutoGenerateColumns="False">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Coordinador">
                                                    <ItemTemplate>
                                                        <asp:Literal ID="rbtnMarkup" runat="server"></asp:Literal>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="personal_id" HeaderText="Código" />
                                                <asp:BoundField DataField="nombre" HeaderText="Miembro" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ibtnEliminar" runat="server" 
                                                            ImageUrl="~/Images/edit_remove.png" onclick="ibtnEliminar_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="1%" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                            
                                            <asp:ValidationSummary ID="vsMensajes" runat="server" ShowMessageBox="True" 
                                                ShowSummary="False" ValidationGroup="Guardar" />
                                            
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="ibtnGrabar" />
                            </Triggers>
                        </asp:UpdatePanel>   
                    </td>
                </tr>
            </table>
 
    </div>
    </form>
</body>
</html>
