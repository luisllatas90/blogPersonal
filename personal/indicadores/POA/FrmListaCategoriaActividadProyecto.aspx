﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaCategoriaActividadProyecto.aspx.vb"
    Inherits="indicadores_POA_FrmListaCategoriaActividadProyecto" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="foco" runat="server" />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Registro de Categoría - Actividad"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td>
                    Categoría:
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoriasProgramaProyecto" runat="server" Width="350px">
                    </asp:DropDownList>
                    <span lang="es-pe">&nbsp;&nbsp; </span>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Consultar" CssClass="btnBuscar" />
                </td>
            </tr>
            <tr style="height: 1px;">
                <td colspan="7">
                    &nbsp;
                    <asp:GridView ID="dgv_Categoria" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="codigo_cpa,codigo_cap,codigo_cat" CellPadding="3">
                        <Columns>
                            <asp:BoundField HeaderText="ACTIVIDAD" DataField="actividad">
                                <HeaderStyle Width="650px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ASIGNAR" ControlStyle-Width="10px">
                                <ControlStyle Width="10px"></ControlStyle>
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
                <td colspan="7">
                    <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="7" class="style1">
                    <asp:Button ID="btnNuevo" runat="server" Text="    Guardar" CssClass="btnNuevo" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <div runat="server" id="aviso">
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
