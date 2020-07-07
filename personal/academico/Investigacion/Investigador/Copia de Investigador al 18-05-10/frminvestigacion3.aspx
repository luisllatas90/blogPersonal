<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminvestigacion3.aspx.vb" Inherits="Investigador_frminvestigacion3" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones :: Registrar Proyecto</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js" type="text/javascript"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function OcultarTabla()
            {
            if (document.form1.FileArchivo.value!="")
                {
                document.all.tblDatos.style.display="none"
                document.all.tblmensaje.style.display=""                
                }
            }
        
        function validadescripcion(source,arguments)
            
            {
                if (document.form1.TxtDesFin.style.visibility=='visible')
                        if (document.form1.TxtDesFin.value=='')
                            arguments.IsValid = false
                        else
                            arguments.IsValid = true
            }
        
        function MuestraCaja()
            {
                if (document.form1.DDLFinanciamiento.value=='E'||document.form1.DDLFinanciamiento.value=='C')
                    {
                    document.all.LblDesFin.style.visibility='visible';
                    document.form1.TxtDesFin.style.visibility='visible';
                    }
                else
                    {
                    document.all.LblDesFin.style.visibility='hidden'; 
                    document.form1.TxtDesFin.style.visibility='hidden';    
                    }
            }
     </script>
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
<body style="margin-top:0px">
    <form id="form1" runat="server">
        <table id="tblDatos" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="left" colspan="3" style="height: 34px; background-color: #f0f0f0">
                    &nbsp;
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar3" Text="     Guardar"
                        ToolTip="Guardar un proyecto de investigacion" Width="72px" /></td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <table class="contornotabla" style="width: 317px; height: 197px;">
                        <tr>
                            <td colspan="2" style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana;
                                height: 17px; text-align: center">
                                <asp:Label ID="LblTituloFrm" runat="server" Text="Registrar Proyecto de Investigación"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 23px">
                                &nbsp;Titulo</td>
                            <td align="left" style="width: 257px; height: 23px;">
                                <asp:Label ID="LblTitulo" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 13px">
                                &nbsp;Proyecto</td>
                            <td align="left" style="width: 257px; height: 13px;">
                                <asp:FileUpload ID="FileArchivo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Width="423px" Height="18px" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        runat="server" ControlToValidate="FileArchivo" ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                        SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileArchivo" ErrorMessage="Debe seleccionar un archivo para subir al servidor."
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 2px">
                                &nbsp;Costo</td>
                            <td align="left" style="width: 257px; height: 2px;">
                                <asp:TextBox ID="TxtCosto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Width="56px"  style="text-align: right" Height="19px" Font-Names="verdana" Font-Size="9pt"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtCosto"
                                    ErrorMessage="Ingrese costo de investigacion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 10px" >
                                &nbsp;Financiamiento</td>
                            <td align="left" style="height: 10px;" >
                                <asp:DropDownList ID="DDLFinanciamiento" runat="server" Width="358px">
                                    <asp:ListItem Value="N">--- Seleccione Tipo de Financiamiento ---</asp:ListItem>
                                    <asp:ListItem Value="A">Autofinanciado</asp:ListItem>
                                    <asp:ListItem Value="U">Financiado por Universidad</asp:ListItem>
                                    <asp:ListItem Value="E">Financiado por Entidad Externa</asp:ListItem>
                                    <asp:ListItem Value="C">Financiamiento Compartido</asp:ListItem>
                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server"
                                    ErrorMessage="Seleccione tipo de financiamiento" Operator="NotEqual" SetFocusOnError="True"
                                    ValueToCompare="N" ControlToValidate="DDLFinanciamiento">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 10px">
                                &nbsp;<asp:Label ID="LblDesFin" style="visibility:hidden" runat="server" Text="Descripcion Financiamiento"></asp:Label></td>
                            <td align="left" style="height: 10px">
                                <asp:TextBox ID="TxtDesFin" style="visibility:hidden" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    TextMode="MultiLine" Width="420px" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Ingrese Descripcion de Financiamiento" ClientValidationFunction="validadescripcion">*</asp:CustomValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
        
            <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img alt='imagen' border="0" src="../../../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    
    </form>
</body>
</html>
