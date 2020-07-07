<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaItemActividad.aspx.vb"
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
        <asp:Label ID="Label1" runat="server" Text="Asignación: Actividades - Items"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td>
                    Actividades:
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlActividades" runat="server" Width="500px">
                    </asp:DropDownList>
                    <span lang="es-pe"></span>
                </td>
            </tr>
            <tr>
                <td>
                    <span lang="es-pe">Tipo de Items: </span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipo" runat="server" Width="500px">
                        <asp:ListItem Value="%">&lt;&lt;TODOS&gt;&gt;</asp:ListItem>
                        <asp:ListItem Value="A">ARTÍCULOS</asp:ListItem>
                        <asp:ListItem Value="R">RUBROS</asp:ListItem>
                        <asp:ListItem Value="P">PLANILLA</asp:ListItem>
                    </asp:DropDownList>
                    <span lang="es-pe"></span>
                </td>
                <td>
                    Movimiento:
                </td>
                <td>
                    <asp:DropDownList ID="ddlMovimiento" runat="server" Width="100px">
                        <asp:ListItem Value="%">&lt;&lt;TODOS&gt;&gt;</asp:ListItem>
                        <asp:ListItem Value="I">INGRESO</asp:ListItem>
                        <asp:ListItem Value="E">EGRESO</asp:ListItem>
                    </asp:DropDownList>
                    <span lang="es-pe"></span>
                </td>
            </tr>
            <tr>
                <td>
                    Descripción de Items:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txt_texto" runat="server" Width="500px"></asp:TextBox>
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
                        DataKeyNames="codigo_cip,codigo_cat,codigoitem,codigocon, tipo, id" CellPadding="3">
                        <Columns>
                            <asp:BoundField HeaderText="CÓDIGO" DataField="codigoitem">
                                <HeaderStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ITEMS" DataField="concepto">
                                <HeaderStyle Width="650px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="UND" DataField="unidad">
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="PRECIO">
                                <ItemTemplate>
                                    <asp:TextBox Style="text-align: right" ID="txtprecio" class="caja_poa" runat="server"
                                        Text='<%# Bind("precio_cip") %>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle CssClass="celda_combinada" Width="40px" Height="20px" HorizontalAlign="Right" />
                            </asp:TemplateField>
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
                    <asp:Button ID="btnClonar" runat="server" Text="    Clonar ACTIVIDAD" CssClass="btnClonarPOA" />
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
                                    CLONAR <span lang="es-pe">ACTIVIDADES - ITEMS</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold">
                                    Actividades:
                                </td>
                                <td>
                                    <asp:Label ID="lblPOA" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold">
                                    Actividades a Clonar:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlActividadesClonar" runat="server" Width="500">
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
