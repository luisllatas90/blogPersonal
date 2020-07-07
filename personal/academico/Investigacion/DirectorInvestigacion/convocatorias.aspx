<%@ Page Language="VB" AutoEventWireup="false" CodeFile="convocatorias.aspx.vb" Inherits="DirectorInvestigacion_convocatorias" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../../../private/funciones.js" type="text/javascript" language="javascript"></script>
    <script src="../../../../private/calendario.js" type="text/javascript" language="javascript"></script>
</head>
<body style="background-color: #F0F0F0">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td style="font-size: 11pt; color: navy; font-family: verdana; font-weight: bold; text-align: center;">
                    &nbsp;
                    Registro de Convocatorias</td>
            </tr>
            <tr>
                <td style="font-size: 8pt; color: navy; font-family: verdana; border-top: black 1px solid; border-bottom: black 1px solid; height: 35px;">
                    &nbsp;
                    Opciones de Busqueda :&nbsp;<asp:DropDownList ID="DDLOpciones" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy">
                        <asp:ListItem Value="1">Titulo</asp:ListItem>
                        <asp:ListItem Value="2">Institucion</asp:ListItem>
                        <asp:ListItem Value="3">Descripcion</asp:ListItem>
                        <asp:ListItem Value="4">Temas</asp:ListItem>
                    </asp:DropDownList>
                    criterio de Busqueda
                    <asp:TextBox ID="TxtCriterio" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Width="230px" Font-Size="8pt"></asp:TextBox>
                    <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                    <asp:Button ID="CmdNuevo" runat="server" Text="Nuevo" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" /></td>
            </tr>
            <tr>
                <td style="font-size: 7pt; color: red; font-family: verdana">
                    (Para poder modificar o eliminar los datos de una convocatoria seleccione una de la lista y
                    a continuación realice la acción respectiva.</td>
            </tr>
            <tr>
                <td style="height: 300px" valign="top">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataKeyNames="codigo_con" DataSourceID="COnsultarConvocatorias"
                        Width="100%" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField DataField="codigo_con" HeaderText="codigo_con" InsertVisible="False"
                                ReadOnly="True" SortExpression="codigo_con" Visible="False" />
                            <asp:BoundField DataField="titulo_con" HeaderText="Titulo" SortExpression="titulo_con" >
                                <ItemStyle VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="institucion_con" HeaderText="Institucion" SortExpression="institucion_con" >
                                <ItemStyle VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_con" HeaderText="Descripcion" SortExpression="descripcion_con" >
                                <ItemStyle VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="naturaleza_con" HeaderText="Naturaleza" SortExpression="naturaleza_con" Visible="False" >
                                <ItemStyle VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="temas_con" HeaderText="Temas" SortExpression="temas_con" >
                                <ItemStyle VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecharegistro_con" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha Registro"
                                HtmlEncode="False" SortExpression="fecharegistro_con">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaFinal_Con" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha Entrega"
                                HtmlEncode="False" SortExpression="FechaFinal_Con">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RutaArchivo_Con" HeaderText="Archivo" SortExpression="RutaArchivo_Con" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:CommandField SelectText=" " ShowSelectButton="True" />
                        </Columns>
                        <RowStyle Height="23px" Font-Names="Verdana" Font-Size="8pt" />
                        <SelectedRowStyle BackColor="#FFFF80" />
                        <HeaderStyle BackColor="#006699" Font-Names="Verdana" Font-Size="8pt" ForeColor="White"
                            Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:Panel ID="PanelFormulario" runat="server" Visible="False" Width="100%">
                        <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" cellpadding="0">
                            <tr>
                                <td align="center" colspan="2" style="font-size: 8pt; color: navy; font-family: verdana;
                                    height: 32px">
                                    &nbsp;<asp:Label ID="LblTitulo" runat="server" Font-Bold="True" Font-Names="Verdana"
                                        Font-Size="11pt" Text="Registrar una Convocatoria"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 105px; font-size: 8pt; color: navy; font-family: verdana;">
                                    &nbsp;
                                    Titulo</td>
                                <td style="width: 524px">
                                    <asp:TextBox ID="TxtTitulo" runat="server" TextMode="MultiLine" Width="510px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTitulo" ErrorMessage="Ingrese Titulo de Convocatoria"
                                        SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr style="color: #000000">
                                <td style="width: 105px; font-size: 8pt; color: navy; font-family: verdana;">
                                    &nbsp;
                                    Institución</td>
                                <td style="width: 524px">
                                    <asp:TextBox ID="TxtInstitucion" runat="server" Width="510px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtInstitucion"
                                        ErrorMessage="Ingrese Institución" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr style="color: #000000">
                                <td style="width: 105px; font-size: 8pt; color: navy; font-family: verdana;">
                                    &nbsp;
                                    Descripción</td>
                                <td style="width: 524px">
                                    <asp:TextBox ID="TxtDescripcion" runat="server" TextMode="MultiLine" Width="510px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" Height="71px"></asp:TextBox><asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDescripcion"
                                        ErrorMessage="Ingrese Descripcion" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 105px; font-size: 8pt; color: navy; font-family: verdana;">
                                    &nbsp;
                                    Temas<br />
                                    &nbsp; Relacionados</td>
                                <td style="width: 524px">
                                    <asp:TextBox ID="TxtTemas" runat="server" TextMode="MultiLine" Width="510px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 105px; font-size: 8pt; color: navy; font-family: verdana;">
                                    &nbsp; Fecha Límite</td>
                                <td style="width: 524px">
                                    <asp:TextBox ID="TxtFecha" runat="server" Width="75px" onkeydown="return false" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><input id="Button1"
                                        type="button" onclick="MostrarCalendario('TxtFecha')" class="cunia" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtFecha"
                                        ErrorMessage="Ingrese Fecha" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td style="width: 105px; font-size: 8pt; color: navy; font-family: verdana;">
                                    &nbsp;
                                    Archivo 
                                    <br />
                                    &nbsp; Convocatoria 
                                    <br />
                                    &nbsp; (*.pdf)</td>
                                <td style="width: 524px">
                                    <asp:FileUpload ID="FileArchivo" runat="server" Width="509px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileArchivo"
                                        ErrorMessage="Seleccione Archivo" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" ValidationGroup="guardar" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    <asp:Button ID="CmdEliminar" runat="server" Text="Eliminar" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnClientClick="return confirm('¿Desea Eliminar el Elemento Seleccionado?')" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    
    </div>
        <asp:SqlDataSource ID="COnsultarConvocatorias" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
            SelectCommand="INV_ConsultarConvocatorias" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="DDLOpciones" Name="tipo" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="TxtCriterio" DefaultValue=" " Name="criterio" PropertyName="Text"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="guardar" />
    </form>
</body>
</html>
