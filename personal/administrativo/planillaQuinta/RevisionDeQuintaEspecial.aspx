<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="RevisionDeQuintaEspecial.aspx.vb" Inherits="librerianet_planillaQuinta_RevisionDeQuintaEspecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../../private/tooltip.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td>
                        Ejercicio Presupuestal:
                        <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Planilla:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="cboPlanilla" runat="server" 
                        AutoPostBack="True">
                            <asp:ListItem Value="134">Planilla 1</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkCecos" runat="server" Checked="True" 
                        Text="Sólo planillas registradas y pendientes por revisar" 
                        AutoPostBack="True" ForeColor="Blue" />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" Height="150px" 
                        ScrollBars="Vertical">
                            <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_cco" GridLines="Horizontal" Width="98%">
                                <Columns>
                                    <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costos" />
                                    <asp:BoundField DataField="TotalRegistrado" HeaderText="Total" >
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                </Columns>
                                <SelectedRowStyle BackColor="#FFDD95" />
                                <HeaderStyle BackColor="#FF9900" />
                            </asp:GridView>
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
                        <table style="width:100%;" class="contornotabla">
                            <tr>
                                <td height="13" 
                    style="font-weight: bold;">
                                    Evaluación del detalle de quinta especial</td>
                            </tr>
                            <tr>
                                <td bgcolor="#FFFFEA" height="13" 
                    style="padding: inherit; border: 1px solid #FFFF00">
                                    <img alt="" src="../../../images/help.gif" /> Ingrese un comentario para enviar correo 
                    a la instancia correspondiente al dar clic en el botón que corresponda</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtMensaje" runat="server" TextMode="MultiLine" Width="100%" 
                        Rows="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="cmdRetornar" runat="server" Text="Retornar para modificación" 
                        Width="225px" />
                                    &nbsp;<asp:Button ID="cmdEnviar" runat="server" Text="Enviar a Dirección de Personal" 
                                        Width="225px" />
                                    &nbsp;<asp:Label ID="lblRespuesta" runat="server" ForeColor="Blue"></asp:Label>
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                        AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <font style="color:Blue">Procesando. Espere un momento...</font>
                                            <img alt="" src="../../images/loading.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvDetalleQuinta" 
                        runat="server" AutoGenerateColumns="False" 
                                                        DataKeyNames="codigo_Adplla,codigo_ejp,codigo_plla,codigo_per,codigo_cco,codigo_Fqta,codigo_per_registra" 
                                                        DataSourceID="SqlDataSource1" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de costos" 
                                                                SortExpression="descripcion_Cco" Visible="False" />
                                            <asp:BoundField DataField="Trabajador" HeaderText="Trabajador" ReadOnly="True" 
                                                                SortExpression="Trabajador" />
                                            <asp:BoundField DataField="descripcion_Fqta" HeaderText="Función" 
                                                                SortExpression="descripcion_Fqta" />
                                            <asp:BoundField DataField="importe_adplla" HeaderText="Importe" 
                                                                SortExpression="importe_adplla">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="observacion_adplla" HeaderText="Observacion" 
                                                                SortExpression="observacion_adplla" />
                                            <asp:BoundField DataField="Estado" HeaderText="Estado" ReadOnly="True" 
                                                                SortExpression="Estado" />
                                            <asp:BoundField DataField="codigo_adplla" HeaderText="codigo_adplla" 
                                                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_adplla" 
                                                                Visible="False" />
                                            <asp:BoundField DataField="codigo_Ejp" HeaderText="codigo_Ejp" 
                                                                SortExpression="codigo_Ejp" Visible="False" />
                                            <asp:BoundField DataField="codigo_Plla" HeaderText="codigo_Plla" 
                                                                SortExpression="codigo_Plla" Visible="False" />
                                            <asp:BoundField DataField="Codigo_Per" HeaderText="Codigo_Per" 
                                                                SortExpression="Codigo_Per" Visible="False" />
                                            <asp:BoundField DataField="codigo_Cco" HeaderText="codigo_Cco" 
                                                                SortExpression="codigo_Cco" Visible="False" />
                                            <asp:BoundField DataField="codigo_Fqta" HeaderText="codigo_Fqta" 
                                                                SortExpression="codigo_Fqta" Visible="False" />
                                            <asp:BoundField DataField="Responsable_Registro" 
                                                                HeaderText="Responsable de Registro" />
                                        </Columns>
                                        <HeaderStyle BackColor="#FF9900" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
        <asp:HiddenField ID="hddSw" runat="server" />
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                        SelectCommand="PRESU_ConsultarAnexoDetallePlanillaEjecutadoPorTrabajadoryCco" 
                                                        SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="cboPlanilla" Name="codigo_plla" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                            <asp:ControlParameter ControlID="gvCecos" Name="codigo_cco" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                            <asp:Parameter Name="codigo_per" Type="Int32" DefaultValue="0" />                                                            
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
    </form>
</body>
</html>
