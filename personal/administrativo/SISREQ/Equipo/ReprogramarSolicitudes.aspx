
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReprogramarSolicitudes.aspx.vb" Inherits="Equipo_ReprogramarSolicitudes" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href ="../private/estilo.css" rel="stylesheet" type ="text/css" />
    <link href ="../private/estiloweb.css" rel="stylesheet" type ="text/css" />
    <script language ="javascript" type="text/javascript" src ="../private/funciones.js"></script>
    <script language ="javascript" type="text/javascript" src ="../private/funcion.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td rowspan="1" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;<strong> Buscar por:</strong>
                                            <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" Style="text-transform: capitalize">
                                                <asp:ListItem Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="1">&#193;rea</asp:ListItem>
                                                <asp:ListItem Value="2">Tipo Solicitud</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="cboValor" runat="server" AutoPostBack="True" Style="text-transform: capitalize"
                                                Visible="False">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="50px">
                                                        Ver:
                                                    </td>
                                                    <td>
                                            <asp:RadioButtonList ID="RblVer" runat="server" AutoPostBack="True" 
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Iniciadas</asp:ListItem>
                                                <asp:ListItem Value="2">No Iniciadas</asp:ListItem>
                                                <asp:ListItem Value="3">Vencidas</asp:ListItem>
                                            </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 1px; background-color: #004182;">
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 13px;">
                                &nbsp;&nbsp;
                                <img alt="" src="../images/CUADRO_VERDE.jpg" /> : Asignada a un grupo
                                <img alt="" src="../images/cuadro_azul.jpg" /> : Asignada a una persona&nbsp;</td>
                            <td style="height: 13px;" align="right">
                                <asp:Label ID="LblRegistros" runat="server" Text="Total de Registros: 0" 
                                    ForeColor="#000099"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top" style="text-align: center;">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td id="cell1" align="center">
                                            <asp:LinkButton ID="lbIniciadas" runat="server" BorderColor="Silver" BorderWidth="0px"
                                                EnableTheming="False" Font-Underline="False" Height="20px" Width="200px" 
                                                BackColor="LemonChiffon" ForeColor="Black" Font-Bold="False" Visible="False" >             Iniciadas</asp:LinkButton></td>
                                        <td id="cell2" align="center" >
                                            <asp:LinkButton ID="lbNoIniciadas" runat="server" BorderColor="Silver" BorderWidth="0px"
                                                EnableTheming="False" Font-Underline="False" Height="20px" Width="200px" 
                                                BackColor="LemonChiffon" ForeColor="Black" CssClass="bordesSID" Visible="False">              No Iniciadas</asp:LinkButton>
                                        </td>
                                        <td id="cell3" align="center" >
                                            <asp:LinkButton ID="lbVencidas" runat="server" BorderColor="Silver" BorderWidth="0px"
                                                EnableTheming="False" Font-Underline="False" Height="20px" Width="200px" 
                                                BackColor="LemonChiffon" ForeColor="Black" Visible="False">                Vencidas</asp:LinkButton>
                                        </td>
                                        <td style="width: 33%; height: 14px; border-bottom: dimgray thin solid; visibility: hidden;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" 
                                            rowspan="" height="400" valign="top">
                                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Vertical" Width="100%" 
                                                HorizontalAlign="Center">
                                <asp:GridView ID="GvSolicitudes" runat="server" AllowSorting="True"
                                    AutoGenerateColumns="False" DataSourceID="ObjDatos" Width="98%" 
                                        CellPadding="4" GridLines="Horizontal" PageSize="8">
                                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="id_sol" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_tsol" HeaderText="id_tsol" SortExpression="id_tsol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo Solicitud" 
                                            SortExpression="descripcion_tsol" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="prioridad">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" SortExpression="codigo_cco"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" SortExpression="descripcion_cco" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha"
                                            HtmlEncode="False" SortExpression="fecha_sol">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vigente_solest" HeaderText="vigente_solest" SortExpression="vigente_solest"
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_apl" HeaderText="codigo_apl" SortExpression="codigo_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" 
                                            Visible="False" >
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Font-Underline="False" HorizontalAlign="Center" Width="10px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_solequ" HeaderText="id_solequ" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id_solequ" Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField HeaderText="Reprog." >
                                            <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" />
                                        </asp:HyperLinkField>
                                        <asp:HyperLinkField HeaderText="Admin." >
                                            <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle Height="40px" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        No se encontraron registros
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle Height="20px" Font-Bold="True" ForeColor="White" 
                                        CssClass="TituloReq" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td rowspan="1" valign="top" style="height: 8px; background-color: #004182">
                    </td>
            </tr>
            <tr>
                <td rowspan="1" valign="top" id="CelDatos">
                    <iframe id="ifSolicitud" frameborder="0" name="ifSolicitud" src="DatosSolicitud.aspx"
                        style="height: 242px" width="100%"></iframe>
                </td>
            </tr>
            <tr>
                <td rowspan="1" style="height: 1px; background-color: #004182;" valign="top">
                </td>
            </tr>
        </table>
    
    </div>
                                    <asp:ObjectDataSource ID="ObjDatos" 
        runat="server" SelectMethod="ObtieneSolicitudesAReprogramar"
                                        TypeName="clsRequerimientos">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="cod_per" QueryStringField="id" Type="Int32" />
                                            <asp:ControlParameter ControlID="RblVer" DefaultValue="1" Name="tipo" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                            <asp:ControlParameter ControlID="CboCampo" DefaultValue="0" Name="campo" PropertyName="SelectedValue"
                                                Type="Int16" />
                                            <asp:ControlParameter ControlID="cboValor" DefaultValue="0" Name="valor" PropertyName="SelectedValue"
                                                Type="Int16" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
    </form>
</body>
</html>
