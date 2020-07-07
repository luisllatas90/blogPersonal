<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ingresantes2.aspx.cs" Inherits="Ingresantes2"  %>
<%@ Register Assembly="DundasWebOlapManager" Namespace="Dundas.Olap.Manager" TagPrefix="DOMC" %>
<%@ Register Assembly="DundasWebOlapDataProviderAdomdNet" Namespace="Dundas.Olap.Data.AdomdNet"  TagPrefix="DODPN" %>
<%@ Register Assembly="DundasWebUIControls" Namespace="Dundas.Olap.WebUIControls"    TagPrefix="DOCWC" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <script type="text/javascript" src="JScript.js"></script>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div style="font-family: Tahoma;">
            <DOCWC:OlapToolbar ID="OlapToolbar1" runat="server" Width="731px"
                OnClick="OlapToolbar1_Click" OlapManagerID="OlapManager1">
            </DOCWC:OlapToolbar>
            <asp:Panel ID="PanelChartGrid" runat="server" Width="740px" Style="margin-top: 4px;
                text-align: left; font-family: Tahoma;" Height="524px">
                <asp:Panel ID="Panel1" runat="server" Height="25px" Width="196px" Style="white-space: nowrap;
                    position: relative; left: 4px;">
                    <asp:Panel ID="PanelTabChart" runat="server" Width="74px" CssClass="ppbtn_selected"
                        Style="left: 79px; padding-top: 4px; position: absolute; top: 0px;" onmouseover="tabControlOver(this);"
                        onmouseout="tabControlOut(this);">
                        &nbsp;<img src="../../Images/icon-ChartOLAP.gif" alt="" />&nbsp;&nbsp; Grafico</asp:Panel>
                    <asp:Panel ID="PanelTabGrid" runat="server" Width="78px" CssClass="ppbtn_normal"
                        Style="left: 0px; padding-top: 4px; position: absolute; top: 0px;" onmouseover="tabControlOver(this);"
                        onmouseout="tabControlOut(this);">
                        &nbsp;<img src="../../Images/icon-OLAPGrid.gif" alt="" />&nbsp;&nbsp; Tabular</asp:Panel>
                    <asp:Panel ID="PanelTabBoth" runat="server" Width="70px" CssClass="ppbtn_normal"
                        Style="left: 201px; padding-top: 4px; position: absolute; top: 1px;" onmouseover="tabControlOver(this);"
                        onmouseout="tabControlOut(this);" Visible="False">
                        &nbsp;<img src="../../Images/olap-sidebyside.gif" alt="" />&nbsp;&nbsp;
                        <span style="position: relative;top: -3px;">Split</span></asp:Panel>
                </asp:Panel>
                <asp:Panel ID="PanelChartGridFrame" runat="server" Width="730px" BorderColor="gainsboro"
                    BorderStyle="Solid" BorderWidth="1px" Style="padding: 4px;" Height="485px">
                    <table cellpadding="0" cellspacing="0" style="height: 100%">
                        <tr>
                            <td valign="top">
                                <DOCWC:OlapChart ID="OlapChart1" runat="server" Style="display: block;" OnInit="OlapChart1_Init"
                                    OlapManagerID="OlapManager1" Height="480px">
                                    <Legends>
                                        <DOCWC:Legend Alignment="Center" Docking="Bottom" Enabled="False" LegendStyle="Column"
                                            Name="Default">
                                        </DOCWC:Legend>
                                    </Legends>
                                    <ChartAreas>
                                        <DOCWC:ChartArea Name="Default">
                                        </DOCWC:ChartArea>
                                    </ChartAreas>
                                    <ToolbarSettings>
                                        <SelectChartType Visible="False" Description="Select Chart Type" />
                                        <ToggleLegend Description="Mostrar o No Leyenda" />
                                        <Print Description="Imprimir Grafico" />
                                        <SelectPalette Description="Seleccionar Color" Visible="False" />
                                        <PrintPreview Description="Vista Previa del Gr&#225;fico" />
                                        <Properties Description="Propiedades" Visible="False" />
                                        <SaveImage Description="Grabar Grafico en Imagen" />
                                        <Copy Description="Copiar Grafico" />
                                    </ToolbarSettings>
                                    <Series>
                                        <DOCWC:Series Name="Default" ShowLabelAsValue="True">
                                        </DOCWC:Series>
                                    </Series>
                                </DOCWC:OlapChart>
                            </td>
                            <td valign="top">
                                <DOCWC:OlapGrid ID="OlapGrid1" runat="server" Height="480px" Width="730px" OlapManagerID="OlapManager1"
                                    TitleFont="Tahoma, 12pt"
                                    TitleBackColor="WhiteSmoke" TitleColor="Black" TitleText="Ingresantes por Semestre Academico y Modalidad de Ingreso segun Escuela Profesional" TitleVisible="False" CellBorderWidth="1px" BorderWidth="0px" AddContextMenu="False" SubTotalsVisible="False" >
                                    <TitlePadding Bottom="10" Top="8" />
                                    <ToolbarSettings>
                                        <Copy Description="Copiar Tabla" />
                                        <Print Description="Imprimir Tabla" />
                                        <PrintPreview Description="Vista Previa de Tabla" />
                                        <Orientation Description="Cambiar Orientacion de Tabla" />
                                        <ShowTotal Description="Ver Totales en Grupo de Tabla" />
                                        <SelectAppearance Description="Apariencia de Tabla" />
                                    </ToolbarSettings>
                                    <RowsHeaderStyle BackColor="#ECE9D8" BorderColor="Black" ForeColor="Black">
                                    </RowsHeaderStyle>
                                    <TotalsStyle BorderColor="Black" ForeColor="Black">
                                        <Font Bold="True" Names="Tahoma" Size="9pt"></Font>
                                    </TotalsStyle>
                                    <DataAreaStyle BorderColor="Transparent" ForeColor="Black">
                                    </DataAreaStyle>
                                    <ColumnsHeaderStyle BackColor="#990000" BorderColor="Black">
                                    </ColumnsHeaderStyle>
                                    <InterlacedRowsStyle BackColor="#DECE9C" BorderColor="White" BorderStyle="None" ForeColor="Black">
                                    </InterlacedRowsStyle>
                                    <Padding Left="11" Right="11" />
                                </DOCWC:OlapGrid>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="HiddenFieldChartGridState" runat="server" Value="Grid" />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="PanelAxes" runat="server" Width="740px" Style="margin-top: 4px; position: relative;
                text-align: left; font-family: Tahoma;">
                <asp:Panel ID="PanelAxesBuilderTabs" runat="server" Height="25px" Width="274px" Style="white-space: nowrap;
                    position: relative; left: 3px; top: 0px;">
                    <asp:Panel ID="PanelTabCategories" runat="server" Width="70px" CssClass="ppbtn_selected"
                        Style="left: 0px; padding-top: 4px; position: absolute; top: 0px;" onmouseover="tabControlOver(this);"
                        onmouseout="tabControlOut(this);">
                        &nbsp;&nbsp;Modalidad</asp:Panel>
                    <asp:Panel ID="PanelTabSeries" runat="server" Width="110px" CssClass="ppbtn_normal"
                        Style="left: 71px; padding-top: 4px; position: absolute; top: 0px;" onmouseover="tabControlOver(this);"
                        onmouseout="tabControlOut(this);" Height="12px">
                        &nbsp;&nbsp;Semestre Ingreso</asp:Panel>
                    <asp:Panel ID="PanelTabSlicer" runat="server" Width="70px" CssClass="ppbtn_normal"
                        Style="left: 182px; padding-top: 4px; position: absolute; top: 0px;" onmouseover="tabControlOver(this);"
                        onmouseout="tabControlOut(this);">
                        &nbsp; Escuelas</asp:Panel>
                </asp:Panel>
                <asp:Panel ID="PanelAxesBuilders" runat="server" Width="730px" Height="32px" BorderColor="gainsboro"
                    BorderStyle="Solid" BorderWidth="1px" Style="padding: 4px;">
                    <input id="ButtonAddDimension" runat="server" style="height: 26px; margin-top: 3px;
                        float: left;" type="button" value="Add Dimension" visible="false" />
                    <DOCWC:AxisBuilder ID="AxisBuilderCategorical" runat="server" AxisType="Categorical"
                        Height="32px" OlapManagerID="OlapManager1" Width="620px" BackColor="GhostWhite"
                        DialogDirection="UpRight" Style="float: right;" EmptyBuilderMessage=" ">
                    </DOCWC:AxisBuilder>
                    <DOCWC:AxisBuilder ID="AxisBuilderSeries" runat="server" AxisType="Series" Height="32px"
                        OlapManagerID="OlapManager1" Width="620px" BackColor="GhostWhite" DialogDirection="UpRight"
                        Visible="false" Style="float: right;" EmptyBuilderMessage=" ">
                    </DOCWC:AxisBuilder>
                    <DOCWC:AxisBuilder ID="AxisBuilderSlicer" runat="server" AxisType="Slicer" Height="32px"
                        OlapManagerID="OlapManager1" Width="620px" BackColor="GhostWhite" DialogDirection="UpRight"
                        Visible="false" Style="float: right;" EmptyBuilderMessage=" ">
                    </DOCWC:AxisBuilder>
                    <asp:HiddenField ID="HiddenFieldAxisType" runat="server" Value="Categorical" />
                </asp:Panel>
            </asp:Panel>
            <br />
        </div>
    </center>
    <DOMC:OlapManager ID="OlapManager1" runat="server" DataProviderID="AdomdNetDataProvider1"
        OnCommand="OlapManager1_Command">
        <ToolbarSettings>
            <Transpose Description="Pivots Data" Visible="False" />
        </ToolbarSettings>
    </DOMC:OlapManager>
    <DODPN:AdomdNetDataProvider ID="AdomdNetDataProvider1" runat="server">
    </DODPN:AdomdNetDataProvider>
    </form>
</body>
</html>