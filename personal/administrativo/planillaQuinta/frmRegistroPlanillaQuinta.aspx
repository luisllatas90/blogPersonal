﻿<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="frmRegistroPlanillaQuinta.aspx.vb" Inherits="planillaQuinta_frmRegistroPlanillaQuinta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width:100%;" class="contornotabla">
                                <tr>
                                    <td>
                                        Ejercicio Presupuestal</td>
                                    <td>
                                        <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="150px">
                                        Planilla</td>
                                    <td>
                                        <asp:DropDownList ID="cboPlanilla" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="134">Planilla 1</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Centro de costos
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Ingrese centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos" 
                                            Width="200px"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarCecos" runat="server" 
                                            ImageUrl="~/images/busca.gif" ValidationGroup="BuscarCecos" />
                                        <asp:Label ID="lblTextBusqueda" runat="server" Text="(clic aquí)"></asp:Label>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue">Busqueda 
                                        Avanzada</asp:LinkButton>
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                            AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <font style="color:Blue">Procesando. Espere un momento...</font>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                            Width="100%">
                                            <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                                CellPadding="4" DataKeyNames="codigo_cco" ForeColor="#333333" GridLines="None" 
                                                ShowHeader="False" Width="98%">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ItemStyle ForeColor="Red" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron items con el término de búsqueda</b>
                                                </EmptyDataTemplate>
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FF9900" colspan="2" style="font-weight: 700">
                                        Detalle de Quinta Especial</td>
                                </tr>
                                <tr>
                                    <td>
                                        Personal
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                            ControlToValidate="cboPersonal" ErrorMessage="Ingrese personal" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboPersonal" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaPersonal" runat="server" ValidationGroup="BuscarCecos" 
                                            Width="200px"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarPersonal" runat="server" 
                                            ImageUrl="~/images/busca.gif" ValidationGroup="BuscarCecos" />
                                        <asp:Label ID="lblClicPersonal" runat="server" Text="(clic aquí)"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:LinkButton ID="lnkBusquedaAvanzadaPer" runat="server" ForeColor="Blue">Busqueda 
                                        Avanzada</asp:LinkButton>
                                        <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                            AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <font style="color:Blue">Procesando. Espere un momento...</font>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="Panel2" runat="server" Height="150px" ScrollBars="Vertical" 
                                            Width="100%">
                                            <asp:GridView ID="gvPersonal" runat="server" ShowHeader="False" Width="98%" 
                                                AutoGenerateColumns="False" CellPadding="4" DataKeyNames="codigo_per" 
                                                ForeColor="#333333" GridLines="None">
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_per" HeaderText="Código" />
                                                    <asp:BoundField DataField="personal" HeaderText="Persona" />
                                                    <asp:CommandField ShowSelectButton="True">
                                                        <ItemStyle ForeColor="Red" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Función
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                            ControlToValidate="cboFuncion" ErrorMessage="Ingrese función" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboFuncion" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Importe
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                            ControlToValidate="txtImporte" ErrorMessage="El importe debe ser mayor a cero" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                            ControlToValidate="txtImporte" ErrorMessage="Ingrese importe" 
                                            ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImporte" runat="server" Width="85px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Observación </td>
                                    <td>
                                        <asp:TextBox ID="txtObservacion" runat="server" MaxLength="250" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                            ValidationGroup="Guardar" />
                                        &nbsp;<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" />
                                        &nbsp;
                                        <asp:Label ID="lblMsjGuardar" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="margin-left: 80px">
                                        <table class="contornotabla" style="width:100%;">
                                            <tr>
                                                <td align="right" colspan="3">
                                                    <asp:Label ID="lblRespuesta" runat="server" ForeColor="Blue"></asp:Label>
                                                    <asp:HiddenField ID="hddSw" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddTotal" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddCodigo_Adplla" runat="server" />
                                                    <asp:Label ID="lblMsjEnviado" runat="server" ForeColor="Red"></asp:Label>
                                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                                                    <asp:Button ID="cmdEnviar" runat="server" Text="Aprobar Planilla(Enviar)" 
                                                        />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="3">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:GridView ID="gvDetalleQuinta" runat="server" AutoGenerateColumns="False" 
                                                        ShowFooter="True"
                                                        DataKeyNames="codigo_Adplla,codigo_ejp,codigo_plla,codigo_per,codigo_cco,codigo_Fqta" 
                                                        DataSourceID="SqlDataSource1" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de costos" 
                                                                SortExpression="descripcion_Cco" />
                                                            <asp:BoundField DataField="Trabajador" HeaderText="Trabajador" ReadOnly="True" 
                                                                SortExpression="Trabajador" />
                                                            <asp:BoundField DataField="descripcion_Fqta" HeaderText="Función" 
                                                                SortExpression="descripcion_Fqta" />
                                                            <asp:BoundField DataField="importe_adplla" HeaderText="Importe" 
                                                                SortExpression="importe_adplla" />
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
                                                            <asp:CommandField SelectText="Editar" ShowSelectButton="True">
                                                                <ItemStyle ForeColor="Blue" />
                                                            </asp:CommandField>
                                                            <asp:CommandField ShowDeleteButton="True">
                                                                <ItemStyle ForeColor="Red" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#FF9900" />
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
                                                <td>
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                        SelectCommand="PRESU_ConsultarAnexoDetallePlanillaEjecutadoPorTrabajadoryCco" 
                                                        SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="cboPlanilla" Name="codigo_plla" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                            <asp:ControlParameter ControlID="cboCecos" Name="codigo_cco" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                            <asp:ControlParameter ControlID="cboPersonal" DefaultValue="" Name="codigo_per" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
