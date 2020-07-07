<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMonitorConsultaOrdenes.aspx.vb" Inherits="logistica_frmMonitorConsultaOrdenes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" class="contornotabla">
            <tr>
                <td>
                    <b>Buscar por:</b>
                    <asp:RadioButtonList ID="rbtBuscarPor" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True">
                        <asp:ListItem Value="PE">Por pedido</asp:ListItem>
                        <asp:ListItem Value="OR">Por orden C/S</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlOrdenes" runat="server">
                        &nbsp;&nbsp;<asp:Label ID="lblNro" runat="server" Text="Número de orden:"></asp:Label>&nbsp;<asp:TextBox ID="txtNro" runat="server" Width="78px"></asp:TextBox>&nbsp;
                        <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
                        &nbsp;<asp:DropDownList ID="cboTipoOrden" runat="server">
                            <asp:ListItem Value="15">Ordenes de Compra</asp:ListItem>
                            <asp:ListItem Value="36">Ordenes de Servicio</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="cmdBuscarOrd" runat="server" 
    Text="Buscar" />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" runat="server" Height="200px" ScrollBars="Vertical" 
                        CssClass="contornotabla">
                        <asp:GridView ID="gvCabOrden" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_Rco,Tipo Documento,descripcionEstado_Eped" 
                            DataSourceID="SqlDataSource4" Width="98%" AllowPaging="True" PageSize="5">
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
                                <asp:BoundField DataField="pago" HeaderText="Forma de Pago" />
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
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_BuscarOrdenCS" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="rbtBuscarPor" Name="tipo" 
                                    PropertyName="SelectedValue" Type="String"  /> 
                                <asp:ControlParameter ControlID="cboTipoOrden" Name="param1" 
                                    PropertyName="SelectedValue" Type="String"  /> 
                                <asp:Parameter Name="param2" Type="String" DefaultValue=" " />
                                <asp:Parameter Name="param3" Type="String" DefaultValue=" "/>
                                <asp:ControlParameter ControlID="txtNro" Name="param4" 
                                   PropertyName="Text" Type="String" /> 
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblEstado" runat="server"></asp:Label>&nbsp;| <asp:LinkButton ID="lnkVerRevisiones" runat="server" Font-Underline="True" 
                        ForeColor="Blue">Ver Revisiones</asp:LinkButton>
&nbsp;|
                    <asp:LinkButton ID="lnkVerDatos" runat="server" Font-Underline="True" 
                        ForeColor="Blue">Ver Datos Generales</asp:LinkButton>
                        
                    <!-- Fecha: 2018-09-10 | Programador: enevado ************************** -->
                    <!-- ******************** <LinkButton>: VerArchivos ******************** -->
                    &nbsp;| 
                    <asp:LinkButton ID="lnkVerArchivos" runat="server" Font-Underline="true" ForeColor="Blue">Ver Archivos</asp:LinkButton>
                    <!-- ******************** </LinkButton>: VerArchivos ******************** --> 
                    
                                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDatosGenerales" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDetalleCompra" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="idArt" DataSourceID="SqlDataSource2" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_rco" HeaderText="codigo_rco" ReadOnly="True" 
                                                SortExpression="codigo_rco" Visible="False" />
                                            <asp:BoundField DataField="idArt" HeaderText="idArt" ReadOnly="True" 
                                                SortExpression="idArt" Visible="False" />
                                            <asp:BoundField DataField="DetalleArticulo_Dco" HeaderText="Artículo" 
                                                SortExpression="DetalleArticulo_Dco" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" 
                                                SortExpression="descripcionuni" />
                                            <asp:BoundField DataField="Precio_Dco" DataFormatString="{0:#,###,##0.00}" 
                                                HeaderText="Precio" SortExpression="Precio_Dco">
                                                <ItemStyle HorizontalAlign="Right" BackColor="#FFDF9D" Font-Bold="True" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad_Dco" HeaderText="Cantidad" 
                                                SortExpression="cantidad_Dco">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="subtotal_Dco" DataFormatString="{0:#,###,##0.00}" 
                                                HeaderText="Sub total" SortExpression="subtotal_Dco">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Pedidos Relacionados" SelectText="Ver" 
                                                ShowSelectButton="True">
                                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Width="100px" />
                                            </asp:CommandField>
                                        </Columns>
                                        <SelectedRowStyle BackColor="#FFFFCC" />
                                        <HeaderStyle BackColor="#FFAA55" ForeColor="#333333" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="LOG_ConsultarDetalleOrden" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="font-weight: 700">
                                    » Pedidos Relacionados</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" 
                                        DataSourceID="SqlDataSource1">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" 
                                                SortExpression="codigo_cco" Visible="False" />
                                            <asp:BoundField DataField="AreaPresupuestal" HeaderText="Centro de costos" 
                                                SortExpression="AreaPresupuestal" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="codigo_Ped" HeaderText="Nro Pedido" 
                                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_Ped" >
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" 
                                                SortExpression="descripcionArt" />
                                            <asp:BoundField DataField="observacion_Dpe" HeaderText="Observación" 
                                                SortExpression="observacion_Dpe" />
                                            <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" 
                                                SortExpression="descripcionuni" />
                                            <asp:BoundField DataField="precioReferencial_Dpe" 
                                                HeaderText="Precio referencial" SortExpression="precioReferencial_Dpe" >
                                                <ItemStyle HorizontalAlign="Right" BackColor="#FFFFD2" Font-Bold="True" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad_Dpe" HeaderText="Cantidad Pedida" 
                                                SortExpression="cantidad_Dpe" >
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle BackColor="#FFE064" ForeColor="#FF6600" />
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="LOG_ConsultarPedidosRelacionadosOrden" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                            <asp:ControlParameter ControlID="gvDetalleCompra" Name="id_art" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlObservaciones" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td style="font-weight: 700">
                                    Revisiones</td>
                                <td>
                                    &nbsp;</td>
                                <td style="font-weight: 700">
                                    Observaciones resueltas</td>
                            </tr>
                            <tr>
                                <td width="48%">
                                    <asp:GridView ID="gvRevisiones" runat="server" DataSourceID="SqlDataSource7" 
                                        Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo Orden" HeaderText="Tipo Orden" />
                                            <asp:BoundField DataField="login_Per" HeaderText="Usuario" />
                                            <asp:BoundField DataField="Evaluación" HeaderText="Evaluación" />
                                            <asp:BoundField DataField="Observacion" HeaderText="Observación" />
                                            <asp:BoundField DataField="fecha_Rcom" HeaderText="Fecha" />
                                        </Columns>
                                        <HeaderStyle BackColor="#F7A350" ForeColor="#1E21D3" />
                                    </asp:GridView>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td valign="top" width="48%">
                                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource6" 
                                        Width="100%">
                                        <HeaderStyle BackColor="#F7A350" ForeColor="Blue" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="LOG_ConsultarObservacionesOrdenes" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:SqlDataSource ID="SqlDataSource7" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="LOG_ConsultarRevisionesOrdenes" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <!-- Fecha: 2018-09-10 | Programador: enevado ************************** -->
            <!-- ******************** <tr>: pnlArchivos ******************** -->
            <tr>
                <td>
                    <asp:Panel ID="pnlArchivos" runat="server">
                        <asp:GridView ID="gvArchivos" runat="server" Width="50%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="IdArchivosCompartidos" >
                            <Columns>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre Archivo" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operacion">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVerArchivo" runat="server" Text="Ver Archivo" CommandName="VerArchivo" 
                                            CommandArgument="<%# Ctype(Container,GridViewRow).RowIndex %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros!
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle ForeColor="Red"/>
                            <SelectedRowStyle BackColor="#FFCC66" />
                            <HeaderStyle BackColor="#FF9900" ForeColor="#0000CC" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <!-- ******************** <tr>: pnlArchivos ******************** -->
            </table>
    
    </div>
    <asp:HiddenField ID="hdCodigo_Rco" runat="server" />
    </form>
</body>
</html>
