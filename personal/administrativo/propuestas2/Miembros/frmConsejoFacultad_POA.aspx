<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsejoFacultad_POA.aspx.vb"
    Inherits="administrativo_propuestas2_Miembros_frmConsejoFacultad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
    <title></title>
    <%--<link href="../estilo.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .btn
        {
            border: 1px solid #5D7B9D;
            background: #F7F6F3;
            font-family: Tahoma;
            font-size: 8pt;
            font-weight: bold;
            padding: 3px;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>
            MIEMBROS DEL CONSEJO DE FACULTAD<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </h4>
    </div>
    <table style="width: 100%; height: 100%">
        <tr>
            <td class="contornotabla" valign="top" width="100%" colspan="6">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td bgcolor="#F0F0F0" width="20%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 490px;">
                                        <asp:Button ID="Button2" runat="server" Text="            Consultar" CssClass="enviarpropuesta"
                                            Height="35px" Width="90px" Visible="False" />
                                        <asp:Button ID="Button3" runat="server" CssClass="nuevo1" Height="35px" Text="        Nuevo"
                                            Width="76px" />
                                        <asp:Button ID="Button4" runat="server" Text="            Registrar" CssClass="guardar_prp"
                                            Height="35px" Width="90px" Enabled="false" />
                                        <asp:Button ID="Button5" runat="server" Text="Cancelar" CssClass="salir_prp" Height="35px"
                                            Width="112px" Enabled="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="contornotabla">
                <asp:Panel ID="PanelConsulta" runat="server">
                    <table cellpadding="3">
                        <tr>
                            <td>
                                <b>Consejo</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFacultad" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="contornotabla">
                <asp:Panel ID="PanelRegistro" runat="server" Visible="false">
                    <table cellpadding="3">
                        <tr>
                            <td colspan="2">
                            </td>
                            <%--<td>
                            </td>
                            <td>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <b>Personal Responsable </b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPersonal" runat="server" Width="500px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Cargo</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCargo" runat="server" Height="19px" Width="248px">
                                    <asp:ListItem Selected="True" Value="F">Miembro del Consejo de Facultad</asp:ListItem>
                                    <asp:ListItem Value="T">Secretario de Facultad</asp:ListItem>
                                    <asp:ListItem Enabled="False" Value="G">Consejo de Postgrado</asp:ListItem>
                                    <asp:ListItem Value="C">Coordinador Educacion Continua</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Cargo de Personal</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCargoPersonal" runat="server" Height="19px" Width="500px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="contornotabla">
                <asp:Panel ID="PanelActualizar" runat="server" Visible="false">
                    <table cellpadding="3">
                        <tr>
                            <td>
                                <b>Personal Responsable Actual</b>
                            </td>
                            <td>
                                <asp:Label ID="lblPersonalActual" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Personal Responsable Nuevo</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPersonalNuevo" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Cargo</b>
                            </td>
                            <td>
                                <asp:Label ID="lblCargoActual" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="contornotabla">
                <asp:Panel ID="PanelLista" runat="server">
                    <div style="padding: 5px;">
                        <asp:GridView ID="dgvLista" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_Cjf,responsable_Cco,Cargo_Cjf,codigo_pcc"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="2" ForeColor="Black" GridLines="Both">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundField DataField="codigo_Cjf" HeaderText="codigo_Cjf" Visible="False" />
                                <asp:BoundField DataField="codigo_pcc" HeaderText="Cód. Resp." Visible="true" />
                                <asp:BoundField DataField="responsable_Cco" HeaderText="Personal Responsable" />
                                <asp:BoundField DataField="Cargo_Cjf" HeaderText="Cargo" />
                                <asp:BoundField DataField="Cargo_personal" HeaderText="Cargo Personal" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CommandName="Actualizar" CssClass="btn" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Button" ControlStyle-CssClass="btn" DeleteText="Inactivar"
                                    ShowDeleteButton="True">
                                    <ControlStyle CssClass="btn" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdPerActual" runat="server" />
    <asp:HiddenField ID="hdcodigo_Cjf" runat="server" />
    </form>
</body>
</html>
