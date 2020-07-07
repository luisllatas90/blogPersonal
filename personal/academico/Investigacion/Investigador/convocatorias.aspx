<%@ Page Language="VB" AutoEventWireup="false" CodeFile="convocatorias.aspx.vb" Inherits="Investigador_convocatorias" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <script type="text/javascript"  language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body style="background-color: #F0F0F0">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana; text-align: center">
                    Convocatorias para Investigaciones</td>
            </tr>
            <tr>
                <td style="font-size: 8pt; color: navy; font-family: verdana">
                    <hr style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                        border-bottom: black 1px solid; height: 1px" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 8pt; color: navy; font-family: verdana">
                    &nbsp;
                    Opciones de Busqueda :&nbsp;<asp:DropDownList ID="DDLOpciones" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy">
                        <asp:ListItem Value="1">Titulo</asp:ListItem>
                        <asp:ListItem Value="2">Institucion</asp:ListItem>
                        <asp:ListItem Value="3">Descripcion</asp:ListItem>
                        <asp:ListItem Value="4">Temas</asp:ListItem>
                    </asp:DropDownList>
                    criterio de Busqueda
                    <asp:TextBox ID="TxtCriterio" runat="server" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Width="250px" Font-Size="8pt"></asp:TextBox>
                    <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" />
                    </td>
            </tr>
            <tr>
                <td style="font-size: 8pt; color: navy; font-family: verdana; height: 1px">
                    <hr style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                        border-bottom: black 1px solid; height: 1px" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 8pt; color: red; font-family: verdana; height: 1px">
                    &nbsp;A continuación se muestran un listado de las convocatorias para Investigaciones,
                    puede descargar el detalle haciendo click en el ícono
                    <img src="../../../../images/download.gif" />
                    de cada registro.</td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="codigo_con" 
                        DataSourceID="OCnvocatorias" CellPadding="3" RepeatColumns="1" 
                        RepeatDirection="Horizontal" Width="100%">
                        <ItemTemplate>
                            <table style="width: 100%" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td style="font-weight: normal; font-size: 10pt; color: white; font-family: verdana; background-color: #006699">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="../../../../images/nuevoinv.gif"
                                            Visible='<%# IIF (DateDiff(DateInterval.Day, Eval("fecharegistro_con"), Now) < 7,true ,false ) %>' />
                                        <asp:Label ID="titulo_conLabel" runat="server" Text='<%# Eval("titulo_con") %>'></asp:Label>
                                        -&nbsp;
                                        <asp:Label ID="institucion_conLabel" runat="server" Text='<%# Eval("institucion_con") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-size: 8pt; font-family: verdana; background-color: #ffffa8; font-weight: bold; text-align: justify;">
                                        Descripcion<br />
                                        <asp:Label ID="descripcion_conLabel" runat="server" Text='<%# Eval("descripcion_con") %>' Font-Bold="False"></asp:Label>
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 8pt; font-family: verdana; background-color: #ffffa8; font-weight: bold;">
                                        Tematica<br />
                                        <asp:Label ID="temas_conLabel" runat="server" Text='<%# Eval("temas_con") %>' Font-Bold="False"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                        <br />
                                        Fecha Límite :
                                        <asp:Label ID="FechaFinal_ConLabel" runat="server" Text='<%# Eval("FechaFinal_Con", "{0:d}") %>' ForeColor="Black"></asp:Label>
                                        &nbsp;Descargar Formatos :
                                        <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="../../../../images/download.gif"
                                            NavigateUrl='<%# "../directorinvestigacion/convocatorias/" & Eval("RutaArchivo_Con") %>'
                                            Target="_blank"></asp:HyperLink>
                                        </td>
                                </tr>
                            </table>
                            <hr style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                border-bottom: black 1px solid; height: 1px" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50%" Wrap="False" />
                    </asp:DataList><asp:SqlDataSource ID="OCnvocatorias" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                        SelectCommand="INV_ConsultarConvocatorias" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DDLOpciones" Name="tipo" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="TxtCriterio" DefaultValue=" " Name="criterio" PropertyName="Text"
                                Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
