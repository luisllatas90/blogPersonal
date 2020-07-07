<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAreasCategoria.aspx.vb"
    Inherits="indicadores_POA_FrmListaAreasCategoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .style2
        {
            text-align: left;
        }
        .style3
        {
            border: 1px solid #bfac4c;
            background: #eee9cf url('../../../Images/buscar_poa.png') no-repeat 0% center;
            color: #685d25;
            font-weight: bold;
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
                    Ejercicio Presupuestal:
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
                    <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
                        <asp:ListItem Value="0"> << TODOS >></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 1px;">
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnBuscar" runat="server" Text="   Consultar" CssClass="style3" />
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="dgv_Categoria" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="codigo_aca,codigo_poa,codigo_cap" CellPadding="3">
                        <Columns>
                            <asp:BoundField HeaderText="CATEGORIA" DataField="categoria">
                                <HeaderStyle Width="650px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ESTADO" ControlStyle-Width="5px">
                                <ControlStyle Width="5px"></ControlStyle>
                                <HeaderStyle Width="10px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No se Encontraron Registros
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle BackColor="Black" />
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="style2">
                    <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="style1">
                    <asp:Button ID="btnNuevo" runat="server" Text="    Agregar" CssClass="btnNuevo" />
                    <span lang="es-pe">&nbsp;&nbsp; </span>
                    <asp:Button ID="btnClonar" runat="server" Text="    Clonar POA" CssClass="btnClonarPOA" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <span lang="es-pe">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <span lang="es-pe">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div id="ClonarPOA" runat="server">
                        <table id="TablaClonarPoa" runat="server" style="border: 1px solid #FF0000; margin: 0 auto;"
                            visible="false" width="60%">
                            <tr>
                                <td colspan="2" style="text-align: center; font-weight: bold;">
                                    CLONAR CATEGORÍAS POA
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold">
                                    Área POA:
                                </td>
                                <td>
                                    <asp:Label ID="lblPOA" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold">
                                    POA:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPoaClonar" runat="server" Width="500">
                                        <asp:ListItem Value="0"> << TODOS >></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span lang="es-pe">&nbsp;</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button ID="btnRegistrarClon" runat="server" Text="    Registar" CssClass="btnNuevo" />
                                    <span lang="es-pe">&nbsp;&nbsp; </span>
                                    <asp:Button ID="btnCancelarClon" runat="server" Text="    Cancelar" CssClass="btnCancelarClonar" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <span lang="es-pe">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span lang="es-pe">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span lang="es-pe">&nbsp;</span>
                </td>
            </tr>
            <tr>
                <td colspan="4" runat="server" id="aviso_contador">
                    <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <br />
        </table>
    </div>
    </form>
</body>
</html>
