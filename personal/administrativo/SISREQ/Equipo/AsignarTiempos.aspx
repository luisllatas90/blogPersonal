<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AsignarTiempos.aspx.vb" Inherits="Equipo_Estados" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href ="../private/estilo.css" rel="stylesheet" type ="text/css" />
    <link href ="../private/estiloweb.css" rel="stylesheet" type ="text/css" />
    <script language ="javascript" type="text/javascript" src ="../private/funciones.js"></script>
    <script language ="javascript" type="text/javascript" src ="../private/funcion.js"></script>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="frmModulo" runat="server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td rowspan="1" >
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" 
                      >
                        <tr>
                            <td align="right" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td width="100%" height="35">
                                            &nbsp;<strong>Buscar por:</strong>
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" style="text-transform: capitalize">
                                    <asp:ListItem Value="-1">--Seleccione Campo Busqueda--</asp:ListItem>
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">M&#243;dulo</asp:ListItem>
                                    <asp:ListItem Value="2">Tipo de solicitud</asp:ListItem>
                                </asp:DropDownList>&nbsp;
                                            <asp:DropDownList ID="cboValor" runat="server" AutoPostBack="True" Visible="False" style="text-transform: capitalize">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td width="100%" style="height: 1px; background-color:#004182";>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="25px">
                                &nbsp;
                                <img alt="" src="../images/CUADRO_VERDE.jpg" /> : Asignada a un grupo
                                <img alt="" src="../images/cuadro_azul.jpg" /> : Asignada a una persona&nbsp;</td>
                            <td align="right" height="25px">
                                <asp:Label ID="LblRegistros" runat="server" Text="Total de Registros: 0" 
                                    ForeColor="#0000CC"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 450px;" valign="top" colspan="2" align="center">
                                <asp:GridView ID="GvSolicitudes" runat="server"
                                    AutoGenerateColumns="False" DataSourceID="ObjDatos" AllowPaging="True" 
                                    AllowSorting="True" Width="98%" CellPadding="4" ForeColor="#333333" 
                                    PageSize="8" GridLines="Horizontal" >
                                    <EmptyDataRowStyle ForeColor="Red" />
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="id_sol" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_tsol" HeaderText="id_tsol" SortExpression="id_tsol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol" Visible="False">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="prioridad">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" SortExpression="codigo_cco"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" 
                                            SortExpression="descripcion_cco" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha"
                                            HtmlEncode="False" SortExpression="fecha_sol" Visible="False">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vigente_solest" HeaderText="vigente_solest" SortExpression="vigente_solest"
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_apl" HeaderText="codigo_apl" SortExpression="codigo_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" 
                                            SortExpression="descripcion_apl">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_est" HeaderText="id_est" SortExpression="id_est" Visible="False" />
                                        <asp:BoundField DataField="id_solequ" HeaderText="id_solequ" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id_solequ" Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:HyperLinkField HeaderText="PROG." Text="" >
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle Height="40px" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        No se encontraron registros
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle CssClass="TituloReq" Height="20px" 
                                        Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td rowspan="1" valign="top" style="height: 8px; background-color: #004182">
                </td>
            </tr>
            <tr>
                <td rowspan="1" valign="top" align="center">
                <iframe id="ifSolicitud" name="ifSolicitud" src ="DatosSolicitud.aspx" 
                        frameborder="0" width="100%" style="height: 230px" ></iframe>
                </td>
            </tr>
            <tr>
                <td rowspan="1" style="height: 1px; background-color: #004182;" valign="top">
                </td>
            </tr>
        </table>
    
    </div>
                                <asp:ObjectDataSource ID="ObjDatos" runat="server" SelectMethod="ObtieneSolicitudesParaAsignarCronograma"
                                    TypeName="clsRequerimientos">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="cod_per" QueryStringField="id" Type="Int32" />
                                        <asp:ControlParameter ControlID="CboCampo" Name="tabla" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="cboValor" Name="campo" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
    </form>
</body>
</html>
