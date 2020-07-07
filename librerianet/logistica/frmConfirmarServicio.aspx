<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="frmConfirmarServicio.aspx.vb" Inherits="logistica_frmConfirmarServicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css"rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
      <busyboxdotnet:busybox id="BusyBox1" runat="server" showbusybox="OnLeavingPage" image="Clock"
                text="Su solicitud está siendo procesada..." title="Por favor espere" />
    <div>
    
        <table style="width:100%;" cellpadding="0" cellspacing="2" 
            class="contornotabla">
            <tr>
                <td>
                    Ver pedidos:
                    <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="A">Pendientes</asp:ListItem>
                        <asp:ListItem Value="C">Atendidos</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;Número/Nombre Proveedor:
                    <asp:TextBox ID="txtBusqueda" runat="server" Rows="3"></asp:TextBox>
                &nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                        <asp:GridView ID="gvCabOrden" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_Rco,Tipo Documento" 
                            DataSourceID="SqlDataSource5" Width="98%" AllowPaging="True" 
                            AllowSorting="True" PageSize="5">
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
                                <asp:CommandField SelectText="" ShowSelectButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="#FFCC66" />
                            <HeaderStyle BackColor="#FF9900" ForeColor="#0000CC" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarServiciosPorConfirmar" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="cboEstado" DefaultValue="A" Name="tipo" 
                                    PropertyName="SelectedValue" Type="String" />
                                <asp:ControlParameter ControlID="txtBusqueda" DefaultValue="%" Name="valor" 
                                    PropertyName="Text" Type="String" />
                                <asp:QueryStringParameter Name="funcion" QueryStringField="ctf" 
                                    Type="Int32" />
                                <asp:QueryStringParameter Name="codigo_per" QueryStringField="id" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="contornotabla" style="width:100%;">
                        <tr>
                <td width="15%">
                    Evaluación:</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="rbtVeredicto" 
                        ErrorMessage="Seleccione el veredicto de la evaluación" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
                <td align="justify">
                    <asp:RadioButtonList ID="rbtVeredicto" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True">
                        <asp:ListItem Value="C">Conforme</asp:ListItem>
                        <asp:ListItem Value="X">Derivar a</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
        <asp:Panel ID="pnlDerivar" runat="server" Visible="False">
            Derivar a:&nbsp; <asp:DropDownList ID="cboPersonalDerivar" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
        </asp:Panel>
                </td>
                <td>
                    <asp:LinkButton ID="lnkVerRevisiones" runat="server" Font-Underline="True" 
                        ForeColor="Blue">Ver Revisiones</asp:LinkButton>
&nbsp;|
                    <asp:LinkButton ID="lnkVerDatos" runat="server" Font-Underline="True" 
                        ForeColor="Blue">Ver Datos Generales</asp:LinkButton>
                                </td>
                        </tr>
                        <tr>
                <td valign="top" class="style2">
                    Observación:</td>
                <td valign="top">
                    &nbsp;</td>
                <td valign="top" colspan="2">
                    <asp:TextBox ID="txtObservacion" runat="server" Width="75%"></asp:TextBox>
                                </td>
                <td align="right" valign="top">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Calificar" 
                        ValidationGroup="Guardar" CssClass="guardar" Height="23px" Width="95px" />
&nbsp;
                </td>
                        </tr>
                        <tr>
                <td valign="top" colspan="5">
                    <hr />
                            </td>
                        </tr>
                        <tr>
                <td colspan="5">
                    <asp:Panel ID="PnlDatosGenerales" runat="server">
                        <asp:GridView ID="gvDetalleCompra" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_rco,idArt" 
    DataSourceID="SqlDataSource2" Width="100%">
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
                                <asp:CommandField HeaderText="Pedidos Relacionados" SelectText="Ver" 
                                    ShowSelectButton="True">
                                    <ItemStyle ForeColor="Red" HorizontalAlign="Center" Width="100px" />
                                </asp:CommandField>
                            </Columns>
                            <SelectedRowStyle BackColor="#FFFF99" />
                            <HeaderStyle BackColor="#FF9933" ForeColor="#0000CC" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
                        </tr>
                        <tr>
                <td colspan="5">
                    &nbsp;</td>
                        </tr>
                        <tr>
                <td colspan="5">
                    <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="NumeroDoc" HeaderText="Núm. Documento" />
                            <asp:BoundField DataField="NroPedido" HeaderText="Nro Pedido" />
                            <asp:BoundField DataField="solicitante" HeaderText="Solicitante" />
                            <asp:BoundField DataField="centrocostos" HeaderText="Centro de costos" />
                        </Columns>
                        <HeaderStyle BackColor="#F7A350" ForeColor="Blue" />
                    </asp:GridView>
                </td>
                        </tr>
                        <tr>
                <td colspan="5">
                    <asp:Panel ID="pnlObservaciones" runat="server" Visible="False">
                        <table style="width:100%;">
                            <tr>
                                <td style="font-weight: bold">
                                    Revisiones</td>
                                <td>
                                    &nbsp;</td>
                                <td style="font-weight: bold">
                                    Observaciones resueltas</td>
                            </tr>
                            <tr>
                                <td width="48%">
                                    <asp:GridView ID="gvRevisiones" runat="server" DataSourceID="SqlDataSource1" 
                                        Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo Orden" HeaderText="Tipo Orden" />
                                            <asp:BoundField DataField="login_per" HeaderText="Login " />
                                            <asp:BoundField DataField="Evaluación" HeaderText="Evaluación" />
                                            <asp:BoundField DataField="observacion" HeaderText="Observación" />
                                            <asp:BoundField DataField="fecha_Rcom" HeaderText="Fecha registro" />
                                        </Columns>
                                        <HeaderStyle BackColor="#F7A350" ForeColor="#1E21D3" />
                                    </asp:GridView>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td valign="top" width="48%">
                                    <asp:GridView ID="gvObservacionesResueltas" runat="server" DataSourceID="SqlDataSource7" 
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
                            <tr>
                                <td style="font-weight: bold">
                                    Confirmación de Servicio</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:GridView ID="gvConfirmacion" runat="server" AutoGenerateColumns="False" 
                                        DataSourceID="SqlDataSource8">
                                        <Columns>
                                            <asp:BoundField DataField="Personal" HeaderText="Verificado por" />
                                            <asp:BoundField DataField="fecha_cos" HeaderText="Fecha registro" />
                                            <asp:BoundField DataField="observacion_cos" HeaderText="Observación" />
                                            <asp:BoundField DataField="estado_cos" HeaderText="Estado" />
                                        </Columns>
                                        <HeaderStyle BackColor="#F7A350" ForeColor="Blue" />
                                    </asp:GridView>
                                </td>
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
                        <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarObservacionesOrdenes" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
                        </tr>
                        <tr>
                <td class="style2" colspan="5">
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="LOG_ConsultarDetalleOrden" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource8" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="LOG_ConsultarConfirmacionDePedidos" 
                            SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                        </asp:SqlDataSource>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
