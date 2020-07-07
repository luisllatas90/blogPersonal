<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="frmConsultarCargosPorCeco.aspx.vb" Inherits="personal_administrativo_pec_frmConsultarCargosPorCeco" Theme="Acero" %>

<%@ Register assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
     <!-- Para Calendario
     <script src="../SISREQ/private/calendario.js" language="javascript" type="text/javascript"></script>
     <script src="../SISREQ/private/PopCalendar.js" language="javascript" type="text/javascript"></script>
     -->
</head>
<body>
    <form id="form1" runat="server">
    <%  'response.write(clsfunciones.cargacalendario) %>
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        <asp:Button ID="btnExportar" runat="server" Text="Exportar a Excel" 
            SkinID="BotonAExcel" />
    
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td width="10%" >
                        Centro de Costos:</td>
                    <td width="90%"  align=left>
                        <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos"></asp:TextBox>
                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" style="width: 14px" 
                            Height="16px" ImageUrl="~/images/busca.gif" Width="23px" />
                        <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                        <br />
                        <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue">Búsqueda Avanzada</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td  colspan="2">
                        <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                            Width="100%">
                            <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                DataKeyNames="codigo_cco" ForeColor="#333333" ShowHeader="False" 
                              Width="98%" SkinID="skinGridView">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <b>No se encontraron items con el término de búsqueda</b>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style4">
                        Servicio:</td>
                    <td class="style3">
                        <asp:DropDownList ID="cboServ" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="btnConsultar" runat="server" Text="Ejecutar Consulta" 
                            SkinID="BotonBuscar" />
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    
                </tr>
                <tr>
                    <td class="style2" colspan="2">
                        Resultado de la consulta:<br />
                        Nota: La columna Abono es igual a Pagos en Efectivo + Notas de Credito </td>
                </tr>
                <tr>
                    <td class="style2" colspan="2">
                        <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" DataKeyNames="codigo_Deu" GridLines="None" 
                          SkinID="skinGridViewLineasIntercalado">
                            <FooterStyle Font-Bold="True" />
                            <Columns>
                                <asp:BoundField HeaderText="Nro" />
                                <asp:BoundField DataField="COD. RESP." HeaderText="COD. RESP." ReadOnly="True" 
                                    SortExpression="COD. RESP." >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CLIENTE" HeaderText="CLIENTE" ReadOnly="True" 
                                    SortExpression="CLIENTE" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CARRERA" HeaderText="CARRERA" ReadOnly="True" 
                                    SortExpression="CARRERA" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_CARGO" HeaderText="FECHA_CARGO" 
                                    ReadOnly="True" SortExpression="FECHA_CARGO" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SERVICIO" HeaderText="SERVICIO" 
                                    SortExpression="SERVICIO" >
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CARGOS" HeaderText="CARGOS" ReadOnly="True" 
                                    SortExpression="CARGOS" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ABONOS" HeaderText="ABONOS" ReadOnly="True" 
                                    SortExpression="ABONOS" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DIFERENCIA" HeaderText="SALDO" 
                                    SortExpression="DIFERENCIA" >
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FECHA_VENC" HeaderText="FECHA_VENC" 
                                    SortExpression="FECHA_VENC" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Observacion" HeaderText="OBSERVACION">
                                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="codigo_Deu" HeaderText="codigo_Deu" ReadOnly="True" 
                                    SortExpression="codigo_Deu" Visible="False" />
                                <asp:HyperLinkField DataNavigateUrlFields="codigo_deu" 
                                    DataNavigateUrlFormatString="frmVerAbonos.aspx?id={0}" HeaderText="DETALLE" 
                                    Text="Ver Abonos" Target="_blank" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle Font-Bold="True" />
                            <HeaderStyle Font-Bold="True" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
