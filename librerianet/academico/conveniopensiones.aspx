<%@ Page Language="VB" AutoEventWireup="false" CodeFile="conveniopensiones.aspx.vb" Inherits="conveniopensiones" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 175px;
            font-size: 10pt;
            font-weight: bold;
        }
        .style2
        {
            font-size: x-small;
            color: #0000CC;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="border: 1px solid #000000; width:100%; font-family: Arial;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td class="style1">
                            Código Universitario</td>
                        <td class="style2">
                            :
                            <asp:Label ID="LblCodigoUniver" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Apellidos y Nombres</td>
                        <td class="style2">
                            :
                            <asp:Label ID="LblNombres" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Escuela Profesional</td>
                        <td class="style2">
                            :
                            <asp:Label ID="LblEscuela" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div>
    
    </div>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="LblListaConvenio" runat="server" 
                    style="font-family: Arial; font-weight: 700; font-size: x-small" 
                    Text="Lista de Convenios"></asp:Label>
&nbsp;
                <asp:Label ID="LblClicConvenio" runat="server" 
                    style="font-family: Verdana; font-size: xx-small; color: #CC0000" 
                    Text="Haga clic en el Convenio para visualizar los detalles"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_Cpa" DataSourceID="ListaConvenios" GridLines="None" 
                    Width="100%">
                    <RowStyle Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" />
                    <Columns>
                        <asp:BoundField DataField="codigo_Cpa" HeaderText="N° Convenio" 
                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_Cpa">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado_Cpa" HeaderText="Estado" 
                            SortExpression="estado_Cpa">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_Cpa" HeaderText="Fecha" 
                            SortExpression="fecha_Cpa">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="reponsable" HeaderText="Analista Pensiones" 
                            SortExpression="reponsable">
                            <ItemStyle Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombreResp_Cpa" HeaderText="Resp. del Pago" 
                            SortExpression="nombreResp_Cpa">
                            <ItemStyle Width="180px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="observacion_Cpa" HeaderText="Observacion" 
                            SortExpression="observacion_Cpa" />
                        <asp:CommandField SelectText=" " ShowSelectButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="#FFFF99" />
                    <HeaderStyle BackColor="#27758C" Font-Names="Verdana" Font-Size="10pt" 
                        ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp; &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblPlanPagos" runat="server" 
                    style="font-size: x-small; font-family: Arial; font-weight: 700" 
                    Text="Plan de Pagos"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LblDeudas" runat="server" 
                    style="font-weight: 700; font-family: Arial; font-size: x-small" 
                    Text="Deudas en Convenio"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="PlanPagos" Width="95%">
                    <RowStyle Font-Names="Verdana" Font-Size="8pt" />
                    <Columns>
                        <asp:BoundField DataField="fechaVencimiento_Dcp" HeaderText="Fecha Vcto." 
                            ReadOnly="True" SortExpression="fechaVencimiento_Dcp">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="simbolo_moneda" HeaderText="Mon." 
                            SortExpression="simbolo_moneda">
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="importe_Dcp" HeaderText="Cuota" 
                            SortExpression="importe_Dcp">
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pago" HeaderText="Pago" ReadOnly="True" 
                            SortExpression="Pago">
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" SortExpression="Saldo">
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Mora" DataField="Mora">
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Sub Total">
                            <ItemStyle HorizontalAlign="Right" Width="15%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#27758C" Font-Names="Verdana" Font-Size="10pt" 
                        ForeColor="White" />
                </asp:GridView>
                <table cellpadding="0" cellspacing="0" 
                    style="width: 95%; font-family: Verdana; font-size: 10px; font-weight: bold;">
                    <tr>
                        <td width="20%">
                            <asp:Label ID="LblTotalPlan" runat="server" ForeColor="#0000CC" Text="TOTALES"></asp:Label>
                        </td>
                        <td width="5%">
                            <asp:Label ID="LblMoneda" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="15%">
                            <asp:Label ID="LblCuota" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="15%">
                            <asp:Label ID="LblPago" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="15%">
                            <asp:Label ID="LblSaldo" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="15%">
                            <asp:Label ID="LblMora" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="15%">
                            <asp:Label ID="LblSubTotal" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="DeudasConvenio" Width="95%">
                    <RowStyle Font-Names="Verdana" Font-Size="8pt" />
                    <Columns>
                        <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" 
                            ReadOnly="True" SortExpression="descripcion_Sco">
                            <ItemStyle Width="50%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="simbolo_moneda" HeaderText="Mon." 
                            SortExpression="simbolo_moneda">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="monto_Dcp" HeaderText="Importe" 
                            SortExpression="monto_Dcp">
                            <ItemStyle HorizontalAlign="Right" Width="40%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#27758C" Font-Names="Verdana" Font-Size="10pt" 
                        ForeColor="White" />
                </asp:GridView>
                <table style="width: 95%; font-family: Verdana; font-size: 10px; font-weight: bold;">
                    <tr>
                        <td width="50%">
                            <asp:Label ID="LblTotalPlan0" runat="server" ForeColor="#0000CC" Text="TOTALES"></asp:Label>
                        </td>
                        <td width="10%">
                            <asp:Label ID="LblMoneda0" runat="server"></asp:Label>
                        </td>
                        <td align="right" width="40%">
                            <asp:Label ID="LblTotalimporte" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:SqlDataSource ID="PlanPagos" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarConvenioPago" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="PP" Name="tipo" Type="String" />
            <asp:Parameter DefaultValue="&quot;&quot;" Name="tipoCli" Type="String" />
            <asp:ControlParameter ControlID="GridView1" Name="codigo" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DeudasConvenio" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarConvenioPago" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="DE" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="E" Name="tipoCli" 
                QueryStringField="tipo" Type="String" />
            <asp:ControlParameter ControlID="GridView1" Name="codigo" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="ListaConvenios" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarConvenioPago" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="CA" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="E" Name="tipoCli" 
                QueryStringField="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="codigo" 
                QueryStringField="codigo_alu" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
