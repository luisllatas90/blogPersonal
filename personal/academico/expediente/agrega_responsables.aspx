<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agrega_responsables.aspx.vb" Inherits="Investigador_agrega_responsables" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../private/tooltip.js"></script>
    <script language="JavaScript" src="private/validainvestigaciones.js"></script>
    <STYLE type="text/css">
BODY {
scrollbar-face-color:#AED9F4;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="right" colspan="3" rowspan="1" style="height: 26px; background-color: #f0f0f0">
                    <asp:Button ID="Button1" runat="server" CssClass="cerrar_prp" Height="14px"
                        Text="     Finalizar" ToolTip="Finalizar agregar responsables" Width="71px" UseSubmitBehavior="False" />&nbsp;
                    &nbsp;
                    &nbsp;<asp:Button ID="CmdAgregar" runat="server" BorderColor="Transparent" BorderStyle="Solid"
                        CssClass="attach_prp" Height="17px" Text="     Agregar" Width="79px" ToolTip="Agregue un responsable a la investigación." ValidationGroup="agregar" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="3" align="center">
                    <table style="width: 630px; height: 27px;" cellpadding="0" cellspacing="0" class="contornotabla" id="TABLE1">
                        <tr>
                            <td colspan="2" style="font-weight: bold; font-size: 11pt; color: maroon; font-family: verdana;
                                height: 32px; text-align: center">
                                Agregue un Responsable</td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">
                                &nbsp; Tipo de Investigador</td>
                            <td style="width: 500px">
                                <asp:DropDownList ID="DDLTipoPersona" runat="server" AutoPostBack="True" style="font-size: 8pt; color: navy; font-family: verdana" ToolTip="Tipo de personal">
                                    <asp:ListItem Value="1">Personal USAT</asp:ListItem>
                                    <asp:ListItem Value="2">Estudiante USAT</asp:ListItem>
                                    <asp:ListItem Value="3">Externo</asp:ListItem>
                                </asp:DropDownList>
                                <img src="../../../images/menus/prioridad_.gif" style="cursor: help" tooltip="<b>TIPO DE INVESTIGADOR</b><br><b><span style='text-align:justify'>- Alumno Investigador de la USAT</b>, Ingrese su nombre o apellido y click en buscar, aparecerá la lista de coincidencias, seleccione al alumno que participará en la Investigación luego seleccione el tipo de participación que tiene en la Investigación<br><b>- Personal Externo a la USAT</b>, se deberá llenar sus datos personales y su centro laboral , acontinuación el tipo de participación.<br><b> - Personal Investigador de la USAT </b>ingresar su nombre o apellido y click en buscar, aparecerá la lista de coincidencias y seleccione al Investigador correspondiente, posteriormente se listará el personal con sus lineas de investigación. Se puede participar de una investigación sin estar enlazado por linea alguna, y a continuación, seleccione el tipo de participación que tiene en la Investigación.</span>"/></td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <asp:Panel ID="PanPersonal" runat="server" Height="50px" Width="630px">
                                    <table style="width: 630px" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 130px">
                                                &nbsp;
                                                Buscar
                                                <img src="../../../images/menus/prioridad_.gif" style="cursor: help" tooltip="<b>Buscar Persona</b><br>Ingrese Apellido Paterno, Materno y/o Nombres y luego haga click en buscar, <br>se mostrará una lista con los datos coincidentes." /></td>
                                            <td style="width: 500px">
                                                <asp:TextBox ID="TxtBuscar" runat="server" Width="237px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" ToolTip="Ingrese apellido paterno, materno o nombres para buscar"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtBuscar"
                                                    ErrorMessage="Ingrese un criterio de búsqueda" SetFocusOnError="True" ValidationGroup="buscar">*</asp:RequiredFieldValidator>
                                                <asp:Button ID="CmdBuscar" runat="server" Text="      Buscar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="buscar1" Width="66px" ValidationGroup="buscar" ToolTip="Click en buscar para mostrar lista de personal" /></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp; Seleccione un personal de la lista inferior:
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validapersonal"
                                                    ErrorMessage="Seleccione un personal de la lista" ValidationGroup="agregar">*</asp:CustomValidator></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                &nbsp;<asp:ListBox ID="LstPersonal" runat="server" Width="601px" Rows="6" style="font-size: 8pt; color: navy; font-family: verdana" ToolTip="Seleccione un personal de la lista" Height="195px"></asp:ListBox></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="PanExterno" runat="server" Height="50px" Width="630px">
                                    <table width="630" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="height: 25px">
                                                &nbsp; &nbsp;Nombre</td>
                                            <td style="width: 500px; height: 25px;">
                                                <asp:TextBox ID="TxtNombre" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" Width="200px" ToolTip="Nombre de personal externo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNombre"
                                                    ErrorMessage="Nombre requerido" SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px">
                                                &nbsp; &nbsp;Apellido Paterno</td>
                                            <td>
                                                <asp:TextBox ID="TxtPaterno" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" Width="200px" ToolTip="Apellido Paterno de personal externo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtPaterno"
                                                    ErrorMessage="Apellido Paterno Requerido" SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px">
                                                &nbsp; &nbsp;Apellido Materno</td>
                                            <td>
                                                <asp:TextBox ID="TxtMaterno" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" Width="200px" ToolTip="Apellido Materno de personal externo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtMaterno"
                                                    ErrorMessage="Apellido materno requerido" SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px">
                                                &nbsp; &nbsp;Centro Laboral</td>
                                            <td>
                                                <asp:TextBox ID="TxtCentroLab" runat="server" Width="300px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" ToolTip="Centro Laboral de personal externo"></asp:TextBox>&nbsp;
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtCentroLab"
                                                    ErrorMessage="Centro Laboral Requerido" SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 13px">
                                &nbsp;
                                                <asp:Label ID="LblLinea" runat="server" Text="Linea Investigación" Width="110px"></asp:Label></td>
                            <td style="height: 25px">
                                                <asp:DropDownList ID="DDLLinea" runat="server" style="font-size: 8pt; color: navy; font-family: verdana" Width="365px" ToolTip="Linea de Investigacion de personal">
                                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                                Tipo de Participación</td>
                            <td style="height: 25px">
                                <asp:DropDownList ID="DDLTipoParticipacion" runat="server" style="font-size: 8pt; color: navy; font-family: verdana" Width="366px" ToolTip="Tipo de participacion de personal">
                                </asp:DropDownList>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLTipoParticipacion"
                                    ErrorMessage="Seleccione un tipo de participacion" MaximumValue="800" MinimumValue="1"
                                    SetFocusOnError="True" Type="Integer" ValidationGroup="agregar">*</asp:RangeValidator></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td align="center" colspan="3" rowspan="1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3" rowspan="1">
                    <asp:Panel ID="PanResponsables" runat="server" Height="200px" ScrollBars="Auto" Width="630px">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderColor="Transparent"
                            DataKeyNames="codigo_Res" DataSourceID="ObjResponsable" GridLines="None" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="N&#176;">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_Res" HeaderText="codigo_Res" InsertVisible="False"
                                    ReadOnly="True" SortExpression="codigo_Res" Visible="False" />
                                <asp:BoundField DataField="descripcion_Tpi" HeaderText="Responsabilidad" SortExpression="descripcion_Tpi">
                                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Situacion">
                                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Datos_Per" HeaderText="Datos_Per" ReadOnly="True" SortExpression="Datos_Per"
                                    Visible="False">
                                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Datos2_Per" HeaderText="Responsable" ReadOnly="True" SortExpression="Datos2_Per">
                                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Datos_Alu" HeaderText="Datos_Alu" ReadOnly="True" SortExpression="Datos_Alu"
                                    Visible="False" />
                                <asp:BoundField DataField="Datos_Ext" HeaderText="Datos_Ext" ReadOnly="True" SortExpression="Datos_Ext"
                                    Visible="False" />
                                <asp:BoundField DataField="nombre_Lin" HeaderText="nombre_Lin" SortExpression="nombre_Lin"
                                    Visible="False" />
                                <asp:CommandField ButtonType="Image" DeleteImageUrl="../../../images/menus/noconforme_small.gif"
                                    HeaderText="Eliminar" ShowDeleteButton="True">
                                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                            </Columns>
                            <RowStyle Font-Names="Verdana" Font-Size="8pt" Height="20px" />
                            <HeaderStyle BackColor="#E1F1FB" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy"
                                HorizontalAlign="Center" />
                        </asp:GridView>
                        &nbsp;
                    </asp:Panel>
                    <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="buscar" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="agregar" />
        <asp:ObjectDataSource ID="ObjResponsable" runat="server" DeleteMethod="EliminarResponsableInv"
            SelectMethod="ConsultarInvestigaciones" TypeName="Personal">
            <DeleteParameters>
                <asp:Parameter Name="codigo_res" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="8" Name="tipo" Type="String" />
                <asp:QueryStringParameter DefaultValue="" Name="param1" QueryStringField="codigo_Inv"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HidenCodigoInv" runat="server" />
    </form>
</body>
</html>
