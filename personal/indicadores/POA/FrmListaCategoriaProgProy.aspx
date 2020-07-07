<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaCategoriaProgProy.aspx.vb"
    Inherits="indicadores_POA_FrmListaCategoriaProgProy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            width: 111px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="foco" runat="server" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Categoría de Programa/Proyectos"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                
                <td class="style1">
                    &nbsp; Vigencia: &nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlVigencia" runat="server" Width="150px">
                        <asp:ListItem Value="1">Vigente</asp:ListItem>
                        <asp:ListItem Value="0">No Vigente</asp:ListItem>
                        <asp:ListItem Value="2">Todos</asp:ListItem>
                    </asp:DropDownList>
                    <span lang="es-pe">&nbsp;&nbsp; </span>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr style="height: 1px;">
                <td colspan="7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Button ID="btnNuevo" runat="server" Text="    Nueva Categoría" CssClass="btnNuevo" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgv_Categoria" runat="server" Width="100%" AutoGenerateColumns="False"
            DataKeyNames="codigo_cap" CellPadding="3">
            <Columns>
                <asp:BoundField HeaderText="DESCRIPCIÓN" DataField="nombre_cap">
                    <HeaderStyle Width="550px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="estado_cap" DataField="estado_cap" Visible="False">
                    <HeaderStyle Width="100px" />
                </asp:BoundField>
                <asp:CommandField ShowSelectButton="true" ButtonType="Image" HeaderText="EDITAR"
                    SelectImageUrl="../../../images/editar_poa.png" UpdateImageUrl="../../images/editar.gif"
                    SelectText="Editar">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            ImageUrl="../../Images/menus/noconforme_small.gif" AlternateText="Eliminar" OnClientClick="return confirm('¿Desea Eliminar Registro?.')" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No se Encontraron Registros
            </EmptyDataTemplate>
            <EmptyDataRowStyle BackColor="Black" />
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
        </asp:GridView>
        <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
        <table width="95%">
            <tr>
                <td runat="server" id="aviso_contador">
                    <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
