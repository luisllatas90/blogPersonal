<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModificarPresupuesto_V3-HC.aspx.vb"
    Inherits="presupuesto_areas_ModificarPresupuesto_V3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estiloWeb_V2.css" rel="stylesheet" type="text/css" />

    <script src="../../private/funciones.js" type="text/javascript" language="javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table class="pagina" cellpadding="8" cellspacing="0">
        <tr>
            <td colspan="2" class="tituloPagina">
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right" width="20%">
                            <asp:Button ID="btnObservar" runat="server" Text=" Observar" BorderStyle="Outset"
                                CssClass="observar" Width="80px" Font-Bold="True" />
                            <asp:Button ID="btnAprobar" runat="server" Text=" Aprobar" BorderStyle="Outset" CssClass="aprobar"
                                Width="80px" Font-Bold="True" OnClientClick="return confirm('¿Está seguro que desea aprobar el presupuesto?');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlObservaciones" runat="server" class="observaciones">
                    <table width="100%">
                        <tr>
                            <td>
                                Observaciones :
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:BulletedList ID="blObservaciones" runat="server" BulletStyle="Disc" DisplayMode="Text"
                                    DataTextField="descripcion">
                                </asp:BulletedList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlObservar" runat="server">
                    <table width="100%" cellpadding="8" cellspacing="0">
                        <tr>
                            <td width="15%">
                                Observación
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ingresar observación"
                                    ControlToValidate="txtObservacion" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="85%">
                                <asp:TextBox ID="txtObservacion" runat="server" MaxLength="1600" TextMode="MultiLine"
                                    Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Button ID="cmdVolver" runat="server" Text="Regresar" BorderStyle="Outset" CssClass="volver"
                                    Font-Bold="True" />
                                &nbsp;
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" BorderStyle="Outset" CssClass="guardar"
                                    ValidationGroup="Guardar" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlPresupuesto" runat="server">
                    <table width="100%" cellpadding="8" cellspacing="0">
                        <tr>
                            <td width="15%">
                                <%--Centro de Costo--%> <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                                Programa / Proyecto <%--treyes 28/11/2016 se modifica a solicitud de esaavedra--%>
                            </td>
                            <td>
                                <asp:Label ID="lblCeco" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Instancia
                            </td>
                            <td>
                                <asp:Label ID="lblInstancia" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Estado
                            </td>
                            <td>
                                <asp:Label ID="lblEstado" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cabeceraDatos">
                                Lista de actividades de presupuesto (Dar clic sobre cada actividad para VER su detalle)
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvCabecera" runat="server" AutoGenerateColumns="False" DataSourceID="sqlDatosCabecera"
                                    DataKeyNames="codigo_Dap,codigo_Ejp,codigo_iep,habilitado_Pto,codigo_cco" Width="100%"
                                    ShowFooter="true">
                                    <Columns>
                                        <asp:BoundField DataField="ACTIVIDAD" HeaderText="ACTIVIDAD" SortExpression="ACTIVIDAD" />
                                        <asp:BoundField DataField="INGRESOS (S/.)" HeaderText="INGRESOS (S/.)" ReadOnly="True"
                                            SortExpression="INGRESOS (S/.)" DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EGRESOS (S/.)" HeaderText="EGRESOS (S/.)" ReadOnly="True"
                                            SortExpression="EGRESOS (S/.)" DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_Cco" HeaderText="codigo_Cco" SortExpression="codigo_Cco"
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_Pto" HeaderText="codigo_Pto" SortExpression="codigo_Pto"
                                            Visible="False" />
                                        <%--<asp:CommandField SelectText="" ShowSelectButton="True" />--%>
                                        <asp:CommandField ButtonType="Image" SelectText="Select" SelectImageUrl="~/images/Presupuesto/previo.gif"
                                            ShowSelectButton="True" HeaderText="VER">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <strong>No se encontraron registros</strong>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#D8E5FF" />
                                    <HeaderStyle CssClass="tituloTabla" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="cabeceraDatos" id="TdDetalle">
                                Detalle de presupuesto
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="TdNuevo">
                                <asp:Button ID="cmdNuevo" runat="server" Text="Nuevo" BorderStyle="Outset" CssClass="nuevoSmall"
                                    ToolTip="Añadir un nuevo ítem al presupuesto" />
                       
                            </td>
                            </tr>
                            <tr>
                            <td colspan="2" align="right">
                                   <asp:Button ID="cmdVisibilidad" runat="server" Text="   Visibilidad" BorderStyle="Outset" CssClass="nuevoSmall" Visible="false" />
                            </td>
                            </tr>
                        
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvDetalle" runat="server" DataSourceID="sqlDatosDetalle" Width="100%"
                                    AutoGenerateColumns="False" DataKeyNames="codigo_dpr" >
                                    <Columns>
                                        <asp:BoundField DataField="TIPO" HeaderText="TIPO" ReadOnly="True" SortExpression="TIPO" />
                                        <asp:BoundField DataField="CLASE" HeaderText="CLASE" SortExpression="CLASE" />
                                        <asp:BoundField DataField="COD.ITEM" HeaderText="COD.ITEM" SortExpression="COD.ITEM" />
                                        <asp:BoundField DataField="DES.ESTANDAR" HeaderText="DES.ESTANDAR" ReadOnly="True"
                                            SortExpression="DES.ESTANDAR" />
                                        <asp:BoundField DataField="DET.DESCRIPCION" HeaderText="DET.DESCRIPCION" SortExpression="DET.DESCRIPCION" />
                                        <asp:BoundField DataField="UNIDAD" HeaderText="UNIDAD" SortExpression="UNIDAD" />
                                        <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" SortExpression="CANTIDAD">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PREC.UNIT." HeaderText="PREC.UNIT." SortExpression="PREC.UNIT."
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SUBTOTAL" HeaderText="SUBTOTAL" ReadOnly="True" SortExpression="SUBTOTAL"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ENE (S/.)" HeaderText="ENE" SortExpression="ENE (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FEB (S/.)" HeaderText="FEB" SortExpression="FEB (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MAR (S/.)" HeaderText="MAR" SortExpression="MAR (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ABR (S/.)" HeaderText="ABR" SortExpression="ABR (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="MAY (S/.)" HeaderText="MAY" SortExpression="MAY (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JUN (S/.)" HeaderText="JUN" SortExpression="JUN (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="JUL (S/.)" HeaderText="JUL" SortExpression="JUL (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AGO (S/.)" HeaderText="AGO" SortExpression="AGO (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SET (S/.)" HeaderText="SET" SortExpression="SET (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OCT (S/.)" HeaderText="OCT" SortExpression="OCT (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NOV (S/.)" HeaderText="NOV" SortExpression="NOV (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DIC (S/.)" HeaderText="DIC" SortExpression="DIC (S/.)"
                                            DataFormatString="{0:#,###,##0.00}">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TIPOCON" HeaderText="TIPOCON" ReadOnly="True" SortExpression="TIPOCON"
                                            Visible="False" />
                                        <asp:BoundField DataField="CodConcepto" HeaderText="CodConcepto" SortExpression="CodConcepto"
                                            Visible="False" />
                                        <asp:BoundField DataField="IdUni" HeaderText="IdUni" SortExpression="IdUni" Visible="False" />
                                        <asp:CheckBoxField DataField="indicoCantidades" HeaderText="indicoCantidades" SortExpression="indicoCantidades"
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_Dpr" HeaderText="codigo_Dpr" InsertVisible="False"
                                            ReadOnly="True" SortExpression="codigo_Dpr" Visible="False" />
                                        <asp:CommandField ButtonType="Image" DeleteText="" EditImageUrl="~/images/Presupuesto/editar.gif"
                                            ShowEditButton="True" SelectText="" HeaderText="EDITAR">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="ELIMINAR">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return confirm('¿Desea eliminar el registro?');"
                                                    ImageUrl="~/images/Presupuesto/eliminar.gif" CommandName="Delete" ToolTip="Eliminar ítem presupuestado" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VISIBLE">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="checkbox1" runat="server" Checked='<%# Bind("visible_dpr") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="tituloTabla" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:SqlDataSource ID="sqlDatosCabecera" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                                    SelectCommand="PRESU_ConsultarPresupuestoPorUnidadArea_V3" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hddCodigo_pto" Name="codigo_pto" PropertyName="Value"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:SqlDataSource ID="sqlDatosDetalle" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                                    SelectCommand="PRESU_ConsultarPresupuestoPorUnidadAreaDetalle_V2" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="1" Name="tipo" Type="String" />
                                        <asp:ControlParameter ControlID="hddCodigo_ejp" Name="codigo_Ejp" PropertyName="Value"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="hddCodigo_Dap" Name="codigo_Dap" PropertyName="Value"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="hddCodigo_pto" Name="codigo" PropertyName="Value"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" id="TdRegresar">
                                <asp:Button ID="cmdRegresar" runat="server" Text="Regresar" BorderStyle="Outset"
                                    CssClass="volver" Width="80px" Font-Bold="True" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="Guardar" />
    <asp:HiddenField ID="hddCodigo_pto" runat="server" />
    <asp:HiddenField ID="hddCodigo_dap" runat="server" />
    <asp:HiddenField ID="hddCodigo_ejp" runat="server" />
    <asp:HiddenField ID="hddCodigo_iep" runat="server" />
    <asp:HiddenField ID="hddEvaPto" runat="server" Value="0" />
    <asp:HiddenField ID="hddHabilitado_pto" runat="server" />
    <asp:HiddenField ID="hddCantidadObs" runat="server" />
    </form>
</body>
</html>
