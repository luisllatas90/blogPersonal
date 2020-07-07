<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pdp.aspx.vb" Inherits="personales" title="Campus Virtual :: Hoja de Vida" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
  <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
      <script type="text/javascript" src="private/expediente.js"></script>
    <style>
        .tab_pasar
        {
            font-weight: bold;
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_sobre.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .tab_normal
        {
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_normal.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
       
        .style1
        {
            width: 100%;
        }
       
        body{ font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: Black;font-size:8pt;
	font: normal;
	}
       
        .style2
        {
            font-size: 8pt;
            color: black;
            font-family: verdana;
            width: 87px;
        }
        .style3
        {
            height: 109px;
        }
        .style4
        {
            height: 86px;
        }
       
    </style>
<title>Campus Virtual : Hoja de Vida</title>
<script type="text/javascript">
function religion(){
if (eval("document.form1.DDLReligion.value=='Catolico'"))
    eval("document.form1.DDLSacramento.disabled=false")
else 
    eval("document.form1.DDLSacramento.disabled=true")
}
</script>


</head>
<body>
<div id="divmensaje"></div>
<script type="text/javascript" src="div.js"></script>

<form id="form1" runat="server">
<center>
    <table class="style1" width="100%" cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td width="75%" align="left" valign="top">
    <table cellpadding="0" cellspacing="0" border="0" 
                    style="border-right: 1px solid; border-top: goldenrod 1px solid; border-left: 1px solid; border-bottom: 1px solid" 
                    class="tabla_personal" align="left" width="100%">
        <tr>
            <td style="height: 29px;" class="titulo_tabla">
                &nbsp;Registro de plan de desarrollo personal PDP</td>
            <td style="height: 29px;" class="titulo_tabla" align="right">
                &nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td align="center" style="border-bottom: gold 1px solid; height: 29px" 
                colspan="2">
                &nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="style4">
                <table>
                    <tr>
                        <td style="border-color: black; border-width: 1px; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; "
                            valign="top" class="style3">
                <table style="width: 544px" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" class="style2">
                            Archivo PDP</td>
                        <td align="left" style="font-size: 10pt; color: darkred; font-family: Arial;
                            height: 21px">
                            &nbsp;
                            <asp:FileUpload ID="FuFoto" runat="server" Width="250px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" ToolTip="Fotografia" CssClass="datos_combo" /><asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" style="cursor: hand" /><span style="font-size: 8pt">
                            (*.doc,*.docx, .pdf. Hasta 1 Mb)</span>
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator2"
                                runat="server" 
                                ControlToValidate="FuFoto" 
                                ErrorMessage="Sólo puede ingresar archivos con extensiones *.doc y *.pdf"
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.doc|.docx)$">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="height: 2px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" style="height: 2px">&nbsp;</td>
                    </tr>
                    </table>
                            </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td align="center" style="border-top: gold 1px solid; height: 40px" colspan="2">
                <asp:ValidationSummary ID="ListaErrores" runat="server" ShowMessageBox="True" DisplayMode="List" ShowSummary="False" />
                &nbsp; &nbsp; &nbsp;<asp:Button ID="CmdGuardar" runat="server" CssClass="tab_normal"
                    Height="26px" Text="Guardar PDP" Width="86px" />&nbsp;
            </td>
        </tr>
    </table>
                </td>
            </tr>
        </table>
    </center>
   </form>
   </body>
   </html>
   


