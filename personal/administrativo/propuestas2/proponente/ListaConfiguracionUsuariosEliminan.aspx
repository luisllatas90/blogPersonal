<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaConfiguracionUsuariosEliminan.aspx.vb"
    Inherits="administrativo_propuestas2_proponente_ListaConfiguracionUsuariosEliminan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../estilo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../funciones.js"> </script>

    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <style type="text/css">
        .style2
        {
            width: 15%;
            height: 22px;
        }
        .style3
        {
            height: 22px;
        }
    </style>

    <script type="text/javascript">
        function HabilitarBoton(tipo, fila) {
            document.form1.txtelegido.value = fila.id
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h4>
            CONFIGURACION DE USUARIOS CON PERMISOS A ELIMINAR
        </h4>
    </div>
    <table style="width: 100%; margin-bottom: 229px;" align="left">
        <tr>
            <td class="contornotabla" valign="top" width="100%" colspan="6">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td bgcolor="#F0F0F0" class="bordeinf" width="20%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 10%;">
                                        Personal Responsable:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Personal" runat="server" Height="21px" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%;">
                                        Permiso de Eliminar:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddl_Eliminar" runat="server" Height="21px" Width="15%">
                                            <asp:ListItem Value="0">-- SELECCIONE -- </asp:ListItem>
                                            <asp:ListItem Value="1">SI</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        Recibe E-Mail:
                                    </td>
                                    <td class="style3">
                                        <asp:DropDownList ID="ddl_mail" runat="server" Height="21px" Width="15%">
                                            <asp:ListItem Value="0">-- SELECCIONE -- </asp:ListItem>
                                            <asp:ListItem Value="1">SI</asp:ListItem>
                                            <asp:ListItem Value="2">NO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Estado:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstado" runat="server" Height="21px" Width="15%">
                                            <asp:ListItem Value="1">ACTIVO</asp:ListItem>
                                            <asp:ListItem Value="0">NO ACTIVO</asp:ListItem>
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chk_TodosMails" runat="server" 
                                            Text="Se enviarán todos los mails eliminados" TextAlign="Left" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Button ID="cmdRegistrar" runat="server" CssClass="guardar_prp" Height="47px"
                                            Text="        Registrar" Width="120px" />
                                        <asp:Button ID="cmdConsultar" runat="server" CssClass="nuevo1" Height="47px" Text="        Consultar"
                                            Width="120px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="dgvPropuestas" runat="server" Width="100%" AutoGenerateColumns="False"
                    PageSize="15" CellPadding="2" ForeColor="#333333" Font-Size="Smaller">
                    <RowStyle Height="20px" />
                    <Columns>
                        <asp:BoundField DataField="codigo_elp" HeaderText="ID">
                            <ItemStyle Width="5%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="responsable_per" HeaderText="Responsable">
                            <ItemStyle Width="55%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="elimina_elp" HeaderText="Elimima PRP">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="enviaMail_elp" HeaderText="E-Mail">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="todosMails" HeaderText="Todos Mail">
                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="../../../Images/menus/noconforme_small.gif" OnClientClick="return confirm('¿Desea Eliminar Registro?.')"
                                    Text="ELIMINAR" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#666666" ForeColor="White" Height="22px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td bgcolor="#F0F0F0" class="contornotabla" colspan="6">
                <asp:Panel ID="Panel1" runat="server" Height="100%">
                    <asp:HiddenField ID="txtelegido" runat="server" />
                    <asp:Label ID="lbl_numeroItems" runat="server" Font-Bold="True"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
