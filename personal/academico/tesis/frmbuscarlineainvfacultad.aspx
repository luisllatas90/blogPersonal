<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmbuscarlineainvfacultad.aspx.vb" Inherits="frmbuscarresponsabletesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <table width="90%" cellpadding="4" cellspacing="0">
        <tr>
            <td style=" width:20%">
                Seleccione la
                Facultad</td>
            <td style=" width:70%">
                <asp:DropDownList ID="dpFacultad" runat="server" CssClass="cajas2" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="listadiv" style=" width:100%; height:500px" class="contornotabla">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="3" DataKeyNames="codigo_are" ForeColor="#333333" GridLines="Horizontal" 
                    Width="100%">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <img alt="" src="../../../images/menus/abrir.gif" style="height: 28px; width:28px" />
                            </ItemTemplate>
                            <ItemStyle Width="3%" />
                        </asp:TemplateField>
<asp:TemplateField HeaderText="LINEA DE INVESTIGACIÓN">
    <ItemTemplate>
        <asp:Label ID="lblnombre_are" runat="server" ForeColor="Maroon" 
            Text='<%# UCASE(eval("nombre_are")) %>'></asp:Label>
        <br />
        <asp:Label ID="lblproposito_are" runat="server" Font-Italic="True" 
            ForeColor="#000066" Text='<%# eval("proposito_are") %>'></asp:Label>
    </ItemTemplate>

<ItemStyle Width="62%"></ItemStyle>
</asp:TemplateField>
                        <asp:BoundField DataField="nombre_dac" HeaderText="DEPARTAMENTO ACADÉMICO">
                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="MARCAR">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <asp:Label ID="Label1" runat="server" CssClass="rojo" 
                            
                            Text="No se encontraron lineas de investigación registradas en la Facultad seleccionada" 
                            Font-Size="11px"></asp:Label>
                    </EmptyDataTemplate>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
            <asp:Button ID="cmdRegresar" runat="server" Text="Regresar" 
                    UseSubmitBehavior="False" />
&nbsp;<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Visible="False" />
            </td>
        </tr>
        </table>
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="12px" 
        ForeColor="Red"></asp:Label>
        </center>
    </form>
</body>
</html>
