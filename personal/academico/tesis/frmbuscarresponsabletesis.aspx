<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmbuscarresponsabletesis.aspx.vb" Inherits="frmbuscarresponsabletesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar autores de la tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="3" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblTitulo" runat="server" CssClass="usatTitulo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            <table width="80%" cellpadding="3" cellspacing="0" 
                    style="border: 1px solid #507CD1; background-color: #eff3fb;">
             <tr>
                <td style=" width:25%">Ingresar apellidos o nombres</td>
                <td style=" width:50%"><asp:TextBox ID="txtTermino" runat="server" 
                        CssClass="cajas2" MaxLength="50"></asp:TextBox></td>
                <td style=" width:25%"><asp:Button ID="cmdBuscar" runat="server" Text="    Buscar" CssClass="buscar_prp_small" />
            <asp:Button ID="CmdCancelar" runat="server" CssClass="regresar2" 
                    Text="     Cancelar" Height="25px" BackColor="#EFF3FB" 
                        BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="1px" Width="65px" />
                            </td>
            </tr>
            </table>
            </td>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataKeyNames="codigo_alu" ForeColor="#333333" 
                    Width="100%" Visible="False" BorderColor="#628BD7" BorderStyle="Solid" 
                    BorderWidth="1px">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="alumno" HeaderText="Estudiante">
                            <ItemStyle Width="45%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ingreso">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:ButtonField ButtonType="Button" CommandName="codigo_alu" 
                            ImageUrl="~/images/anadir.gif" Text="Añadir ahora">
                            <ControlStyle CssClass="agregar2" Width="100px" />
                            <ItemStyle Width="5%" />
                        </asp:ButtonField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <asp:DataList ID="DataList1" runat="server" RepeatColumns="5" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_per" Visible="False">
                    <ItemTemplate>
                        <table width="100%" onmouseover="Resaltar(1,this)" onmouseout="Resaltar(0,this)" class="contornotabla_azul">
                            <tr>
                                <td align="center">
                                    <asp:Image ID="Image1" runat="server"  
                                        ImageUrl='<%# eval("foto_per") %>' Height="69px" Width="63px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblapellidos" runat="server" Text='<%# eval("apellidos") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblnombres" runat="server" Text='<%# eval("nombres_per") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" Text='<%# eval("descripcion_cco") %>' 
                                        Font-Size="8px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="cmdElegir" runat="server" CssClass="agregar4" 
                                        Text="      Elegir ahora" Width="80px" BorderColor="#999999" 
                                        BorderStyle="Solid" BorderWidth="1px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
        </table>
    <p style="text-align:center"><asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="12px" 
        ForeColor="Red"></asp:Label></p>
    </form>
</body>
</html>
