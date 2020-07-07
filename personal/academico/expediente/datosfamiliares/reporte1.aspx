<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporte1.aspx.vb" Inherits="datosfamiliares_reporte1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        <link href="private/estilos.css" rel="stylesheet"  type="text/css" />
        .style1
        {
            width: 95%;
        }
        .style3
        {
            width: 67px;
        }
        .style4
        {
            width: 426px;
        }
        .style5
        {
            width: 109px;
        }
        .style6
        {
            width: 67px;
            height: 48px;
        }
        .style7
        {
            width: 426px;
            height: 48px;
        }
        .style8
        {
            width: 109px;
            height: 48px;
        }
        .style9
        {
            height: 48px;
        }
        .style13
        {
            height: 23px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="style1">
        <tr>
            <td style="font-size: small; font-family: Arial, Helvetica, sans-serif; font-weight: bold;">
                Reporte de Familiares por área</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="font-family: Arial, Helvetica, sans-serif; font-size: small; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000080;" 
                    width="100%">
                    <tr>
                        <td class="style3">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Área</td>
                        <td class="style4">
                            <asp:DropDownList ID="ddlArea" runat="server" DataSourceID="SqlDataSource2" 
                                DataTextField="descripcion_cco" DataValueField="codigo_cco" Width="300px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                SelectCommand="FAM_ReporteFamilia" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="AR" Name="tipo" Type="String" />
                                    <asp:Parameter DefaultValue="0" Name="codigo_cco" Type="Int32" />
                                    <asp:Parameter DefaultValue="0" Name="esUSat" Type="Int32" />
                                    <asp:Parameter DefaultValue="0" Name="mesMatri" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td class="style5">
                            Mes Matrimonio</td>
                        <td>
                            <asp:DropDownList ID="ddlMes" runat="server" Width="180px">
                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Es usat</td>
                        <td class="style4">
                            <asp:DropDownList ID="ddlUsat" runat="server" Height="19px" Width="180px">
                                <asp:ListItem Value="0">TODOS</asp:ListItem>
                                <asp:ListItem Value="1">SI</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style5">
                            Ordenar por</td>
                        <td>
                            <asp:DropDownList ID="ddlOrden" runat="server" Height="19px" Width="179px">
                                <asp:ListItem Value="PE">Personal</asp:ListItem>
                                <asp:ListItem Value="CO">Conyuge</asp:ListItem>
                                <asp:ListItem Value="FE">Fecha por año</asp:ListItem>
                                <asp:ListItem Value="DI">Fecha por día</asp:ListItem>
                                <asp:ListItem Value="FR">Fecha Registro</asp:ListItem>
                                <asp:ListItem Value="SE">Sexo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            Sexo</td>
                        <td class="style7">
                            <asp:DropDownList ID="ddlSexo" runat="server" Height="19px" Width="180px">
                                <asp:ListItem Value="0">TODOS</asp:ListItem>
                                <asp:ListItem Value="F">Femenino</asp:ListItem>
                                <asp:ListItem Value="M">Masculino</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style8">
                            <asp:Button ID="txtConsultar" runat="server" Text="Consultar" />
                        </td>
                        <td class="style9">
                            <asp:Button ID="cmdExportar" runat="server" Text="Exportar" Height="26px" />
                        </td>
                    </tr>
                </table>
    
        <asp:DataList ID="dlPersonal" runat="server" DataSourceID="SqlDataSource1" 
                    EnableViewState="False">
            <ItemTemplate>
                <table style="font-family: Verdana; font-size: 10px; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000080;" 
                    width="1180">
                    <tr>
                        <td valign="top" width="20">
                            <asp:Label ID="lblCodPer" runat="server" Text='<%# Eval("codigo_per") %>' 
                                Visible="False"></asp:Label>
                        </td>
                        <td valign="top" width="200">
                            <asp:Label ID="responsable_CcoLabel" runat="server" 
                                Text='<%# Eval("responsable_Cco") %>' Font-Bold="True" />
                            <br />
                            Telf.
                            <asp:Label ID="telefono_per" runat="server" Font-Bold="True" 
                                Text='<%# Eval("telefono_per") %>' />
                        </td>
                        <td valign="top" class="style10" width="120">
                            <asp:Label ID="sexo_per" runat="server" 
                                Text='<%# iif(Eval("sexo_per")="M","Masculino","Femenino") %>' />
                        </td>
                        <td class="style10" valign="top">
                            <asp:Label ID="ConyugeLabel" runat="server" Text='<%# Eval("Conyuge") %>' />
                        </td>
                        <td valign="top" width="80" align="center">
                            <asp:Label ID="FechaMatLabel" runat="server" Text='<%# Eval("FechaMat") %>' />
                        </td>
                        <td align="center" valign="top" width="80">
                            <asp:Label ID="FechaMatLabel0" runat="server" Text='<%# Eval("anios") %>' />
                        </td>
                        <td valign="top" width="200">
                            <asp:DataList ID="DataList2" runat="server" DataSourceID="SqlDataSource2" 
                                EnableViewState="False">
                                <ItemTemplate>
                                    -
                                    <asp:Label ID="descripcion_CcoLabel" runat="server" 
                                        Text='<%# Eval("descripcion_Cco") %>' />
                                    <br />
                                    <br />
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                SelectCommand="SELECT dbo.CentroCostos.descripcion_Cco FROM dbo.PersonalCentroCostos INNER JOIN dbo.CentroCostos ON dbo.PersonalCentroCostos.codigo_Cco = dbo.CentroCostos.codigo_Cco WHERE (dbo.PersonalCentroCostos.estado_Pcc = '1') AND (dbo.PersonalCentroCostos.codigo_Per = @param1)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="lblCodPer" Name="param1" PropertyName="Text" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td align="center" class="style1" valign="top" width="100">
                            <asp:Label ID="TipoLabel" runat="server" Text='<%# Eval("Tipo") %>' />
                        </td>
                        <td valign="top" width="200">
                            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" 
                                EnableViewState="False">
                                <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblHijo" runat="server" Font-Bold="True" 
                                                    Text='<%# Eval("hijo") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="70">
                                                <asp:Label ID="lblSexo" runat="server" Text='<%# Eval("sexo") %>' />
                                            </td>
                                            <td align="center" width="10">
                                                |</td>
                                            <td width="120">
                                                <asp:Label ID="lblfechaNac" runat="server" Text='<%# Eval("FechaNac") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style14">
                                                <asp:Label ID="lblEstudios" runat="server" Text='<%# Eval("estudios") %>' />
                                            </td>
                                            <td align="center" width="10">
                                                |</td>
                                            <td>
                                                <asp:Label ID="lblusat" runat="server" Text='<%# Eval("usat") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                SelectCommand="FAM_ReporteFamilia" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="HI" Name="tipo" Type="String" />
                                    <asp:ControlParameter ControlID="lblCodPer" DefaultValue="" Name="codigo_cco" 
                                        PropertyName="Text" Type="Int32" />
                                    <asp:Parameter DefaultValue="0" Name="esUSat" Type="Int32" />
                                    <asp:Parameter DefaultValue="0" Name="mesMatri" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td valign="top" width="100">
                            <asp:Label ID="TipoLabel0" runat="server" 
                                Text='<%# Eval("ultimaActualizacion_dhab") %>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
            <HeaderTemplate>
                <table style="font-family: Verdana; font-size: 11px; color: #0033CC; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000080;">
                    <tr style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000080" 
                        align="center">
                        <td width="20">
                            &nbsp;</td>
                        <td width="200">
                            Personal</td>
                        <td width="120">
                            Sexo</td>
                        <td width="200">
                            Conyuge</td>
                        <td width="80">
                            Fecha</td>
                        <td width="80">
                            Años</td>
                        <td width="200">
                            Área</td>
                        <td class="style1" width="100">
                            Es USAT</td>
                        <td width="200">
                            Hijos</td>
                        <td width="100">
                            Registro</td>
                    </tr>
                </table>
            </HeaderTemplate>
        </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
            SelectCommand="FAM_ReporteFamiliaFiltros" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter DefaultValue="PE" Name="tipo" Type="String" />
                <asp:Parameter DefaultValue="0" Name="codigo_cco" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="esUSat" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="mesMatri" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="sexo" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
            </td>
        </tr>
        <tr>
            <td class="style13">
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
