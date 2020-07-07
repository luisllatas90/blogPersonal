<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nuevapropuesta_POA_v1.aspx.vb"
    Inherits="nuevapropuesta" Debug="true" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css">
    <script type="text/javascript" src="../funciones.js"> </script>
    <script type="text/javascript">
        function OcultarTabla() {
            if (document.form1.FileArchivo.value != "" && document.form1.TxtNombre.value != "") {
                document.all.tblDatos.style.display = "none"
                document.all.tblmensaje.style.display = ""
            }
        }

        function Cerrar() {
            if (confirm('¿Desea guardar la propuesta como borrador?') == true) {
                ;

            } else {
                history.back();
            }
        }
    </script>

    <script type="text/javascript" language="javascript">

        function ValidarEnvio() {
            var Cadena = document.form1.FileArchivo.value
            var SubCadena = Cadena
            var n = 0
            if (document.form1.FileArchivo.value != "") {
                for (var i = 0; i < 15; i++) {
                    n = SubCadena.indexOf("\\");
                    // alert(n)
                    if (n == -1) {
                        break;
                    }
                    SubCadena = SubCadena.substr(n + 1, SubCadena.length - 7)
                }
            }
            document.form1.TxtNombre.value = SubCadena
        }

        function ponervalortext(nombre, numero) {
            SeleccionarFila1()
            form1.txtelegido.value = numero
            form1.txtnombrearchivo.value = nombre
        }

        function SeleccionarFila1() {
            oRow = window.event.srcElement.parentElement;
            if (oRow.tagName == "TR") {
                AnteriorFila.Typ = "Sel";
                AnteriorFila.className = AnteriorFila.Typ + "Off";
                AnteriorFila = oRow;
            }
            oRow.Typ = "Selected";
            oRow.className = oRow.Typ;
        }		
    </script>

    <title>Propuestas en Borrador</title>
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style11
        {
            width: 60%;
        }
        .style10
        {
            width: 18px;
        }
        .style8
        {
            height: 222px;
            width: 18px;
        }
        .style12
        {
            width: 100%;
            height: 435px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div>
        <table class="contornotabla_azul" cellpadding="0" cellspacing="0" align="left">
            <tr>
                <td valign="top" bgcolor="#F0F0F0" class="style11">
                    <table style="width: 100%; background-color: #F0F0F0;" align="center" cellpadding="0"
                        cellspacing="0">
                        <tr>
                            <td class="bordeinf" valign="top">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="cmdEnviar" runat="server" Text="   Enviar" CssClass="enviarpropuesta"
                                                Height="35px" Width="100px" />
                                            <asp:Button ID="cmdGuardar" runat="server" Text="        Guardar" CssClass="guardar_prp"
                                                Height="35px" Width="100px" ValidationGroup="Guardar" />
                                            <asp:Button ID="cmdPrioridad" runat="server" Text="        Prioridad" CssClass="prioridad"
                                                Height="35px" Width="100px" />
                                            <asp:Button ID="cmdAdjuntar" runat="server" Text="     Adjuntar" CssClass="attach_"
                                                Height="35px" Width="100px" Visible="False" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="cmdSalir" runat="server" Text="Salir" CssClass="salir_prp" Height="35px"
                                                Width="112px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="color: #0000FF;" class="style12">
                                    <tr>
                                        <td colspan="7" style="font-weight: bold">
                                            <asp:Label ID="lblIdPropuesta" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblIdUsuario" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="lblNuevoDap" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="font-weight: bold">
                                            DATOS GENERALES
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30px" style="font-weight: bold">
                                            Tipo Propuesta:
                                        </td>
                                        <td colspan="5">
                                            <asp:DropDownList ID="ddlTipoPropuesta" runat="server" Width="450px" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30px" style="font-weight: bold" rowspan="2">
                                            Propuesta:
                                        </td>
                                        <td colspan="5" width="10%">
                                            <asp:DropDownList ID="ddl_propuesta" runat="server" AutoPostBack="True" Width="450px">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdcodigo_cco" runat="server" />
                                            <asp:Label ID="lbl_Propuesta" runat="server" Font-Bold="True" Text="[                                ]"></asp:Label>
                                            <br />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="True" Visible="False">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblPrioridad" runat="server" Font-Bold="True" ForeColor="Red" Text=" !Prioridad Alta"
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtPropuesta" runat="server" Width="450px" MaxLength="100" Style="font-size: x-small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">
                                            Área:
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="lblArea" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCentroCosto" runat="server" AutoPostBack="True" Width="50px"
                                                Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">
                                            Derivar a:
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="lblFacultad" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblFacultadID" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="font-weight: bold">
                                        </td>
                                    </tr>
                                    <tr id="TDPresupuesto" runat="server">
                                        <td colspan="7" style="font-weight: bold">
                                            PRESUPUESTO
                                        </td>
                                    </tr>
                                    <tr id="TDMargen" runat="server">
                                        <td>
                                            Margen:
                                        </td>
                                        <td colspan="6" style="font-weight: bold; color: #000000;">
                                            <asp:Label ID="lblMargen" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="TDRentabilidad" runat="server">
                                        <td>
                                            Rentabilidad (%):
                                        </td>
                                        <td style="font-weight: bold; color: #000000;">
                                            <asp:Label ID="lblRentabilidad" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>
                                        </td>
                                        <td colspan="5" style="font-weight: bold; color: #000000;">
                                            <asp:Label ID="lblMsgRentabilidad" runat="server" Font-Bold="True" Style="font-size: x-small"
                                                ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td style="font-weight: bold">
                                            Moneda:
                                        </td>
                                        <td valign="middle" width="10%">
                                            <asp:Label ID="lblMoneda" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:DropDownList ID="ddlMoneda" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="5" width="10%">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Tipo de Cambio:   "></asp:Label>
                                            <asp:Label ID="lblTipoCambioSimbolo" runat="server"></asp:Label>
                                            <asp:Label ID="lblTipoCambio" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td>
                                            Ingreso
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblIngreso" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txtIngreso" runat="server" Width="70px" AutoPostBack="True" Visible="False">0</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIngreso"
                                                ErrorMessage="Ingrese un Importe" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td width="10%">
                                            Egreso
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblEgreso" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:TextBox ID="txtEgreso" runat="server" Width="70px" AutoPostBack="True" Visible="False">0</asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEgreso"
                                                ErrorMessage="Ingrese un Importe" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td width="10%">
                                            Utilidad
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblUtilidad" runat="server" Font-Bold="True" ForeColor="Blue">0</asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="TDImportes" runat="server">
                                        <td style="font-weight: bold">
                                            Ingreso S/.
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblIngresoMN" runat="server">0</asp:Label>
                                        </td>
                                        <td style="font-weight: bold">
                                            Egreso S/.
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblEgresoMN" runat="server">0</asp:Label>
                                        </td>
                                        <td style="font-weight: bold">
                                            Utilidad S/.
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblUtilidadMN" runat="server" Font-Bold="True" ForeColor="Blue">0</asp:Label><asp:Label
                                                ID="lblMsgUtilidad" runat="server" Font-Bold="True" Style="font-size: x-small"
                                                ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold" valign="top">
                                            RESUMEN
                                        </td>
                                        <td colspan="6">
                                            <asp:TextBox ID="txtResumen" runat="server" Height="60px" MaxLength="10" Rows="4"
                                                Width="450px" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtResumen"
                                                ErrorMessage="Ingrese un Resumen" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold" valign="top">
                                            IMPORTANCIA
                                        </td>
                                        <td colspan="6">
                                            <asp:TextBox ID="txtImportancia" runat="server" Height="60px" MaxLength="10" Rows="4"
                                                Width="450px" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtImportancia"
                                                ErrorMessage="Ingrese la Importancia" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                    ValidationGroup="Guardar" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="38%" align="center" bgcolor="#F0F0F0" valign="top" style="border-left-style: solid;
                    border-left-width: 1px; border-left-color: #999999;">
                    <table id="tblDatos" width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="3" valign="middle" style="vertical-align: middle" class="style7">
                                &nbsp;<img src="../images/attach_2_small.gif" style="vertical-align: middle" />
                                <asp:Label ID="Label2" runat="server" Text="Adjuntar Archivos" Font-Bold="True" Font-Names="Verdana"
                                    Font-Size="9pt" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" class="style7">
                                <hr style="height: 1px" />
                                <asp:FileUpload ID="FileArchivo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Width="90%" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileArchivo"
                                    ErrorMessage="Seleccione el archivo a subir" SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana">
                                &nbsp;Nombre&nbsp;
                                <asp:TextBox ID="TxtNombre" runat="server" Width="200px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNombre" ErrorMessage="Ingrese un nombre al archivo"
                                    SetFocusOnError="True" ValidationGroup="agregar">*</asp:RequiredFieldValidator>
                                <asp:Button ID="CmdAgregar" OnClientClick="OcultarTabla()" runat="server" Text="Agregar"
                                    CssClass="attach_prp" Width="79px" ValidationGroup="agregar" />
                                <hr style="height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana"
                                class="style10">
                                &nbsp;
                            </td>
                            <td colspan="2" style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana">
                                &nbsp;<img src="../images/attach_3.gif" />
                                Archivos Adjuntos
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style8">
                                &nbsp;
                            </td>
                            <td style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana;
                                width: 80%; height: 222px; border-right: blue 1px solid; border-top: blue 1px solid;
                                border-left: blue 1px solid; border-bottom: blue 1px solid; background-color: white;
                                border-color: #3366FF;" valign="top">
                                &nbsp;&nbsp;
                                <table width="100%" style="font-weight: bold">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="imgPresupuesto" runat="server" ImageUrl="~/administrativo/propuestas2/images/ext/xls.gif"
                                                Style="width: 16px" />
                                        </td>
                                        <td align="left">
                                            &nbsp;<asp:Button ID="btn_MostrarPresupuesto" runat="server" Text="PRESUPUESTO POA"
                                                CssClass="agregar2" Width="160px" Height="22px" BackColor="White" BorderColor="White"
                                                Font-Bold="True" Font-Strikeout="False" Font-Underline="False" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    ShowHeader="False" DataKeyNames="nombre_apr" GridLines="Horizontal" ShowFooter="True">
                                    <RowStyle Height="30px" BorderStyle="None" />
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_apr" HeaderText="Codigo_apr" Visible="False">
                                            <HeaderStyle Width="0%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_apr" HeaderText="Archivo" Visible="False">
                                            <HeaderStyle Width="0%" />
                                            <ItemStyle Width="50%" />
                                        </asp:BoundField>
                                        <asp:ImageField DataImageUrlField="extension" DataImageUrlFormatString="../images/ext/{0}.gif"
                                            HeaderText="imagen" ConvertEmptyStringToNull="False">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:ImageField>
                                        <asp:BoundField DataField="descripcion_apr" HeaderText="descripcion">
                                            <HeaderStyle Width="70%" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowDeleteButton="True">
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="30px" />
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td colspan="1" rowspan="2" style="font-weight: bold; font-size: 8pt; color: black;
                                font-family: verdana" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-weight: normal; font-size: 8pt; color: black; font-family: verdana"
                                valign="top" class="style10">
                                &nbsp;
                            </td>
                            <td align="center" style="font-weight: normal; font-size: 8pt; width: 287px; color: black;
                                font-family: verdana" valign="top">
                                &nbsp;<asp:Label ID="LblMensaje" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold; font-size: 8pt; color: black; font-family: verdana">
                                <br />
                                <br />
                                &nbsp;
                            </td>
                            <td style="font-weight: 700">
                                <br />
                                <br />
                                <asp:Button ID="CmdVer" runat="server" Text="Ver" CssClass="agregar2" Width="79px"
                                    Visible="False" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="txtelegido" runat="server" />
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableViewState="False"
                        ShowMessageBox="True" ValidationGroup="agregar" />
                    <asp:HiddenField ID="txtTipo" runat="server" />
                    <asp:HiddenField ID="txtEstado" runat="server" />
                    <asp:HiddenField ID="txtMenu" runat="server" />
                    <asp:HiddenField ID="txtnombrearchivo" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table align="center">
                        <tr>
                            <td colspan="7">
                                <asp:GridView ID="dgv_PresupuestoIngreso" runat="server" Width="100%" AutoGenerateColumns="False"
                                    Visible="False" HorizontalAlign="Center" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad" />
                                        <asp:BoundField DataField="item" HeaderText="Item">
                                            <HeaderStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C2}">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="SubTotal" DataFormatString="{0:C2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        INGRESOS: NO SE HA REGISTRADO NINGUN ITEM EN PRESUPUESTO
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" />
                                    <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:GridView ID="dgv_Presupuesto" runat="server" Width="100%" AutoGenerateColumns="False"
                                    Visible="False" HorizontalAlign="Center" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad" />
                                        <asp:BoundField DataField="item" HeaderText="Item">
                                            <HeaderStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C2}">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="SubTotal" DataFormatString="{0:C2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="porcentaje" HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        EGRESOS: NO SE HA REGISTRADO NINGUN ITEM EN PRESUPUESTO
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" />
                                    <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
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
