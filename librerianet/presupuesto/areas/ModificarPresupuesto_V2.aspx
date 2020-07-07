<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModificarPresupuesto_V2.aspx.vb" Inherits="presupuesto_areas_ModificarPresupuesto_V2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>

    <style type="text/css">
        .style1
        {
            font-weight: normal;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
  
                    <table style="width:100%;" border="0" cellpadding="2" cellspacing="0">
                        <tr bgcolor="#F5F9FC">
                <td width="15%" colspan="2">
                    Periodo presupuestal:
                    <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                <td colspan="2" width="15%">
                    Área presupuestal&nbsp;&nbsp;&nbsp; :
                    <asp:DropDownList ID="cboAreaPresu" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                        </tr>
                            <tr>
                <td colspan="2" 
                    style="font-weight: bold; width: 100%; background-color: #DAE4F3; color: #3366CC;">
                    Cabecera de requerimiento<span class="style1"> (Dar clic en uno de los elementos 
                    del listado siguiente)</span></td>
                            </tr>
                        <tr>
                <td colspan="2">
                    <asp:GridView ID="gvCabecera" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" DataKeyNames="codigo_Pto,codigo_ppr,habilitado_Pto" 
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="COD.CCOSTOS" HeaderText="COD.CCOSTOS" 
                                SortExpression="COD.CCOSTOS" />
                            <asp:BoundField DataField="CENTRO COSTOS" HeaderText="CENTRO COSTOS" 
                                SortExpression="CENTRO COSTOS" />
                            <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" 
                                SortExpression="ESTADO" />
                            <asp:BoundField DataField="PROG.PRESUPUESTAL" HeaderText="PROG.PRESUPUESTAL" 
                                SortExpression="PROG.PRESUPUESTAL" />
                            <asp:BoundField DataField="NRO.REQUER." HeaderText="NRO.REQUER." 
                                SortExpression="NRO.REQUER." >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRIORIDAD" HeaderText="PRIORIDAD" 
                                SortExpression="PRIORIDAD" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="INGRESOS (S/.)" HeaderText="INGRESOS (S/.)" 
                                ReadOnly="True" SortExpression="INGRESOS (S/.)" 
                                DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EGRESOS (S/.)" HeaderText="EGRESOS (S/.)" 
                                ReadOnly="True" SortExpression="EGRESOS (S/.)" 
                                DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_Ejp" HeaderText="codigo_Ejp" 
                                SortExpression="codigo_Ejp" Visible="False" />
                            <asp:BoundField DataField="codigo_Cco" HeaderText="codigo_Cco" 
                                SortExpression="codigo_Cco" Visible="False" />
                            <asp:BoundField DataField="codigo_Pto" HeaderText="codigo_Pto" 
                                SortExpression="codigo_Pto" Visible="False" />
                            <asp:BoundField DataField="codigo_Ppr" HeaderText="codigo_Ppr" 
                                SortExpression="codigo_Ppr" Visible="False" />
                            <asp:BoundField DataField="CODIGOPADRE_CCO" HeaderText="CODIGOPADRE_CCO" 
                                SortExpression="CODIGOPADRE_CCO" Visible="False" />
                            <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#FFFFDD" />
                        <HeaderStyle CssClass="TituloTabla" />
                    </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                <td colspan="2">
                    &nbsp;</td>
                        </tr>
                            <tr>
                <td colspan="2" 
                    style="font-weight: bold; width: 100%; background-color: #DAE4F3; color: #3366CC;" 
                                    id="TdDetalle">
                Detalle de requerimiento <span class="style1">(Dar clic en el icono
                    <img alt="" src="../../images/Presupuesto/editar.gif" /> ubicado al final de 
                    cada elemento de la lista)</span></td>
                            </tr>
                        <tr>
                <td colspan="2">
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="PRESU_ConsultarPresupuestoPorUnidadAreaDetalle" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="1" Name="tipo" Type="String" />
                            <asp:ControlParameter ControlID="cboPeriodoPresu" DefaultValue="" 
                                Name="codigo_Ejp" PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="hddCodigo_ppr" Name="codigo_Ppr" 
                                PropertyName="Value" Type="Int32" />
                            <asp:ControlParameter ControlID="hddCodigo_pto" Name="codigo" 
                                PropertyName="Value" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                <td colspan="2">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="PRESU_ConsultarPresupuestoPorUnidadArea_V2" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboPeriodoPresu" Name="codigo_Ejp" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="cboAreaPresu" Name="codigoArea" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                            </td>
                        </tr>
                            </table>
  
                        <asp:HiddenField ID="hddCodigo_pto" runat="server" />
                        <asp:HiddenField ID="hddCodigo_ppr" runat="server" />
  
                    <asp:GridView ID="gvDetalle" runat="server" DataSourceID="SqlDataSource2" 
                        Width="100%" AutoGenerateColumns="False" DataKeyNames="codigo_dpr">
                        <Columns>
                            <asp:BoundField DataField="TIPO" HeaderText="TIPO" ReadOnly="True" 
                                SortExpression="TIPO" />
                            <asp:BoundField DataField="CLASE" HeaderText="CLASE" SortExpression="CLASE" />
                            <asp:BoundField DataField="COD.ITEM" HeaderText="COD.ITEM" 
                                SortExpression="COD.ITEM" />
                            <asp:BoundField DataField="DES.ESTANDAR" HeaderText="DES.ESTANDAR" 
                                ReadOnly="True" SortExpression="DES.ESTANDAR" />
                            <asp:BoundField DataField="DET.DESCRIPCION" HeaderText="DET.DESCRIPCION" 
                                SortExpression="DET.DESCRIPCION" />
                            <asp:BoundField DataField="SUBPRIORIDAD" HeaderText="SUBPRIORIDAD" 
                                SortExpression="SUBPRIORIDAD" />
                            <asp:BoundField DataField="UNIDAD" HeaderText="UNIDAD" 
                                SortExpression="UNIDAD" />
                            <asp:BoundField DataField="CANTIDAD" HeaderText="CANTIDAD" 
                                SortExpression="CANTIDAD" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PREC.UNIT." HeaderText="PREC.UNIT." 
                                SortExpression="PREC.UNIT." DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SUBTOTAL" HeaderText="SUBTOTAL" ReadOnly="True" 
                                SortExpression="SUBTOTAL" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ENE (S/.)" HeaderText="ENE (S/.)" 
                                SortExpression="ENE (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FEB (S/.)" HeaderText="FEB (S/.)" 
                                SortExpression="FEB (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAR (S/.)" HeaderText="MAR (S/.)" 
                                SortExpression="MAR (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ABR (S/.)" HeaderText="ABR (S/.)" 
                                SortExpression="ABR (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAY (S/.)" HeaderText="MAY (S/.)" 
                                SortExpression="MAY (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JUN (S/.)" HeaderText="JUN (S/.)" 
                                SortExpression="JUN (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JUL (S/.)" HeaderText="JUL (S/.)" 
                                SortExpression="JUL (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AGO (S/.)" HeaderText="AGO (S/.)" 
                                SortExpression="AGO (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SET (S/.)" HeaderText="SET (S/.)" 
                                SortExpression="SET (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OCT (S/.)" HeaderText="OCT (S/.)" 
                                SortExpression="OCT (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NOV (S/.)" HeaderText="NOV (S/.)" 
                                SortExpression="NOV (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DIC (S/.)" HeaderText="DIC (S/.)" 
                                SortExpression="DIC (S/.)" DataFormatString="{0:#,###,##0.00}" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TIPOCON" HeaderText="TIPOCON" ReadOnly="True" 
                                SortExpression="TIPOCON" Visible="False" />
                            <asp:BoundField DataField="CodConcepto" HeaderText="CodConcepto" 
                                SortExpression="CodConcepto" Visible="False" />
                            <asp:BoundField DataField="IdUni" HeaderText="IdUni" SortExpression="IdUni" 
                                Visible="False" />
                            <asp:CheckBoxField DataField="indicoCantidades" HeaderText="indicoCantidades" 
                                SortExpression="indicoCantidades" Visible="False" />
                            <asp:BoundField DataField="codigo_Dpr" HeaderText="codigo_Dpr" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_Dpr" 
                                Visible="False" />
                            <asp:CommandField ButtonType="Image" 
                                DeleteText="" 
                                EditImageUrl="~/images/Presupuesto/editar.gif" 
                                ShowEditButton="True" SelectText="" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:CommandField ButtonType="Image" 
                                DeleteImageUrl="~/images/Presupuesto/eliminar.gif" ShowDeleteButton="True" />
                            <asp:TemplateField>
                            <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return confirm('¿Desea eliminar el registro?');" ImageUrl="~/images/Presupuesto/eliminar.gif" CommandName="Delete" />
                            </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="TituloTabla" />
                    </asp:GridView>
  
                        <asp:HiddenField ID="hddHabilitado" runat="server" />
  
    </form>
</body>
</html>
