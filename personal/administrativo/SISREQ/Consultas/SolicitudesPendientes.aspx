<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudesPendientes.aspx.vb" Inherits="Consultas_SolicitudesPendientes" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" type="text/css" rel="stylesheet" />
    <link href="../private/estiloweb.css" type="text/css" rel="stylesheet" />
    <script src="../private/funcion.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table style="width: 100%; height: 169px" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td rowspan="1"
                    valign="top">
                    <table style="width: 100%; height: 34px">
                        <tr>
                            <td height="35px" >
                                    &nbsp;
                                    <asp:Label ID="LblConsultar" runat="server" Font-Bold="True" Text="CONSULTAR:"></asp:Label>
&nbsp;<asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="0">--Seleccione Campo--</asp:ListItem>
                                        <asp:ListItem Value="1">Todos</asp:ListItem>
                                        <asp:ListItem Value="2">Por Responsable</asp:ListItem>
                                    </asp:DropDownList>
         
                                <asp:DropDownList ID="CboResponsable" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">-- Todos --</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; width: 95%;" valign="top">
                    <table style="width: 100%; height: 60px">
                        <tr>
                            <td valign="top" style="height: 1px; background-color: #004182;">
                             </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" style="text-align: center; height: 400px;">
                                <asp:GridView ID="GvRequerimientos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    DataSourceID="ObjSolicitud" AllowSorting="True" EnableTheming="True" 
                                    PageSize="8" Width="98%" DataKeyNames="id_est" ForeColor="#999999" 
                                    GridLines="Horizontal">
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="id_sol" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol">
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_tsol" HeaderText="id_tsol" SortExpression="id_tsol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol" Visible="False">
                                            <ItemStyle Width="120px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="prioridad">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" SortExpression="descripcion_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" SortExpression="descripcion_cco">
                                            <ItemStyle Width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha"
                                            SortExpression="fecha_sol" HtmlEncode="False">
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="vigente_solest" HeaderText="vigente_solest" SortExpression="vigente_solest"
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_apl" HeaderText="codigo_apl" SortExpression="codigo_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl" Visible="False">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_est" HeaderText="id_est" SortExpression="id_est" Visible="False" />
                                        <asp:BoundField DataField="id_solequ" HeaderText="id_solequ" InsertVisible="False"
                                            ReadOnly="True" SortExpression="id_solequ" Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Image" ImageUrl="../images/editar.gif">
                                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <RowStyle Height="40px" ForeColor="Black" />
                                    <PagerStyle Height="15px" />
                                    <SelectedRowStyle BackColor="#FFFFA8" ForeColor="Black" />
                                    <HeaderStyle Height="20px" ForeColor="White" CssClass="TituloReq" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="text-align: center">
                                <span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una solicitud para visualizar su detalle</span></td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 8px; background-color: #004182;">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="text-align: center">
                            <iframe id="fradetalle" name="fradetalle" src="../Equipo/DatosSolicitud.aspx" 
                                    frameborder="0" scrolling ="no" style="width: 100%; height: 243px;"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" style="height: 1px; background-color: #004182;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
     <asp:HiddenField  ID="txtelegido" runat="server" />
                                <asp:ObjectDataSource ID="ObjSolicitud" runat="server" SelectMethod="obtieneSolicitudPorResponsable"
                                    TypeName="clsRequerimientos">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="0" Name="cod_per" SessionField="cod_per" 
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
    </form>
</body>
</html>
