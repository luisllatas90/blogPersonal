<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="frmConsultarPlanillaQuintaArea.aspx.vb" Inherits="planillaQuinta_frmConsultarPlanillaQuintaArea" %>

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
                                    
                                    <td  valign="top" id="tdHistorico" >
                                        <asp:Panel ID="pnlHistorico" runat="server" Height="200px" width="100%" BorderWidth="1px">
                                            <table id="tblMnuPrincipal" width="100%" >
                                                <tr>
                                                    <td >
                                                        &nbsp;» Historial<asp:GridView ID="GridView1" runat="server" 
                                                            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" ShowFooter="True" 
                                                            Width="95%">
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
                                                    </td>
                                                    <td width="3%"align="right" height="8%" style="cursor:hand" class="bordeinf" >
                                                        <img alt="" src="../../../images/maximiza.gif" 
                                                            onclick="Maximizar(this,'../../../','100%','65%')" /></td>
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
                                    <td colspan="2" style="margin-left: 80px">
                                        <table  style="width:100%;">
                                            
                                            <tr>
                                                <td colspan="2">
                                                    <asp:GridView ID="gvDetalleQuinta" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                        DataKeyNames="codigo_Adplla,codigo_ejp,codigo_plla,codigo_per,codigo_cco,codigo_Fqta,codigo_per_registra" 
                                                         Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="Trabajador" HeaderText="Trabajador" ReadOnly="True" 
                                                                SortExpression="Trabajador" />
                                                                
                                                            <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de costos" 
                                                                SortExpression="descripcion_Cco" Visible="True" />
                                                            
                                                            <asp:BoundField DataField="descripcion_Fqta" HeaderText="Función" 
                                                                SortExpression="descripcion_Fqta" />
                                                            <asp:BoundField DataField="importe_adplla" HeaderText="Importe" 
                                                                SortExpression="importe_adplla">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="observacion_adplla" HeaderText="Observacion" 
                                                                SortExpression="observacion_adplla" />
                                                            <asp:BoundField DataField="Estado" HeaderText="Instancia" ReadOnly="True" 
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
                                            <tr>
                                                <td colspan="2">
                                                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
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
                                                    </asp:SqlDataSource>--%>
                                                    <asp:HiddenField ID="hddSw" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddAntes" runat="server" Value="0" />
                                                    <asp:HiddenField ID="hddActual" runat="server" Value="0" />
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
