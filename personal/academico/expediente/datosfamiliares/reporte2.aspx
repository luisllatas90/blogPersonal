<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporte2.aspx.vb" Inherits="datosfamiliares_reporte2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">

        .style3
        {
            width: 82px;
        }
        .style4
        {
            width: 72px;
        }
        .style5
        {
            width: 109px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" class="style1">
        <tr>
            <td style="font-size: small; font-family: Arial, Helvetica, sans-serif; font-weight: bold;">
                Reporte de Hijos por Rango de Edades</td>
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
                            Desde</td>
                        <td class="style4">
                            <asp:DropDownList ID="ddlDesde" runat="server" Height="33px" Width="62px">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem Value="41"></asp:ListItem>
                                <asp:ListItem Value="42"></asp:ListItem>
                                <asp:ListItem Value="43"></asp:ListItem>
                                <asp:ListItem Value="44"></asp:ListItem>
                                <asp:ListItem Value="45"></asp:ListItem>
                                <asp:ListItem Value="46"></asp:ListItem>
                                <asp:ListItem Value="47"></asp:ListItem>
                                <asp:ListItem Value="48"></asp:ListItem>
                                <asp:ListItem Value="49"></asp:ListItem>
                                <asp:ListItem Value="50"></asp:ListItem>
                                <asp:ListItem Value="51"></asp:ListItem>
                                <asp:ListItem Value="52"></asp:ListItem>
                                <asp:ListItem Value="53"></asp:ListItem>
                                <asp:ListItem Value="54"></asp:ListItem>
                                <asp:ListItem Value="55"></asp:ListItem>
                            </asp:DropDownList>
&nbsp;</td>
                        <td class="style5">
                            años</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Hasta</td>
                        <td class="style4">
                            <asp:DropDownList ID="ddlHasta" runat="server" Height="33px" Width="62px">
                                <asp:ListItem Value="0">0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem Value="41"></asp:ListItem>
                                <asp:ListItem Value="42"></asp:ListItem>
                                <asp:ListItem Value="43"></asp:ListItem>
                                <asp:ListItem Value="44"></asp:ListItem>
                                <asp:ListItem Value="45"></asp:ListItem>
                                <asp:ListItem Value="46"></asp:ListItem>
                                <asp:ListItem Value="47"></asp:ListItem>
                                <asp:ListItem Value="48"></asp:ListItem>
                                <asp:ListItem Value="49"></asp:ListItem>
                                <asp:ListItem Value="50"></asp:ListItem>
                                <asp:ListItem Value="51"></asp:ListItem>
                                <asp:ListItem Value="52"></asp:ListItem>
                                <asp:ListItem Value="53"></asp:ListItem>
                                <asp:ListItem Value="54"></asp:ListItem>
                                <asp:ListItem Value="55"></asp:ListItem>
                            </asp:DropDownList>
&nbsp;</td>
                        <td class="style5">
                            años</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Ordenar por</td>
                        <td class="style4">
                            <asp:DropDownList ID="ddlOrden" runat="server" Height="19px" Width="179px" 
                                AutoPostBack="True">
                                <asp:ListItem Value="HJ">HIJO</asp:ListItem>
                                <asp:ListItem Value="SE">SEXO</asp:ListItem>
                                <asp:ListItem Value="NA">FECHA NACIMIENTO</asp:ListItem>
                                <asp:ListItem Value="ED">EDAD</asp:ListItem>
                                <asp:ListItem Value="PA">PADRE</asp:ListItem>
                                <asp:ListItem Value="RE">AREA</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style5">
                            &nbsp;</td>
                        <td align="center">
                            <asp:Button ID="txtConsultar" runat="server" Text="Consultar" />
                        &nbsp;&nbsp;
                            <asp:Button ID="cmdExportar" runat="server" Text="Exportar" />
                        </td>
                    </tr>
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
                    </table>
    
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1">
                    <ItemTemplate>
                        <table style="width: 100%; font-family: Verdana; font-size: 10px;">
                            <tr>
                                <td width="200">
                                    <asp:Label ID="Hijo" runat="server" Text='<%# Eval("Hijo") %>' />
                                </td>
                                <td width="100px">
                                    <asp:Label ID="Sexo" runat="server" Text='<%# Eval("sexo") %>' />
                                </td>
                                <td width="80">
                                    <asp:Label ID="fecha" runat="server" Text='<%# Eval("Fecha") %>' />
                                </td>
                                <td width="80" align="center">
                                    <asp:Label ID="edad" runat="server" Text='<%# Eval("edad") %>' />
                                </td>
                                <td width="200">
                                    <asp:Label ID="padre" runat="server" Text='<%# Eval("padre") %>' />
                                </td>
                                <td width="150">
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
                                            <asp:ControlParameter ControlID="area" Name="param1" PropertyName="Text" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:Label ID="area" runat="server" Text='<%# Eval("area") %>' 
                                        Visible="False" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <table style="width: 100%; font-family: Verdana; font-size: 10px; font-weight: bold; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000080;">
                            <tr>
                                <td width="200">
                                    Hijo</td>
                                <td width="100">
                                    Sexo</td>
                                <td width="80">
                                    Fecha Nac.</td>
                                <td align="center" width="80">
                                    Edad</td>
                                <td width="200">
                                    Padre</td>
                                <td width="150">
                                    Área</td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                </asp:DataList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                    SelectCommand="FAM_ReporteFamilia" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlOrden" DefaultValue="HJ" Name="tipo" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:Parameter DefaultValue="0" Name="codigo_cco" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlDesde" DefaultValue="0" Name="esUSat" 
                            PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlHasta" DefaultValue="0" Name="mesMatri" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
    
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
