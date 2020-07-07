<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAreasCategoria_DS.aspx.vb"
    Inherits="indicadores_POA_FrmListaAreasCategoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
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
                <td width="100px">
                    Ejercicio Presupuestal: <span lang="es-pe">
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
                <td colspan="3">
                </td>
            </tr>
            <tr style="height: 1px;">
                <td colspan="5">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Button ID="btnBuscar" runat="server" Text="   Consultar" CssClass="btnBuscar" />
                    &nbsp; &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" width="50%" valign="top">
                    <asp:GridView ID="dgv_Categoria" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="codigo_aca,codigo_poa,codigo_cap" CellPadding="3">
                        <Columns>
                            <asp:BoundField HeaderText="CATEGORÍA" DataField="categoria">
                                <HeaderStyle Width="650px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ESTADO" ControlStyle-Width="5px">
                                <HeaderStyle Width="10px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No se Encontraron Registros
                        </EmptyDataTemplate>
                        <EmptyDataRowStyle BackColor="Black" />
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                    </asp:GridView>
                </td>
                <td colspan="2" width="50%" valign="top">
                    <asp:GridView ID="dgv_poa" runat="server" Width="100%" AutoGenerateColumns="False"
                        DataKeyNames="codigo_aca,codigo_poa,codigo_cap" CellPadding="3">
                        <Columns>
                            <asp:BoundField HeaderText="POA" DataField="categoria">
                                <HeaderStyle Width="650px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="ESTADO" ControlStyle-Width="5px">
                                <HeaderStyle Width="10px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccion" runat="server" />
                                </ItemTemplate>
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
                <td colspan="5">
                    <asp:Button ID="btnAgregar" runat="server" Text="    Agregar" 
                        CssClass="btnNuevo" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5" runat="server" id="aviso_contador">
                    <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <br />
    </div>
    </form>
</body>
</html>
