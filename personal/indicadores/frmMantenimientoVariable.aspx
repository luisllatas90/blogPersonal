<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoVariable.aspx.vb"
    Inherits="Indicadores_Formularios_frmMantenimientoVariable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>

    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>

    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>

    <script type="text/javascript" language="javascript">
        function PintarFilaElegida(obj) {
            if (obj.style.backgroundColor == "white") {
                obj.style.backgroundColor = "#E6E6FA"//#395ACC
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
        function cmdNuevo_onclick() {

        }
    </script>

    <style type="text/css">
        .usatFormatoCampo
        {
            float: left;
            font-weight: bold;
            padding-top: 10px;
            padding-left: 30px;
            color: #27333c;
            font-family: Arial;
            height: 20px;
            width: 200px;
        }
        .usatFormatoCampoAncho
        {
            float: left;
            font-weight: bold;
            padding-top: 10px;
            padding-left: 30px;
            color: #27333c;
            font-family: Arial;
            height: 20px;
            width: 585px; /*border:1px solid red;*/
        }
        .usatTituloAzul
        {
            font-family: Arial;
            font-size: 12pt;
            font-weight: bold;
            color: #3063c5;
            width: 831px;
            height: 20px;
        }
        .usatPanel
        {
            border: 1px solid #C0C0C0; /*height:490px;	        */
            height: 550px;
            max-height: 600PX;
            -moz-border-radius: 15px; /*padding-top:10px;*/
        }
        .usatPanelConsulta
        {
            border: 1px solid #C0C0C0; /*-moz-border-radius: 15px; */
            padding-top: 10px;
            margin-top: 10px;
            padding-bottom: 10px;
        }
        #lblSubtitulo
        {
            position: relative;
            top: -10px;
            left: 10px;
            font-size: 10pt;
            color: #3063c5;
            font-weight: bold;
            background-color: White;
            text-align: center;
        }
        #lblConsulta
        {
            position: relative;
            top: -20px;
            left: 10px;
            font-size: 10pt;
            color: #3063c5;
            font-weight: bold;
            background-color: White;
            text-align: center;
        }
        /***** Para avisos **************/
        .mensajeError
        {
            border: 1px solid #e99491;
            background-color: #fed8d5;
            padding-top: 2px;
            -moz-border-radius: 15px;
            padding-left: 25px;
        }
        .mensajeExito
        {
            border: 1px solid #488e00;
            background-color: #c5f4b5;
            padding-top: 2px;
            -moz-border-radius: 15px;
            padding-left: 25px;
        }
        .mensajeAviso
        {
            border: 1px solid #FACC2E;
            background-color: #F2F5A9;
            padding-top: 2px;
            -moz-border-radius: 15px;
            padding-left: 25px;
        }
        /********************************/</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">
        Añadir Variables <a href="#" onclick="apprise('Añade un nueva Variable.');">
            <img src="../Images/info.png" style="border: 0" alt="Ayuda." />
        </a>
    </div>
    <!-- Para avisos -->
    <div style="clear: both; height: 5px">
    </div>
    <div id="avisos" runat="server" style="height: 25px; padding-top: 2px; width: 98%">
        <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF" />
        <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblDnt" runat="server" Text="Label" Visible="False"></asp:Label>
    </div>
    <div style="clear: both; height: 10px">
    </div>
    <!-------------------------------------------->
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro" Width="120px"></asp:Label>
        <div style="margin-top: 10px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblCategoria" runat="server" Text="Categoría"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlCategorias" runat="server" Width="70%">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Text="Sumatoria de valores"></asp:Label>
                &nbsp;<asp:CheckBox ID="chkSumatoria" runat="server" />
            </div>
            <div style="clear: both; height: 5px;">
            </div>
        </div>
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblPeriodicidad" runat="server" Text="Periodicidad"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPeriodicidad" runat="server" Width="100%">
                </asp:DropDownList>
            </div>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Debe seleccionar una Periodicidad."
                ControlToValidate="ddlPeriodicidad" Display="Dynamic" Operator="NotEqual" ValidationGroup="grupo1"
                ValueToCompare="0" Text="*">
            </asp:CompareValidator>
            <div style="clear: both; height: 5px;">
            </div>
        </div>
        <div>
            <div class="usatFormatoCampo">
                <asp:Label ID="lblCodigo" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtDescripcion" runat="server" Width="580px" ValidationGroup="grupo1"
                    Font-Size="Smaller"></asp:TextBox>
            </div>
            <div style="padding-top: 15px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion"
                    ErrorMessage="Debe ingresar la Descripción." ValidationGroup="grupo1" Display="Dynamic"
                    Text="*">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDescripcion"
                    Display="Dynamic" ErrorMessage="(*) Ingrese carácteres válidos en la Descripción."
                    ValidationExpression="^[a-zA-ZñÑáéíóúÁÉÍÓÚ0-9\-\.\s]*" ValidationGroup="grupo1"
                    SetFocusOnError="true" EnableTheming="True" Text="*">
                </asp:RegularExpressionValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="(*) Ingresar valores válidos"
                    ControlToValidate="txtDescripcion" Display="Dynamic" ValidationGroup="grupo1"
                    Text="*">
                </asp:CustomValidator>
            </div>
            <div style="clear: both; height: 5px;">
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height: 150px;">
            <div class="usatFormatoCampo" style="height: 120px">
                <asp:Label ID="Label2" runat="server" Text="Configuración de Niveles de Subvariable"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="height: 120px">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvNivelesAux" runat="server" AutoGenerateColumns="False" DataKeyNames="Codigo,Abreviatura"
                            CellPadding="4" Width="100%" BackColor="White" BorderColor="White" Font-Bold="False">
                            <RowStyle BackColor="White" ForeColor="#333333" BorderColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccione">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccion" runat="server" Width="5px" OnCheckedChanged="chkSeleccion_CheckedChanged"
                                            AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                                <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" Visible="False" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Nivel Subvariable" />
                                <asp:TemplateField HeaderText="Elementos del Nivel Subvariable" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div class="scrollbars" id="divChk" style="overflow: auto; height: 50px; width: 300px">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBoxList ID="chklstAuxItems" runat="server" BorderWidth="1px" Visible="True"
                                                        BorderColor="Black" Height="40px" OnSelectedIndexChanged="chklstAuxItems_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:CheckBoxList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="chklstAuxItems" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            </select>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvNivelesAux" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="float: left; padding-top: 20px;">
                <asp:Label ID="lblValidarNAux" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div style="height: 100px">
            <div class="usatFormatoCampo" style="height: 120px; width: 200px">
                <asp:Label ID="Label1" runat="server" Text="Configuración de Niveles de Dimensión"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="width: 585px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvNivelesDim" runat="server" AutoGenerateColumns="False" DataKeyNames="Codigo,Abreviatura,codigo_naux"
                            CellPadding="4" Width="100%" BackColor="White" BorderColor="White" Font-Bold="False">
                            <RowStyle BackColor="White" ForeColor="#333333" BorderColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccione">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccion2" runat="server" Width="5px" OnCheckedChanged="chkSeleccion2_CheckedChanged"
                                            AutoPostBack="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                                <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" Visible="False" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvNivelesAux" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="float: left; padding-top: 20px;">
                <asp:Label ID="lblND" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div style="height: 200px;">
            <div class="usatFormatoCampo" style="height: 120px;">
                <asp:Label ID="lblSubdimensiones" runat="server" Text="Configuración de Niveles de Subdimensión"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho" style="height: 120px; width: 585px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvNivelesSub" runat="server" AutoGenerateColumns="False" DataKeyNames="Codigo,Abreviatura,Codigonid"
                            CellPadding="4" Width="100%" BackColor="White" BorderColor="White" Font-Bold="False">
                            <RowStyle BackColor="White" ForeColor="#333333" BorderColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Seleccione">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccion" runat="server" Width="5px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                                <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" Visible="False" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Nivel Subdimensión" />
                                <asp:BoundField DataField="NivelDimension" HeaderText="Nivel Dimensión" />
                                <asp:BoundField DataField="Codigonid" HeaderText="Codigonid" Visible="False" />
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvNivelesDim" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div style="float: left; padding-top: 20px;">
                <asp:Label ID="lblValidacionCheckboxlist" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div>
        <table cellpadding="3" cellspacing="0" style="width: 100%; border: 1px solid #96ACE7"
            border="0">
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width: 80%">
                    <asp:CheckBox ID="chkEstructura" Text="Generar Estructura" runat="server" Checked="True"
                        Visible="false" />
                    &nbsp;
                    <br />
                    <asp:Label ID="Label3" runat="server" Visible="false" Text="(*) Permite generar toda la estructura jerárquica de la variable, previa configuración de los niveles."
                        Font-Bold="False" ForeColor="#FF0066" Font-Size="X-Small"></asp:Label>
                </td>
                <td align="right" style="width: 20%" valign="top">
                    <asp:Button ID="cmdGuardar" runat="server" Text="   Guardar" CssClass="guardar2"
                        ValidationGroup="grupo1" />
                    &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2"
                        Width="70px" />
                </td>
            </tr>
            <tr style="background-color: #E8EEF7; font-weight: bold">
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
                        ShowSummary="true" HeaderText="Errores encontrados:" BorderWidth="0" ValidationGroup="grupo1"
                        Font-Bold="False" ForeColor="#FF0066" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div class="usatPanelConsulta">
        <asp:Label ID="lblConsulta" runat="server" Text="Consulta de Variables" Width="150px"></asp:Label>
        <div style="height: 50px">
            <div class="usatFormatoCampo">
                <asp:Label ID="lblBuscar" runat="server" Text="Ingrese parámetro de búsqueda"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:TextBox ID="txtBuscar" runat="server" Width="80%"></asp:TextBox>
                <!-- Para Validar Caja de Busqueda -->
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBuscar"
                    Display="Dynamic" ErrorMessage="Ingrese carácteres válidos para buscar la Variable."
                    ValidationExpression="[a-zA-ZñÑáéíóúÁÉÍÓÚ ,.]{3,}" ValidationGroup="grupo2" SetFocusOnError="true"
                    EnableTheming="True" Text="*">
                </asp:RegularExpressionValidator>
                <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Se encontraron palabras reservadas en la búsqueda de la Variable."
                    ControlToValidate="txtBuscar" Display="Dynamic" OnServerValidate="CustomValidator2_ServerValidate"
                    Text="*" ValidationGroup="grupo2">
                </asp:CustomValidator>
                <!------------------------------------------------------------------------------------>
                <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="buscar" ValidationGroup="grupo2" />
                <!-- Para Validar Caja de Busqueda -->
            </div>
        </div>
        <!-- Para Validar Caja de Busqueda -->
        <div style="clear: both; height: 10px">
        </div>
        <div style="padding-left: 25px">
            <table cellpadding="3" cellspacing="0">
                <tr style="font-weight: bold">
                    <td style="width: 80%">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="BulletList"
                            ShowSummary="true" HeaderText="Errores encontrados en la Búsqueda:" BorderWidth="0"
                            ValidationGroup="grupo2" Font-Bold="False" ForeColor="#FF0066" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div>
            <!------------------------------------------------------------------------------------>
            <div>
                <asp:Label ID="lblAviso" runat="server" Text="(*) Las variables remarcadas de rojo se encuentran bloquedas debido a que se encuentran ligadas a fórmulas e indicadores."
                    ForeColor="Red"></asp:Label>
                <table cellpadding="3" cellspacing="0" style="width: 99%; border: 1px solid #96ACE7"
                    border="0">
                    <tr>
                        <td style="text-align: center" align="center">
                            <asp:GridView ID="gvVariable" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False"
                                DataKeyNames="Codigo,CodigoCat,CodigoPeri,sumatoria_var,Var_valor,PertenceFormula"
                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField HeaderText="N°">
                                        <ItemStyle Width="15px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
                                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                                    <asp:BoundField HeaderText="Variable" DataField="Descripcion">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Periodicidad" HeaderText="Periodicidad" />
                                    <asp:BoundField DataField="ExisteValor" HeaderText="Existe Valor" />
                                    <asp:BoundField HeaderText="Fecha Registro" DataField="FechaRegistro" />
                                    <asp:BoundField DataField="CodigoCat" HeaderText="CodigoCat" Visible="False" />
                                    <asp:BoundField DataField="CodigoPeri" HeaderText="CodigoPeri" Visible="False" />
                                    <asp:BoundField DataField="sumatoria_var" HeaderText="Sum.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10px" />
                                    </asp:BoundField>
                                    <asp:CommandField ShowSelectButton="True" SelectImageUrl="../images/editar.gif" ButtonType="Image" />
                                    <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="../images/eliminar.gif"
                                        ButtonType="Image" />
                                    <asp:BoundField DataField="Var_valor" HeaderText="Var_valor" Visible="False" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#284775" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
