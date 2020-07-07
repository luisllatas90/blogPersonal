<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EliminarPropuestaPOA_v1.aspx.vb"
    Inherits="administrativo_propuestas2_proponente_EliminarPropuestaPOA_v1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../estilo.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../funciones.js"> </script>

    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            width: 54px;
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
            LISTADO DE PROPUESTAS<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
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
                                    <td style="width: 15%;">
                                        Ejercicio Presupuestal:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEjercicio" runat="server" Height="21px" Width="140">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 5%;">
                                        Estado:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstado" runat="server" Height="21px" Width="114px">
                                            <asp:ListItem>ACTIVO</asp:ListItem>
                                            <asp:ListItem>NO ACTIVO</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
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
                        <asp:BoundField DataField="codigo_Prp" HeaderText="ID">
                            <ItemStyle Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_prp" HeaderText="Propuesta" />
                        <asp:BoundField DataField="tipoPropuesta" HeaderText="Tipo de Propuesta">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechainicio_Ipr" HeaderText="Fecha Inicio">
                            <ItemStyle Width="80px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Eliminar" ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                    ImageUrl="../../../Images/menus/noconforme_small.gif" OnClientClick="return confirm('¿Desea Eliminar Registro?.')"
                                    Text="Eliminar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
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
