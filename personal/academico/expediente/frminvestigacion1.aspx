<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminvestigacion1.aspx.vb" Inherits="Investigador_frminvestigacion1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones :: Nuevo</title>
    <link  href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../private/calendario.js"></script>
    <script language="JavaScript" src="../../../private/tooltip.js"></script>
</head>
<body >
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table style="width: 600px" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" rowspan="2" align="center" style="height: 436px">
                    <table cellpadding="0" cellspacing="0" class="contornotabla" style="width: 600px">
                        <tr>
                            <td colspan="3" rowspan="3" align="center">
                                <table style="width: 600px; height: 366px;" cellpadding="3" cellspacing="3">
                                    <tr>
                                        <td colspan="2" style="font-weight: bold; font-size: 12pt; color: maroon; font-family: verdana;
                                            height: 33px; text-align: center">
                                            <br />
                                            Registro de Investigaciones Anteriores o Externas a USAT<br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 2px">
                                            <hr style="color: maroon; height: 1px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 52px;">
                                            Título de 
                                            <br />
                                            Investigación</td>
                                        <td style="height: 52px">
                                            <asp:TextBox ID="TxtTitulo" runat="server" Width="427px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTitulo"
                                                ErrorMessage="Ingrese titulo de investigacion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 45px">
                                            Institución donde realizó la&nbsp;
                                            investigación</td>
                                        <td style="height: 45px">
                                            <asp:RadioButton ID="RbUSAT" runat="server" Checked="True" GroupName="institucion"
                                                Text="USAT" />&nbsp;
                                            <asp:RadioButton ID="RbOtros" runat="server" GroupName="institucion" Text="Otros" />
                                            <asp:TextBox ID="TxtInstitucion" runat="server" MaxLength="300" Style="border-right: black 1px solid;
                                                border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy;
                                                border-bottom: black 1px solid; font-family: verdana" Width="315px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 50px">
                                            &nbsp;Temática</td>
                                        <td style="height: 50px">
                                            <asp:DropDownList ID="DDLTematica" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" Width="427px">
                                            </asp:DropDownList>
                                            <br />
                                            &nbsp;(opcional)</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 62px">
                                            Tipo de Investigación</td>
                                        <td style="height: 62px">
                                            <asp:DropDownList ID="DDLTipo" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" Width="427px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLTipo"
                                                ErrorMessage="Seleccione Tipo de Investigación" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 51px">
                                            Fechas</td>
                                        <td style="height: 51px">
                                            &nbsp;Inicio :
                                            <input id="Button1" class="cunia" type="button" onclick="MostrarCalendario('TxtFecInicio'); return false;" />
                                            <asp:TextBox ID="TxtFecInicio" runat="server" Width="77px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" style="text-align: right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtFecInicio"
                                                ErrorMessage="Seleccione fecha de inicio" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                            &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; Término :
                                            <input id="Button2" class="cunia" type="button" onclick="MostrarCalendario('TxtFecFin'); return false;" />
                                            <asp:TextBox ID="TxtFecFin" runat="server" Width="77px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" style="text-align: right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtFecFin"
                                                ErrorMessage="Seleccione fecha de termino" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 63px">
                                            Informe de Investigación</td>
                                        <td style="height: 63px">
                                            <asp:FileUpload ID="FileInforme" runat="server" Width="427px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileInforme"
                                                ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                                SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileInforme"
                                                ErrorMessage="Debe seleccionar un archivo para subir como informe de investigación."
                                                SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                                            (Solo puede subir archivos con extension *.rar, *.zip, *.pdf, *.doc 
                                            <br />
                                            con un tamaño
                                            menor a 10 MB)</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px; height: 66px">
                                            Resumen de Investigación</td>
                                        <td style="height: 66px">
                                            <asp:FileUpload ID="FileResumen" runat="server" Width="427px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" /><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileResumen"
                                                ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                                SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="FileResumen" ErrorMessage="Debe seleccionar un archivo para subir como resumen de investigación."
                                                    SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                                            (Solo puede subir archivos con extension *.rar, *.zip, *.pdf, *.doc 
                                            <br />
                                            con un tamaño
                                            menor a 5 MB)</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px">
                                            Beneficiarios</td>
                                        <td>
                                            <asp:TextBox ID="TxtBeneficiarios" runat="server" Height="66px" TextMode="MultiLine" Width="404px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                    <asp:Label ID="lblMensjae" runat="server"></asp:Label></td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td align="right" colspan="2" rowspan="1">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2" rowspan="1">
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar_prp" Text="           Siguiente"
                        Width="100px" /></td>
            </tr>
        </table>
    
    </div>
        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    </form>
</body>
</html>
