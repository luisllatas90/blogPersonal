<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="frmRegistroPlanillaQuintaArea.aspx.vb" Inherits="planillaQuinta_frmRegistroPlanillaQuintaArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript">

function Maximizar(img,ruta,tmMax,tmNormal)
{
	if(tblMnuPrincipal.style.display==""){
		img.src=ruta + 'images/maximiza.gif' 
		tblMnuPrincipal.style.display="none"
		tblTituloMnu.style.display=""
		tdHistorico.style.width="3%"
		tblTituloMnu.style.border="1px solid #808080"
		pnlHistorico.style.border=""
	}
	else{
		img.src=ruta + 'images/minimiza.gif'
		tblMnuPrincipal.style.display=""
		tblTituloMnu.style.display="none"
		tdHistorico.style.width="40%"
        pnlHistorico.style.border="1px solid #808080"
	}
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width:100%;" >
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="width:100%;" class="contornotabla">
                                <tr>
                                    <td colspan="2">
                                        Ejercicio Presupuestal:
                                        <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Planilla&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                        <asp:DropDownList ID="cboPlanilla" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="134">Planilla 1</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        Centro de costos<asp:CompareValidator ID="CompareValidator4" runat="server" 
                                            ControlToValidate="cboCecos" ErrorMessage="Ingrese centro de costos" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;<asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        &nbsp;<asp:ImageButton ID="imgRefrescar" runat="server" Height="22px" 
                                            ImageUrl="../../../images/refresh_svg.jpg" Width="22px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FF9900" colspan="2" style="font-weight: 700">
                                        Detalle de Quinta Especial</td>
                                </tr>
                                <tr>
                                    <td>
                                        Personal<asp:CompareValidator ID="CompareValidator3" runat="server" 
                                            ControlToValidate="cboPersonal" ErrorMessage="Ingrese personal" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                        <asp:DropDownList ID="cboPersonal" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaPersonal" runat="server" ValidationGroup="BuscarCecos" 
                                            Width="200px"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBuscarPersonal" runat="server" 
                                            ImageUrl="../../images/busca.gif" ValidationGroup="BuscarCecos" />
                                        <asp:Label ID="lblClicPersonal" runat="server" Text="(clic aquí)"></asp:Label>
                                    </td>
                                    <td rowspan="7" valign="top" width="40%" id="tdHistorico" >
                                        <asp:Panel ID="pnlHistorico" runat="server" Height="200px" BorderWidth="1px">
                                            
                                            <table id="tblMnuPrincipal" >
                                                <tr>
                                                    <td style="font-weight: 700">
                                                        &nbsp;» Historial</td>
                                                    <td width="3%"align="right" height="8%" style="cursor:hand" class="bordeinf" >
                                                        <img alt="" src="../../../images/maximiza.gif" 
                                                            onClick="Maximizar(this,'../../../','100%','65%')" /></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <b>Antes:</b> acumulados procesados de meses anteriores al mes seleccionado</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <b>Actual:</b> acumulado procesado y no procesado del mes seleccionado
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Panel ID="Panel3" runat="server" Height="100px" ScrollBars="Vertical" 
                                                            Width="100%">
                                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                                                                DataSourceID="ObjectDataSource1" Width="95%">
                                                                <Columns>
                                                                    <asp:BoundField DataField="TRABAJADOR" HeaderText="Trabajador">
                                                                        <HeaderStyle BackColor="#FF9900" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ANTES" HeaderText="Antes">
                                                                        <HeaderStyle BackColor="Yellow" />
                                                                        <ItemStyle BackColor="#FFFF66" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ACTUAL" HeaderText="Actual">
                                                                        <HeaderStyle BackColor="#3399FF" />
                                                                        <ItemStyle BackColor="#66CCFF" HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <br />
                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                                            SelectMethod="ConsultarHistoricoQuintaPlanillaArea" TypeName="clsPlanilla">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="cboPlanilla" Name="codigo_plla" 
                                                                    PropertyName="SelectedValue" Type="Int32" />
                                                                <asp:QueryStringParameter Name="codigo_per" QueryStringField="id" 
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%;display:none;cursor:hand" id="tblTituloMnu" bgcolor="#CCCCCC">
	                                            <tr><td align="left" valign="top">
		                                            <img alt="" src="../../../images/minimiza.gif" onClick="Maximizar(this,'../../../','100%','65%')" /></td>
                                                     </td>
	                                            </tr>
                                            </table> 
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lnkBusquedaAvanzadaPer" runat="server" ForeColor="Blue">Busqueda 
                                        Avanzada</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                        Función<asp:CompareValidator ID="CompareValidator2" runat="server" 
                                            ControlToValidate="cboFuncion" ErrorMessage="Ingrese función" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                        <asp:DropDownList ID="cboFuncion" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Importe<asp:CompareValidator ID="CompareValidator1" runat="server" 
                                            ControlToValidate="txtImporte" ErrorMessage="El importe debe ser mayor a cero" 
                                            Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                            ControlToValidate="txtImporte" ErrorMessage="Ingrese importe" 
                                            ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                        <asp:TextBox ID="txtImporte" runat="server" Width="85px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Detalle de servicio:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtObservacion" runat="server" MaxLength="250" Rows="3" 
                                            TextMode="MultiLine" Width="98%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="margin-left: 80px">
                                        <table class="contornotabla" style="width:100%;">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                                        ValidationGroup="Guardar" />
                                                    &nbsp;<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" />
                                                    &nbsp;
                                                    <asp:Label ID="lblMsjGuardar" runat="server" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="cmdEnviar" runat="server" Text="Enviar para revisión" 
                                                        Width="151px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2">
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                                        AssociatedUpdatePanelID="UpdatePanel1">
                                                        <ProgressTemplate>
                                                            <font style="color:Blue">Procesando. Espere un momento...</font>
                                                            <img alt="" src="../../images/loading.gif" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:Label ID="lblRespuesta" runat="server" ForeColor="Blue"></asp:Label>
                                                    <asp:Label ID="lblMsjEnviado" runat="server" ForeColor="Red"></asp:Label>
                                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="justify" bgcolor="#FFFFEA" 
                                                    style="padding: inherit; border: 1px solid #FFFF00" colspan="2">
                                                    <asp:Label ID="lbl" runat="server" BackColor="Yellow" BorderWidth="1px" 
                                                        Width="30px"></asp:Label>
                                                    &nbsp;Cualquier modificación sólo lo puede hacer la persona que realizó el registro</td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="gvDetalleQuinta" runat="server" AutoGenerateColumns="False" 
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
                                                <td colspan="2">
                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                        SelectCommand="PRESU_ConsultarAnexoDetallePlanillaEjecutadoPorAreayCco" 
                                                        SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="cboPlanilla" Name="codigo_plla" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                            <asp:ControlParameter ControlID="cboCecos" Name="codigo_cco" 
                                                                PropertyName="SelectedValue" Type="Int32" />
                                                            <asp:QueryStringParameter DefaultValue="" Name="codigo_per" 
                                                                QueryStringField="id" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <asp:HiddenField ID="hddSw" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddTotal" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddCodigo_Adplla" runat="server" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />
                                                </td>
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
