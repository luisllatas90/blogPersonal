<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrdenesObservadas.aspx.vb" Inherits="logistica_OrdenesObservadas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    
        <table style="width:100%;" cellpadding="0" cellspacing="2" 
            class="contornotabla">
            <tr>
                <td>
                    Observadas por:
                    <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource3" DataTextField="Personal" 
                        DataValueField="codigo_Per">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="LOG_ConsultarEvaluadoresCompra" 
                        SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="contornotabla">
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="gvCabOrden" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_Rco,codigo_Rcom,Tipo Documento,codigo_cob,referencia_rco" 
                            DataSourceID="SqlDataSource4" Width="98%" AllowPaging="True">
                            <Columns>
                                <asp:BoundField DataField="codigo_Rco" HeaderText="codigo_Rco" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="codigo_Rco" 
                                    Visible="False" />
                                <asp:BoundField DataField="fechaReg_Rco" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha Reg." SortExpression="fechaReg_Rco">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaDoc_Rco" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha Doc." SortExpression="fechaDoc_Rco">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_Tdo" HeaderText="Tipo Documento" 
                                    SortExpression="descripcion_Tdo" Visible="False" />
                                <asp:BoundField DataField="Tipo Documento" HeaderText="Tipo Orden" 
                                    ReadOnly="True" SortExpression="Tipo Documento">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numeroDoc_Rco" HeaderText="Número" 
                                    SortExpression="numeroDoc_Rco">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrePro" HeaderText="Proveedor" 
                                    SortExpression="nombrePro" />
                                <asp:BoundField DataField="moneda_Rco" HeaderText="Moneda" 
                                    SortExpression="moneda_Rco">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="porcentajeIGV_Rco" 
                                    DataFormatString="{0:#,###,##0.00}" HeaderText="IGV" 
                                    SortExpression="porcentajeIGV_Rco">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="precioIncluyeIGV_Rco" HeaderText="Incluye IGV" 
                                    SortExpression="precioIncluyeIGV_Rco">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CheckBoxField>
                                <asp:BoundField DataField="totalCompra_Rco" DataFormatString="{0:#,###,##0.00}" 
                                    HeaderText="Total (S/.)" SortExpression="totalCompra_Rco">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_Alm" HeaderText="Almacén" 
                                    SortExpression="nombre_Alm" />
                                <asp:CommandField SelectText="" ShowSelectButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="#FFCC66" />
                            <HeaderStyle BackColor="#FF9900" ForeColor="#0000CC" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarOrdenesObservadas" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="cboEstado" 
                                    Name="codigo" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="contornotabla" style="width:100%;">
                        <tr>
                <td valign="top" class="style2">
                    Observación:
                    <asp:TextBox ID="txtObservacion" runat="server" Rows="3" 
                        Width="440px" MaxLength="500"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                        runat="server" ControlToValidate="txtObservacion" 
                        ErrorMessage="Tiene que registrar una respuesta a esta observacion" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;<asp:LinkButton ID="lnkRevisiones" runat="server" 
                        ForeColor="Blue">Ver 
                    Revisiones</asp:LinkButton>
&nbsp;|<asp:LinkButton ID="lnkDatosGenerales" runat="server" ForeColor="Blue">Datos Generales</asp:LinkButton>
                                </td>
                <td valign="top">
                    &nbsp;</td>
                <td align="right" valign="top">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Calificar" 
                        ValidationGroup="Guardar" CssClass="guardar" Height="23px" Width="95px" />
&nbsp;
                </td>
                        </tr>
                        <tr>
                <td valign="top" colspan="3">
                    <hr />
                            </td>
                        </tr>
                        <tr>
                <td colspan="3">
                    <asp:Panel ID="pnlDetalle" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDetalleCompra" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_rco,idArt" DataSourceID="SqlDataSource2" 
                Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_rco" HeaderText="codigo_rco" ReadOnly="True" 
                                SortExpression="codigo_rco" Visible="False" />
                                            <asp:BoundField DataField="idArt" HeaderText="idArt" ReadOnly="True" 
                                SortExpression="idArt" Visible="False" />
                                            <asp:BoundField DataField="DetalleArticulo_Dco" HeaderText="Articulo" 
                                SortExpression="DetalleArticulo_Dco" />
                                            <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" 
                                SortExpression="descripcionuni" />
                                            <asp:BoundField DataField="Precio_Dco" HeaderText="Precio" 
                                SortExpression="Precio_Dco" DataFormatString="{0:#,###,##0.00}" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad_Dco" HeaderText="Cantidad" 
                                SortExpression="cantidad_Dco" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="subtotal_Dco" HeaderText="Sub total" 
                                SortExpression="subtotal_Dco" DataFormatString="{0:#,###,##0.00}" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#FF9933" ForeColor="#0000CC" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEtiquetaObs" runat="server" 
                Text="Observación de la orden:"></asp:Label>
                                    &nbsp;<asp:Label ID="lblObservacionesOrden" runat="server" Height="20px" 
                        Width="80%"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                        </tr>
                        <tr>
                <td class="style2" colspan="3">
                    <asp:Panel ID="pnlObservaciones" runat="server" Visible="False">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    Revisiones</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Observaciones resueltas</td>
                            </tr>
                            <tr>
                                <td width="48%">
                                    <asp:GridView ID="gvRevisiones" runat="server" DataSourceID="SqlDataSource1" 
                                        Width="100%">
                                        <HeaderStyle BackColor="#F7A350" ForeColor="#1E21D3" />
                                    </asp:GridView>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td valign="top" width="48%">
                                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource5" 
                                        Width="100%">
                                        <HeaderStyle BackColor="#F7A350" ForeColor="Blue" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="LOG_ConsultarRevisionesOrdenes" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarDetalleOrden" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarObservacionesOrdenes" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
                        </tr>
                        </table>
                </td>
            </tr>
            </table>
    
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
    
    </form>
</body>
</html>
