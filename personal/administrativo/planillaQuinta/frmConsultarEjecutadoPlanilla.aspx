<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultarEjecutadoPlanilla.aspx.vb" Inherits="planillaQuinta_frmConsultarEjecutadoPlanilla" %>

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
    
        <table style="width:100%;" class="contornotabla">
            <tr>
                <td>
                    Periodo Presupuestal:
                    <asp:DropDownList ID="cboPeriodoPresu" runat="server">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="cmdExportar" runat="server" Text="Exportar A Excel" 
                        Width="130px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblNroRegistros" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="UnidadGestion" HeaderText="Unidad de Gestión" 
                                SortExpression="UnidadGestion" />
                            <asp:BoundField DataField="AreaPresupuestal" HeaderText="Area Presupuestal" 
                                SortExpression="AreaPresupuestal" />
                            <asp:BoundField DataField="descripcion_Cplla" HeaderText="Concepto Planilla" 
                                SortExpression="descripcion_Cplla" />
                            <asp:BoundField DataField="TotPre" DataFormatString="{0:#,###,##0.00}" 
                                HeaderText="Total Presupuestado" ReadOnly="True" SortExpression="TotPre">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TotEje" DataFormatString="{0:#,###,##0.00}" 
                                HeaderText="Total Ejecutado" ReadOnly="True" SortExpression="TotEje">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#FF9900" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="PRESU_ConsultarEjecutadoPlanillaHastaElMomento" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboPeriodoPresu" Name="codigo_Ejp" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
