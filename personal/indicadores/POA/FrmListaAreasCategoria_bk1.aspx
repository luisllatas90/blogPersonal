<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAreasCategoria_bk1.aspx.vb"
    Inherits="indicadores_POA_FrmListaAreasCategoria" %>

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
        <asp:Label ID="Label1" runat="server" Text="Asignación: ÁREAS POA - Categorías"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td>
                    Plan Estratégico:
                </td>
                <td>
                    <asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true">
                    </asp:DropDownList>
                    <span lang="es-pe">&nbsp;&nbsp; </span>
                </td>
                <td width="140px">
                    Ejercicio Presupuestal
                </td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Área:
                </td>
                <td>
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>--%>
                    <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
                        <asp:ListItem Value="0"> << TODOS >></asp:ListItem>
                    </asp:DropDownList>
                    <%-- </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlplan" EventName="SelectedIndexchanged" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                </td>
                <td>
                    Categoría:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoriasProgramaProyecto" runat="server" Width="250px">
                    </asp:DropDownList>
                    <span lang="es-pe">&nbsp;&nbsp; </span>
                    <%--<asp:Button ID="btnBuscar" runat="server" Text="   Consultar" CssClass="btnBuscar" />--%>
                </td>
            </tr>
            <%-- <tr>
                    <td>
                        Categoría:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategoriasProgramaProyecto" runat="server" Width="350px">
                        </asp:DropDownList>
                        <span lang="es-pe">&nbsp;&nbsp; </span>
                        <asp:Button ID="btnBuscar" runat="server" Text="   Consultar" CssClass="btnBuscar" />
                    </td>
                </tr>--%>
            <tr style="height: 1px;">
                <td colspan="7">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <asp:Button ID="btnBuscar" runat="server" Text="   Consultar" CssClass="btnBuscar" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnNuevo" runat="server" Text="    Agregar" CssClass="btnNuevo" />
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
            DataKeyNames="codigo_aca,codigo_poa,codigo_cap" CellPadding="3">
            <Columns>
                <asp:BoundField HeaderText="ÁREA" DataField="area">
                    <HeaderStyle Width="550px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="CATEGORÍA" DataField="categoria">
                    <HeaderStyle Width="550px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="ESTADO" DataField="estado_aca">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
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
