<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConfigurarAccesosQuintaEspecial.aspx.vb" Inherits="personal_administrativo_SISREQ_PlanillaQuinta_ConfigurarAccesosQuintaEspecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">

a:Link {
	color: #000000;
	text-decoration: none;
}

        .style2
        {
            font-weight: normal;
        }
    </style>
</head>
<body style="top-margin:0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;" cellpadding="0" cellspacing="0" 
                class="ContornoTabla">
                <tr>
                    <td>
                        <table style="width:100%;">
                            <tr>
                                <td style="font-weight: normal">
                                    Centro de costos
                                    <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Ingrese centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" 
                                ValueToCompare="0">*</asp:CompareValidator>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;<asp:DropDownList ID="cboCecos" runat="server">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos" 
                                            Width="200px"></asp:TextBox>
                                    <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                            ImageUrl="../../images/busca.gif" 
                                ValidationGroup="BuscarCecos" />
                                    <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="font-weight: normal">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" 
                                ForeColor="Blue">Busqueda 
                                        Avanzada</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: normal">
                                    <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                            Width="100%">
                                        <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" DataKeyNames="codigo_cco" ForeColor="#333333" GridLines="None" 
                                                ShowHeader="False" Width="98%">
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                                <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
                                                <asp:CommandField ShowSelectButton="True">
                                                    <ItemStyle ForeColor="Red" />
                                                </asp:CommandField>
                                            </Columns>
                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <b>No se encontraron items con el término de búsqueda</b>
                                            </EmptyDataTemplate>
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: normal">
                                    Responsable de registro&nbsp;<asp:CompareValidator ID="CompareValidator5" 
                                        runat="server" ControlToValidate="cboRegistro" 
                                        ErrorMessage="Ingrese responsable de registro" Operator="GreaterThan" 
                                        ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    &nbsp;&nbsp;&nbsp; :
                                    <asp:DropDownList ID="cboRegistro" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: normal">
                                    Responsable de aprobación
                                    <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                        ControlToValidate="cboAprobacion" 
                                        ErrorMessage="Ingrese responsable de aprobación" Operator="GreaterThan" 
                                        ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    :
                                    <asp:DropDownList ID="cboAprobacion" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: normal">
                                    <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" />
                                    &nbsp;<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                        ValidationGroup="Guardar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="ContornoTabla" style="width:100%;">
                            <tr>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                        AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <span style="color: #0000FF"><span class="style2">Espere un momento... se esta 
                                            procesando su solicitud
                                            <img alt="" src="../../../images/loading.gif" /></span> </span>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvCecosAsignados" runat="server" AutoGenerateColumns="False" 
                                        BorderWidth="1px" DataKeyNames="ID" Font-Bold="False" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="CENTRO_COSTOS" HeaderText="Centro de Costos">
                                                <ItemStyle HorizontalAlign="Justify" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RESPONSABLE_REGISTRO" 
                                                HeaderText="Responsable de Registro" />
                                            <asp:BoundField DataField="RESPONSABLE_APROBACION" 
                                                HeaderText="Responsable de aprobación" />
                                            <asp:CommandField ButtonType="Image" 
                                                DeleteImageUrl="../../../images/eliminar.gif" 
                                                HeaderText="Eliminar" ShowDeleteButton="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>
                                        </Columns>
                                        <HeaderStyle BackColor="#FF9900" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
