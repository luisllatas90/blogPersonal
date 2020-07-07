<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_test.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>

    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hdcodigo_poa" runat="server" Value="0" />
    <div style="font-family: 'Helvetica Neue',Helvetica,Arial,sans-serif; font-size: 13px;
        color: #337ab7; padding-bottom: 10px; font-weight: bold">
        <asp:Label ID="Label3" runat="server" Text="Planes Operativos"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <asp:SqlDataSource ID="SQLCargaPoas" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                        SelectCommand="POA_ListaPoaActividad" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlPlan" PropertyName="SelectedValue" Name="codigo_pla"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="ddlEjercicio" PropertyName="SelectedValue" Name="ejercicio"
                                Type="Int32" />
                            <asp:QueryStringParameter QueryStringField="id" Name="codigo_per" Type="String" />
                            <asp:QueryStringParameter QueryStringField="ctf" Name="codigo_tfu" Type="String" />
                            <asp:ControlParameter ControlID="ddlEstado" Name="opcion" PropertyName="SelectedValue"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td width="100px">
                    Plan Estratégico
                </td>
                <td width="510px">
                    <asp:DropDownList ID="ddlPlan" runat="server" Width="500" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    Ejercicio Presupuestal
                </td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    Estado
                </td>
                <td>
                    <asp:DropDownList ID="ddlestado" runat="server" AutoPostBack="true">
                        <asp:ListItem Value="T">Todos</asp:ListItem>
                        <asp:ListItem Value="P">Pendientes</asp:ListItem>
                        <asp:ListItem Value="A">Asignados</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgvpoa" runat="server" Width="100%" DataSourceID="SQLCargaPoas"
            AutoGenerateColumns="False" DataKeyNames="codigo_poa,responsable_poa" HeaderStyle-Height="20px"
            CellPadding="4">
            <Columns>
                <asp:BoundField HeaderText="PLAN OPERATIVO ANUAL" DataField="nombre_poa">
                    <HeaderStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable">
                    <HeaderStyle Width="25%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp">
                    <HeaderStyle Width="5%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="INGRESOS" DataField="limite_ingreso" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EGRESOS" DataField="limite_egreso" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="UTILIDAD" DataField="utilidad" DataFormatString="{0:N}">
                    <HeaderStyle Width="9%" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="ASIGNAR" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                            ImageUrl="../../images/previo.gif" Text="ASIGNAR" />
                    </ItemTemplate>
                    <HeaderStyle Width="3%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="codigo_poa" HeaderText="codigo_poa" Visible="False" />
            </Columns>
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <EmptyDataTemplate>
                No se Encontraron Registros</EmptyDataTemplate>
        </asp:GridView>
        <div class="titulo_poa">
            <asp:Label ID="Label1" runat="server" Text="Programas Y Proyectos"></asp:Label>
        </div>
        <table width="100%">
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlActividades" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                        SelectCommand="POA_ListaActividadesxPoa" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hdcodigo_poa" PropertyName="Value" Name="codigo_poa"
                                Type="Int32" />
                            <asp:Parameter DefaultValue="0" Name="codigo_tac" Type="Int32" />
                            <asp:QueryStringParameter QueryStringField="id" Name="codigo_per" Type="String" />
                            <asp:QueryStringParameter QueryStringField="ctf" Name="ctf" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="dgvActividades" runat="server" AutoGenerateColumns="False" DataSourceID="SqlActividades"
                        DataKeyNames="codigo_acp" HeaderStyle-Height="20px" CellPadding="4">
                        <Columns>
                            <asp:BoundField HeaderText="CODIGO IEP" DataField="codigo_iep" Visible="false" />
                            <asp:BoundField HeaderText="ACTIVIDAD" DataField="resumen_acp">
                                <HeaderStyle Width="45%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="TIPO ACTIVIDAD" DataField="descripcion_tac" ControlStyle-Width="40px" />
                            <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable">
                                <HeaderStyle Width="33%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="INGRESOS(S/.)" DataField="ingresos_acp" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="EGRESOS(S/.)" DataField="egresos_acp" DataFormatString="{0:N}">
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectText="Editar"
                                SelectImageUrl="../../../images/editar_poa.png" HeaderText="EDITAR">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="ENVIAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" AlternateText="Enviar"
                                        CommandName="Edit" ImageUrl="../../images/inv_paso.png" Text="Editar" OnClientClick="return confirm('¿Esta Seguro que Desea Enviar Actividad?.')" />
                                </ItemTemplate>
                                <ControlStyle Height="17px" Width="17px" />
                                <HeaderStyle Width="3%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" AlternateText="Eliminar"
                                        CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" Text="Eliminar"
                                        OnClientClick="return confirm('¿Esta Seguro que Desea Eliminar Actividad?.')" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
