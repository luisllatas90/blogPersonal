<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminvestigacionotros.aspx.vb" Inherits="Investigador_frminvestigacionotros" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones :: Nuevo</title>
    <link  href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/calendario.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
     <script type="text/javascript">
        function OcultarTabla()
        {
            if (document.form1.FileInforme.value!="" &&  document.form1.FileResumen.value!="")
            {
                document.all.tblDatos.style.display="none"
                document.all.tblmensaje.style.display=""                
           }
        }
     </script>
    <style type="text/css">
        .style1
        {
            height: 14px;
            width: 173px;
        }
        .style2
        {
            height: 27px;
            width: 173px;
        }
        .style3
        {
            width: 173px;
        }
        .style4
        {
            height: 22px;
            width: 173px;
        }
        .style5
        {
            height: 48px;
            width: 173px;
        }
        .style6
        {
            height: 45px;
            width: 173px;
        }
        .style7
        {
            height: 84px;
            width: 173px;
        }
    </style>
</head>
<body style="margin-top: 0px;" >
    <form id="form1" runat="server">
        <table style="width: 100%; height: 479px;" cellpadding="0" cellspacing="0" id="tblDatos">
            <tr>
                <td onclick="javascript:location.href='frminvestigacion1.aspx?id=<% response.write(request.querystring("id")) %>'" 
                    style="background-color: buttonhighlight; height: 29px; border-right: dimgray 1px solid; border-top: dimgray 1px solid; border-left: dimgray 1px solid; border-bottom: dimgray 1px solid; cursor:hand; border-style: none none solid none;"  
                    align="center">
                    INVESTIGACIONES INTERNAS</td>
                  <td style="background-color: #f0f0f0; height: 29px; border-right: dimgray 1px solid; border-top: dimgray 1px solid; border-left: dimgray 1px solid; font-weight: bold; color: highlight; width: 50%;" 
                    align="center">
                      INV. FINALIZADAS</td>
            </tr>
        <tr><td colspan="2" style="background-color: #f0f0f0; border-right: dimgray 1px solid; border-left: dimgray 1px solid; height: 15px;">
            <table style="width:100%;">
                <tr>
                    <td>
            <img height="17" src="../../../../images/help.gif" style="cursor: help" tooltip="<b>Registrar Investigaciones Finalizadas y/o Externas a USAT</b><br><b>Paso1</b> :<br> - Registre los datos generales de la investigación como el Título y en que Institución la desarrolló. <br> - Ingrese los dos archivos que corresponde al Informe y el Resumen de la Investigación.<br> - Finalmente debe registrar la cita de publicación, luego hacer click en Guardar y Siguiente. <br><b>Paso2 :</b><br> - Posteriormente debe de asignar al(los) autor(es) de la investigación, recuerde que por defecto UD. ES EL AUTOR PRINCIPAL."
                width="22" /><asp:Button ID="CmdGuardar" runat="server" CssClass="guardar_prp" Text="             Guardar y Siguiente"
                        Width="154px" OnClientClick="OcultarTabla()" />
                    </td>
                    <td style="font-family: verdana; font-size: 7pt; color: #FF0000; padding-left: 5pt; font-weight: bold; text-align: justify;">
                        Registrará sólo aquellas investigaciones que esten culminadas y/o publicadas 
                        realizadas tanto dentro de la institución como fuera de la misma.</td>
                </tr>
            </table>
                    </td></tr>
            <tr>
                <td colspan="2" align="center" valign="top">
                    <table cellpadding="0" cellspacing="0" class="contornotabla" style="width: 100%; height: 388px;">
                        <tr>
                            <td colspan="3" align="center" style="height: 394px" valign="top">
                                <table style="width: 100%; height: 353px;" cellpadding="3" cellspacing="3">
                                    <tr>
                                        <td colspan="2" style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana;
                                            height: 18px; text-align: center">
                                            Registro de Investigaciones Finalizadas</td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            Título de 
                                            Investigación</td>
                                        <td style="height: 14px">
                                            <asp:TextBox ID="TxtTitulo" runat="server" Width="437px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTitulo"
                                                ErrorMessage="Ingrese titulo de investigacion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            Institución</td>
                                        <td style="height: 27px">
                                            <asp:RadioButton ID="RbUSAT" runat="server" Checked="True" GroupName="institucion"
                                                Text="USAT" />&nbsp;
                                            <asp:RadioButton ID="RbOtros" runat="server" GroupName="institucion" Text="Otros" />
                                            <asp:TextBox ID="TxtInstitucion" runat="server" MaxLength="300" Style="border-right: black 1px solid;
                                                border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy;
                                                border-bottom: black 1px solid; font-family: verdana" Width="325px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            Zona de Influencia</td>
                                        <td valign="top">
                                            <asp:DropDownList ID="DDLAmbito" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana">
                                            <asp:ListItem Value="-1">-- Ambito --</asp:ListItem>
                                                <asp:ListItem Value="I">Internacional</asp:ListItem>
                                                <asp:ListItem Value="N">Nacional</asp:ListItem>
                                                <asp:ListItem Value="R">Regional</asp:ListItem>
                                                <asp:ListItem Value="L">Local</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLAmbito"
                                                ErrorMessage="Seleccione Ambito" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                            <asp:DropDownList ID="DDLPoblacion" runat="server" style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana">
                                                <asp:ListItem Value="-1">-- Poblaci&#243;n --</asp:ListItem>
                                                <asp:ListItem Value="U">Urbana</asp:ListItem>
                                                <asp:ListItem Value="R">Rural</asp:ListItem>
                                                <asp:ListItem Value="A">Ambos</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DDLPoblacion"
                                                ErrorMessage="Seleccione Poblacion" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator><br />
                                            Detalle
                                            <asp:TextBox ID="TxtDetalle" runat="server" MaxLength="300" Style="border-right: black 1px solid;
                                                border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy;
                                                border-bottom: black 1px solid; font-family: verdana" ToolTip="Pequeño detalle en zona de inflencia"
                                                Width="396px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="style4">
                                            Fechas</td>
                                        <td style="height: 22px">
                                            Inicio :
                                            <input id="Button2" class="cunia" type="button" 
                                                onclick="MostrarCalendario('TxtFecInicio'); return false;" />
                                            <asp:TextBox ID="TxtFecInicio" runat="server" Width="73px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" style="text-align: right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtFecInicio"
                                                ErrorMessage="Seleccione fecha de inicio" SetFocusOnError="True">*</asp:RequiredFieldValidator>&nbsp; 
                                            Duración :
                                            <asp:TextBox ID="TxtDuracion" runat="server" Width="28px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" style="text-align: right"></asp:TextBox>
                                            <asp:DropDownList ID="DDLDuracion" runat="server" 
                                                style="font-size: 8pt; text-transform: capitalize; color: navy; font-family: verdana" 
                                                Width="99px" Height="16px">
                      <asp:ListItem Value="-1">-- Duracion --</asp:ListItem>
                      <asp:ListItem Value="M">Meses</asp:ListItem>
                      <asp:ListItem Value="A">A&#241;os</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="DDLDuracion"
                                                ErrorMessage="Seleccione Duracion" Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            Informe de Investigación</td>
                                        <td style="height: 48px; color: red;">
                                            <asp:FileUpload ID="FileInforme" runat="server" Width="437px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileInforme"
                                                ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                                SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileInforme"
                                                ErrorMessage="Debe seleccionar un archivo para subir como informe de investigación."
                                                SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                                            (Solo puede grabar archivos con extension *.rar, *.zip, *.pdf, *.doc 
                                            <br />
                                            con un tamaño
                                            menor a 10 MB)</td>
                                    </tr>
                                    <tr>
                                        <td class="style6">
                                            Resumen de Investigación</td>
                                        <td style="height: 45px; color: red;">
                                            <asp:FileUpload ID="FileResumen" runat="server" Width="437px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy" /><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileResumen"
                                                ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                                SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="FileResumen" ErrorMessage="Debe seleccionar un archivo para subir como resumen de investigación."
                                                    SetFocusOnError="True">*</asp:RequiredFieldValidator><br />
                                            (Solo puede grabar archivos con extension *.rar, *.zip, *.pdf, *.doc 
                                            <br />
                                            con un tamaño
                                            menor a 5 MB)</td>
                                    </tr>
                                    <tr>
                                        <td class="style7">
                                            Citación de Publicación Ej.<br />
                                            <br />
                                            <span style="LETTER-SPACING: -0.15pt; font-family: verdana; font-size: 7pt; text-align: justify;">
                                            Maturana, J. y Vallejos, A. 2005. Base de datos: Teca en pequeñas fincas en 
                                            Java. CD. CIFOR. <span class="SpellE">Bogor</span>, Indonesia</span></td>
                                        <td style="height: 84px">
                                            <asp:TextBox ID="TxtCita" runat="server" Height="73px" TextMode="MultiLine" 
                                                Width="436px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
                                                Font-Names="Verdana" Font-Size="8pt" ForeColor="Navy"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="font-size: 8pt; color: navy; font-style: italic; font-family: verdana;
                                            height: 25px; text-align: center">
                                            * Recuerde que las investigaciones ingresadas en esta sección<br />
                                            se mostraran en Investigaciones de Tipo Informe - Finalizadas.</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </table>
                    <asp:Label ID="lblMensjae" runat="server"></asp:Label></td>
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

        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
        &nbsp; &nbsp;&nbsp;
    </form>
</body>
</html>
