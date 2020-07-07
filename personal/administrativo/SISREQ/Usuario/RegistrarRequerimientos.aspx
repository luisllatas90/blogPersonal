<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistrarRequerimientos.aspx.vb" Inherits="SolicitudRequerimientos" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href ="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href ="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <title>Página sin título</title>
</head>
<body style="margin-top:0">
    <form id="frmRegRequerimientos" runat="server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style="text-align: center; width: 95%;">
                    <table width="100%">
                        <tr>
                            <td 
                                
                                style="height: 20px; text-align: center; text-transform: uppercase; font-weight: bold;">
                                Registrar Requerimientos de una solicitud</td>
                        </tr>
                        <tr>
                            <td>
                                <b>Ver:</b>
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Nuevo M&#243;dulo</asp:ListItem>
                                    <asp:ListItem Value="2">Por Módulo</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                                <asp:DropDownList ID="CboValor" runat="server" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 1px; background-color: #004182;">
                            </td>
                        </tr>
                        <tr>
                            <td height="15">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 167px" valign="top">
                                <asp:GridView ID="GvSolicitudes" runat="server" 
                                    AllowSorting="True" AutoGenerateColumns="False" 
                                    PageSize="12" Width="100%" GridLines="Horizontal">
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="id_sol" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol" >
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_tsol" HeaderText="id_tsol" SortExpression="id_tsol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol" >
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" 
                                            SortExpression="prioridad" Visible="False" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" SortExpression="codigo_cco" Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" SortExpression="descripcion_cco" >
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha" SortExpression="fecha_sol" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="False" >
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vigente_solest" HeaderText="vigente_solest" SortExpression="vigente_solest" Visible="False" />
                                        <asp:BoundField DataField="codigo_apl" HeaderText="codigo_apl" SortExpression="codigo_apl" Visible="False" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" 
                                            SortExpression="descripcion_apl" >
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_est" HeaderText="id_est" SortExpression="id_est" Visible="False" />
                                        <asp:BoundField DataField="id_solequ" HeaderText="id_solequ" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id_solequ" Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est" >
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField HeaderText="Ver" Text="Ver" >
                                            <ItemStyle Width="15px" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <RowStyle Height="35px" />
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <HeaderStyle CssClass="TituloReq" Height="20px" ForeColor="White" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:CnxBDUSAT %>"
                                    SelectCommand="paReq_ConsultarSolicitudesEnEspera" 
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="codigo_per" QueryStringField="id" Type="Int32" />
                                        <asp:Parameter DefaultValue="1" Name="tipo" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
