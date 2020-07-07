<%@ Page Language="VB" AutoEventWireup="false" CodeFile="biblioteca.aspx.vb" Inherits="biblioteca_biblioteca" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>"</title>
    <link rel="STYLESHEET"  href="../../private/estilo.css" />
    <script type="text/ecmascript">
    function OcultarTabla()
            {
              document.all.tblDatos.style.display="none"
              document.all.tblmensaje.style.display=""              
            }
    </script>
</head>
<body style="margin:0,0,0,0; background-color:#F0F0F0"  >
    <form id="form1" runat="server" onsubmit="return OcultarTabla();">
    <div>
        <table id="tblDatos" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    Subir Catálogo</td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2" align="center" valign="middle">
                    &nbsp;<table style="border-right: black 1px solid; padding-right: 5px; border-top: black 1px solid;
                        padding-left: 5px; padding-bottom: 5px; margin: 5px; border-left: black 1px solid;
                        width: 90%; padding-top: 5px; border-bottom: black 1px solid" width="90%">
                        <tr>
                            <td colspan="3" rowspan="3" style="width: 90%">
                    <table width="100%" style="background-color: white">
                        <tr>
                            <td style="height: 35px">
                                &nbsp;&nbsp; Proveedor</td>
                            <td colspan="2" style="height: 35px">
                                <asp:DropDownList ID="DDLProveedor" runat="server" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="height: 47px">
                                &nbsp;&nbsp; Ruta Archivo</td>
                            <td colspan="2" style="height: 47px">
                                <asp:FileUpload ID="FileSubir" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileSubir"
                                    ErrorMessage="Solo puede subir archivos con extension *.xls" SetFocusOnError="True"
                                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.xls|.XLS)$">*</asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileSubir"
                                    Display="Dynamic" ErrorMessage="Seleccione un archivo para el envio." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Button ID="Button1" runat="server" Text="          Enviar" BorderStyle="Solid" BorderWidth="1px" CssClass="guardar_prp" Width="87px" /></td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
        <table id="tblmensaje" border="0" bordercolor="#111111" cellpadding="0" cellspacing="0"
            class="contornotabla" height="100%" style="display: none; border-collapse: collapse"
            width="100%">
            <tr>
                <td align="center" bgcolor="#feffff" class="usatEtiqOblig" width="100%">
                    Procesando<br />
                    Por favor espere un momento...<br />
                    <img border="0" height="20" src="../../images/cargando.gif" width="209" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
